using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<int> ThetLogin = new List<int>();
        public static List<int> LogedID= new List<int>();
        public static string[] ArrAuthenctication= new string[24];
        public static Dictionary<string, List<string>> AuthenticationData = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<string>> AuthenticationData2 = new Dictionary<string, List<string>>();
        public static string Authentication = "";
        public static string UserID = "";
        public static string UserID2 = "";
        public static string UserName = "";
        public static string Password = "";
        public static string JobID = "";
        public static string CurrentYear = ""; 
        public static string CurrentMonth = "";
        public static string MonthCost = ""; public static string MonthQty = "";
        public MainWindow()
        {
            InitializeComponent();
            Classes.TheConnectionString();
            Classes.GetWS();
            Classes.GetDateFormate();
            Classes.GetDateTimeFormate();
            //DateTime DatetimeNow = new DateTime();
            //DatetimeNow = DateTime.Now;
            //if (DatetimeNow.Date > Convert.ToDateTime("11/1/2020"))
            //{
            //    MessageBox.Show("Please Check The Licence");
            //    System.Windows.Application.Current.Shutdown();
            //    return;
            //}
            Classes.UpdateDateFormat();
            Classes.Language();
            //Calculation.Calculate();
            NameofForm.Text = "Food Cost";
            //int num = (DateTime.Now.Hour* DateTime.Now.Day) +0x21;
            Classes.RetrieveTheYearAndMonth();
            //Check_Month();
            LogIn logIn = new LogIn();
            logIn.ShowDialog();
            if(logIn.CheckToLogin == false)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        //private void Check_Month()
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
        //    SqlConnection con = new SqlConnection(connString);
        //    try
        //    {
        //        con.Open();

        //        SqlCommand cmd = new SqlCommand("select concat(substring(datename(month,getdate()),0,4),',',year(getdate()))", con);
        //        string Current_Month = cmd.ExecuteScalar().ToString();
        //        con.Close();

        //        con.Open();
        //        cmd = new SqlCommand("select concat(substring(datename(month,Date),0,4),',',year(Date)) from Close_Month", con);
        //        SqlDataReader reader = cmd.ExecuteReader();


        //       while(reader.Read())
        //        {
        //            if (reader[0].ToString() == Current_Month)
        //                return;
        //        }

        //        con.Close();

        //        con.Open();
        //        cmd = new SqlCommand("insert into Close_Month values ((select getdate()))",con);
        //        cmd.ExecuteNonQuery();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if ((sender as ListView).SelectedItems.Count > 0)
            {
                BackGroundImage.Visibility = Visibility.Hidden;
                UserControl usc = null;
                GridMain.Children.Clear();

                try
                {
                    switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
                    {
                        case "Items":
                            usc = new Food_Cost.NewItems_Food_Cost();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Items";
                            break;

                        case "PurchaseOrder":
                            usc = new PurchaseOrder();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Purchase Orders";
                            break;

                        case "RecieveOrder":
                            usc = new RecieveOrder();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Recieve Orders";
                            break;


                        case "Transfer_Kitchen":
                            usc = new Transfer_Kitchens();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Transfer Kitchen";
                            break;

                        case "Transfer_Resturant":
                            usc = new Transfer_Resturant();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Transfer Restaurant";
                            break;

                        case "Inventory":
                            usc = new Store_Sertup();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Restaurants";
                            break;

                        case "Vendors":
                            usc = new Vendors();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Vendors";
                            break;

                        case "KitcheItemsn":
                            usc = new KitcheItemsn();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Kitchen Items";
                            break;

                        case "Recipes":
                            usc = new Recipes();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Recipes";
                            break;

                        case "GenerateBatch":
                            usc = new GenerateBatch();  
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Generate Batch";
                            break;


                        case "CategoriesAndSub":
                            usc = new CategoriesAndSub();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Recipe Category And Recipe SUbCategory";
                            break;

                        case "ProcessBulkItems":
                            usc = new ProcessBulkItem();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Process Bulk Items";
                            break;

                        case "Adjustment":
                            usc = new AdjacmentInventory();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "adjustment";
                            break;

                        case "AdjustmentReasons":
                            usc = new AdjacmentsReasons();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "adjustment Reason";
                            break;

                        case "StockInventory":
                            usc = new StockInventory();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Stock Inventory";
                            break;

                        case "PhysicalInventory":
                            usc = new PhysicalInventory();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Physical Inventory";
                            break;

                        case "OutletRecipeSubtraction":
                            usc = new OutletRecipeSubtraction();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "POS Recipe Subtraction";
                            break;

                        case "Users":
                            usc = new Users();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "User";
                            break;

                        case "Units":
                            usc = new Units();
                            GridMain.Children.Add(usc);
                            NameofForm.Text = "Units";
                            break;

                        case "CenterSetup":
                            setup window = new setup();
                            window.Owner = this;
                            window.ShowDialog();
                            NameofForm.Text = "Setup Center";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogIn logIn = new LogIn();
            
            logIn.Owner = this;
            logIn.ShowDialog();
        }
    }
}