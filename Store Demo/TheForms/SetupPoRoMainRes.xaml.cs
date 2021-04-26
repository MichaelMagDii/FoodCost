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
    /// Interaction logic for SetupPoRoMainRes.xaml
    /// </summary>
    public partial class SetupPoRoMainRes : Window
    {
        public SetupPoRoMainRes()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Val = "";
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            if (Num_cbx.Text.Equals(""))
                return;

            if (Num_cbx.Text == "Yes")
            { Val = "True"; }
            else
            { Val = "False"; }
            con.Open();
            SqlCommand cmd = new SqlCommand(string.Format("update Setup_Code set POROMainRes='{0}'", Val), con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Saved");
            this.Close();
        }
    }
}
