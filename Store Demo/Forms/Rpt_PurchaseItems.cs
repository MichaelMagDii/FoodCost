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
    public partial class Rpt_PurchaseItems : Form
    {
        public Rpt_PurchaseItems()
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


        private void BtnCreateDate_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnCreateDate.Checked == true)
            {
                BtnDeliveryDate.Checked = false;
            }
            else
                BtnDeliveryDate.Checked = true;
        }       //Done Swap Between Delivery Date and Ctrate Date

        private void BtnDeliveryDate_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnDeliveryDate.Checked == true)
            {
                BtnCreateDate.Checked = false;
            }
            else
                BtnCreateDate.Checked = true;
        }       //Done Swap Between Delivery date and Cretae Date

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
                Where = " (Restaurant_ID = 1 AND Kitchen_ID = 1)";
                Filter = "Kitchen: My kitchen";
            }
            if (TxtItemCode.Text.ToString() != "")
            {
                Where += " AND Item_ID = '" + TxtItemCode.Text.ToString() + "'";
            }

            if (BtnCreateDate.Checked == true)
            {
                Where += " And Create_Date between '" + Convert.ToDateTime(dtp_from.Value).ToString(Classes.sysDateTimeFormat) + "' AND '" + Convert.ToDateTime(dtp_to.Value).ToString(Classes.sysDateTimeFormat) + "'";
                Filter += " \n" + "Date: Create_Date";
            }
            else if (BtnDeliveryDate.Checked == true)
            {
                Where += " And Delivery_Date between '" + Convert.ToDateTime(dtp_from.Value).ToString(Classes.sysDateTimeFormat) + "' AND '" + Convert.ToDateTime(dtp_to.Value).ToString(Classes.sysDateTimeFormat) + "'";
                Filter += " \n" + "Date: Delivery_Date";
            }
            ReportView Rec = new ReportView();
            Rec.Rpt = new CR_POitems();
            string Select = "*";
            DataTable dt = Classes.RetrieveData(Select, Where, "POItems");
            Rec.Rpt.SetDataSource(dt);
            Rec.Rpt.SetParameterValue("Rpt_Fdate", dtp_from.Value);
            Rec.Rpt.SetParameterValue("Rpt_Tdate", dtp_to.Value);
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
        }           //Done to Loaod Items Data 
    }
}
