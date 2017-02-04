using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransportReports
{
    class ReportUtils
    {
        public static void LoadReportList(TreeView tv)
        {
            ReportTreeNode root = new ReportTreeNode("Отчеты", ReportType.None);
            root.Nodes.AddRange(new TreeNode[]
            {
                new ReportTreeNode("Отчет по фактически совершенным поездкам", ReportType.ActivePass),
                new ReportTreeNode("Отчет по льготникам", ReportType.Privilege),
                new ReportTreeNode("Отчет по маршруту", ReportType.Route),
                new ReportTreeNode("Отчет по организации", ReportType.Organisation),
                new ReportTreeNode("Отчет по терминалу кондуктора", ReportType.Terminal),
                new ReportTreeNode("Отчет по транзакциям", ReportType.Transaction),
                new ReportTreeNode("Отчет по транспортной карте", ReportType.TransportCard),
                new ReportTreeNode("Отчет по транспортному средству", ReportType.TransportVehicle)
            });
            root.Expand();
            tv.Nodes.Add(root);
            tv.SelectedNode = root;
            tv.Focus();
        }
    }
}
