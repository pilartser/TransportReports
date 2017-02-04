namespace TransportReports
{
    class Constants
    {
        public static readonly string ConstGetExcelReportRows =
            @"SELECT VALUE,
                     list_num,
                     row_num,
                     col_num
            FROM cptt.TMP$TREP_REPORT_EXCEL
            ORDER BY list_num,
                     row_num,
                     col_num";
    }
}
