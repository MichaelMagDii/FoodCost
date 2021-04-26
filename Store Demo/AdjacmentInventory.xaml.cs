using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for AdjacmentInventory.xaml
    /// </summary>
    public partial class AdjacmentInventory : UserControl
    {
        public string ValOfResturant = "";
        public string ValOfKitchen = "";
        string physicalinventoryID = "";
        List<string> Authenticated = new List<string>();
        bool Blind = false;

        //Adjacment Coming from Physcial Inventory
        public AdjacmentInventory(string ValofRest,string valofKit,string valofOPhiID)
        {
            Authenticated = MainWindow.AuthenticationData["AddjacmentItems"];
            InitializeComponent();
            ValOfResturant = ValofRest;   ValOfKitchen = valofKit;
            Adjact.Visibility = Visibility.Visible;
            NumberOfItemText.Visibility = Visibility.Visible;
            TotalofItems.Visibility = Visibility.Visible;
            NUmberOfItems.Visibility = Visibility.Visible;
            Total_Price.Visibility = Visibility.Visible;
            Adjact.IsEnabled = true;
            adjacChose.Visibility = Visibility.Hidden;
            AdjacInfo.Visibility = Visibility.Visible;
            ItemsDGV.IsReadOnly = true;
            Serial_Adjacment_NO.Text = Classes.InCrementTransactionSerial("Adjacment_tbl", "Adjacment_ID");
            LoadPhysicalInventory(ValofRest,valofKit,valofOPhiID);
        }
        private void LoadPhysicalInventory(string valofResturant,string valofKitchen,string PhiID)
        {
            physicalinventoryID = PhiID;
            int NumOfItems = 0; double totalCost = 0;
            DataTable dt = new DataTable();
            DataTable Dat = new DataTable();
            string FirstName = ""; string SeconName = "";
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            //
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select Inventory_Date,Comment,Blind,Resturant_ID,KitchenID from PhysicalInventory_tbl Where Inventory_ID='{0}'", PhiID), con);
                Dat = new DataTable();
                adapter.Fill(Dat);

                DataRow row = Dat.Rows[0];
                ValOfResturant = row["Resturant_ID"].ToString();
                ValOfKitchen = row["KitchenID"].ToString();
                Reasoncbx.Items.Add("Physical Inventory");
                Reasoncbx.Text = "Physical Inventory";
                Reasoncbx.IsEnabled = false;
                Adjacment_Date.Text = row["Inventory_Date"].ToString();
                commenttxt.Text = row["Comment"].ToString();
                Blind = Convert.ToBoolean(row["Blind"].ToString());
            }
            catch (Exception ex)
            {   MessageBox.Show(ex.ToString());   }
            finally
            {   con.Close();   }
            if (Blind == false)
            {
                dt.Columns.Add("Code");
                dt.Columns.Add("Name");
                dt.Columns.Add("Name2");
                dt.Columns.Add("Qty");
                dt.Columns.Add("Cost");
                try
                {
                    con.Open();
                    string s = "select Item_ID,Qty,Cost from PhysicalInventory_Items Where  Inventory_ID=" + PhiID;
                    cmd = new SqlCommand(s, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            con2.Open();
                            string q = "SELECT Name,Name2 From Setup_Items Where Code='" + reader["Item_ID"].ToString() + "'";
                            cmd2 = new SqlCommand(q, con2);
                            reader2 = cmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                FirstName = reader2["Name"].ToString();
                                SeconName = reader2["Name2"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            con2.Close();
                        }
                        NumOfItems++;
                        totalCost += Convert.ToDouble(reader["Cost"].ToString());
                        dt.Rows.Add(reader["Item_ID"].ToString(), FirstName, SeconName, reader["Qty"].ToString(), reader["Cost"]);
                    }
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dt.Columns[i].ReadOnly = true;
                    }

                    ItemsDGV.DataContext = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                NUmberOfItems.Text = NumOfItems.ToString();
                Total_Price.Text = totalCost.ToString();
            }
            else
            {
                dt.Columns.Add("Code");
                dt.Columns.Add("Name");
                dt.Columns.Add("Name2");
                dt.Columns.Add("Qty");
                dt.Columns.Add("Phsycal Qty");
                dt.Columns.Add("Variance");
                dt.Columns.Add("Cost");
                try
                {
                    con.Open();
                    string s = "select Item_ID,Qty,InventoryQty,Variance,Cost from PhysicalInventory_Items Where  Inventory_ID=" + PhiID;
                    cmd = new SqlCommand(s, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            con2.Open();
                            string q = "SELECT Name,Name2 From Setup_Items Where Code='" + reader["Item_ID"].ToString() + "'";
                            cmd2 = new SqlCommand(q, con2);
                            reader2 = cmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                FirstName = reader2["Name"].ToString();
                                SeconName = reader2["Name2"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            con2.Close();
                        }
                        NumOfItems++;
                        totalCost += Convert.ToDouble(reader["Cost"].ToString());
                        dt.Rows.Add(reader["Item_ID"].ToString(), FirstName, SeconName, reader["Qty"].ToString(), reader["InventoryQty"].ToString(), reader["Variance"].ToString(), reader["Cost"]);
                    }
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dt.Columns[i].ReadOnly = true;
                    }

                    ItemsDGV.DataContext = dt;
                }
                catch (Exception ex)
                {  MessageBox.Show(ex.ToString());   }
                finally
                {  con.Close();  }
                NUmberOfItems.Text = NumOfItems.ToString();
                Total_Price.Text = totalCost.ToString();
            }
          
            con.Close();
        }

        // Normal Adjacment 
        DataTable dt = new DataTable();
        public AdjacmentInventory()
        {
            if (MainWindow.AuthenticationData.Count != 0)
            {
                if (MainWindow.AuthenticationData.ContainsKey("AddjacmentItems"))
                {
                    Authenticated = MainWindow.AuthenticationData["AddjacmentItems"];
                    if (Authenticated.Count == 0)
                    {
                        MessageBox.Show("You Havent a Privilage to Open this Page");
                        LogIn logIn = new LogIn();
                        logIn.ShowDialog();
                    }
                    else
                    {
                        InitializeComponent();
                        LoadAllResturant();
                    }
                }
            }
            else { MessageBox.Show(" You Should Logined First !"); LogIn logIn = new LogIn();   logIn.ShowDialog(); }
        }           //Done  
        public void LoadAllResturant()
        {
            DataTable Resturants = Classes.RetrieveResturants();
            for(int i=0;i<Resturants.Rows.Count;i++)
            {
                Outletcbx.Items.Add(Resturants.Rows[i][0]);
            }
        }       //Done
        private void ResturantComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Outletcbx.SelectedItem != null)
            {
                ValOfResturant = Classes.RetrieveRestaurantCode(Outletcbx.SelectedItem.ToString());
                Kitchencbx.Items.Clear();
                DataTable Kitchens = Classes.RetrieveKitchens(Outletcbx.SelectedItem.ToString());
                for (int i = 0; i < Kitchens.Rows.Count; i++)
                {
                    Kitchencbx.Items.Add(Kitchens.Rows[i][0]);
                }
            }
        }       //Done
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Kitchencbx.SelectedItem != null)
            {
                ValOfKitchen = Classes.RetrieveKitchenCode(Kitchencbx.SelectedItem.ToString(),Outletcbx.SelectedItem.ToString());
                Adjact.Visibility = Visibility.Visible;
                NumberOfItemText.Visibility = Visibility.Visible;
                TotalofItems.Visibility = Visibility.Visible;
                NUmberOfItems.Visibility = Visibility.Visible;
                Total_Price.Visibility = Visibility.Visible;
                Adjact.IsEnabled = true;
                adjacChose.Visibility = Visibility.Hidden;
                AdjacInfo.Visibility = Visibility.Visible;
                addItemBtn.Visibility = Visibility.Visible;
                RemoveItemBtn.Visibility = Visibility.Visible;
                LoadAllReasons();
                Serial_Adjacment_NO.Text = Classes.InCrementTransactionSerial("Adjacment_tbl", "Adjacment_ID");
            }
        }       //Done
        private void LoadAllReasons()
        {
            DataTable TheReasons = Classes.RetrieveData("Name", "Active='True'", "Setup_AdjacmentReasons_tbl");
            for(int i=0;i<TheReasons.Rows.Count;i++)
            {
                Reasoncbx.Items.Add(TheReasons.Rows[i][0].ToString());
            }
        }   //Done  
        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("AddItemAddjacment") == -1 && Authenticated.IndexOf("CheckAllAddjacment") == -1)
            {  LogIn logIn = new LogIn();  logIn.ShowDialog();  }
            else
            {
                ValOfResturant = ValOfResturant;
                ValOfKitchen = ValOfKitchen;
                Items itemswindow = new Items(this);
                itemswindow.ShowDialog();
            }
        }       //Done
        private void RemoveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("DeleteAddjacment") == -1 && Authenticated.IndexOf("CheckAllAddjacment") == -1)
            { LogIn logIn = new LogIn();  logIn.ShowDialog();   }
            else
            {
                //DataGrid grid = sender as DataGrid;
                //int codeTodelete = grid.SelectedIndex;
                //if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                //{
                //    DataTable dt = new DataTable();
                //    dt = ((DataView)ItemsDGV.ItemsSource).ToTable();
                //    dt.Rows.RemoveAt(codeTodelete);
                //    ItemsDGV.DataContext = dt;
                //}
            }
        }       //Done
        private void ItemsDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            double totalPrice = 0;
            dt = ItemsDGV.DataContext as DataTable;
            dt.Columns["Variance"].ReadOnly = false;
            if (e.Column.Header == "Adjacmentable Qty")
            {
                dt.Rows[e.Row.GetIndex()]["Variance"] = (Double.Parse(((e.EditingElement as TextBox).Text).ToString()) - Convert.ToDouble(dt.Rows[e.Row.GetIndex()]["Qty"]));
                dt.Columns["Adjacmentable Qty"].ReadOnly = false;
                dt.Columns["Variance"].ReadOnly = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        totalPrice += Convert.ToDouble(dt.Rows[i]["Cost"]);
                    }   catch { }
                }
                NUmberOfItems.Text = dt.Rows.Count.ToString();
                Total_Price.Text = (totalPrice).ToString();
            }
        }               //Done
        private bool DoSomeChecks()
        {
            bool Complete = true;
            if (ItemsDGV.Items.Count == 0)
            {
                MessageBox.Show("First You should Select Items !"); Complete =false;
            }
            if (Serial_Adjacment_NO.Text.Equals(""))
            {
                MessageBox.Show("First You should Enter The Serial !"); Complete = false;
            }
            if (Adjacment_NO.Text.Equals(""))
            {
                MessageBox.Show("First You should Enter The Manual Number !"); Complete = false;
            }
            if (Reasoncbx.Text.Equals(""))
            {
                MessageBox.Show("First You should Choose The Reason !"); Complete =false;
            }
            if (Adjacment_Date.Text.Equals(""))
            {
                MessageBox.Show("First You should Enter The Date !"); Complete =false;
            }
            return Complete;
        }
        private void Adjact_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("AdjacmentAdjacment") == -1 && Authenticated.IndexOf("CheckAllAddjacment") == -1)
            {   LogIn logIn = new LogIn();  logIn.ShowDialog();  }
            else
            {
                if (Reasoncbx.Text != "Physical Inventory")
                {
                    if (!DoSomeChecks())
                        return;

                    SqlConnection con = new SqlConnection(Classes.DataConnString);
                    try
                    {
                        string FiledSelection = "Adjacment_ID,Adjacment_Num,Adjacment_Reason,Adjacment_Date,Comment,Resturant_ID,KitchenID,Create_Date,Post_Date,User_ID,WS,Total_Cost";
                        string Values = string.Format("'{0}',{1},(select Code From Setup_AdjacmentReasons_tbl where Name='{2}'),'{3}','{4}',{5},{6},GETDATE(),GETDATE(),'{7}','{8}','{9}'", Serial_Adjacment_NO.Text, Adjacment_NO.Text, Reasoncbx.Text, Convert.ToDateTime(Adjacment_Date.Text).ToString("MM-dd-yyyy"), commenttxt.Text, ValOfResturant, ValOfKitchen, MainWindow.UserID, Classes.WS, Total_Price.Text);
                        Classes.InsertRow("Adjacment_tbl", FiledSelection, Values);
                    }
                    catch (Exception ex)
                    {   MessageBox.Show(ex.ToString());  }

                    try
                    {
                        for (int i = 0; i < ItemsDGV.Items.Count; i++)
                        {
                            string FiledSelection = "Adjacment_ID,Item_ID,Qty,AdjacmentableQty,Variance,Cost";
                            string Values = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}'", Serial_Adjacment_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]));
                            Classes.InsertRow("Adjacment_Items", FiledSelection, Values);
                        }
                    }
                    catch (Exception ex)
                    {   MessageBox.Show(ex.ToString());   }
                    con.Open();
                    try
                    {
                        for (int i = 0; i < ItemsDGV.Items.Count; i++)
                        {
                            string H = string.Format("Update Items set Qty={0}, Current_Cost={4}, Net_Cost=({4} * {0}) where ItemID = '{1}' and RestaurantID ={2} and KitchenID={3}", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), ValOfResturant, ValOfKitchen, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]));
                            SqlCommand cmd = new SqlCommand(H, con);
                            cmd.ExecuteNonQuery();
                            
                            H = string.Format("update ItemsYear set {0}={1},{2}={3} where ItemID='{4}' and Restaurant_ID='{5}' and Kitchen_ID='{6}' and Year='{7}'",MainWindow.MonthQty, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), MainWindow.MonthCost, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]), (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), ValOfResturant, ValOfKitchen, MainWindow.CurrentYear);
                            cmd = new SqlCommand(H, con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {  MessageBox.Show(ex.ToString());   }
                    finally
                    {  MessageBox.Show("Edited Successfully");  }
                }
                else
                {
                    SaveThePhyscialAdjacment();
                }
                Adjact.IsEnabled = false;
            }
        }
        private void SaveThePhyscialAdjacment()
        {
            if (!DoSomeChecks())
                return;

            SqlConnection con = new SqlConnection(Classes.DataConnString);
            con.Open();
            if (Blind == false)
            {
                try
                {
                    string FiledSelection = "Adjacment_ID,Adjacment_Num,Adjacment_Reason,Adjacment_Date,Comment,Resturant_ID,KitchenID,Create_Date,Post_Date,User_ID,WS,Total_Cost";
                    string Values = string.Format("'{0}',{1},(select Code From Setup_AdjacmentReasons_tbl where Name='{2}'),'{3}','{4}',{5},{6},GETDATE(),GETDATE(),'{7}','{8}','{9}'", Serial_Adjacment_NO.Text, Adjacment_NO.Text, Reasoncbx.Text, Convert.ToDateTime(Adjacment_Date.Text).ToString("MM-dd-yyyy"), commenttxt.Text, ValOfResturant, ValOfKitchen, MainWindow.UserID, Classes.WS,Total_Price.Text);
                    Classes.InsertRow("Adjacment_tbl", FiledSelection, Values);
                }
                catch (Exception ex)
                {  MessageBox.Show(ex.ToString()); }

                try
                {
                    for (int i = 0; i < ItemsDGV.Items.Count; i++)
                    {
                        string FiledSelection = "Adjacment_ID,Item_ID,Qty,Cost";
                        string Values = string.Format("'{0}','{1}','{2}','{3}'", Serial_Adjacment_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]));
                        Classes.InsertRow("Adjacment_Items", FiledSelection, Values);
                    }
                }
                catch (Exception ex)
                {  MessageBox.Show(ex.ToString());  }

                try
                {
                    for (int i = 0; i < ItemsDGV.Items.Count; i++)
                    {
                        string H = string.Format("Update Items set Qty={0}, Current_Cost={4}, Net_Cost=({4} * {0}) where ItemID = '{1}' and RestaurantID ={2} and KitchenID={3}", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), ValOfResturant, ValOfKitchen, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]));
                        SqlCommand cmd = new SqlCommand(H, con);
                        cmd.ExecuteNonQuery();

                        H = string.Format("update ItemsYear set {0}={1},{2}={3} where ItemID='{4}' and Restaurant_ID='{5}' and Kitchen_ID='{6}' and Year='{7}'", MainWindow.MonthQty, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), MainWindow.MonthCost, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), ValOfResturant, ValOfKitchen, MainWindow.CurrentYear);
                        cmd = new SqlCommand(H, con);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());   }
                finally
                {  MessageBox.Show("Edited Successfully");  }
            }
            else
            {
                try
                {
                    string FiledSelection = "Adjacment_ID,Adjacment_Num,Adjacment_Reason,Adjacment_Date,Comment,Resturant_ID,KitchenID,Create_Date,Post_Date,User_ID,WS,Total_Cost";
                    string Values = string.Format("'{0}',{1},(select Code From Setup_AdjacmentReasons_tbl where Name='{2}'),'{3}','{4}',{5},{6},GETDATE(),GETDATE(),'{7}','{8}','{9}'", Serial_Adjacment_NO.Text, Adjacment_NO.Text, Reasoncbx.Text, Convert.ToDateTime(Adjacment_Date.Text).ToString("MM-dd-yyyy"), commenttxt.Text, ValOfResturant, ValOfKitchen, MainWindow.UserID, Classes.WS,Total_Price.Text);
                    Classes.InsertRow("Adjacment_tbl", FiledSelection, Values);
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());   }

                try
                {
                    for (int i = 0; i < ItemsDGV.Items.Count; i++)
                    {
                        string FiledSelection = "Adjacment_ID,Item_ID,Qty,AdjacmentableQty,Variance,Cost";
                        string Values = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}'", Serial_Adjacment_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), ((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4], ((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5], Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]));
                        Classes.InsertRow("Adjacment_Items", FiledSelection, Values);
                    }
                }
                catch (Exception ex)
                {  MessageBox.Show(ex.ToString());  }

                try
                {
                    for (int i = 0; i < ItemsDGV.Items.Count; i++)
                    {
                        string H = string.Format("Update Items set Qty={0}, Current_Cost={4}, Net_Cost=({4} * {0}) where ItemID = '{1}' and RestaurantID ={2} and KitchenID={3}", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), ValOfResturant, ValOfKitchen, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]));
                        SqlCommand cmd = new SqlCommand(H, con);
                        cmd.ExecuteNonQuery();

                        H = string.Format("update ItemsYear set {0}={1},{2}={3} where ItemID='{4}' and Restaurant_ID='{5}' and Kitchen_ID='{6}' and Year='{7}'", MainWindow.MonthQty, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), MainWindow.MonthCost, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]), (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), ValOfResturant, ValOfKitchen, MainWindow.CurrentYear);
                        cmd = new SqlCommand(H, con);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {  MessageBox.Show(ex.ToString()); }
                finally
                {  MessageBox.Show("Edited Successfully"); }
            }

            try
            {
                string s = string.Format("update PhysicalInventory_tbl set Inventory_Type='Closed' where Inventory_ID={0}", physicalinventoryID);
                SqlCommand cmd = new SqlCommand(s, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }       //Done
        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }       //Done

    }
}
