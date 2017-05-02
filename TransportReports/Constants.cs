using System.Security.Cryptography;

namespace TransportReports
{
    class Constants
    {
        public static readonly string ConstGetExcelReportRows =
            @"SELECT list_num,
                     row_num,
                     col_name,
                     value
            FROM cptt.TMP$TREP_REPORT_EXCEL
            ORDER BY list_num,
                     row_num,
                     col_name";

        public static readonly string ConstGetExcelReportFormat =
            @"SELECT list_num,
                     range,
                     font_size,
                     border,
                     is_merged,
                     is_colored
            FROM cptt.tmp$trep_report_excel_format
            ORDER BY list_num";

        public static readonly string ConstGetRouteList =
            @"SELECT r.id AS id_element,
                   REPLACE(c.operator_name, ' ', '_') || '_' || r.code AS name_element
            FROM ROUTE r,
                 division             div,
                 cptt.v$trep_carriers c
            WHERE r.id_division = div.id
            AND div.id_operator = c.id_operator
            AND r.id != 900246845";

        public static readonly string ConstGetTermList =
            @"SELECT trm.id AS id_element,
                   trm.code AS name_element
            FROM(SELECT DISTINCT id_term FROM cptt.tmp$trep_data_terminal) pre_trm,
                 cptt.term trm
            WHERE pre_trm.id_term = trm.id
            --AND trm.id = 25300246845
            ORDER BY trm.id";

        public static readonly string ConstGetTransportVehicleList =
            @"SELECT v.id AS id_element,
                   REPLACE(op.name, ' ', '_') || '_' || v.code AS name_element
            FROM cptt.vehicle  v,
                 cptt.division div,
                 cptt.operator op
            WHERE v.id_division = div.id
            AND div.id_operator = op.id
            AND op.id NOT IN (SELECT id FROM cptt.ref$trep_agents_locked)
            AND op.role = 1
                 --AND v.id = 139700246845
            AND v.code NOT IN (':')
            AND EXISTS
             (SELECT 1 FROM cptt.tmp$trep_data td WHERE td.id_vehicle = v.id)";

        public static readonly string ConstGetTransportCardList =
            @"SELECT DISTINCT card_num as id_element,
                              cptt.num_to_ean(card_num) as name_element
            FROM cptt.t_data   trans,
                 cptt.division div
            WHERE trans.kind IN (7, 8, 10, 11, 12, 13, 37) --активация
            AND trans.d = 0 -- не удален
            AND trunc(trans.date_of) >= :pActivationBeginDate
            AND trunc(trans.date_of) <= :pActivationEndDate
            AND trans.id_division = div.id
            AND div.id_operator NOT IN(SELECT id FROM cptt.ref$trep_agents_locked)
            --AND card_num IN ('0020025798', '0150004801', '0150004292')
            ";

        public static readonly string ConstGetOrganisationList =
            @"SELECT DISTINCT id_operator AS id_element,
                              op.name     AS name_element
                FROM cptt.TMP$TREP_PASS_SERIESPRIVOP spo,
                     cptt.operator                   op
                WHERE spo.id_operator = op.id";

        public static string ConstGetLockedAgentsList =
            @"SELECT op.id,
                   NAME,
                   decode(role, 1, 'Перевозчик', 2, 'Агент') as role_name,
                   decode(tal.id, NULL, 'N', 'Y') AS is_locked
            FROM cptt.operator               op,
                 cptt.REF$TREP_AGENTS_LOCKED tal
            WHERE op.id = tal.id(+)
            ORDER BY role DESC, name ASC";

    }
}
