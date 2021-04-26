using System;
using System.Collections.Generic;
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
    /// Interaction logic for ItemsDefinitionSetup.xaml
    /// </summary>
    public partial class ItemsDefinitionSetup : Window
    {
        public ItemsDefinitionSetup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            if (Num_cbx.Text.Equals(""))
                return;

            con.Open(); 
            SqlCommand cmd = new SqlCommand(string.Format("update Setup_Code set TreeDefinition='{0}'", Num_cbx.Text), con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Saved");
            this.Close();
        }
    }
}
