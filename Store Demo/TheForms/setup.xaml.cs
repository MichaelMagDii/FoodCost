using Food_Cost.TheForms;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for setup.xaml
    /// </summary>
    public partial class setup : Window
    {
        public setup()
        {
            InitializeComponent();
            //LoadOpenMonths();
        }

        private void LoadOpenMonths()
        {
            //string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
            //SqlConnection con = new SqlConnection(connString);
            //try
            //{
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand("select concat(substring(datename(month,Date),0,4),',',year(Date)) from Close_Month", con);
            //    SqlDataReader reader = cmd.ExecuteReader();

            //    List<CloseMonth_List> l = new List<CloseMonth_List>();
            //    while (reader.Read())
            //        l.Add(new CloseMonth_List() { Month = reader[0].ToString(), ButtonText = "Close" });

            //    CloseMonth_ListView.ItemsSource = l;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    con.Close();
            //}
        }

        class CloseMonth_List
        {
            public string Month { get; set; }
            public string ButtonText { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Setup_Station setup_Station = new Setup_Station();
            setup_Station.Owner = this;
            setup_Station.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Fiscal_Period fiscal_Period = new Fiscal_Period();
            fiscal_Period.Owner = this;
            fiscal_Period.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close_Month close_Month = new Close_Month();
            close_Month.Owner = this;
            close_Month.ShowDialog();
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            Forms.ReportsForm reportsfrm = new Forms.ReportsForm();
            //reportsfrm.Owner = this;
            reportsfrm.ShowDialog();
        }

        private void Connection_Click(object sender, RoutedEventArgs e)
        {
            ConnectionString connectionString = new ConnectionString();
            connectionString.Owner = this;
            connectionString.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ItemsDefinitionSetup itemsDefinitionSetup = new ItemsDefinitionSetup();
            itemsDefinitionSetup.Owner = this;
            itemsDefinitionSetup.ShowDialog();
        }

        private void GenerateBatchBtn(object sender, RoutedEventArgs e)
        {
            SetupGenerateBtch setupGenerateBatch = new SetupGenerateBtch();
            setupGenerateBatch.Owner = this;
            setupGenerateBatch.ShowDialog();
        }

        private void PO_ROSetup(object sender, RoutedEventArgs e)
        {
            SetupPoRoMainRes setupPOROMainRes = new SetupPoRoMainRes();
            setupPOROMainRes.Owner = this;
            setupPOROMainRes.ShowDialog();
        }
    }
}
