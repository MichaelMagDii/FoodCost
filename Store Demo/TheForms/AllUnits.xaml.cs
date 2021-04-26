using System;
using System.Collections.Generic;
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
    /// Interaction logic for AllUnits.xaml
    /// </summary>
    public partial class AllUnits : Window
    {
        public AllUnits()
        {
            InitializeComponent();
        }

        NewItems_Food_Cost newFoodCost;
        string ItemUnit = "";
        public AllUnits(NewItems_Food_Cost _newFoodCost,string UnitNum)
        {
            InitializeComponent();
            newFoodCost = _newFoodCost;
            ItemUnit = UnitNum;
            LoadAllVendors("Items");
        }

        Recipes recipes;
        string RecipeForm;
        public AllUnits(Recipes _Recipes,string From)
        {
            InitializeComponent();
            RecipeForm = From;
            recipes = _Recipes;
            LoadAllVendors("Recipes");
        }

        Units units;
        string whichUnit = "";
        public AllUnits(Units _Units,string BaseUnit,string whichValue)
        {
            InitializeComponent();
            units = _Units;
            ItemUnit = BaseUnit;
            whichUnit = whichValue;
            LoadAllVendors("Units");
        }

        private void LoadAllVendors(string LoadFrom)
        {
            UnitsDGV.DataContext = null;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = "";
                if (LoadFrom == "Items" && ItemUnit == "Unit")
                    s = string.Format("select Code,Name,IsActive from Units where IsActive='True'");
                else if (LoadFrom == "Items")
                    s = string.Format("select Code,Name,IsActive from Units where IsActive='True' and Name <> '{0}'", newFoodCost.unit2.Text);
                else if (LoadFrom == "Recipes" && RecipeForm == "MainUnit")
                    s = string.Format("select Code,Name,IsActive from Units where IsActive='True'");
                else if (LoadFrom == "Recipes" && RecipeForm == "DataGrid" && (recipes.RecipesDGV.SelectedItem as DataRowView).Row.ItemArray[0].ToString() != "")
                    s = string.Format("select Code,Name,IsActive from Units where Name in((select Unit from Setup_Items where Code = '{0}'),(select Unit2 from Setup_Items where Code = '{0}'),(select Unit from Setup_Items where Code = '{0}'))", (recipes.RecipesDGV.SelectedItem as DataRowView).Row.ItemArray[0]);
                else if (LoadFrom == "Recipes" && RecipeForm == "DataGrid" && (recipes.RecipesDGV.SelectedItem as DataRowView).Row.ItemArray[1].ToString() != "")
                    s = string.Format("select Code,Name,IsActive from Units");
                else if (LoadFrom == "Units")
                    s = string.Format("select Code,Name,IsActive from Units where IsActive='True' and Name <> '{0}'", ItemUnit);

                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                    da.Fill(dt);

                UnitsDGV.DataContext = dt;
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

        private void UnitsDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {

                    if (main.GridMain.Children[0].GetType().Name == "NewItems_Food_Cost")
                    {
                        if(ItemUnit == "Unit")
                        {
                            newFoodCost.unit.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;
                        }
                        else if (ItemUnit == "Unit1")
                        {
                            newFoodCost.unit2.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;
                        }
                        else
                        {
                            newFoodCost.unit_txt1.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;

                        }
                    }
                    else if (main.GridMain.Children[0].GetType().Name == "Recipes")
                    {
                        if (RecipeForm == "DataGrid")
                        {
                            DataTable dt = recipes.RecipesDGV.DataContext as DataTable;
                            dt.Columns["Recipe_Unit"].ReadOnly = false;
                            dt.Rows[recipes.RecipesDGV.SelectedIndex]["Recipe_Unit"] = (grid.SelectedItem as DataRowView).Row["Name"];
                            dt.Columns["Recipe_Unit"].ReadOnly = true;
                            recipes.RecipesDGV.DataContext = dt;
                        }
                        else if(RecipeForm == "MainUnit")
                        {
                           recipes.Unitstxt.Text= (grid.SelectedItem as DataRowView).Row["Name"].ToString();
                        }
                    }
                    else if(main.GridMain.Children[0].GetType().Name == "Units")
                    {
                        if(whichUnit == "BaseUnit")
                        {
                           units.BaseUnit.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;
                        }
                        else
                        {
                            units.Secondunit.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string; 
                        }
                    }
                    this.Close();

                }
            }
        }
    }
}
