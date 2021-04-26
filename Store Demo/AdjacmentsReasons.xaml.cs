using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AdjacmentsReasons.xaml
    /// </summary>
    public partial class AdjacmentsReasons : UserControl
    {
        List<string> Authenticated = new List<string>();
        public AdjacmentsReasons()
        {

            if (MainWindow.AuthenticationData.Count != 0)
            {
                if (MainWindow.AuthenticationData.ContainsKey("AdjacmentReasons"))
                {
                    Authenticated = MainWindow.AuthenticationData["AdjacmentReasons"];
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
        }

        private void FillDGV()
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Name2");
            DT.Columns.Add("Active",typeof(bool));
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "select Code,Name,Name2,Active from Setup_AdjacmentReasons_tbl";
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DT.Rows.Add(reader["Code"].ToString(), reader["Name"].ToString(), reader["Name2"].ToString(), reader["Active"]);
                }
                for(int i=0;i<DT.Columns.Count;i++)
                {
                    DT.Columns[i].ReadOnly = true;
                }
                ReasonsDGV.DataContext = DT;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                reader.Close();
                con.Close();
            }
        }    //Done

        public void MainUiFormat()
        {
            Code_txt.IsEnabled = false;
            Name_txt.IsEnabled = false;
            Name2_txt.IsEnabled = false;
            Active_chbx.IsEnabled = false;
            Active_chbx.IsChecked = false;
            SaveBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = false;
            UndoBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            NewBtn.IsEnabled = true;

        }   //Done

        public void EnableUI()
        {
            Code_txt.IsEnabled = true;
            Name_txt.IsEnabled = true;
            Name2_txt.IsEnabled = true;
            Active_chbx.IsEnabled = true;
            SaveBtn.IsEnabled = true;
            UpdateBtn.IsEnabled = true;
            UndoBtn.IsEnabled = true;
            DeleteBtn.IsEnabled = true;
            NewBtn.IsEnabled = true;
        }   //Done

        private void ClearUIFields()
        {
            Code_txt.Text = "";
            Name_txt.Text = "";
            Name2_txt.Text = "";
            Active_chbx.IsChecked = false;
        }   //Done

        private void NewButtonClicked(object sender, RoutedEventArgs e)
        {
            EnableUI();
            ClearUIFields();
            Active_chbx.IsChecked = true;
            NewBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("SaveAddjacmentReasons") == -1 && Authenticated.IndexOf("CheckAllAddjacmentReasons") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                if (Code_txt.Text != "1")
                {
                    if (Code_txt.Text == "")
                    {
                        MessageBox.Show("Code Field Can't Be Empty");
                        return;
                    }

                    for (int i = 0; i < ReasonsDGV.Items.Count; i++)
                    {
                        if (Code_txt.Text == ((DataRowView)ReasonsDGV.Items[i]).Row.ItemArray[0].ToString())
                        {
                            MessageBox.Show("This Code Is Not Avaliable");
                            return;
                        }
                    }

                    SqlConnection con = new SqlConnection(Classes.DataConnString);
                    try
                    {
                        con.Open();
                        string s = "insert into Setup_AdjacmentReasons_tbl(Code, Name, Name2, Active) values (" + Code_txt.Text + ",N'" + Name_txt.Text + "',N'" + Name2_txt.Text + "','" + Active_chbx.IsChecked + "')";
                        SqlCommand cmd = new SqlCommand(s, con);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        MainUiFormat();

                        ReasonsDGV.DataContext = null;
                        FillDGV();
                    }
                    MessageBox.Show("Saved Successfully");
                }
                else
                {
                    MessageBox.Show("You can't Update This Reason");
                }
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("UpdateAddjacmentReasonss") == -1 && Authenticated.IndexOf("CheckAllAddjacmentReasons") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                SqlConnection con = new SqlConnection(Classes.DataConnString);

                try
                {
                    con.Open();
                    string s = "Update Setup_AdjacmentReasons_tbl SET Name=N'" + Name_txt.Text +
                                                   "',Name2=N'" + Name2_txt.Text +
                                                   "',Active='" + Active_chbx.IsChecked +
                                                   "'Where Code =" + Code_txt.Text;
                    SqlCommand cmd = new SqlCommand(s, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                    MainUiFormat();

                    ReasonsDGV.DataContext = null;
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
            if (Authenticated.IndexOf("DeleteAddjacmentReasons") == -1 && Authenticated.IndexOf("CheckAllAddjacmentReasons") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                if (Code_txt.Text != "1")
                {
                    SqlConnection con = new SqlConnection(Classes.DataConnString);
                    try
                    {
                        con.Open();
                        string s = "Delete Setup_AdjacmentReasons_tbl Where Code =" + Code_txt.Text;
                        SqlCommand cmd = new SqlCommand(s, con);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        MainUiFormat();

                        ReasonsDGV.DataContext = null;
                        FillDGV();
                    }
                    MessageBox.Show("Deleted Successfully");
                }
                else
                {
                    MessageBox.Show("You can't delete This Reason");
                }
            }
        }

        private void RowClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid data = sender as DataGrid;

                if (data != null && data.SelectedItems != null && data.SelectedItems.Count == 1)
                {
                    Code_txt.Text = ((DataRowView)ReasonsDGV.SelectedItem).Row.ItemArray[0].ToString();
                    Name_txt.Text = ((DataRowView)ReasonsDGV.SelectedItem).Row.ItemArray[1].ToString();
                    Name2_txt.Text = ((DataRowView)ReasonsDGV.SelectedItem).Row.ItemArray[2].ToString();
                    Active_chbx.IsChecked = (bool)(((DataRowView)ReasonsDGV.SelectedItem).Row.ItemArray[3]);

                    EnableUI();
                    Code_txt.IsEnabled = false;
                    NewBtn.IsEnabled = false;
                    SaveBtn.IsEnabled = false;
                }
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }


    }
}
