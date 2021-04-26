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
    /// Interaction logic for Fiscal_Inventorry.xaml
    /// </summary>
    public partial class Fiscal_Inventorry : UserControl
    {
        string ValOfResturant = "";
        string ValOfKitchen = "";
        DataTable dt = new DataTable();
        string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;

        public Fiscal_Inventorry()
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
                string s = "select Name from Store_Setup";
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
                string s = "select Name from Kitchens_Setup Where RestaurantID=(select Code From Store_Setup Where Name='" + Outletcbx.SelectedItem.ToString() + "')";
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
            dt.Columns.Clear();
            dt.Rows.Clear();
            try
            {
                string FirstName = ""; string SeconName = "";
                SqlDataReader reader = null;
                SqlDataReader reader2 = null;
                SqlConnection con = new SqlConnection(connString);
                SqlConnection con2 = new SqlConnection(connString);
                // DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                dt.Columns.Add("ItemsID");
                dt.Columns.Add("Name");
                dt.Columns.Add("Name2");
                dt.Columns.Add("Qty");
                dt.Columns.Add("StaticQty");
                dt.Columns.Add("Variance");
                string NameOfResturant = "";
                string NameofKitchen = "";
                if (Outletcbx.SelectedItem != null)
                {
                    NameOfResturant = Outletcbx.SelectedItem.ToString();
                    NameofKitchen = Kitchencbx.SelectedItem.ToString();
                    try
                    {
                        con.Open();
                        string s = "SELECT Code FROM Store_Setup Where Name='" + NameOfResturant + "'";
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
                        string s = "SELECT Code FROM Kitchens_Setup Where Name='" + NameofKitchen + "'";
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

                    try
                    {
                        con.Open();
                        string s = "select RestaurantID,KitchenID,ItemID,Qty from Items Where RestaurantID=" + ValOfResturant + " and KitchenID=" + ValOfKitchen;
                        cmd = new SqlCommand(s, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            try
                            {
                                con2.Open();
                                string q = "SELECT Name,Name2 From Setup_Items Where Code='" + reader["ItemID"].ToString() + "'";
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
                            dt.Rows.Add(reader["ItemID"].ToString(), FirstName, SeconName, reader["Qty"].ToString(), "", "");
                        }
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dt.Columns[i].ReadOnly = true;
                        }
                        dt.Columns["StaticQty"].ReadOnly = false;
                        //dt.Columns["DifferenceQty"].ReadOnly = false;

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

            }
            catch
            {

            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void ItemsDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
