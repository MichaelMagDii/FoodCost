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
using System.Data;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for Fiscal_Period.xaml
    /// </summary>
    public partial class Fiscal_Period : Window
    {
        HashSet<string> SetStatus = new HashSet<string>();

        private void load_years()
        {
            string cols = "Year varchar(50),Month varchar(50),[Month Type] varchar(50),[From] date,[To] date,isClosed bit";
            Classes.CreateTable("Setup_Fiscal_Period", cols);
            DataTable Years = Classes.RetrieveData("DISTINCT(Year)", "Setup_Fiscal_Period");
            foreach (DataRow year in Years.Rows)
            {
                CBCreatedYears.Items.Add(year[0]);
            }
        }

        private void Close_All()
        {
            for (int i = 0; i < 13; i++)
            {
                DatePicker From = FindName("from" + (i + 1)) as DatePicker;
                DatePicker To = FindName("to" + (i + 1)) as DatePicker;
                From.IsEnabled = false;
                To.IsEnabled = false;
            }
        }

        private void Open_All()
        {
            for (int i = 0; i < 13; i++)
            {
                DatePicker From = FindName("from" + (i + 1)) as DatePicker;
                DatePicker To = FindName("to" + (i + 1)) as DatePicker;
                From.IsEnabled = true;
                To.IsEnabled = true;
            }
        }

        private void Clear_All()
        {
            for (int i = 0; i < 13; i++)
            {
                DatePicker From = FindName("from" + (i + 1)) as DatePicker;
                DatePicker To = FindName("to" + (i + 1)) as DatePicker;
                From.Text = "";
                To.Text = "";
            }
            CBCreatedYears.Text = "";
            MonthType_cbx.Text = "";
            Year.Text = "";
        }

        private void insert()
        {
            string where = "Year = '" + Year.Text + "'";
            Classes.DeleteRows(where, "Setup_Fiscal_Period");

            int n = 12;
            if (MonthType_cbx.SelectedIndex == 1)
                n = 13;

            for (int i = 0; i < n; i++)
            {
                DatePicker From = FindName("from" + (i + 1)) as DatePicker;
                DatePicker To = FindName("to" + (i + 1)) as DatePicker;
                string values = "'" + Year.Text + "','" + (i + 1).ToString() + "','" + MonthType_cbx.Text + "','";
                values += Convert.ToDateTime(From.Text) + "','" + Convert.ToDateTime(To.Text).AddHours(23.9999) + "','" + "0'";
                Classes.InsertRow("Setup_Fiscal_Period", values);
            }
            if (!CBCreatedYears.Items.Contains(Year.Text))
            {
                CBCreatedYears.Items.Add(Year.Text);
            }
            
            MessageBox.Show("Saved");

        }

        public Fiscal_Period()
        {
            InitializeComponent();
            MonthType_cbx.SelectedIndex = 0;
            load_years();
            Close_All();
            BtnNew.IsEnabled = true;
            BtnDelete.IsEnabled = false;
            BtnSave.IsEnabled = false;
            BtnUndo.IsEnabled = false;
            BtnEdit.IsEnabled = false;
            BtnExit.IsEnabled = true;
            MonthType_cbx.IsEnabled = false;
            Year.IsEnabled = false;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonthType_cbx.Text != "")
            {
                if (MonthType_cbx.SelectedIndex == 0)
                {
                    Month13_StackPanel.Visibility = Visibility.Hidden;
                }
                else
                    Month13_StackPanel.Visibility = Visibility.Visible;
            }
        }

        private void Year_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string year = (e.Source as TextBox).Text;

            if (MonthType_cbx.SelectedIndex == 0)
            {
                from1.Text = "1/1/" + year;
                from2.Text = "2/1/" + year;
                from3.Text = "3/1/" + year;
                from4.Text = "4/1/" + year;
                from5.Text = "5/1/" + year;
                from6.Text = "6/1/" + year;
                from7.Text = "7/1/" + year;
                from8.Text = "8/1/" + year;
                from9.Text = "9/1/" + year;
                from10.Text = "10/1/" + year;
                from11.Text = "11/1/" + year;
                from12.Text = "12/1/" + year;

                to1.Text = "1/30/" + year;
                to2.Text = "2/28/" + year;
                to3.Text = "3/31/" + year;
                to4.Text = "4/30/" + year;
                to5.Text = "5/31/" + year;
                to6.Text = "6/30/" + year;
                to7.Text = "7/31/" + year;
                to8.Text = "8/31/" + year;
                to9.Text = "9/30/" + year;
                to10.Text = "10/31/" + year;
                to11.Text = "11/30/" + year;
                to12.Text = "12/31/" + year;
            }
            else if (MonthType_cbx.SelectedIndex == 1)
            {
                from1.Text = "1/1/" + year;
                from2.Text = "1/29/" + year;
                from3.Text = "2/26/" + year;
                from4.Text = "3/26/" + year;
                from5.Text = "4/23/" + year;
                from6.Text = "5/21/" + year;
                from7.Text = "6/18/" + year;
                from8.Text = "7/16/" + year;
                from9.Text = "8/13/" + year;
                from10.Text = "9/10/" + year;
                from11.Text = "10/8/" + year;
                from12.Text = "11/2/" + year;
                from13.Text = "12/3/" + year;

                to1.Text = "1/28/" + year;
                to2.Text = "2/25/" + year;
                to3.Text = "3/25/" + year;
                to4.Text = "4/22/" + year;
                to5.Text = "5/20/" + year;
                to6.Text = "6/17/" + year;
                to7.Text = "7/15/" + year;
                to8.Text = "8/15/" + year;
                to9.Text = "9/9/" + year;
                to10.Text = "10/7/" + year;
                to11.Text = "11/4/" + year;
                to12.Text = "12/2/" + year;
                to13.Text = "12/31/" + year;
            }
        }

        private void CreatedYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnEdit.IsEnabled = true;
            DataTable MonthsforYear = Classes.RetrieveData("*", "Year = '" + CBCreatedYears.SelectedItem + "'", "Setup_Fiscal_Period");
            if (MonthsforYear.Rows.Count != 0)
            {
                if (MonthsforYear.Rows[0]["Month Type"].ToString() == "Type2")
                {
                    Month13_StackPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    Month13_StackPanel.Visibility = Visibility.Hidden;
                }
                Year.Text = CBCreatedYears.SelectedItem.ToString();
                MonthType_cbx.Text = MonthsforYear.Rows[0]["Month Type"].ToString();

                SetStatus = new HashSet<string>();
                for (int i = 0; i < MonthsforYear.Rows.Count; i++)
                {
                    DatePicker From = FindName("from" + (i + 1)) as DatePicker;
                    DatePicker To = FindName("to" + (i + 1)) as DatePicker;
                    From.Text = MonthsforYear.Rows[i]["From"].ToString();
                    To.Text = MonthsforYear.Rows[i]["TO"].ToString();
                    SetStatus.Add(MonthsforYear.Rows[i]["isClosed"].ToString());
                }
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Clear_All();
            Open_All();
            BtnUndo.IsEnabled = true;
            BtnExit.IsEnabled = true;
            MonthType_cbx.IsEnabled = true;
            Year.IsEnabled = true;
            BtnSave.IsEnabled = true;
            BtnDelete.IsEnabled = false;
            BtnNew.IsEnabled = false;
            BtnEdit.IsEnabled = false;
            CBCreatedYears.IsEnabled = false;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!SetStatus.Contains("True"))
            {
                BtnUndo.IsEnabled = true;
                BtnSave.IsEnabled = true;
                BtnDelete.IsEnabled = true;
                MonthType_cbx.IsEnabled = true;
                Year.IsEnabled = true;
                BtnNew.IsEnabled = false;
                BtnEdit.IsEnabled = false;
                Open_All();
            }
            else
            {
                MessageBox.Show("Please open all months in this year before doing this fucking action Motherfucker");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Year.Text != "")
                {
                    insert();
                    Clear_All();
                    Close_All();
                    BtnNew.IsEnabled = true;
                    BtnExit.IsEnabled = true;
                    CBCreatedYears.IsEnabled = true;
                    BtnEdit.IsEnabled = false;
                    BtnSave.IsEnabled = false;
                    BtnDelete.IsEnabled = false;
                    BtnUndo.IsEnabled = false;
                    MonthType_cbx.IsEnabled = false;
                    Year.IsEnabled = false;
                }
                else
                    MessageBox.Show("Nrakz Shwya M3lsh");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            BtnNew.IsEnabled = true;
            CBCreatedYears.IsEnabled = true;
            BtnExit.IsEnabled = true;
            BtnEdit.IsEnabled = false;
            BtnSave.IsEnabled = false;
            BtnDelete.IsEnabled = false;
            BtnUndo.IsEnabled = false;
            MonthType_cbx.IsEnabled = false;
            Year.IsEnabled = false;

            string where = "Year = '" + CBCreatedYears.SelectedItem + "'";
            Classes.DeleteRows(where, "Setup_Fiscal_Period");
            CBCreatedYears.Items.Remove(Year.Text);
            Clear_All();
            Close_All();
            MessageBox.Show("Done");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (from1.IsEnabled)
            {
                MessageBoxResult result = MessageBox.Show("close without save", "Confirmation",
                              MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            BtnNew.IsEnabled = true;
            CBCreatedYears.IsEnabled = true;
            BtnExit.IsEnabled = true;
            BtnEdit.IsEnabled = false;
            BtnSave.IsEnabled = false;
            BtnDelete.IsEnabled = false;
            BtnUndo.IsEnabled = false;
            MonthType_cbx.IsEnabled = false;
            Year.IsEnabled = false;
            Clear_All();
            Close_All();
        }
    }
}
