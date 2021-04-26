using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
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
    /// Interaction logic for FiscalInventory.xaml
    /// </summary>
    public partial class StockInventory : UserControl
    {
        public string ValOfResturant = "";
        public string ValOfKitchen = "";
        DataTable dt = new DataTable();
        string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
        public StockInventory()
        {
            InitializeComponent();
            LoadAllResturant();
        }

        public void LoadAllResturant()
        {
            SqlConnection con = new SqlConnection(connString);
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
        private void ResturantComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kitchencbx.Items.Clear();
            SqlConnection con = new SqlConnection(connString);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "select Name from Setup_Kitchens Where RestaurantID=(select Code From Setup_Restaurant Where Name='" + Outletcbx.SelectedItem.ToString() + "')";
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
        private void GetInventoryID()
        {
            SqlConnection con = new SqlConnection(connString);
            try
            {
                con.Open();
                string s = "Select TOP(1)Inventory_ID From StockInventory_tbl ORDER BY Inventory_ID DESC";
                SqlCommand cmd = new SqlCommand(s, con);
                if (cmd.ExecuteScalar() == null)
                {
                    Serial_Inventory_NO.Text = "1";
                }
                else
                {
                    Serial_Inventory_NO.Text = (int.Parse(cmd.ExecuteScalar().ToString()) + 1).ToString();
                }
                con.Close();
            }
            catch { }

        }
        private void GetCodeofResturantAndKitchen()
        {
            SqlConnection con = new SqlConnection(connString);
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
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetCodeofResturantAndKitchen();
            Inventory.Visibility = Visibility.Visible;
            NumberOfItemText.Visibility = Visibility.Visible;
            NUmberOfItems.Visibility = Visibility.Visible;
            TotalofItems.Visibility = Visibility.Visible;
            Total_Price.Visibility = Visibility.Visible;
            Inventory.IsEnabled = true;
            InventoryChose.Visibility = Visibility.Hidden;
            InventoryInfo.Visibility = Visibility.Visible;
            addItemBtn.Visibility = Visibility.Visible;
            RemoveItemBtn.Visibility = Visibility.Visible;
            GetInventoryID();
        }
        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            ValOfResturant = ValOfResturant;
            ValOfKitchen = ValOfKitchen;
            Items itemswindow = new Items(this);
            itemswindow.ShowDialog();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        private void RemoveItemBtn_Click(object sender, RoutedEventArgs e)
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
      

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDGV.Items.Count == 0)
            {
                MessageBox.Show("First You should Select Items !");
                return;
            }
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
            

            SqlConnection con = new SqlConnection(connString);
            try
            {
                con.Open();
                string s = string.Format("insert into StockInventory_tbl(Inventory_ID,Inventory_Num,Inventory_Type,Inventory_Date,Comment,Resturant_ID,KitchenID,Post_Date) values({0},{1},'{2}',{3},'{4}',{5},{6},GETDATE())", Serial_Inventory_NO.Text, Inventory_NO.Text, Typecbx.Text, InventoryDate.Text, commenttxt.Text, ValOfResturant, ValOfKitchen);
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

            try
            {
                con.Open();
                for (int i = 0; i < ItemsDGV.Items.Count; i++)
                {
                    string s = "Insert into StockInventory_Items(Inventory_ID,Item_ID,Qty,InventoryQty,Variance,Cost) Values ( " + Serial_Inventory_NO.Text + ",'" + (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]) + "'," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[3]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5]) + "," + Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]) + ")";
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

            try
            {
                con.Open();
                for (int i = 0; i < ItemsDGV.Items.Count; i++)
                {
                    string H = string.Format("Update Items set Qty={0}, Net_Cost=(Current_Cost * {0}) where ItemID = '{1}' and RestaurantID ={2} and KitchenID={3}", Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[4]), (((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[0]), ValOfResturant, ValOfKitchen);
                    SqlCommand cmd = new SqlCommand(H, con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MessageBox.Show("Edited Successfully");
            }
        }

        private void ItemsDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            dt = ItemsDGV.DataContext as DataTable;
            int CountOfItems = 0;
            try
            {
                dt.Columns["Variance"].ReadOnly = false;
                dt = ((DataView)ItemsDGV.ItemsSource).ToTable();
                if (e.Column.Header == "OriginalQty")
                {
                    try
                    {
                        (ItemsDGV.SelectedItem as DataRowView).Row[5] = (Convert.ToDouble((ItemsDGV.SelectedItem as DataRowView).Row.ItemArray[3]) - double.Parse((e.EditingElement as TextBox).Text)).ToString();
                    }
                    catch { }
                }
                dt.Columns["OriginalQty"].ReadOnly = false;
                dt.Columns["Variance"].ReadOnly = true;
            }
            catch { }

            try
            {

                double totalPrice = 0;
                for (int i = 0; i < ItemsDGV.Items.Count; i++)
                {
                    try
                    {
                        totalPrice += Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[6]);
                    }
                    catch
                    {

                    }
                }
                NUmberOfItems.Text = (ItemsDGV.Items.Count).ToString();
                Total_Price.Text = (totalPrice).ToString();
            }
            catch { }
        }
    }
}
