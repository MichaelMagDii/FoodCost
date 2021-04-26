using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Food_Cost.Report;


namespace Food_Cost
{

    public class Recalculate
    {
        static string where, cols, values, tempwhere;
        static double Qty, Cost;
        static Dictionary<string, string> changed_trasfers = new Dictionary<string, string>();
        static Dictionary<string, string> item_cost = new Dictionary<string, string>();
        static DataTable BETable;


        private static void LoadITEM_COST(DataTable DTPreviousMonth)
        {
            if (DTPreviousMonth.Rows.Count != 0)
            {
                string Pyear = DTPreviousMonth.Rows[0]["Year"].ToString();
                string Pmonth = DTPreviousMonth.Rows[0]["Month"].ToString();
                string where = "Year = '" + Pyear + "' AND Month = '" + Pmonth + "'";
                DataTable TempItems = Classes.RetrieveData("*", where, "BeginningEndingMonthView");
                foreach (DataRow DRI in TempItems.Rows)
                {
                    item_cost[DRI["KitchenName"].ToString() + DRI["Item_ID"].ToString()] = DRI["Cost"].ToString();
                }
            }
        }

        private static void Insert(int k)
        {
            values = "";
            cols = "_DATE,Restaurant_ID,Kitchen_ID,KitchenName,Trantype,ID,Item_ID,Qty,Current_Qty,Cost,CurrentCost";
            string s = "";
            for (int i = 0; i < BETable.Columns.Count; i++)
            {
                if (i == 0)
                {
                    values += s + "'" + Convert.ToDateTime(BETable.Rows[k][i].ToString()).ToString(Classes.sysDateTimeFormat) + "'";
                    s = ",";
                }
                else
                {
                    values += s + "'" + BETable.Rows[k][i].ToString() + "'";
                    s = ",";
                }
            }
            Classes.InsertRow("TransActions", cols, values);
        }

        private static void GetPrevQty(int k, DataTable DTPreviousMonth)
        {
            double TempCurrQty = double.Parse(BETable.Rows[k]["Current_Qty"].ToString());
            if (DTPreviousMonth.Rows.Count != 0)
            {
                string TempPrevQty = "";
                string Pyear = DTPreviousMonth.Rows[0]["Year"].ToString();
                string Pmonth = DTPreviousMonth.Rows[0]["Month"].ToString();
                string ItemID = BETable.Rows[k]["Item_ID"].ToString();
                string KitName = BETable.Rows[k]["KitchenName"].ToString();
                string where = "Year = '" + Pyear + "' AND Month = '" + Pmonth + "' And KitchenName= '" + KitName + "' AND Item_ID = '" + ItemID + "'";
                try
                {
                    TempPrevQty = Classes.RetrieveData("Qty", where, "BeginningEndingMonthView").Rows[0][0].ToString();
                }
                catch { TempPrevQty = "0"; }
                BETable.Rows[k]["Current_Qty"] = (TempCurrQty + double.Parse(TempPrevQty)).ToString();
            }

            Insert(k);
        }

