using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransportReports
{
    class ReportTreeNode: TreeNode
    {
        public ReportType Type;

        public ReportTreeNode(string text, ReportType type)
        {
            this.Text = text;
            this.Type = type;
        }
    }
}
