using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TransportReports
{
    class ReportTreeNode: TreeNode
    {
        public ReportType Type;

        public ReportTreeNode(string text, ReportType type, Color color)
        {
            this.Text = text;
            this.Type = type;
            this.ForeColor = color;
        }
    }
}
