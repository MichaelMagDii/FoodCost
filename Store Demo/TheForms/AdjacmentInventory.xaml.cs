using Food_Cost.TheForms;
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
        List<string> Authenticated = new List<string>();
        public string ValOfResturant = "";
        public string ValOfKitchen = "";
        string physicalinventoryID = "";        // When Return Physical Inventory ID    
        bool Blind = false;                     // Physical Inventory is BLind of Not Blind
        DataTable dt = new DataTable();
        //NORMAL ADJCUMENT
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
            else { MessageBox.Show(" You Should Logined First !"); LogIn logIn = new LogIn(); logIn.ShowDialog(); }
        }           //Done  
        public void LoadAllResturant()
        {
            DataTable Resturants = Classes.RetrieveResturants();
            for (int i = 0; i < Resturants.Rows.Count; i++)
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
                ValOfKitchen = Classes.RetrieveKitchenCode(Kitchencbx.SelectedItem.ToString(), Outletcbx.SelectedItem.ToString());
                Adjact.Visibility = Visibility.Visible;
                NumberOfItemText.Visibility = Visibility.Visible;
                TotalofItems.Visibility = Visibility.Visible;
                NUmberOfItems.Visibility = Visibility.Visible;
                Total_Price.Visibility = Visibility.Visible;
                Adjact.IsEnabled = true;
                adjacChose.Visibility = Visibility.Hidden;
                AdjacInfo.Visibility = Visibility.Visible;
                addItemBtn.Visibility = Visibility.Visible;
                addRecipeBtn.Visibility = Visibility.Visible;
                RemoveItemBtn.Visibility = Visibility.Visible;
                RemoveItemBtn.IsEnabled = true;
                SaveBtn.Visibility = Visibility.Visible;
                searchBtn.Visibility = Visibility.Visible;
                addRecipeBtn.IsEnabled = true;
                LoadAllReasons();
                Serial_Adjacment_NO.Text = Classes.InCrementTransactionSerial("Adjacment_tbl", "Adjacment_ID");
            }
        }       //Done
        private void LoadAllReasons()
        {
            DataTable TheReasons = Classes.RetrieveData("Name", "Active='True'", "Setup_AdjacmentReasons_tbl");
            for (int i = 0; i < TheReasons.Rows.Count; i++)
            {
                Reasoncbx.Items.Add(TheReasons.Rows[i][0].ToString());
            }
        }   //Done 
        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("AddItemAddjacment") == -1 && Authenticated.IndexOf("CheckAllAddjacment") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                Items itemswindow = new Items(this);
                itemswindow.ShowDialog();
            }
        }       //Done
        private void addRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("AddItemAddjacment") == -1 && Authenticated.IndexOf("CheckAllAddjacment") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                AllRecipes allRecipes = new AllRecipes(this);
                allRecipes.ShowDialog();
            }
        }       //DOne
        private void RemoveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("DeleteAddjacment") == -1 && Authenticated.IndexOf("CheckAllAddjacment") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                DataTable ToDelete = ItemsDGV.DataContext as DataTable;
                ToDelete.Rows.RemoveAt(ItemsDGV.SelectedIndex);
                ItemsDGV.DataContext = ToDelete;
            }
        }       //Done
        private bool DoSomeChecks()
        {
            bool Complete = true;
            if (ItemsDGV.Items.Count == 0)
            {
                MessageBox.Show("First You should Select Items !"); Complete = false;
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
                MessageBox.Show("First You should Choose The Reason !"); Complete = false;
            }
            if (Adjacment_Date.Text.Equals(""))
            {
                MessageBox.Show("First You should Enter The Date !"); Complete = false;
            }
            for (int i = 0; i < ItemsDGV.Items.Count; i++)
            {
                if (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4].ToString() == "")
                {
                    MessageBox.Show("Please First Check The Data !"); Complete = false;
                    return Complete;
                }
            }
            return Complete;
        }           //Done
        private void ItemsDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                double totalPrice = 0;
                dt = ItemsDGV.DataContext as DataTable;
                dt.Columns["Total Cost"].ReadOnly = false;
                if (e.Column.Header.ToString() == "Adjacmentable Qty")
                {
                    dt.Columns["Variance"].ReadOnly = false;
                    dt.Rows[e.Row.GetIndex()]["Variance"] = (Double.Parse(((e.EditingElement as TextBox).Text).ToString()) - Convert.ToDouble(dt.Rows[e.Row.GetIndex()]["Qty"]));
                    dt.Rows[e.Row.GetIndex()]["Total Cost"] = (Double.Parse(((e.EditingElement as TextBox).Text).ToString()) * Convert.ToDouble(dt.Rows[e.Row.GetIndex()]["Cost"]));
                    dt.Columns["Variance"].ReadOnly = true;
                }
                else if (e.Column.Header.ToString() == "Cost")
                {
                    try
                    { dt.Rows[e.Row.GetIndex()]["Total Cost"] = (Double.Parse(((e.EditingElement as TextBox).Text).ToString()) * Convert.ToDouble(dt.Rows[e.Row.GetIndex()]["Adjacmentable Qty"])); }
                    catch { dt.Rows[e.Row.GetIndex()]["Total Cost"] = (Double.Parse(((e.EditingElement as TextBox).Text).ToString()) * Convert.ToDouble(dt.Rows[e.Row.GetIndex()]["Qty"])); }
                }
                dt.Columns["Total Cost"].ReadOnly = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    { totalPrice += Convert.ToDouble(dt.Rows[i]["Total Cost"]); }
                    catch { }
                }
                NUmberOfItems.Text = dt.Rows.Count.ToString();
                Total_Price.Text = (totalPrice).ToString();
            }
            catch { }
        }       //Done
        private bool CheckIfTransactionExist()
        {
            bool IsExist;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            con.Open();
            string s = string.Format("Select Adjacment_ID from Adjacment_tbl where Adjacment_ID='{0}'", Serial_Adjacment_NO.Text);
            SqlCommand cmd = new SqlCommand(s, con);
            if (cmd.ExecuteScalar() == null)
            {
                IsExist = false;
            }
            else
            {
                IsExist = true;
            }
            return IsExist;
        }           //Done
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!DoSomeChecks())
                return;

            Save_Changes();
            MessageBox.Show("Saved successfully");
        }           //Done
        private void Adjact_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("AdjacmentAdjacment") == -1 && Authenticated.IndexOf("CheckAllAddjacment") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                if (Reasoncbx.Text != "Physical Inventory")
                {
                    if (!DoSomeChecks())
                        return;

                    Save_Changes();
                    SqlConnection con = new SqlConnection(Classes.DataConnString);
                    con.Open();
                    try
                    {

                        string H = string.Format("Update Adjacment_tbl set Post='True' where Adjacment_ID = '{0}'", Serial_Adjacment_NO.Text);
                        SqlCommand cmd = new SqlCommand(H, con);
                        cmd.ExecuteNonQuery();

                        for (int i = 0; i < ItemsDGV.Items.Count; i++)
                        {
                            if ((bool)((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[9] == false)
                            {
                                H = string.Format("Update Items set Qty={0}, Current_Cost={4}, Net_Cost=({4} * {0}) where ItemID = '{1}' and RestaurantID ={2} and KitchenID={3}", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), ValOfResturant, ValOfKitchen, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]));
                                cmd = new SqlCommand(H, con);
                                cmd.ExecuteNonQuery();

                                H = string.Format("update ItemsYear set {0}={1},{2}={3} where ItemID='{4}' and Restaurant_ID='{5}' and Kitchen_ID='{6}' and Year='{7}'", MainWindow.MonthQty, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), MainWindow.MonthCost, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]), (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), ValOfResturant, ValOfKitchen, MainWindow.CurrentYear);
                                cmd = new SqlCommand(H, con);
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                H = string.Format("Update RecipeQty set Qty={0}, Price={4} where Recipe_ID = '{1}' and Resturant_ID ={2} and Kitchen_ID={3}", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), ValOfResturant, ValOfKitchen, Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]));
                                cmd = new SqlCommand(H, con);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.ToString()); }
                    finally
                    { MessageBox.Show("Edited Successfully"); }
                }
                else
                {
                    SaveThePhyscialAdjacment();
                }
                Adjact.IsEnabled = false;
                searchBtn.IsEnabled = false;
                addItemBtn.IsEnabled = false;
                RemoveItemBtn.IsEnabled = false;
                SaveBtn.IsEnabled = false;
                addRecipeBtn.IsEnabled = false;

            }
        }           //DOne
        private void Save_Changes()
        {
            if (!DoSomeChecks())
                return;

            if (CheckIfTransactionExist() == true)
            {
                string Where = string.Format("Adjacment_ID='{0}'", Serial_Adjacment_NO.Text);
                Classes.DeleteRows(Where, "Adjacment_Items");

                try
                {
                    for (int i = 0; i < ItemsDGV.Items.Count; i++)
                    {

                        string FiledSelection = "Adjacment_ID,Item_ID,Qty,AdjacmentableQty,Variance,Cost,Recipe";
                        string Values = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}'", Serial_Adjacment_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]), (bool)((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[9]);
                        Classes.InsertRow("Adjacment_Items", FiledSelection, Values);
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }
                Classes.LogTable(Classes.MyComm.CommandText.ToString(), Serial_Adjacment_NO.Text, "Adjacment_tbl", "Update");


            }
            else
            {
                try
                {
                    string FiledSelection = "Adjacment_ID,Adjacment_Num,Adjacment_Reason,Adjacment_Date,Comment,Resturant_ID,KitchenID,Create_Date,User_ID,WS,Total_Cost";
                    string Values = string.Format("'{0}',{1},(select Code From Setup_AdjacmentReasons_tbl where Name='{2}'),'{3}','{4}',{5},{6},GETDATE(),'{7}','{8}','{9}'", Serial_Adjacment_NO.Text, Adjacment_NO.Text, Reasoncbx.Text, Convert.ToDateTime(Adjacment_Date.Text).ToString("MM-dd-yyyy"), commenttxt.Text, ValOfResturant, ValOfKitchen, MainWindow.UserID, Classes.WS, Total_Price.Text);
                    Classes.InsertRow("Adjacment_tbl", FiledSelection, Values);
                }
                catch (Exception ex)

                { MessageBox.Show(ex.ToString()); }

                try
                {
                    for (int i = 0; i < ItemsDGV.Items.Count; i++)
                    {
                        string FiledSelection = "Adjacment_ID,Item_ID,Qty,AdjacmentableQty,Variance,Cost,Recipe";
                        string Values = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}'", Serial_Adjacment_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]), (bool)((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[9]);
                        Classes.InsertRow("Adjacment_Items", FiledSelection, Values);
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }
                Classes.LogTable(Classes.MyComm.CommandText.ToString(), Serial_Adjacment_NO.Text, "Adjacment_tbl", "New");

            }
        }           //Done 
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            AllAdjacments all_Adjacments = new AllAdjacments(this);
            all_Adjacments.ShowDialog();
        }           //Done

        //Physcial Inventory Load and Functions 
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
                    string Values = string.Format("'{0}',{1},(select Code From Setup_AdjacmentReasons_tbl where Name='{2}'),'{3}','{4}',{5},{6},GETDATE(),GETDATE(),'{7}','{8}','{9}'", Serial_Adjacment_NO.Text, Adjacment_NO.Text, Reasoncbx.Text, Convert.ToDateTime(Adjacment_Date.Text).ToString("MM-dd-yyyy"), commenttxt.Text, ValOfResturant, ValOfKitchen, MainWindow.UserID, Classes.WS, Total_Price.Text);
                    Classes.InsertRow("Adjacment_tbl", FiledSelection, Values);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                try
                {
                    for (int i = 0; i < ItemsDGV.Items.Count; i++)
                    {
                        string FiledSelection = "Adjacment_ID,Item_ID,Qty,AdjacmentableQty,Variance,Cost";
                        string Values = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}'", Serial_Adjacment_NO.Text, (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]), "0", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]));
                        Classes.InsertRow("Adjacment_Items", FiledSelection, Values);
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

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
                { MessageBox.Show(ex.ToString()); }
                finally
                { MessageBox.Show("Edited Successfully"); }
            }
            else
            {
                try
                {
                    string FiledSelection = "Adjacment_ID,Adjacment_Num,Adjacment_Reason,Adjacment_Date,Comment,Resturant_ID,KitchenID,Create_Date,Post_Date,User_ID,WS,Total_Cost";
                    string Values = string.Format("'{0}',{1},(select Code From Setup_AdjacmentReasons_tbl where Name='{2}'),'{3}','{4}',{5},{6},GETDATE(),GETDATE(),'{7}','{8}','{9}'", Serial_Adjacment_NO.Text, Adjacment_NO.Text, Reasoncbx.Text, Convert.ToDateTime(Adjacment_Date.Text).ToString("MM-dd-yyyy"), commenttxt.Text, ValOfResturant, ValOfKitchen, MainWindow.UserID, Classes.WS, Total_Price.Text);
                    Classes.InsertRow("Adjacment_tbl", FiledSelection, Values);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

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
                { MessageBox.Show(ex.ToString()); }

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
                { MessageBox.Show(ex.ToString()); }
                finally
                { MessageBox.Show("Edited Successfully"); }
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

        //


        //Adjacment Coming from Physcial Inventory
        public AdjacmentInventory(string ValofRest,string valofKit,string valofOPhiID)
        {
            Authenticated = MainWindow.AuthenticationData["AddjacmentItems"];
            InitializeComponent();
            ValOfResturant = ValofRest;   ValOfKitchen = valofKit;
            NumberOfItemText.Visibility = Visibility.Visible;
            TotalofItems.Visibility = Visibility.Visible;
            NUmberOfItems.Visibility = Visibility.Visible;
            Total_Price.Visibility = Visibility.Visible;
            Adjact.IsEnabled = true;
            adjacChose.Visibility = Visibility.Hidden;
            AdjacInfo.Visibility = Visibility.Visible;
            Adjact.Visibility = Visibility.Visible;
            ItemsDGV.IsReadOnly = true;
            Serial_Adjacment_NO.Text = Classes.InCrementTransactionSerial("Adjacment_tbl", "Adjacment_ID");
            LoadPhysicalInventory(ValofRest,valofKit,valofOPhiID);
        }
        private void LoadPhysicalInventory(string valofResturant,string valofKitchen,string PhiID)
        {
            //Michael 
            physicalinventoryID = PhiID;
            string Where = "";double TotalCost = 0;
            DataTable PhysicalDetailsData = new DataTable();
            DataTable AdjacmentDetails = new DataTable();
            DataTable ItemsQtyAndCost = new DataTable();
            DataTable ItemsName = new DataTable();
            Where = string.Format("Inventory_ID='{0}'", PhiID);
            PhysicalDetailsData = Classes.RetrieveData("Inventory_Date,Comment,Blind,Resturant_ID,KitchenID", Where, "PhysicalInventory_tbl");
            ValOfResturant =PhysicalDetailsData.Rows[0][3].ToString();
            ValOfKitchen = PhysicalDetailsData.Rows[0][4].ToString();
            Reasoncbx.Items.Add("Physical Inventory");
            Reasoncbx.Text = "Physical Inventory";
            Reasoncbx.IsEnabled = false;
            Adjacment_Date.Text = PhysicalDetailsData.Rows[0][0].ToString();
            commenttxt.Text = PhysicalDetailsData.Rows[0][1].ToString();
            Blind = Convert.ToBoolean(PhysicalDetailsData.Rows[0][2].ToString());
            if (Blind == true)
            {
                AdjacmentDetails.Columns.Add("Code");
                AdjacmentDetails.Columns.Add("Name");
                AdjacmentDetails.Columns.Add("Name2");
                AdjacmentDetails.Columns.Add("Qty");
                AdjacmentDetails.Columns.Add("Phsycal Qty");
                AdjacmentDetails.Columns.Add("Variance");
                AdjacmentDetails.Columns.Add("Cost");
                AdjacmentDetails.Columns.Add("Total Cost");
                try
                {
                    Where = string.Format("Inventory_ID='{0}'", PhiID);
                    ItemsQtyAndCost = Classes.RetrieveData("Item_ID,Qty,InventoryQty,Variance,Cost", Where, "PhysicalInventory_Items");
                    for (int i = 0; i < ItemsQtyAndCost.Rows.Count; i++)
                    {
                        Where = string.Format("Code='{0}'", ItemsQtyAndCost.Rows[i][0].ToString());
                        ItemsName = Classes.RetrieveData("Name,Name2", Where, "Setup_Items");
                        double TotalItemsCost = (Convert.ToDouble(ItemsQtyAndCost.Rows[i][1].ToString()) * Convert.ToDouble(ItemsQtyAndCost.Rows[i][4].ToString()));
                        TotalCost += TotalItemsCost;
                        AdjacmentDetails.Rows.Add(ItemsQtyAndCost.Rows[i][0], ItemsName.Rows[0][0], ItemsName.Rows[0][1], ItemsQtyAndCost.Rows[i][1], ItemsQtyAndCost.Rows[i][2], ItemsQtyAndCost.Rows[i][3], ItemsQtyAndCost.Rows[i][4], TotalItemsCost);
                    }
                    for (int i = 0; i < AdjacmentDetails.Columns.Count; i++)
                    {
                        AdjacmentDetails.Columns[i].ReadOnly = true;
                    }
                    AdjacmentDetails.Columns["Phsycal Qty"].ReadOnly = false;
                }
                catch { }
            }
            else
            {
                AdjacmentDetails.Columns.Add("Code");
                AdjacmentDetails.Columns.Add("Name");
                AdjacmentDetails.Columns.Add("Name2");
                AdjacmentDetails.Columns.Add("Qty");
                AdjacmentDetails.Columns.Add("Cost");
                AdjacmentDetails.Columns.Add("Total Cost");
                try
                {
                    Where = string.Format("Inventory_ID='{0}'", PhiID);
                    ItemsQtyAndCost = Classes.RetrieveData("Item_ID,Qty,Cost", Where, "PhysicalInventory_Items");
                    for (int i = 0; i < ItemsQtyAndCost.Rows.Count; i++)
                    {
                        Where = string.Format("Code='{0}'", ItemsQtyAndCost.Rows[i][0].ToString());
                        ItemsName = Classes.RetrieveData("Name,Name2", Where, "Setup_Items");
                        double TotalItemsCost = (Convert.ToDouble(ItemsQtyAndCost.Rows[i][1].ToString()) * Convert.ToDouble(ItemsQtyAndCost.Rows[i][2].ToString()));
                        TotalCost += TotalItemsCost;
                        AdjacmentDetails.Rows.Add(ItemsQtyAndCost.Rows[i][0], ItemsName.Rows[0][0], ItemsName.Rows[0][1], ItemsQtyAndCost.Rows[i][1], ItemsQtyAndCost.Rows[i][2], TotalItemsCost);
                    }
                    for (int i = 0; i < AdjacmentDetails.Columns.Count; i++)
                    {
                        AdjacmentDetails.Columns[i].ReadOnly = true;
                    }
                    AdjacmentDetails.Columns["Qty"].ReadOnly = false;
                }
                catch { }
            }
            ItemsDGV.DataContext = AdjacmentDetails;
            NUmberOfItems.Text = AdjacmentDetails.Rows.Count.ToString();
            Total_Price.Text = TotalCost.ToString();
        }

       
    }
}