        private static void CalculateQty(DataRow DRCurrentMonth, DataTable DTPreviousMonth)
        {
            string TheColumns = "_DATE datetime,Restaurant_ID int,Kitchen_ID int,KitchenName varchar(50),Trantype varchar(50),ID varchar(50)";
            TheColumns += ",Item_ID varchar(50),ItemName varchar(50),Qty float,Current_Qty float,Cost float,CurrentCost float";
            Classes.CreateTable("TransActions", TheColumns);

            BETable = Classes.RetrieveStoredWithParamaeters("SPTransActions", "@StartDate,@EndDate", DRCurrentMonth["From"].ToString() + ',' + DRCurrentMonth["To"].ToString());

            where = "_DATE between '" + Convert.ToDateTime(DRCurrentMonth["From"].ToString()).ToString(Classes.sysDateTimeFormat) + "' AND '" + Convert.ToDateTime(DRCurrentMonth["To"].ToString()).ToString(Classes.sysDateTimeFormat) + "'";

            Classes.DeleteRows(where, "TransActions");

            for (int k = 0; k < BETable.Rows.Count; k++)
            {
                if (k == 0)
                {
                    GetPrevQty(k, DTPreviousMonth);
                }
                else if (BETable.Rows[k]["Trantype"].ToString() == "Adjactment")
                {
                    BETable.Rows[k]["Qty"] = (double.Parse(BETable.Rows[k]["Current_Qty"].ToString()) - double.Parse(BETable.Rows[k - 1]["Current_Qty"].ToString())).ToString();
                    string TempWhere = "Adjacment_ID = '" + BETable.Rows[k]["ID"].ToString() + "' And Item_ID = '" + BETable.Rows[k]["Item_ID"].ToString() + "'";
                    Classes.UpdateRow("Qty,Variance", BETable.Rows[k - 1]["Current_Qty"].ToString() + "," + BETable.Rows[k]["Qty"].ToString(), TempWhere, "Adjacment_Items");
                    Insert(k);
                }
                else if (BETable.Rows[k]["Item_ID"].ToString() == BETable.Rows[k - 1]["Item_ID"].ToString() && BETable.Rows[k]["KitchenName"].ToString() == BETable.Rows[k - 1]["KitchenName"].ToString())
                {
                    BETable.Rows[k]["Current_Qty"] = (double.Parse(BETable.Rows[k]["Current_Qty"].ToString()) + double.Parse(BETable.Rows[k - 1]["Current_Qty"].ToString())).ToString();
                    Insert(k);
                }
                else
                {
                    GetPrevQty(k, DTPreviousMonth);
                }
            }
        }

