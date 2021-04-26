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
    /// Interaction logic for AllSubCategories.xaml
    /// </summary>
    public partial class AllSubCategories : Window
    {
        DataTable DT = new DataTable();
        Recipes recipe;
        string vaal;
        string v = "";
        public AllSubCategories(Recipes _Recipe, string val)
        {
            InitializeComponent();
            vaal = val;
            LoadAllCategories(vaal);
            recipe = _Recipe;

        }

        public void LoadAllCategories(string val)
        {
            DT.Columns.Add("Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Name2");
            DT.Columns.Add("Active");
            v = val;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;

            try
            {
                con.Open();
                string q = "SELECT Code,Name,Name2,IsActive From  Setup_RecipeSubCategories Where IsActive='True' and Category_ID=" + val;
                SqlCommand cmd = new SqlCommand(q, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DT.Rows.Add(reader["Code"], reader["Name"], reader["Name2"], reader["IsActive"]);
                }
                SubCategories.DataContext = DT;
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


        private void MouseDoubleClick_click(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    this.Close();
                    recipe.SUBCategorytxt.Text = ((DataRowView)SubCategories.SelectedItems[0]).Row.ItemArray[1].ToString();
                    recipe.SUBCategtxt.Text = ((DataRowView)SubCategories.SelectedItems[0]).Row.ItemArray[0].ToString();
                    return;
                }


            }
        }

        private void TextDataChange(object sender, TextChangedEventArgs e)
        {
            DT.Rows.Clear();
            SubCategories.DataContext = null;
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            if ((RadioByCode.IsChecked == true || RadioByName.IsChecked == true) && SearchTxt.Text != "")
            {
                if (RadioByCode.IsChecked == true && RadioByName.IsChecked == false)
                {
                    try
                    {
                        con.Open();
                        string q = "SELECT Code,Name,Name2,IsActive From  Setup_RecipeSubCategories Where IsActive='True' and Category_ID=" + v+" AND Code Like '%" + SearchTxt.Text + "%'";
                        SqlCommand cmd = new SqlCommand(q, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DT.Rows.Add(reader["Code"], reader["Name"], reader["Name2"], reader["IsActive"]);
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
                else if (RadioByName.IsChecked == true && RadioByCode.IsChecked == false)
                {
                    try
                    {
                        con.Open();
                        string q = "SELECT Code,Name,Name2,IsActive From  Setup_RecipeSubCategories Where IsActive='True' and Category_ID = " + v+" AND Name Like '%" + SearchTxt.Text + "%'";
                        SqlCommand cmd = new SqlCommand(q, con);
                        //SqlCommand cmd = new SqlCommand("GetCategory", con);
                        //cmd.CommandType = CommandType.StoredProcedure;
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DT.Rows.Add(reader["Code"], reader["Name"], reader["Name2"], reader["IsActive"]);
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
            }
            else
            {
                try
                {
                    con.Open();
                    string q = "SELECT Code,Name,Name2,IsActive From  Setup_RecipeSubCategories Where IsActive='True' and Category_ID=" + v;
                    SqlCommand cmd = new SqlCommand(q, con);
                    //SqlCommand cmd = new SqlCommand("GetCategory", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DT.Rows.Add(reader["Code"], reader["Name"], reader["Name2"], reader["IsActive"]);
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
            SubCategories.DataContext = DT;

        }
    }
}
