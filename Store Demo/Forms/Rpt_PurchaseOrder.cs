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
    public partial class Rpt_PurchaseOrder : Form
    {
        public Rpt_PurchaseOrder()
        {
            InitializeComponent();
            BtnCreateDate.Checked = true;
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
        }           //Done swap From To Types of Date

        private void BtnDeliveryDate_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnDeliveryDate.Checked == true)
            {
                BtnCreateDate.Checked = false;
            }
            else
                BtnCreateDate.Checked = true;
        }           //Done swap From To Types of Date

        private void show_btn_Click(object sender, EventArgs e)
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

            if (BtnCreateDate.Checked == true)
            {
                Where += "And Create_Date between '" + Convert.ToDateTime(dtp_from.Value).ToString(Classes.sysDateTimeFormat) + "' AND '" + Convert.ToDateTime(dtp_to.Value).ToString(Classes.sysDateTimeFormat) + "'";
                Filter += " \n" + "Date : Create_Date";
            }
            else if (BtnDeliveryDate.Checked == true)
            {
                Where += "And Delivery_Date between '" + Convert.ToDateTime(dtp_from.Value).ToString(Classes.sysDateTimeFormat) + "' AND '" + Convert.ToDateTime(dtp_to.Value).ToString(Classes.sysDateTimeFormat) + "'";
                Filter += " \n" + "Date : Delivery_Date";
            }
            DataTable dt = Classes.RetrieveData("*", Where, "POView");
            ReportView Rec = new ReportView();
            Rec.Rpt = new CR_PO();
            Rec.Rpt.SetDataSource(dt);
            Rec.Rpt.SetParameterValue("Rpt_Fdate", dtp_from.Value);
            Rec.Rpt.SetParameterValue("Rpt_Tdate", dtp_to.Value);
            Rec.Rpt.SetParameterValue("Filter", Filter);
            Rec.Show();
        }
    }
}
