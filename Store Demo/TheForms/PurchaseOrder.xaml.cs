using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for PurchaseOrder.xaml
    /// </summary>
    public partial class PurchaseOrder : UserControl
    {
        public string RestaurantCode = "";public string KitchenCode = "";
        List<string> Authenticated = new List<string>();
        int IndexOfRecord = 0;
        public PurchaseOrder()
        {
            if (MainWindow.AuthenticationData.Count != 0)
            {
                if (MainWindow.AuthenticationData.ContainsKey("Purchase"))
                {
                    Authenticated = MainWindow.AuthenticationData["Purchase"];
                    if (Authenticated.Count == 0)
                    {
                        MessageBox.Show("You Havent a Privilage to Open this Page");
                        LogIn logIn = new LogIn();
                        logIn.ShowDialog();
                    }
                    else
                    {
                        InitializeComponent();
                        LoadTheMainRestaurant();
                        DateTime now = DateTime.Now;
                        MainUiFormat();
                    }
                }
            }
            else
            {
                MessageBox.Show("You should Login First !");
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
        }   //Done
        private void LoadTheMainRestaurant()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "select Code,Name from Setup_Restaurant where IsMain='True'";
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                reader.Read();
                RestaurantCode = reader["Code"].ToString();
                ShipTo.Text = reader["Name"].ToString();
            }
            catch
            { MessageBox.Show("First You should create Main Restaurant"); }
            reader.Close();
            reader = null;
            try
            {
                string s = string.Format("select Code,Name from Setup_Kitchens where RestaurantID='{0}' and IsMain='True'", RestaurantCode);
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                reader.Read();
                KitchenCode = reader["Code"].ToString();
                KitchenShipTo.Text = reader["Name"].ToString();
            }
            catch
            {   MessageBox.Show("First You should create Main Kitchen as Main Restaurant");   }
            //
            try
            {
                string s = string.Format("select Code from Setup_Kitchens where RestaurantID='{0}' and IsMain='True'", RestaurantCode);
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                reader.Read();
                KitchenCode = reader["Code"].ToString();
            }
            catch { }
        }
        private void MainUiFormat()
        {
            MainGrid.IsEnabled = false;
            SaveBtn.IsEnabled = false;
            UndoBtn.IsEnabled = false;
            NewBtn.IsEnabled = true;
            searchBtn.IsEnabled = true;
        }   ///Done
        public void EnableUI()
        {
            MainGrid.IsEnabled = true;
            RemoveItemBtn.IsEnabled = true;
            AddItemsBtn.IsEnabled = true;
            Vendor.IsEnabled = true;
            Delivery_dt.IsEnabled = true;
            SaveBtn.IsEnabled = true;
            UndoBtn.IsEnabled = true;
            NewBtn.IsEnabled = true;
            CopyBtn.IsEnabled = true;
        }    //Done
        private bool DoSomeChecks()
        {
            if (PO_NO.Text.Equals(""))
            {   MessageBox.Show("P.O # Can't Be Empty");   }
            else if (ShipTo.Text.Equals(""))
            {   MessageBox.Show("ShipTo Can't Be Empty");  }
            else if (Vendor.Text.Equals(""))
            {   MessageBox.Show("Vendor Can't Be Empty");  }
            else if (Delivery_dt.Text.Equals(""))
            {   MessageBox.Show("Delivery Date Can't Be Empty");  }
            else if (ShipTo.Text.Equals(""))
            {   MessageBox.Show("Ship to Kitchen Can't Be Empty");  }
            else if (ItemsDGV.Items.Count == 0)
            {   MessageBox.Show("Items can not be empty");  }
            else
            {
                for (int i = 0; i < ItemsDGV.Items.Count; i++)
                {
                    try
                    {   float.Parse((ItemsDGV.Items[i] as DataRowView).Row["Qty"].ToString());   }
                    catch
                    {
                        MessageBox.Show("Qty input error");
                        ItemsDGV.CurrentCell = new DataGridCellInfo(ItemsDGV.Items[i], ItemsDGV.Columns[5]);  //nb2a n3dl el index kol mara nezawd column
                        ItemsDGV.BeginEdit();
                        return false;
                    }

                    try
                    {   float.Parse((ItemsDGV.Items[i] as DataRowView).Row["Price"].ToString());  }
                    catch
                    {
                        MessageBox.Show("Price input error");
                        ItemsDGV.CurrentCell = new DataGridCellInfo(ItemsDGV.Items[i], ItemsDGV.Columns[6]);  //nb2a n3dl el index kol mara nezawd column
                        ItemsDGV.BeginEdit();
                        return false;
                    }

                }

                return true;
            }
            return false;
        }   //Done
        private void ClearFields()
        {
            PO_NO.Text = "";
            Total_Price_Without_Tax.Text = "0";
            Total_Price_With_Tax.Text = "0";
            Serial_PO_NO.Text = "";
            ShipTo.Text = "";
            KitchenShipTo.Text = "";
            Vendor.Text = "";
            Delivery_dt.Text = "";
            commenttxt.Text = "";
            ItemsDGV.DataContext = null;
        }   //Done

        private void Save_PO_Items(SqlConnection con)
        {
            try
            {
                con.Open();
                DataTable Dat = ItemsDGV.DataContext as DataTable;
                for (int i = 0; i < ItemsDGV.Items.Count; i++)
                {
                    string FiledSelection = "Item_ID,PO_Serial,Qty,Unit,Serial,Price_Without_Tax,Tax,Price_With_Tax,Net_Price,Tax_Included";
                    string Values = "'" + Dat.Rows[i]["Code"] + "','" + Serial_PO_NO.Text + "'," + Dat.Rows[i]["Qty"] + ",N'" + Dat.Rows[i]["Unit"] + "'," + i + "," + Dat.Rows[i]["Unit Price Without Tax"] + "," + Dat.Rows[i]["Tax"].ToString().Substring(0, Dat.Rows[i]["Tax"].ToString().Length - 1) + "," + Dat.Rows[i]["Unit Price With Tax"] + "," + Dat.Rows[i]["Total Price With Tax"] + ",'" + Dat.Rows[i]["Tax Included"].ToString() + "'";
                    Classes.InsertRow("PO_Items", FiledSelection, Values);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { con.Close(); }
        }  //Done

        //Events
        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("NewPO") == -1 && Authenticated.IndexOf("CheckAllPO") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                EnableUI();
                ClearFields();
                Delivery_dt.SelectedDate = DateTime.Now;
                Serial_PO_NO.Text = Classes.InCrementTransactionSerial("PO", "PO_Serial");
                PO_NO.IsReadOnly = false;
                commenttxt.IsReadOnly = false;
                ItemsDGV.IsReadOnly = false;
                CopyBtn.IsEnabled = false;
                searchBtn.IsEnabled = false;
                NewBtn.IsEnabled = false;
                Total_Price_With_Tax.Text = "0";
            }
        }           //Done
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("OrderPO") == -1 && Authenticated.IndexOf("CheckAllPO") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                if (!DoSomeChecks())
                    return;

                SqlConnection con = new SqlConnection(Classes.DataConnString);
                SqlConnection con2 = new SqlConnection(Classes.DataConnString);

                try
                {
                    con2.Open();
                    string s = string.Format("select PO_Serial From PO WHere PO_Serial='{0}'", Serial_PO_NO.Text);
                    SqlCommand cmd = new SqlCommand(s, con2);
                    if (cmd.ExecuteScalar() == null)
                    {
                        try
                        {
                            Save_PO_Items(con);
                            Save_PO_Details(con);
                        }
                        catch { return; }
                    }
                    else
                    {
                        try
                        {
                            s = string.Format("delete From PO_Items where PO_Serial='{0}'", Serial_PO_NO.Text);
                            cmd = new SqlCommand(s, con2);
                            cmd.ExecuteNonQuery();
                            try
                            {
                                Save_PO_Items(con);
                                Edit_PO_Details(con);
                            }
                            catch { return; }
                        }
                        catch { return; }
                    }
                }
                catch { }
                MainUiFormat();
                ClearFields();
                MessageBox.Show("Saved Successfully");
            }
        }               //Done
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            MainUiFormat();
            CopyBtn.IsEnabled = false;
        }               //Done
        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("AddItemPO") == -1 && Authenticated.IndexOf("CheckAllPO") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                Items itemswindow = new Items(this);
                itemswindow.ShowDialog();
            }
        }           //Done
        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("CopyPO") == -1 && Authenticated.IndexOf("CheckAllPO") == -1)
            {
                MainWindow.UserID2 = MainWindow.UserID;
                MainWindow.AuthenticationData2 = MainWindow.AuthenticationData;
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                EnableUI();
                Serial_PO_NO.Text = Classes.InCrementTransactionSerial("PO", "PO_Serial");
                PO_NO.Text = "";
                NewBtn.IsEnabled = false;
                CopyBtn.IsEnabled = false;
                if (MainWindow.AuthenticationData2.Count > 0)
                {
                    MainWindow.AuthenticationData = MainWindow.AuthenticationData2;
                    MainWindow.UserID = MainWindow.UserID2;
                }
            }
        }           //Done
        private void RemoveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("RemoveItemPO") == -1 && Authenticated.IndexOf("CheckAllPO") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                DataTable dt = ItemsDGV.DataContext as DataTable;
                dt.Rows.RemoveAt(IndexOfRecord);
                ItemsDGV.DataContext = dt;
                RemoveItemBtn.IsEnabled = false;
            }
        }       //Done
        private void ItemsDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                DataGrid data = sender as DataGrid;

                if (data != null && data.SelectedItems != null && data.SelectedItems.Count == 1)
                {
                    RemoveItemBtn.IsEnabled = true;
                }
            }
        }       //Done
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("SearchPO") == -1 && Authenticated.IndexOf("CheckAllPO") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                EnableUI();
                NewBtn.IsEnabled = false;
                searchBtn.IsEnabled = false;
                SaveBtn.IsEnabled = false;

                All_Purchase_Orders all_Purchase_Orders = new All_Purchase_Orders(this);
                all_Purchase_Orders.ShowDialog();
            }
        }       //Done
        private void VendorBtn_Click(object sender, RoutedEventArgs e)
        {
            AllVendor allVendor = new AllVendor(this);
            allVendor.ShowDialog();
        }   //Done
        private void ItemsDGV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                IndexOfRecord = grid.SelectedIndex;
            }
        }   //Done
        private void ItemDgv_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataTable dt = ItemsDGV.DataContext as DataTable;
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
            ItemsDGV.DataContext = dt;
        }   //Done
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

        }           //Done
        private void ResturantBtn_Click(object sender, RoutedEventArgs e)
        {
            All_Resturants allRestaurant = new All_Resturants(this);
            allRestaurant.ShowDialog();
            KitchenBtn.IsEnabled = true;
            RestaurantCode = Classes.RetrieveRestaurantCode(ShipTo.Text);
        }
        private void KitchenBtn_Click(object sender, RoutedEventArgs e)
        {
            All_Kitchens allKitchen = new All_Kitchens(this, ShipTo.Text);
            allKitchen.ShowDialog();
            KitchenCode = Classes.RetrieveKitchenCode(KitchenShipTo.Text , ShipTo.Text);
        }
        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }   //Done
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }   //Done

        //

        private void Save_PO_Details(SqlConnection con)
        {
            try
            {
                con.Open();
                string FiledSelection = "PO_Serial,PO_No,Ship_ToRestaurant,Ship_ToKitchen,Vendor_ID,Create_Date,Delivery_Date,WS,Comment,Total_Price,Restaurant_ID,Kitchen_ID,UserID,Status";
                string values = string.Format("'{0}','{1}',{2},{3},(select Code from Vendors where Name = N'{4}'),GETDATE(),'{5}','{6}',N'{7}','{8}',{9},'{10}','{11}','{12}'", Serial_PO_NO.Text, PO_NO.Text, RestaurantCode,KitchenCode, Vendor.Text,Convert.ToDateTime(Delivery_dt.Text).ToString("MM-dd-yyyy"), Classes.WS, commenttxt.Text, Total_Price_With_Tax.Text,RestaurantCode, KitchenCode, MainWindow.UserID, Statustxt.Text);
                Classes.InsertRow("PO", FiledSelection, values);
            }
            catch (Exception ex)    {   MessageBox.Show(ex.ToString());   }
            finally { con.Close(); }
        }   //Done
        private void Edit_PO_Details(SqlConnection con)
        {
            try
            {
                con.Open();
                string FiledSelection = "PO_No,Ship_ToRestaurant,Ship_ToKitchen,Vendor_ID,Delivery_Date,Last_Modified_Date,Comment,Total_Price,Status";
                string Values = string.Format("'{0}',{1},{2},(select Code from Vendors where Name = N'{3}'),'{4}',GETDATE(),N'{5}','{6}','{7}'", PO_NO.Text, RestaurantCode ,KitchenCode, Vendor.Text, Convert.ToDateTime(Delivery_dt.Text).ToString("MM-dd-yyyy"), commenttxt.Text, Total_Price_With_Tax.Text, Statustxt.Text);
                string where = string.Format("PO_Serial={0}", Serial_PO_NO.Text);
                Classes.UpdateRow(FiledSelection, Values, where, "PO");
            }
            catch (Exception ex)     {   MessageBox.Show(ex.ToString());   }
            finally { con.Close(); }
        }    //Done

        //events
        
  
       
        
       
    }
}