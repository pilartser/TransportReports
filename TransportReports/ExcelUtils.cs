using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace TransportReports
{
    class ExcelUtils
    {
        public static string GetCellAddress(int row, int column)
        {
            return GetExcelColumnName(column) + row;
        }

        private static string GetExcelColumnName(int column)
        {
            int dividend = column;
            StringBuilder columnName = new StringBuilder();
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName.Insert(0, Convert.ToChar(65 + modulo));
                dividend = (int)((dividend - modulo) / 26);
            }
            return columnName.ToString();
        }

        public static void OutloadExcel(string pathTemplate, string pathOutput, DataTable dt)
        {
            Excel.Application exApp = new Excel.Application();
            exApp.Visible = false;
            exApp.Workbooks.Open(pathTemplate);
            Excel.Workbook exWorkBook = exApp.Workbooks[1];
            foreach (DataRow row in dt.Rows)
            {
                int listNum = Routines.GetInt(row["list_num"]);
                if (listNum > exWorkBook.Sheets.Count)
                {
                    MessageBox.Show($"Прерывание формирования excel. Лист с номером {listNum} отсутствует.");
                }
                Excel.Worksheet exSheet = (Excel.Worksheet)exWorkBook.Sheets.Item[listNum];
                Excel.Range cells = exSheet.Range[GetCellAddress(Routines.GetInt(row["row_num"]),
                    Routines.GetInt(row["col_num"])), Type.Missing];
                cells.Value2 = Routines.GetString(row["Value"]);
            }
            exWorkBook.SaveAs(pathOutput);
            exApp.Visible = true;
            //exApp.Quit();
        }
    }
}