        private static void CalculateCost(DataRow DRCurrentMonth, DataTable DTPreviousMonth)
        {
            LoadITEM_COST(DTPreviousMonth);
            where = "_DATE between '" + Convert.ToDateTime(DRCurrentMonth["From"].ToString()).ToString(Classes.sysDateTimeFormat) + "' AND '" + Convert.ToDateTime(DRCurrentMonth["To"].ToString()).ToString(Classes.sysDateTimeFormat) + "'";
            string order = " order by Item_ID, _DATE ";

            DataTable Transactions = Classes.RetrieveData("*", where + order, "TransActions");

            for (int i = 0; i < Transactions.Rows.Count; i++)
            {
                string itemCostKey = Transactions.Rows[i]["KitchenName"].ToString() + Transactions.Rows[i]["Item_ID"].ToString();
                string ChangedTranKye = Transactions.Rows[i]["ID"].ToString() + Transactions.Rows[i]["Item_ID"].ToString();

                where = "Trantype = '" + Transactions.Rows[i]["Trantype"].ToString();
                where += "' AND Item_ID = '" + Transactions.Rows[i]["Item_ID"].ToString();
                where += "' AND ID = '" + Transactions.Rows[i]["ID"].ToString() + "'";

                if (Transactions.Rows[i]["Trantype"].ToString() == "Receive" || Transactions.Rows[i]["Trantype"].ToString() == "Transfer_In")
                {
                    if (changed_trasfers.ContainsKey(ChangedTranKye) == true && Transactions.Rows[i]["Trantype"].ToString() == "Transfer_In")
                    {
                        Cost = double.Parse(changed_trasfers[ChangedTranKye]);
                    }
                    else
                    {
                        Cost = double.Parse(Transactions.Rows[i]["Cost"].ToString());
                    }

                    if (Transactions.Rows[i]["Current_Qty"].Equals(Transactions.Rows[i]["Qty"]))
                    {
                        item_cost[itemCostKey] = Cost.ToString();
                        Classes.UpdateCell("CurrentCost", Cost.ToString(), where, "TransActions");
                    }
                    else
                    {
                        double CurrentCost;
                        double CurrentQty = double.Parse(Transactions.Rows[i]["Current_Qty"].ToString()) - double.Parse(Transactions.Rows[i]["Qty"].ToString());
                        if (CurrentQty < 0)
                        {
                            MessageBox.Show("Time error in" + Transactions.Rows[i]["Trantype"]+ " " + Transactions.Rows[i]["ID"]);
                            return ;
                        }
                        Qty = double.Parse(Transactions.Rows[i]["Qty"].ToString());
                        if (item_cost.ContainsKey(itemCostKey))
                            CurrentCost = double.Parse(item_cost[itemCostKey]);
                        else
                            CurrentCost = double.Parse(Transactions.Rows[i]["Cost"].ToString());

                        double Current_Qty = double.Parse(Transactions.Rows[i]["Current_Qty"].ToString());
                        string NewCost;

                        if (Current_Qty == 0)
                        {
                            NewCost = "0";
                        }
                        else
                        {
                            NewCost = ((CurrentCost * CurrentQty + Cost * Qty) / Current_Qty).ToString();
                        }

                        item_cost[itemCostKey] = NewCost;
                        Classes.UpdateCell("CurrentCost", NewCost.ToString(), where, "TransActions");
                    }
                }
                else if (Transactions.Rows[i]["Trantype"].ToString() == "Adjactment")
                {
                    item_cost[itemCostKey] = Transactions.Rows[i]["Cost"].ToString();
                    Classes.UpdateCell("CurrentCost", Transactions.Rows[i]["Cost"].ToString(), where, "TransActions");
                }
                else if (Transactions.Rows[i]["Trantype"].ToString() == "Transfer_Out")
                {
                    if (item_cost.ContainsKey(itemCostKey))
                    {
                        if (!item_cost[itemCostKey].Equals(Transactions.Rows[i]["Cost"].ToString()))
                        {
                            changed_trasfers[ChangedTranKye] = item_cost[itemCostKey];
                            Classes.UpdateRow("CurrentCost,Cost", item_cost[itemCostKey] + "," + item_cost[itemCostKey], where, "TransActions");

                            //where = "Item_ID = '" + Transactions.Rows[i]["Item_ID"].ToString() + "' AND Transfer_ID = '" + Transactions.Rows[i]["ID"].ToString() + "'";
                            //Classes.UpdateCell("Cost", item_cost[itemCostKey], where, "Transfer_Kitchens_Items");

                            string tempwhere = "Item_ID = '" + Transactions.Rows[i]["Item_ID"].ToString() + "' AND Request_ID = '" + Transactions.Rows[i]["ID"].ToString() + "'";
                            Classes.UpdateCell("Cost", item_cost[itemCostKey], tempwhere, "Requests_Items");
                            Classes.UpdateCell("Net_Cost", "Cost * Qty", "Requests_Items");

                            DataTable drRoSerial = Classes.RetrieveData("SELECT RO_Serial from RO where Transactions_No = '" + Transactions.Rows[i]["ID"].ToString() + "' and Type = 'Transfer_Kitchen'");
                            if (drRoSerial.Rows.Count > 0)
                            {
                                tempwhere = "Item_ID = '" + Transactions.Rows[i]["Item_ID"].ToString() + "' AND RO_No = '" + drRoSerial.Rows[0][0].ToString() + "'";
                                Classes.UpdateRow("Price_With_Tax,Price_Without_Tax", item_cost[itemCostKey] + "," + item_cost[itemCostKey], tempwhere, "RO_Items");
                                Classes.UpdateCell("Net_Price", "Price_With_Tax * Qty", "RO_Items");
                            }
                        }
                    }
                }
                else if (Transactions.Rows[i]["Trantype"].ToString() == "Generate")
                {
                    Classes.UpdateRow("CurrentCost,Cost", item_cost[itemCostKey] + "," + item_cost[itemCostKey], where, "TransActions");
                    tempwhere = "Item_ID = '" + Transactions.Rows[i]["Item_ID"].ToString() + "' AND Generate_ID = '" + Transactions.Rows[i]["ID"].ToString() + "'";
                    Classes.UpdateCell("Cost", item_cost[itemCostKey], tempwhere, "GenerateRecipe_Items");
                    Classes.UpdateCell("Net_Cost", "Cost * ItemQty", "GenerateRecipe_Items");

                }
            }
        }

