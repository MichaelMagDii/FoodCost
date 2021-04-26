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
    /// Interaction logic for Units.xaml
    /// </summary>
    public partial class Units : UserControl
    {
        List<string> Authenticated = new List<string>();
        public Units()
        {
            if (MainWindow.AuthenticationData.Count != 0)
            {
                if (MainWindow.AuthenticationData.ContainsKey("Units"))
                {
                    Authenticated = MainWindow.AuthenticationData["Units"];
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
                        LoadAllCOnv();
                        MainUiFormat();
                    }
                }
            }
        }

        //UNits
        private void FillDGV()
        {
            DataTable TheUnits = new DataTable();
            TheUnits = Classes.RetrieveData("Code,Name,IsActive", "Units");
            Unit_DGV.DataContext = TheUnits;
        }
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
            FillDGV();
        }
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
        }
        private void ClearUIFields()
        {
            Code_txt.Text = "";
            Name_txt.Text = "";
            Active_chbx.IsChecked = false;
        }
        private void NewButtonClicked(object sender, RoutedEventArgs e)
        {
            EnableUI();
            ClearUIFields();
            NewBtn.IsEnabled = false;
            Active_chbx.IsChecked = true;
            UpdateBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = "select Max(Code) from Units";
                SqlCommand cmd = new SqlCommand(s, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString() == "")
                        Code_txt.Text = "1";
                    else
                        Code_txt.Text = (int.Parse(reader[0].ToString()) + 1).ToString();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
            finally
            { con.Close(); }
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("SaveUnits") == -1 && Authenticated.IndexOf("CheckAllUnits") == -1)
            {   LogIn logIn = new LogIn();   logIn.ShowDialog();   }
            else
            {
                if (Code_txt.Text == "")
                {
                    MessageBox.Show("Code Field Can't Be Empty");
                    return;
                }

                try
                {
                    string FiledSelection = "Code,Name,IsActive,Create_Date,User_ID,WS";
                    string Values = string.Format("'{0}',N'{1}','{2}',GETDATE(),'{3}','{4}'", Code_txt.Text, Name_txt.Text, Active_chbx.IsChecked.ToString(), MainWindow.UserID, Classes.WS);
                    Classes.InsertRow("Units", FiledSelection, Values);
                }
                catch (Exception ex)
                {    MessageBox.Show(ex.ToString());    }
                finally
                {
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), Code_txt.Text, "Units", "New");
                    MainUiFormat();
                    Unit_DGV.DataContext = null;
                    FillDGV();
                }
                MessageBox.Show("Saved Successfully");
            }
        }
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("UpdateUnits") == -1 && Authenticated.IndexOf("CheckAllUnits") == -1)
            {   LogIn logIn = new LogIn();   logIn.ShowDialog();  }
            else
            {
                try
                {
                    string FiledSelection = "Name,IsActive,Last_modifiled_Date";
                    string Values = string.Format("N'{0}','{1}',GETDATE()", Name_txt.Text, Active_chbx.IsChecked);
                    string Where = "Code = " + Code_txt.Text;
                    Classes.UpdateRow(FiledSelection, Values, Where, "Units");
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());  }
                finally
                {
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), Code_txt.Text, "Units", "Update");
                    MainUiFormat();
                    Unit_DGV.DataContext = null;
                    FillDGV();
                }
                MessageBox.Show("Updated Successfully");
            }
        }
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {   MainUiFormat();   }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("DeleteUnits") == -1 && Authenticated.IndexOf("CheckAllUnits") == -1)
            {   LogIn logIn = new LogIn();   logIn.ShowDialog();    }
            else
            {
                try
                {
                    string where = "Code = " + Code_txt.Text;
                    Classes.DeleteRows(where, "Units");
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());    }
                finally
                {
                    MainUiFormat();
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), Code_txt.Text, "Units", "Delete");
                    Unit_DGV.DataContext = null;
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
                    Code_txt.Text = ((DataRowView)Unit_DGV.SelectedItems[0]).Row.ItemArray[0].ToString();
                    Name_txt.Text = ((DataRowView)Unit_DGV.SelectedItems[0]).Row.ItemArray[1].ToString();
                    Active_chbx.IsChecked = Convert.ToBoolean(((DataRowView)Unit_DGV.SelectedItems[0]).Row.ItemArray[2]);

                    EnableUI();
                    Code_txt.IsEnabled = false;
                    NewBtn.IsEnabled = false;
                    SaveBtn.IsEnabled = false;
                }
            }
        }

        //Recipe Units
        private void LoadAllCOnv()
        {
            DataTable TheConvUnits = new DataTable();
            TheConvUnits = Classes.RetrieveData("Code,BaseUnit_Name as 'Base Unit',Value as Qty,SecondUnit_Name 'Second Unit'", "Units_Conversion");
            RecipeUnit_DGV.DataContext = TheConvUnits;
        }
        private void Unit1_Button(object sender, RoutedEventArgs e)
        {
            AllUnits allUnits = new AllUnits(this, BaseUnit.Text, "BaseUnit");
            allUnits.ShowDialog();
        }
        private void Unit2_Button(object sender, RoutedEventArgs e)
        {
            AllUnits allUnits = new AllUnits(this, BaseUnit.Text, "Unit2");
            allUnits.ShowDialog();
        }
        private void NewUnitBtn_Click(object sender, RoutedEventArgs e)
        {
            BtnRecipeUnit2.IsEnabled = true;
            BtnRecipeUnit.IsEnabled = true;
            SaveUNitBtn.IsEnabled = true;
            BaseUnit.Text = ""; Secondunit.Text = "";
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = "select Max(Code) from Units_Conversion";
                SqlCommand cmd = new SqlCommand(s, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString() == "")
                        TheCode_txt.Text = "1";
                    else
                        TheCode_txt.Text = (int.Parse(reader[0].ToString()) + 1).ToString();
                }
            }
            catch { }
        }
        private void SaveUNitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("SaveConvUnits") == -1 && Authenticated.IndexOf("CheckAllUnits") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                try
                {
                    string FiledSelection = "Code,BaseUnit_ID,BaseUnit_Name,Value,SecondUnit_ID,SecondUnit_Name,Create_Date,User_ID,WS";
                    string Values = string.Format("'{5}',(select Code from Units where Name='{0}'),N'{0}','{1}',(select Code from Units where Name='{2}'),N'{2}',GETDATE(),'{3}','{4}'", BaseUnit.Text, ConvUnit2.Text, Secondunit.Text, MainWindow.UserID, Classes.WS, TheCode_txt.Text);
                    Classes.InsertRow("Units_Conversion", FiledSelection, Values);
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                finally
                {
                    MessageBox.Show("Saved Sussesfully !");
                    RecipeUnit_DGV.DataContext = null;
                    LoadAllCOnv();
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), TheCode_txt.Text, "Units_Conversion", "New");
                    SaveUNitBtn.IsEnabled = false;
                    BtnRecipeUnit2.IsEnabled = false;
                    BtnRecipeUnit.IsEnabled = false;
                }
            }
        }
        private void UpdateUnitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("UpdateConvUnits") == -1 && Authenticated.IndexOf("CheckAllUnits") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                try
                {
                    string FiledSelection = "BaseUnit_ID,BaseUnit_Name,Value,SecondUnit_ID,SecondUnit_Name,Last_modifiled_Date";
                    string Values = string.Format("(select Code From Units where Name='{0}'),N'{0}','{1}',(select Code From Units where Name='{2}'),'{2}',GETDATE()", BaseUnit.Text, ConvUnit2.Text, Secondunit.Text);
                    string Where = "Code=" + TheCode_txt.Text;
                    Classes.UpdateRow(FiledSelection, Values, Where, "Units_Conversion");
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString()); }
                {
                    RecipeUnit_DGV.DataContext = null;
                    MessageBox.Show("Saved Sussesfully !");
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), TheCode_txt.Text, "Units_Conversion", "Update");
                    SaveUNitBtn.IsEnabled = false;
                    BtnRecipeUnit2.IsEnabled = false;
                    BtnRecipeUnit.IsEnabled = false;
                    UpdateUnitBtn.IsEnabled = false;
                    LoadAllCOnv();
                    BaseUnit.Text = "";
                    Secondunit.Text = "";
                    ConvUnit2.Text = "";
                }
            }
        }
        private void UndoUnitBtn_Click(object sender, RoutedEventArgs e)
        {
            BtnRecipeUnit2.IsEnabled = false;
            BtnRecipeUnit.IsEnabled = false;
            BaseUnit.Text = "";
            Secondunit.Text = "";
            RecipeUnit_DGV.DataContext = null;
            LoadAllCOnv();
        }
        private void DeleteUnitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("DeleteConvUnits") == -1 && Authenticated.IndexOf("CheckAllUnits") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                try
                {
                    string where = "Code = " + TheCode_txt.Text;
                    Classes.DeleteRows(where, "Units");
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());     }
                finally
                {
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), TheCode_txt.Text, "Units_Conversion", "Delete");
                    BtnRecipeUnit2.IsEnabled = false;
                    BtnRecipeUnit.IsEnabled = false;
                    SaveUNitBtn.IsEnabled = false;
                    UpdateUnitBtn.IsEnabled = false;
                    DeleteUnitBtn.IsEnabled = false;

                    RecipeUnit_DGV.DataContext = null;
                    LoadAllCOnv();
                }
                MessageBox.Show("Deleted Successfully");
            }
        }

        //Events
        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void RecipeUnit_DGV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid data = sender as DataGrid;

                if (data != null && data.SelectedItems != null && data.SelectedItems.Count == 1)
                {
                    TheCode_txt.Text = ((DataRowView)RecipeUnit_DGV.SelectedItems[0]).Row.ItemArray[0].ToString();
                    BaseUnit.Text = ((DataRowView)RecipeUnit_DGV.SelectedItems[0]).Row.ItemArray[1].ToString();
                    ConvUnit2.Text = ((DataRowView)RecipeUnit_DGV.SelectedItems[0]).Row.ItemArray[2].ToString();
                    Secondunit.Text = ((DataRowView)RecipeUnit_DGV.SelectedItems[0]).Row.ItemArray[3].ToString();

                    EnableUI();
                    UpdateUnitBtn.IsEnabled = true;
                    DeleteUnitBtn.IsEnabled = true;
                }
            }
        }
    }
}