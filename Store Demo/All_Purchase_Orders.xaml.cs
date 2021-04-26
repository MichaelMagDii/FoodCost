using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for All_Purchase_Orders.xaml
    /// </summary>
    public partial class All_Purchase_Orders : Window
    {
        string PO_id;

        PurchaseOrder purchaseOrder;
        public All_Purchase_Orders(PurchaseOrder _purchaseOrder)
        {
            InitializeComponent();

            purchaseOrder = _purchaseOrder;
            SqlConnection con = new SqlConnection(Classes.DataConnString);

            try
            {
                con.Open();
                string s = string.Format("select PO_Serial AS Serial,PO_NO as Number,(Select Name From Setup_Restaurant Where Code=Ship_To) AS Resturant,(Select Name From Vendors Where Code=Vendor_ID) as Vendor,Delivery_Date as Delivery_Date FROM PO where Status <> 'Post'");
                SqlDataAdapter adapter = new SqlDataAdapter(s, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                PO_DGV.DataContext = dt;
            }
            catch { con.Close(); }
            con.Close();
        }

        Transfer_Resturant transfer_Resturant;
        public All_Purchase_Orders(Transfer_Resturant _TranfserResturant)
        {
            InitializeComponent();

            transfer_Resturant = _TranfserResturant;
            SqlConnection con = new SqlConnection(Classes.DataConnString);

            try
            {
                con.Open();
                string s = string.Format("Select Transfer_Serial as Serial,Manual_Transfer_No as Number,Transfer_Date as Date,(SELECT Name FROM Setup_Restaurant Where Code=From_Resturant_ID) as 'From Resturant',(SELECT Name FROM Setup_Kitchens Where Code=From_Kitchen_ID AND RestaurantID=From_Resturant_ID ) as 'From Kitchen',(SELECT Name FROM Setup_Restaurant Where Code=To_Resturant_ID) as 'To Resturant',(SELECT Name FROM Setup_Kitchens Where Code=To_Kitchen_ID AND RestaurantID=To_Resturant_ID) as 'To Kitchen' FROM Transfer_Kitchens Where Type='Transfer_Resturant' and Status <> 'Post' order by Transfer_Serial DESC");
                SqlDataAdapter adapter = new SqlDataAdapter(s, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                PO_DGV.DataContext = dt;
            }
            catch { con.Close(); }
            con.Close();
        }

        Transfer_Kitchens transferKitchen;
        public All_Purchase_Orders(Transfer_Kitchens _TranfserKitchen)
        {
            InitializeComponent();

            transferKitchen = _TranfserKitchen;
            SqlConnection con = new SqlConnection(Classes.DataConnString);

            try
            {
                con.Open();
                string s = string.Format("Select Transfer_Serial as Serial,Manual_Transfer_No As Number,Transfer_Date As Date,(SELECT Name FROM Setup_Restaurant Where Code=From_Resturant_ID) as 'From Resturant',(SELECT Name FROM Setup_Kitchens Where Code=From_Kitchen_ID AND RestaurantID=From_Resturant_ID ) as 'From Kitchen' ,(SELECT Name FROM Setup_Restaurant Where Code=To_Resturant_ID) as 'To Resturant', (SELECT Name FROM Setup_Kitchens Where Code=To_Kitchen_ID AND RestaurantID=To_Resturant_ID) as 'To Kitchen' FROM Transfer_Kitchens Where Type='Transfer_Kitchen' and Status <> 'Post' Order by Transfer_Serial DESC");
                SqlDataAdapter adapter = new SqlDataAdapter(s, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                PO_DGV.DataContext = dt;
            }
            catch { con.Close(); }
            con.Close();
        }

        RecieveOrder recieveOrder;
        public All_Purchase_Orders(RecieveOrder _RecieveOrder)
        {
            InitializeComponent();
            recieveOrder = _RecieveOrder;
            LoadAllPO();
        }

        private void PO_DGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    SqlConnection con = new SqlConnection(Classes.DataConnString);     SqlConnection con2 = new SqlConnection(Classes.DataConnString);
                    MainWindow main = Application.Current.MainWindow as MainWindow;
                    string IsRecieved = "";
                    if (main.GridMain.Children[0].GetType().Name == "PurchaseOrder")
                    {
                        try
                        {
                            DataTable dt = PO_DGV.DataContext as DataTable;

                            con.Open();
                            SqlCommand cmd = new SqlCommand(string.Format("select PO_Serial,PO_No,(select Name from Setup_Restaurant where  Setup_Restaurant.Code = Ship_To) as Restaurant_Trans_ID,(select Name from Vendors where Vendors.Code = Vendor_ID) as Vendor_ID,Delivery_Date,Comment,Status from PO where PO_Serial = '{0}'", dt.Rows[PO_DGV.SelectedIndex]["Serial"]), con);
                            SqlDataReader reader = cmd.ExecuteReader();

                            reader.Read();

                            PO_id = reader["PO_Serial"].ToString();
                            purchaseOrder.Serial_PO_NO.Text = reader["PO_Serial"].ToString();
                            purchaseOrder.PO_NO.Text = reader["PO_No"].ToString();
                            purchaseOrder.ShipTo.Text = reader["Restaurant_Trans_ID"].ToString();
                            purchaseOrder.Vendor.Text = reader["Vendor_ID"].ToString();
                            DateTime TheDatetime = Convert.ToDateTime(reader["Delivery_Date"].ToString());
                            purchaseOrder.Delivery_dt.Text = TheDatetime.ToString("yyyy-MM-dd");
                            purchaseOrder.Delivery_time.Text = TheDatetime.ToString("HH:mm:ss");
                            purchaseOrder.commenttxt.Text = reader["Comment"].ToString();
                            reader.Close();

                            SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select Item_ID as Code,(select [Manual Code] from Setup_Items where Code= Item_ID) as Manual_Code,(select Name from Setup_Items where Code = Item_ID) as Name,(select Name2 from Setup_Items where Code = Item_ID) as Name2,Unit,(select Tax_Included from PO_Items as PI where PI.Item_ID=PO_Items.Item_ID and PO_Serial='{0}') as [Tax Included], Qty,Price_Without_Tax as Price,Tax,Price_With_Tax as [Unit Price With Tax],Price_Without_Tax as [Unit Price Without Tax],Price_With_Tax*Qty as [Total Price With Tax],Price_Without_Tax*Qty as [Total Price Without Tax] from PO_Items where PO_Serial = '{0}' order by Serial", PO_id), con);
                            dt = new DataTable();

                            adapter.Fill(dt);
                            dt = LoadTaxValue(dt);
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                dt.Columns[i].ReadOnly = true;
                            }
                            dt.Columns["Price"].ReadOnly = false;
                            dt.Columns["Qty"].ReadOnly = false;
                            dt.Columns["Tax Included"].ReadOnly = false;
                            purchaseOrder.ItemsDGV.DataContext = dt;

                            float total = 0; float total_Without_Tax = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                total_Without_Tax += float.Parse(dt.Rows[i]["Total Price Without Tax"].ToString());
                                total += float.Parse(dt.Rows[i]["Total Price With Tax"].ToString());
                                purchaseOrder.Total_Price_With_Tax.Text = total.ToString();
                                purchaseOrder.Total_Price_Without_Tax.Text = total_Without_Tax.ToString();
                            }

                            purchaseOrder.EnableUI();
                            purchaseOrder.SaveBtn.IsEnabled = false;
                        }
                        catch(Exception ex) { con.Close(); MessageBox.Show(ex.ToString()); }
                        if (IsRecieved == "Recieved" || purchaseOrder.Statustxt.Text == "Post")
                        {
                            purchaseOrder.PO_NO.IsReadOnly = true;
                            purchaseOrder.Vendor.IsReadOnly = true;
                            purchaseOrder.Vendor.IsEnabled = false;
                            purchaseOrder.Delivery_dt.IsEnabled = false;
                            purchaseOrder.Delivery_time.IsEnabled = false;
                            purchaseOrder.commenttxt.IsReadOnly = true;
                            purchaseOrder.ItemsDGV.IsReadOnly = true;
                            purchaseOrder.AddItemsBtn.IsEnabled = false;
                            purchaseOrder.RemoveItemBtn.IsEnabled = false;
                            purchaseOrder.Statustxt.IsEnabled = false;
                        }
                        else
                        {
                            purchaseOrder.PO_NO.IsReadOnly = false;
                            purchaseOrder.Vendor.IsReadOnly = false;
                            purchaseOrder.Vendor.IsEnabled = true;
                            purchaseOrder.Delivery_dt.IsEnabled = true;
                            purchaseOrder.Delivery_time.IsEnabled = true;
                            purchaseOrder.commenttxt.IsReadOnly = false;
                            purchaseOrder.ItemsDGV.IsReadOnly = false;
                            purchaseOrder.AddItemsBtn.IsEnabled = true;
                            purchaseOrder.RemoveItemBtn.IsEnabled = true;
                            purchaseOrder.AddItemsBtn.IsEnabled = true;
                            purchaseOrder.SaveBtn.IsEnabled = true;
                            purchaseOrder.Statustxt.IsEnabled = true;
                        }

                        con.Close();
                        this.Close();
                    }

                    else if (main.GridMain.Children[0].GetType().Name == "RecieveOrder")
                    {
                        con2 = new SqlConnection(Classes.DataConnString);
                        con = new SqlConnection(Classes.DataConnString);
                        float from_rest_Qty = 0; float from_rest_Cost = 0; float to_rest_Qty = 0;   float to_rest_Cost = 0;
                        if (recieveOrder.TabControl.SelectedIndex == 1)
                        {
                            bool IfItemRecieved = false;
                            DataTable dt = new DataTable();
                            string CodeofRO = "";
                            recieveOrder.recieveTransfer.IsEnabled = true;
                            recieveOrder.ManualResturanttxt.IsEnabled = true;
                            recieveOrder.DeliveryRestauranttxt.IsEnabled = true;
                            recieveOrder.DeliveryROKitchenTime.IsEnabled = true;
                            recieveOrder.CommentRestaurant.IsEnabled = true;
                            DataTable Dat = new DataTable();
                            try
                            {
                                con.Open();
                                SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select Request_Serial,Manual_Request_No,Request_Date,Comment,(select Name From Setup_Restaurant where Code=From_Resturant_ID) as From_Resturat,(select Name From Setup_Restaurant where Code = To_Resturant_ID) as To_Resturat, (select Name from Setup_Kitchens where Code = From_Kitchen_ID and RestaurantID = From_Resturant_ID) as From_Kitchen,(select Name from Setup_Kitchens where Code = To_Kitchen_ID  and RestaurantID = To_Resturant_ID) as To_Kitchen from Requests_tbl Where Type='Transfer_Resturant' and Request_Serial='{0}'", ((DataRowView)PO_DGV.SelectedItem).Row.ItemArray[0]), con);
                                dt = new DataTable();
                                adapter.Fill(dt);

                                DataRow row = dt.Rows[0];
                                CodeofRO = row["Request_Serial"].ToString();
                                RecieveOrder.TransferResturantID = row["Request_Serial"].ToString();
                                recieveOrder.TransferResturanttxt.Text = row["Manual_Request_No"].ToString();
                                recieveOrder.DeliveryRestauranttxt.Text = Convert.ToDateTime(row["Request_Date"]).ToString("dd-MM-yyyy");
                                recieveOrder.FromRestaurant_Resturanttxt.Text = row["From_Resturat"].ToString();
                                recieveOrder.ToResturant_Restauranttxt.Text = row["To_Resturat"].ToString();
                                recieveOrder.FromKitchen_Resturanttxt.Text = row["From_Kitchen"].ToString();
                                recieveOrder.ToKitchen_Restauranttxt.Text = row["To_Kitchen"].ToString();
                            }
                            catch (Exception ex)
                            { MessageBox.Show(ex.ToString()); }
                            finally
                            { con.Close(); }

                            Dat.Columns.Add("Received", typeof(bool));
                            Dat.Columns.Add("ItemID");
                            Dat.Columns.Add("Name");
                            Dat.Columns.Add("Name2");
                            Dat.Columns.Add("Unit");
                            Dat.Columns.Add("Expire Date", typeof(bool));
                            Dat.Columns.Add("Qty");
                            Dat.Columns.Add(recieveOrder.FromKitchen_Resturanttxt.Text + " Qty");
                            Dat.Columns.Add(recieveOrder.FromKitchen_Resturanttxt.Text + " Unit Cost");
                            Dat.Columns.Add(recieveOrder.FromKitchen_Resturanttxt.Text + " total Cost");
                            Dat.Columns.Add(recieveOrder.ToKitchen_Restauranttxt.Text + " Qty");
                            Dat.Columns.Add(recieveOrder.ToKitchen_Restauranttxt.Text + " Unit Cost");
                            Dat.Columns.Add(recieveOrder.ToKitchen_Restauranttxt.Text + " total Cost");
                            try
                            {
                                con.Open();
                                string M = "SELECT Item_ID,Qty,Cost,Unit FROM Requests_Items where Request_ID=" + CodeofRO;
                                SqlCommand cmd = new SqlCommand(M, con);
                                SqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    try
                                    {
                                        con2.Open(); string s = string.Format("SELECT Item_ID FROM RO_Items Where Item_ID={1} AND RO_No = (select RO_ID from RO where PO_No = '{0}' and Type='Transfer_Resturant')", CodeofRO, reader["Item_ID"]);
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
                                        DataTable TheValues = Classes.RetriveCostAndQty(recieveOrder.FromRestaurant_Resturanttxt.Text, recieveOrder.FromKitchen_Resturanttxt.Text, reader["Item_ID"].ToString());
                                        from_rest_Qty = (float.Parse(TheValues.Rows[0][0].ToString()));
                                        from_rest_Cost = float.Parse(TheValues.Rows[0][1].ToString());

                                        TheValues = Classes.RetriveCostAndQty(recieveOrder.ToResturant_Restauranttxt.Text, recieveOrder.ToKitchen_Restauranttxt.Text,reader["Item_ID"].ToString());
                                        to_rest_Qty = (float.Parse(TheValues.Rows[0][0].ToString()) + float.Parse(reader["Qty"].ToString()));
                                        to_rest_Cost = (((float.Parse(reader["Qty"].ToString()) * float.Parse(reader["Cost"].ToString())) + (float.Parse(TheValues.Rows[0][0].ToString()) * float.Parse(TheValues.Rows[0][1].ToString()))) / to_rest_Qty);
                                    }
                                    catch (Exception ex)
                                    {  MessageBox.Show(ex.ToString()); }
                                    finally { con2.Close(); }
                                    //
                                    double NetCostFrom = from_rest_Qty * from_rest_Cost;
                                    double NetCostTo = to_rest_Qty * to_rest_Cost;
                                    try
                                    {
                                        con2.Open();
                                        string S = "SELECT Name,Name2,ExpDate FROM Setup_Items where Code='" + reader["Item_ID"].ToString() + "'";
                                        SqlCommand cmd2 = new SqlCommand(S, con2);
                                        SqlDataReader reader2 = cmd2.ExecuteReader();
                                        while (reader2.Read())
                                        {
                                            Dat.Rows.Add(IfItemRecieved, reader["Item_ID"], reader2["Name"], reader2["Name2"], reader["Unit"], reader2["ExpDate"], reader["Qty"], from_rest_Qty, from_rest_Cost, NetCostFrom, to_rest_Qty, to_rest_Cost, NetCostTo);
                                        }
                                        reader2.Close();
                                    }
                                    catch (Exception ex)
                                    {  MessageBox.Show(ex.ToString());   }
                                    finally
                                    {   con2.Close();   }
                                }

                            }
                            catch (Exception ex)
                            {   MessageBox.Show(ex.ToString());   }
                            finally
                            {  con.Close();  }
                            for (int i = 0; i < Dat.Columns.Count; i++)
                            {
                                Dat.Columns[i].ReadOnly = true;
                            }
                            Dat.Columns["Received"].ReadOnly = false;
                            Dat.Columns["Qty"].ReadOnly = false;
                            recieveOrder.ItemsResturantDGV.DataContext = Dat;
                            float Total_Price = 0;
                            for (int i = 0; i < recieveOrder.ItemsResturantDGV.Items.Count; i++)
                            {
                                Total_Price += (float.Parse(Dat.Rows[i]["Qty"].ToString()) * float.Parse(Dat.Rows[i][recieveOrder.FromKitchen_Resturanttxt.Text + " Unit Cost"].ToString()));
                            }
                            recieveOrder.NumberOfItemsResturant.Text = recieveOrder.ItemsResturantDGV.Items.Count.ToString();
                            recieveOrder.Total_Price_With_Tax_Resturant.Text = Total_Price.ToString();
                            this.Close();
                        }

                        else if (recieveOrder.TabControl.SelectedIndex == 2)
                        {
                            bool IfItemRecieved = false;
                            DataTable dt = new DataTable();
                            string CodeofRO = "";
                            recieveOrder.recieveTransferInter.IsEnabled = true;
                            recieveOrder.ManualKitchentxt.IsEnabled = true;
                            recieveOrder.DeliveryKitchentxt.IsEnabled = true;
                            recieveOrder.DeliveryROInterTime.IsEnabled = true;
                            recieveOrder.CommentKitchentxt.IsEnabled = true;
                            DataTable Dat = new DataTable();
                            try
                            {
                                con.Open();
                                SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select Request_Serial,Manual_Request_No,Request_Date,Comment,(select Name From Setup_Restaurant where Code=From_Resturant_ID) as Resturat, (select Name from Setup_Kitchens where Code = From_Kitchen_ID and RestaurantID = From_Resturant_ID) as From_Kitchen, (select Name from Setup_Kitchens where Code = To_Kitchen_ID  and RestaurantID = From_Resturant_ID) as To_Kitchen from Requests_tbl Where Type='Transfer_Kitchen' and Request_Serial='{0}'", ((DataRowView)PO_DGV.SelectedItem).Row.ItemArray[0]), con);
                                dt = new DataTable();
                                adapter.Fill(dt);

                                DataRow row = dt.Rows[0];
                                CodeofRO = row["Request_Serial"].ToString();
                                RecieveOrder.TransferKitchenID = row["Request_Serial"].ToString();
                                recieveOrder.TransferKitchentxt.Text = row["Manual_Request_No"].ToString();
                                recieveOrder.DeliveryKitchentxt.Text = Convert.ToDateTime(row["Request_Date"]).ToString("dd-MM-yyyy");
                                //recieveOrder.CommentRoInter.Text = row["Comment"].ToString();
                                recieveOrder.Resturant_Kitchentxt.Text = row["Resturat"].ToString();
                                recieveOrder.FromKitchen_Kitchentxt.Text = row["From_Kitchen"].ToString();
                                recieveOrder.ToKitchen_Kitchentxt.Text = row["To_Kitchen"].ToString();
                            }
                            catch (Exception ex)
                            {   MessageBox.Show(ex.ToString());     }
                            finally
                            {    con.Close();  }
                            //try
                            //{
                            //    con.Open();
                            //    string s = string.Format("select Type from RO where Type='Transfer_Kitchen' and PO_No='{0}'", CodeofRO);
                            //    SqlCommand cmd = new SqlCommand(s, con);
                            //    IsRecieved = cmd.ExecuteScalar().ToString();
                            //}
                            //catch
                            //{
                            //    IsRecieved = "Not Recived";
                            //}
                            //finally
                            //{
                            //    con.Close();
                            //}
                            Dat.Columns.Add("Received", typeof(bool));
                            Dat.Columns.Add("ItemID");
                            Dat.Columns.Add("Name");
                            Dat.Columns.Add("Name2");
                            Dat.Columns.Add("Unit");
                            Dat.Columns.Add("Expire Date", typeof(bool));
                            Dat.Columns.Add("Qty");
                            Dat.Columns.Add(recieveOrder.FromKitchen_Kitchentxt.Text + " Qty");
                            Dat.Columns.Add(recieveOrder.FromKitchen_Kitchentxt.Text + " Unit Cost");
                            Dat.Columns.Add(recieveOrder.FromKitchen_Kitchentxt.Text + " total Cost");
                            Dat.Columns.Add(recieveOrder.ToKitchen_Kitchentxt.Text + " Qty");
                            Dat.Columns.Add(recieveOrder.ToKitchen_Kitchentxt.Text + " Unit Cost");
                            Dat.Columns.Add(recieveOrder.ToKitchen_Kitchentxt.Text + " total Cost");
                            try
                            {
                                con.Open();
                                string M = "select Item_ID,Qty,Cost,Unit FROM Requests_Items where Request_ID=" + CodeofRO;
                                SqlCommand cmd = new SqlCommand(M, con);
                                SqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    try
                                    {
                                        con2.Open();
                                        string s = string.Format("SELECT Item_ID FROM RO_Items Where Item_ID={1} AND RO_No = (select RO_ID from RO where PO_No = '{0}' and Type='Transfer_Kitchen')", CodeofRO, reader["Item_ID"]);
                                        SqlCommand cmd2 = new SqlCommand(s, con2);
                                        if (cmd2.ExecuteScalar() != null)
                                        {
                                            IfItemRecieved = true;
                                        }
                                        else { IfItemRecieved = false; }

                                    }
                                    catch { }
                                    finally { con2.Close(); }
                                    //
                                    try
                                    {

                                        DataTable TheVales = Classes.RetriveCostAndQty(recieveOrder.Resturant_Kitchentxt.Text, recieveOrder.FromKitchen_Kitchentxt.Text,reader["Item_ID"].ToString() );
                                        from_rest_Qty = (float.Parse(TheVales.Rows[0][0].ToString()));
                                        from_rest_Cost = float.Parse(TheVales.Rows[0][1].ToString());

                                        TheVales = Classes.RetriveCostAndQty(recieveOrder.Resturant_Kitchentxt.Text, recieveOrder.ToKitchen_Kitchentxt.Text ,reader["Item_ID"].ToString() );
                                        to_rest_Qty = (float.Parse(TheVales.Rows[0][0].ToString()) + float.Parse(reader["Qty"].ToString()));
                                        to_rest_Cost = (((float.Parse(reader["Qty"].ToString()) * float.Parse(reader["Cost"].ToString())) + (float.Parse(TheVales.Rows[0][0].ToString()) * float.Parse(TheVales.Rows[0][1].ToString()))) / to_rest_Qty);
                                    }
                                    catch (Exception ex)
                                    {   MessageBox.Show(ex.ToString());  }
                                    finally { con2.Close(); }
                                    //
                                    double NetCostFrom = from_rest_Qty * from_rest_Cost;
                                    double NetCostTo = to_rest_Qty * to_rest_Cost;
                                    try
                                    {
                                        con2.Open();
                                        string S = "SELECT Name,Name2,ExpDate FROM Setup_Items where Code='" + reader["Item_ID"].ToString() + "'";
                                        SqlCommand cmd2 = new SqlCommand(S, con2);
                                        SqlDataReader reader2 = cmd2.ExecuteReader();
                                        while (reader2.Read())
                                        {
                                            Dat.Rows.Add(IfItemRecieved, reader["Item_ID"], reader2["Name"], reader2["Name2"], reader["Unit"], reader2["ExpDate"], reader["Qty"], from_rest_Qty, from_rest_Cost, NetCostFrom, to_rest_Qty, to_rest_Cost, NetCostTo);
                                        }
                                        reader2.Close();
                                    }
                                    catch (Exception ex)
                                    {  MessageBox.Show(ex.ToString());   }
                                    finally
                                    {   con2.Close();  }
                                }

                            }
                            catch (Exception ex)
                            {  MessageBox.Show(ex.ToString());   }
                            finally
                            {  con.Close();   }
                            for (int i = 0; i < Dat.Columns.Count; i++)
                            {
                                Dat.Columns[i].ReadOnly = true;
                            }
                            Dat.Columns["Received"].ReadOnly = false;
                            Dat.Columns["Qty"].ReadOnly = false;
                            recieveOrder.ItemsKitchenDGV.DataContext = Dat;
                            float Total_Price = 0;
                            for (int i = 0; i < recieveOrder.ItemsKitchenDGV.Items.Count; i++)
                            {
                                Total_Price += (float.Parse(Dat.Rows[i]["Qty"].ToString()) * float.Parse(Dat.Rows[i][recieveOrder.FromKitchen_Kitchentxt.Text + " Unit Cost"].ToString()));
                            }
                            recieveOrder.NumberOfItemsKitchen.Text = recieveOrder.ItemsKitchenDGV.Items.Count.ToString();
                            recieveOrder.Total_Price_With_Tax_Kitchen.Text = Total_Price.ToString();
                            this.Close();
                        }

                        else if (recieveOrder.TabControl.SelectedIndex == 4)
                        {
                            string reqRecieve = "";
                            DataTable dt = new DataTable();

                            DataTable Dat = new DataTable();
                            try
                            {
                                con.Open();
                                SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select Request_Serial,Manual_Request_No,Request_Date,Comment,(select Name From Setup_Restaurant where Code = To_Resturant_ID) as To_Resturat,(select Name from Setup_Kitchens where Code = To_Kitchen_ID  and RestaurantID = To_Resturant_ID) as To_Kitchen,Type from Requests_tbl Where Request_Serial='{0}'", ((DataRowView)PO_DGV.SelectedItem).Row.ItemArray[0]), con);
                                dt = new DataTable();
                                adapter.Fill(dt);

                                DataRow row = dt.Rows[0];
                                recieveOrder.CodeRequesttxt.Text = row["Request_Serial"].ToString();
                                recieveOrder.ManualRequesttxt.Text = row["Manual_Request_No"].ToString();
                                DateTime TheDateTime = Convert.ToDateTime(row["Request_Date"].ToString());
                                recieveOrder.Request_Date.Text = TheDateTime.ToString("dd-MM-yyyy");
                                //recieveOrder.Request_Time.Text = TheDateTime.ToString("HH:mm:ss");
                                recieveOrder.TypeCbx.Text = row["Type"].ToString();
                                recieveOrder.RequestCommenttxt.Text = row["Comment"].ToString();
                                recieveOrder.TOResturantReq.Text = row["To_Resturat"].ToString();
                                recieveOrder.TOKitchenReq.Text = row["To_Kitchen"].ToString();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            finally
                            {
                                con.Close();
                            }

                            //
                            try
                            {
                                con.Open();
                                string s = string.Format("select Type from RO where (Type='Transfer_Resturant' OR Type='Transfer_Kitchen' ) and PO_No='{0}'", recieveOrder.TransferKitchentxt.Text);
                                SqlCommand cmd = new SqlCommand(s, con);
                                reqRecieve = cmd.ExecuteScalar().ToString();
                            }
                            catch
                            {
                                reqRecieve = "Not Recived";
                            }
                            finally
                            {
                                con.Close();
                            }
                            //
                            recieveOrder.toKItchenAndResturant.Visibility = Visibility.Visible;
                            recieveOrder.NUmberOfItemsReq.Visibility = Visibility.Visible;
                            recieveOrder.NumberOfItemTextReq.Visibility = Visibility.Visible;
                            recieveOrder.Total_PriceReq.Visibility = Visibility.Visible;
                            recieveOrder.TotalofItemsReq.Visibility = Visibility.Visible;
                            recieveOrder.Reply.Visibility = Visibility.Visible;
                            recieveOrder.Reply.IsEnabled = true;



                            Dat.Columns.Add("Received", typeof(bool));
                            Dat.Columns.Add("Code");
                            Dat.Columns.Add("Manual Code");
                            Dat.Columns.Add("Name");
                            Dat.Columns.Add("Name2");
                            Dat.Columns.Add("Qty");
                            Dat.Columns.Add(recieveOrder.KitchenReqcbx.Text + " Qty");
                            Dat.Columns.Add(recieveOrder.KitchenReqcbx.Text + " Unit Cost");
                            Dat.Columns.Add(recieveOrder.KitchenReqcbx.Text + " total Cost");
                            Dat.Columns.Add(recieveOrder.TOKitchenReq.Text + " Qty");
                            Dat.Columns.Add(recieveOrder.TOKitchenReq.Text + " Unit Cost");
                            Dat.Columns.Add(recieveOrder.TOKitchenReq.Text + " total Cost");
                            try
                            {
                                con.Open();
                                string M = "SELECT Item_ID,Qty,Cost FROM Requests_Items where Request_ID=" + recieveOrder.CodeRequesttxt.Text;
                                SqlCommand cmd = new SqlCommand(M, con);
                                SqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    try
                                    {
                                        con2.Open();
                                        using (SqlCommand cmd2 = new SqlCommand(string.Format("select Qty,Current_Cost from Items where ItemID = '{0}' and RestaurantID = (select Code from Setup_Restaurant where Name = '{1}') and KitchenID = (select Code from Setup_Kitchens where Name = '{2}') union all select Qty, Current_Cost from Items where ItemID = '{0}' and RestaurantID = (select Code from Setup_Restaurant where Name = '{3}') and KitchenID = (select Code from Setup_Kitchens where Name = '{4}')", reader["Item_ID"], recieveOrder.ResturantReqcbx.Text, recieveOrder.KitchenReqcbx.Text, recieveOrder.TOResturantReq.Text, recieveOrder.TOKitchenReq.Text), con2))
                                        {
                                            SqlDataReader reader2 = cmd2.ExecuteReader();
                                            reader2.Read();

                                            from_rest_Qty = (float.Parse(reader2["Qty"].ToString()) - float.Parse(reader["Qty"].ToString()));
                                            from_rest_Cost = float.Parse(reader2["Current_Cost"].ToString());

                                            reader2.Read();
                                            to_rest_Qty = (float.Parse(reader2["Qty"].ToString()) + float.Parse(reader["Qty"].ToString()));
                                            to_rest_Cost = (((float.Parse(reader["Qty"].ToString()) * float.Parse(reader["Cost"].ToString())) + (float.Parse(reader2["Qty"].ToString()) * float.Parse(reader2["Current_Cost"].ToString()))) / to_rest_Qty);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }
                                    finally { con2.Close(); }
                                    //
                                    double NetCostFrom = from_rest_Qty * from_rest_Cost;
                                    double NetCostTo = to_rest_Qty * to_rest_Cost;
                                    try
                                    {
                                        con2.Open();
                                        string S = "SELECT [Manual Code],Name,Name2 FROM Setup_Items where Code='" + reader["Item_ID"].ToString() + "'";
                                        SqlCommand cmd2 = new SqlCommand(S, con2);
                                        SqlDataReader reader2 = cmd2.ExecuteReader();
                                        while (reader2.Read())
                                        {
                                            Dat.Rows.Add(true, reader["Item_ID"], reader2["Manual Code"], reader2["Name"], reader2["Name2"], reader["Qty"], from_rest_Qty, from_rest_Cost, NetCostFrom, to_rest_Qty, to_rest_Cost, NetCostTo);
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

                            Dat.Columns["Received"].ReadOnly = false;
                            Dat.Columns["Qty"].ReadOnly = false;
                            recieveOrder.RequestsItemsDGV.DataContext = Dat;
                            float Total_Price = 0;
                            for (int i = 0; i < recieveOrder.RequestsItemsDGV.Items.Count; i++)
                            {
                                Total_Price += float.Parse(Dat.Rows[i][recieveOrder.TOKitchenReq.Text + " total Cost"].ToString());
                            }
                            recieveOrder.NUmberOfItemsReq.Text = recieveOrder.RequestsItemsDGV.Items.Count.ToString();
                            recieveOrder.Total_PriceReq.Text = Total_Price.ToString();
                            recieveOrder.RequestssDGV.Visibility = Visibility.Hidden;
                            recieveOrder.RequestsItemsDGV.Visibility = Visibility.Visible;

                            recieveOrder.toKItchenAndResturant.Visibility = Visibility.Visible;
                            recieveOrder.NUmberOfItemsReq.Visibility = Visibility.Visible;
                            recieveOrder.NumberOfItemTextReq.Visibility = Visibility.Visible;
                            recieveOrder.Total_PriceReq.Visibility = Visibility.Visible;
                            recieveOrder.TotalofItemsReq.Visibility = Visibility.Visible;
                            recieveOrder.Reply.Visibility = Visibility.Visible;

                            if (reqRecieve == "Recieved")
                            {
                                recieveOrder.CodeRequesttxt.IsEnabled = false;
                                recieveOrder.ManualRequesttxt.IsEnabled = false;
                                recieveOrder.TypeCbx.IsEnabled = false;
                                recieveOrder.Request_Date.IsEnabled = false;
                                recieveOrder.Request_Time.IsEnabled = false;
                                recieveOrder.RequestCommenttxt.IsEnabled = false;
                                recieveOrder.TOResturantReq.IsEnabled = false;
                                recieveOrder.TOKitchenReq.IsEnabled = false;
                                recieveOrder.RequestsItemsDGV.IsEnabled = false;
                                recieveOrder.Reply.IsEnabled = false;
                            }
                            else
                            {
                                recieveOrder.CodeRequesttxt.IsEnabled = true;
                                recieveOrder.ManualRequesttxt.IsEnabled = true;
                                recieveOrder.TypeCbx.IsEnabled = true;
                                recieveOrder.Request_Date.IsEnabled = true;
                                recieveOrder.Request_Time.IsEnabled = true;
                                recieveOrder.RequestCommenttxt.IsEnabled = true;
                                recieveOrder.TOResturantReq.IsEnabled = true;
                                recieveOrder.TOKitchenReq.IsEnabled = true;
                                recieveOrder.RequestsItemsDGV.IsEnabled = true;


                                recieveOrder.Reply.IsEnabled = true;
                            }
                            this.Close();
                        }
                    }

                    else if (main.GridMain.Children[0].GetType().Name == "Transfer_Kitchens")
                    {
                        transferKitchen.MainGrid.IsEnabled = true;
                        transferKitchen.TransferBtn.IsEnabled = true;
                        transferKitchen.NewBtn.IsEnabled = true;
                        DataTable dt = new DataTable();
                        DataTable dat = PO_DGV.DataContext as DataTable;
                        string FromKitchenQty = "", FRomKitchenCost = "", FromKitchenTotal = "", ToKitchenQty = "", ToKitchenCost = "", ToKitchenTotal = "";
                        SqlCommand cmd2 = new SqlCommand();
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand(string.Format("select Transfer_Serial,Manual_Transfer_No,Transfer_Date,(SELECT Name FROM Setup_Restaurant Where Code=From_Resturant_ID)AS Resturant,(SELECT Name FROM Setup_Kitchens Where Code=From_Kitchen_ID AND RestaurantID=From_Resturant_ID )AS FROM_KItchen, (SELECT Name FROM Setup_Kitchens Where Code=To_Kitchen_ID AND RestaurantID=To_Resturant_ID)AS TO_Kitchen,Comment,Status from Transfer_Kitchens where Transfer_Serial = '{0}'", dat.Rows[PO_DGV.SelectedIndex]["Serial"]), con);
                            SqlDataReader reader = cmd.ExecuteReader();

                            reader.Read();

                            transferKitchen.transfer_No.Text = reader["Transfer_Serial"].ToString();
                            transferKitchen.Manual_transfer_No.Text = reader["Manual_Transfer_No"].ToString();
                            DateTime TheDateTime = Convert.ToDateTime(reader["Transfer_Date"].ToString());
                            transferKitchen.Transfer_dt.Text = TheDateTime.ToString("yyyy-MM-dd");
                            transferKitchen.Transfer_Time.Text = TheDateTime.ToString("HH:mm:ss");
                            transferKitchen.commenttxt.Text = reader["Comment"].ToString();
                            transferKitchen.Statustxt.Text = reader["Status"].ToString();
                            transferKitchen.Resturant.Text = reader["Resturant"].ToString();
                            transferKitchen.From_Kitchen.Text = reader["FROM_KItchen"].ToString();
                            transferKitchen.To_Kitchen.Text = reader["TO_Kitchen"].ToString();

                            dt.Columns.Add("Code");
                            dt.Columns.Add("Manual Code");
                            dt.Columns.Add("Name");
                            dt.Columns.Add("Name2");
                            dt.Columns.Add("Unit");
                            dt.Columns.Add("Qty");
                            dt.Columns.Add(transferKitchen.From_Kitchen.Text + " Qty");
                            dt.Columns.Add(transferKitchen.From_Kitchen.Text + " Unit Cost");
                            dt.Columns.Add(transferKitchen.From_Kitchen.Text + " total Cost");
                            dt.Columns.Add(transferKitchen.To_Kitchen.Text + " Qty");
                            dt.Columns.Add(transferKitchen.To_Kitchen.Text + " Unit Cost");
                            dt.Columns.Add(transferKitchen.To_Kitchen.Text + " total Cost");
                            reader.Close();
                            con2.Open();
                            string W = string.Format("select Item_ID as Code,(select [Manual Code] from Setup_Items where Code= Item_ID) as Manual_Code,(select Name from Setup_Items where Code = Item_ID) as Name,(select Name2 from Setup_Items where Code = Item_ID) as Name2,Unit, Qty from Transfer_Kitchens_Items where Transfer_ID = '{0}' order by Serial", transferKitchen.transfer_No.Text);
                            cmd = new SqlCommand(W, con);
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                try
                                {
                                    DataTable TheValue = Classes.RetriveCostAndQty(transferKitchen.Resturant.Text, transferKitchen.From_Kitchen.Text, reader["Code"].ToString());
                                    float from_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString());
                                    float from_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString());

                                    TheValue = Classes.RetriveCostAndQty(transferKitchen.Resturant.Text, transferKitchen.To_Kitchen.Text, reader["Code"].ToString());
                                    float to_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString());
                                    float to_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString());


                                    FromKitchenQty = (from_rest_Qty - float.Parse(reader["Qty"].ToString())).ToString();
                                    FRomKitchenCost = from_rest_Cost.ToString();
                                    FromKitchenTotal = (from_rest_Cost * (from_rest_Qty - float.Parse(reader["Qty"].ToString()))).ToString();

                                    ToKitchenQty = (to_rest_Qty + float.Parse(reader["Qty"].ToString())).ToString();
                                    ToKitchenCost = (((to_rest_Cost * to_rest_Qty) + (float.Parse(reader["Qty"].ToString()) * from_rest_Cost)) / (to_rest_Qty + (float.Parse(reader["Qty"].ToString())))).ToString();
                                    ToKitchenTotal = (((to_rest_Cost * to_rest_Qty) + (float.Parse(reader["Qty"].ToString()) * from_rest_Cost)) / (to_rest_Qty + (float.Parse(reader["Qty"].ToString()))) * (to_rest_Qty + float.Parse(reader["Qty"].ToString()))).ToString();
                                }
                                catch (Exception ex)
                                {   MessageBox.Show(ex.ToString());   }

                                dt.Rows.Add(reader["Code"], reader["Manual_Code"], reader["Name"], reader["Name2"],reader["Unit"], reader["Qty"], FromKitchenQty, FRomKitchenCost, FromKitchenTotal, ToKitchenQty, ToKitchenCost, ToKitchenTotal);
                            }
                        }
                        catch { }
                        finally { con.Close(); con2.Close(); }

                        dt.Columns["Qty"].ReadOnly = false;
                        dt.Columns["Manual Code"].ReadOnly = false;
                        dt.Columns["Name"].ReadOnly = false;
                        dt.Columns["Name2"].ReadOnly = false;

                        transferKitchen.ItemsDGV.DataContext = dt;

                        float total = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            total += float.Parse(dt.Rows[i][transferKitchen.To_Kitchen.Text + " total Cost"].ToString());
                        }
                        transferKitchen.NUmberOfItems.Text = dt.Rows.Count.ToString();
                        transferKitchen.Total_Price.Text = total.ToString();


                        if (transferKitchen.Statustxt.Text == "Post")
                        {
                            transferKitchen.transfer_No.IsReadOnly = true;
                            transferKitchen.Manual_transfer_No.IsReadOnly = true;
                            transferKitchen.Transfer_dt.IsEnabled = false;
                            transferKitchen.Transfer_Time.IsEnabled = false;
                            transferKitchen.Statustxt.IsEnabled = false;
                            transferKitchen.commenttxt.IsReadOnly = true;
                            transferKitchen.Resturant.IsReadOnly = true;
                            transferKitchen.From_Kitchen.IsReadOnly = true;
                            transferKitchen.To_Kitchen.IsReadOnly = true;
                            transferKitchen.resturantBtn.IsEnabled = false;
                            transferKitchen.FromKitchenBtn.IsEnabled = false;
                            transferKitchen.ToKitchenBtn.IsEnabled = false;
                            transferKitchen.ItemsDGV.IsReadOnly = true;
                            transferKitchen.AddItemsBtn.IsEnabled = false;
                            transferKitchen.RemoveItemBtn.IsEnabled = false;
                            transferKitchen.SearchBtn.IsEnabled = true;
                            transferKitchen.NewBtn.IsEnabled = true;
                            transferKitchen.UndoBtn.IsEnabled = true;
                            transferKitchen.TransferBtn.IsEnabled = false;
                        }
                        else
                        {
                            transferKitchen.transfer_No.IsReadOnly = true;
                            transferKitchen.Manual_transfer_No.IsReadOnly = false;
                            transferKitchen.Manual_transfer_No.IsEnabled = true;
                            transferKitchen.Transfer_dt.IsEnabled = true;
                            transferKitchen.Transfer_Time.IsEnabled = true;
                            transferKitchen.Statustxt.IsEnabled = true;
                            transferKitchen.commenttxt.IsReadOnly = false;
                            transferKitchen.commenttxt.IsEnabled = true;
                            transferKitchen.Resturant.IsReadOnly = true;
                            transferKitchen.From_Kitchen.IsReadOnly = true;
                            transferKitchen.To_Kitchen.IsReadOnly = true;
                            transferKitchen.resturantBtn.IsEnabled = true;
                            transferKitchen.FromKitchenBtn.IsEnabled = true;
                            transferKitchen.ToKitchenBtn.IsEnabled = true;
                            transferKitchen.ItemsDGV.IsReadOnly = false;
                            transferKitchen.AddItemsBtn.IsEnabled = true;
                            transferKitchen.RemoveItemBtn.IsEnabled = true;
                            transferKitchen.SearchBtn.IsEnabled = true;
                            transferKitchen.NewBtn.IsEnabled = true;
                            transferKitchen.UndoBtn.IsEnabled = true;
                            transferKitchen.TransferBtn.IsEnabled = true;
                        }

                        con.Close();
                        this.Close();
                    }

                    else if (main.GridMain.Children[0].GetType().Name == "Transfer_Resturant")
                    {
                        transfer_Resturant.MainGrid.IsEnabled = true;
                        transfer_Resturant.TransferBtn.IsEnabled = true;
                        transfer_Resturant.NewBtn.IsEnabled = true;
                        DataTable dt = new DataTable();
                        DataTable dat = PO_DGV.DataContext as DataTable;
                        string FromKitchenQty = "", FRomKitchenCost = "", FromKitchenTotal = "", ToKitchenQty = "", ToKitchenCost = "", ToKitchenTotal = "";
                        SqlCommand cmd2 = new SqlCommand();
                        SqlDataReader reader2 = null;
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand(string.Format("select Transfer_Serial,Manual_Transfer_No,Transfer_Date,(SELECT Name FROM Setup_Restaurant Where Code=From_Resturant_ID)AS From_Resturant,(SELECT Name FROM Setup_Restaurant Where Code=To_Resturant_ID)AS To_Resturant,(SELECT Name FROM Setup_Kitchens Where Code=From_Kitchen_ID AND RestaurantID=From_Resturant_ID )AS FROM_KItchen, (SELECT Name FROM Setup_Kitchens Where Code=To_Kitchen_ID AND RestaurantID=To_Resturant_ID)AS TO_Kitchen,Comment,Status from Transfer_Kitchens where Transfer_Serial = '{0}'", dat.Rows[PO_DGV.SelectedIndex]["Serial"]), con);
                            SqlDataReader reader = cmd.ExecuteReader();

                            reader.Read();

                            transfer_Resturant.transfer_No.Text = reader["Transfer_Serial"].ToString();
                            transfer_Resturant.Manual_transfer_No.Text = reader["Manual_Transfer_No"].ToString();
                            DateTime TheDateTime = Convert.ToDateTime(reader["Transfer_Date"].ToString());
                            transfer_Resturant.Transfer_dt.Text = TheDateTime.ToString("yyyy-MM-dd");
                            transfer_Resturant.Transfer_TIme.Text = TheDateTime.ToString("HH:mm:ss");
                            transfer_Resturant.commenttxt.Text = reader["Comment"].ToString();
                            transfer_Resturant.Statustxt.Text = reader["Status"].ToString();
                            transfer_Resturant.From_Resturant.Text = reader["From_Resturant"].ToString();
                            transfer_Resturant.ToResturant.Text = reader["To_Resturant"].ToString();
                            transfer_Resturant.From_Kitchen.Text = reader["FROM_KItchen"].ToString();
                            transfer_Resturant.To_Kitchen.Text = reader["TO_Kitchen"].ToString();

                            dt.Columns.Add("Code");
                            dt.Columns.Add("Manual Code");
                            dt.Columns.Add("Name");
                            dt.Columns.Add("Name2");
                            dt.Columns.Add("Unit");
                            dt.Columns.Add("Qty");
                            dt.Columns.Add(transfer_Resturant.From_Resturant.Text + " Qty");
                            dt.Columns.Add(transfer_Resturant.From_Resturant.Text + " Unit Cost");
                            dt.Columns.Add(transfer_Resturant.From_Resturant.Text + " total Cost");
                            dt.Columns.Add(transfer_Resturant.ToResturant.Text + " Qty");
                            dt.Columns.Add(transfer_Resturant.ToResturant.Text + " Unit Cost");
                            dt.Columns.Add(transfer_Resturant.ToResturant.Text + " total Cost");
                            reader.Close();

                            con2.Open();
                            string W = string.Format("select Item_ID as Code,(select [Manual Code] from Setup_Items where Code= Item_ID) as Manual_Code,(select Name from Setup_Items where Code = Item_ID) as Name,(select Name2 from Setup_Items where Code = Item_ID) as Name2,Unit, Qty from Transfer_Kitchens_Items where Transfer_ID = '{0}' order by Serial", transfer_Resturant.transfer_No.Text);
                            cmd = new SqlCommand(W, con);
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                try
                                {
                                    DataTable TheValue = Classes.RetriveCostAndQty(transfer_Resturant.From_Resturant.Text, transfer_Resturant.From_Kitchen.Text, reader["Code"].ToString());
                                    float from_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString());
                                    float from_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString());

                                    TheValue = Classes.RetriveCostAndQty(transfer_Resturant.ToResturant.Text, transfer_Resturant.To_Kitchen.Text, reader["Code"].ToString());
                                    float to_rest_Qty = float.Parse(TheValue.Rows[0][0].ToString());
                                    float to_rest_Cost = float.Parse(TheValue.Rows[0][1].ToString());

                                    FromKitchenQty = (from_rest_Qty - float.Parse(reader["Qty"].ToString())).ToString();
                                    FRomKitchenCost = from_rest_Cost.ToString();
                                    FromKitchenTotal = (from_rest_Cost * (from_rest_Qty - float.Parse(reader["Qty"].ToString()))).ToString();

                                    ToKitchenQty = (to_rest_Qty + float.Parse(reader["Qty"].ToString())).ToString();
                                    ToKitchenCost = (((to_rest_Cost * to_rest_Qty) + (float.Parse(reader["Qty"].ToString()) * from_rest_Cost)) / (to_rest_Qty + (float.Parse(reader["Qty"].ToString())))).ToString();
                                    ToKitchenTotal = (((to_rest_Cost * to_rest_Qty) + (float.Parse(reader["Qty"].ToString()) * from_rest_Cost)) / (to_rest_Qty + (float.Parse(reader["Qty"].ToString()))) * (to_rest_Qty + float.Parse(reader["Qty"].ToString()))).ToString();
                                }
                                catch (Exception ex)
                                {  MessageBox.Show(ex.ToString()); }

                                dt.Rows.Add(reader["Code"], reader["Manual_Code"], reader["Name"], reader["Name2"], reader["Unit"], reader["Qty"], FromKitchenQty, FRomKitchenCost, FromKitchenTotal, ToKitchenQty, ToKitchenCost, ToKitchenTotal);
                            }


                        }
                        catch { }
                        finally { con.Close(); con2.Close(); }

                        dt.Columns["Qty"].ReadOnly = false;
                        dt.Columns["Manual Code"].ReadOnly = false;
                        dt.Columns["Name"].ReadOnly = false;
                        dt.Columns["Name2"].ReadOnly = false;

                        transfer_Resturant.ItemsDGV.DataContext = dt;

                        float total = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            total += float.Parse(dt.Rows[i][transfer_Resturant.ToResturant.Text + " total Cost"].ToString());
                        }
                        transfer_Resturant.NUmberOfItems.Text = dt.Rows.Count.ToString();
                        transfer_Resturant.Total_Price.Text = total.ToString();


                        if (transfer_Resturant.Statustxt.Text == "Post")
                        {
                            transfer_Resturant.ItemsView.IsEnabled = false;
                            transfer_Resturant.DetailsView.IsEnabled = false;
                            transfer_Resturant.SearchBtn.IsEnabled = true;
                            transfer_Resturant.NewBtn.IsEnabled = true;
                            transfer_Resturant.UndoBtn.IsEnabled = true;
                            transfer_Resturant.TransferBtn.IsEnabled = false;
                        }
                        else
                        {
                            transfer_Resturant.ItemsView.IsEnabled = true;
                            transfer_Resturant.DetailsView.IsEnabled = true;
                            transfer_Resturant.SearchBtn.IsEnabled = true;
                            transfer_Resturant.NewBtn.IsEnabled = true;
                            transfer_Resturant.UndoBtn.IsEnabled = true;
                            transfer_Resturant.TransferBtn.IsEnabled = true;
                        }

                        con.Close();
                        this.Close();
                    }
                }
            }
        }

        private DataTable LoadTaxValue(DataTable dt)
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;
            /// 
            int q = dt.Columns["Tax"].Ordinal;
            dt.Columns.RemoveAt(q);
            dt.Columns.Add("Tax", typeof(string));
            dt.Columns["Tax"].SetOrdinal(q);
            dt.Columns["Tax"].DataType = typeof(string);
            con.Open();
            for(int i=0;i<dt.Rows.Count;i++)
            {
                using (SqlCommand cmd = new SqlCommand(string.Format("select Is_TaxableItem,TaxableValue from Setup_Items where Code = '{0}'", dt.Rows[i]["Code"].ToString()), con))
                {
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    
                    if (reader["Is_TaxableItem"].ToString() == "False")
                        dt.Rows[i]["Tax"] = "0%";
                    else
                        dt.Rows[i]["Tax"] = reader["TaxableValue"] + "%";
                }
                reader.Close();
            }
            return dt;
        }
        private void LoadAllPO()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            if (recieveOrder.TabControl.SelectedIndex==1)
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select Request_Serial,Manual_Request_No,Request_Date,(select Name From Setup_Restaurant where Code=From_Resturant_ID) as From_Resturat,(select Name From Setup_Restaurant where Code = To_Resturant_ID) as To_Resturat,(select Name from Setup_Kitchens where Code = From_Kitchen_ID and RestaurantID = From_Resturant_ID) as From_Kitchen,(select Name from Setup_Kitchens where Code = To_Kitchen_ID  and RestaurantID = To_Resturant_ID) as To_Kitchen From Requests_tbl where type = 'Transfer_Resturant' and Status='Post' and Request_Serial not in (select Transactions_No From RO where Transactions_No=Request_Serial and type = 'Transfer_Resturant')", con);
                    da.Fill(dt);
                    PO_DGV.DataContext = dt;
                }
                catch (Exception ex)
                {     MessageBox.Show(ex.ToString());  }
                finally {   con.Close();  }
            }
            else if(recieveOrder.TabControl.SelectedIndex == 2)
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select Request_Serial,Manual_Request_No,Request_Date,(select Name From Setup_Restaurant where Code=From_Resturant_ID) as Resturat,(select Name from Setup_Kitchens where Code = From_Kitchen_ID and RestaurantID = From_Resturant_ID) as From_Kitchen,(select Name from Setup_Kitchens where Code = To_Kitchen_ID and RestaurantID = From_Resturant_ID) as To_Kitchen From Requests_tbl where type = 'Transfer_Kitchen' and Status='Post'and Request_Serial not in (Select Transactions_No from RO where Transactions_No=Request_Serial and type = 'Transfer_Kitchen')", con);
                    da.Fill(dt);
                    PO_DGV.DataContext = dt;
                }
                catch (Exception ex)
                {  MessageBox.Show(ex.ToString());   }
                finally
                {   con.Close();  }
            }
            else if(recieveOrder.TabControl.SelectedIndex == 4)
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select Request_Serial,Manual_Request_No,Request_Date, (select Name From Setup_Restaurant where Code=From_Resturant_ID) as Resturat,(select Name from Setup_Kitchens where Code = From_Kitchen_ID and RestaurantID = From_Resturant_ID) as From_Kitchen,(select Name from Setup_Kitchens where Code = To_Kitchen_ID and RestaurantID = From_Resturant_ID) as To_Kitchen,Type From Requests_tbl ORDER BY Request_Serial DESC", con);
                    da.Fill(dt);
                    PO_DGV.DataContext = dt;
                }
                catch (Exception ex)
                {    MessageBox.Show(ex.ToString());   }
                finally  { con.Close();  }
            }
        }
    }
}
