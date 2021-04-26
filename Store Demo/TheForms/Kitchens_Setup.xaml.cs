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
using System.Data;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for Setup_Kitchens.xaml
    /// </summary>
    public partial class Setup_Kitchens : Window
    {
        string ResturantCode = "";
        List<string> Authenticated = new List<string>();
        public Setup_Kitchens(string _RestaurantCode)
        {
            if (MainWindow.AuthenticationData.ContainsKey("Kitchens"))
            {
                Authenticated = MainWindow.AuthenticationData["Kitchens"];
                if (Authenticated.Count == 0)
                {
                    MessageBox.Show("You Havent a Privilage to Open this Page");
                    LogIn logIn = new LogIn();
                    logIn.ShowDialog();
                }
                else
                {
                    ResturantCode = _RestaurantCode;
                    InitializeComponent();
                    ParentStore(_RestaurantCode);
                    FillDGV(_RestaurantCode);
                    MainUiFormat();
                }
            }
        }

        private void ParentStore(string Code)
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = string.Format("select Name from Setup_Restaurant where Code = {0}", Code);
                SqlCommand cmd = new SqlCommand(s, con);
                ParentStore_cbx.Text = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        private void FillDGV(string Code)
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Name2");
            DT.Columns.Add("Main", typeof(bool));
            DT.Columns.Add("Outlet", typeof(bool));
            DT.Columns.Add("Active", typeof(bool));
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;

            try
            {
                con.Open();
                string s = string.Format("select Code,Name,Name2,IsMain,IsOutlet,IsActive from Setup_Kitchens where RestaurantID = {0} order by Code", Code);
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DT.Rows.Add(reader["Code"].ToString(), reader["Name"].ToString(), reader["Name2"].ToString(), reader["IsMain"].ToString(), reader["IsOutlet"].ToString(), reader["IsActive"].ToString());
                }
                Stores_DGV.DataContext = DT;
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
        }
        private void MainUiFormat()
        {
            Code_txt.IsEnabled = false;
            Name_txt.IsEnabled = false;
            Name2_txt.IsEnabled = false;
            Active_chbx.IsEnabled = false;
            IsMain.IsEnabled = false;
            IsOutlet.IsEnabled = false;
            SaveBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = false;
            UndoBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            Active_chbx.IsChecked = false;
            NewBtn.IsEnabled = true;
        }
        public void EnableUI()
        {
            Code_txt.IsEnabled = true;
            Name_txt.IsEnabled = true;
            Name2_txt.IsEnabled = true;
            Active_chbx.IsEnabled = true;
            IsMain.IsEnabled = true;
            IsOutlet.IsEnabled = true;
            SaveBtn.IsEnabled = true;
            UpdateBtn.IsEnabled = true;
            UndoBtn.IsEnabled = true;
            DeleteBtn.IsEnabled = true;
            NewBtn.IsEnabled = true;
        }
        private void ClearUIFields()
        {
            Code_txt.Text = "";
            Name_txt.Text = "";
            Name2_txt.Text = "";
            Active_chbx.IsChecked = false;
            IsMain.IsChecked = false;
            IsOutlet.IsChecked = false;
        }
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
            if (Authenticated.IndexOf("SaveKitchens") == -1 && Authenticated.IndexOf("CheckAllKitchens") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                SqlConnection con2 = new SqlConnection(Classes.DataConnString);
                DataTable DT = new DataTable();
                if (Code_txt.Text == "")
                {
                    MessageBox.Show("Code Field Can't Be Empty");
                    return;
                }

                for (int i = 0; i < Stores_DGV.Items.Count; i++)
                {
                    if (Code_txt.Text == ((DataRowView)Stores_DGV.Items[i]).Row.ItemArray[0].ToString())
                    {
                        MessageBox.Show("This Code Is Not Avaliable");
                        return;
                    }
                }

                if (IsMain.IsChecked == true)
                {
                    con.Open();
                    string s = string.Format("select IsMain from Setup_Kitchens where IsMain='True' and RestaurantID='{0}'", ResturantCode);
                    SqlCommand cmd = new SqlCommand(s, con);
                    if (cmd.ExecuteScalar() != null)
                    {
                        MessageBox.Show("Can't be more Than Main Kitchen !");
                        return;
                    }
                }

                try
                {
                    string FiledSelection = "Code,Name,Name2,IsMain,IsOutlet,IsActive,RestaurantID,Create_Date,WS,UserID";
                    string values = string.Format("'{0}', N'{1}', N'{2}', '{3}','{4}','{5}','{6}',{7},'{8}','{9}'", Code_txt.Text, Name_txt.Text, Name2_txt.Text, IsMain.IsChecked, IsOutlet.IsChecked, Active_chbx.IsChecked, ResturantCode, "GETDATE()", Classes.WS, MainWindow.UserID);
                    Classes.InsertRow("Setup_Kitchens", FiledSelection, values);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                    MainUiFormat();

                    Stores_DGV.DataContext = null;
                    FillDGV(ResturantCode);
                }
                if (IsMain.IsChecked == true || IsOutlet.IsChecked == true)
                {
                    string FiledSelectionKitchenItems = "KitchenID,ItemID,RestaurantID,MinQty,MaxQty,shulfID";
                    string FiledSelectionItems = "KitchenID,ItemID,RestaurantID,Qty,Units,Last_Cost,Current_Cost,Net_Cost";
                    string FiledSelectionItemsYear = "ItemID,Restaurant_ID,Kitchen_ID,Year";
                    string valuesItems = ""; string ValuesKitchenItems = ""; string ValuesItemsYear = ""; string Comma = "";
                    try
                    {
                        DT = Classes.RetrieveData("Code", "Setup_Items");
                        int NumOfItemsPerRec = DT.Rows.Count % 1000;
                        int NumOfItems = 0;
                        if (DT.Rows.Count > 0)
                        {
                            if (DT.Rows.Count > 1000)
                            {
                                for (int i = 0; i <= NumOfItemsPerRec; i++)
                                {
                                    for (int q = NumOfItems; q < (i + 1) * 1000; q++)
                                    {
                                        ValuesKitchenItems = Comma + string.Format("('{0}','{1}','{2}','0','0','0')", Code_txt.Text, DT.Rows[q].ItemArray[0], ResturantCode);
                                        valuesItems = Comma + string.Format("('{0}','{1}','{2}','0','','','0','')", Code_txt.Text, DT.Rows[q].ItemArray[0], ResturantCode);
                                        ValuesItemsYear = Comma + string.Format("('{0}','{1}','{2}','{3}')", DT.Rows[q].ItemArray[0], ResturantCode, Code_txt.Text, MainWindow.CurrentYear);
                                        Comma = ",";
                                    }
                                    NumOfItems = (i + 1) * 1000;
                                }
                                Classes.InsertRows("Setup_KitchenItems", FiledSelectionKitchenItems, ValuesKitchenItems);
                                Classes.InsertRows("ItemsYear", FiledSelectionItemsYear, ValuesItemsYear);
                                Classes.InsertRows("Items", FiledSelectionItems, valuesItems);
                            }
                            else
                            {
                                for (int i = 0; i < DT.Rows.Count; i++)
                                {
                                    ValuesKitchenItems += Comma + string.Format("('{0}','{1}','{2}','0','0','0')", Code_txt.Text, DT.Rows[i].ItemArray[0], ResturantCode);
                                    valuesItems += Comma + string.Format("('{0}','{1}','{2}','0','','','','')", Code_txt.Text, DT.Rows[i].ItemArray[0], ResturantCode);
                                    ValuesItemsYear += Comma + string.Format("('{0}','{1}','{2}','{3}')", DT.Rows[i].ItemArray[0], ResturantCode, Code_txt.Text, MainWindow.CurrentYear);
                                    Comma = ",";
                                }
                                Classes.InsertRows("Setup_KitchenItems", FiledSelectionKitchenItems, ValuesKitchenItems);
                                Classes.InsertRows("ItemsYear", FiledSelectionItemsYear, ValuesItemsYear);
                                Classes.InsertRows("Items", FiledSelectionItems, valuesItems);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                        con2.Close();
                    }
                }
                MessageBox.Show("Saved Successfully");
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("UpdateKitchens") == -1 && Authenticated.IndexOf("CheckAllKitchens") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                SqlConnection con = new SqlConnection(Classes.DataConnString);

                if (IsMain.IsChecked == true)
                {
                    con.Open();
                    string s = string.Format("select Code,IsMain from Setup_Kitchens where IsMain='True' and RestaurantID='{0}'", ResturantCode);
                    SqlCommand cmd = new SqlCommand(s, con);
                    if(cmd.ExecuteScalar().ToString() != Code_txt.Text)
                    {
                        MessageBox.Show("Can't be more Than Main Kitchen !");
                        return;
                    }
                }

                try
                {
                    string FiledSlection = "Name,Name2,IsMain,IsOutlet,IsActive,Last_Modified_Date";
                    string values = string.Format("N'{0}', N'{1}', '{2}', '{3}','{4}',{5}", Name_txt.Text, Name2_txt.Text, IsMain.IsChecked, IsOutlet.IsChecked, Active_chbx.IsChecked, "GETDATE()");
                    string Where = string.Format("Code={0} and RestaurantID='{1}'", Code_txt.Text, ResturantCode);
                    Classes.UpdateRow(FiledSlection, values, Where, "Setup_Kitchens");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                    MainUiFormat();

                    Stores_DGV.DataContext = null;
                    FillDGV(ResturantCode);
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
            if (Authenticated.IndexOf("DeleteKitchens") == -1 && Authenticated.IndexOf("CheckAllKitchens") == -1)
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
                    string s = string.Format("delete Setup_Kitchens where Code='{0}' and RestaurantID='{1}'", Code_txt.Text, ResturantCode);
                    SqlCommand cmd = new SqlCommand(s, con);
                    cmd.ExecuteNonQuery();

                    s = string.Format("delete Setup_KitchenItems where KitchenID='{0}' and RestaurantID='{1}'", Code_txt.Text, ResturantCode);
                    cmd = new SqlCommand(s, con);
                    cmd.ExecuteNonQuery();


                    s = string.Format("delete Items where KitchenID='{0}' and RestaurantID='{1}'", Code_txt.Text, ResturantCode);
                    cmd = new SqlCommand(s, con);
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

                    Stores_DGV.DataContext = null;
                    FillDGV(ResturantCode);
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
                    Code_txt.Text = ((DataRowView)Stores_DGV.SelectedItem).Row.ItemArray[0].ToString();
                    Name_txt.Text = ((DataRowView)Stores_DGV.SelectedItem).Row.ItemArray[1].ToString();
                    Name2_txt.Text = ((DataRowView)Stores_DGV.SelectedItem).Row.ItemArray[2].ToString();
                    Active_chbx.IsChecked = Convert.ToBoolean(((DataRowView)Stores_DGV.SelectedItem).Row.ItemArray[5]);
                    IsOutlet.IsChecked = Convert.ToBoolean(((DataRowView)Stores_DGV.SelectedItem).Row.ItemArray[4]);
                    IsMain.IsChecked = Convert.ToBoolean(((DataRowView)Stores_DGV.SelectedItem).Row.ItemArray[3]);

                    EnableUI();
                    Code_txt.IsEnabled = false;
                    NewBtn.IsEnabled = false;
                    SaveBtn.IsEnabled = false;
                }
            }
        }
    }
}
