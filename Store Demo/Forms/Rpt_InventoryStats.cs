using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Food_Cost.Report;

namespace Food_Cost.Forms
{
    public partial class Rpt_InventoryStats : Form
    {
        public Rpt_InventoryStats()
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
            if (!CBMyKitchen.Checked)
            {
                Where = "";
                Filter = "";
                uC_TVKitchens1.Kitchen_Checked(ref f , ref Where);
            }
            else
            {
                Where = " Restaurant_ID IN (1) AND Kitchen_ID IN (1)";
                Filter = "Kitchen: My kitchen";
            }

            if (TxtItemCode.Text.ToString() != "")
            {
                Where += " AND ItemID = '" + TxtItemCode.Text.ToString() + "'";
            }

            ReportView Rec = new ReportView();
            Rec.Rpt = new CR_InventoryStats();

            DataTable dt = Classes.RetrieveData("*", Where, "InventoryStats");
            Rec.Rpt.SetDataSource(dt);
            Rec.Rpt.SetParameterValue("Filter", Filter);
            Rec.Show();
        }

        private void BtnItem_Click(object sender, EventArgs e)
        {
            FrmSelection frm = new FrmSelection();
            frm.loaddata("Code", "Name", "Setup_Items");
            frm.ShowDialog();
            if (frm.Code != "" && frm.Code != null)
            {
                TxtItemCode.Text = frm.selrow.Cells[0].Value.ToString();
                TxtItemName.Text = frm.selrow.Cells[1].Value.ToString();
            }
        }           //Done Finall Function " To load Items to Choose "
    }
}
