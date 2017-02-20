using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace TransportReports
{
    public partial class Main : Form
    {
        private string _database;
        private string _login;
        private string _password;
        private int _threadCount = 0;
        private int _threadNum = 0;
        private bool _isEditorMode;
        private readonly OracleConnection _connection;

        public Main(string database, string login, string password)
        {
            InitializeComponent();

            _database = database;
            _login = login;
            _password = password;
            _connection = DatabaseUtils.CreateConnection(database, login, password);
        }

        public Main(string database, string login, string password, string isEditorMode)
            : this(database, login, password)
        {
            _isEditorMode = "0".Equals(isEditorMode);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ReportType type = ((ReportTreeNode) tvReports.SelectedNode).Type;
            if (CalcData(type))
            {
                if (type == ReportType.Route)
                {
                    OracleDataReader reader = DatabaseUtils.GetReader(_connection, Constants.ConstGetRouteList);
                    if ((reader == null) || (!reader.HasRows)) return;
                    DateTime timeBegin = DateTime.Now;
                    while (reader.Read())
                    {
                        long id = Routines.GetLong(reader["id_element"]);
                        string name = Routines.GetString(reader["name_element"]);
                        Thread t = new Thread(() =>
                        {
                            while (true)
                            {
                                lock (this)
                                {
                                    if (_threadCount < 4)
                                    {
                                        _threadCount++;
                                        _threadNum++;
                                        break;
                                    }
                                }
                                Thread.Sleep(50);
                            }
                            CalcOutput(type, id, name);
                            lock (this)
                            {
                                _threadCount--;
                            }
                            if (_threadCount == 0)
                                MessageBox.Show($"Выгрузилось {_threadNum} отчетов за {(DateTime.Now-timeBegin).ToString(@"dd\.hh\:mm\:ss")}!");
                        });
                        t.IsBackground = true;
                        t.Start();
                    }
                }
                else
                {
                    CalcOutput(type);
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text += $" ({Application.ProductVersion})";
            SetDefaultValues();
            ReportUtils.LoadReportList(tvReports);
        }

        private bool CalcData(ReportType type)
        {
            switch (type)
            {
                case ReportType.Route:
                    return DatabaseUtils.CallProcedure(_connection, "cptt.pkg$trep_reports.fillpassroutetermday", GetOracleParameters(type));
                case ReportType.ActivePass:
                case ReportType.ActivePassRegional:
                case ReportType.Privilege:
                case ReportType.Organisation:
                case ReportType.Terminal:
                case ReportType.Transaction:
                case ReportType.TransportCard:
                case ReportType.TransportVehicle:
                case ReportType.None:
                default:
                    return true;
            }
        }

        private bool CalcOutput(ReportType type, long idElement = -1, string nameElement = "")
        {
            OracleConnection conn = null;
            try
            {
                string templateName = "";
                string outputName = "";
                string outputProcName = "";
                bool isColorize = chbColorizeExcelReport.Checked;
                bool isOpenAfterCreate = chbOpenExcelReport.Checked;
                OracleParameter[] parameters = {};
                switch (type)
                {
                    case ReportType.ActivePass:
                        templateName = "ActivePass.xlsx";
                        outputName = $@"Отчет инвеcтора-оператора_{dtActivePassPassBeginDate.Value.ToString("ddMMyyyy")}_{dtActivePassPassEndDate.Value.ToString("ddMMyyyy")}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx";

                        outputProcName = "cptt.pkg$trep_reports.fillReportActivePassExcel";
                        parameters = GetOracleParameters(type);
                        break;
                    case ReportType.ActivePassRegional:
                        templateName = "ActivePass.xlsx";
                        outputName = $@"Отчет инвеcтора-оператора(развернутые региональные льготники)_{dtActivePassPassBeginDate.Value.ToString("ddMMyyyy")}_{dtActivePassPassEndDate.Value.ToString("ddMMyyyy")}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx";

                        outputProcName = "cptt.pkg$trep_reports.fillReportActivePassExcel";
                        parameters = GetOracleParameters(type);
                        break;
                    case ReportType.Transaction:
                        templateName = "Transaction.xlsx";
                        outputName = $@"Отчет по транзакциям_{dtPassPassBeginDate.Value.ToString("ddMMyyyy")}_{dtPassPassEndDate.Value.ToString("ddMMyyyy")}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx";
                        outputProcName = "cptt.pkg$trep_reports.fillReportTransactionExcel";
                        parameters = GetOracleParameters(type);
                        break;
                    case ReportType.Privilege:
                        templateName = "Privilege.xlsx";
                        outputName = $@"Отчет по льготникам_{dtActivePassPassBeginDate.Value.ToString("ddMMyyyy")}_{dtActivePassPassEndDate.Value.ToString("ddMMyyyy")}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx";

                        outputProcName = "cptt.pkg$trep_reports.fillReportPrivilegeExcel";
                        parameters = GetOracleParameters(type);
                        break;
                    case ReportType.Route:
                        templateName = "Route.xltx";
                        outputName = $@"Отчет по маршруту_{nameElement}_{dtActivePassPassBeginDate.Value.ToString("ddMMyyyy")}_{dtActivePassPassEndDate.Value.ToString("ddMMyyyy")}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx";
                        outputProcName = "cptt.pkg$trep_reports.fillReportRouteExcel";
                        parameters = new[]
                        {
                            new OracleParameter()
                            {
                                ParameterName = "pIdRoute", OracleDbType = OracleDbType.Int64, Value = idElement
                            }
                        }.Concat(GetOracleParameters(type)).ToArray();
                        break;
                    case ReportType.Organisation:
                    case ReportType.Terminal:

                    case ReportType.TransportCard:
                    case ReportType.TransportVehicle:
                    case ReportType.None:
                    default:
                        return false;
                }
                var templatePath = Path.Combine(Application.StartupPath, "Template", templateName);
                var outputPath = Path.Combine(Application.StartupPath, "Output", outputName);
                if (!File.Exists(templatePath))
                {
                    MessageBox.Show(this, "Не найден файл шаблона", "Ошибка загрузки шаблона", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (!Directory.Exists(Path.GetDirectoryName(outputPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
                conn = DatabaseUtils.CreateConnection(_database, _login, _password);
                if (!DatabaseUtils.CallProcedure(conn, outputProcName, parameters)) return false;
                DataTable dtRows = DatabaseUtils.FillDataTable(conn, Constants.ConstGetExcelReportRows);
                DataTable dtFormat = DatabaseUtils.FillDataTable(conn, Constants.ConstGetExcelReportFormat);
                ExcelUtils.OutloadExcel(templatePath, outputPath, dtRows, dtFormat, isColorize, isOpenAfterCreate);
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"При формировании выгрузки Excel произошла ошибка:\r\n{e.Message}");
                conn?.Close();
                return false;
            }
        }

        private void SetDefaultValues()
        {
            //Отчет инвестора-оператора
            dtActivePassActivationBeginDate.Value = new DateTime(2016, 11, 13);
            dtActivePassActivationEndDate.Value = new DateTime(2016, 12, 12);
            dtActivePassPassBeginDate.Value = new DateTime(2016, 12, 1, 3, 0, 0);
            dtActivePassPassEndDate.Value = new DateTime(2017, 1, 1, 3, 0, 0);
            //отчет по транзакциям
            dtPassPassBeginDate.Value = new DateTime(2016, 12, 1, 3, 0, 0);
            dtPassPassEndDate.Value = new DateTime(2017, 1, 1, 3, 0, 0);
        }

        private void ShowTab(ReportType type)
        {
            TabPage tp;
            switch (type)
            {
                case ReportType.ActivePass:
                case ReportType.ActivePassRegional:
                case ReportType.Privilege:
                    tp = tpActivePass;
                    break;
                case ReportType.Transaction:
                case ReportType.Route:
                    tp = tpPass;
                    break;
                case ReportType.Organisation:
                case ReportType.Terminal:
                case ReportType.TransportCard:
                case ReportType.TransportVehicle:
                case ReportType.None:
                default:
                    tp = tpEmpty;
                    break;
            }
            btnRun.Enabled = !tpEmpty.Equals(tp);
            tcReportParams.SelectedTab = tp;
            tvReports.Focus();
        }

        private void tvReports_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowTab(((ReportTreeNode) tvReports.SelectedNode).Type);
        }

        private void tvReports_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            SolidBrush selectedTreeBrush = new SolidBrush(e.Node.TreeView.BackColor);
            if (e.Node == e.Node.TreeView.SelectedNode)
            {
                e.Graphics.FillRectangle(selectedTreeBrush, e.Bounds);
                ControlPaint.DrawBorder(e.Graphics, e.Bounds, Color.Black, ButtonBorderStyle.Dashed);
                //DrawFocusRectangle(e.Graphics, e.Bounds, e.Node.ForeColor, SystemColors.Highlight);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.TreeView.Font, e.Bounds, e.Node.ForeColor, TextFormatFlags.GlyphOverhangPadding);
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private OracleParameter[] GetOracleParameters(ReportType type)
        {
            switch (type)
            {
                case ReportType.ActivePass:
                case ReportType.ActivePassRegional:
                case ReportType.Privilege:
                    return new[]
                    {
                        new OracleParameter
                        {
                            ParameterName = "pActivationBeginDate", OracleDbType = OracleDbType.Date, Value = dtActivePassActivationBeginDate.Value
                        },
                        new OracleParameter
                        {
                            ParameterName = "pActivationEndDate", OracleDbType = OracleDbType.Date, Value = dtActivePassActivationEndDate.Value
                        },
                        new OracleParameter
                        {
                            ParameterName = "pPassBeginDate", OracleDbType = OracleDbType.Date, Value = dtActivePassPassBeginDate.Value
                        },
                        new OracleParameter
                        {
                            ParameterName = "pPassEndDate", OracleDbType = OracleDbType.Date, Value = dtActivePassPassEndDate.Value
                        },
                    };
                case ReportType.Route:
                case ReportType.Transaction:
                    return new[]
                    {
                        new OracleParameter
                        {
                            ParameterName = "pPassBeginDate", OracleDbType = OracleDbType.Date, Value = dtActivePassPassBeginDate.Value
                        },
                        new OracleParameter
                        {
                            ParameterName = "pPassEndDate", OracleDbType = OracleDbType.Date, Value = dtActivePassPassEndDate.Value
                        },
                    };
                case ReportType.Organisation:
                case ReportType.Terminal:
                case ReportType.TransportCard:
                case ReportType.TransportVehicle:
                case ReportType.None:
                default:
                    return new OracleParameter[] {};
            }
        }
    }
}
