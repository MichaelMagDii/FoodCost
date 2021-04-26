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
    public partial class Rpt_BinCard : Form
    {
        public Rpt_BinCard()
        {
            InitializeComponent();
        }       //Done " Just Intialixation "
        DataTable DtItem_BinCard;
        DataTable dt;
        string Where = "",Filter = "";
        string s = "",f = "",s2 = "";
        string Qty, Cost;
        DataTable Dt,Dt2;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        Dictionary<string, string> Stores = new Dictionary<string, string>();
        Dictionary<string, List<string>> FilterDic = new Dictionary<string, List<string>>();

        private void CBMyKitchen_CheckedChanged(object sender, EventArgs e)
        {
            if (CBMyKitchen.Checked == true)
            {
                uC_TVKitchens1.Visible = false;
            }
            else
            {
                uC_TVKitchens1.Visible = true;
            }
        }           //Done 

        Dictionary<string, Tuple<string, string>> BBalance = new Dictionary<string, Tuple<string, string>>();
        Dictionary<string, Tuple<string, string>> EBalance = new Dictionary<string, Tuple<string, string>>();
            
        private void BtnItem_Click(object sender, EventArgs e)
        {
            FrmSelection frm = new FrmSelection();
            frm.loaddata("Code","Name","Setup_Items");
            frm.ShowDialog();
            if (frm.Code != "" && frm.Code != null)
            {
                TxtItemCode.Text = frm.selrow.Cells[0].Value.ToString();
                TxtItemName.Text = frm.selrow.Cells[1].Value.ToString();
            }
        }       //Done to Load Items

        private void loadBEBalance()
        {
            foreach (string KitName in uC_TVKitchens1.KitchensList)
            {
                string where = " _Date < '" + Convert.ToDateTime(dtp_from.Value).ToString(Classes.sysDateFormat) + "' AND Item_ID = '" + TxtItemCode.Text + "' AND KitchenName = '" + KitName + "' order by  _DATE DESC";
                DataTable DTTop = Classes.RetrieveData("top 1 * ", where, "TransActions");
                if (DTTop.Rows.Count != 0)
                {
                    Qty = DTTop.Rows[0]["Current_Qty"].ToString();
                    Cost = DTTop.Rows[0]["CurrentCost"].ToString();
                    BBalance[KitName] = new Tuple<string, string>(Qty, Cost);
                }
                else
                {
                    BBalance[KitName] = new Tuple<string, string>("0", "0");
                }
                where = " _Date <= '" + Convert.ToDateTime(dtp_to.Value).ToString(Classes.sysDateFormat) + "' AND Item_ID = '" + TxtItemCode.Text + "' AND KitchenName = '" + KitName + "' order by  _DATE DESC";
                DTTop = Classes.RetrieveData("top 1 * ", where, "TransActions");
                if (DTTop.Rows.Count != 0)
                {
                    Qty = DTTop.Rows[0]["Current_Qty"].ToString();
                    Cost = DTTop.Rows[0]["CurrentCost"].ToString();
                    EBalance[KitName] = new Tuple<string, string>(Qty, Cost);
                }
                else
                {
                    EBalance[KitName] = new Tuple<string, string>("0", "0");
                }
            }
        }

        private void btnRport_Click(object sender, EventArgs e)
        {            
            DtItem_BinCard = Classes.RetrieveData("*", "TransActionsView");
            DtItem_BinCard.Clear();
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
            loadBEBalance();


            if (TxtItemCode.Text.ToString() != "")
            {
                Where += " AND Item_ID = '" + TxtItemCode.Text.ToString() + "'";
            }

            string Select = "*";
            string WhereTA = Where + " And _Date between '" + Convert.ToDateTime(dtp_from.Value).ToString(Classes.sysDateTimeFormat)  + "' AND '" + Convert.ToDateTime(dtp_to.Value.Date.AddHours(23.999)).ToString(Classes.sysDateTimeFormat) + "'";
            string order = "Order by _Date";
            dt = Classes.RetrieveData(Select, WhereTA+ order, "TransActionsView");
            foreach (DataRow DR in dt.Rows)
            {
                DataRow Ndr = DtItem_BinCard.NewRow();

                Ndr["BQty"] = BBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["BCost"] = BBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["EQty"] = EBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["ECost"] = EBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["Qty"] = DR["Qty"].ToString();
                Ndr["Current_Qty"] = DR["Current_Qty"].ToString();
                Ndr["Cost"] = DR["Cost"].ToString();
                Ndr["CurrentCost"] = DR["CurrentCost"].ToString();
                Ndr["Unit"] = DR["Unit"].ToString();
                Ndr["Trantype"] = DR["Trantype"].ToString();
                Ndr["Item_ID"] = DR["Item_ID"].ToString();
                Ndr["ItemName"] = DR["ItemName"].ToString();

                Ndr["_Date"] = DR["_Date"].ToString();
                Ndr["KitchenName"] = DR["KitchenName"].ToString();
                Ndr["RestaurantName"] = DR["RestaurantName"].ToString();
                DtItem_BinCard.Rows.Add(Ndr);
            }

            #region test
            /*
            //Receive
            string WhereRO = Where + " And Receiving_Date between '" + dtp_from.Value.Date + "' AND '" + dtp_to.Value.Date + "'";
            dt = Classes.RetrieveData(Select, WhereRO, "ReceiveItemsView");
            foreach (DataRow DR in dt.Rows)
            {
                DataRow Ndr = DtItem_BinCard.NewRow();

                Ndr["BQty"] = BBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["BCost"] = BBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["EQty"] = EBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["ECost"] = EBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["Qty"] = DR["Qty"].ToString();
                Ndr["Cost"] = DR["Price_With_Tax"].ToString();
                Ndr["Unit"] = DR["Unit"].ToString();

                Ndr["Date"] = DR["Receiving_Date"].ToString();
                Ndr["KitchenName"] = DR["KitchenName"].ToString();
                Ndr["RestaurantName"] = DR["RestaurantName"].ToString();
                Ndr["TransDetails"] = " Receive";
                DtItem_BinCard.Rows.Add(Ndr);
            }

            //Adjustment
            string WhereAdj = Where + " And Adjacment_Date between '" + dtp_from.Value.Date + "' AND '" + dtp_to.Value.Date + "'";
            dt = Classes.RetrieveData(Select, WhereAdj, "BinAdjView");
            foreach (DataRow DR in dt.Rows)
            {
                DataRow Ndr = DtItem_BinCard.NewRow();
                Ndr["BQty"] = BBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["BCost"] = BBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["EQty"] = EBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["ECost"] = EBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["Qty"] = "-" + DR["Variance"].ToString();
                Ndr["Cost"] = DR["Cost"].ToString();
                Ndr["Date"] = DR["Adjacment_Date"].ToString();
                Ndr["KitchenName"] = DR["KitchenName"].ToString();
                Ndr["RestaurantName"] = DR["RestaurantName"].ToString();
                Ndr["TransDetails"] = "Adjustment";
                DtItem_BinCard.Rows.Add(Ndr);
            }

            //Transfer IN
            string WhereTransfer = Where + " And Receiving_Date between '" + dtp_from.Value.Date + "' AND '" + dtp_to.Value.Date + "'";
            dt = Classes.RetrieveData(Select, WhereTransfer, "TransferItemsIn");
            foreach (DataRow DR in dt.Rows)
            {
                DataRow Ndr = DtItem_BinCard.NewRow();

                Ndr["BQty"] = BBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["BCost"] = BBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["EQty"] = EBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["ECost"] = EBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["Net_Price"] = 0;
                Ndr["Qty"] = DR["Qty"].ToString();
                Ndr["Unit"] = DR["Unit"].ToString();
                Ndr["Cost"] = DR["Cost"].ToString();
                Ndr["Date"] = DR["Receiving_Date"].ToString();

                Ndr["KitchenName"] = DR["KitchenName"].ToString();
                Ndr["RestaurantName"] = DR["RestaurantName"].ToString();
                Ndr["TransDetails"] = "Transferd In ";
                DtItem_BinCard.Rows.Add(Ndr);
            }

            //Transfer Out
            WhereTransfer = Where + " And Request_Date between '" + dtp_from.Value.Date + "' AND '" + dtp_to.Value.Date + "'";
            dt = Classes.RetrieveData(Select, WhereTransfer, "TransferItemsOut");
            foreach (DataRow DR in dt.Rows)
            {
                DataRow Ndr = DtItem_BinCard.NewRow();
                Ndr["BQty"] = BBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["BCost"] = BBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["EQty"] = EBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["ECost"] = EBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["Qty"] = "-" + DR["Qty"].ToString();
                Ndr["Unit"] = DR["Unit"].ToString();
                Ndr["Cost"] = DR["Cost"].ToString();
                Ndr["Date"] = DR["Request_Date"].ToString();

                Ndr["KitchenName"] = DR["KitchenName"].ToString();
                Ndr["RestaurantName"] = DR["RestaurantName"].ToString();
                Ndr["TransDetails"] = "Transferd Out ";
                DtItem_BinCard.Rows.Add(Ndr);
            }

            WhereTransfer = Where + " And Generate_Date between '" + dtp_from.Value.Date + "' AND '" + dtp_to.Value.Date + "'";
            dt = Classes.RetrieveData(Select, WhereTransfer, "GeneratedRecipesView");
            foreach (DataRow DR in dt.Rows)
            {
                DataRow Ndr = DtItem_BinCard.NewRow();
                Ndr["BQty"] = BBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["BCost"] = BBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["EQty"] = EBalance[DR["KitchenName"].ToString()].Item1;
                Ndr["ECost"] = EBalance[DR["KitchenName"].ToString()].Item2;

                Ndr["Qty"] = "-" + DR["Qty"].ToString();
                Ndr["Unit"] = DR["Unit"].ToString();
                Ndr["Cost"] = "0";
                Ndr["Date"] = DR["Generate_Date"].ToString();
                Ndr["KitchenName"] = DR["KitchenName"].ToString();
                Ndr["RestaurantName"] = DR["RestaurantName"].ToString();
                Ndr["TransDetails"] = "Generated Reciepe";
                DtItem_BinCard.Rows.Add(Ndr);
            }
            */
            #endregion test

            ReportView Rec = new ReportView();
            Rec.Rpt = new CR_BinCard_2();
            Rec.Rpt.SetDataSource(DtItem_BinCard);
            Rec.Rpt.SetParameterValue("Rpt_Fdate", dtp_from.Value.Date);
            Rec.Rpt.SetParameterValue("Rpt_Tdate", dtp_to.Value);
            Rec.Rpt.SetParameterValue("ItemID", TxtItemCode.Text.ToString());
            Rec.Rpt.SetParameterValue("ItemName", TxtItemName.Text.ToString());
            Rec.Rpt.SetParameterValue("Filter", Filter);

            Where = Where.Replace(("Restaurant_ID"), ("RestaurantID"));
            Where = Where.Replace(("Kitchen_ID"), ("KitchenID"));
            Where = Where.Replace(("Item_ID"), ("ItemID"));
            dt = Classes.RetrieveData("*", Where, "Items");
            if (dt.Rows.Count != 0)
            {
                DataRow DRItems = dt.Rows[0];
                Rec.Rpt.SetParameterValue("Max", DRItems["MaxNumber"].ToString());
                Rec.Rpt.SetParameterValue("Min", DRItems["MinNumber"].ToString());
            }
            else
            {
                Rec.Rpt.SetParameterValue("Max", "0");
                Rec.Rpt.SetParameterValue("Min", "0");
            }
            Rec.Show();
        }
        
    }
}
