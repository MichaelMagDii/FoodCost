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
    /// Interaction logic for Unit_Picker.xaml
    /// </summary>
        
    public partial class Unit_Picker : Window
    {
        SqlConnection con;
        PurchaseOrder purchaseOrder;
        Recipes recipes;
        public Unit_Picker(PurchaseOrder _this)
        {
            InitializeComponent();

            string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
            con = new SqlConnection(connString);

            purchaseOrder = _this;
            LoadedUnits();
        }
        public Unit_Picker(Recipes _Recipes)
        {
            InitializeComponent();
            string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
            con = new SqlConnection(connString);
            recipes = _Recipes;
            LoadedUnits();
        }

        private void LoadedUnits()
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            if (main.GridMain.Children[0].GetType().Name == "RecieveOrder")
            {
                try
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(string.Format("select * from Units where Name in((select Unit from Setup_Items where Code = '{0}'),(select Unit2 from Setup_Items where Code = '{0}'),(select Unit from Setup_Items where Code = '{0}'))", (purchaseOrder.ItemsDGV.SelectedItem as DataRowView).Row["Code"]), con))
                        da.Fill(dt);
                    UnitDgv.DataContext = dt;
                }
                catch { }
            }
            else if(main.GridMain.Children[0].GetType().Name == "Recipes" && (recipes.RecipesDGV.SelectedItem as DataRowView).Row.ItemArray[0].ToString() != "")
            {
                try
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(string.Format("select * from Units where Name in((select Unit from Setup_Items where Code = '{0}'),(select Unit2 from Setup_Items where Code = '{0}'),(select Unit from Setup_Items where Code = '{0}'))", (recipes.RecipesDGV.SelectedItem as DataRowView).Row.ItemArray[0]), con))
                        da.Fill(dt);
                    UnitDgv.DataContext = dt;
                }
                catch { }
            }
            else if (main.GridMain.Children[0].GetType().Name == "Recipes" && (recipes.RecipesDGV.SelectedItem as DataRowView).Row.ItemArray[1].ToString() != "")
            {
                DataTable dt = new DataTable();
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(string.Format("select * from Units"),con))
                    da.Fill(dt);
                UnitDgv.DataContext = dt;
            }
        }

        private void UnitDgv_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;

            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                if (main.GridMain.Children[0].GetType().Name == "RecieveOrder")
                {
                    DataTable dt = purchaseOrder.ItemsDGV.DataContext as DataTable;
                    dt.Rows[purchaseOrder.ItemsDGV.SelectedIndex]["Unit"] = (grid.SelectedItem as DataRowView).Row["Name"];
                    purchaseOrder.DataContext = dt;
                    this.Close();
                }
                else if(main.GridMain.Children[0].GetType().Name == "Recipes")
                {
                    DataTable dt = recipes.RecipesDGV.DataContext as DataTable;
                    dt.Columns["Recipe_Unit"].ReadOnly = false;
                    dt.Rows[recipes.RecipesDGV.SelectedIndex]["Recipe_Unit"] = (grid.SelectedItem as DataRowView).Row["Name"];
                    dt.Columns["Recipe_Unit"].ReadOnly = true;
                    recipes.RecipesDGV.DataContext = dt;
                    this.Close();
                }
            }
        }
    }
}
