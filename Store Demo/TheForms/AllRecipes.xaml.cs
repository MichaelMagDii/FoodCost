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
        MainWindow main = Application.Current.MainWindow as MainWindow;
        DataTable DT = new DataTable();
        public AllRecipes()
        {
            InitializeComponent();
        }
        Recipes recipes;
        GenerateBatch generatebatch;
        Transfer_Resturant transfer_Resturant;
        Transfer_Kitchens transfer_Kitchens;
        AdjacmentInventory adjacmentInventory;

        public AllRecipes(AdjacmentInventory _adjacmentInventory)
        {
            InitializeComponent();
            adjacmentInventory = _adjacmentInventory;
            LoadToGrid();
        }
        public AllRecipes(Recipes _recipes)
        {
            InitializeComponent();
            LoadToGrid();
            recipes = _recipes;
        }
        public AllRecipes(GenerateBatch _GererateBatch)
        {
            InitializeComponent();
            LoadToGrid();
            generatebatch = _GererateBatch;
        }
        public AllRecipes(Transfer_Resturant _Transfer_Restaurant)
        {
            InitializeComponent();
            LoadToGrid();
            transfer_Resturant = _Transfer_Restaurant;
        }
        public AllRecipes(Transfer_Kitchens _Transfer_Kitchen)
        {
            InitializeComponent();
            LoadToGrid();
            transfer_Kitchens = _Transfer_Kitchen;
        }
        public void LoadToGrid()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            con.Open();
            try
            {
                if (main.GridMain.Children[0].GetType().Name == "Transfer_Resturant" || main.GridMain.Children[0].GetType().Name == "Transfer_Kitchens" )
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT Code,CrossCode as 'Manual Code',Name,Name2,Unit FROM Setup_Recipes where IsActive='True'", con))
                        da.Fill(DT);
                }
                else if( main.GridMain.Children[0].GetType().Name == "AdjacmentInventory")
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(string.Format("SELECT dbo.RecipeQty.Recipe_ID AS Code, dbo.Setup_Recipes.Name, dbo.Setup_Recipes.Name2, dbo.RecipeQty.Qty, dbo.RecipeQty.Price, dbo.Setup_Recipes.Unit FROM dbo.RecipeQty INNER JOIN dbo.Setup_Recipes ON dbo.RecipeQty.Recipe_ID=dbo.Setup_Recipes.Code WHERE (dbo.Setup_Recipes.IsActive = 'True') AND (dbo.RecipeQty.Resturant_ID = {0}) AND (dbo.RecipeQty.Kitchen_ID = {1})", adjacmentInventory.ValOfResturant, adjacmentInventory.ValOfKitchen), con))
                        da.Fill(DT);
                }
                else
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT Code,Name,Name2,(select Name From Setup_RecipeCategory where Code=Category_ID) as Category,(select Name From Setup_RecipeSubCategories where Code=SubCategory_ID) as 'SUB Category',Unit,UnitQty FROM Setup_Recipes where IsActive='True'", con))
                        da.Fill(DT);
                }
                

                AllRecipesDGV.DataContext = DT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            con.Close();
        }

        private void AllRecipesDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            DataGrid grid = sender as DataGrid;
            DataTable dt = new DataTable();

            if (main.GridMain.Children[0].GetType().Name == "Recipes")
            {
                string cost = "";
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                SqlCommand cmd = new SqlCommand();
                DataRowView drv = AllRecipesDGV.SelectedItem as DataRowView;
                dt = new DataTable();
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
                    grid = sender as DataGrid;
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
            else if (main.GridMain.Children[0].GetType().Name == "Transfer_Resturant")
            {
                DataRowView drv = grid.SelectedItem as DataRowView;

                if (transfer_Resturant.ItemsDGV.DataContext == null)
                {
                    dt = drv.DataView.ToTable().Clone();
                    dt.Columns.Add("Qty");
                    dt.Columns.Add(transfer_Resturant.From_Resturant.Text + " Qty");
                    dt.Columns.Add(transfer_Resturant.From_Resturant.Text + " Unit Cost");
                    dt.Columns.Add(transfer_Resturant.From_Resturant.Text + " total Cost");
                    dt.Columns.Add(transfer_Resturant.ToResturant.Text + " Qty");
                    dt.Columns.Add(transfer_Resturant.ToResturant.Text + " Unit Cost");
                    dt.Columns.Add(transfer_Resturant.ToResturant.Text + " total Cost");
                    dt.Columns.Add("Recipe",typeof(bool));
                }
                else
                    dt = transfer_Resturant.ItemsDGV.DataContext as DataTable;

                for (int i = 0; i < dt.Rows.Count; i++)
                    if (dt.Rows[i]["Code"].ToString() == drv.Row["Code"].ToString())
                    {
                        MessageBox.Show("This Item Already Exist");
                        return;
                    }
                //transfer_Resturant.IfTransferRecipes = true;
                dt.ImportRow(drv.Row);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = true;
                }
                dt.Columns["Recipe"].ReadOnly = false;
                dt.Rows[dt.Rows.Count-1]["Recipe"] = true;
                dt.Columns["Recipe"].ReadOnly = true;

                dt.Columns["Qty"].ReadOnly = false;

                transfer_Resturant.ItemsDGV.DataContext = dt;

                this.Close();
            }
            else if (main.GridMain.Children[0].GetType().Name == "Transfer_Kitchens")
            {
                DataRowView drv = grid.SelectedItem as DataRowView;

                if (transfer_Kitchens.ItemsDGV.DataContext == null)
                {
                    dt = drv.DataView.ToTable().Clone();
                    dt.Columns.Add("Qty");
                    dt.Columns.Add(transfer_Kitchens.From_Kitchen.Text + " Qty");
                    dt.Columns.Add(transfer_Kitchens.From_Kitchen.Text + " Unit Cost");
                    dt.Columns.Add(transfer_Kitchens.From_Kitchen.Text + " total Cost");
                    dt.Columns.Add(transfer_Kitchens.To_Kitchen.Text + " Qty");
                    dt.Columns.Add(transfer_Kitchens.To_Kitchen.Text + " Unit Cost");
                    dt.Columns.Add(transfer_Kitchens.To_Kitchen.Text + " total Cost");
                    dt.Columns.Add("Recipe", typeof(bool));
                }
                else
                    dt = transfer_Kitchens.ItemsDGV.DataContext as DataTable;

                for (int i = 0; i < dt.Rows.Count; i++)
                    if (dt.Rows[i]["Code"].ToString() == drv.Row["Code"].ToString())
                    {
                        MessageBox.Show("This Item Already Exist");
                        return;
                    }

                dt.ImportRow(drv.Row);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = true;
                }
                dt.Columns["Recipe"].ReadOnly = false;
                dt.Rows[dt.Rows.Count - 1]["Recipe"] = true;
                dt.Columns["Recipe"].ReadOnly = true;
                dt.Columns["Qty"].ReadOnly = false;

                transfer_Kitchens.ItemsDGV.DataContext = dt;

                this.Close();
            }
            else if (main.GridMain.Children[0].GetType().Name == "AdjacmentInventory")
            {
                SqlConnection con = new SqlConnection(Classes.DataConnString);

                try
                {
                    if (adjacmentInventory.ItemsDGV.DataContext == null)
                    {
                        dt.Columns.Add("Code");
                        dt.Columns.Add("Name");

                        dt.Columns.Add("Name2");
                        dt.Columns.Add("Qty");
                        dt.Columns.Add("Adjacmentable Qty");
                        dt.Columns.Add("Variance");
                        dt.Columns.Add("Cost");
                        dt.Columns.Add("Unit");
                        dt.Columns.Add("Total Cost");
                        dt.Columns.Add("Recipe", typeof(bool));
                    }
                    else
                    {
                        dt = adjacmentInventory.ItemsDGV.DataContext as DataTable;
                        for (int i = 0; i < dt.Rows.Count; i++)
                            if (dt.Rows[i]["Code"].ToString() == (((DataRowView)grid.SelectedItem).Row.ItemArray[0]).ToString())
                            {
                                MessageBox.Show("Item Existed");
                                return;
                            }
                    }
                    double TotalCostOfREcipe = Convert.ToDouble(DT.Rows[0][3].ToString()) * Convert.ToDouble(DT.Rows[0][4].ToString());
                    dt.Rows.Add(DT.Rows[0][0], DT.Rows[0][1], DT.Rows[0][2], DT.Rows[0][3], "", "", DT.Rows[0][4], DT.Rows[0][5], TotalCostOfREcipe, true);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dt.Columns[i].ReadOnly = true;
                    }
                    dt.Columns["Adjacmentable Qty"].ReadOnly = false;
                    dt.Columns["Cost"].ReadOnly = false;
                    adjacmentInventory.NUmberOfItems.Text = dt.Rows.Count.ToString();
                    adjacmentInventory.ItemsDGV.DataContext = dt;
                    this.Close();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            }

            this.Close();
        }

        private void TextDataChange(object sender, TextChangedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            con.Open();
            DataTable DT = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            AllRecipesDGV.DataContext = null;

            try
            {
                if (main.GridMain.Children[0].GetType().Name == "Transfer_Resturant" || main.GridMain.Children[0].GetType().Name == "Transfer_Kitchens")
                {
                    if ((RadioByCode.IsChecked == true || RadioByName.IsChecked == true) && SearchTxt.Text != "")
                    {
                        if (RadioByCode.IsChecked == true && RadioByName.IsChecked == false)
                        {
                            string Q = "SELECT Code,CrossCode as 'Manual Code',Name,Name2,Unit FROM Setup_Recipes where IsActive='True' and Code Like '%" + SearchTxt.Text + "%'";
                            da = new SqlDataAdapter(Q, con);
                        }
                        else if (RadioByName.IsChecked == true && RadioByCode.IsChecked == false)
                        {
                            string Q = "SELECT Code,CrossCode as 'Manual Code',Name,Name2,Unit FROM Setup_Recipes where IsActive='True' and Name Like '%" + SearchTxt.Text + "%'";
                            da = new SqlDataAdapter(Q, con);
                        }
                    }
                    else
                    {
                        da = new SqlDataAdapter("SELECT Code,CrossCode as 'Manual Code',Name,Name2,Unit FROM Setup_Recipes where IsActive='True'", con);
                    }
                }
                else if(main.GridMain.Children[0].GetType().Name == "AdjacmentInventory")
                {
                    if ((RadioByCode.IsChecked == true || RadioByName.IsChecked == true) && SearchTxt.Text != "")
                    {
                        if (RadioByCode.IsChecked == true && RadioByName.IsChecked == false)
                        {
                            string Q = string.Format("SELECT dbo.RecipeQty.Recipe_ID AS Code, dbo.Setup_Recipes.Name, dbo.Setup_Recipes.Name2, dbo.RecipeQty.Qty, dbo.RecipeQty.Price, dbo.Setup_Recipes.Unit FROM dbo.RecipeQty INNER JOIN dbo.Setup_Recipes ON dbo.RecipeQty.Recipe_ID=dbo.Setup_Recipes.Code WHERE (dbo.Setup_Recipes.IsActive = 'True') AND (dbo.RecipeQty.Resturant_ID = {0}) AND (dbo.RecipeQty.Kitchen_ID = {1}) AND (dbo.RecipeQty.Recipe_ID Like '%{2}%')", adjacmentInventory.ValOfResturant, adjacmentInventory.ValOfKitchen, SearchTxt.Text);
                            //string Q = string.Format("select Recipe_ID as Code, (SELECT Name from Setup_Recipes where Code=Recipe_ID) as Name,(SELECT Name2 from Setup_Recipes where Code=Recipe_ID) as Name2, Qty, Price,(SELECT Unit from Setup_Recipes where Code=Recipe_ID) as Unit  from RecipeQty where IsActive='True' AND Resturant_ID={0} and Kitchen_ID={1} AND Code Like '%2%'", adjacmentInventory.ValOfResturant, adjacmentInventory.ValOfKitchen, SearchTxt.Text);
                            da = new SqlDataAdapter(Q, con);
                        }
                        else if (RadioByName.IsChecked == true && RadioByCode.IsChecked == false)
                        {
                            string Q = string.Format("SELECT dbo.RecipeQty.Recipe_ID AS Code, dbo.Setup_Recipes.Name, dbo.Setup_Recipes.Name2, dbo.RecipeQty.Qty, dbo.RecipeQty.Price, dbo.Setup_Recipes.Unit FROM dbo.RecipeQty INNER JOIN dbo.Setup_Recipes ON dbo.RecipeQty.Recipe_ID=dbo.Setup_Recipes.Code WHERE (dbo.Setup_Recipes.IsActive = 'True') AND (dbo.RecipeQty.Resturant_ID = {0}) AND (dbo.RecipeQty.Kitchen_ID = {1}) AND (dbo.Setup_Recipes.Name Like '%{2}%')", adjacmentInventory.ValOfResturant, adjacmentInventory.ValOfKitchen, SearchTxt.Text);
                            //string Q = string.Format("select Recipe_ID as Code, (SELECT Name from Setup_Recipes where Code=Recipe_ID) as Name,(SELECT Name2 from Setup_Recipes where Code=Recipe_ID) as Name2, Qty, Price,(SELECT Unit from Setup_Recipes where Code=Recipe_ID) as Unit  from RecipeQty where IsActive='True' AND Resturant_ID={0} and Kitchen_ID={1} AND Name Like '%2%'", adjacmentInventory.ValOfResturant, adjacmentInventory.ValOfKitchen, SearchTxt.Text);
                            da = new SqlDataAdapter(Q, con);
                        }
                    }
                    else
                    {
                        da = new SqlDataAdapter(string.Format("SELECT dbo.RecipeQty.Recipe_ID AS Code, dbo.Setup_Recipes.Name, dbo.Setup_Recipes.Name2, dbo.RecipeQty.Qty, dbo.RecipeQty.Price, dbo.Setup_Recipes.Unit FROM dbo.RecipeQty INNER JOIN dbo.Setup_Recipes ON dbo.RecipeQty.Recipe_ID=dbo.Setup_Recipes.Code WHERE (dbo.Setup_Recipes.IsActive = 'True') AND (dbo.RecipeQty.Resturant_ID = {0}) AND (dbo.RecipeQty.Kitchen_ID = {1})", adjacmentInventory.ValOfResturant, adjacmentInventory.ValOfKitchen), con);
                    }
                }
                else
                {
                    if ((RadioByCode.IsChecked == true || RadioByName.IsChecked == true) && SearchTxt.Text != "")
                    {
                        AllRecipesDGV.DataContext = null;
                        if (RadioByCode.IsChecked == true && RadioByName.IsChecked == false)
                        {
                            string Q = "SELECT Code,Name,Name2,(select Name From Setup_RecipeCategory where Code=Category_ID) as Category,(select Name From Setup_RecipeSubCategories where Code=SubCategory_ID) as 'SUB Category',Unit FROM Setup_Recipes where IsActive='True' and Code Like '%" + SearchTxt.Text + "%'";
                            da = new SqlDataAdapter(Q, con);
                        }
                        else if (RadioByName.IsChecked == true && RadioByCode.IsChecked == false)
                        {
                            string Q = "SELECT Code,Name,Name2,(select Name From Setup_RecipeCategory where Code=Category_ID) as Category,(select Name From Setup_RecipeSubCategories where Code=SubCategory_ID) as 'SUB Category',Unit,UnitQty FROM Setup_Recipes where IsActive='True' and Name Like '%" + SearchTxt.Text + "%'";
                            da = new SqlDataAdapter(Q, con);
                        }
                    }
                    else
                    {
                        da = new SqlDataAdapter("SELECT Code,Name,Name2,(select Name From Setup_RecipeCategory where Code=Category_ID) as Category,(select Name From Setup_RecipeSubCategories where Code=SubCategory_ID) as 'SUB Category',Unit,UnitQty FROM Setup_Recipes where IsActive='True'", con);
                    }
                }
                da.Fill(DT);
                AllRecipesDGV.DataContext = DT; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            con.Close();
        }
    }
}