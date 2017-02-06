using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace TransportReports
{
    public partial class Main : Form
    {

        private string _database;
        private string _login;
        private string _password;
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
            TryCalc(((ReportTreeNode) tvReports.SelectedNode).Type);
            //CallActivePassCalc(new Dictionary<string, DateTime>
            //{
            //    {"pActivationBeginDate", dtActivationBeginDate.Value},
            //    {"pActivationEndDate", dtActivationEndDate.Value},
            //    {"pPassBeginDate", dtPassBeginDate.Value},
            //    {"pPassEndDate", dtPassEndDate.Value}
            //},
            //    $@"Output\Отчет инвеcтора-оператора_{dtPassBeginDate.Value.ToString("ddMMyyyy")}_{
            //        dtPassEndDate.Value.ToString("ddMMyyyy")}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx");
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text += $" ({Application.ProductVersion})";
            SetDefaultValues();
            ReportUtils.LoadReportList(tvReports);
        }

        private bool TryCalc(ReportType type)
        {
            try
            {
                string templateName = "";
                string outputName = "";
                string procName = "";
                OracleParameter[] parameters = { };
                switch (type)
                {
                    case ReportType.ActivePass:
                        templateName = "ActivePass.xlsx";
                        outputName =
                            $@"Отчет инвеcтора-оператора_{dtPassBeginDate.Value.ToString("ddMMyyyy")}_{
                                dtPassEndDate.Value.ToString("ddMMyyyy")}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx";

                        procName = "cptt.pkg$trep_reports.fillReportActivePassExcel";
                        parameters = new[]
                        {
                        new OracleParameter{ParameterName = "pActivationBeginDate", OracleDbType = OracleDbType.Date, Value = dtActivationBeginDate.Value},
                        new OracleParameter{ParameterName = "pActivationEndDate", OracleDbType = OracleDbType.Date, Value = dtActivationEndDate.Value},
                        new OracleParameter{ParameterName = "pPassBeginDate", OracleDbType = OracleDbType.Date, Value = dtPassBeginDate.Value},
                        new OracleParameter{ParameterName = "pPassEndDate", OracleDbType = OracleDbType.Date, Value = dtPassEndDate.Value},
                    };
                        break;
                    case ReportType.Privilege:
                    case ReportType.Route:
                    case ReportType.Organisation:
                    case ReportType.Terminal:
                    case ReportType.Transaction:
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
                    MessageBox.Show(this, "Не найден файл шаблона", "Ошибка загрузки шаблона", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
                if (!Directory.Exists(Path.GetDirectoryName(outputPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
                if (!DatabaseUtils.CallProcedure(_connection,
                    procName,
                    parameters
                    )) return false;
                DataTable dtRows = DatabaseUtils.FillDataTable(_connection, Constants.ConstGetExcelReportRows);
                DataTable dtFormat = DatabaseUtils.FillDataTable(_connection, Constants.ConstGetExcelReportFormat);
                ExcelUtils.OutloadExcel(templatePath, outputPath, dtRows, dtFormat);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"При формировании выгрузки Excel произошла ошибка:\r\n{e.Message}");
                return false;
            } 
        }

        private void SetDefaultValues()
        {
            dtActivationBeginDate.Value = new DateTime(2016, 11, 13);
            dtActivationEndDate.Value = new DateTime(2016, 12, 12);
            dtPassBeginDate.Value = new DateTime(2016, 12, 1, 3, 0, 0);
            dtPassEndDate.Value = new DateTime(2017, 1, 1, 3, 0, 0);
        }

        private void ShowTab(ReportType type)
        {
            TabPage tp;
            switch (type)
            {
                case ReportType.ActivePass:
                
                    tp = tpDateActivePass;
                    break;
                case ReportType.Privilege:
                case ReportType.Route:
                case ReportType.Organisation:
                case ReportType.Terminal:
                case ReportType.Transaction:
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
    }
}
