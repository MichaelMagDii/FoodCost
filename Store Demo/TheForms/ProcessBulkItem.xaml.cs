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
    /// Interaction logic for ProcessBulkItem.xaml
    /// </summary>
    public partial class ProcessBulkItem : UserControl
    {
        List<string> Authenticated = new List<string>();
        string CodeOfResturant = "";
        string CodeOfKitchens = "";
        public ProcessBulkItem()
        {
            if (MainWindow.AuthenticationData.ContainsKey("ProcessBulk"))
            {
                Authenticated = MainWindow.AuthenticationData["ProcessBulk"];
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
        }       //Doen Finall Function
        private void LoadAllResturant()
        {
            DataTable Restaurants = Classes.RetrieveResturants();
            for (int i = 0; i < Restaurants.Rows.Count; i++)
            {
                StoreIDcbx.Items.Add(Restaurants.Rows[i][0].ToString());
            }
        }           //Doen Finall Function
        private void ResturantComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StoreIDcbx.SelectedItem != null)
            {
                Kitchencbx.Items.Clear();
                CodeOfResturant = Classes.RetrieveRestaurantCode(StoreIDcbx.SelectedItem.ToString());
                DataTable Kitchens = Classes.RetrieveKitchens(StoreIDcbx.SelectedItem.ToString());
                for (int i = 0; i < Kitchens.Rows.Count; i++)
                {
                    Kitchencbx.Items.Add(Kitchens.Rows[i][0].ToString());
                }
            }
        }               //Doen Finall Function
        private void kitchenComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CodeOfKitchens = Classes.RetrieveKitchenCode(Kitchencbx.SelectedItem.ToString(), StoreIDcbx.SelectedItem.ToString());
            LoadAllBulkItems();
            Details.Visibility = Visibility.Hidden;
            ItemsDetails.Visibility = Visibility.Visible;
        }           //Doen Finall Function
        private void LoadAllBulkItems()
        {
            string Where = "";
            DataTable DT = new DataTable();
            DT.Columns.Add("Code");
            DT.Columns.Add("Manual Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Qty");
            DT.Columns.Add("Unit");
            DT.Columns.Add("Cost");
            DataTable ItemQtyCost = new DataTable();
            DataTable ItemsInfo = new DataTable();
            ItemsInfo = Classes.RetrieveData("Code,[Manual Code],Name,Unit,weight", "Is_BulkItem='true'", "Setup_Items");
            for (int i = 0; i < ItemsInfo.Rows.Count; i++)
            {
                Where = string.Format("ItemID='{0}' and RestaurantID='{1}' and KitchenID='{2}'", ItemsInfo.Rows[i][0].ToString(), CodeOfResturant, CodeOfKitchens);
                ItemQtyCost = Classes.RetrieveData("Qty,Current_Cost", Where, "Items");
                if (ItemQtyCost.Rows.Count > 0)
                {
                    if (Convert.ToDouble(ItemQtyCost.Rows[0][0].ToString()) > 0 && Convert.ToDouble(ItemQtyCost.Rows[0][1].ToString()) > 0)
                    {
                        DT.Rows.Add(ItemsInfo.Rows[i][0], ItemsInfo.Rows[i][1], ItemsInfo.Rows[i][2], (Convert.ToDouble(ItemQtyCost.Rows[0][0]) * Convert.ToDouble(ItemsInfo.Rows[i][4])), ItemsInfo.Rows[i][3], ItemQtyCost.Rows[0][1]);
                    }
                }
            }
            for (int i = 0; i < ItemsInfo.Columns.Count; i++)
            {
                DT.Columns[i].ReadOnly = true;
            }
            ItemsDGV.DataContext = DT;
        }           //Doen Finall Function
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0.-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }  //Doen Finall Functione
        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }       //Doen Finall Function

        private void ItemsDGV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Michael's Update
            double Cost = 0, Weight = 0, _Qty = 0, _Cost = 0;
            string Where = "";
            DataTable ItemsInfo = new DataTable();
            DataTable BulkItems = new DataTable();
            DataTable DT = new DataTable();
            DT.Columns.Add("Code");
            DT.Columns.Add("Manual Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Weight Precentage");
            DT.Columns.Add("Cost Precentage");
            DT.Columns.Add("Weight");
            DT.Columns.Add("Cost");
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                Where = string.Format("Item_Code='{0}'", ((DataRowView)grid.SelectedItem).Row.ItemArray[0]);
                BulkItems = Classes.RetrieveData("Code,WeightPrecentage,CostPrecentage", Where, "Setup_BulkItems");
                for (int i = 0; i < BulkItems.Rows.Count; i++)
                {
                    Where = string.Format("Code='{0}'", BulkItems.Rows[i][0]);
                    ItemsInfo = Classes.RetrieveData("[Manual Code],Name", Where, "Setup_Items");
                    _Qty = Convert.ToDouble(((DataRowView)grid.SelectedItem).Row.ItemArray[3]);
                    _Cost = Convert.ToDouble(((DataRowView)grid.SelectedItem).Row.ItemArray[5]);
                    Cost = ((Convert.ToDouble(BulkItems.Rows[i][2].ToString()) * _Cost) / 100);
                    Weight = ((Convert.ToDouble(BulkItems.Rows[i][1].ToString()) * _Qty) / 100);
                    DT.Rows.Add(BulkItems.Rows[i][0], ItemsInfo.Rows[0][0], ItemsInfo.Rows[0][1], BulkItems.Rows[i][1].ToString(), BulkItems.Rows[i][2].ToString(), Weight, Cost);
                    ItemsofBulkItemsDGV.Visibility = Visibility.Visible;
                }
            }
            else
            {
                ItemsofBulkItemsDGV.Visibility = Visibility.Hidden; return;
            }
            for (int i = 0; i < DT.Columns.Count; i++)
            {
                DT.Columns[i].ReadOnly = true;
            }
            DT.Columns["Weight Precentage"].ReadOnly = false;
            DT.Columns["Cost Precentage"].ReadOnly = false;
            DT.Columns["Weight"].ReadOnly = false;
            DT.Columns["Cost"].ReadOnly = false;

            ItemsofBulkItemsDGV.DataContext = DT;

        }       //Doen Finall Function
        private void ItemsofBulkItemsDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataTable DT = new DataTable();
            DT = ItemsofBulkItemsDGV.DataContext as DataTable;
            double BaseWeight = 0, BaseCost = 0, WeightPrecentage = 0, CostPresentage = 0, Cost = 0, Weight = 0;
            BaseWeight = Convert.ToDouble(((DataRowView)ItemsDGV.SelectedItem).Row.ItemArray[3]);
            BaseCost = Convert.ToDouble(((DataRowView)ItemsDGV.SelectedItem).Row.ItemArray[5]);
            if (e.Column.Header.ToString() == "Weight Precentage")
            {
                WeightPrecentage = Convert.ToDouble((e.EditingElement as TextBox).Text);
                DT.Rows[e.Row.GetIndex()]["Weight"] = ((WeightPrecentage * BaseWeight) / 100).ToString();
            }
            else if (e.Column.Header.ToString() == "Cost Precentage")
            {
                CostPresentage = Convert.ToDouble((e.EditingElement as TextBox).Text);
                DT.Rows[e.Row.GetIndex()]["Cost"] = ((CostPresentage * BaseCost) / 100).ToString();
            }
            else if (e.Column.Header.ToString() == "Weight")
            {
                Weight = Convert.ToDouble((e.EditingElement as TextBox).Text);
                DT.Rows[e.Row.GetIndex()]["Weight Precentage"] = ((Weight * 100) / BaseWeight).ToString();
            }
            else if (e.Column.Header.ToString() == "Cost")
            {
                Cost = Convert.ToDouble((e.EditingElement as TextBox).Text);
                DT.Rows[e.Row.GetIndex()]["Cost Precentage"] = ((Cost * 100) / BaseCost).ToString();
            }
        }
        private bool CheckToSave()
        {

            double TotalCostPrecentage = 0, TotalWeightPrecentage = 0;
            DataTable DT = new DataTable();
            DT = ItemsofBulkItemsDGV.DataContext as DataTable;
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                TotalWeightPrecentage += Convert.ToDouble(DT.Rows[i]["Weight Precentage"]);
                TotalCostPrecentage += Convert.ToDouble(DT.Rows[i]["Cost Precentage"]);
            }
            if (TotalWeightPrecentage != 100 || TotalCostPrecentage != 100)
            {
                MessageBox.Show("Please Check the Data First !!");
                return false;
            }
            return true;
        }
        private void BulkItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            //Michael's Update
            if (Authenticated.IndexOf("DoProcessBulk") == -1 && Authenticated.IndexOf("CheckAllBulk") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                if (CheckToSave())
                {
                    try 
                    {
                        string BulkItemID = "", BulkItemWeight = "", BulkItemCost = "", w = "";
                        BulkItemID = ((DataRowView)ItemsDGV.SelectedItem).Row.ItemArray[0].ToString();
                        BulkItemWeight = ((DataRowView)ItemsDGV.SelectedItem).Row.ItemArray[3].ToString();
                        BulkItemCost = ((DataRowView)ItemsDGV.SelectedItem).Row.ItemArray[5].ToString();
                        SqlConnection con = new SqlConnection(Classes.DataConnString);
                        SqlCommand cmd = new SqlCommand();
                        DataTable DT = new DataTable();
                        DT = ItemsofBulkItemsDGV.DataContext as DataTable;
                        string ID = Classes.InCrementTransactionSerial("Process_BulkItems", "ProcessBulk_ID");
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            con.Open();
                            w = string.Format("UPDATE Items set Qty=Qty+{0},Last_Cost=Current_Cost,Current_Cost=(((Qty*Current_Cost)+({0}*{4}))/(Qty+{0})) where ItemID='{1}' and RestaurantID={2} and KitchenID={3}", DT.Rows[i]["Weight"], DT.Rows[i]["Code"], CodeOfResturant, CodeOfKitchens, DT.Rows[i]["Cost"]);
                            cmd = new SqlCommand(w, con);
                            int n = cmd.ExecuteNonQuery();
                            if (n == 0)
                            {
                                w = string.Format("insert into Items(RestaurantID,KitchenID,ItemID,Qty,Current_Cost,Net_Cost) values({0},{1},'{2}',{3},{4},{5})", CodeOfResturant, CodeOfKitchens, DT.Rows[i]["Code"], DT.Rows[i]["Weight"], DT.Rows[i]["Cost"], Convert.ToDouble(DT.Rows[i]["Cost"]) * Convert.ToDouble(DT.Rows[i]["Weight"]));
                                cmd = new SqlCommand(w, con);
                                cmd.ExecuteNonQuery();
                            }

                            w = string.Format("Update ItemsYear set {0}={0}+{2},{1}=(({0}*{1})+({2}*{3})/({0}+{1})) where ItemID='{4}' and Restaurant_ID='{5}' and Kitchen_ID='{6}' and Year='{7}'", MainWindow.MonthQty, MainWindow.MonthCost, DT.Rows[i]["Weight"], DT.Rows[i]["Cost"], DT.Rows[i]["Code"], CodeOfResturant, CodeOfKitchens, MainWindow.CurrentYear);
                            cmd = new SqlCommand(w, con);
                            n = cmd.ExecuteNonQuery();
                            if (n == 0)
                            {
                                w = string.Format("insert into ItemsYear(ItemID,Restaurant_ID,Kitchen_ID,Year,{0},{1}) values('{2}','{3}','{4}','{5}',{6},{7})", MainWindow.MonthQty, MainWindow.MonthCost, DT.Rows[i]["Code"], CodeOfResturant, CodeOfKitchens, MainWindow.CurrentYear, DT.Rows[i]["Weight"], DT.Rows[i]["Cost"]);
                                cmd = new SqlCommand(w, con);
                                cmd.ExecuteNonQuery();
                            }

                            w = string.Format("insert into Process_BulkItems_Items(ProcessBulk_ID,ParentItem_ID,ParentQty,ParentCost,ChiledItem_ID,ChiledQty,ChiledCost) values('{0}','{1}',{2},{3},'{4}',{5},{6})", ID, BulkItemID, BulkItemWeight, BulkItemCost, DT.Rows[i]["Code"], DT.Rows[i]["Weight"], DT.Rows[i]["Cost"]);
                            cmd = new SqlCommand(w, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        con.Open();
                        w = string.Format("update Items set Qty=0 where RestaurantID={0} and KitchenID={1} and ItemID='{2}' ", CodeOfResturant, CodeOfKitchens, BulkItemID);
                        cmd = new SqlCommand(w, con);
                        cmd.ExecuteNonQuery();

                        w = string.Format("update ItemsYear set {0}={1} Where ItemID='{2}' and Restaurant_ID='{3}' and Kitchen_ID='{4}' and Year='{5}'", MainWindow.MonthQty, BulkItemWeight, BulkItemID, CodeOfResturant, CodeOfKitchens, MainWindow.CurrentYear);
                        cmd = new SqlCommand(w, con);
                        cmd.ExecuteNonQuery();

                        w = string.Format("insert into Process_BulkItems(ProcessBulk_ID,Process_Date,User_ID,Resturant_ID,KitchenID,Post_Date) values('{0}',GETDATE(),'{1}',{2},{3},GETDATE())", ID, MainWindow.UserID, CodeOfResturant, CodeOfKitchens);
                        cmd = new SqlCommand(w, con);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }
            }
        }           //Doen Finall Function

        
    }
}
