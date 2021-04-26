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
    /// Interaction logic for Close_Month.xaml
    /// </summary>
    public partial class Close_Month : Window
    {
        public static int TheIncrementValue = 0;
        string where, Month, Year, PMonth, PYear;
        Dictionary<string, List<string>> FilterDic = new Dictionary<string, List<string>>();
        DataTable DTCurrentMonth, DTPreviousMonth;


        private void Open_Click(object sender, RoutedEventArgs e)
        {
            where = "Month = '" + PMonth + "' and Year = '" + PYear + "'";
            Classes.UpdateCell("isClosed", "0", where, "Setup_Fiscal_Period");
            LoadMonths();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProgressB.Maximum = 10;
                Recalculate.ReCalculate_Cost_Qty(DTCurrentMonth.Rows[0], DTPreviousMonth);
                ProgressB.Value = 50;
                ProgressB.UpdateLayout();
                Recalculate.CloseMonth(DTCurrentMonth.Rows[0]);
                ProgressB.Value = 90;
                where = "Month = '" + Month + "' and Year = '" + Year + "'";
                Classes.UpdateCell("isClosed", "1", where, "Setup_Fiscal_Period");
                ProgressB.Value = 100;
                LoadMonths();
                MessageBox.Show("End Month Done Sucssesfully !!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       
        public Close_Month()
        {
            InitializeComponent();
            LoadMonths();
            ProgressB.Value = 0;
        }

        private void LoadMonths()
        {
            try
            {
                DTCurrentMonth =  Classes.RetrieveData("top 1 *", "isClosed = 'False'", "Setup_Fiscal_Period");
                if (DTCurrentMonth.Rows.Count != 0)
                {
                    Month = DTCurrentMonth.Rows[0]["Month"].ToString();
                    Year = DTCurrentMonth.Rows[0]["Year"].ToString();

                    CurrMonth.Content = "Month" + Month + "," + Year;

                    DTPreviousMonth = Classes.RetrieveData("top 1 *", "isClosed = 'True' order by [From] desc", "Setup_Fiscal_Period");
                   
                    if (DTPreviousMonth.Rows.Count != 0)
                    {
                        PMonth = DTPreviousMonth.Rows[0]["Month"].ToString();
                        PYear = DTPreviousMonth.Rows[0]["Year"].ToString();

                        PrevMonth.Content = "Month" + PMonth + "," + PYear;
                        //OpenBtn.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        PrevMonth.Content = "";
                        OpenBtn.Visibility = Visibility.Hidden;
                    }
                }
                else
                    MessageBox.Show("Please Create A New Fiscal Period");
            }
            catch(Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

       
    }
}
