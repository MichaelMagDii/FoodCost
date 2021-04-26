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
    /// Interaction logic for AllVendor.xaml
    /// </summary>
    public partial class AllVendor : Window
    {
        public AllVendor()
        {
            InitializeComponent();
        }
        NewItems_Food_Cost newFoodCost;
        public AllVendor(NewItems_Food_Cost _newFoodCost)
        {
            InitializeComponent();
            newFoodCost = _newFoodCost;
            LoadAllVendors("Items");
        }

        PurchaseOrder purchaseOrder;
        public AllVendor(PurchaseOrder _purchaseOrder)
        {
            InitializeComponent();
            purchaseOrder = _purchaseOrder;
            LoadAllVendors("Purchase");
        }

        private void LoadAllVendors(string LoadFrom)
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = "";
                if (LoadFrom == "Items" || LoadFrom== "Purchase")
                    s = "select Code,Name,IsActive as Active from Vendors where IsActive='True'";
               
                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                    da.Fill(dt);

                VendorsDGV.DataContext = dt;
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

        private void VendorsDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    if (main.GridMain.Children[0].GetType().Name == "NewItems_Food_Cost")
                    {
                        newFoodCost.PrefVendortxt.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;
                        this.Close();
                    }
                    else if(main.GridMain.Children[0].GetType().Name == "PurchaseOrder")
                    {
                        purchaseOrder.Vendor.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;
                        this.Close();
                    }
                    
                }
            }
        }
    }
}
