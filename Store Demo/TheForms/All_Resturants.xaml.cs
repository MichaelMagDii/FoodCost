using CrystalDecisions.Web.HtmlReportRender;
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
    /// Interaction logic for All_Resturants.xaml
    /// </summary>
    public partial class All_Resturants : Window
    {
        Transfer_Kitchens transfer_Kitchens;
        Transfer_Resturant Transfer_Resturant;
        GenerateBatch generateBatch;
        PurchaseOrder purchaseOrder;
        RecieveOrder recieveOrder; 
        string Resturant;
        public All_Resturants(RecieveOrder _recieveOrder)
        {
            InitializeComponent();
            recieveOrder = _recieveOrder;
            LoadResturants("RecieveOrder");
        }
        public All_Resturants(PurchaseOrder _purchaseOrder)
        {
            InitializeComponent();
            purchaseOrder = _purchaseOrder;
            LoadResturants("PurchaseOrder");
        }

        public All_Resturants(GenerateBatch _GenerateBatch)
        {
            InitializeComponent();
            generateBatch = _GenerateBatch;
            LoadResturants("GenerateBatch");
        }

        public All_Resturants(Transfer_Resturant _Transfer_Resturant,string _resturant)
        {
            InitializeComponent();
            Transfer_Resturant = _Transfer_Resturant;
            Resturant = _resturant;
            LoadResturants("Transfer_Resturant");
        }
        public All_Resturants(Transfer_Kitchens _transfer_Kitchens, string _Resturant)
        {
            InitializeComponent();
            transfer_Kitchens = _transfer_Kitchens;
            Resturant = _Resturant;
            LoadResturants("TransferKitchen");
        }

        private void LoadResturants(string Transfer)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = "";
                if (Transfer == "TransferKitchen" || Transfer == "GenerateBatch" || Transfer == "PurchaseOrder" || Transfer == "RecieveOrder")
                     s = "select Code,Name,Name2,IsMain,IsActive from Setup_Restaurant where IsActive='True'";
                else if (Transfer == "Transfer_Resturant")
                {
                    if (Resturant == "From_Resturant")
                        s = "select Code,Name,Name2,IsMain,IsActive from Setup_Restaurant where IsActive='True'";
                    else
                        s = string.Format("select Code,Name,Name2,IsMain,IsActive from Setup_Restaurant where IsActive='True' and Name <> '{0}'", Transfer_Resturant.From_Resturant.Text);
                }
                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                    da.Fill(dt);

                ResturantsDGV.DataContext = dt;
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
        
        private void ResturantDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = Application.Current.MainWindow as MainWindow;

            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    
                    if (main.GridMain.Children[0].GetType().Name == "Transfer_Resturant")
                    {
                        if (Resturant == "From_Resturant")
                            Transfer_Resturant.From_Resturant.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;
                        else if (Resturant == "To_Resturant")
                            Transfer_Resturant.ToResturant.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;
                        
                        this.Close();
                    }
                    else if(main.GridMain.Children[0].GetType().Name == "Transfer_Kitchens")
                    {
                            transfer_Kitchens.Resturant.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;

                        this.Close();
                    }
                    else if(main.GridMain.Children[0].GetType().Name == "PurchaseOrder")
                    {
                        purchaseOrder.ShipTo.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;
                        purchaseOrder.RestaurantCode = (grid.SelectedItem as DataRowView).Row["Code"] as string;
                        this.Close();
                    }
                    else if (main.GridMain.Children[0].GetType().Name == "RecieveOrder")
                    {
                        recieveOrder.Restaurant.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;
                        recieveOrder.RestaurantId = (grid.SelectedItem as DataRowView).Row["Code"].ToString();
                        this.Close();
                    }
                    else
                    {
                        generateBatch.StoreIDcbx.Text = (grid.SelectedItem as DataRowView).Row["Name"] as string;
                        generateBatch.valofStore = ((DataRowView)ResturantsDGV.SelectedItems[0]).Row.ItemArray[0].ToString();
                        this.Close();
                    }
                }
            }
        }
    }
}