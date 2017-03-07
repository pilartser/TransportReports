using System.Drawing;
using System.Windows.Forms;

namespace TransportReports
{
    class ReportUtils
    {
        public static void LoadReportList(TreeView tv)
        {
            ReportTreeNode root = new ReportTreeNode("Отчеты", ReportType.None, Color.Black);
            root.Nodes.AddRange(new TreeNode[]
            {
                new ReportTreeNode("Отчет по Инвестора-Оператора", ReportType.ActivePass, Color.DarkGreen),
                new ReportTreeNode("Отчет по Инвестора-Оператора (развернутые региональные льготники)", ReportType.ActivePassRegional, Color.DarkGreen),
                new ReportTreeNode("Отчет по льготникам", ReportType.Privilege, Color.DarkGreen),
                new ReportTreeNode("Отчет по маршруту", ReportType.Route, Color.DarkGreen),
                new ReportTreeNode("Отчет по организации", ReportType.Organisation, Color.DarkRed),
                new ReportTreeNode("Отчет по терминалу кондуктора", ReportType.Terminal, Color.DarkGreen),
                new ReportTreeNode("Отчет по транзакциям", ReportType.Transaction, Color.DarkGreen),
                new ReportTreeNode("Отчет по транспортной карте", ReportType.TransportCard, Color.DarkRed),
                new ReportTreeNode("Отчет по транспортному средству", ReportType.TransportVehicle, Color.DarkRed)
            });
            root.Expand();
            tv.Nodes.Add(root);
            tv.SelectedNode = root;
            tv.Focus();
        }

        public static string GetReaderQuery(ReportType type)
        {
            switch (type)
            {
                case ReportType.Route:
                    return Constants.ConstGetRouteList;
                case ReportType.Terminal:
                    return Constants.ConstGetTermList;
                default:
                    return "";
            }
        }
    }
}
