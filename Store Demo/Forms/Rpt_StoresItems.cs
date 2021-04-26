using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food_Cost.Report;
using System.Windows.Forms;

namespace Food_Cost.Forms
{
    public partial class Rpt_StoresItems : Form
    {
        public Rpt_StoresItems()
        {
            InitializeComponent();
        }
        string Where = "";
        string Filter = "";
        string s = "";
        string f = "";
        string s2 = "";
        DataTable Dt;
        DataTable Dt2;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        Dictionary<string, string> Stores = new Dictionary<string, string>();
        Dictionary<string, List<string>> FilterDic = new Dictionary<string, List<string>>();


        private void show_btn_Click(object sender, EventArgs e)
        {
            if (CBMyKitchen.Checked)
            {
                Where = "";
                Filter = "";
                UC_TVKitchens2.Kitchen_Checked(ref f, ref Where);
            }
            else
            {
                Where = " Restaurant_ID IN (1) AND Kitchen_ID IN (1)";
                Filter = "Kitchen: My kitchen";
            }

            ReportView Rec = new ReportView();
            Rec.Rpt = new CR_Stores();

            DataTable dt = Classes.RetrieveData("*", Where, "View_Stores");
            Rec.Rpt.SetDataSource(dt);
            Rec.Rpt.SetParameterValue("Filter", Filter);
            Rec.Show();
        }

    }
}
