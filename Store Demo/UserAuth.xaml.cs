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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for UserAuth.xaml
    /// </summary>
    public partial class UserAuth : Window
    {
        List<string> Authenticated = new List<string>();
        public UserAuth()
        {
            if (MainWindow.AuthenticationData.Count != 0)
            {
                if (MainWindow.AuthenticationData.ContainsKey("UsersAuthentication"))
                {
                    Authenticated = MainWindow.AuthenticationData["UsersAuthentication"];
                    if (Authenticated.Count == 0)
                    {
                        MessageBox.Show("You Havent a Privilage to Open this Page");
                        LogIn logIn = new LogIn();
                        logIn.ShowDialog();
                    }
                    else
                    {
                        InitializeComponent();
                        MainUIFormat();
                        LoadUserClasses();
                    }
                }
            }
            else { MessageBox.Show("You Should Logined First !"); LogIn logIn = new LogIn();    logIn.ShowDialog();   }
        }
        private void LoadUserClasses()
        {
            DataTable TheUserClasses = new DataTable();
            
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            TheUserClasses = Classes.RetrieveData("Name", "UserClass_tbl");
            ListBox_UserClasses.Items.Clear();
            for (int i = 0; i < TheUserClasses.Rows.Count; i++)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = TheUserClasses.Rows[i][0].ToString();
                ListBox_UserClasses.Items.Add(listViewItem);
            }
        }
        private void ClearData()
        {
            UserClassIDtxt.Text = "";
            Nametxt.Text = "";
        }
        private void MainUIFormat()
        {
            UserClassIDtxt.IsEnabled = false;
            NewBtn.IsEnabled = true;
            Nametxt.IsEnabled = false;
            saveBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = false;
            UndoBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            ClearData();
        }
        private void IncreaseUserID()
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = "select Max(UserClass_ID) from UserClass_tbl";
                SqlCommand cmd = new SqlCommand(s, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString() == "")
                        UserClassIDtxt.Text = "1";
                    else
                        UserClassIDtxt.Text = (int.Parse(reader[0].ToString()) + 1).ToString();
                }
            }
            catch (Exception ex)
            {   MessageBox.Show(ex.ToString());   }
            finally
            {   con.Close(); }
        }
        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            MainUIFormat();
            Active_chbx.IsChecked = true;
            IncreaseUserID();
            UserClassIDtxt.IsEnabled = true;
            saveBtn.IsEnabled = true;
            UndoBtn.IsEnabled = true;
            Nametxt.IsEnabled = true;
            PrivillageStackPanel.IsEnabled = true;
            NewBtn.IsEnabled = false;
        }
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            MainUIFormat();
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("SaveUsersAuthentication") == -1 && Authenticated.IndexOf("CheckAllUsersAuthentication") == -1)
            {  LogIn logIn = new LogIn();     logIn.ShowDialog();    }
            else
            {
                if (UserClassIDtxt.Text == "")
                {
                    MessageBox.Show("Please enter ID Field");
                    return;
                }
                if (Nametxt.Text == "")
                {
                    MessageBox.Show("Please enter Name Field");
                    return;
                }

                try
                {
                    string FiledSelection = "UserClass_ID,Name,Active,CreateDate,User_ID,WS";
                    string Values = string.Format("'{0}',N'{1}','{2}',GETDATE(),'{3}','{4}'", UserClassIDtxt.Text, Nametxt.Text, Active_chbx.IsChecked.ToString(), MainWindow.UserID, Classes.WS);
                    Classes.InsertRow("UserClass_tbl", FiledSelection, Values);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }
                Classes.LogTable(Classes.MyComm.CommandText.ToString(), UserClassIDtxt.Text, "UserClass_tbl", "New");
                MainUIFormat();
                MessageBox.Show("Saved Successfully");
                LoadUserClasses();                  
            }
        }
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            string TheCommand = "";
            if (Authenticated.IndexOf("UpdateUsersAuthentication") == -1 && Authenticated.IndexOf("CheckAllUsersAuthentication") == -1)
            {   LogIn logIn = new LogIn();   logIn.ShowDialog();  }
            else
            {
                int length = 0;
                var Privilage = new StringBuilder();
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                if (ListBox_UserPrivileges.IsVisible == true)
                {
                    for (int i = 1; i < ListBox_UserPrivileges.Items.Count; i++)
                    {
                        TreeViewItem TreeName = (ListBox_UserPrivileges.Items[i] as TreeViewItem);
                        if (i != 2)
                        {
                            Privilage.Append((ListBox_UserPrivileges.Items[i] as TreeViewItem).Name);
                            Privilage.Append(":");
                        }
                        for (int j = 0; j < TreeName.Items.Count; j++)
                        {

                            if (TreeName.Items[j].GetType().Name == "CheckBox")
                            {
                                CheckBox valOfCheck = (TreeName.Items[j] as CheckBox);
                                if (valOfCheck.IsChecked == true)
                                {
                                    Privilage.Append((TreeName.Items[j] as CheckBox).Name);
                                    if (j < TreeName.Items.Count - 1)
                                        Privilage.Append(",");
                                }
                            }
                            else if (TreeName.Items[j].GetType().Name == "ListView")
                            {
                                ListView Listview = (TreeName.Items[j] as ListView);
                                for (int k = 0; k < Listview.Items.Count; k++)
                                {
                                    TreeViewItem SubTreeName = (Listview.Items[k] as TreeViewItem);
                                    Privilage.Append((Listview.Items[k] as TreeViewItem).Name);
                                    Privilage.Append(":");
                                    for (int l = 0; l < SubTreeName.Items.Count; l++)
                                    {
                                        if (SubTreeName.Items[l].GetType().Name == "CheckBox")
                                        {
                                            CheckBox valOfCheck = (SubTreeName.Items[l] as CheckBox);
                                            if (valOfCheck.IsChecked == true)
                                            {
                                                Privilage.Append((SubTreeName.Items[l] as CheckBox).Name);
                                                if (l < SubTreeName.Items.Count - 1)
                                                    Privilage.Append(",");
                                            }
                                        }
                                    }
                                    length = Privilage.Length - 1;
                                    if (Privilage[length] == ',')
                                    {
                                        Privilage.Remove(length, 1);
                                    }
                                    Privilage.Append(".");
                                }
                                length = Privilage.Length - 1;
                                if (Privilage[length] == ',')
                                {
                                    Privilage.Remove(length, 1);
                                    Privilage.Append(".");
                                }
                            }
                        }
                        length = Privilage.Length - 1;
                        if (Privilage[length] == ',')
                        {
                            Privilage.Remove(length, 1);
                        }
                        if (Privilage[Privilage.Length - 1] != '.')
                        {
                            Privilage.Append(".");
                        }
                    }
                    try
                    {
                        con.Open();
                        string s = string.Format("update UserPrivilages_tbl set ClassPrv='{0}', ModifiedDate=GETDATE() where UserClass_ID='{1}'", Privilage, UserClassIDtxt.Text);
                        SqlCommand cmd = new SqlCommand(s, con);
                        int IfExist = cmd.ExecuteNonQuery();

                        if (IfExist == 0)
                        {
                            s = string.Format("insert into UserPrivilages_tbl values ('{0}','{1}',GETDATE(),GETDATE(),'{2}')", UserClassIDtxt.Text, Privilage, MainWindow.UserID);
                            cmd = new SqlCommand(s, con);
                            cmd.ExecuteNonQuery();
                        }
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
                try
                {
                    con.Open();

                    TheCommand = "Update UserClass_tbl SET " + "Name = '" + Nametxt.Text + "', Active = '" + Active_chbx.IsChecked + "', ModifiedDate = GETDATE() Where UserClass_ID = " + UserClassIDtxt.Text;
                    SqlCommand cmd = new SqlCommand(TheCommand, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                Classes.LogTable(TheCommand, UserClassIDtxt.Text, "UserClass_tbl", "Update");
                MessageBox.Show("Updated Successfully");
                LoadUserClasses();
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("DeleteUsersAuthentication") == -1 && Authenticated.IndexOf("CheckAllUsersAuthentication") == -1)
            {   LogIn logIn = new LogIn();    logIn.ShowDialog();    }
            else
            {
                SqlConnection con = new SqlConnection(Classes.DataConnString);

                try
                {
                    string where = "UserClass_ID = " + UserClassIDtxt.Text;
                    Classes.DeleteRows(where, "UserClass_tbl");
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());   }
                Classes.LogTable(Classes.MyComm.CommandText.ToString(), UserClassIDtxt.Text, "UserClass_tbl", "Delete");
                MessageBox.Show("Deleted Successfully");
            }   
        }
        private void User_Class_Mouse_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                ListView listView = sender as ListView;
                if (sender != null && listView.SelectedItems != null && listView.SelectedItems.Count == 1)
                {
                    SqlConnection con = new SqlConnection(Classes.DataConnString);
                    try
                    {
                        con.Open();
                        string s = string.Format("select * from UserClass_tbl where Name = '{0}'", (listView.SelectedItem as ListViewItem).Content);
                        DataTable dt = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                            da.Fill(dt);

                        Nametxt.Text = (listView.SelectedItem as ListViewItem).Content.ToString();
                        UserClassIDtxt.Text = dt.Rows[0]["UserClass_ID"].ToString();
                        Active_chbx.IsChecked = dt.Rows[0]["Active"].ToString() == "True";
                        ListBox_UserClasses.Visibility = Visibility.Hidden;
                        ListBox_UserPrivileges.Visibility = Visibility.Visible;
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
            }
            Nametxt.IsEnabled = true;
            UpdateBtn.IsEnabled = true;
            UndoBtn.IsEnabled = true;
            DeleteBtn.IsEnabled = true;
            NewBtn.IsEnabled = false;
        }
        private void SelectAll_Checked(object sender, RoutedEventArgs e)
        {
            if(SelectAll.IsChecked == true)
            {
                for (int i = 1; i < ListBox_UserPrivileges.Items.Count; i++)
                {
                    TreeViewItem TreeName = (ListBox_UserPrivileges.Items[i] as TreeViewItem);
                    for (int j = 0; j < TreeName.Items.Count; j++)
                    {

                        if (TreeName.Items[j].GetType().Name == "CheckBox")
                        {
                            CheckBox valOfCheck = (TreeName.Items[j] as CheckBox);
                            valOfCheck.IsChecked = true;
                        }
                        else if (TreeName.Items[j].GetType().Name == "ListView")
                        {
                            ListView Listview = (TreeName.Items[j] as ListView);
                            for (int k = 0; k < Listview.Items.Count; k++)
                            {
                                TreeViewItem SubTreeName = (Listview.Items[k] as TreeViewItem);
                                for (int l = 0; l < SubTreeName.Items.Count; l++)
                                {
                                    if (SubTreeName.Items[l].GetType().Name == "CheckBox")
                                    {
                                        CheckBox valOfCheck = (SubTreeName.Items[l] as CheckBox);
                                        valOfCheck.IsChecked = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void SelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < ListBox_UserPrivileges.Items.Count; i++)
            {
                TreeViewItem TreeName = (ListBox_UserPrivileges.Items[i] as TreeViewItem);
                for (int j = 0; j < TreeName.Items.Count; j++)
                {

                    if (TreeName.Items[j].GetType().Name == "CheckBox")
                    {
                        CheckBox valOfCheck = (TreeName.Items[j] as CheckBox);
                        valOfCheck.IsChecked = false;
                    }
                    else if (TreeName.Items[j].GetType().Name == "ListView")
                    {
                        ListView Listview = (TreeName.Items[j] as ListView);
                        for (int k = 0; k < Listview.Items.Count; k++)
                        {
                            TreeViewItem SubTreeName = (Listview.Items[k] as TreeViewItem);
                            for (int l = 0; l < SubTreeName.Items.Count; l++)
                            {
                                if (SubTreeName.Items[l].GetType().Name == "CheckBox")
                                {
                                    CheckBox valOfCheck = (SubTreeName.Items[l] as CheckBox);
                                    valOfCheck.IsChecked = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}