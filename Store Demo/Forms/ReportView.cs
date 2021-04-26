using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Food_Cost.Report
{
    public partial class ReportView : Form
    {
        public ReportView()
        {
            InitializeComponent();

        }
        public CrystalDecisions.CrystalReports.Engine.ReportDocument Rpt;


        private void ReportView_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = Rpt;
        }

        private void ReportView_Load_1(object sender, EventArgs e)
        {
            crystalReportViewer2.ReportSource = Rpt;
        }
    }
}
