using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class PhysicalInventory : UserControl
    {
        public string ValOfResturant = "";
        public string ValOfKitchen = "";
        public string Valoftype = "";
        List<string> Authenticated = new List<string>();
        bool Blind = false;

        public PhysicalInventory()
        {
            if (MainWindow.AuthenticationData.Count != 0)
            {
                if (MainWindow.AuthenticationData.ContainsKey("PhysicalInventory"))
                {
                    Authenticated = MainWindow.AuthenticationData["PhysicalInventory"];
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
            else { MessageBox.Show("You Should Logined First !"); LogIn logIn = new LogIn(); logIn.ShowDialog();  }
        }
        public void LoadAllResturant()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "select Name from Setup_Restaurant where IsActive='True'";
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var data = reader["Name"].ToString();
                    Outletcbx.Items.Add(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                reader.Close();
                con.Close();
            }
        }    //Done
        private void ResturantComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kitchencbx.Items.Clear();
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "select Name from Setup_Kitchens Where IsActive='True' and RestaurantID=(select Code From Setup_Restaurant Where Name='" + Outletcbx.SelectedItem.ToString() + "')";
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var data = reader["Name"].ToString();
                    Kitchencbx.Items.Add(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                reader.Close();
                con.Close();
            }
        }    //Done
        private void Kitchencbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TheInventoryDetails.IsEnabled = true;
        }  //Done
        private void GetCodeofResturantAndKitchen()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.Open();
                string s = "SELECT Code FROM Setup_Restaurant Where Name='" + Outletcbx.SelectedItem.ToString() + "'";
                cmd = new SqlCommand(s, con);
                ValOfResturant = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }


            try
            {
                con.Open();
                string s = "SELECT Code FROM Setup_Kitchens Where Name='" + Kitchencbx.SelectedItem.ToString() + "'";
                cmd = new SqlCommand(s, con);
                ValOfKitchen = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }  //Done
        private void InsertToONHandTable()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("MakeAnPhysicalInventory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ResturantID", ValOfResturant);
                cmd.Parameters.AddWithValue("@KitchenID", ValOfKitchen);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }

        }   //Done
        private void StartInventory()
        {
            inventory.Visibility = Visibility.Visible;
            NumberOfItemText.Visibility = Visibility.Visible;
            NUmberOfItems.Visibility = Visibility.Visible;
            TotalofItems.Visibility = Visibility.Visible;
            Total_Price.Visibility = Visibility.Visible;
            searchBtn.Visibility = Visibility.Visible;
            InventoryChose.Visibility = Visibility.Hidden;
            UndoBtn.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Visible;
            InventoryInfo.Visibility = Visibility.Visible;
        }   //Done
        private void GetInventoryID()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = string.Format("Select TOP(1) Inventory_ID From PhysicalInventory_tbl where Inventory_ID like '{0}%' ORDER BY Inventory_ID DESC", Classes.IDs);
                SqlCommand cmd = new SqlCommand(s, con);
                if (cmd.ExecuteScalar() == null)
                {
                    Serial_Inventory_NO.Text = Classes.IDs + "0000001";
                }
                else
                {
                    Serial_Inventory_NO.Text = "0" + (Int64.Parse(cmd.ExecuteScalar().ToString()) + 1).ToString();
                }
                con.Close();
            }
            catch { }
        }  //Done
        private void TheInventoryDetails_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("StartTheInventory") == -1 && Authenticated.IndexOf("CheckAllPhysicalInventory") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                try
                {
                    con.Open();
                    string s = string.Format("select Inventory_ID,Inventory_Type From PhysicalInventory_tbl where Inventory_Type='Open'");
                    SqlCommand cmd = new SqlCommand(s, con);
                    if (cmd.ExecuteScalar() != null)
                    {
                        MessageBoxResult result = MessageBox.Show("You Hve an Opened Physical Inventory , You wan't to Open It ?", "Confirmation",
                                 MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.OK)
                        {
                            string W = string.Format("select Top(1)Inventory_ID From PhysicalInventory_tbl ORDER BY Inventory_ID DESC");
                            cmd = new SqlCommand(W, con);
                            StartInventory();
                            LoadOpenPhysicalInventory(cmd.ExecuteScalar().ToString());
                        }
                    }
                    else
                    {
                        if (Outletcbx.Text.Equals(""))
                        {
                            MessageBox.Show("you Shoud Choose The Resturant first !");
                            return;
                        }
                        else if (Kitchencbx.Text.Equals(""))
                        {
                            MessageBox.Show("you Shoud Choose The Kitchen first !");
                            return;
                        }
                        DeleteDataAtQtyOnHand();
                        GetCodeofResturantAndKitchen();
                        InsertToONHandTable();
                        StartInventory();
                        GetInventoryID();
                        LoadAllItems();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                con.Close();
            }

        }   //Done
        private void DeleteDataAtQtyOnHand()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = "delete PhysicalInventory_QtyOnHand";
                SqlCommand cmd = new SqlCommand(s, con);
                cmd.ExecuteNonQuery();
            }
            catch { }
        }   //Done
        private void LoadAllItems()
        {
            int NumberOfItems = 0; double SumOfCOst = 0;
            DataTable dt = new DataTable();
            if (NotBlindChx.IsChecked == true)
            {
                dt.Columns.Add("Code");
                dt.Columns.Add("Name");
                dt.Columns.Add("Name2");
                dt.Columns.Add("Qty");
                dt.Columns.Add("Phsycal Qty");
                dt.Columns.Add("Variance");
                dt.Columns.Add("Cost");
                string FirstName = ""; string SeconName = "";
                SqlDataReader reader = null;
                SqlDataReader reader2 = null;
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                SqlConnection con2 = new SqlConnection(Classes.DataConnString);
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                try
                {
                    con.Open();
                    string s = "select Item_ID,Qty,Cost FROM PhysicalInventory_QtyOnHand where Resturant_ID=" + ValOfResturant + " and Kitchen_ID=" + ValOfKitchen;
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
                        NumberOfItems++;
                        SumOfCOst += Convert.ToDouble(reader["Cost"].ToString());
                        dt.Rows.Add(reader["Item_ID"].ToString(), FirstName, SeconName, reader["Qty"].ToString(), "", "", reader["Cost"]);
                    }
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dt.Columns[i].ReadOnly = true;
                    }
                    dt.Columns["Phsycal Qty"].ReadOnly = false;
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
            }
            else if (NotBlindChx.IsChecked == false)
            {
                dt.Columns.Add("Code");
                dt.Columns.Add("Name");
                dt.Columns.Add("Name2");
                dt.Columns.Add("Qty");
                dt.Columns.Add("Cost");
                string FirstName = ""; string SeconName = "";
                SqlDataReader reader = null;
                SqlDataReader reader2 = null;
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                SqlConnection con2 = new SqlConnection(Classes.DataConnString);
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                try
                {
                    con.Open();
                    string s = "select Item_ID,Qty,Cost FROM PhysicalInventory_QtyOnHand where Resturant_ID=" + ValOfResturant + " and Kitchen_ID=" + ValOfKitchen;
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
                        NumberOfItems++;
                        SumOfCOst += Convert.ToDouble(reader["Cost"].ToString());
                        dt.Rows.Add(reader["Item_ID"].ToString(), FirstName, SeconName, reader["Qty"].ToString(), reader["Cost"]);
                    }
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dt.Columns[i].ReadOnly = true;
                    }
                    dt.Columns["Qty"].ReadOnly = false;

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
            }
            NUmberOfItems.Text = NumberOfItems.ToString();
            Total_Price.Text = SumOfCOst.ToString();
            Blind =(bool)NotBlindChx.IsChecked;
        }   //Done

        private void LoadOpenPhysicalInventory(string PhycialInventoryID)
        {
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
                SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select Inventory_ID,Inventory_Num,Inventory_Type,Inventory_Date,Comment,Blind from PhysicalInventory_tbl Where Inventory_ID='{0}'", PhycialInventoryID), con);
                Dat = new DataTable();
                adapter.Fill(Dat);

                DataRow row = Dat.Rows[0];
                Serial_Inventory_NO.Text = row["Inventory_ID"].ToString();
                Inventory_NO.Text = row["Inventory_Num"].ToString();
                Typecbx.Text = row["Inventory_Type"].ToString();
                InventoryDate.Text = Convert.ToDateTime(row["Inventory_Date"]).ToString("dd-MM-yyyy");
                commenttxt.Text = row["Comment"].ToString();
                Blind = Convert.ToBoolean(row["Blind"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            if (Blind == true)
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
                    string s = "select Item_ID,Qty,InventoryQty,Variance,Cost from PhysicalInventory_Items Where  Inventory_ID=" + PhycialInventoryID;
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
                        dt.Rows.Add(reader["Item_ID"].ToString(), FirstName, SeconName, reader["Qty"].ToString(), reader["InventoryQty"], reader["Variance"], reader["Cost"]);
                    }
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dt.Columns[i].ReadOnly = true;
                    }
                    dt.Columns["Phsycal Qty"].ReadOnly = false;

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
                dt.Columns.Add("Cost");
                try
                {
                    con.Open();
                    string s = "select Item_ID,Qty,Cost from PhysicalInventory_Items Where  Inventory_ID=" + PhycialInventoryID;
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
                    dt.Columns["Qty"].ReadOnly = false;

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

        }   //Done

        private void ItemsDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int CountOfBindItems = 0;
            int CountOfChecked = 0;
            int CountOfComment = 0;
            DataTable dt = new DataTable();
            dt = ItemsDGV.DataContext as DataTable;
            if (NotBlindChx.IsChecked == true)
            {
                int CountOfItems = 0;
                try
                {
                    dt.Columns["Variance"].ReadOnly = false;
                    dt = ((DataView)ItemsDGV.ItemsSource).ToTable();
                    if (e.Column.Header == "Phsycal Qty")
                    {
                        try
                        {
                            (ItemsDGV.SelectedItem as DataRowView).Row[5] = (double.Parse((e.EditingElement as TextBox).Text) - Convert.ToDouble((ItemsDGV.SelectedItem as DataRowView).Row.ItemArray[3]) ).ToString();
                        }
                        catch { }
                    }
                    dt.Columns["Phsycal Qty"].ReadOnly = false;
                    dt.Columns["Variance"].ReadOnly = true;
                }
                catch { }
            }  
        }   //Done
        
       /* private void BackUpData()
        {
            string Path = System.IO.File.ReadAllText("BackUpData.txt");
            SqlConnection con = new SqlConnection(connString);
            string folderName = Path + "\\" + "FoodCost.bak";
            con.Open();
            SqlCommand cmd = new SqlCommand("BackUpDataBase", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", folderName);
            cmd.ExecuteNonQuery();
            con.Close();
        }*/

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("SaveTheInventory") == -1 && Authenticated.IndexOf("CheckAllPhysicalInventory") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                if (Serial_Inventory_NO.Text.Equals(""))
                {
                    MessageBox.Show("First You should Enter The Serial !");
                    return;
                }
                if (Inventory_NO.Text.Equals(""))
                {
                    MessageBox.Show("First You should Enter The Manual Number !");
                    return;
                }
                if (Typecbx.Text.Equals(""))
                {
                    MessageBox.Show("First You should Choose The Type !");
                    return;
                }
                if (InventoryDate.Text.Equals(""))
                {
                    MessageBox.Show("First You should Choose The Date !");
                    return;
                }
                SaveInfoANdData();
            }
        }          //Done

        private void SaveInfoANdData()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            try
            {
                con2.Open();
                string s = string.Format("select Blind From PhysicalInventory_tbl where Inventory_Type='Open'");
                SqlCommand cmd2 = new SqlCommand(s, con2);
                if (cmd2.ExecuteScalar() != null)
                {
                    if (cmd2.ExecuteScalar().ToString() == "True")
                    {
                        try
                        {
                            con.Open();
                            for (int i = 0; i < ItemsDGV.Items.Count; i++)
                            {
                                s = string.Format("update PhysicalInventory_Items set Qty={0},InventoryQty={1},Variance={2} where Inventory_ID={3} and Item_ID={4}", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5]), Serial_Inventory_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]));
                                SqlCommand cmd = new SqlCommand(s, con);
                                cmd.ExecuteNonQuery();
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
                    }
                    else if (cmd2.ExecuteScalar().ToString() == "False")
                    {
                        try
                        {
                            con.Open();
                            for (int i = 0; i < ItemsDGV.Items.Count; i++)
                            {
                                s = string.Format("update PhysicalInventory_Items set Qty={0} where Inventory_ID={1} and Item_ID={2}", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), Serial_Inventory_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]));
                                SqlCommand cmd = new SqlCommand(s, con);
                                cmd.ExecuteNonQuery();
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
                    }
                    try
                    {
                        con.Open();
                        s = string.Format("update PhysicalInventory_tbl set Inventory_Num={0},Inventory_Date='{1}',Comment='{2}' where Inventory_ID='{3}'", Inventory_NO.Text,Convert.ToDateTime(InventoryDate.Text).ToString("MM-dd-yyyy"), commenttxt.Text, Serial_Inventory_NO.Text);
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
                        MessageBox.Show("The inventory Saved Sucessfully !");
                    }
                }
                else
                {
                    if (NotBlindChx.IsChecked == true)
                    {
                        try
                        {
                            con.Open();
                            for (int i = 0; i < ItemsDGV.Items.Count; i++)
                            {
                                s = "Insert into PhysicalInventory_Items(Inventory_ID,Item_ID,Qty,InventoryQty,Variance,Cost) Values ( " + Serial_Inventory_NO.Text + ",'" + (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]) + "'," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]) + ")";
                                SqlCommand cmd = new SqlCommand(s, con);
                                cmd.ExecuteNonQuery();
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
                    }
                    else if (NotBlindChx.IsChecked == false)
                    {
                        try
                        {
                            con.Open();
                            for (int i = 0; i < ItemsDGV.Items.Count; i++)
                            {
                                s = "Insert into PhysicalInventory_Items(Inventory_ID,Item_ID,Qty,Cost) Values (' " + Serial_Inventory_NO.Text + "','" + (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]) + "'," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]) + ")";
                                SqlCommand cmd = new SqlCommand(s, con);
                                cmd.ExecuteNonQuery();
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
                    }
                    try
                    {
                        con.Open();
                        s = string.Format("insert into PhysicalInventory_tbl(Inventory_ID,Inventory_Num,Inventory_Type,Inventory_Date,Comment,Resturant_ID,KitchenID,Post_Date,UserID,Blind,WS,Create_Date) values('{0}',{1},'{2}','{3}','{4}',{5},{6},GETDATE(),'{7}','{8}','{9}',GETDATE())", Serial_Inventory_NO.Text, Inventory_NO.Text, Typecbx.Text,Convert.ToDateTime(InventoryDate.Text).ToString("MM-dd-yyyy"), commenttxt.Text, ValOfResturant, ValOfKitchen, MainWindow.UserID, NotBlindChx.IsChecked,Classes.WS);
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
                        MessageBox.Show("The inventory Saved Sucessfully !");
                    }
                }
            }
            catch { }
        }
        
        private void Typecbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Typecbx.Text !="")
            {
               if((Typecbx.SelectedItem as ComboBoxItem).Content.ToString() == "Closed")
                {
                    SaveBtn.IsEnabled = false;
                    inventory.IsEnabled = true;
                }
               else if((Typecbx.SelectedItem as ComboBoxItem).Content.ToString() == "Open")
                {
                    SaveBtn.IsEnabled = true;
                    inventory.IsEnabled = false;
                }
            }
        }   //Done

        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = string.Format("select Inventory_ID,Inventory_Type From PhysicalInventory_tbl where Inventory_Type='Open'");
                SqlCommand cmd = new SqlCommand(s, con);
                if (cmd.ExecuteScalar() != null)
                {
                    clearData();
                }
                else
                {
                    s = string.Format("Delete PhysicalInventory_QtyOnHand");
                    SqlCommand cmd2 = new SqlCommand(s, con);
                    cmd2.ExecuteNonQuery();
                    clearData();
                    InventoryChose.Visibility = Visibility.Visible;
                    InventoryInfo.Visibility = Visibility.Hidden;
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
            finally
            {
                con.Close();
                InventoryChose.Visibility = Visibility.Visible;
                InventoryInfo.Visibility = Visibility.Hidden;
            }
        }

        private void clearData()
        {
            Serial_Inventory_NO.Text = "";
            Inventory_NO.Text = "";
            Typecbx.Text = "";
            InventoryDate.Text = "";
            commenttxt.Text = "";
            ItemsDGV.DataContext = null;
        }

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("InventoryPhysicalInventory") == -1 && Authenticated.IndexOf("CheckAllPhysicalInventory") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                SaveInfoANdData();
                UserControl usc = new Food_Cost.AdjacmentInventory(ValOfResturant, ValOfKitchen, Serial_Inventory_NO.Text);
                TheMainGrid.Children.Clear();
                TheMainGrid.Children.Add(usc);


                /* if (Serial_Inventory_NO.Text.Equals(""))
                 {
                     MessageBox.Show("First You should Enter The Serial !");
                     return;
                 }
                 if (Inventory_NO.Text.Equals(""))
                 {
                     MessageBox.Show("First You should Enter The Manual Number !");
                     return;
                 }
                 if (Typecbx.Text.Equals(""))
                 {
                     MessageBox.Show("First You should Choose The Type !");
                     return;
                 }
                 if (InventoryDate.Text.Equals(""))
                 {
                     MessageBox.Show("First You should Choose The Date !");
                     return;
                 }

                 SqlConnection con = new SqlConnection(connString);
                 if (NotBlindChx.IsChecked == false)
                 {
                     try
                     {
                         con.Open();
                         for (int i = 0; i < ItemsDGV.Items.Count; i++)
                         {
                             string s = "Insert into PhysicalInventory_Items(Inventory_ID,Item_ID,Qty,InventoryQty,Variance,Cost) Values ( " + Serial_Inventory_NO.Text + ",'" + (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]) + "'," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]) + ")";
                             SqlCommand cmd = new SqlCommand(s, con);
                             cmd.ExecuteNonQuery();
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

                 }
                 else if (NotBlindChx.IsChecked == true)
                 {
                     try
                     {
                         con.Open();
                         for (int i = 0; i < ItemsDGV.Items.Count; i++)
                         {
                             string s = "Insert into PhysicalInventory_Items(Inventory_ID,Item_ID,Qty,Cost) Values ( " + Serial_Inventory_NO.Text + ",'" + (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]) + "'," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]) + "," + (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5]).ToString() + ")";
                             SqlCommand cmd = new SqlCommand(s, con);
                             cmd.ExecuteNonQuery();
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


                 }
                 try
                 {
                     con.Open();
                     string s = string.Format("insert into PhysicalInventory_tbl(Inventory_ID,Inventory_Num,Inventory_Type,Inventory_Date,Comment,Resturant_ID,KitchenID,Post_Date,UserID,Blind) values({0},{1},'{2}',{3},'{4}',{5},'{6}',GETDATE(),{7},{8})", Serial_Inventory_NO.Text, Inventory_NO.Text, Typecbx.Text, InventoryDate.Text, commenttxt.Text, ValOfResturant, ValOfKitchen, MainWindow.UserID, NotBlindChx.IsChecked);
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
                 }
             }*/
            }
        }
    }
}
