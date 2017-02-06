using System;
using System.Data;
using System.Drawing;
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

        public static void OutloadExcel(string pathTemplate, string pathOutput, DataTable dtRows, DataTable dtFormat)
        {
            Excel.Application exApp = new Excel.Application();
            try
            {
                exApp.Visible = false;
                exApp.Workbooks.Open(pathTemplate);
                Excel.Workbook exWorkBook = exApp.Workbooks[1];
                foreach (DataRow row in dtRows.Rows)
                {
                    int listNum = Routines.GetInt(row["list_num"]);
                    if (listNum > exWorkBook.Sheets.Count)
                    {
                        MessageBox.Show($"Прерывание формирования excel. Лист с номером {listNum} отсутствует.");
                    }
                    Excel.Worksheet exSheet = (Excel.Worksheet)exWorkBook.Sheets.Item[listNum];
                    string cellAddress = Routines.GetString(row["col_name"]) + Routines.GetString(row["row_num"]);
                    Excel.Range cells = exSheet.Range[cellAddress, Type.Missing];
                    string cellValue = Routines.GetString(row["value"]);
                    if ((cellValue.Length > 0) && (cellValue[0] == '='))
                    {
                        cells.Interior.Color = Color.LightBlue;
                        cells.Formula = cellValue;
                    }
                    else
                    {
                        cells.Interior.Color = Color.Yellow;
                        cells.Value2 = cellValue;
                    }
                    cells.Font.Name = "Arial";
                    cells.Font.Size = 8;
                    cells.Rows.AutoFit();
                }

                foreach (DataRow row in dtFormat.Rows)
                {
                    int listNum = Routines.GetInt(row["list_num"]);
                    if (listNum > exWorkBook.Sheets.Count)
                    {
                        MessageBox.Show($"Прерывание формирования excel. Лист с номером {listNum} отсутствует.");
                    }
                    Excel.Worksheet exSheet = (Excel.Worksheet)exWorkBook.Sheets.Item[listNum];
                    string cellAddress = Routines.GetString(row["range"]);
                    Excel.Range cells = exSheet.Range[cellAddress, Type.Missing];
                    int? cellFontSize = Routines.TryGetInt(row["font_size"]);
                    if (cellFontSize != null) cells.Font.Size = cellFontSize;
                    int? cellBorderLineStyle = Routines.TryGetInt(row["border"]);
                    if (cellBorderLineStyle != null) cells.Borders.LineStyle = cellBorderLineStyle;
                }
                exWorkBook.SaveAs(pathOutput);
                exApp.Visible = true;
                //exApp.Quit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                exApp.Quit();
            }
        }
    }
}
