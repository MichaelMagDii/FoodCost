using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Shapes;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for Setup_station.xaml
    /// </summary>
    public partial class Setup_Station : Window
    {
        public Setup_Station()
        {
            InitializeComponent();
            LoadAllResturant();
        }
        private void LoadAllResturant()
        {
            DataTable TheRestaurants = Classes.RetrieveResturants();
            for (int i = 0; i < TheRestaurants.Rows.Count; i++)
            {
                Restaurant_cbx.Items.Add(TheRestaurants.Rows[i][0].ToString());
            }
        }

        private void Restaurant_cbx_Selected(object sender, SelectionChangedEventArgs e)
        {
            DataTable TheKitchens = new DataTable();
            if(Restaurant_cbx.SelectedItem != null)
            {
                TheKitchens = Classes.RetrieveKitchens(Restaurant_cbx.SelectedItem.ToString());
                for(int i=0;i<TheKitchens.Rows.Count;i++)
                {
                    Kitchen_cbx.Items.Add(TheKitchens.Rows[i][0].ToString());
                }
            }
        }
 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (WorkStation_no.Text == "" || Restaurant_cbx.Text == "" || Kitchen_cbx.Text == "")
            {
                MessageBox.Show("Null Fields");
                return;
            }
            string ResturantCode = Classes.RetrieveRestaurantCode(Restaurant_cbx.SelectedItem.ToString());
            string KitchenCode = Classes.RetrieveKitchenCode(Kitchen_cbx.SelectedItem.ToString(), Restaurant_cbx.SelectedItem.ToString());


            if (WorkStation_no.Text.Length == 1)
                WorkStation_no.Text = "0" + WorkStation_no.Text;
            if (ResturantCode.Length == 1)
                ResturantCode = "0" + ResturantCode;
            if (KitchenCode.Length == 1)
                KitchenCode = "0" + KitchenCode;

            string WorkStationInfo = WorkStation_no.Text + "," + ResturantCode + "," + KitchenCode+","+ WorkStation_Name.Text;
            File.WriteAllText("Workstation.dll", WorkStationInfo);
            Classes.LogTable(WorkStationInfo, WorkStation_Name.Text, "WS", "Update");

            MessageBox.Show("Saved");
            Classes.GetWS();
        }
    }
}
