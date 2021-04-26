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
using System.Windows.Shapes;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for ParentWindow.xaml
    /// </summary>
    public partial class ParentWindow : Window
    {
        string CodeOfparent = "";
        string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
        int ValuesOfItems = 0;
        List<DgvData> ItemsParentData = new List<DgvData>();
        List<DgvData> ItemsParentDataStarting = new List<DgvData>();
        public ParentWindow(string Code)
        {
            InitializeComponent();
            LoadToDGVOfParentItemsStartly(Code);
            CodeOfparent = Code;
        }

        private void LoadToDGVOfParentItemsStartly(string Code)
        {
            SqlConnection con = new SqlConnection(connString);
            try
            {
                con.Open();
                string s = "SELECT Name From Setup_Items Where Code="+Code;
                SqlCommand cmd = new SqlCommand(s, con);
                ItemNametxt.Text = cmd.ExecuteScalar().ToString();
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
                string s = "SELECT Code,Name From Setup_ParentItems Where Parent_Item=+'"+Code+"'";
                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                    da.Fill(dt);

                ParentItemsDGV.DataContext = dt;
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

        private void LoadToDGVOfParentItems(string _Name)
        {
            DataTable dt = new DataTable();
            dt = ((DataTable)ParentItemsDGV.DataContext);
            SqlConnection con = new SqlConnection(connString);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "select Code,Name from Setup_Items where Name = '" + _Name+"'";
                SqlCommand cmd = new SqlCommand(s, con);
                using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                    da.Fill(dt);

                ParentItemsDGV.DataContext = dt;
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

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }   // Done

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ParentGrid.Visibility = Visibility.Hidden;
            LoadToDGVOfItems();
            ShowItems.Visibility = Visibility.Visible;
        }  //Done

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                con.Open();
                string s11 = "delete from Setup_ParentItems where Parent_Item='" + CodeOfparent+ "'";
                cmd = new SqlCommand(s11, con);
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
                    for (int q = 0; q < ParentItemsDGV.Items.Count; q++)
                    {
                        string s11 = "insert into Setup_ParentItems(Code,Name,Parent_Item) values ('" + ((DataRowView)ParentItemsDGV.Items[q]).Row.ItemArray[0] + "','" + ((DataRowView)ParentItemsDGV.Items[q]).Row.ItemArray[1] + "','" + CodeOfparent + "')";
                        cmd = new SqlCommand(s11, con);
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
                    LoadToDGVOfParentItemsStartly(CodeOfparent);
                    MessageBox.Show("Saved Sudssful");
                }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ParentItemsDGV.SelectedItems.Count !=0)
            {
                string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
                SqlConnection con = new SqlConnection(connString);
                try
                {
                    con.Open();
                    string iteem = ((DataRowView)ParentItemsDGV.SelectedItems[0]).Row.ItemArray[0].ToString();
                    string s11 = "delete from Setup_ParentItems where Code='" + iteem + "' AND Parent_Item='" + CodeOfparent + "'";
                    SqlCommand cmd = new SqlCommand(s11, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                    LoadToDGVOfParentItemsStartly(CodeOfparent);
                    MessageBox.Show("Deleted Sudssful");
                }
            }
            
        }
    

        // Function and Events of Data Grid View Strted From Here 
        // Functions
        private void LoadToDGVOfItems()
        {
            SqlConnection con = new SqlConnection(connString);
            try
            {
                con.Open();
                string s = "select Code,Name,Name2,Category from Setup_Items";
                DataTable dtt = new DataTable();

                using (SqlDataAdapter daa = new SqlDataAdapter(s, con))
                    daa.Fill(dtt);

                ItemsDGV.DataContext = dtt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }    //Done

        //Events of Data Grid View 
        private void ItemsDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    String Codeval = (grid.SelectedItem as DataRowView).Row.ItemArray[1].ToString();
                    ShowItems.Visibility = Visibility.Hidden;
                    ParentGrid.Visibility = Visibility.Visible;
                    LoadToDGVOfParentItems(Codeval);
                }
            }
        }

        private void TextDataChange(object sender, TextChangedEventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            if ((RadioByCode.IsChecked == true || RadioByName.IsChecked == true) && SearchTxt.Text != "")
            { 
                ItemsDGV.DataContext = null;
                if (RadioByCode.IsChecked == true && RadioByName.IsChecked == false)
                {
                    try
                    {
                        con.Open();
                        string s = "select Code,Name,Name2,Category from Setup_Items Where Code Like '%" + SearchTxt.Text + "%'";
                        DataTable dt = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                            da.Fill(dt);

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
                else if (RadioByName.IsChecked == true && RadioByCode.IsChecked == false)
                {
                    try
                    {
                        con.Open();
                        string s = "select Code,Name,Name2,Category from Setup_Items Where Name Like '%" + SearchTxt.Text + "%'";
                        DataTable dt = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                            da.Fill(dt);

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
            else
            {
                try
                {
                    con.Open();
                    string s = "select Code,Name,Name2,Category from Setup_Items";
                    DataTable dt = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                        da.Fill(dt);

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
    }
}


