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
            AND div.id_operator = c.id_operator";
    }
}
