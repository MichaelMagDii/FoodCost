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
    /// Interaction logic for KitcheItemsn.xaml
    /// </summary>
    public partial class KitcheItemsn : UserControl
    {
        DataTable dt = new DataTable();
        List<string> Authenticated = new List<string>();
        string ValtoDelete = "";
        string ValOfResturant = "";
        string ValOfKitchen = "";
        int Count = 0;
        public KitcheItemsn()
        {
            if (MainWindow.AuthenticationData.ContainsKey("KitchenItems"))
            {
                Authenticated = MainWindow.AuthenticationData["KitchenItems"];
                if (Authenticated.Count == 0)
                {
                    MessageBox.Show("You Havent a Privilage to Open this Page");
                    LogIn logIn = new LogIn();
                    logIn.ShowDialog();
                }
                else
                {
                    InitializeComponent();
                    LoadAllOutlet();
                    SaveBtn.IsEnabled = false;
                    dt.Columns.Add("Code");
                    dt.Columns.Add("Manual Code");
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Name2");
                    dt.Columns.Add("Shelf");
                    dt.Columns.Add("Min Qty");
                    dt.Columns.Add("Max Qty");
                    dt.Columns.Add("Unit");
                }
            }
                   
        }
        private void LoadAllOutlet()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "select Name from Setup_Restaurant";
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
        }
        private void OutletComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kitchencbx.Items.Clear();
            ValOfResturant = "";
            string v = Outletcbx.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = "select Code from Setup_Restaurant WHERE Name='" + v + "'";
                SqlCommand cmd = new SqlCommand(s, con);
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


            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "select Name from Setup_Kitchens WHERE RestaurantID=" + ValOfResturant;
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
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string b = "";
            if (Kitchencbx.SelectedItem != null)
            {
                string v = Kitchencbx.SelectedItem.ToString();
                if (Outletcbx.SelectedItem != null)
                {
                    LoadDatatoGrid();
                    SaveBtn.IsEnabled = true;
                    ButtonGrid.Visibility = Visibility.Visible;
                }
            }
        }
        private void GetKitchenID()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.Open();
                string s = "select Code from Setup_Kitchens WHERE Name='" + Kitchencbx.SelectedItem.ToString() + "'";
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
        }
        public void LoadDatatoGrid()
        {
            GetKitchenID();
            dt.Rows.Clear();
            ItemsDGV.DataContext = null;
            Count = 0;
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            string valuoOfKitchen = Kitchencbx.SelectedItem.ToString();
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
           
            try
            {
                con.Open();
                string s = "Select ItemID,ShulfID,MinQty,MaxQty From Setup_KitchenItems Where RestaurantID=" + ValOfResturant+ " and KitchenID=" + ValOfKitchen;
                cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    con2.Open();
                    string W = "select Name,Name2,[Manual Code],Unit from Setup_Items Where Code=" + reader["ItemID"].ToString();
                    SqlCommand _cmd = new SqlCommand(W, con2);
                    reader2 = _cmd.ExecuteReader();
                    while(reader2.Read())
                    {
                        dt.Rows.Add(reader["ItemID"], reader2["Manual Code"], reader2["Name"], reader2["Name2"], reader["ShulfID"], reader["MinQty"], reader["MaxQty"], reader2["Unit"]);
                        Count++;
                    }
                    con2.Close();
                }
                for(int i =0;i<dt.Columns.Count;i++)
                {
                    dt.Columns[i].ReadOnly = true;
                }
                dt.Columns["Shelf"].ReadOnly = false;
                dt.Columns["Min Qty"].ReadOnly = false;
                dt.Columns["Max Qty"].ReadOnly = false;
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
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("AddKitchenItems") == -1 && Authenticated.IndexOf("CheckAllKitchenItems") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                Items items = new Items(this);
                items.ShowDialog();
                SaveBtn.IsEnabled = true;
                //SaveBtn.IsEnabled = true;
            }
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("SaveKitchenItems") == -1 && Authenticated.IndexOf("CheckAllKitchenItems") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                DataTable DT = new DataTable();
                DT = ItemsDGV.DataContext as DataTable;
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (DT.Rows[i].ItemArray.Contains(""))
                    {
                        MessageBox.Show(string.Format("item {0} has empty fields", DT.Rows[i].ItemArray[2]));
                        return;
                    }
                }
                con.Open();
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    string s = string.Format("update Setup_KitchenItems set shulfID='{0}',MinQty='{1}',MaxQty='{2}',Last_Modified_Date=GETDATE() where ItemID='{3}' and RestaurantID={4} and KitchenID={5}", DT.Rows[i]["Shelf"], DT.Rows[i]["Min Qty"], DT.Rows[i]["Max Qty"], DT.Rows[i]["Code"], ValOfResturant, ValOfKitchen);
                    SqlCommand cmd = new SqlCommand(s, con);
                    int n = cmd.ExecuteNonQuery();

                    if (n == 0)
                    {
                        string WhereFiltering = string.Format("RestaurantID='{0}' and KitchenID='{1}' and ItemID='{2}'", ValOfResturant, ValOfKitchen, DT.Rows[i]["Code"]);
                        DataTable Table = Classes.RetrieveData("ItemID", WhereFiltering, "Setup_KitchenItems");
                        if (Table.Rows.Count == 0)
                        {
                            string FiledSelection = "RestaurantID,KitchenID,ItemID,ShulfID,MinQty,MaxQty,Create_Date,WS,UserID";
                            string Values = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}',GETDATE(),'{6}','{7}'", ValOfResturant, ValOfKitchen, DT.Rows[i]["Code"], DT.Rows[i]["Shelf"], DT.Rows[i]["Min Qty"], DT.Rows[i]["Max Qty"], Classes.WS, MainWindow.UserID);
                            Classes.InsertRow("Setup_KitchenItems", FiledSelection, Values);
                        }

                        WhereFiltering = string.Format("RestaurantID='{0}' and KitchenID='{1}' and ItemID='{2}'", ValOfResturant, ValOfKitchen, DT.Rows[i]["Code"]);
                        Table = Classes.RetrieveData("ItemID", WhereFiltering, "Items");
                        if (Table.Rows.Count == 0)
                        {
                            string FiledSelection = "KitchenID,ItemID,RestaurantID,Qty,Units,Last_Cost,Current_Cost,Net_Cost";
                            string Values = string.Format("'{0}','{1}','{2}','0','','','0',''", ValOfKitchen, DT.Rows[i]["Code"], ValOfResturant);
                            Classes.InsertRow("Items", FiledSelection, Values);
                        }

                        WhereFiltering = string.Format("Restaurant_ID='{0}' and Kitchen_ID='{1}' and ItemID='{2}' and Year='{3}'", ValOfResturant, ValOfKitchen, DT.Rows[i]["Code"],MainWindow.CurrentYear);
                        Table = Classes.RetrieveData("ItemID", WhereFiltering, "ItemsYear");
                        if (Table.Rows.Count == 0)
                        {
                            string FiledSelection = "ItemID,Restaurant_ID,Kitchen_ID,Year";
                            string Values = string.Format("'{0}','{1}','{2}','{3}'",  DT.Rows[i]["Code"], ValOfResturant, ValOfKitchen,MainWindow.CurrentYear);
                            Classes.InsertRow("ItemsYear", FiledSelection, Values);
                        }
                    }
                }

                con.Close();
                MessageBox.Show("Items saved successfuly");
                SaveBtn.IsEnabled = false;
                LoadDatatoGrid();
            }
        }
        private void RowClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid data = sender as DataGrid;

                if (data != null && data.SelectedItems != null && data.SelectedItems.Count == 1)
                {
                    ValtoDelete = ((DataRowView)data.SelectedItem).Row.ItemArray[0].ToString();
                }
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("DeleteKitchenItems") == -1 && Authenticated.IndexOf("CheckAllKitchenItems") == -1)
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
                    string H = "SELECT Item_Code FROM Setup_RecipeItems WHere Item_Code='" + ValtoDelete + "'";
                    SqlCommand cmd = new SqlCommand(H, con);
                    if (cmd.ExecuteScalar() == null)
                    {
                        try
                        {
                            H = "Delete Setup_KitchenItems Where ItemID=" + ValtoDelete + " And RestaurantID=" + ValOfResturant + " AND KitchenID=" + ValOfKitchen;
                            cmd = new SqlCommand(H, con);
                            cmd.ExecuteNonQuery();

                            //H = "Delete Items Where ItemID=" + ValtoDelete + " And RestaurantID=" + ValOfResturant + " AND KitchenID=" + ValOfKitchen;
                            //cmd = new SqlCommand(H, con);
                            //cmd.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        finally
                        {
                            LoadDatatoGrid();
                            MessageBox.Show("Items are deleteed  ");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can't Delete this Item");
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
        } //el mafrood ntcheck lw el item da m4 equal 0 yb2a can not delete        
    }
}