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
            else { MessageBox.Show("You Should Logined First !"); LogIn logIn = new LogIn(); logIn.ShowDialog(); }
        }           //Done
        public void LoadAllResturant()
        {
            DataTable Resturants = Classes.RetrieveResturants();
            for (int i = 0; i < Resturants.Rows.Count; i++)
            {
                Outletcbx.Items.Add(Resturants.Rows[i][0]);
            }
        }    //Done
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
        }    //Done
        private void Kitchencbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Outletcbx.Text != "")
            {
                ValOfKitchen = Classes.RetrieveKitchenCode(Kitchencbx.SelectedItem.ToString(), Outletcbx.SelectedItem.ToString());
                TheInventoryDetails.IsEnabled = true;
            }
        }  //Done
        private void TheInventoryDetails_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("StartTheInventory") == -1 && Authenticated.IndexOf("CheckAllPhysicalInventory") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                Blind = NotBlindChx.IsChecked.Value;
                DataTable DT = new DataTable();
                string FiledSelection = "Inventory_ID";
                string Where = string.Format("Inventory_Type='Open' and Resturant_ID={0} and KitchenID={1}", ValOfResturant, ValOfKitchen);
                DT = Classes.RetrieveData(FiledSelection, Where, "PhysicalInventory_tbl");
                if (DT.Rows.Count > 0)
                {
                    MessageBoxResult result = MessageBox.Show("You Hve an Opened Physical Inventory , You wan't to Open It ?", "Confirmation",
                               MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        StartInventory();
                        LoadOpenPhysicalInventory(DT.Rows[0][0].ToString());
                    }
                }
                else
                {
                    if (Outletcbx.Text.Equals(""))
                    { MessageBox.Show("you Shoud Choose The Resturant first !"); return; }
                    else if (Kitchencbx.Text.Equals(""))
                    { MessageBox.Show("you Shoud Choose The Kitchen first !"); return; }
                    DeleteDataAtQtyOnHand();
                    InsertToONHandTable();
                    StartInventory();
                    GetInventoryID();
                    LoadAllItems();
                }
            }
        }   //Done
        private void DeleteDataAtQtyOnHand()
        {
            string Where = string.Format("Resturant_ID={0} and Kitchen_ID={1}", ValOfResturant, ValOfKitchen);
            Classes.DeleteRows(Where, "PhysicalInventory_QtyOnHand");
        }   //Done
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
            { MessageBox.Show(ex.ToString()); }
            finally
            { con.Close(); }

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
        private void LoadAllItems()
        {
            DataTable PhysicalItems = new DataTable();
            DataTable ItemsQtyAndCost = new DataTable();
            DataTable ItemsName = new DataTable();
            string Where = ""; double SumOfCOst = 0;
            if (NotBlindChx.IsChecked == true)
            {
                PhysicalItems.Columns.Add("Code");
                PhysicalItems.Columns.Add("Name");
                PhysicalItems.Columns.Add("Name2");
                PhysicalItems.Columns.Add("Qty");
                PhysicalItems.Columns.Add("Phsycal Qty");
                PhysicalItems.Columns.Add("Variance");
                PhysicalItems.Columns.Add("Cost");
                PhysicalItems.Columns.Add("Total Cost");
                try
                {
                    Where = string.Format("Resturant_ID={0} and Kitchen_ID={1}", ValOfResturant, ValOfKitchen);
                    ItemsQtyAndCost = Classes.RetrieveData("Item_ID,Qty,Cost", Where, "PhysicalInventory_QtyOnHand");
                    for (int i = 0; i < ItemsQtyAndCost.Rows.Count; i++)
                    {
                        Where = string.Format("Code='{0}'", ItemsQtyAndCost.Rows[i][0].ToString());
                        ItemsName = Classes.RetrieveData("Name,Name2", Where, "Setup_Items");
                        double TotalItemsCost = (Convert.ToDouble(ItemsQtyAndCost.Rows[i][1].ToString()) * Convert.ToDouble(ItemsQtyAndCost.Rows[i][2].ToString()));
                        SumOfCOst += TotalItemsCost;
                        PhysicalItems.Rows.Add(ItemsQtyAndCost.Rows[i][0].ToString(), ItemsName.Rows[0][0].ToString(), ItemsName.Rows[0][1].ToString(), ItemsQtyAndCost.Rows[i][1].ToString(),"","", ItemsQtyAndCost.Rows[i][2].ToString(), TotalItemsCost);
                    }
                }
                catch { }
                {
                    for (int i = 0; i < PhysicalItems.Columns.Count; i++)
                    { PhysicalItems.Columns[i].ReadOnly = true;    }
                    PhysicalItems.Columns["Phsycal Qty"].ReadOnly = false;
                }
            }
            else if (NotBlindChx.IsChecked == false)
            {   
                PhysicalItems.Columns.Add("Code");
                PhysicalItems.Columns.Add("Name");
                PhysicalItems.Columns.Add("Name2");
                PhysicalItems.Columns.Add("Qty");
                PhysicalItems.Columns.Add("Cost");
                PhysicalItems.Columns.Add("Total Cost");
                try
                {
                    Where = string.Format("Resturant_ID={0} and Kitchen_ID={1}", ValOfResturant, ValOfKitchen);
                    ItemsQtyAndCost = Classes.RetrieveData("Item_ID,Qty,Cost", Where, "PhysicalInventory_QtyOnHand");
                    for(int i=0;i<ItemsQtyAndCost.Rows.Count;i++)
                    {
                        Where = string.Format("Code='{0}'", ItemsQtyAndCost.Rows[i][0].ToString());
                        ItemsName = Classes.RetrieveData("Name,Name2", Where, "Setup_Items");
                        double TotalItemsCost = (Convert.ToDouble(ItemsQtyAndCost.Rows[i][1].ToString()) * Convert.ToDouble(ItemsQtyAndCost.Rows[i][2].ToString()));
                        SumOfCOst += TotalItemsCost;
                        PhysicalItems.Rows.Add(ItemsQtyAndCost.Rows[i][0].ToString(), ItemsName.Rows[0][0].ToString(), ItemsName.Rows[0][1].ToString(), ItemsQtyAndCost.Rows[i][1].ToString(), ItemsQtyAndCost.Rows[i][2].ToString(), TotalItemsCost);
                    }
                }
                catch {}
                for (int i = 0; i < PhysicalItems.Columns.Count; i++)
                {
                    PhysicalItems.Columns[i].ReadOnly = true;
                }
                PhysicalItems.Columns["Qty"].ReadOnly = false;
            }
            ItemsDGV.DataContext = PhysicalItems;
            NUmberOfItems.Text = PhysicalItems.Rows.Count.ToString();
            Total_Price.Text = SumOfCOst.ToString();
            Blind = (bool)NotBlindChx.IsChecked;
        }   //Done
        private void LoadOpenPhysicalInventory(string PhycialInventoryID)
        {
            DataTable PhysicalInventoryItems = new DataTable();
            DataTable ItemsName = new DataTable();
            string Where = "";string FiledSelection = "";
            DataTable PhysicalInventoryDetails = new DataTable();
            FiledSelection = "Inventory_ID,Inventory_Num,Inventory_Type,Inventory_Date,Comment,Blind";
            Where = string.Format("Inventory_ID='{0}'", PhycialInventoryID);
            PhysicalInventoryDetails = Classes.RetrieveData(FiledSelection, Where, "PhysicalInventory_tbl");
            double SumOfCOst = 0;
            //Loaded Items
            Serial_Inventory_NO.Text = PhysicalInventoryDetails.Rows[0][0].ToString();
            Inventory_NO.Text = PhysicalInventoryDetails.Rows[0][1].ToString();
            Typecbx.Text = PhysicalInventoryDetails.Rows[0][2].ToString();
            InventoryDate.Text = Convert.ToDateTime(PhysicalInventoryDetails.Rows[0][3].ToString()).ToString("dd-MM-yyyy");
            commenttxt.Text = PhysicalInventoryDetails.Rows[0][4].ToString();
            Blind = Convert.ToBoolean(PhysicalInventoryDetails.Rows[0][5].ToString());
            //
            if(Blind == true)
            {
                PhysicalInventoryItems.Columns.Add("Code");
                PhysicalInventoryItems.Columns.Add("Name");
                PhysicalInventoryItems.Columns.Add("Name2");
                PhysicalInventoryItems.Columns.Add("Qty");
                PhysicalInventoryItems.Columns.Add("Phsycal Qty");
                PhysicalInventoryItems.Columns.Add("Variance");
                PhysicalInventoryItems.Columns.Add("Cost");
                PhysicalInventoryItems.Columns.Add("Total Cost");
                try
                {
                    Where = string.Format("Inventory_ID='{0}'", PhycialInventoryID);
                    PhysicalInventoryDetails = Classes.RetrieveData("Item_ID,Qty,InventoryQty,Variance,Cost", Where, "PhysicalInventory_Items");
                    for (int i = 0; i < PhysicalInventoryDetails.Rows.Count; i++)
                    {
                        Where = string.Format("Code='{0}'", PhysicalInventoryDetails.Rows[i][0].ToString());
                        ItemsName = Classes.RetrieveData("Name,Name2", Where, "Setup_Items");
                        double TotalItemsCost = (Convert.ToDouble(PhysicalInventoryDetails.Rows[i][1].ToString()) * Convert.ToDouble(PhysicalInventoryDetails.Rows[i][4].ToString()));
                        SumOfCOst += TotalItemsCost;
                        PhysicalInventoryItems.Rows.Add(PhysicalInventoryDetails.Rows[i][0], ItemsName.Rows[0][0], ItemsName.Rows[0][1], PhysicalInventoryDetails.Rows[i][1], PhysicalInventoryDetails.Rows[i][2], PhysicalInventoryDetails.Rows[i][3], PhysicalInventoryDetails.Rows[i][4], TotalItemsCost);
                    }
                    for (int i = 0; i < PhysicalInventoryItems.Columns.Count; i++)
                    {
                        PhysicalInventoryItems.Columns[i].ReadOnly = true;
                    }
                    PhysicalInventoryItems.Columns["Phsycal Qty"].ReadOnly = false;
                    ItemsDGV.DataContext = PhysicalInventoryItems;
                }
                catch { }
            }
            else
            {
                PhysicalInventoryItems.Columns.Add("Code");
                PhysicalInventoryItems.Columns.Add("Name");
                PhysicalInventoryItems.Columns.Add("Name2");
                PhysicalInventoryItems.Columns.Add("Qty");
                PhysicalInventoryItems.Columns.Add("Cost");
                PhysicalInventoryItems.Columns.Add("Total Cost");
                try
                {
                    Where = string.Format("Inventory_ID='{0}'", PhycialInventoryID);
                    PhysicalInventoryDetails = Classes.RetrieveData("Item_ID,Qty,Cost", Where, "PhysicalInventory_Items");
                    for (int i = 0; i < PhysicalInventoryDetails.Rows.Count; i++)
                    {
                        Where = string.Format("Code='{0}'", PhysicalInventoryDetails.Rows[i][0].ToString());
                        ItemsName = Classes.RetrieveData("Name,Name2", Where, "Setup_Items");
                        double TotalItemsCost = (Convert.ToDouble(PhysicalInventoryDetails.Rows[i][1].ToString()) * Convert.ToDouble(PhysicalInventoryDetails.Rows[i][2].ToString()));
                        SumOfCOst += TotalItemsCost;
                        PhysicalInventoryItems.Rows.Add(PhysicalInventoryDetails.Rows[i][0], ItemsName.Rows[0][0], ItemsName.Rows[0][1], PhysicalInventoryDetails.Rows[i][1], PhysicalInventoryDetails.Rows[i][2], TotalItemsCost);
                    }
                    for (int i = 0; i < PhysicalInventoryItems.Columns.Count; i++)
                    {
                        PhysicalInventoryItems.Columns[i].ReadOnly = true;
                    }
                    PhysicalInventoryItems.Columns["Qty"].ReadOnly = false;
                }
                catch { }
            }
            ItemsDGV.DataContext = PhysicalInventoryItems;
            NUmberOfItems.Text = PhysicalInventoryItems.Rows.Count.ToString();
            Total_Price.Text = SumOfCOst.ToString();
        }   //Done
        private void ItemsDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            double totalPrice = 0;
            DataTable Dt = new DataTable();
            Dt = ItemsDGV.DataContext as DataTable;
            Dt.Columns["Total Cost"].ReadOnly = false;
            try
            {
                if (Blind == true)
                {
                    Dt.Columns["variance"].ReadOnly = false;
                    Dt.Rows[e.Row.GetIndex()]["Variance"] = (double.Parse((e.EditingElement as TextBox).Text) - Convert.ToDouble(Dt.Rows[e.Row.GetIndex()]["Qty"])).ToString();
                    Dt.Rows[e.Row.GetIndex()]["Total Cost"] = (double.Parse((e.EditingElement as TextBox).Text) * Convert.ToDouble(Dt.Rows[e.Row.GetIndex()]["Cost"])).ToString();
                    Dt.Columns["variance"].ReadOnly = true;
                }
                else
                {
                    Dt.Rows[e.Row.GetIndex()]["Total Cost"] = (double.Parse((e.EditingElement as TextBox).Text) * Convert.ToDouble(Dt.Rows[e.Row.GetIndex()]["Cost"])).ToString();
                }
            }
            catch (Exception ex){ MessageBox.Show(ex.ToString()); }
            Dt.Columns["Total Cost"].ReadOnly = true;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                try
                {
                    totalPrice += Convert.ToDouble(Dt.Rows[i]["Total Cost"]);
                }
                catch { }
            }
            NUmberOfItems.Text = Dt.Rows.Count.ToString();
            Total_Price.Text = (totalPrice).ToString();
        }   //Done
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable DT = new DataTable();
                string Where = string.Format("Inventory_Type='Open' and Resturant_ID={0} and KitchenID={1}", ValOfResturant, ValOfKitchen);
                DT = Classes.RetrieveData("Inventory_ID", Where, "PhysicalInventory_tbl");
                if (DT.Rows.Count == 0)
                {
                    Where = string.Format("Resturant_ID={0} and Kitchen_ID={1}", ValOfResturant, ValOfKitchen);
                    Classes.DeleteRows(Where, "PhysicalInventory_QtyOnHand");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            clearData();
            InventoryChose.Visibility = Visibility.Visible;
            InventoryInfo.Visibility = Visibility.Hidden;

        }       
        private void Typecbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Typecbx.Text != "")
            {
                if ((Typecbx.SelectedItem as ComboBoxItem).Content.ToString() == "Closed")
                {
                    SaveBtn.IsEnabled = false;
                    inventory.IsEnabled = true;
                }
                else if ((Typecbx.SelectedItem as ComboBoxItem).Content.ToString() == "Open")
                {
                    SaveBtn.IsEnabled = true;
                    inventory.IsEnabled = false;
                }
            }
        }   //Done
        private void clearData()
        {
            Serial_Inventory_NO.Text = "";
            Inventory_NO.Text = "";
            Typecbx.Text = "";
            InventoryDate.Text = "";
            commenttxt.Text = "";
            ItemsDGV.DataContext = null;
        }
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
            try
            {
                string FiledSelected = ""; string Value = "";
                DataTable IfOpend = new DataTable();
                string Where = string.Format("Inventory_Type='Open' and  Resturant_ID={0} and KitchenID={1}", ValOfResturant, ValOfKitchen);
                IfOpend = Classes.RetrieveData("Blind", Where, "PhysicalInventory_tbl");
                if (IfOpend.Rows.Count > 0)
                {
                    if (IfOpend.Rows[0][0].ToString() == "True")
                    {
                        for (int i = 0; i < ItemsDGV.Items.Count; i++)
                        {
                            FiledSelected = "Qty,InventoryQty,Variance";
                            Value = string.Format("{0},{1},{2}", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5]));
                            Where = string.Format("Inventory_ID='{0}' and Item_ID='{1}'", Serial_Inventory_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]));
                            Classes.UpdateRow(FiledSelected, Value, Where, "PhysicalInventory_Items");
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ItemsDGV.Items.Count; i++)
                        {
                            FiledSelected = "Qty";
                            Value = string.Format("{0}", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]));
                            Where = string.Format("Inventory_ID='{0}' and Item_ID='{1}'", Serial_Inventory_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]));
                            Classes.UpdateRow(FiledSelected, Value, Where, "PhysicalInventory_Items");
                        }
                    }
                    FiledSelected = "Inventory_Num,Inventory_Date,Comment";
                    Value = string.Format("'{0}','{1}','{2}'", Inventory_NO.Text, Convert.ToDateTime(InventoryDate.Text).ToString("MM-dd-yyyy"), commenttxt.Text);
                    Where = string.Format("Inventory_ID='{0}'", Serial_Inventory_NO.Text);
                    Classes.UpdateRow(FiledSelected, Value, Where, "PhysicalInventory_tbl");
                }
                else
                {
                    if (NotBlindChx.IsChecked == true)
                    {
                        for (int i = 0; i < ItemsDGV.Items.Count; i++)
                        {
                            FiledSelected = "Inventory_ID,Item_ID,Qty,InventoryQty,Variance,Cost";
                            Value = string.Format("'{0}','{1}',{2},'{3}','{4}',{5}", Serial_Inventory_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), ((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4], ((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5], Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]));
                            Classes.InsertRow("PhysicalInventory_Items", FiledSelected, Value);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ItemsDGV.Items.Count; i++)
                        {
                            FiledSelected = "Inventory_ID,Item_ID,Qty,Cost";
                            Value = string.Format("'{0}','{1}',{2},{3}", Serial_Inventory_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]));
                            Classes.InsertRow("PhysicalInventory_Items", FiledSelected, Value);
                        }
                    }
                    FiledSelected = "Inventory_ID,Inventory_Num,Inventory_Type,Inventory_Date,Comment,Resturant_ID,KitchenID,Post_Date,UserID,Blind,WS,Create_Date";
                    Value = string.Format("'{0}',{1},'{2}','{3}','{4}',{5},{6},GETDATE(),'{7}','{8}','{9}',GETDATE()", Serial_Inventory_NO.Text, Inventory_NO.Text, Typecbx.Text, Convert.ToDateTime(InventoryDate.Text).ToString("MM-dd-yyyy"), commenttxt.Text, ValOfResturant, ValOfKitchen, MainWindow.UserID, NotBlindChx.IsChecked, Classes.WS);
                    Classes.InsertRow("PhysicalInventory_tbl", FiledSelected, Value);
                }
                MessageBox.Show("The inventory Saved Sucessfully !");
            }
            catch { }
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
            }
        }
    }
}
