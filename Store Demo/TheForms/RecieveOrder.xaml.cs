using Food_Cost.Report;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for RecieveOrder.xaml
    /// </summary>
    public partial class RecieveOrder : UserControl
    {
        List<string> AuthenticatedPO = new List<string>();
        List<string> AuthenticatedRsturant = new List<string>();
        List<string> AuthenticatedKitchen = new List<string>();
        List<string> AuthenticatedWithoutPO = new List<string>();
        List<string> AuthenticatedRequest = new List<string>();
        DataTable Resturants;DataTable Kitchens;
        public static string TransferResturantID = "";  public static string TransferKitchenID = "";
        public string RestaurantId, KitchenId = "";
        int IndexOfRecord = 0;
        string FiledSelection = ""; string Values = "";
        public RecieveOrder()
        {
            if (MainWindow.AuthenticationData.Count != 0)
            {
                if (MainWindow.AuthenticationData.ContainsKey("RecievePO") || MainWindow.AuthenticationData.ContainsKey("RecieveReturantTrnsfer") || MainWindow.AuthenticationData.ContainsKey("RecieveKitchen") || MainWindow.AuthenticationData.ContainsKey("RecieveWithoutPurchse") || MainWindow.AuthenticationData.ContainsKey("Request"))
                {
                    AuthenticatedPO = MainWindow.AuthenticationData["RecievePO"];
                    AuthenticatedRsturant = MainWindow.AuthenticationData["RecieveReturantTrnsfer"];
                    AuthenticatedKitchen = MainWindow.AuthenticationData["RecieveKitchen"];
                    AuthenticatedWithoutPO = MainWindow.AuthenticationData["RecieveWithoutPurchse"];
                    AuthenticatedRequest = MainWindow.AuthenticationData["Request"];
                    if (AuthenticatedPO.Count == 0 && AuthenticatedRsturant.Count == 0 && AuthenticatedKitchen.Count == 0 && AuthenticatedWithoutPO.Count == 0 && AuthenticatedRequest.Count == 0)
                    {
                        MessageBox.Show("You Havent a Privilage to Open this Page");
                        LogIn logIn = new LogIn();
                        logIn.ShowDialog();
                    }
                    else
                    {
                        InitializeComponent();
                        ExpireDate.ItemExpireDate.Clear();
                        //LoadToDGV();
                        string Serial = Classes.InCrementTransactionSerial("RO", "RO_Serial");
                        CodePurchaseROtxt.Text = Serial;
                        CodeResturanttxt.Text = Serial;
                        CodeKitchentxt.Text =   Serial;
                        CodeWithouttxt.Text = Serial;
                        Resturants = Classes.RetrieveResturants();
                        LoadTheResturant();
                    }
                }
            }
            else { MessageBox.Show("You should Login First !"); LogIn logIn = new LogIn();  logIn.ShowDialog();   }
        }
        private void MainUiFormat()
        {
            if (TabControl.SelectedIndex == 0)
            {
                RecieveOrderDGV.IsEnabled = true;
                Recieve.IsEnabled = false;
            }
            else if (TabControl.SelectedIndex == 1)
            {
                ManualResturanttxt.IsEnabled = false;
                DeliveryRestauranttxt.IsEnabled = false;
                DeliveryROKitchenTime.IsEnabled = false;
                CommentRestaurant.IsEnabled = false;
                recieveTransfer.IsEnabled = false;
            }
            else if (TabControl.SelectedIndex == 2)
            {
                ManualKitchentxt.IsEnabled = false;
                DeliveryKitchentxt.IsEnabled = false;
                DeliveryROInterTime.IsEnabled = false;
                CommentKitchentxt.IsEnabled = false;
                recieveTransferInter.IsEnabled = false;
            }
            else if (TabControl.SelectedIndex == 3)
            {
                KitchenCbx.IsEnabled = false;
                DeliveryWithouttxt.IsEnabled = false;
                Delivery_timeWithout.IsEnabled = false;
                ResturantCbx.IsEnabled = false;
                ManualWithouttxt.IsEnabled = false;
                CommentWithouttxt.IsEnabled = false;
                AddItemBtn.IsEnabled = false;
                DeleteItemBtn.IsEnabled = false;
                _NewBtn.IsEnabled = true;
                REcivedWithoutBtn_Click.IsEnabled = false;
                WithoutPoUndoBtn.IsEnabled = true;
            }
            else if(TabControl.SelectedIndex==4)
            {
                CodeRequesttxt.IsEnabled = false;
                ManualRequesttxt.IsEnabled = false;
                TypeCbx.IsEnabled = false;
                RequestsItemsDGV.DataContext = null;
                RequestssDGV.Visibility = Visibility.Visible;
                RequestsItemsDGV.Visibility = Visibility.Hidden;
            }
        }
        public void EnableUI()
        {
            if (TabControl.SelectedIndex == 0)
            {
                    
                Recieve.IsEnabled = true;

                CodePurchaseROtxt.IsEnabled = false;
                PO.IsEnabled = false;
                ManualPurchaseROtxt.IsEnabled = true;
                vendortxt.IsEnabled = false;
                Delivery_dt.IsEnabled = false;
                commenttxt.IsEnabled = true;
                Delivery_time.IsEnabled = false;
            }
            else if (TabControl.SelectedIndex == 1)
            {
                ManualResturanttxt.IsEnabled = true;
                CommentRestaurant.IsEnabled = true;
                recieveTransfer.IsEnabled = true;
            }
            else if (TabControl.SelectedIndex == 2)
            {
                DeliveryWithouttxt.IsEnabled = true;
                Delivery_timeWithout.IsEnabled = true;
                ManualKitchentxt.IsEnabled = true;
                CommentKitchentxt.IsEnabled = true;
                recieveTransferInter.IsEnabled = true;
            }
            else if (TabControl.SelectedIndex == 3)
            {
                _NewBtn.IsEnabled = false;
                ManualWithouttxt.IsEnabled = true;
                DeliveryWithouttxt.IsEnabled = true;
                Delivery_timeWithout.IsEnabled = true;
                CommentWithouttxt.IsEnabled = true;
                AddItemBtn.IsEnabled = true;
                DeleteItemBtn.IsEnabled = true;
                REcivedWithoutBtn_Click.IsEnabled = true;
                ResturantCbx.IsEnabled = true;
                KitchenCbx.IsEnabled = true;
            }
        }
        private void ClearFields()
        {
            if (TabControl.SelectedIndex == 0)
            {
                CodePurchaseROtxt.Text = "";

                ManualPurchaseROtxt.Text = "";
                PO.Text = "";
                Delivery_time.Text = "";
                vendortxt.Text = "";
                Delivery_dt.Text = "";
                commenttxt.Text = "";
                Total_Price_Without_Tax_Purchase.Text = "";
                Total_Price_With_Tax_Purchase.Text = "";

                ItemsDGV.DataContext = null;
            }
            else if (TabControl.SelectedIndex == 1)
            {
                CodeResturanttxt.Text = "";
                ManualResturanttxt.Text = "";
                TransferResturanttxt.Text = "";
                DeliveryRestauranttxt.Text = "";
                DeliveryROKitchenTime.Text = "";
                CommentRestaurant.Text = "";
                FromRestaurant_Resturanttxt.Text = "";
                FromKitchen_Resturanttxt.Text = "";
                ToResturant_Restauranttxt.Text = "";
                ToKitchen_Restauranttxt.Text = "";
                NumberOfItemsResturant.Text = "0";
                Total_Price_With_Tax_Resturant.Text = "0";

                ItemsResturantDGV.DataContext = null;
            }
            else if (TabControl.SelectedIndex == 2)
            {
                CodeKitchentxt.Text = "";
                ManualKitchentxt.Text = "";
                TransferKitchentxt.Text = "";
                DeliveryKitchentxt.Text = "";
                DeliveryROInterTime.Text = "";
                CommentKitchentxt.Text = "";
                Resturant_Kitchentxt.Text = "";
                FromKitchen_Kitchentxt.Text = "";
                ToKitchen_Kitchentxt.Text = "";
                NumberOfItemsKitchen.Text = "0";
                Total_Price_With_Tax_Kitchen.Text = "0";

                ItemsKitchenDGV.DataContext = null;
            }
            else if (TabControl.SelectedIndex == 3)
            {
                CodeWithouttxt.Text = "";
                ManualWithouttxt.Text = "";
                ResturantCbx.Text = "";
                KitchenCbx.Text = "";
                DeliveryWithouttxt.Text = "";
                Delivery_timeWithout.Text = "";
                CommentWithouttxt.Text = "";
                Total_Price_Without_Tax.Text = "";
                Total_Price_With_Tax.Text = "";
                ItemsWithoutDGV.DataContext = null;
            }
            else if(TabControl.SelectedIndex == 4)
            {
                CodeRequesttxt.Text = "";
                ManualRequesttxt.Text = "";
                TypeCbx.Text = "";
                Request_Date.Text = "";
                RequestCommenttxt.Text = "";
            }

        }
        private bool DoSomeChecks()
        {
            int Count = 0;
            if (TabControl.SelectedIndex == 0)
            {
                if (CodePurchaseROtxt.Text.Equals(""))
                {
                    MessageBox.Show("R.O# Can't Be Empty");
                    return false;
                }
                else if (ManualPurchaseROtxt.Text.Equals(""))
                {
                    MessageBox.Show("Manual R.O# Can't Be Empty");
                    return false;
                }
                else if (PO.Text.Equals(""))
                {
                    MessageBox.Show("P.O Reference# Can't Be Empty");
                    return false;
                }
                else if (Delivery_dt.Text.Equals(""))
                {
                    MessageBox.Show("Delivery Date Can't Be Empty");
                    return false;
                }
                //else if (Delivery_time.Text == null)
                //{
                //    MessageBox.Show("Delivery Time Can't Be Empty");
                //    return false;
                //}
                else if (vendortxt.Text.Equals(""))
                {
                    MessageBox.Show("Vendor can not be empty");
                    return false;
                }
                else if (ItemsDGV.Items.Count == 0)
                {
                    MessageBox.Show("Items can not be empty");
                    return false;
                }
                for (int i = 0; i < ItemsDGV.Items.Count; i++)
                {
                    if (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0].ToString() == "False")
                    {
                        Count++;
                    }
                }
                if (Count == ItemsDGV.Items.Count)
                {
                    MessageBox.Show("Choose Items That You will Recieve");
                    return false;
                }

                for (int i = 0; i < ItemsDGV.Items.Count; i++)
                {
                    int QtyofExpire = 0;
                    if (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6].ToString() == "True")
                    {
                        string ItemCode = ((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[1].ToString();
                        try
                        {
                            foreach (Tuple<string, string> tuple in ExpireDate.ItemExpireDate[ItemCode])
                            {
                                QtyofExpire += Convert.ToInt32(tuple.Item1);
                            }
                        }
                        catch { }
                        if (QtyofExpire != Convert.ToInt32(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[8]))
                        {
                            MessageBox.Show("Please Check The Expire Dates !");
                            return false;
                        }
                    }

                }
            }

            else if(TabControl.SelectedIndex==1)
            {

                if (ManualResturanttxt.Text.Equals(""))
                {
                    MessageBox.Show("R.O# Can't Be Empty");
                    return false;
                }

                for (int i = 0; i < ItemsResturantDGV.Items.Count; i++)
                {
                    int QtyofExpire = 0;
                    if (((DataRowView)ItemsResturantDGV.Items[i]).Row.ItemArray[5].ToString() == "True")
                    {
                        string ItemCode = ((DataRowView)ItemsResturantDGV.Items[i]).Row.ItemArray[1].ToString();
                        try
                        {
                            foreach (Tuple<string, string> tuple in ExpireDate.ItemExpireDate[ItemCode])
                            {
                                QtyofExpire += Convert.ToInt32(tuple.Item1);
                            }
                        }
                        catch { }
                        if (QtyofExpire != Convert.ToInt32(((DataRowView)ItemsResturantDGV.Items[i]).Row.ItemArray[6]))
                        {
                            MessageBox.Show("Please Check The Expire Dates !");
                            return false;
                        }
                    }
                }
            }

            else if(TabControl.SelectedIndex==2)
            {
                if (ManualKitchentxt.Text.Equals(""))
                {
                    MessageBox.Show("R.O# Can't Be Empty");
                    return false;
                }

                for (int i = 0; i < ItemsKitchenDGV.Items.Count; i++)
                {
                    int QtyofExpire = 0;
                    if (((DataRowView)ItemsKitchenDGV.Items[i]).Row.ItemArray[5].ToString() == "True")
                    {
                        string ItemCode = ((DataRowView)ItemsKitchenDGV.Items[i]).Row.ItemArray[1].ToString();
                        try
                        {
                            foreach (Tuple<string, string> tuple in ExpireDate.ItemExpireDate[ItemCode])
                            {
                                QtyofExpire += Convert.ToInt32(tuple.Item1);
                            }
                        }
                        catch { }
                        if (QtyofExpire != Convert.ToInt32(((DataRowView)ItemsKitchenDGV.Items[i]).Row.ItemArray[6]))
                        {
                            MessageBox.Show("Please Check The Expire Dates !");
                            return false;
                        }
                    }

                }
            }

            else if (TabControl.SelectedIndex == 3)
            {
                if (CodeWithouttxt.Text.Equals(""))
                {
                    MessageBox.Show("R.O# Can't Be Empty");
                    return false;
                }
                else if (ManualWithouttxt.Text.Equals(""))
                {
                    MessageBox.Show("Manual R.O# Can't Be Empty");
                    return false;
                }
                else if (DeliveryWithouttxt.Text.Equals(""))
                {
                    MessageBox.Show("Date Can't Be Empty");
                    return false;
                }
                else if (Delivery_timeWithout.Text==null)
                {
                    MessageBox.Show("Time Can't Be Empty");
                    return false;
                }
                else if (ResturantCbx.Text.Equals(""))
                {
                    MessageBox.Show("Resturant R.O# Can't Be Empty");
                    return false;
                }
                else if (KitchenCbx.Text.Equals(""))
                {
                    MessageBox.Show("Kitchen R.O# Can't Be Empty");
                    return false;
                }

                else if (ItemsWithoutDGV.Items.Count == 0)
                {
                    MessageBox.Show("Items can not be empty");
                    return false;
                }
                else
                {
                    for (int i = 0; i < ItemsWithoutDGV.Items.Count; i++)
                    {

                        try
                        {
                            Convert.ToDouble((ItemsWithoutDGV.Items[i] as DataRowView).Row["Price"].ToString());
                        }
                        catch
                        {
                            MessageBox.Show("Price input error");
                            //ItemsWithoutDGV.CurrentCell = new DataGridCellInfo(ItemsWithoutDGV.Items[i], ItemsWithoutDGV.Columns[13]);  //nb2a n3dl el index kol mara nezawd column
                            //ItemsWithoutDGV.BeginEdit();
                            return false;
                        }
                        if((ItemsWithoutDGV.Items[i] as DataRowView).Row["Qty"].ToString() == "0" || (ItemsWithoutDGV.Items[i] as DataRowView).Row["Qty"].ToString() == "")
                        {
                            MessageBox.Show("Price Qty error");
                            return false;
                        }
                    }
                }

                for (int i = 0; i < ItemsWithoutDGV.Items.Count; i++)
                {
                    int qtyofexpire = 0;
                    if (((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[5].ToString() == "True")
                    {
                        string itemcode = ((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[0].ToString();
                        try
                        {
                            foreach (Tuple<string, string> tuple in ExpireDate.ItemExpireDate[itemcode])
                            {
                                qtyofexpire += Convert.ToInt32(tuple.Item1);
                            }
                        }
                        catch { }
                        if (qtyofexpire != Convert.ToInt32(((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[7]))
                        {
                            MessageBox.Show("please check the expire dates !");
                            return false;
                        }
                    }

                }
            }

            else if(TabControl.SelectedIndex == 4)
            {
                if (Request_Date.Text == "")
                {
                    MessageBox.Show("Plese Enter the Date !");
                    return false;
                }
                else if (Request_Time.Text == "")
                {
                    MessageBox.Show("Please Enter the Time !");
                    return false;
                }

                for (int i = 0; i < RequestsItemsDGV.Items.Count; i++)
                {
                    if (((DataRowView)RequestsItemsDGV.Items[i]).Row.ItemArray[0].ToString() == "False")
                    {
                        Count++;
                    }
                    else
                    {
                        if (Convert.ToDouble(((DataRowView)RequestsItemsDGV.Items[i]).Row.ItemArray[8].ToString()) < 0)
                        {
                            MessageBox.Show("You have input error !");
                            return false;
                        }
                    }
                }
                if (Count == RequestsItemsDGV.Items.Count)
                {
                    MessageBox.Show("Please Select The Items !");
                    return false;
                }

                for (int i = 0; i < RequestsItemsDGV.Items.Count; i++)
                {
                    int QtyofExpire = 0;
                    if (((DataRowView)RequestsItemsDGV.Items[i]).Row.ItemArray[6].ToString() == "True")
                    {
                        string ItemCode = ((DataRowView)RequestsItemsDGV.Items[i]).Row.ItemArray[1].ToString();
                        try
                        {
                            foreach (Tuple<string, string> tuple in ExpireDate.ItemExpireDate[ItemCode])
                            {
                                QtyofExpire += Convert.ToInt32(tuple.Item1);
                            }
                        }
                        catch { }
                        if (QtyofExpire != Convert.ToInt32(((DataRowView)RequestsItemsDGV.Items[i]).Row.ItemArray[7]))
                        {
                            MessageBox.Show("Please Check The Expire Dates !");
                            return false;
                        }
                    }

                }
            }

            return true;

        }

        //Recive Purshace 
        private void RestaurantBtn_Click(object sender, RoutedEventArgs e)
        {
            All_Resturants allRestaurant = new All_Resturants(this);
            allRestaurant.ShowDialog();
            KitchenBtn.IsEnabled = true;
        }       //Done
        private void KitchenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Restaurant.Text != "")
            {
                All_Kitchens allKitchen = new All_Kitchens(this, Restaurant.Text);
                allKitchen.ShowDialog();
            }
            if (Restaurant.Text != "" && Kitchen.Text != "")
            {
                ChoosingRestANDKitchens.Visibility = Visibility.Hidden;
                RO_PO_Detailed.Visibility = Visibility.Visible;
                LoadToDGVWithResAndKitchen();
            }
        }           //Done
        private void LoadToDGVWithResAndKitchen()
        {
            DataTable AllROs = new DataTable();
            string FiledSelection = "PO_Serial AS Number, (Select Name From Vendors Where Code = Vendor_ID)AS Vendor,Delivery_Date as 'Delivery Date'";
            string WhereCondition = string.Format("Status='Post' AND PO_Serial NOT IN (select Transactions_No from RO where Type='Recieve_Purchase') AND Restaurant_ID={0} AND Kitchen_ID={1} ORDER BY PO_Serial DESC ",RestaurantId,KitchenId);
            AllROs = Classes.RetrieveData(FiledSelection, WhereCondition, "PO");
            RecieveOrderDGV.DataContext = AllROs;
        }           //Done
        private void LoadToDGV()
        {
            DataTable AllROs = new DataTable();
            string FiledSelection = "PO_Serial AS Number, (Select Name From Vendors Where Code = Vendor_ID)AS Vendor,Delivery_Date as 'Delivery Date'";
            string WhereCondition = "Status='Post' and PO_Serial not in (select Transactions_No from RO where Type='Recieve_Purchase') ORDER BY PO_Serial DESC";
            AllROs = Classes.RetrieveData(FiledSelection, WhereCondition, "PO");
            RecieveOrderDGV.DataContext = AllROs;
        }    //Done
        private void RecieveOrderDGV_Click(object sender, MouseButtonEventArgs e)
        {
            ExpireDate.ItemExpireDate.Clear();
            CodePurchaseROtxt.Text =  Classes.InCrementTransactionSerial("RO", "RO_Serial");
            Recieve.IsEnabled = true;
            bool IfItemRecieved = true;
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    string code = (grid.SelectedItem as DataRowView).Row[0].ToString();
                    if (code == "") return;
                    string valuoSerial = "";
                    string valuoNumber = "";
                    SqlConnection con2 = new SqlConnection(Classes.DataConnString); SqlConnection con = new SqlConnection(Classes.DataConnString);
                    DataTable dt = new DataTable(); DataTable Dat = new DataTable();
                    Dat.Columns.Add(new DataColumn("Received", typeof(bool)));
                    Dat.Columns.Add("Code");
                    Dat.Columns.Add("Manual Code");
                    Dat.Columns.Add("Name");
                    Dat.Columns.Add("Name2");
                    Dat.Columns.Add("Unit");
                    Dat.Columns.Add("Expire Date", typeof(bool));
                    Dat.Columns.Add("Is_TaxableItem", typeof(bool));
                    Dat.Columns.Add("Qty");
                    Dat.Columns.Add("Price Without Tax");
                    Dat.Columns.Add("Tax");
                    Dat.Columns.Add("Price_With_Tax");
                    Dat.Columns.Add("Net_Price");
                    try
                    {
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select PO_Serial,PO_NO," + string.Format("(select Name from Vendors where Code = {0}) as Vendor", "Vendor_ID") + ",Delivery_Date,Comment,Restaurant_ID,Kitchen_ID,WS from PO where PO_Serial = '{0}'", code), con);
                        adapter.Fill(dt);

                        DataRow row = dt.Rows[0];
                        PO.Text = row["PO_NO"].ToString();
                        valuoSerial = row["PO_Serial"].ToString();
                        valuoNumber = row["PO_NO"].ToString();
                        Delivery_dt.Text = Convert.ToDateTime(row["Delivery_Date"]).ToString("dd-MM-yyyy");
                        vendortxt.Text = row["Vendor"].ToString();
                        commenttxt.Text = row["Comment"].ToString();
                    }
                    finally { con.Close(); }
                    try
                    {
                        con.Open();
                        string M = "SELECT Item_ID,Qty,Unit,Price_Without_Tax,Tax,Price_With_Tax,Net_Price,Tax_Included  FROM PO_Items Where PO_Serial='" + valuoSerial + "'";
                        SqlCommand cmd = new SqlCommand(M, con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            try
                            {
                                con2.Open();
                                string s = string.Format("SELECT Item_ID FROM RO_Items Where Item_ID='{1}' AND RO_No = (select RO_Serial from RO where Transactions_No = '{0}' and Type='Recieve_Purchase')", valuoSerial, reader["Item_ID"]);
                                SqlCommand cmd2 = new SqlCommand(s, con2);
                                if (cmd2.ExecuteScalar() != null)
                                {
                                    IfItemRecieved = true;
                                }
                                else { IfItemRecieved = false; }

                            }
                            catch { }
                            finally { con2.Close(); }
                            try
                            {
                                con2.Open();
                                string S = "SELECT Name,Name2,[Manual Code],ExpDate FROM Setup_Items where Code='" + reader["Item_ID"].ToString() + "'";
                                SqlCommand cmd2 = new SqlCommand(S, con2);
                                SqlDataReader reader2 = cmd2.ExecuteReader();
                                while (reader2.Read())
                                {
                                    Dat.Rows.Add(IfItemRecieved, reader["Item_ID"], reader2["Manual Code"], reader2["Name"], reader2["Name2"], reader["Unit"], reader2["ExpDate"], reader["Tax_Included"], reader["Qty"], reader["Price_Without_Tax"], reader["Tax"], reader["Price_With_Tax"], reader["Net_Price"]);
                                }
                                reader2.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            finally
                            {
                                con2.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                    }
                    for (int i = 0; i < Dat.Columns.Count; i++)
                    {
                        Dat.Columns[i].ReadOnly = true;
                    }
                    Dat.Columns["Received"].ReadOnly = false;
                    Dat.Columns["Qty"].ReadOnly = false;
                    ItemsDGV.DataContext = Dat;
                    float Price_With_Tax_Purchase = 0; float Price_Without_Tax_Purchase = 0;
                    for (int i = 0; i < ItemsDGV.Items.Count; i++)
                    {
                        Price_With_Tax_Purchase += float.Parse(Dat.Rows[i]["Net_Price"].ToString());
                        Price_Without_Tax_Purchase += (float.Parse(Dat.Rows[i]["Qty"].ToString()) * float.Parse(Dat.Rows[i]["Price Without Tax"].ToString()));
                    }
                    Total_Price_Without_Tax_Purchase.Text = Price_Without_Tax_Purchase.ToString();
                    Total_Price_With_Tax_Purchase.Text = Price_With_Tax_Purchase.ToString();
                }
            }
        }  //Done
        private void Row_Changed(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                DataTable Dat = new DataTable();
                Dat = (ItemsDGV.DataContext as DataTable);
                Dat.Columns["Net_Price"].ReadOnly = false;

                float Qty = float.Parse((e.Row.Item as DataRowView).Row["Qty"].ToString());
                float Price = float.Parse((e.Row.Item as DataRowView).Row["Price_With_Tax"].ToString());
                if (e.Column.Header.ToString() == "Qty")
                {
                    (e.Row.Item as DataRowView).Row["Net_Price"] = (double.Parse((e.EditingElement as TextBox).Text) * Convert.ToDouble(((DataRowView)ItemsDGV.SelectedItem).Row.ItemArray[11])).ToString();
                }
                Dat.Columns["Net_Price"].ReadOnly = true;
                float Price_With_Tax_Purchase = 0; float Price_Without_Tax_Purchase = 0;
                for (int i = 0; i < ItemsDGV.Items.Count; i++)
                {
                    if(e.Row.GetIndex()==i)
                    {
                        Price_With_Tax_Purchase += float.Parse(Dat.Rows[i]["Net_Price"].ToString());
                        Price_Without_Tax_Purchase += (float.Parse((e.EditingElement as TextBox).Text) * float.Parse(Dat.Rows[i]["Price Without Tax"].ToString()));
                    }
                    else
                    {
                        Price_With_Tax_Purchase += float.Parse(Dat.Rows[i]["Net_Price"].ToString());
                        Price_Without_Tax_Purchase += (float.Parse(Dat.Rows[i]["Qty"].ToString()) * float.Parse(Dat.Rows[i]["Price Without Tax"].ToString()));
                    }
                    
                }
                Total_Price_Without_Tax_Purchase.Text = Price_Without_Tax_Purchase.ToString();
                Total_Price_With_Tax_Purchase.Text = Price_With_Tax_Purchase.ToString();
            }
            catch { }
        }   //Done
        private void RecievedBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticatedPO.IndexOf("RecieveROPO") == -1 && AuthenticatedPO.IndexOf("CheckAllROPO") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                string QTyonHand = ""; string CostOfItemsOnHand = ""; string QtyOnHandMultipleCost = "";

                if (!DoSomeChecks())
                    return;
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                SqlConnection con2 = new SqlConnection(Classes.DataConnString);
                SqlCommand cmd = new SqlCommand();
                try
                {
                    con.Open();
                    DataTable dt = ItemsDGV.DataContext as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        QTyonHand = ""; CostOfItemsOnHand = ""; QtyOnHandMultipleCost = "";
                        if (dt.Rows[i]["Received"].ToString() == "False")
                        {
                            continue;
                        }
                        con2.Open();
                        string s = string.Format("select Qty,Current_Cost From Items where RestaurantID={1} and KitchenID={2} and ItemID={0}", dt.Rows[i]["Code"].ToString(),RestaurantId,KitchenId);
                        SqlCommand _CMD = new SqlCommand(s, con2);
                        SqlDataReader _reader = _CMD.ExecuteReader();
                        while (_reader.Read())
                        {
                            QTyonHand = (Convert.ToDouble(_reader["Qty"].ToString())).ToString();
                            CostOfItemsOnHand = _reader["Current_Cost"].ToString();
                        }
                        con2.Close();
                        try
                        {
                            QtyOnHandMultipleCost = (Convert.ToDouble(QTyonHand) * Convert.ToDouble(CostOfItemsOnHand)).ToString();
                            QTyonHand = (Convert.ToDouble(QTyonHand) + Convert.ToDouble(dt.Rows[i]["Qty"].ToString())).ToString();
                            CostOfItemsOnHand = ((Convert.ToDouble(QtyOnHandMultipleCost) + (Convert.ToDouble(dt.Rows[i]["Qty"].ToString()) * Convert.ToDouble(dt.Rows[i]["Price_With_Tax"]))) / Convert.ToDouble(QTyonHand)).ToString();
                        }
                        catch
                        {
                            QTyonHand = dt.Rows[i]["Qty"].ToString();
                            CostOfItemsOnHand = dt.Rows[i]["Price_With_Tax"].ToString();
                        }
                        //
                        FiledSelection = "Item_ID,RO_No,Qty,Unit,Serial,Price_Without_Tax,Tax,Price_With_Tax,Net_Price,QtyOnHand_To,Cost_To";
                        Values = "'" + dt.Rows[i]["Code"].ToString() + "','" + CodePurchaseROtxt.Text + "'," + dt.Rows[i]["Qty"] + ",N'" + dt.Rows[i]["Unit"] + "','" + i +"',"+ dt.Rows[i]["Price Without Tax"] + "," + dt.Rows[i]["Tax"] + "," + dt.Rows[i]["Price_With_Tax"] + "," + dt.Rows[i]["Net_Price"] + ",'" + QTyonHand + "','" + CostOfItemsOnHand + "'";
                        Classes.InsertRow("RO_Items", FiledSelection, Values);

                        try
                        {
                            foreach (Tuple<string, string> tuple in ExpireDate.ItemExpireDate[dt.Rows[i]["Code"].ToString()])
                            {
                                string q = string.Format("update ItemsExpireDate set Qty=Qty+'{0}' where RestaurantID='{1}' and KitchenID='{2}' and ItemID='{3}' and ExpireDate='{4}'", tuple.Item1, RestaurantId, KitchenId, dt.Rows[i]["Code"].ToString(), Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                SqlCommand TheCmd = new SqlCommand(q, con);
                                int W = TheCmd.ExecuteNonQuery();

                                if (W == 0)
                                {
                                    FiledSelection = "RestaurantID,KitchenID,ItemID,QTy,ExpireDate";
                                    Values = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}'", RestaurantId, KitchenId, dt.Rows[i]["Code"], tuple.Item1, Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                    Classes.InsertRow("ItemsExpireDate", FiledSelection, Values);
                                }
                            }
                        }
                        catch { }

                        s = string.Format("Update Items set Qty = Qty + {1},Last_Cost = Current_Cost,Current_Cost = ((Current_Cost * Qty)+({1} * {3}))/(Qty+{1}),Units = '{4}',Net_Cost=((((Current_Cost * Qty)+({1} * {3}))/(Qty+{1})) * (Qty+{5})) where ItemID = '{0}' and RestaurantID ={6} and KitchenID = {7}", dt.Rows[i]["Code"], dt.Rows[i]["QTy"], "1", dt.Rows[i]["Price_With_Tax"], dt.Rows[i]["Unit"], dt.Rows[i]["Qty"],RestaurantId,KitchenId);
                        SqlCommand _cmd = new SqlCommand(s, con);
                        int n = _cmd.ExecuteNonQuery();
                        if (n == 0)
                        {
                            FiledSelection = "RestaurantID,KitchenID,ItemID,Qty,Units,Last_Cost,Current_Cost,Net_Cost";
                            Values = string.Format("{0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'", RestaurantId, KitchenId, dt.Rows[i]["Code"], dt.Rows[i]["Qty"], "", dt.Rows[i]["Price_With_Tax"], dt.Rows[i]["Price_With_Tax"], dt.Rows[i]["Net_Price"]);
                            Classes.InsertRow("Items", FiledSelection, Values);
                        }

                        // ana Hena basma3 fe Table al ItemsYear
                        s = string.Format("Update ItemsYear set {0} = {0} + {2},{1} = (({1}* {0})+({2} * {3}))/({0}+{2}) where Year='{5}' and ItemID = '{4}' and Restaurant_ID ={6} and Kitchen_ID = {7}",MainWindow.MonthQty,MainWindow.MonthCost, dt.Rows[i]["QTy"], dt.Rows[i]["Price_With_Tax"], dt.Rows[i]["Code"], MainWindow.CurrentYear,RestaurantId,KitchenId);
                        _cmd = new SqlCommand(s, con);
                        n = _cmd.ExecuteNonQuery();
                        if (n == 0)
                        {
                            FiledSelection = string.Format("ItemID,Restaurant_ID,Kitchen_ID,Year,{0},{1}", MainWindow.MonthQty, MainWindow.MonthCost);
                            Values = string.Format("'{0}',{1},{2},'{3}',{4},{5}", dt.Rows[i]["Code"], RestaurantId, KitchenId, MainWindow.CurrentYear, dt.Rows[i]["QTy"], dt.Rows[i]["Price_With_Tax"]);
                            Classes.InsertRow("ItemsYear", FiledSelection, Values);
                        }

                    }

                    FiledSelection = "RO_Serial,RO_No,Transactions_No,Status,Receiving_Date,Resturant_ID,Kitchen_ID,WS,Type,Comment,UserID,Create_Date,Total_Cost";
                    Values = string.Format("'{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}',N'{9}','{10}',GETDATE(),'{11}'", CodePurchaseROtxt.Text, ManualPurchaseROtxt.Text, (RecieveOrderDGV.SelectedItem as DataRowView).Row.ItemArray[0].ToString(), "Recieved", Convert.ToDateTime(Delivery_dt.Text).ToString("MM-dd-yyyy") + " " + DateTime.Now.ToString("HH:mm:ss"),RestaurantId,KitchenId, Classes.WS, "Recieve_Purchase", commenttxt.Text, MainWindow.UserID, Total_Price_With_Tax_Purchase.Text);
                    Classes.InsertRow("RO", FiledSelection, Values);
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                finally
                {
                    MessageBox.Show("Order Recived Succesfully");
                    ExpireDate.ItemExpireDate.Clear();
                    con.Close();
                }
                EnableUI();
                ClearFields();
                MainUiFormat();
                LoadToDGV();
            }
        }       //Done

        //Transfer Restaurant 

        private void recieveTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticatedRsturant.IndexOf("RecieveROResturant") == -1 && AuthenticatedRsturant.IndexOf("CheckAllROResturant") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                if (!DoSomeChecks())
                    return;

                DataTable dt = ItemsResturantDGV.DataContext as DataTable;
                SqlConnection con = new SqlConnection(Classes.DataConnString);

                try
                {
                    con.Open();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Received"].ToString() == "False")
                            break;

                        float netCost = float.Parse(dt.Rows[i]["Qty"].ToString()) * float.Parse(dt.Rows[i][FromKitchen_Resturanttxt.Text + " Unit Cost"].ToString());

                        string s = "insert RO_Items (Item_ID,RO_No,Qty,Unit,Price_Without_Tax,Tax,Price_With_Tax,Net_Price,QtyOnHand_To,Cost_To,QtyOnHand_From,Cost_From,Recipes) values ('" + dt.Rows[i]["ItemID"].ToString() + "','" + CodeResturanttxt.Text + "'," + dt.Rows[i]["Qty"] + ",'" + dt.Rows[i]["Unit"] + "'," + dt.Rows[i][FromKitchen_Resturanttxt.Text + " Unit Cost"] + ",0 ," + dt.Rows[i][FromKitchen_Resturanttxt.Text + " Unit Cost"] + "," + netCost + "," + dt.Rows[i][ToKitchen_Restauranttxt.Text + " Qty"] + "," + dt.Rows[i][ToKitchen_Restauranttxt.Text + " Unit Cost"] + "," + dt.Rows[i][FromKitchen_Resturanttxt.Text + " Qty"] + "," + dt.Rows[i][FromKitchen_Resturanttxt.Text + " Unit Cost"] + ",'"+ dt.Rows[i]["Recipe"] + "')";
                        SqlCommand _CMD = new SqlCommand(s, con);
                        _CMD.ExecuteNonQuery();
                        if ((bool)dt.Rows[i]["Recipe"] == true)
                        {
                            s = string.Format("update RecipeQty set Qty=Qty+'{0}',Price={4} where Resturant_ID=(select Code From Setup_Restaurant Where Name='{1}') and Kitchen_ID=(select Code From Setup_Kitchens where Name='{2}' and RestaurantID=(select Code From Setup_Restaurant where Name='{1}')) and Recipe_ID='{3}'", dt.Rows[i]["Qty"], ToResturant_Restauranttxt.Text, ToKitchen_Restauranttxt.Text, dt.Rows[i]["ItemID"], dt.Rows[i][ToKitchen_Restauranttxt.Text + " Unit Cost"]);
                            _CMD = new SqlCommand(s, con);
                            int IfNot = _CMD.ExecuteNonQuery();

                            if (IfNot == 0)
                            {
                                s = string.Format("insert RecipeQty values('{0}',{1},(select Code From Setup_Restaurant where Name='{2}'),(select Code From Setup_Kitchens where Name='{3}' and RestaurantID=(select Code From Setup_Restaurant where Name='{2}')),{4})", dt.Rows[i]["ItemID"], dt.Rows[i]["Qty"], ToResturant_Restauranttxt.Text, ToKitchen_Restauranttxt.Text, dt.Rows[i][ToKitchen_Restauranttxt.Text + " Unit Cost"]);
                                _CMD = new SqlCommand(s, con);
                                _CMD.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            try
                            {
                                foreach (Tuple<string, string> tuple in ExpireDate.ItemExpireDate[dt.Rows[i]["ItemID"].ToString()])
                                {
                                    s = string.Format("update ItemsExpireDate set Qty=Qty-{0} where RestaurantID=(select Code From Setup_Restaurant where Name='{1}') and KitchenID=(select Code From Setup_Kitchens where Name='{2}' and RestaurantID=(select Code From Setup_Restaurant where Name='{1}'))  and ItemID='{3}' and ExpireDate='{4}'", tuple.Item1, FromRestaurant_Resturanttxt.Text, FromKitchen_Resturanttxt.Text, dt.Rows[i]["ItemID"], Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                    SqlCommand TheCMD = new SqlCommand(s, con);
                                    TheCMD.ExecuteNonQuery();

                                    s = string.Format("update ItemsExpireDate set Qty=Qty+{0} where RestaurantID=(select Code From Setup_Restaurant where Name='{1}') and KitchenID=(select Code From Setup_Kitchens where Name='{2}' and RestaurantID=(select Code From Setup_Restaurant where Name='{1}')) and ItemID='{3}' and ExpireDate='{4}'", tuple.Item1, ToResturant_Restauranttxt.Text, ToKitchen_Restauranttxt.Text, dt.Rows[i]["ItemID"], Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                    TheCMD = new SqlCommand(s, con);
                                    int IfNot = TheCMD.ExecuteNonQuery();

                                    if (IfNot == 0)
                                    {
                                        s = string.Format("insert ItemsExpireDate values((select Code From Setup_Restaurant where Name='{0}'),(select Code From Setup_Kitchens where Name='{1}' and RestaurantID=(select Code From Setup_Restaurant where Name='{0}')),'{2}','{3}','{4}')", ToResturant_Restauranttxt.Text, ToKitchen_Restauranttxt.Text, dt.Rows[i]["ItemID"], tuple.Item1, Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                        TheCMD = new SqlCommand(s, con);
                                        TheCMD.ExecuteNonQuery();
                                    }

                                }
                            }
                            catch { }


                            //ana hne bzbat Table al ItemsYear
                            s = string.Format("Update ItemsYear set {0} = {0} + {2},{1} = (({1}* {0})+({2} * {3}))/({0}+{2}) where Year='{5}' and ItemID = '{4}' and Restaurant_ID =(select Code From Setup_Restaurant where Name='{6}') and Kitchen_ID =(select Code From Setup_Kitchens where Name='{7}' and RestaurantID=(select Code From Setup_Restaurant where Name='{6}'))", MainWindow.MonthQty, MainWindow.MonthCost, dt.Rows[i][ToKitchen_Restauranttxt.Text + " Qty"], dt.Rows[i][ToKitchen_Restauranttxt.Text + " Unit Cost"], dt.Rows[i]["ItemID"], MainWindow.CurrentYear, ToResturant_Restauranttxt.Text, ToKitchen_Restauranttxt.Text);
                            SqlCommand _cmd = new SqlCommand(s, con);
                            int n = _cmd.ExecuteNonQuery();
                            if (n == 0)
                            {
                                FiledSelection = string.Format("ItemID,Restaurant_ID,Kitchen_ID,Year,{0},{1}", MainWindow.MonthQty, MainWindow.MonthCost);
                                Values = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}'", dt.Rows[i]["ItemID"], Classes.RetrieveRestaurantCode(ToResturant_Restauranttxt.Text), Classes.RetrieveKitchenCode(ToKitchen_Restauranttxt.Text, ToResturant_Restauranttxt.Text), MainWindow.CurrentYear, dt.Rows[i][ToKitchen_Restauranttxt.Text + " Qty"], dt.Rows[i][ToKitchen_Restauranttxt.Text + " Unit Cost"]);
                                Classes.InsertRow("ItemsYear", FiledSelection, Values);
                            }

                            //s = string.Format("Update Items set Qty= {1},Net_Cost={4} where ItemID = '{0}' and RestaurantID =(select Code From Setup_Restaurant where Name='{2}') and KitchenID=(Select Code from Setup_Kitchens Where Name='{3}')", dt.Rows[i]["ItemID"], dt.Rows[i][FromKitchen_Resturanttxt.Text + " Qty"], FromRestaurant_Resturanttxt.Text, FromKitchen_Resturanttxt.Text, dt.Rows[i][FromKitchen_Resturanttxt.Text + " total Cost"]);
                            //SqlCommand _cmd = new SqlCommand(s, con);
                            //_cmd.ExecuteNonQuery();

                            s = string.Format("Update Items set Qty= {1},Last_Cost = Current_Cost,Current_Cost = '{4}',Net_Cost='{5}' where ItemID = '{0}' and RestaurantID =(select Code From Setup_Restaurant Where Name='{2}') and KitchenID=(select Code From Setup_Kitchens Where Name='{3}')", dt.Rows[i]["ItemID"], dt.Rows[i][ToKitchen_Restauranttxt.Text + " Qty"], ToResturant_Restauranttxt.Text, ToKitchen_Restauranttxt.Text, dt.Rows[i][ToKitchen_Restauranttxt.Text + " Unit Cost"], dt.Rows[i][ToKitchen_Restauranttxt.Text + " total Cost"]);
                            _cmd = new SqlCommand(s, con);
                            n = _cmd.ExecuteNonQuery();

                            if (n == 0)
                            {
                                s = string.Format("insert into Items(RestaurantID,KitchenID,ItemID,Qty,Units,Last_Cost,Current_Cost,Net_Cost) Values ((select Code From Setup_Restaurant WHere Name='{0}'),(	select Code From Setup_Kitchens WHere Name='{1}'),'{2}','{3}','{4}','{5}','{6}','{7}')", ToResturant_Restauranttxt.Text, ToKitchen_Restauranttxt.Text, dt.Rows[i]["ItemID"], dt.Rows[i]["Qty"], "", dt.Rows[i][ToKitchen_Restauranttxt.Text + " Unit Cost"], dt.Rows[i][ToKitchen_Restauranttxt.Text + " Unit Cost"], dt.Rows[i][ToKitchen_Restauranttxt.Text + " total Cost"]);
                                _cmd = new SqlCommand(s, con);
                                _cmd.ExecuteNonQuery();
                            }

                        }
                    }

                    SqlCommand cmd = new SqlCommand(string.Format("Insert into RO(RO_Serial,RO_No,Transactions_No,Status,Create_Date,Receiving_Date,Resturant_ID,Kitchen_ID,WS,Type,Comment,UserID,Total_Cost)Values ('{0}','{1}','{2}','{3}',GETDATE(),'{4}',(select Code From Setup_Restaurant WHere Name='{5}'),(select Code From Setup_Kitchens WHere Name='{6}'),'{7}','{8}','{9}','{10}','{11}')", CodeResturanttxt.Text, ManualResturanttxt.Text, TransferResturantID, "Recieved",Convert.ToDateTime(DeliveryRestauranttxt.Text).ToString("MM-dd-yyyy") + " " + DateTime.Now.ToString("HH:mm:ss"), ToResturant_Restauranttxt.Text, ToKitchen_Restauranttxt.Text, Classes.WS, "Transfer_Resturant", commenttxt.Text, MainWindow.UserID, Total_Price_With_Tax_Resturant.Text), con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                finally
                {
                    MessageBox.Show("Order Recived saved Succesful");
                    con.Close();
                }
                ClearFields();
                MainUiFormat();
            }
        }       //Done
        private void SearchKitchen_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticatedRsturant.IndexOf("SearchROResturant") == -1 && AuthenticatedRsturant.IndexOf("CheckAllROResturant") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                CodeResturanttxt.Text = Classes.InCrementTransactionSerial("RO", "RO_Serial");
                All_Purchase_Orders all_Purchase_Orders = new All_Purchase_Orders(this);
                all_Purchase_Orders.ShowDialog();
            }
        }       //Done
        private void Recieve_Restaurant_Transfer_Change(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header == "Qty")
            {
                DataTable Dat = ItemsDGV.DataContext as DataTable;
                float to_rest_Qty = 0; float to_rest_Cost = 0; float from_rest_Qty = 0; float from_rest_Cost = 0;
                for (int i = 0; i < Dat.Columns.Count; i++)
                { Dat.Columns[i].ReadOnly = false; }
                string ItemCode = (e.Row.Item as DataRowView).Row["ItemID"].ToString();

                if ((bool)(e.Row.Item as DataRowView).Row["Recipe"] == true)
                {
                    try
                    {
                        DataTable TheValue = Classes.RetriveCostAndQtyRecipes(FromRestaurant_Resturanttxt.Text, FromKitchen_Resturanttxt.Text, ItemCode);
                        try
                        {
                            from_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString());
                            from_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString());
                        }
                        catch
                        {

                            from_rest_Qty = 0;
                            from_rest_Cost = 0;
                        }
                        TheValue = Classes.RetriveCostAndQtyRecipes(FromRestaurant_Resturanttxt.Text, FromKitchen_Resturanttxt.Text, ItemCode);
                        try
                        {
                            to_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString());
                            to_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString());
                        }
                        catch
                        {
                            to_rest_Qty = 0;
                            to_rest_Cost = 0;
                        }
                        Dat.Rows[e.Row.GetIndex()]["Qty"] = (float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Qty"] = (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Unit Cost"] = from_rest_Cost.ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Total Cost"] = (from_rest_Cost * (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text))).ToString();

                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Qty"] = (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Unit Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text)))).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Total Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text))) * (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text))).ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        (e.EditingElement as TextBox).Text = "";
                    }
                    try
                    {
                        double totalPrice = 0;
                        for (int i = 0; i < ItemsDGV.Items.Count; i++)
                        {
                            try
                            {
                                totalPrice += (Convert.ToDouble(Dat.Rows[i]["Qty"]) * Convert.ToDouble(Dat.Rows[i][FromKitchen_Resturanttxt.Text + " Unit Cost"]));
                            }
                            catch { }
                        }
                        NumberOfItemsKitchen.Text = (ItemsDGV.Items.Count).ToString();
                        Total_Price_With_Tax_Kitchen.Text = (totalPrice).ToString();
                    }
                    catch { }
                    for (int i = 0; i < Dat.Columns.Count; i++)
                    {
                        Dat.Columns[i].ReadOnly = true;
                    }
                    Dat.Columns["Qty"].ReadOnly = false;
                    ItemsDGV.DataContext = Dat;
                }
                else
                {
                    try
                    {
                        DataTable TheValue = Classes.RetriveCostAndQty(FromRestaurant_Resturanttxt.Text, FromKitchen_Resturanttxt.Text, ItemCode);
                        try
                        {
                            from_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString());
                            from_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString());
                        }
                        catch
                        {

                            from_rest_Qty = 0;
                            from_rest_Cost = 0;
                        }
                        TheValue = Classes.RetriveCostAndQty(FromRestaurant_Resturanttxt.Text, FromKitchen_Resturanttxt.Text, ItemCode);
                        try
                        {
                            to_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString());
                            to_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString());
                        }
                        catch
                        {
                            to_rest_Qty = 0;
                            to_rest_Cost = 0;
                        }
                        Dat.Rows[e.Row.GetIndex()]["Qty"] = (float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Qty"] = (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Unit Cost"] = from_rest_Cost.ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Total Cost"] = (from_rest_Cost * (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text))).ToString();

                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Qty"] = (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Unit Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text)))).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Total Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text))) * (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text))).ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        (e.EditingElement as TextBox).Text = "";
                    }
                    try
                    {
                        double totalPrice = 0;
                        for (int i = 0; i < ItemsDGV.Items.Count; i++)
                        {
                            try
                            {
                                totalPrice += (Convert.ToDouble(Dat.Rows[i]["Qty"]) * Convert.ToDouble(Dat.Rows[i][FromKitchen_Resturanttxt.Text + " Unit Cost"]));
                            }
                            catch
                            {

                            }
                        }
                        NumberOfItemsKitchen.Text = (ItemsDGV.Items.Count).ToString();
                        Total_Price_With_Tax_Kitchen.Text = (totalPrice).ToString();
                    }
                    catch { }
                    for (int i = 0; i < Dat.Columns.Count; i++)
                    {
                        Dat.Columns[i].ReadOnly = true;
                    }
                    Dat.Columns["Qty"].ReadOnly = false;
                    ItemsDGV.DataContext = Dat;
                }
            }

            //
            /*if (e.Column.Header == "Qty")
            {
                DataTable Dat = ItemsResturantDGV.DataContext as DataTable;
                float to_rest_Qty = 0; float to_rest_Cost = 0; float from_rest_Qty = 0; float from_rest_Cost = 0;
                for (int i = 0; i < Dat.Columns.Count; i++)
                { Dat.Columns[i].ReadOnly = false; }
                string ItemCode = (e.Row.Item as DataRowView).Row["ItemID"].ToString();

                try
                {
                    if ((bool)(e.Row.Item as DataRowView).Row["Recipe"] == true)
                    {
                        DataTable TheValues = Classes.RetriveCostAndQtyRecipes(FromRestaurant_Resturanttxt.Text, FromKitchen_Resturanttxt.Text, ItemCode);
                        try
                        {
                            from_rest_Qty = float.Parse(TheValues.Rows[0][0].ToString());
                            from_rest_Cost = float.Parse(TheValues.Rows[0][1].ToString());
                        }
                        catch
                        {
                            from_rest_Qty = 0;
                            from_rest_Cost = 0;
                        }
                        TheValues = Classes.RetriveCostAndQtyRecipes(ToResturant_Restauranttxt.Text, ToKitchen_Restauranttxt.Text, ItemCode);
                        try
                        {
                            to_rest_Qty = float.Parse(TheValues.Rows[0][0].ToString());
                            to_rest_Cost = float.Parse(TheValues.Rows[0][1].ToString());
                        }
                        catch
                        {

                            to_rest_Qty = 0;
                            to_rest_Cost = 0;
                        }

                        Dat.Rows[e.Row.GetIndex()]["Qty"] = (float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Qty"] = (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Unit Cost"] = from_rest_Cost.ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Total Cost"] = (from_rest_Cost * (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text))).ToString();

                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Qty"] = (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Unit Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text)))).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Total Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text))) * (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text))).ToString();
                    }
                    else
                    {
                        DataTable TheValues = Classes.RetriveCostAndQty(FromRestaurant_Resturanttxt.Text, FromKitchen_Resturanttxt.Text, ItemCode);
                        try
                        {
                            from_rest_Qty = float.Parse(TheValues.Rows[0][0].ToString());
                            from_rest_Cost = float.Parse(TheValues.Rows[0][1].ToString());
                        }
                        catch
                        {
                            from_rest_Qty = 0;
                            from_rest_Cost = 0;
                        }
                        TheValues = Classes.RetriveCostAndQty(ToResturant_Restauranttxt.Text, ToKitchen_Restauranttxt.Text, ItemCode);
                        try
                        {
                            to_rest_Qty = float.Parse(TheValues.Rows[0][0].ToString());
                            to_rest_Cost = float.Parse(TheValues.Rows[0][1].ToString());
                        }
                        catch
                        {

                            to_rest_Qty = 0;
                            to_rest_Cost = 0;
                        }

                        Dat.Rows[e.Row.GetIndex()]["Qty"] = (float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Qty"] = (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Unit Cost"] = from_rest_Cost.ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Resturanttxt.Text + " Total Cost"] = (from_rest_Cost * (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text))).ToString();

                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Qty"] = (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Unit Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text)))).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Restauranttxt.Text + " Total Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text))) * (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text))).ToString();
                    }
                    try
                    {
                        double totalPrice = 0;
                        for (int i = 0; i < ItemsResturantDGV.Items.Count; i++)
                        {
                            try
                            {
                                totalPrice += (float.Parse(Dat.Rows[i]["Qty"].ToString()) * float.Parse(Dat.Rows[i][FromKitchen_Resturanttxt.Text + " Unit Cost"].ToString()));
                            }
                            catch
                            {

                            }
                        }
                        NumberOfItemsKitchen.Text = (ItemsResturantDGV.Items.Count).ToString();
                        Total_Price_With_Tax_Kitchen.Text = (totalPrice).ToString();
                    }
                    catch { }
                    for (int i = 0; i < Dat.Columns.Count; i++)
                    {
                        Dat.Columns[i].ReadOnly = true;
                    }
                    Dat.Columns["Received"].ReadOnly = false;
                    Dat.Columns["Qty"].ReadOnly = false;
                    ItemsResturantDGV.DataContext = null;
                    ItemsResturantDGV.DataContext = Dat;
                }
                catch { }
            } */
        }    //Done


        //Transfer Kitchen

        private void Recieve_Kitchen_Transfer_Change(object sender, DataGridCellEditEndingEventArgs e)
        {
            if(e.Column.Header=="Qty")
            {
                DataTable Dat = ItemsKitchenDGV.DataContext as DataTable;
                for (int i = 0; i < Dat.Columns.Count; i++)
                {
                    Dat.Columns[i].ReadOnly = false;
                }
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                con.Open();
                string ItemCode = (e.Row.Item as DataRowView).Row["ItemID"].ToString();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(string.Format("select Qty,Current_Cost from Items where ItemID = '{0}' and RestaurantID = (select Code from Setup_Restaurant where Name = '{1}') and KitchenID = (select Code from Setup_Kitchens where Name = '{2}') union all select Qty, Current_Cost from Items where ItemID = '{0}' and RestaurantID = (select Code from Setup_Restaurant where Name = '{1}') and KitchenID = (select Code from Setup_Kitchens where Name = '{3}')", ItemCode, Resturant_Kitchentxt.Text, FromKitchen_Kitchentxt.Text, ToKitchen_Kitchentxt.Text), con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        float from_rest_Qty = 0; float from_rest_Cost = 0;
                        try
                        {
                            from_rest_Qty = float.Parse(reader["Qty"].ToString());
                            from_rest_Cost = float.Parse(reader["Current_Cost"].ToString());
                        }
                        catch
                        {

                            from_rest_Qty = 0;
                            from_rest_Cost = 0;
                        }

                        reader.Read();
                        float to_rest_Qty = 0; float to_rest_Cost = 0;
                        try
                        {
                            to_rest_Qty = float.Parse(reader["Qty"].ToString());
                            to_rest_Cost = float.Parse(reader["Current_Cost"].ToString());
                        }
                        catch
                        {
                            to_rest_Qty = 0;
                            to_rest_Cost = 0;
                        }

                        Dat.Rows[e.Row.GetIndex()]["Qty"] = (float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Kitchentxt.Text + " Qty"] = (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Kitchentxt.Text + " Unit Cost"] = from_rest_Cost.ToString();
                        Dat.Rows[e.Row.GetIndex()][FromKitchen_Kitchentxt.Text + " Total Cost"] = (from_rest_Cost * (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text))).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Kitchentxt.Text + " Qty"] = (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Kitchentxt.Text + " Unit Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text)))).ToString();
                        Dat.Rows[e.Row.GetIndex()][ToKitchen_Kitchentxt.Text + " Total Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text))) * (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text))).ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    (e.EditingElement as TextBox).Text = "";
                }
                finally { con.Close(); }

                try
                {

                    double totalPrice = 0;
                    for (int i = 0; i < ItemsKitchenDGV.Items.Count; i++)
                    {
                        try
                        {
                            totalPrice += Convert.ToDouble(((DataRowView)ItemsKitchenDGV.Items[i]).Row.ItemArray[11]);
                        }
                        catch
                        {

                        }
                    }
                    NumberOfItemsKitchen.Text = (ItemsKitchenDGV.Items.Count).ToString();
                    Total_Price_With_Tax_Kitchen.Text = (totalPrice).ToString();
                }
                catch { }
                for (int i = 0; i < Dat.Columns.Count; i++)
                {
                    Dat.Columns[i].ReadOnly = true;
                }
                Dat.Columns["Qty"].ReadOnly = false;
                Dat.Columns["Received"].ReadOnly = false;
            }
            
        }       //Done
        private void recieveInterTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticatedKitchen.IndexOf("RecieveROKitchen") == -1 && AuthenticatedKitchen.IndexOf("CheckAllROKitchen") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                if (!DoSomeChecks())
                    return;

                DataTable dt = ItemsKitchenDGV.DataContext as DataTable;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString);

                try
                {
                    con.Open();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Received"].ToString() == "False")
                            break;

                        float netCost = float.Parse(dt.Rows[i]["Qty"].ToString()) * float.Parse(dt.Rows[i][FromKitchen_Kitchentxt.Text + " Unit Cost"].ToString());
                        string s = "insert RO_Items (Item_ID,RO_No,Qty,Unit,Price_Without_Tax,Tax,Price_With_Tax,Net_Price,QtyOnHand_To,Cost_To,QtyOnHand_From,Cost_From,Recipes) values ('" + dt.Rows[i]["ItemID"].ToString() + "','" + CodeKitchentxt.Text + "'," + dt.Rows[i]["Qty"] + ",'" + dt.Rows[i]["Unit"] + "'," + dt.Rows[i][FromKitchen_Kitchentxt.Text + " Unit Cost"] + ",0 ," + dt.Rows[i][FromKitchen_Kitchentxt.Text + " Unit Cost"] + "," + netCost + "," + dt.Rows[i][ToKitchen_Kitchentxt.Text + " Qty"] + "," + dt.Rows[i][ToKitchen_Kitchentxt.Text + " Unit Cost"] + "," + dt.Rows[i][FromKitchen_Kitchentxt.Text + " Qty"] + "," + dt.Rows[i][FromKitchen_Kitchentxt.Text + " Unit Cost"] + ",'" + dt.Rows[i]["Recipe"] + "')";
                        SqlCommand _CMD = new SqlCommand(s, con);
                        _CMD.ExecuteNonQuery();
                        if ((bool)dt.Rows[i]["Recipe"] == true)
                        {
                            s = string.Format("update RecipeQty set Qty=Qty+'{0}',Price={4} where Resturant_ID=(select Code From Setup_Restaurant Where Name='{1}') and Kitchen_ID=(select Code From Setup_Kitchens where Name='{2}' and RestaurantID=(select Code From Setup_Restaurant where Name='{1}')) and Recipe_ID='{3}'", dt.Rows[i]["Qty"], Resturant_Kitchentxt.Text, ToKitchen_Kitchentxt.Text, dt.Rows[i]["ItemID"], dt.Rows[i][ToKitchen_Kitchentxt.Text + " Unit Cost"]);
                            _CMD = new SqlCommand(s, con);
                            int IfNot = _CMD.ExecuteNonQuery();

                            if (IfNot == 0)
                            {
                                s = string.Format("insert RecipeQty values('{0}',{1},(select Code From Setup_Restaurant where Name='{2}'),(select Code From Setup_Kitchens where Name='{3}' and RestaurantID=(select Code From Setup_Restaurant where Name='{2}')),{4})", dt.Rows[i]["ItemID"], dt.Rows[i]["Qty"], Resturant_Kitchentxt.Text, ToKitchen_Kitchentxt.Text, dt.Rows[i][ToKitchen_Kitchentxt.Text + " Unit Cost"]);
                                _CMD = new SqlCommand(s, con);
                                _CMD.ExecuteNonQuery();
                            }
                        }
                        else
                        {

                            try
                            {
                                foreach (Tuple<string, string> tuple in ExpireDate.ItemExpireDate[dt.Rows[i]["ItemID"].ToString()])
                                {
                                    s = string.Format("update ItemsExpireDate set Qty=Qty-{0} where RestaurantID=(select Code From Setup_Restaurant where Name='{1}') and KitchenID=(select Code From Setup_Kitchens where Name='{2}' and RestaurantID=(select Code From Setup_Restaurant where Name='{1}'))  and ItemID='{3}' and ExpireDate='{4}'", tuple.Item1, Resturant_Kitchentxt.Text, FromKitchen_Kitchentxt.Text, dt.Rows[i]["ItemID"], Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                    SqlCommand TheCMD = new SqlCommand(s, con);
                                    TheCMD.ExecuteNonQuery();

                                    s = string.Format("update ItemsExpireDate set Qty=Qty+{0} where RestaurantID=(select Code From Setup_Restaurant where Name='{1}') and KitchenID=(select Code From Setup_Kitchens where Name='{2}' and RestaurantID=(select Code From Setup_Restaurant where Name='{1}')) and ItemID='{3}' and ExpireDate='{4}'", tuple.Item1, Resturant_Kitchentxt.Text, ToKitchen_Kitchentxt.Text, dt.Rows[i]["ItemID"], Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                    TheCMD = new SqlCommand(s, con);
                                    int IfNot = TheCMD.ExecuteNonQuery();

                                    if (IfNot == 0)
                                    {
                                        s = string.Format("insert ItemsExpireDate values((select Code From Setup_Restaurant where Name='{0}'),(select Code From Setup_Kitchens where Name='{1}' and RestaurantID=(select Code From Setup_Restaurant where Name='{0}')),'{2}','{3}','{4}')", Resturant_Kitchentxt.Text, ToKitchen_Kitchentxt.Text, dt.Rows[i]["ItemID"], tuple.Item1, Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                        TheCMD = new SqlCommand(s, con);
                                        TheCMD.ExecuteNonQuery();
                                    }

                                }
                            }
                            catch { }

                            //ana hne bzbat Table al ItemsYear
                            s = string.Format("Update ItemsYear set {0} = {0} + {2},{1} = (({1}* {0})+({2} * {3}))/({0}+{2}) where Year='{5}' and ItemID = '{4}' and Restaurant_ID ='{6}' and Kitchen_ID ='{7}'", MainWindow.MonthQty, MainWindow.MonthCost, dt.Rows[i][ToKitchen_Kitchentxt.Text + " Qty"], dt.Rows[i][ToKitchen_Kitchentxt.Text + " Unit Cost"], dt.Rows[i]["ItemID"], MainWindow.CurrentYear, Classes.RetrieveRestaurantCode(Resturant_Kitchentxt.Text), Classes.RetrieveKitchenCode(ToKitchen_Kitchentxt.Text, Resturant_Kitchentxt.Text));
                            SqlCommand _cmd = new SqlCommand(s, con);
                            int n = _cmd.ExecuteNonQuery();
                            if (n == 0)
                            {
                                FiledSelection = string.Format("ItemID,Restaurant_ID,Kitchen_ID,Year,{0},{1}", MainWindow.MonthQty, MainWindow.MonthCost);
                                Values = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}'", dt.Rows[i]["ItemID"], Classes.RetrieveRestaurantCode(Resturant_Kitchentxt.Text), Classes.RetrieveKitchenCode(ToKitchen_Restauranttxt.Text, Resturant_Kitchentxt.Text), MainWindow.CurrentYear, dt.Rows[i][ToKitchen_Kitchentxt.Text + " Qty"], dt.Rows[i][ToKitchen_Kitchentxt.Text + " Unit Cost"]);
                                Classes.InsertRow("ItemsYear", FiledSelection, Values);
                            }

                            //s = string.Format("Update Items set Qty= {1},Net_Cost={4} where ItemID = '{0}' and RestaurantID =(select Code From Setup_Restaurant where Name='{2}') and KitchenID=(Select Code from Setup_Kitchens Where Name='{3}')", dt.Rows[i]["ItemID"], dt.Rows[i][FromKitchen_Kitchentxt.Text + " Qty"], Resturant_Kitchentxt.Text, FromKitchen_Kitchentxt.Text, dt.Rows[i][FromKitchen_Kitchentxt.Text + " total Cost"]);
                            //SqlCommand _cmd = new SqlCommand(s, con);
                            //_cmd.ExecuteNonQuery();

                            s = string.Format("Update Items set Qty= {1},Last_Cost = Current_Cost,Current_Cost = '{4}',Net_Cost='{5}' where ItemID = '{0}' and RestaurantID =(select Code From Setup_Restaurant Where Name='{2}') and KitchenID=(select Code From Setup_Kitchens Where Name='{3}')", dt.Rows[i]["ItemID"], dt.Rows[i][ToKitchen_Kitchentxt.Text + " Qty"], Resturant_Kitchentxt.Text, ToKitchen_Kitchentxt.Text, dt.Rows[i][ToKitchen_Kitchentxt.Text + " Unit Cost"], dt.Rows[i][ToKitchen_Kitchentxt.Text + " total Cost"]);
                            _cmd = new SqlCommand(s, con);
                            n = _cmd.ExecuteNonQuery();

                            if (n == 0)
                            {
                                s = string.Format("insert into Items(RestaurantID,KitchenID,ItemID,Qty,Units,Last_Cost,Current_Cost,Net_Cost) Values ((select Code From Setup_Restaurant WHere Name='{0}'),(	select Code From Setup_Kitchens WHere Name='{1}'),'{2}','{3}','{4}','{5}','{6}','{7}')", Resturant_Kitchentxt.Text, ToKitchen_Kitchentxt.Text, dt.Rows[i]["ItemID"], dt.Rows[i]["Qty"], "", dt.Rows[i][ToKitchen_Kitchentxt.Text + " Unit Cost"], dt.Rows[i][ToKitchen_Kitchentxt.Text + " Unit Cost"], dt.Rows[i][ToKitchen_Kitchentxt.Text + " total Cost"]);
                                _cmd = new SqlCommand(s, con);
                                _cmd.ExecuteNonQuery();
                            }

                        }
                    }

                    SqlCommand cmd = new SqlCommand(string.Format("Insert into RO(RO_Serial,RO_No,Transactions_No,Status,Create_Date,Receiving_Date,Resturant_ID,Kitchen_ID,WS,Type,Comment,UserID,Total_Cost)Values ('{0}','{1}','{2}','{3}',GETDATE(),'{4}',(select Code From Setup_Restaurant WHere Name='{5}'),(select Code From Setup_Kitchens WHere Name='{6}'),'{7}','{8}','{9}','{10}','{11}')", CodeKitchentxt.Text, ManualKitchentxt.Text, TransferKitchenID, "Recieved",Convert.ToDateTime(DeliveryKitchentxt.Text).ToString("MM-dd-yyyy") + " " + DateTime.Now.ToString("HH:mm:ss"), Resturant_Kitchentxt.Text, ToKitchen_Kitchentxt.Text, Classes.WS, "Transfer_Kitchen", CommentKitchentxt.Text, MainWindow.UserID, Total_Price_With_Tax_Kitchen.Text), con);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                finally
                {
                    MessageBox.Show("Order Recived saved Succesful");
                    con.Close();
                }

                ClearFields();
                MainUiFormat();
            }
        }
        private void SearchVoucharBn(object sender, RoutedEventArgs e)
        {
            if (AuthenticatedKitchen.IndexOf("SearchROKitchen") == -1 && AuthenticatedKitchen.IndexOf("CheckAllROKitchen") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                ExpireDate.ItemExpireDate.Clear();
                CodeKitchentxt.Text = Classes.InCrementTransactionSerial("RO", "RO_Serial");
                All_Purchase_Orders items = new All_Purchase_Orders(this);
                items.ShowDialog();
            }
        }       //Doone


        //Transfer Without Purshace 
        private void NewWithoutBn_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            DeliveryWithouttxt.SelectedDate = DateTime.Now;
            CodeWithouttxt.Text = Classes.InCrementTransactionSerial("RO", "RO_Serial");
            LoadTheResturant();
            EnableUI();
        }       //Done
        private void ResturantCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResturantCbx.SelectedItem != null)
            {
                KitchenReqcbx.Items.Clear();
                RestaurantId = Classes.RetrieveRestaurantCode(ResturantCbx.SelectedItem.ToString());

                Kitchens = Classes.RetrieveKitchens(ResturantCbx.SelectedItem.ToString());
                for (int i = 0; i < Kitchens.Rows.Count; i++)
                {
                    KitchenCbx.Items.Add(Kitchens.Rows[i][0].ToString());

                }
            }
            if (ResturantCbx.SelectedItem != null)
            {
                KitchenCbx.Items.Clear();
                RestaurantId = Classes.RetrieveRestaurantCode(ResturantCbx.SelectedItem.ToString());

                Kitchens = Classes.RetrieveKitchens(ResturantCbx.SelectedItem.ToString());
                for (int i = 0; i < Kitchens.Rows.Count; i++)
                {
                    KitchenCbx.Items.Add(Kitchens.Rows[i][0].ToString());
                }
               
            }
        }       //Done
        private void KitchenCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (KitchenCbx.SelectedItem != null)
            {
                if (KitchenCbx.SelectedItem.ToString() != "")
                {
                    ExpireDate.ItemExpireDate.Clear();
                    KitchenId = Classes.RetrieveKitchenCode(KitchenCbx.SelectedItem.ToString(), ResturantCbx.SelectedItem.ToString());
                }
            }
        }       //Done
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Items itemswindow = new Items(this);
            itemswindow.ShowDialog();
        }   //Done
        private void ItemsWithoutDGV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                IndexOfRecord = grid.SelectedIndex;
            }
        }       //Done
        private void DeleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = ItemsWithoutDGV.DataContext as DataTable;
            dt.Rows.RemoveAt(ItemsWithoutDGV.SelectedIndex);
            ItemsWithoutDGV.DataContext = dt;
        }   //Done
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainUiFormat();
            ClearFields();
        }       //Done
        private void ItemDgv_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataTable dt = ItemsWithoutDGV.DataContext as DataTable;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = false;
            }
            try
            {
                SqlConnection con = new SqlConnection(Classes.DataConnString);

                if (e.Column.Header.ToString() == "Price")
                {
                    CalculatePrices(e, dt);
                }
                else if (e.Column.Header.ToString() == "Tax Included")
                {
                    if ((e.EditingElement as CheckBox).IsChecked == true)
                    {
                        con.Open();
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(string.Format("select TaxableValue from Setup_Items where Code = '{0}'", (e.Row.Item as DataRowView).Row["Code"]), con))
                            {
                                string tax = cmd.ExecuteScalar().ToString();
                                if (tax == "")
                                    tax = "0";
                                dt.Rows[e.Row.GetIndex()]["Tax"] = tax + "%";
                            }
                        }
                        catch { }
                        finally { con.Close(); }
                    }
                    else
                    {
                        (e.Row.Item as DataRowView).Row["Tax"] = "0%";
                    }
                    CalculatePrices(e, dt);
                }
                else if (e.Column.Header.ToString() == "Qty")
                {
                    CalculatePrices(e, dt);
                }
            }
            catch { }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = true;
            }
            dt.Columns["Price"].ReadOnly = false;
            dt.Columns["Qty"].ReadOnly = false;
            dt.Columns["Tax Included"].ReadOnly = false;
            ItemsWithoutDGV.DataContext = dt;
        }       //Done
        private void CalculatePrices(DataGridCellEditEndingEventArgs e, DataTable dt)
        {
            float Price, Qty, TaxPrec;
            try
            {
                if (e.Column.Header.ToString() == "Price")
                {
                    Price = float.Parse((e.EditingElement as TextBox).Text);
                    dt.Rows[e.Row.GetIndex()]["Price"] = Price;
                }
                else
                {
                    try
                    {
                        Price = float.Parse((dt.Rows[e.Row.GetIndex()]["Price"]).ToString());
                    }
                    catch { Price = 0; }
                }


                if (e.Column.Header.ToString() == "Qty")
                {
                    Qty = float.Parse((e.EditingElement as TextBox).Text);
                    dt.Rows[e.Row.GetIndex()]["Qty"] = Qty;
                }
                else
                {
                    try
                    {
                        Qty = float.Parse((dt.Rows[e.Row.GetIndex()]["Qty"]).ToString());

                    }
                    catch { Qty = 0; }
                }

                TaxPrec = (float.Parse(dt.Rows[e.Row.GetIndex()]["Tax"].ToString().Substring(0, (e.Row.Item as DataRowView).Row["Tax"].ToString().Length - 1)) / 100)+1;

                if ((e.Row.Item as DataRowView).Row["Tax Included"].ToString() == "True")
                {
                    dt.Rows[e.Row.GetIndex()]["Total Price With Tax"] = (Price * Qty).ToString();
                    dt.Rows[e.Row.GetIndex()]["Total Price Without Tax"] = ((Price / TaxPrec) * Qty).ToString();

                    if ((e.Row.Item as DataRowView).Row["Qty"].ToString() != "0")
                    {
                        dt.Rows[e.Row.GetIndex()]["Unit Price With Tax"] = Price.ToString();
                        dt.Rows[e.Row.GetIndex()]["Unit Price Without Tax"] = (Price / TaxPrec).ToString();
                    }
                }
                else
                {
                    dt.Rows[e.Row.GetIndex()]["Total Price With Tax"] = (Price * Qty).ToString();
                    dt.Rows[e.Row.GetIndex()]["Total Price Without Tax"] = (Price * Qty).ToString();

                    dt.Rows[e.Row.GetIndex()]["Unit Price With Tax"] = Price.ToString();
                    dt.Rows[e.Row.GetIndex()]["Unit Price Without Tax"] = Price.ToString();

                }

                float totalPriceWithoutTax = 0;
                float totalPriceWithTax = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    totalPriceWithoutTax += float.Parse(dt.Rows[i]["Total Price Without Tax"].ToString());
                    Total_Price_Without_Tax.Text = totalPriceWithoutTax.ToString();

                    totalPriceWithTax += float.Parse(dt.Rows[i]["Total Price With Tax"].ToString());
                    Total_Price_With_Tax.Text = totalPriceWithTax.ToString();
                }
            }
            catch { }
        }       //Done
        private void ItemsWithoutDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    if ((grid.SelectedItem as DataRowView).Row.ItemArray[5].ToString() == "True")
                    {
                        if ((grid.SelectedItem as DataRowView).Row.ItemArray[7].ToString() != "")
                        {
                            ExpireDate Expire_Date = new ExpireDate(((grid.SelectedItem as DataRowView).Row.ItemArray[0]).ToString(), ((grid.SelectedItem as DataRowView).Row.ItemArray[7]).ToString());
                            Expire_Date.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Please Enter The Qty First");
                        }
                    }

                }
            }
        }
        private void RecievedWithoutBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (AuthenticatedWithoutPO.IndexOf("RecieveROWithout") == -1 && AuthenticatedWithoutPO.IndexOf("CheckAllROWithout") == -1)
            {    LogIn logIn = new LogIn();  logIn.ShowDialog();    }
            else
            {
                if (DoSomeChecks() == false)
                    return;

                string QTyonHand = ""; string CostOfItemsOnHand = ""; string QtyOnHandMultipleCost = "";
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                try
                {
                    con.Open();
                    string s = string.Format("select * from RO where RO_Serial = {0}", CodeWithouttxt.Text);
                    SqlCommand cmd = new SqlCommand(s, con);
                    int record_no = cmd.ExecuteNonQuery();
                    if (record_no > 0)
                    {
                        MessageBox.Show("Another Work station save at the saame time");
                        CodeWithouttxt.Text = (int.Parse(CodeWithouttxt.Text) + 1).ToString();
                    }

                    DataTable dt = ItemsWithoutDGV.DataContext as DataTable;
                    cmd = new SqlCommand();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        QTyonHand = ""; CostOfItemsOnHand = ""; QtyOnHandMultipleCost = "";
                        DataTable TheValue = Classes.RetriveCostAndQty(KitchenCbx.SelectedItem.ToString(), ResturantCbx.SelectedItem.ToString(), dt.Rows[i]["Code"].ToString());
                        try
                        {
                            QTyonHand = (Convert.ToDouble(TheValue.Rows[0][0].ToString())).ToString();
                            CostOfItemsOnHand = TheValue.Rows[0][1].ToString();
                        }
                        catch  {   QTyonHand = "0";    CostOfItemsOnHand = "0";   }
                     
                        try
                        {
                            QtyOnHandMultipleCost = (Convert.ToDouble(QTyonHand) * Convert.ToDouble(CostOfItemsOnHand)).ToString();
                            QTyonHand = (Convert.ToDouble(QTyonHand) + Convert.ToDouble(dt.Rows[i]["Qty"].ToString())).ToString();
                            CostOfItemsOnHand = ((Convert.ToDouble(QtyOnHandMultipleCost) + (Convert.ToDouble(dt.Rows[i]["Qty"].ToString()) * Convert.ToDouble(dt.Rows[i]["Unit Price With Tax"]))) / Convert.ToDouble(QTyonHand)).ToString();
                        }
                        catch
                        {
                            QTyonHand =dt.Rows[i]["Qty"].ToString();
                            CostOfItemsOnHand = dt.Rows[i]["Unit Price With Tax"].ToString();
                        }

                        FiledSelection = "Item_ID,RO_No,Qty,Unit,Serial,Price_Without_Tax,Tax,Price_With_Tax,Net_Price,QtyOnHand_To,Cost_To";
                        Values = "'" + dt.Rows[i]["Code"].ToString() + "','" + CodeWithouttxt.Text + "'," + dt.Rows[i]["Qty"] + ",N'" + dt.Rows[i]["Unit"] + "','"+ i +"'," + dt.Rows[i]["Unit Price Without Tax"] + "," + dt.Rows[i]["Tax"].ToString().Substring(0, dt.Rows[i]["Tax"].ToString().Length - 1) + "," + dt.Rows[i]["Unit Price With Tax"] + "," + dt.Rows[i]["Total Price With Tax"] + ",'" + QTyonHand + "','" + CostOfItemsOnHand + "'";
                        Classes.InsertRow("RO_Items", FiledSelection, Values);

                        try
                        {
                            foreach (Tuple<string, string> tuple in ExpireDate.ItemExpireDate[dt.Rows[i]["Code"].ToString()])
                            {
                                string q = string.Format("update ItemsExpireDate set Qty=Qty+'{0}' where RestaurantID='{1}' and KitchenID='{2}' and ItemID='{3}' and ExpireDate='{4}'", dt.Rows[i]["Qty"], RestaurantId, KitchenId, dt.Rows[i]["Code"].ToString(), Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                SqlCommand TheCmd = new SqlCommand(q, con);
                                int W = TheCmd.ExecuteNonQuery();

                                if (W == 0)
                                {
                                    FiledSelection = "RestaurantID,KitchenID,ItemID,QTy,ExpireDate";
                                    Values = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}'", RestaurantId, KitchenId, dt.Rows[i]["Code"], tuple.Item1, Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                    Classes.InsertRow("ItemsExpireDate", FiledSelection, Values);
                                }
                            }
                        }
                        catch { }

                        s = string.Format("Update Items set Qty = Qty + {1},Last_Cost = Current_Cost,Current_Cost = ((Current_Cost * Qty)+({1} * {3}))/(Qty+{1}),Units = '{4}',Net_Cost=(((Current_Cost * Qty)+({1} * {3}))/(Qty+{1})*({1}+Qty)) where ItemID = '{0}' and RestaurantID ='{2}' and KitchenID ='{5}'", dt.Rows[i]["Code"], dt.Rows[i]["Qty"], RestaurantId, dt.Rows[i]["Unit Price With Tax"], " ", KitchenId);
                        SqlCommand _cmd = new SqlCommand(s, con);
                        int n = _cmd.ExecuteNonQuery();
                        if (n == 0)
                        {
                            FiledSelection = "RestaurantID,KitchenID,ItemID,Qty,Units,Last_Cost,Current_Cost,Net_Cost";
                            Values = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}'", RestaurantId, KitchenId, dt.Rows[i]["Code"], dt.Rows[i]["Qty"], " ", dt.Rows[i]["Unit Price With Tax"], dt.Rows[i]["Unit Price With Tax"], dt.Rows[i]["Total Price With Tax"]);
                            Classes.InsertRow("Items", FiledSelection, Values);
                        }

                        // ana Hena basma3 fe Table al ItemsYear
                        s = string.Format("Update ItemsYear set {0} = {0} + {2},{1} = (({1}* {0})+({2} * {3}))/({0}+{2}) where Year='{5}' and ItemID = '{4}' and Restaurant_ID ='{6}' and Kitchen_ID ='{7}'", MainWindow.MonthQty, MainWindow.MonthCost, dt.Rows[i]["QTy"], dt.Rows[i]["Unit Price With Tax"], dt.Rows[i]["Code"], MainWindow.CurrentYear,RestaurantId,KitchenId);
                        _cmd = new SqlCommand(s, con);
                        n = _cmd.ExecuteNonQuery();
                        if (n == 0)
                        {
                            FiledSelection = string.Format("ItemID,Restaurant_ID,Kitchen_ID,Year,{0},{1}", MainWindow.MonthQty, MainWindow.MonthCost);
                            Values = string.Format("'{0}','{4}','{5}','{1}',{2},{3}", dt.Rows[i]["Code"], MainWindow.CurrentYear, dt.Rows[i]["QTy"], dt.Rows[i]["Unit Price With Tax"],RestaurantId,KitchenId);
                            Classes.InsertRow("ItemsYear", FiledSelection, Values);
                        }

                    }

                    FiledSelection = "RO_Serial,RO_No,Transactions_No,Status,Receiving_Date,Resturant_ID,Kitchen_ID,WS,Type,Comment,UserID,Create_Date,Total_Cost";
                    Values = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',N'{9}','{10}',GETDATE(),'{11}'", CodeWithouttxt.Text, ManualWithouttxt.Text, "0", "Recieved", Convert.ToDateTime(DeliveryWithouttxt.Text).ToString("MM-dd-yyyy") + " " + DateTime.Now.ToString("HH:mm:ss"), RestaurantId, KitchenId, Classes.WS, "Auto_Recieve", CommentWithouttxt.Text, MainWindow.UserID, Total_Price_With_Tax.Text);
                    Classes.InsertRow("RO", FiledSelection, Values);

                    MainUiFormat();
                    MessageBox.Show("Order Recived Succesfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                finally
                {
                    con.Close();
                }
                ClearFields();
            }
        }       //Done


        //requested Tab 
        private void LoadAllRequests()
        {
            DataTable AllRequests = new DataTable();
            string Filed = "Transfer_Serial as 'Transfer Serial',Manual_Transfer_No as 'Manul Transfer Number',Transfer_Date as 'Transfer Date',(select Name From Setup_Restaurant where Code=To_Resturant_ID) as 'TO Resturant',(select Name From Setup_Kitchens where Code=To_Kitchen_ID  and RestaurantID=To_Resturant_ID) as 'TO Kitchen' ";
            string Where = string.Format("From_Resturant_ID={0} and From_Kitchen_ID={1} and Status='Post' and Transfer_Serial not in (select Request_serial from Requests_tbl where Status='Post')", RestaurantId, KitchenId);
            AllRequests = Classes.RetrieveData(Filed, Where, "Transfer_Kitchens");
            RequestssDGV.DataContext = AllRequests;
        }       //Done FFinall Function
        private void ResturantReqcbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResturantReqcbx.SelectedItem != null)
            {
                KitchenReqcbx.Items.Clear();
                RestaurantId = Classes.RetrieveRestaurantCode(ResturantReqcbx.SelectedItem.ToString());

                Kitchens = Classes.RetrieveKitchens(ResturantReqcbx.SelectedItem.ToString());
                for(int i=0;i<Kitchens.Rows.Count;i++)
                {
                    KitchenReqcbx.Items.Add(Kitchens.Rows[i][0].ToString());

                }
            }
        }       //Done Finall Function  
        private void KitchenReqcbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExpireDate.ItemExpireDate.Clear();
            KitchenId = Classes.RetrieveKitchenCode(KitchenReqcbx.SelectedItem.ToString(), ResturantReqcbx.SelectedItem.ToString());
            RequestChose.Visibility = Visibility.Hidden;
            RequestInfo.Visibility = Visibility.Visible;
            LoadAllRequests();
            //SearchReq.Visibility = Visibility.Visible;
        }       //Done Finall Function
        private void RequestssDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            float from_rest_Qty = 0; float from_rest_Cost = 0; float to_rest_Qty = 0; float to_rest_Cost = 0;
            DataTable dt = new DataTable();
            recieveTransfer.IsEnabled = true;
            ManualResturanttxt.IsEnabled = true;
            DeliveryRestauranttxt.IsEnabled = true;
            CommentRestaurant.IsEnabled = true;

            DataTable Dat = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select Transfer_Serial,Manual_Transfer_No,Transfer_Date,Comment,(select Name From Setup_Restaurant where Code=From_Resturant_ID) as From_Resturat,(select Name From Setup_Restaurant where Code = To_Resturant_ID) as To_Resturat, (select Name from Setup_Kitchens where Code = From_Kitchen_ID and RestaurantID = From_Resturant_ID) as From_Kitchen,(select Name from Setup_Kitchens where Code = To_Kitchen_ID  and RestaurantID = To_Resturant_ID) as To_Kitchen,Type from Transfer_Kitchens Where Transfer_Serial='{0}'", ((DataRowView)RequestssDGV.SelectedItem).Row.ItemArray[0]), con);
                dt = new DataTable();
                adapter.Fill(dt);

                DataRow row = dt.Rows[0];
                CodeRequesttxt.Text = row["Transfer_Serial"].ToString();
                ManualRequesttxt.Text = row["Manual_Transfer_No"].ToString();
                TypeCbx.Text = row["Type"].ToString();
                Request_Date.Text =Convert.ToDateTime(row["Transfer_Date"]).ToString("dd-MM-yyyy");
                RequestCommenttxt.Text = row["Comment"].ToString();
                TOResturantReq.Text = row["To_Resturat"].ToString();
                TOKitchenReq.Text = row["To_Kitchen"].ToString();
            }
            catch (Exception ex)
            {     MessageBox.Show(ex.ToString());   }
            finally
            {  con.Close();  }
            toKItchenAndResturant.Visibility = Visibility.Visible;
            NUmberOfItemsReq.Visibility = Visibility.Visible;
            NumberOfItemTextReq.Visibility = Visibility.Visible;
            Total_PriceReq.Visibility = Visibility.Visible;
            TotalofItemsReq.Visibility = Visibility.Visible;
            Reply.Visibility = Visibility.Visible;
            Reply.IsEnabled = true;


            Dat.Columns.Add("Select", typeof(bool));
            Dat.Columns.Add("Code");
            Dat.Columns.Add("Manual Code");
            Dat.Columns.Add("Name");
            Dat.Columns.Add("Name2");
            Dat.Columns.Add("Unit");
            Dat.Columns.Add("Expire Date",typeof(bool));
            Dat.Columns.Add("Qty");
            Dat.Columns.Add(KitchenReqcbx.Text + " Qty");
            Dat.Columns.Add(KitchenReqcbx.Text + " Unit Cost");
            Dat.Columns.Add(KitchenReqcbx.Text + " total Cost");
            Dat.Columns.Add(TOKitchenReq.Text + " Qty");
            Dat.Columns.Add(TOKitchenReq.Text + " Unit Cost");
            Dat.Columns.Add(TOKitchenReq.Text + " total Cost");
            Dat.Columns.Add("Recipe", typeof(bool));

            try
            {
                con.Open();
                string M = "SELECT Item_ID,Qty,Cost,Unit,Recipes FROM Transfer_Kitchens_Items where Transfer_ID=" + CodeRequesttxt.Text;
                SqlCommand cmd = new SqlCommand(M, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((bool)reader["Recipes"] == true)
                    {
                        DataTable TheValue = Classes.RetriveCostAndQtyRecipes(ResturantReqcbx.Text, KitchenReqcbx.Text, reader["Item_ID"].ToString());
                        try { from_rest_Qty = (float.Parse(TheValue.Rows[0][0].ToString()) - float.Parse(reader["Qty"].ToString())); }
                        catch { from_rest_Qty = 0; }
                        try { from_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString()); }
                        catch { from_rest_Cost = 0; }

                        TheValue = Classes.RetriveCostAndQtyRecipes(TOResturantReq.Text, TOKitchenReq.Text, reader["Item_ID"].ToString());
                        try { to_rest_Qty = (float.Parse(TheValue.Rows[0][0].ToString()) + float.Parse(reader["Qty"].ToString())); }
                        catch { to_rest_Qty = float.Parse(reader["Qty"].ToString()); }
                        try { to_rest_Cost = (((float.Parse(reader["Qty"].ToString()) * float.Parse(reader["Cost"].ToString())) + (float.Parse(TheValue.Rows[0][0].ToString()) * float.Parse(TheValue.Rows[0][1].ToString()))) / to_rest_Qty); }
                        catch { to_rest_Cost = from_rest_Cost; }
                        //
                        double NetCostFrom = from_rest_Qty * from_rest_Cost;
                        double NetCostTo = to_rest_Qty * to_rest_Cost;
                        try
                        {
                            con2.Open();
                            string S = "SELECT CrossCode as [Manual Code],Name,Name2 FROM Setup_Recipes where Code='" + reader["Item_ID"].ToString() + "'";
                            SqlCommand cmd2 = new SqlCommand(S, con2);
                            SqlDataReader reader2 = cmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                Dat.Rows.Add(false, reader["Item_ID"], reader2["Manual Code"], reader2["Name"], reader2["Name2"], reader["Unit"],false, reader["Qty"], from_rest_Qty, from_rest_Cost, NetCostFrom, to_rest_Qty, to_rest_Cost, NetCostTo,reader["Recipes"]);
                            }

                            reader2.Close();
                        }
                        catch (Exception ex)
                        { MessageBox.Show(ex.ToString()); }
                        finally
                        { con2.Close(); }
                    }
                    else
                    {
                        DataTable TheValue = Classes.RetriveCostAndQty(ResturantReqcbx.Text, KitchenReqcbx.Text, reader["Item_ID"].ToString());
                        try { from_rest_Qty = (float.Parse(TheValue.Rows[0][0].ToString()) - float.Parse(reader["Qty"].ToString())); }
                        catch { from_rest_Qty = 0; }
                        try { from_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString()); }
                        catch { from_rest_Cost = 0; }

                        TheValue = Classes.RetriveCostAndQty(TOResturantReq.Text, TOKitchenReq.Text, reader["Item_ID"].ToString());
                        try { to_rest_Qty = (float.Parse(TheValue.Rows[0][0].ToString()) + float.Parse(reader["Qty"].ToString())); }
                        catch { to_rest_Qty = 0; }
                        try { to_rest_Cost = (((float.Parse(reader["Qty"].ToString()) * float.Parse(reader["Cost"].ToString())) + (float.Parse(TheValue.Rows[0][0].ToString()) * float.Parse(TheValue.Rows[0][1].ToString()))) / to_rest_Qty); }
                        catch { to_rest_Cost = 0; }
                        //
                        double NetCostFrom = from_rest_Qty * from_rest_Cost;
                        double NetCostTo = to_rest_Qty * to_rest_Cost;
                        try
                        {
                            con2.Open();
                            string S = "SELECT [Manual Code],Name,Name2,ExpDate FROM Setup_Items where Code='" + reader["Item_ID"].ToString() + "'";
                            SqlCommand cmd2 = new SqlCommand(S, con2);
                            SqlDataReader reader2 = cmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                Dat.Rows.Add(false, reader["Item_ID"], reader2["Manual Code"], reader2["Name"], reader2["Name2"], reader["Unit"], reader2["ExpDate"], reader["Qty"], from_rest_Qty, from_rest_Cost, NetCostFrom, to_rest_Qty, to_rest_Cost, NetCostTo,reader["Recipes"]);
                            }

                            reader2.Close();
                        }
                        catch (Exception ex)
                        { MessageBox.Show(ex.ToString()); }
                        finally
                        { con2.Close(); }
                    }
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            finally
            {   con.Close();  }
            for(int i=0;i<Dat.Columns.Count;i++)
            {
                Dat.Columns[i].ReadOnly = true;
            }
            Dat.Columns["Select"].ReadOnly = false;
            Dat.Columns["Qty"].ReadOnly = false;
            RequestsItemsDGV.DataContext = Dat;
            float Total_Price = 0;
            for (int i = 0; i < RequestsItemsDGV.Items.Count; i++)
            {
                Total_Price += (float.Parse(Dat.Rows[i]["Qty"].ToString()) * float.Parse(Dat.Rows[i][KitchenReqcbx.Text + " Unit Cost"].ToString()));
            }
            NUmberOfItemsReq.Text = RequestsItemsDGV.Items.Count.ToString();
            Total_PriceReq.Text = Total_Price.ToString();
            RequestssDGV.Visibility = Visibility.Hidden;
            RequestsItemsDGV.Visibility = Visibility.Visible;
        }       //Done Finall Function
        private void RequestsItemsDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataTable DT = RequestsItemsDGV.DataContext as DataTable;
            if (e.Column.Header == "Qty")
            {
                float from_rest_Qty = 0; float from_rest_Cost = 0; float to_rest_Qty = 0; float to_rest_Cost = 0;
                if (e.Column.Header.ToString() != "Select")
                {
                    string ItemCode = (e.Row.Item as DataRowView).Row["Code"].ToString();
                    if ((bool)(e.Row.Item as DataRowView).Row["Recipe"] == true)
                    {
                        try
                        {
                            DataTable TheValue = Classes.RetriveCostAndQtyRecipes(ResturantReqcbx.Text, KitchenReqcbx.Text, ItemCode);
                            try { from_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString()); }
                            catch { from_rest_Qty = 0; }
                            try
                            { from_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString()); }
                            catch { from_rest_Cost = 0; }

                            TheValue = Classes.RetriveCostAndQtyRecipes(TOResturantReq.Text, TOKitchenReq.Text, ItemCode);
                            try { to_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString()); }
                            catch { to_rest_Qty = 0; }
                            try
                            { to_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString()); }
                            catch { to_rest_Cost = 0; }

                            for (int i = 0; i < DT.Columns.Count; i++)
                            {
                                DT.Columns[i].ReadOnly = false;
                            }

                            DT.Rows[e.Row.GetIndex()]["Qty"] = float.Parse((e.EditingElement as TextBox).Text).ToString();
                            DT.Rows[e.Row.GetIndex()][KitchenReqcbx.Text + " Qty"] = (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text)).ToString();
                            DT.Rows[e.Row.GetIndex()][KitchenReqcbx.Text + " Unit Cost"] = from_rest_Cost.ToString();
                            DT.Rows[e.Row.GetIndex()][KitchenReqcbx.Text + " Total Cost"] = (from_rest_Cost * (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text))).ToString();

                            DT.Rows[e.Row.GetIndex()][TOKitchenReq.Text + " Qty"] = (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text)).ToString();
                            DT.Rows[e.Row.GetIndex()][TOKitchenReq.Text + " Unit Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text)))).ToString();
                            DT.Rows[e.Row.GetIndex()][TOKitchenReq.Text + " Total Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text))) * (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text))).ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            (e.EditingElement as TextBox).Text = "";
                        }
                    }
                    else
                    {
                        try
                        {
                            DataTable TheValue = Classes.RetriveCostAndQty(ResturantReqcbx.Text, KitchenReqcbx.Text, ItemCode);
                            try { from_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString()); }
                            catch { from_rest_Qty = 0; }
                            try
                            { from_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString()); }
                            catch { from_rest_Cost = 0; }

                            TheValue = Classes.RetriveCostAndQty(TOResturantReq.Text, TOKitchenReq.Text, ItemCode);
                            try { to_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString()); }
                            catch { to_rest_Qty = 0; }
                            try
                            { to_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString()); }
                            catch { to_rest_Cost = 0; }

                            for (int i = 0; i < DT.Columns.Count; i++)
                            {
                                DT.Columns[i].ReadOnly = false;
                            }

                            DT.Rows[e.Row.GetIndex()]["Qty"] = float.Parse((e.EditingElement as TextBox).Text).ToString();
                            DT.Rows[e.Row.GetIndex()][KitchenReqcbx.Text + " Qty"] = (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text)).ToString();
                            DT.Rows[e.Row.GetIndex()][KitchenReqcbx.Text + " Unit Cost"] = from_rest_Cost.ToString();
                            DT.Rows[e.Row.GetIndex()][KitchenReqcbx.Text + " Total Cost"] = (from_rest_Cost * (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text))).ToString();

                            DT.Rows[e.Row.GetIndex()][TOKitchenReq.Text + " Qty"] = (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text)).ToString();
                            DT.Rows[e.Row.GetIndex()][TOKitchenReq.Text + " Unit Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text)))).ToString();
                            DT.Rows[e.Row.GetIndex()][TOKitchenReq.Text + " Total Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text))) * (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text))).ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            (e.EditingElement as TextBox).Text = "";
                        }
                    }

                    try
                    {
                        double totalPrice = 0;
                        for (int i = 0; i < RequestsItemsDGV.Items.Count; i++)
                        {
                            try
                            {
                                totalPrice += (float.Parse(DT.Rows[i]["Qty"].ToString()) * float.Parse(DT.Rows[i][KitchenReqcbx.Text + " Unit Cost"].ToString()));

                                //totalPrice += Convert.ToDouble(DT.Rows[e.Row.GetIndex()][TOKitchenReq.Text + " Total Cost"]);
                            }
                            catch { }
                        }
                        NUmberOfItemsReq.Text = (RequestsItemsDGV.Items.Count).ToString();
                        Total_PriceReq.Text = (totalPrice).ToString();
                    }
                    catch { }
                }
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    DT.Columns[i].ReadOnly = true;
                }
                DT.Columns["Select"].ReadOnly = false;
                DT.Columns["Qty"].ReadOnly = false;
                RequestsItemsDGV.DataContext = DT;
            }
        }       //Done Finall Function
        private void Reply_Click(object sender, RoutedEventArgs e)
        {
            if (AuthenticatedRequest.IndexOf("Requests") == -1 && AuthenticatedRequest.IndexOf("CheckAllRequests") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            if (!DoSomeChecks())
                return;

            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            try
            {
                con2.Open();
                string s = string.Format("select Request_Serial From Requests_tbl Where Request_Serial='{0}'", CodeRequesttxt.Text);
                SqlCommand cmd = new SqlCommand(s, con2);
                if (cmd.ExecuteScalar() == null)
                {
                    try
                    {
                        con.Open();

                        Save_Req_Items(con);
                        Save_Req(con);
                        MessageBox.Show("Transfer saved Sucssfully");
                    }
                    catch { }
                    finally  {  con.Close(); }
                }
                else
                {

                    con.Open();
                    string W = string.Format("delete Requests_Items where Request_ID={0}", CodeRequesttxt.Text);
                    SqlCommand cmd2 = new SqlCommand(W, con);
                    cmd2.ExecuteNonQuery();
                    W = string.Format("delete Requests_ItemsExpireDate where Request_ID={0}", CodeRequesttxt.Text);
                    cmd2 = new SqlCommand(W, con);

                    cmd2.ExecuteNonQuery();
                    Save_Req_Items(con);
                    Edit_Req(con);
                    MessageBox.Show("Transfer Edited Sussesfully");
                }
            }
            catch { }
            LoadAllRequests();
            ClearFields();
            RequestsItemsDGV.Visibility = Visibility.Hidden;
            RequestssDGV.Visibility = Visibility.Visible;
            Reply.Visibility = Visibility.Hidden;
            ExpireDate.ItemExpireDate.Clear();
        }       //Done
        private void Edit_Req(SqlConnection con)
        {
            try
            {
                string FiledSelection = "Request_Date,Comment,Status";
                string Values = string.Format("'{0}','{1}','{2}'",Convert.ToDateTime(Request_Date.Text).ToString("MM-dd-yyyy") + " " + DateTime.Now.ToString("HH:mm:ss"), commenttxt.Text,StatusReq.Text);
                string where = string.Format("Request_Serial={0}", CodeRequesttxt.Text);
                Classes.UpdateRow(FiledSelection, Values, where, "Requests_tbl");
                //string s = string.Format("update PO Set PO_No='{0}',Ship_To=(select Code from Setup_Restaurant where Name = '{1}'),Vendor_ID=(select VendorID from Vendors where Name = '{2}'),Delivery_Date='{3}',Last_Modified_Date=GETDATE(),Comment='{4}',Total_Price='{5}',Status='{6}' Where PO_Serial={7}", PO_NO.Text, ShipTo.Text, Vendor.Text, Delivery_dt.Text+" "+Delivery_time.Text, commenttxt.Text, Total_Price_With_Tax.Text,Statustxt.Text ,Serial_PO_NO.Text);
                //SqlCommand cmd = new SqlCommand(s, con);
                //cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { con.Close(); }
        }       //Done
        private void Save_Req_Items(SqlConnection con)
        {
            try
            {
                DataTable dt = RequestsItemsDGV.DataContext as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Select"].ToString() == "False")
                    {
                        continue;
                    }
                    if ((bool)dt.Rows[i]["Recipe"] == true)
                    {
                        float NetCost = float.Parse(dt.Rows[i]["Qty"].ToString()) * float.Parse(dt.Rows[i][9].ToString());

                        string s = string.Format("insert into Requests_Items values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", dt.Rows[i]["Code"], CodeRequesttxt.Text, dt.Rows[i]["Qty"], dt.Rows[i]["Unit"], i, dt.Rows[i][9], NetCost.ToString(), dt.Rows[i][TOKitchenReq.Text + " Qty"], dt.Rows[i][TOKitchenReq.Text + " Unit Cost"], dt.Rows[i][KitchenReqcbx.Text + " Qty"], dt.Rows[i][KitchenReqcbx.Text + " Unit Cost"], dt.Rows[i]["Recipe"]);
                        SqlCommand cmd = new SqlCommand(s, con);
                        cmd.ExecuteNonQuery();
                        if (StatusReq.Text == "Post")
                        {
                            s = string.Format("update RecipeQty set Qty=Qty-'{0}' where Resturant_ID='{1}' and Kitchen_ID='{2}' and Recipe_ID='{3}'", dt.Rows[i]["Qty"], RestaurantId, KitchenId, dt.Rows[i]["Code"]);
                            cmd = new SqlCommand(s, con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        float NetCost = float.Parse(dt.Rows[i]["Qty"].ToString()) * float.Parse(dt.Rows[i][9].ToString());

                        try
                        {
                            foreach (Tuple<string, string> tuple in ExpireDate.ItemExpireDate[dt.Rows[i]["Code"].ToString()])
                            {
                                FiledSelection = "Item_ID,Request_ID,Qty,ExpireDate";
                                Values = string.Format("'{0}', '{1}', '{2}', '{3}'", dt.Rows[i]["Code"], CodeRequesttxt.Text, tuple.Item1, Convert.ToDateTime(tuple.Item2).ToString("MM-dd-yyyy"));
                                Classes.InsertRow("Requests_ItemsExpireDate", FiledSelection, Values);
                            }
                        }
                        catch { }

                        string s = string.Format("insert into Requests_Items values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", dt.Rows[i]["Code"], CodeRequesttxt.Text, dt.Rows[i]["Qty"], dt.Rows[i]["Unit"], i, dt.Rows[i][9], NetCost.ToString(), dt.Rows[i][TOKitchenReq.Text + " Qty"], dt.Rows[i][TOKitchenReq.Text + " Unit Cost"], dt.Rows[i][KitchenReqcbx.Text + " Qty"], dt.Rows[i][KitchenReqcbx.Text + " Unit Cost"], dt.Rows[i]["Recipe"]);
                        SqlCommand cmd = new SqlCommand(s, con);
                        cmd.ExecuteNonQuery();
                        if (StatusReq.Text == "Post")
                        {
                            s = string.Format("update items set Qty=Qty-'{0}',Net_Cost=((Qty-'{0}')*Current_Cost) where RestaurantID='{1}' and KitchenID='{2}' and ItemID='{3}'", dt.Rows[i]["Qty"], RestaurantId, KitchenId, dt.Rows[i]["Code"]);
                            cmd = new SqlCommand(s, con);
                            cmd.ExecuteNonQuery();

                            s = string.Format("update ItemsYear set {0}={0}-{1} where Restaurant_ID='{2}' and Kitchen_ID='{3}' and ItemID='{4}' and Year='{5}'", MainWindow.MonthQty, dt.Rows[i]["Qty"], RestaurantId, KitchenId, dt.Rows[i]["Code"], MainWindow.CurrentYear);
                            cmd = new SqlCommand(s, con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Items Input Error");
            }
        }       //Done
        private void Save_Req(SqlConnection con)
        {
            try
            {
                string s = string.Format("insert into Requests_tbl(Request_Serial,Manual_Request_No,Request_Date,Comment,From_Resturant_ID,To_Resturant_ID,From_Kitchen_ID,To_Kitchen_ID,Post_Date,Type,UserID,WS,Status,Total_Cost) values('{0}','{1}','{2}','{3}',(select Code from Setup_Restaurant where Name = '{4}'),(select Code from Setup_Restaurant where Name = '{5}'),(select Code from Setup_Kitchens where Name = '{6}'),(select Code from Setup_Kitchens where Name = '{7}'),GETDATE(),'{8}','{9}','{10}','{11}','{12}')", CodeRequesttxt.Text, ManualRequesttxt.Text,Convert.ToDateTime(Request_Date.Text).ToString("MM-dd-yyyy") + " " + DateTime.Now.ToString("HH:mm:ss"), commenttxt.Text, ResturantReqcbx.Text, TOResturantReq.Text, KitchenReqcbx.Text, TOKitchenReq.Text, TypeCbx.Text, MainWindow.UserID, Classes.WS, StatusReq.Text, Total_PriceReq.Text);
                SqlCommand cmd = new SqlCommand(s, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }       //Done
        private void UndoRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            RequestsItemsDGV.DataContext = null;
            RequestsItemsDGV.Visibility = Visibility.Hidden;
            RequestssDGV.Visibility = Visibility.Visible;
            CodeRequesttxt.Text = "";
            ManualRequesttxt.Text = "";
            TypeCbx.Text = "";
            Request_Date.Text = "";
            RequestCommenttxt.Text = "";
            TOResturantReq.Text = "";
            TOKitchenReq.Text = "";

        }       //Done


        //events
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!DoSomeChecks())
                return;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString);

            try
            {
                con.Open();
                DataTable dt = ItemsDGV.DataContext as DataTable;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Received"].ToString() == "False")
                        break;

                    string s = "Update RO_Items SET Qty='" + dt.Rows[i]["Qty"] +
                                               "',Unit='" + dt.Rows[i]["Units"].ToString() +
                                               "',Serial='" + dt.Rows[i]["Serial"].ToString() +
                                               "',Price='" + dt.Rows[i]["Prices"] +
                                               "',Tax_Price='" + dt.Rows[i]["Tax Prec%"] +
                                               "',Net_Price='" + dt.Rows[i]["Final Price"] +
                                               "'Where RO_Serial =" + CodePurchaseROtxt.Text + "AND Item_ID='" + dt.Rows[i]["Code"].ToString() + "'";
                    SqlCommand _CMD = new SqlCommand(s, con);
                    int w = _CMD.ExecuteNonQuery();

                    if (w == 0)
                    {

                        s = "insert into RO_Items (Item_ID,RO_Serial,Qty,Unit,Serial,Price,Tax_Price,Net_Price) values ('" + dt.Rows[i]["Code"].ToString() + "','" + CodePurchaseROtxt.Text + "'," + dt.Rows[i]["Qty"] + ",'" + dt.Rows[i]["Units"].ToString() + "','" + dt.Rows[i]["Serial"].ToString() + "'," + dt.Rows[i]["Prices"] + "," + dt.Rows[i]["Tax Prec%"] + "," + dt.Rows[i]["Final Price"] + ")";
                        _CMD = new SqlCommand(s, con);
                        _CMD.ExecuteNonQuery();
                    }

                    //s = string.Format("Update Stores_Items set Qty= Qty + {1} where ItemID = '{0}'", dt.Rows[i]["Code"], dt.Rows[i]["Qty"]);
                    //SqlCommand _cmd = new SqlCommand(s, con);
                    //int n = _cmd.ExecuteNonQuery();

                    //if (n == 0)
                    //{
                    //    s = string.Format("insert into Stores_Items(StoreID,ItemID,Qty) values((select Top(1) Code from Setup_Restaurant) ,'{0}','{1}')", dt.Rows[i]["Code"], dt.Rows[i]["Qty"]);
                    //    _cmd = new SqlCommand(s, con);
                    //    _cmd.ExecuteNonQuery();
                    //}
                    ///eah da ysta ?
                    //if (i + 1 < dt.Rows.Count)
                    //    items_recieved += dt.Rows[i]["Code"].ToString() + ",";
                    //else
                    //    items_recieved += dt.Rows[i]["Code"].ToString();

                    //total_Price += float.Parse(dt.Rows[i]["Final Price"].ToString());
                }

                SqlCommand cmd = new SqlCommand(string.Format("Update RO SET RO_Number='" + ManualPurchaseROtxt.Text +
                                               "',PO_ID='" + PO.Text +
                                               "',Status='" + "Recieved" +
                                               "',Receiving_Date='" + Delivery_dt.Text +
                                               "',Comment='" + commenttxt.Text +
                                               "'Where RO_Serial =" + CodePurchaseROtxt.Text), con);
                cmd.ExecuteNonQuery();


            }
            catch { }

            finally
            {
                MessageBox.Show("changes saved");
                con.Close();
            }
        }
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            MainUiFormat();
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = "delete from PurchaseOrder_tbl where Code = " + CodePurchaseROtxt.Text;
                SqlCommand cmd = new SqlCommand(s, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
                MainUiFormat();
            }
            MessageBox.Show("Deleted Successfully");
        }
        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            EnableUI();
            Classes.InCrementTransactionSerial("RO", "RO_Serial");
            ManualPurchaseROtxt.IsEnabled = true;
        }
        private void Row_Changed(object sender, EventArgs e)
        {
            DataRowView drv = ((e as DataGridCellEditEndingEventArgs).Row as DataGridRow).Item as DataRowView;

            //if (((e as DataGridCellEditEndingEventArgs).EditingElement as CheckBox).IsChecked == true && bool.Parse(drv.Row["Has_ExpDate"].ToString()) == true)
            //{
            //    //ItemDependencies itemDependencies = new ItemDependencies(this, drv);
            //    //itemDependencies.ShowDialog();
            //}
        }
        private void LoadTheResturant()
        {
          
            ResturantCbx.Items.Clear();
            ResturantReqcbx.Items.Clear();
            for (int i = 0; i < Resturants.Rows.Count; i++)
            {
                ResturantCbx.Items.Add(Resturants.Rows[i][0].ToString());
                ResturantReqcbx.Items.Add(Resturants.Rows[i][0].ToString());
            }
        }
        private void UndoResturant_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            MainUiFormat();
        }
        private void KitchenUndo_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            MainUiFormat();
        }

        private void NewKitchenBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            MainUiFormat();
            EnableUI();
        }

        private void SearchReq_Click(object sender, RoutedEventArgs e)
        {

            All_Purchase_Orders items = new All_Purchase_Orders(this);
            items.ShowDialog();
        }

        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        private void ItemsDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    if ((grid.SelectedItem as DataRowView).Row.ItemArray[6].ToString() == "True")
                    {
                        ExpireDate Expire_Date = new ExpireDate(((grid.SelectedItem as DataRowView).Row.ItemArray[1]).ToString(), ((grid.SelectedItem as DataRowView).Row.ItemArray[8]).ToString());
                        Expire_Date.ShowDialog();
                    }
                }
            }

        }
        private void RequestsItemsDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    if ((grid.SelectedItem as DataRowView).Row.ItemArray[6].ToString() == "True")
                    {
                        if ((grid.SelectedItem as DataRowView).Row.ItemArray[7].ToString() != "")
                        {
                            ExpireDate Expire_Date = new ExpireDate(((grid.SelectedItem as DataRowView).Row.ItemArray[1]).ToString(), ((grid.SelectedItem as DataRowView).Row.ItemArray[7]).ToString(), RestaurantId, KitchenId);
                            Expire_Date.ShowDialog();
                        }
                    }
                }
            }
        }
        private void ItemRoInterDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    if ((grid.SelectedItem as DataRowView).Row.ItemArray[5].ToString() == "True")
                    {
                        if ((grid.SelectedItem as DataRowView).Row.ItemArray[6].ToString() != "")
                        {
                            ExpireDate Expire_Date = new ExpireDate(TransferKitchenID, (grid.SelectedItem as DataRowView).Row.ItemArray[6].ToString(), (grid.SelectedItem as DataRowView).Row.ItemArray[1].ToString());
                            Expire_Date.ShowDialog();
                        }
                    }
                }
            }
        }
        private void ItemKitchenTransferDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    if ((grid.SelectedItem as DataRowView).Row.ItemArray[5].ToString() == "True")
                    {
                        if ((grid.SelectedItem as DataRowView).Row.ItemArray[6].ToString() != "")
                        {
                            ExpireDate Expire_Date = new ExpireDate(TransferResturantID, (grid.SelectedItem as DataRowView).Row.ItemArray[6].ToString(), (grid.SelectedItem as DataRowView).Row.ItemArray[1].ToString());
                            Expire_Date.ShowDialog();
                        }
                    }
                }
            }
        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable RowWithoutInfo = new DataTable();
            RowWithoutInfo.Columns.Add("ID", typeof(string));
            RowWithoutInfo.Columns.Add("Name", typeof(string));
            RowWithoutInfo.Columns.Add("Qty", typeof(string));
            RowWithoutInfo.Columns.Add("Unit", typeof(string));
            RowWithoutInfo.Columns.Add("Price Without Tax", typeof(string));
            RowWithoutInfo.Columns.Add("Tax", typeof(string));
            RowWithoutInfo.Columns.Add("Price With Tax", typeof(string));
            RowWithoutInfo.Columns.Add("Net Price", typeof(string));
            for (int i=0;i<ItemsWithoutDGV.Items.Count;i++)
            {
                RowWithoutInfo.Rows.Add(((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[0], ((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[2], ((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[7], ((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[4], ((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[11], ((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[9], ((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[10], ((DataRowView)ItemsWithoutDGV.Items[i]).Row.ItemArray[12]);
            }
            ReportView Rec = new ReportView();
            Rec.Rpt = new CR_PrintRO();
            Rec.Rpt.SetDataSource(RowWithoutInfo);
            Rec.Rpt.SetParameterValue("Filter", "");
            Rec.Rpt.SetParameterValue("Rpt_Fdate", DeliveryWithouttxt.Text);
            Rec.Rpt.SetParameterValue("Rpt_Tdate", "1-1-2020");
            Rec.Rpt.SetParameterValue("RO", CodeWithouttxt.Text);
            Rec.Rpt.SetParameterValue("Restaurant", ResturantCbx.Text);
            Rec.Rpt.SetParameterValue("Kitchen", KitchenCbx.Text);
            Rec.Show();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}