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
using System.Data;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for Vendors.xaml
    /// </summary>
    public partial class Vendors : UserControl
    {
        List<string> Authenticated = new List<string>();
        public Vendors()
        {
            if (MainWindow.AuthenticationData.Count != 0)
            {
                if (MainWindow.AuthenticationData.ContainsKey("Vendors"))
                {
                    Authenticated = MainWindow.AuthenticationData["Vendors"];
                    if (Authenticated.Count == 0)
                    {
                        MessageBox.Show("You Havent a Privilage to Open this Page");
                        LogIn logIn = new LogIn();
                        logIn.ShowDialog();
                    }
                    else
                    {
                        InitializeComponent();
                        FillDGV();
                        MainUiFormat();
                    }
                }
            }
            else { MessageBox.Show("You Should Logined First !"); LogIn logIn = new LogIn();  logIn.ShowDialog(); }
        }

        //Functions
        private void FillDGV()
        {
            DataTable TheVendros = new DataTable();
            TheVendros = Classes.RetrieveData("Code,Name,IsActive", "Vendors");
            Vendors_DGV.DataContext = TheVendros;
        }       //Done
        private void MainUiFormat()
        {
            Code_txt.IsEnabled = false;
            Name_txt.IsEnabled = false;
            Active_chbx.IsEnabled = false;
            SaveBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = false;
            UndoBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            NewBtn.IsEnabled = true;
        }       //Done
        public void EnableUI()
        {
            Code_txt.IsEnabled = true;
            Name_txt.IsEnabled = true;
            Active_chbx.IsEnabled = true;
            SaveBtn.IsEnabled = true;
            NewBtn.IsEnabled = true;
            UpdateBtn.IsEnabled = true;
            UndoBtn.IsEnabled = true;
            DeleteBtn.IsEnabled = true;
        }       //Done
        private void ClearUIFields()
        {
            Code_txt.Text = "";
            Name_txt.Text = "";
            Active_chbx.IsChecked = false;
        }       //Done

        //Events
        private void NewButtonClicked(object sender, RoutedEventArgs e)
        {
            EnableUI();
            ClearUIFields();
            Active_chbx.IsChecked = true;
            NewBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
        }       //Done
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("SaveVendors") == -1 && Authenticated.IndexOf("CheckAllVendors") == -1)
            {  LogIn logIn = new LogIn(); logIn.ShowDialog();  }
            else
            {
                if (Code_txt.Text == "")
                {
                    MessageBox.Show("Code Field Can't Be Empty");
                    return;
                }

                for(int i=0;i<Vendors_DGV.Items.Count;i++)
                {
                    if(((DataRowView)Vendors_DGV.Items[i]).Row.ItemArray[0].ToString().Equals(Code_txt.Text))
                    {
                        MessageBox.Show("This Code Is Not Avaliable");
                        return;
                    }
                    if (((DataRowView)Vendors_DGV.Items[i]).Row.ItemArray[1].ToString().Equals(Name_txt.Text))
                    {
                        MessageBox.Show("Please Change The Name"); return;
                    }
                }

                try
                {
                    string FiledSelection = "Code,Name,IsActive,CreateDate,UserID,WS";
                    string Values = string.Format("'{0}',N'{1}','{2}',GETDATE(),'{3}','{4}'",Code_txt.Text,Name_txt.Text, Active_chbx.IsChecked.ToString(),MainWindow.UserID,Classes.WS);
                    Classes.InsertRow("Vendors", FiledSelection, Values);
                }
                catch (Exception ex)
                {  MessageBox.Show(ex.ToString());  }
                finally
                {
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), Code_txt.Text, "Vendors", "New");
                    MainUiFormat();

                    Vendors_DGV.DataContext = null;
                    FillDGV();
                }
                MessageBox.Show("Saved Successfully");
            }
        }
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("UpdateVendors") == -1 && Authenticated.IndexOf("CheckAllVendors") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                for (int i = 0; i < Vendors_DGV.Items.Count; i++)
                {
                    if ((((DataRowView)Vendors_DGV.Items[i]).Row.ItemArray[1].ToString().Equals(Name_txt.Text)) && (((DataRowView)Vendors_DGV.Items[i]).Row.ItemArray[0].ToString()) != Code_txt.Text)
                    {
                        MessageBox.Show("Please Change The Name"); return;
                    }
                }

                try
                {
                    string FiledSelection = "Name,IsActive,LastModifiedDate";
                    string Values = string.Format("N'{0}','{1}',GETDATE()", Name_txt.Text, Active_chbx.IsChecked);
                    string Where = "Code = " + Code_txt.Text;
                    Classes.UpdateRow(FiledSelection, Values, Where, "Vendors");
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }
                finally
                {
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), Code_txt.Text, "Vendors", "Update");
                    MainUiFormat();

                    Vendors_DGV.DataContext = null;
                    FillDGV();
                }
                MessageBox.Show("Updated Successfully");
            }
        }
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            MainUiFormat();
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("DeleteVendors") == -1 && Authenticated.IndexOf("CheckAllVendors") == -1)
            {   LogIn logIn = new LogIn();  logIn.ShowDialog();   }
            else
            {
                try
                {
                    string where = "Code = " + Code_txt.Text;
                    Classes.DeleteRows(where, "Vendors");
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());  }
                finally
                {
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), Code_txt.Text, "Vendors", "Delete");
                    MainUiFormat();
                    Vendors_DGV.DataContext=null;
                    FillDGV();
                }
                MessageBox.Show("Deleted Successfully");
            }
        }
        private void RowClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid data = sender as DataGrid;

                if (data != null && data.SelectedItems != null && data.SelectedItems.Count == 1)
                {
                    Code_txt.Text = ((DataRowView)Vendors_DGV.SelectedItem).Row.ItemArray[0].ToString();
                    Name_txt.Text = ((DataRowView)Vendors_DGV.SelectedItem).Row.ItemArray[1].ToString();
                    Active_chbx.IsChecked = (bool)(((DataRowView)Vendors_DGV.SelectedItem).Row.ItemArray[2]);

                    EnableUI();
                    Code_txt.IsEnabled = false;
                    NewBtn.IsEnabled = false;
                    SaveBtn.IsEnabled = false;
                }
            }
        }
    }
}