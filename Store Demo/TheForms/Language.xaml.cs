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

namespace Food_Cost.TheForms
{
    /// <summary>
    /// Interaction logic for Language.xaml
    /// </summary>
    public partial class Language : Window
    {
        public Language()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Val = "";
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            if (Language_cbx.Text.Equals(""))
                return;

            if (Language_cbx.Text == "English")
            { Val = "English"; }
            else
            { Val = "Arabic"; }
            con.Open();
            SqlCommand cmd = new SqlCommand(string.Format("update Setup_Code set Language='{0}'", Val), con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Saved");
            this.Close();
        }
    }
}
