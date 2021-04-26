using System;
using System.Collections.Generic;
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
    /// Interaction logic for ConnectionString.xaml
    /// </summary>
    public partial class ConnectionString : Window
    {
        public ConnectionString()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string folder = System.IO.File.ReadAllText("PosCon.txt");
                //Pass the filepath and filename to the StreamWriter Constructor
                //StreamWriter sw = new StreamWriter(folder);

                //Write a line of text
                File.WriteAllText("PosCon.txt",ConnectionsString.Text);

                //Close the file
                //sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("");
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
        
            //File.WriteAllText("PosCon.txt", ConnectionsString.Text.ToString());
            //MessageBox.Show("Connection Saved");
    }
}
