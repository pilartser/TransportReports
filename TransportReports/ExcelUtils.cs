using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using OfficeOpenXml.Style;
using Excel = Microsoft.Office.Interop.Excel;

namespace TransportReports
{
    internal class ExcelUtils
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
                modulo = (dividend - 1)%26;
                columnName.Insert(0, Convert.ToChar(65 + modulo));
                dividend = (int) ((dividend - modulo)/26);
            }
            return columnName.ToString();
        }

        public static void OutloadExcel(string pathTemplate, string pathOutput, DataTable dtRows, DataTable dtFormat,
            bool isColorize, bool isOpenAfterCreate)
        {
            Excel.Application exApp = new Excel.Application();
            try
            {
                exApp.Visible = false;
                //exApp.TemplatesPath = Path.GetDirectoryName(pathTemplate);
                exApp.Workbooks.Add(pathTemplate);
                Excel.Workbook exWorkBook = exApp.Workbooks[1];

                foreach (DataRow row in dtFormat.Rows)
                {
                    int listNum = Routines.GetInt(row["list_num"]);
                    if (listNum > exWorkBook.Sheets.Count)
                    {
                        MessageBox.Show($"Прерывание формирования excel. Лист с номером {listNum} отсутствует.");
                    }
                    Excel.Worksheet exSheet = (Excel.Worksheet) exWorkBook.Sheets.Item[listNum];
                    string cellAddress = Routines.GetString(row["range"]);
                    Excel.Range cells = exSheet.Range[cellAddress, Type.Missing];
                    cells.Font.Name = "Arial";
                    int? cellFontSize = Routines.TryGetInt(row["font_size"]);
                    cells.Font.Size = cellFontSize ?? 8;
                    int? cellBorderLineStyle = Routines.TryGetInt(row["border"]);
                    if (cellBorderLineStyle != null) cells.Borders.LineStyle = cellBorderLineStyle;
                    if ("Y".Equals(Routines.GetString(row["is_merged"])))
                        cells.Merge();
                }

                foreach (DataRow row in dtRows.Rows)
                {
                    int listNum = Routines.GetInt(row["list_num"]);
                    if (listNum > exWorkBook.Sheets.Count)
                    {
                        MessageBox.Show($"Прерывание формирования excel. Лист с номером {listNum} отсутствует.");
                    }
                    Excel.Worksheet exSheet = (Excel.Worksheet) exWorkBook.Sheets.Item[listNum];
                    string cellAddress = Routines.GetString(row["col_name"]) + Routines.GetString(row["row_num"]);
                    Excel.Range cells = exSheet.Range[cellAddress, Type.Missing];
                    string cellValue = Routines.GetString(row["value"]);
                    if ((cellValue.Length > 0) && (cellValue[0] == '='))
                    {
                        if (isColorize) cells.Interior.Color = Color.LightBlue;
                        cells.Formula = cellValue;
                    }
                    else
                    {
                        if (isColorize) cells.Interior.Color = Color.Yellow;
                        cells.Value2 = cellValue;
                    }
                    cells.Rows.AutoFit();
                }


                exWorkBook.SaveAs(pathOutput);
                exApp.Quit();
                if (isOpenAfterCreate)
                {
                    Excel.Application createdExcel = new Excel.Application();
                    createdExcel.Visible = false;
                    createdExcel.Workbooks.Open(pathOutput);
                    createdExcel.Visible = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                exApp.Quit();
            }
        }

        public static void OutloadExcelEpplus(string pathTemplate, string pathOutput, DataTable dtRows,
            DataTable dtFormat, bool isColorize, bool isOpenAfterCreate)
        {
            try
            {
                //exApp.TemplatesPath = Path.GetDirectoryName(pathTemplate);
                using (ExcelPackage pack = new ExcelPackage(new FileInfo(pathOutput), new FileInfo(pathTemplate)))
                {
                    foreach (DataRow row in dtFormat.Rows)
                    {
                        int listNum = Routines.GetInt(row["list_num"]);
                        if (listNum > pack.Workbook.Worksheets.Count)
                        {
                            MessageBox.Show($"Прерывание формирования excel. Лист с номером {listNum} отсутствует.");
                        }
                        ExcelWorksheet ws = pack.Workbook.Worksheets[listNum];
                        string cellAddress = Routines.GetString(row["range"]);
                        ExcelRange er = ws.Cells[cellAddress];
                        er.Style.Font.Name = "Arial";
                        int? cellFontSize = Routines.TryGetInt(row["font_size"]);
                        er.Style.Font.Size = cellFontSize ?? 8;
                        int? cellBorderLineStyle = Routines.TryGetInt(row["border"]);
                        if (cellBorderLineStyle != null)
                            er.Style.Border.Top.Style =
                                er.Style.Border.Right.Style =
                                    er.Style.Border.Bottom.Style = er.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        if ("Y".Equals(Routines.GetString(row["is_merged"])))
                            er.Merge = true;
                    }

                    foreach (DataRow row in dtRows.Rows)
                    {
                        int listNum = Routines.GetInt(row["list_num"]);
                        if (listNum > pack.Workbook.Worksheets.Count)
                        {
                            MessageBox.Show($"Прерывание формирования excel. Лист с номером {listNum} отсутствует.");
                        }
                        ExcelWorksheet ws = pack.Workbook.Worksheets[listNum];
                        string cellAddress = Routines.GetString(row["col_name"]) + Routines.GetString(row["row_num"]);
                        ExcelRange er = ws.Cells[cellAddress];
                        string cellValue = Routines.GetString(row["value"]);
                        if ((cellValue.Length > 0) && (cellValue[0] == '='))
                        {
                            if (isColorize)
                            {
                                var fill = er.Style.Fill;
                                fill.PatternType = ExcelFillStyle.Solid;
                                fill.BackgroundColor.SetColor(Color.LightBlue);
                            }
                            er.Formula = cellValue;
                        }
                        else
                        {
                            if (isColorize)
                            {
                                var fill = er.Style.Fill;
                                fill.PatternType = ExcelFillStyle.Solid;
                                fill.BackgroundColor.SetColor(Color.Yellow);
                            }
                            float cellFloatValue;
                            if (float.TryParse(cellValue, out cellFloatValue))
                                er.Value = cellFloatValue;
                            else
                                er.Value = cellValue;
                        }
                        //er.Style.WrapText = true; //cells.Rows.AutoFit();
                    }
                    pack.Workbook.Calculate();
                    pack.Save();
                    pack.Dispose();
                }
                if (!isOpenAfterCreate) return;
                Excel.Application createdExcel = new Excel.Application {Visible = false};
                createdExcel.Workbooks.Open(pathOutput);
                createdExcel.Visible = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //exApp.Quit();
            }
        }
    }
}
