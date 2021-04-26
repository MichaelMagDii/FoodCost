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
    /// Interaction logic for AllRecipes.xaml
    /// </summary>
    public partial class AllRecipes : Window
    {
        public AllRecipes()
        {
            InitializeComponent();
        }
        Recipes recipes;
        public AllRecipes(Recipes _recipes)
        {
            InitializeComponent();
            LoadToGrid();
            recipes = _recipes;
        }

        GenerateBatch generatebatch;
        public AllRecipes(GenerateBatch _GererateBatch)
        {
            InitializeComponent();
            LoadToGrid();
            generatebatch = _GererateBatch;
        }


        public void LoadToGrid()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter("SELECT Code,Name,Name2,(select Name From Setup_RecipeCategory where Code=Category_ID) as Category,(select Name From Setup_RecipeSubCategories where Code=SubCategory_ID) as 'SUB Category',Unit,UnitQty FROM Setup_Recipes where IsActive='True'", con))
                    da.Fill(dt);

                AllRecipesDGV.DataContext = dt;
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

        private void AllRecipesDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;

            if (main.GridMain.Children[0].GetType().Name == "Recipes")
            {
                string cost = "";
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                SqlCommand cmd = new SqlCommand();
                DataRowView drv = AllRecipesDGV.SelectedItem as DataRowView;
                DataTable dt = new DataTable();
                dt.Columns.Add("Item_Code");
                dt.Columns.Add("Recipe_Code");
                dt.Columns.Add("Name");
                dt.Columns.Add("Name2");
                dt.Columns.Add("Qty");
                dt.Columns.Add("Recipe_Unit");
                dt.Columns.Add("Cost");
                dt.Columns.Add("Total_Cost");
                dt.Columns.Add("Cost_Precentage");
                //dt = recipes.RecipesDGV.DataContext as DataTable;

                if (sender != null)
                {
                    DataGrid grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        if (recipes.RecipesDGV.DataContext != null)
                        {
                            dt = recipes.RecipesDGV.DataContext as DataTable;
                            for (int i = 0; i < dt.Rows.Count; i++)
                                if (dt.Rows[i]["Recipe_Code"].ToString() == drv.Row.ItemArray[0].ToString())
                                {
                                    MessageBox.Show("Item Existed");
                                    return;
                                }
                        }

                        //if (recipes.RecipesDGV.DataContext != null)
                        //    dt = recipes.RecipesDGV.DataContext as DataTable;

                        try
                        {
                            con.Open();
                            string s = string.Format("select Price from RecipeQty where Recipe_ID={0}", drv.Row.ItemArray[0].ToString());
                            cmd = new SqlCommand(s, con);
                            if (cmd.ExecuteScalar() == null)
                            {
                                cost = "0";
                            }
                            else
                            {
                                cost = cmd.ExecuteScalar().ToString();

                            }
                        }
                        catch { }
                        dt.Rows.Add("", (((DataRowView)grid.SelectedItem).Row.ItemArray[0]).ToString(), (((DataRowView)grid.SelectedItem).Row.ItemArray[1]).ToString(), (((DataRowView)grid.SelectedItem).Row.ItemArray[2]).ToString(), "1", (((DataRowView)grid.SelectedItem).Row.ItemArray[5]).ToString(), cost, cost, "");
                        double sum = 0;
                        double totalCost = 0;
                        dt.Columns["Cost_Precentage"].ReadOnly = false;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sum += Convert.ToDouble(dt.Rows[i]["Total_Cost"]);
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["Cost_Precentage"] = ((Convert.ToDouble(dt.Rows[i]["Total_Cost"])) / (sum) * 100).ToString() + " %";
                            totalCost += (Convert.ToDouble(dt.Rows[i]["Total_Cost"]));

                        }

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dt.Columns[i].ReadOnly = true;
                        }
                        dt.Columns["Qty"].ReadOnly = false;
                        recipes.Tottaltxt.Text = totalCost.ToString();
                    }
                    recipes.RecipesDGV.DataContext = dt;

                    //if (recipes.RecipesDGV.Items.Count != 0)
                    //{
                    //    (recipes.RecipesDGV.Items[recipes.RecipesDGV.Items.Count - 1] as DataRowView).Row[7] = (recipes.RecipesDGV.Items[recipes.RecipesDGV.Items.Count - 1] as DataRowView).Row[6];
                    //    double sum = 0;
                    //    for (int i = 0; i < dt.Rows.Count; i++)
                    //    {
                    //        sum += Convert.ToDouble((recipes.RecipesDGV.Items[i] as DataRowView).Row.ItemArray[7]);
                    //    }

                    //    double _sum = 0;
                    //    for (int i = 0; i < dt.Rows.Count; i++)
                    //    {
                    //        _sum = (Convert.ToDouble((recipes.RecipesDGV.Items[i] as DataRowView).Row.ItemArray[7]) / sum) * 100;
                    //        (recipes.RecipesDGV.Items[i] as DataRowView).Row[8] = _sum.ToString() + " %";
                    //    }
                    //}
                }
            }
            else if(main.GridMain.Children[0].GetType().Name == "GenerateBatch")
            {
                generatebatch.Recipecbx.Text = ((DataRowView)AllRecipesDGV.SelectedItems[0]).Row.ItemArray[1].ToString();
                generatebatch.valofRecipe = ((DataRowView)AllRecipesDGV.SelectedItems[0]).Row.ItemArray[0].ToString();
                generatebatch.UnitofRecipelbl.Content = ((DataRowView)AllRecipesDGV.SelectedItems[0]).Row.ItemArray[5].ToString();
            }

            this.Close();
        }

        private void TextDataChange(object sender, TextChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            if ((RadioByCode.IsChecked == true || RadioByName.IsChecked == true) && SearchTxt.Text != "")
            {
                AllRecipesDGV.DataContext = null;
                if (RadioByCode.IsChecked == true && RadioByName.IsChecked == false)
                {
                    try
                    {
                        con.Open();
                        //lsa 7etet el weight fe el select
                        string Q = "SELECT Code,Name,Name2,(select Name From Setup_RecipeCategory where Code=Category_ID) as Category,(select Name From Setup_RecipeSubCategories where Code=SubCategory_ID) as 'SUB Category',Unit,UnitQty FROM Setup_Recipes where IsActive='True' and Code Like '%" + SearchTxt.Text + "%'";
                        DataTable dt = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(Q, con))
                            da.Fill(dt);

                        AllRecipesDGV.DataContext = dt;


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
                        //lsa 7etet el weight fe el select
                        string Q = "SELECT Code,Name,Name2,(select Name From Setup_RecipeCategory where Code=Category_ID) as Category,(select Name From Setup_RecipeSubCategories where Code=SubCategory_ID) as 'SUB Category',Unit,UnitQty FROM Setup_Recipes where IsActive='True' and Name Like '%" + SearchTxt.Text + "%'";
                        DataTable dt = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(Q, con))
                            da.Fill(dt);

                        AllRecipesDGV.DataContext = dt;
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
                    DataTable dt = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT Code,Name,Name2,(select Name From Setup_RecipeCategory where Code=Category_ID) as Category,(select Name From Setup_RecipeSubCategories where Code=SubCategory_ID) as 'SUB Category',Unit,UnitQty FROM Setup_Recipes where IsActive='True'", con))
                        da.Fill(dt);

                    AllRecipesDGV.DataContext = dt;
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