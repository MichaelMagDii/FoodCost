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
    /// Interaction logic for AllCategories.xaml
    /// </summary>
    public partial class AllCategories : Window
    {
        DataTable DT = new DataTable();
        Recipes recipe;
        CategoriesAndSub _categoriesAndSub;
        bool val = true;
        public AllCategories(CategoriesAndSub categoriesAndSub)
        {
            InitializeComponent();
            LoadAllCategories();
            _categoriesAndSub = categoriesAndSub;
            val = false;
        }

        public AllCategories(Recipes _Recipes)
        {
            InitializeComponent();
            LoadAllCategories();
            recipe = _Recipes;
        }

        public void LoadAllCategories()
        {
            DT.Columns.Add("Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Name2");
            DT.Columns.Add("Active",typeof(bool));
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;

            try
            {
                con.Open();
                string q = "select Code, Name, Name2, IsActive from Setup_RecipeCategory where IsActive='True'";
                SqlCommand cmd = new SqlCommand(q, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DT.Rows.Add(reader["Code"], reader["Name"], reader["Name2"], reader["IsActive"]);
                }
                for(int i=0;i<DT.Columns.Count;i++)
                {
                    DT.Columns[i].ReadOnly = true;
                }
                CategoryDGV.DataContext = DT;
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
                    if (val == false)
                        _categoriesAndSub.Categorycbx.Text = ((DataRowView)CategoryDGV.SelectedItems[0]).Row.ItemArray[1].ToString();
                    else
                    {
                        recipe.Categorytxt.Text = ((DataRowView)CategoryDGV.SelectedItems[0]).Row.ItemArray[1].ToString();
                        recipe.Categtxt.Text = ((DataRowView)CategoryDGV.SelectedItems[0]).Row.ItemArray[0].ToString();
                    }
                    val = true;
                    return;

                }


            }
        }

        private void TextDataChange(object sender, TextChangedEventArgs e)
        {
            CategoryDGV.DataContext = null;
            DT.Rows.Clear();
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(Classes.DataConnString);

            if ((RadioByCode.IsChecked == true || RadioByName.IsChecked == true) && SearchTxt.Text != "")
            {
                if (RadioByCode.IsChecked == true && RadioByName.IsChecked == false)
                {
                    try
                    {
                        con.Open();
                        string q = "select Code, Name, Name2, IsActive from Setup_RecipeCategory where IsActive='True' and Code Like '%" + SearchTxt.Text + "%'";
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
                        string q = "select Code, Name, Name2, IsActive from Setup_RecipeCategory where IsActive='True' and Name Like '%" + SearchTxt.Text + "%'";
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
            }
            else
            {
                try
                {
                    con.Open();
                    string q = "select Code, Name, Name2, IsActive from Setup_RecipeCategory where IsActive='True'";
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
            CategoryDGV.DataContext = DT;
        }
    }
}
