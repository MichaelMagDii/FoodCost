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
        }
        private void LoadAllResturant()
        {
            DataTable Restaurants = Classes.RetrieveResturants();
            for(int i=0;i<Restaurants.Rows.Count;i++)
            {
                StoreIDcbx.Items.Add(Restaurants.Rows[i][0].ToString());
            }
        }
        private void ResturantComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StoreIDcbx.SelectedItem !=null)
            {
                Kitchencbx.Items.Clear();
                CodeOfResturant = Classes.RetrieveRestaurantCode(StoreIDcbx.SelectedItem.ToString());
                DataTable Kitchens = Classes.RetrieveKitchens(StoreIDcbx.SelectedItem.ToString());
                for(int i=0;i<Kitchens.Rows.Count;i++)
                {
                    Kitchencbx.Items.Add(Kitchens.Rows[i][0].ToString());
                }
            }            
        }     
        private void kitchenComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CodeOfKitchens = Classes.RetrieveKitchenCode(Kitchencbx.SelectedItem.ToString(),StoreIDcbx.SelectedItem.ToString());
            LoadAllBulkItems();
            Details.Visibility = Visibility.Hidden;
            ItemsDetails.Visibility = Visibility.Visible;
        }
        private void LoadAllBulkItems()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Checked",typeof(bool));
            dt.Columns.Add("Code");
            dt.Columns.Add("Manual Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Cost");
            
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd2 = new SqlCommand();
            SqlDataReader reader2 = null;
            try
            {
                con.Open();
                string s = "select Code,[Manual Code],Name,Unit,weight FROM Setup_Items where Is_BulkItem='true'";
                cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        con2.Open();
                        string q = string.Format("select Qty,Current_Cost FROM Items Where ItemID='{0}' and RestaurantID='{1}' and KitchenID='{2}'", reader["Code"].ToString(), CodeOfResturant, CodeOfKitchens);
                        cmd2 = new SqlCommand(q, con2);
                        reader2 = cmd2.ExecuteReader();
                        reader2.Read();
                        if(reader2.HasRows == true)
                        {
                            if(reader2["Qty"] !="" && Convert.ToDouble(reader2["Qty"].ToString()) >0)
                            {
                                dt.Rows.Add(false, reader["Code"], reader.GetValue(1), reader["Name"], (Convert.ToDouble(reader2["Qty"]) * Convert.ToDouble(reader["weight"])).ToString(),reader["Unit"],reader2["Current_Cost"]);

                            }

                        }
                    }
                    catch (Exception ex)
                    {  MessageBox.Show(ex.ToString());  }
                    finally
                    { con2.Close();  }
                }
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = true;
                }
                dt.Columns["Checked"].ReadOnly = false;
                ItemsDGV.DataContext = dt;
            }
            catch (Exception ex)
            {  MessageBox.Show(ex.ToString());    }
            finally
            {  con.Close();  }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }  //Done
        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }  //Done

        private void ItemsDGV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataTable Dat = new DataTable();
            Dat.Columns.Add("Code");
            Dat.Columns.Add("Manual Code");
            Dat.Columns.Add("Name");
            Dat.Columns.Add("Weight Precentage");
            Dat.Columns.Add("Cost Precentage");  

            ItemsofBulkItemsDGV.Visibility = Visibility.Visible;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd2 = new SqlCommand();
            SqlDataReader reader2 = null;
            DataTable dt = new DataTable();
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                try
                {
                    con.Open();
                    string s = string.Format("select Code,WeightPrecentage,CostPrecentage from Setup_BulkItems where Item_Code='{0}'", ((DataRowView)grid.SelectedItem).Row.ItemArray[1]);
                    cmd = new SqlCommand(s, con);
                    reader = cmd.ExecuteReader();
                    con2.Open();
                    while(reader.Read())
                    {
                        s = string.Format("select [Manual Code],Name From Setup_Items Where Code='{0}'", reader["Code"]);
                        cmd2 = new SqlCommand(s, con2);
                        reader2 = cmd2.ExecuteReader();
                        reader2.Read();
                        {
                            Dat.Rows.Add(reader["Code"], reader2["Manual Code"], reader2["Name"], reader["WeightPrecentage"] +"  %", reader["CostPrecentage"]+"  %");
                        }
                        reader2.Close();
                    }
                    ItemsofBulkItemsDGV.DataContext = Dat;
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

            BulkItems.IsEnabled = true;
        }
        private void BulkItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("DoProcessBulk") == -1 && Authenticated.IndexOf("CheckAllBulk") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog();  }
            else
            {
                string ID = Classes.InCrementTransactionSerial("Process_BulkItems", "ProcessBulk_ID");
                string BaseCode = ""; string BaseQty = ""; string SUBQty = ""; double BaseCost = 0; string BaseWeight = "";  double CalcQty = 0; double CalcCost = 0;
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                SqlCommand cmd = new SqlCommand();
                SqlConnection con2 = new SqlConnection(Classes.DataConnString);
                SqlCommand cmd2 = new SqlCommand();
                SqlDataReader reader = null;
                for (int i = 0; i < ItemsDGV.Items.Count; i++)
                {
                    if (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0].ToString() == "True")
                    {
                        BaseCode = ((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[1].ToString();
                        BaseQty = ((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4].ToString();
                        SUBQty = BaseQty;
                        BaseCost = Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6].ToString());
                        BaseWeight = (Classes.RetrieveData("Weight", "Code=" + BaseCode, "Setup_Items")).Rows[0][0].ToString();
                        con.Open();
                        try
                        {
                            string s = string.Format("select * from Setup_BulkItems where Item_Code='{0}'", BaseCode);
                            cmd = new SqlCommand(s, con);
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                CalcQty = (((Convert.ToDouble(reader["WeightPrecentage"]) / 100) * Convert.ToDouble(BaseQty)) / Convert.ToDouble(BaseWeight));
                                CalcCost = (((Convert.ToDouble(reader["CostPrecentage"]) / 100)  * Convert.ToDouble(BaseCost)) / Convert.ToDouble(BaseWeight));
                                try
                                {
                                    con2.Open();
                                    string w = string.Format("UPDATE Items set Qty=Qty+{0},Last_Cost=Current_Cost,Current_Cost=(((Qty*Current_Cost)+({0}*{4}))/(Qty+{0})) where ItemID='{1}' and RestaurantID={2} and KitchenID={3}", CalcQty, reader["Code"], CodeOfResturant, CodeOfKitchens, CalcCost);
                                    cmd2 = new SqlCommand(w, con2);
                                    int n = cmd2.ExecuteNonQuery();
                                    if (n == 0)
                                    {
                                        w = string.Format("insert into Items(RestaurantID,KitchenID,ItemID,Qty,Current_Cost,Net_Cost) values({0},{1},'{2}',{3},{4},{5})", CodeOfResturant, CodeOfKitchens, reader["Code"], CalcQty, CalcCost, CalcCost * CalcQty);
                                        cmd2 = new SqlCommand(w, con2);
                                        cmd2.ExecuteNonQuery();
                                    }

                                    w = string.Format("Update ItemsYear set {0}={0}+{2},{1}=(({0}*{1})+({2}*{3})/({0}+{1})) where ItemID='{4}' and Restaurant_ID='{5}' and Kitchen_ID='{6}' and Year='{7}'", MainWindow.MonthQty,MainWindow.MonthCost,CalcQty,CalcCost, reader["Code"], CodeOfResturant, CodeOfKitchens, MainWindow.CurrentYear);
                                    cmd2 = new SqlCommand(w, con2);
                                    n = cmd2.ExecuteNonQuery();
                                    if (n == 0)
                                    {
                                        w = string.Format("insert into ItemsYear(ItemID,Restaurant_ID,Kitchen_ID,Year,{0},{1}) values('{2}','{3}','{4}','{5}',{6},{7})", MainWindow.MonthQty,MainWindow.MonthCost,reader["Code"], CodeOfResturant, CodeOfKitchens, MainWindow.CurrentYear,CalcQty, CalcCost);
                                        cmd2 = new SqlCommand(w, con2);
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                                SUBQty = (Convert.ToDouble(SUBQty) - CalcQty).ToString();

                                try
                                {
                                    string w = string.Format("insert into Process_BulkItems_Items(ProcessBulk_ID,ParentItem_ID,ParentQty,ParentCost,ChiledItem_ID,ChiledQty,ChiledCost) values('{0}','{1}',{2},{3},{4},{5},{6})", ID, BaseCode, ((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4].ToString(), ((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6].ToString(), reader["Code"], BaseCost, BaseCost);
                                    cmd2 = new SqlCommand(w, con2);
                                    cmd2.ExecuteNonQuery();
                                }
                                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                                con2.Close();

                            }
                            try
                            {
                                con2.Open();
                                string w = string.Format("update Items set Qty={0} where RestaurantID={1} and KitchenID={2} and ItemID='{3}' ", SUBQty, CodeOfResturant, CodeOfKitchens, BaseCode);
                                cmd2 = new SqlCommand(w, con2);
                                cmd2.ExecuteNonQuery();
                            }
                            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                            try
                            {
                                string w = string.Format("update ItemsYear set {0}={1} Where ItemID='{2}' and Restaurant_ID='{3}' and Kitchen_ID='{4}' and Year='{5}'", MainWindow.MonthQty,SUBQty, BaseCode, CodeOfResturant, CodeOfKitchens, MainWindow.CurrentYear);
                                cmd2 = new SqlCommand(w, con2);
                                cmd2.ExecuteNonQuery();
                            }
                            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

                            try
                            {
                                string w = string.Format("insert into Process_BulkItems(ProcessBulk_ID,Process_Date,User_ID,Resturant_ID,KitchenID,Post_Date) values('{0}',GETDATE(),'{1}',{2},{3},GETDATE())", ID, MainWindow.UserID, CodeOfResturant, CodeOfKitchens);
                                cmd2 = new SqlCommand(w, con2);
                                cmd2.ExecuteNonQuery();
                            }
                            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                        }
                        catch (Exception ex)
                        {  MessageBox.Show(ex.ToString());  }
                        con2.Close();


                    }
                }
                MessageBox.Show("Done");
            }
            BulkItems.IsEnabled = false;

        }
       
    }
}
