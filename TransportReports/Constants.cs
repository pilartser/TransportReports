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