        private static void UpdateItems(DataRow DRCurrentMonth)
        {
            string where = "Year = '" + DRCurrentMonth["Year"].ToString() + "' AND Month = '" + DRCurrentMonth["Month"].ToString() + "'";
            DataTable ItemsQtyCost = Classes.RetrieveData("*", where + " AND Qty > '0' ", "BeginningEndingMonth");
            foreach (DataRow Item in ItemsQtyCost.Rows)
            {
                where = " RestaurantID = '" + Item["Restaurant_ID"] + "' AND KitchenID = '" + Item["Kitchen_ID"] + "' AND ItemID = '" + Item["Item_ID"] + "'";
                Classes.UpdateRow("Qty,Current_Cost", Item["Qty"] + "," + Item["Cost"], where, "Items");
            }
        }

        public static void ReCalculate_Cost_Qty(DataRow DRCurrentMonth, DataTable DTPreviousMonth)
        {
            try
            {
                CalculateQty(DRCurrentMonth, DTPreviousMonth);
                CalculateCost(DRCurrentMonth, DTPreviousMonth);
                UpdateItems(DRCurrentMonth);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            //MessageBox.Show("Qty And Cost have recalculated");
        }

        public static void CloseMonth(DataRow DRCurrentMonth)
        {
            try
            {
                cols = "Year varchar(50),Month varchar(50),FromDate datetime,ToDate datetime,Restaurant_ID int,Kitchen_ID int,Item_ID varchar(50),Qty bigint,Cost float";

                Classes.CreateTable("BeginningEndingMonth", cols);

                where = "Month = '" + DRCurrentMonth["Month"].ToString() + "' AND Year = '" + DRCurrentMonth["Year"].ToString() + "'";
                Classes.DeleteRows(where, "BeginningEndingMonth");

                DataTable Items = Classes.RetrieveData("Code", "Setup_Items");

                DataTable DTTop = new DataTable();

                DataTable Kitchens = Classes.RetrieveData("*", "Setup_Kitchens");

                string Dvalues = "('" + DRCurrentMonth["Year"] + "','" + DRCurrentMonth["Month"].ToString() + "','" + Convert.ToDateTime(DRCurrentMonth["From"]).ToString(Classes.sysDateTimeFormat) + "','" + Convert.ToDateTime(DRCurrentMonth["To"]).ToString(Classes.sysDateTimeFormat) + "'";

                foreach (DataRow KitName in Kitchens.Rows)
                {
                    values = "";
                    string Dkitchen = ",'" + KitName["Name"].ToString() + "'";
                    for (int i = 0; i < Items.Rows.Count; i++)
                    {
                        where = " _Date <= '" + Convert.ToDateTime(DRCurrentMonth["To"]).ToString(Classes.sysDateTimeFormat) + "' AND Item_ID = '" + Items.Rows[i][0].ToString() + "' AND KitchenName = '" + KitName["Name"].ToString() + "' order by  _DATE DESC";
                        DTTop = Classes.RetrieveData("top 1 * ", where, "TransActions");

                        if (DTTop.Rows.Count != 0)
                        {
                            Qty = double.Parse(DTTop.Rows[0]["Current_Qty"].ToString());
                            Cost = double.Parse(DTTop.Rows[0]["CurrentCost"].ToString());
                        }
                        else
                        {
                            Qty = 0;
                            Cost = 0;
                        }
                        string DQtyCost = ",'" + Items.Rows[i][0].ToString() + "','" + Qty.ToString() + "','" + Cost.ToString() + "'";
                        string RestKitchen = ",'" + KitName["RestaurantID"].ToString() + "','" + KitName["Code"].ToString() + "'";
                        values += Dvalues + RestKitchen + DQtyCost + "),";
                        if (i % 999 == 0 || i == Items.Rows.Count - 1)
                        {
                            Classes.InsertRow("BeginningEndingMonth", values.Substring(1, values.Length - 3));
                            values = "";
                        }
                    }
                }
                //MessageBox.Show("Month Closed Successfully");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
      
    }
}
