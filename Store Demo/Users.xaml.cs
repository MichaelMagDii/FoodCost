using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        List<string> Authenticated = new List<string>();
        public Users()
        {
            if (MainWindow.AuthenticationData.Count != 0)
            {
                if (MainWindow.AuthenticationData.ContainsKey("Users"))
                {
                    Authenticated = MainWindow.AuthenticationData["Users"];
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
            else { MessageBox.Show("You Should Logined First !");    LogIn logIn = new LogIn();   logIn.ShowDialog();   }
        }

        private void FillDGV()
        {
            DataTable TheUsers = new DataTable();
            TheUsers = Classes.RetrieveData("ID,Name,UserName,(select Name FROM UserClass_tbl Where Users.UserClass_ID=UserClass_tbl.UserClass_ID) as Tital,Active", "Users");
            UsersDGV.DataContext = TheUsers;
        }
        public void MainUiFormat()
        {
            userIDtxt.IsEnabled = false;
            Nametxt.IsEnabled = false;
            passwordtxt.IsEnabled = false;
            Active_chbx.IsEnabled = false;
            jobTitle.IsEnabled = false;
            phone.IsEnabled = false;
            saveBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = false;
            UndoBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            newBtn.IsEnabled = true;
            UserNametxt.IsEnabled = false;
            Addresstxt.IsEnabled = false;
            Mailtxt.IsEnabled = false;

        }
        public void EnableUI()
        {
            userIDtxt.IsEnabled = true;
            Nametxt.IsEnabled = true;
            passwordtxt.IsEnabled = true;
            Active_chbx.IsEnabled = true;
            jobTitle.IsEnabled = true;
            phone.IsEnabled = true;
            saveBtn.IsEnabled = true;
            UpdateBtn.IsEnabled = true;
            UndoBtn.IsEnabled = true;
            DeleteBtn.IsEnabled = true;
            newBtn.IsEnabled = true;
            UserNametxt.IsEnabled = true;
            Addresstxt.IsEnabled = true;
            Mailtxt.IsEnabled = true;
        }
        private void ClearUIFields()
        {
            userIDtxt.Text = "";
            Nametxt.Text = "";
            passwordtxt.Password = "";
            Active_chbx.IsChecked = false;
            jobTitle.Text = "";
            phone.Text = "";
            UserNametxt.Text = "";
            Addresstxt.Text = "";
            Mailtxt.Text = "";
        }
        private void LoadAllJobs()
        {
            DataTable TheJobTitles = Classes.RetrieveData("Name", "UserClass_tbl");
            for(int i=0;i<TheJobTitles.Rows.Count;i++)
            {
                jobTitle.Items.Add(TheJobTitles.Rows[i][0].ToString());
            }
        }
        private bool DoSomeChecks()
        {
            bool Val = true;
            if (userIDtxt.Text == "")
            {
                MessageBox.Show("Code Field Can't Be Empty");
                return Val=false;
            }
            else if (Nametxt.Text == "")
            {
                MessageBox.Show("Code Field Can't Be Empty");
                return Val = false;
            }
            else if (UserNametxt.Text == "")
            {
                MessageBox.Show("Code Field Can't Be Empty");
                return Val = false;
            }
            else if (passwordtxt.Password == "")
            {
                MessageBox.Show("Code Field Can't Be Empty");
                return Val = false;
            }
            else if (jobTitle.Text == "")
            {
                MessageBox.Show("Code Field Can't Be Empty");
                return Val = false;
            }
            return Val;
        }

        //events
        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            EnableUI();
            ClearUIFields();
            LoadAllJobs();
            Active_chbx.IsChecked = true;
            newBtn.IsEnabled = false;
            UpdateBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
        }
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            MainUiFormat();
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("SaveUsers") == -1 && Authenticated.IndexOf("CheckAllUsers") == -1)
            {   LogIn logIn = new LogIn();   logIn.ShowDialog();  }
            else
            {
                if (DoSomeChecks() == false)
                    return;

                for(int i=0;i<UsersDGV.Items.Count;i++)
                {
                    if(UsersDGV.Items[i].ToString().Equals(userIDtxt.Text))
                    {
                        MessageBox.Show("This Code Is Not Avaliable");    return;
                    }
                }
             
                try
                {
                    string FiledSelection = "ID,Name,UserName,Password,UserClass_ID,Adress,Mobile,Email,Active,CreateDate,User_ID,WS";
                    string Values = string.Format("'{0}',N'{1}','{2}','{3}',(select UserClass_ID FROM UserClass_tbl Where Name='{4}'),N'{5}','{6}','{7}','{8}',GETDATE(),'{9}','{10}'", userIDtxt.Text, Nametxt.Text, UserNametxt.Text, passwordtxt.Password, jobTitle.Text,  Addresstxt.Text, phone.Text, Mailtxt.Text, Active_chbx.IsChecked.ToString(),MainWindow.UserID,Classes.WS);
                    Classes.InsertRow("Users", FiledSelection, Values);
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());  }
                finally
                {
                    MainUiFormat();
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), userIDtxt.Text, "Users", "New");
                    UsersDGV.DataContext = null;
                    FillDGV();
                }
                MessageBox.Show("Saved Successfully");
            }
        }
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("UpdateUsers") == -1 && Authenticated.IndexOf("CheckAllUsers") == -1)
            {   LogIn logIn = new LogIn();    logIn.ShowDialog();     }
            else
            {
                try
                {
                    string FiledSelection = "Name,UserName,Password,UserClass_ID,Mobile,Adress,Email,Active,ModifiedDate";
                    string Values = string.Format("N'{0}','{1}','{2}',(select UserClass_ID FROM UserClass_tbl Where Name='{3}'),'{4}',N'{5}','{6}','{7}',GETDATE()", Nametxt.Text, UserNametxt.Text, passwordtxt.Password, jobTitle.Text, phone.Text, Addresstxt.Text, Mailtxt.Text, Active_chbx.IsChecked.ToString());
                    string Where = "ID = " + userIDtxt.Text;
                    Classes.UpdateRow(FiledSelection, Values, Where, "Users");
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());  }
                finally
                {
                    MainUiFormat();
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), userIDtxt.Text, "Users", "Update");
                    UsersDGV.DataContext = null;
                    FillDGV();
                }
                MessageBox.Show("Updated Successfully");
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("DeleteUsers") == -1 && Authenticated.IndexOf("CheckAllUsers") == -1)
            {   LogIn logIn = new LogIn();   logIn.ShowDialog();   }
            else
            {
                try
                {
                    string where = "ID = " + userIDtxt.Text;
                    Classes.DeleteRows(where, "Users");
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());   }
                finally
                {
                    MainUiFormat();
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), userIDtxt.Text, "Users", "Delete");
                    UsersDGV.DataContext = null;
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
                    try
                    {
                        DataTable TheUserData = new DataTable();
                        string FiledSelection = "ID,Name,Password,UserName,(Select Name From UserClass_tbl where UserClass_tbl.UserClass_ID=Users.UserClass_ID) as Tital,Adress,Phone,Email,Active";
                        string WhereFiltering = " where ID=" + ((DataRowView)UsersDGV.SelectedItem).Row.ItemArray[0];
                        TheUserData = Classes.RetrieveData(FiledSelection, WhereFiltering, "Users");
                        userIDtxt.Text = TheUserData.Rows[0][0].ToString();
                        Nametxt.Text = TheUserData.Rows[0][1].ToString();
                        UserNametxt.Text = TheUserData.Rows[0][3].ToString();
                        passwordtxt.Password = TheUserData.Rows[0][2].ToString();
                        jobTitle.Text = TheUserData.Rows[0][4].ToString();
                        phone.Text = TheUserData.Rows[0][6].ToString(); ;
                        Addresstxt.Text = TheUserData.Rows[0][5].ToString();
                        Mailtxt.Text = TheUserData.Rows[0][7].ToString();
                        Active_chbx.IsChecked = Convert.ToBoolean(TheUserData.Rows[0][8].ToString());
                    }
                    catch { }

                    EnableUI();
                    userIDtxt.IsEnabled = false;
                    newBtn.IsEnabled = false;
                    saveBtn.IsEnabled = false;
                }
            }
        }
        private void PackIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Authenticated.IndexOf("UsersAuth") == -1 && Authenticated.IndexOf("CheckAllUsers") == -1)
            {   LogIn logIn = new LogIn();    logIn.ShowDialog();      }
            else
            {
                UserAuth w = new UserAuth();
                w.ShowDialog();
            }
        }
    }
}
