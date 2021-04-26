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
    public partial class Rpt_ReceiveItems : Form
    {
        public Rpt_ReceiveItems()
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
        
        private void ShowBtn_Click(object sender, EventArgs e)
        {
            if (!CBMyKitchen.Checked)
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
            if (TxtItemCode.Text.ToString() != "")
            {
                Where += " AND Item_ID = '" + TxtItemCode.Text.ToString() + "'";
            }
            List<string> type = new List<string>();
            if (CBPurchase.Checked)
            {
                type.Add("'Recieve_Purchase'");
            }
            if (CBAutoPurchase.Checked)
            {
                type.Add("'Auto_Recieve'");
            }
            if (CBKitchenTransfer.Checked)
            {
                type.Add("'Transfer_Kitchen'");
            }
            if (CBRestaurantTransfer.Checked)
            {
                type.Add("'Transfer_Resturant'");
            }
            if(type.Count != 0)
            {
                string s = "";
                Where += "And Type IN (";
                foreach(string st in type)
                {
                    Where += s + st;
                    s = ",";
                }
                Where += ")";
            }

            Where += " And Receiving_Date between '" + Convert.ToDateTime(dtp_from.Value).ToString(Classes.sysDateTimeFormat) + "' AND '" + Convert.ToDateTime(dtp_to.Value).ToString(Classes.sysDateTimeFormat) + "'";

            DataTable dt = Classes.RetrieveData("*", Where, "ReceiveItemsView");

            double Food = 0, Bev = 0, Gen = 0;

            foreach(DataRow DR in dt.Rows)
            {
                if (DR["Category"].ToString() == "Food")
                    Food++;
                if (DR["Category"].ToString() == "Beverage")
                    Bev++;
                if (DR["Category"].ToString() == "General")
                    Gen++;
            }
            if (dt.Rows.Count != 0)
            {
                Food = Food / dt.Rows.Count;
                Bev = Bev / dt.Rows.Count;
                Gen = Gen / dt.Rows.Count;
            }
     
            ReportView Rec = new ReportView();
            Rec.Rpt = new CR_ReceiveItemes();
            Rec.Rpt.SetDataSource(dt);
            Rec.Rpt.SetParameterValue("Rpt_Fdate", dtp_from.Value);
            Rec.Rpt.SetParameterValue("Rpt_Tdate", dtp_to.Value);
            Rec.Rpt.SetParameterValue("Filter", Filter);
            Rec.Rpt.SetParameterValue("Food", Food);
            Rec.Rpt.SetParameterValue("Beverage", Bev);
            Rec.Rpt.SetParameterValue("General", Gen);
            Rec.Show();

        }           //Done

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
        }           //Done Load Items Data 
    }
}
