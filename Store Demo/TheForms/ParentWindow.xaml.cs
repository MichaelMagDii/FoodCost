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
    /// Interaction logic for ParentWindow.xaml
    /// </summary>
    public partial class ParentWindow : Window
    {
        string CodeOfparent = "";
        string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
        int ValuesOfItems = 0;
        List<DgvData> ItemsParentData = new List<DgvData>();
        List<DgvData> ItemsParentDataStarting = new List<DgvData>();
        public ParentWindow(string Code)
        {
            InitializeComponent();
            LoadToDGVOfParentItemsStartly(Code);
            CodeOfparent = Code;
        }           //Done FInall Function

        private void LoadToDGVOfParentItemsStartly(string Code)
        {
            string Where = "";
            DataTable ItemName = new DataTable();
            DataTable ParentsItems = new DataTable();
            Where = String.Format("Code='{0}'", Code);
            ItemName = Classes.RetrieveData("Name", Where, "Setup_Items");
            Where = string.Format("Parent_Item='{0}'", Code);
            ParentsItems = Classes.RetrieveData("Code,(Select Name From Setup_Items Where Code=Setup_ParentItems.Code)", Where, "Setup_ParentItems");
            ParentItemsDGV.DataContext = ParentsItems;
            ItemNametxt.Text = ItemName.Rows[0][0].ToString();
        }           //Done FInall Function

        private void LoadToDGVOfParentItems(string _Code)
        {
            DataTable AllItems = new DataTable();
            DataTable theData = new DataTable();
            AllItems = ((DataTable)ParentItemsDGV.DataContext);
            string Where = string.Format("Code='{0}'", _Code);
            theData = Classes.RetrieveData("Code,Name", Where, "Setup_Items");
            for(int i=0;i<theData.Rows.Count;i++)
            {
                AllItems.Rows.Add(theData.Rows[0][0].ToString(), theData.Rows[0][1].ToString());
            }
            ParentItemsDGV.DataContext = AllItems;
        }           //Done Finall Function

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }   // Done Finall Function

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ParentGrid.Visibility = Visibility.Hidden;
            LoadToDGVOfItems();
            ShowItems.Visibility = Visibility.Visible;
        }  //Done Finall Function

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string Where = string.Format("Parent_Item='{0}'", CodeOfparent);
            Classes.DeleteRows(Where, "Setup_ParentItems");
            for(int i=0;i<ParentItemsDGV.Items.Count;i++)
            {
                string Values = string.Format("'{0}','{1}'", ((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[0], CodeOfparent);
                Classes.InsertRow("Setup_ParentItems", Values);
            }
            LoadToDGVOfParentItemsStartly(CodeOfparent);
            MessageBox.Show("Saved Sucsseful");
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ParentItemsDGV.SelectedItems.Count !=0)
            {
                string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
                SqlConnection con = new SqlConnection(connString);
                try
                {
                    con.Open();
                    string iteem = ((DataRowView)ParentItemsDGV.SelectedItems[0]).Row.ItemArray[0].ToString();
                    string s11 = "delete from Setup_ParentItems where Code='" + iteem + "' AND Parent_Item='" + CodeOfparent + "'";
                    SqlCommand cmd = new SqlCommand(s11, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                    LoadToDGVOfParentItemsStartly(CodeOfparent);
                    MessageBox.Show("Deleted Sudssful");
                }
            }
            
        }
    

        // Function and Events of Data Grid View Strted From Here 
        // Functions
        private void LoadToDGVOfItems()
        {
            DataTable AllItems = new DataTable();
            string s = string.Format("Code <> '{0}'", CodeOfparent);
            AllItems = Classes.RetrieveData("Code,Name,Name2,Category",s, "Setup_Items");
            ItemsDGV.DataContext = AllItems;
        }    //Done Finall Function

        //Events of Data Grid View 
        private void ItemsDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    String Codeval = (grid.SelectedItem as DataRowView).Row.ItemArray[0].ToString();
                    ShowItems.Visibility = Visibility.Hidden;
                    ParentGrid.Visibility = Visibility.Visible;
                    LoadToDGVOfParentItems(Codeval);
                }
            }
        }           //Done Finall Function

        private void TextDataChange(object sender, TextChangedEventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
            SqlConnection con = new SqlConnection(connString);
            if ((RadioByCode.IsChecked == true || RadioByName.IsChecked == true) && SearchTxt.Text != "")
            { 
                ItemsDGV.DataContext = null;
                if (RadioByCode.IsChecked == true && RadioByName.IsChecked == false)
                {
                    try
                    {
                        con.Open();
                        string s = "select Code,Name,Name2,Category from Setup_Items Where Code Like '%" + SearchTxt.Text + "%'";
                        DataTable dt = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                            da.Fill(dt);

                        ItemsDGV.DataContext = dt;
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
                else if (RadioByName.IsChecked == true && RadioByCode.IsChecked == false)
                {
                    try
                    {
                        con.Open();
                        string s = "select Code,Name,Name2,Category from Setup_Items Where Name Like '%" + SearchTxt.Text + "%'";
                        DataTable dt = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                            da.Fill(dt);

                        ItemsDGV.DataContext = dt;
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
            else
            {
                try
                {
                    con.Open();
                    string s = "select Code,Name,Name2,Category from Setup_Items";
                    DataTable dt = new DataTable();

                    using (SqlDataAdapter da = new SqlDataAdapter(s, con))
                        da.Fill(dt);

                    ItemsDGV.DataContext = dt;
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
        }           //Done Finall Function
    }
}


