using Food_Cost.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Food_Cost.Forms
{
    public partial class Rpt_GeneratedRecipescs : Form
    {
        public Rpt_GeneratedRecipescs()
        {
            InitializeComponent();
        }
        string Where = "";
        string Filter = "";
        string f = "";

        private void btnRport_Click(object sender, EventArgs e)
        {
            if (!CBMyKitchen.Checked)
            {
                Where = "";
                Filter = "";
                uC_TVKitchens1.Kitchen_Checked(ref f, ref Where);
            }
            else
            {
                Where = " Restaurant_ID IN (1) AND Kitchen_ID IN (1)";
                Filter = "Kitchen: My kitchen"; 
            }
            if (TxtItemCode.Text.ToString() != "")
            {
                Where += " AND Recipe_ID = '" + TxtItemCode.Text.ToString() + "'";
            }
            Where += " And Generate_Date between '" + dtp_from.Value + "' AND '" + dtp_to.Value + "'";

            DataTable dt = Classes.RetrieveData("*", Where, "GeneratedRecipesView");
            for (int i=0; i< dt.Rows.Count; i++)
            {
                string tempwhere = "ID = '" + dt.Rows[i]["Generate_ID"] + "' AND Item_ID = '" + dt.Rows[i]["Item_ID"] + "' AND Trantype = 'Generate' ";
                DataRow Tempdt = Classes.RetrieveData("*", tempwhere, "TransActions").Rows[0];
                dt.Rows[i]["PrevQty"] = (double.Parse(Tempdt["Current_Qty"].ToString())- double.Parse(Tempdt["Qty"].ToString())).ToString();
                dt.Rows[i]["CurrQty"] = Tempdt["Current_Qty"];
            }
            ReportView Rec = new ReportView();
            Rec.Rpt = new CR_GeneratedRecipes();
            Rec.Rpt.SetDataSource(dt);
            Rec.Rpt.SetParameterValue("Rpt_Fdate", dtp_from.Value);
            Rec.Rpt.SetParameterValue("Rpt_Tdate", dtp_to.Value);
            Rec.Rpt.SetParameterValue("Filter", Filter);
            //Rec.Rpt.SetParameterValue("Food", Food);
            //Rec.Rpt.SetParameterValue("Beverage", Bev);
            //Rec.Rpt.SetParameterValue("General", Gen);
            Rec.Show();

        }

        private void BtnItem_Click(object sender, EventArgs e)
        {
            FrmSelection frm = new FrmSelection();
            frm.loaddata("Code", "Name", "Setup_Recipes");
            frm.ShowDialog();
            if (frm.Code != "" && frm.Code != null)
            {
                TxtItemCode.Text = frm.selrow.Cells[0].Value.ToString();
                TxtItemName.Text = frm.selrow.Cells[1].Value.ToString();
            }
        }

    }
}
