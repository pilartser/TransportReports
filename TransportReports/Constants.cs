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
                     is_merged
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
