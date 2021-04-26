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

        //Michael's Update
        public ParentWindow(string Code, string Name)
        {
            InitializeComponent();
            LoadToDGVOfParentItemsStartly(Code);
            CodeOfparent = Code;
            ItemNametxt.Text = Name;
        }           //Done
        private void LoadToDGVOfParentItemsStartly(string Code)
        {
            string Where = ""; DataTable ParentsItems = new DataTable();
            Where = string.Format("Parent_Item='{0}'", Code);
            ParentsItems = Classes.RetrieveData("Code,(Select Name From Setup_Items Where Code=Setup_ParentItems.Code) Name", Where, "Setup_ParentItems");
            ParentItemsDGV.DataContext = ParentsItems;
        }           //Done FInall Function
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ParentItemsView.Visibility = Visibility.Hidden;
            LoadToDGVOfItems();
            AllItemsView.Visibility = Visibility.Visible;
        }  //Done Finall Function
        private void LoadToDGVOfItems()
        {
            DataTable AllItems = new DataTable();
            string s = string.Format("Code <> '{0}' AND Active=1", CodeOfparent);
            AllItems = Classes.RetrieveData("Code,Name,Name2,Category", s, "Setup_Items");
            ItemsDGV.DataContext = AllItems;
        }    //Done Finall Function
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            DataTable DT = ParentItemsDGV.DataContext as DataTable;

            DT.Rows.RemoveAt(ParentItemsDGV.SelectedIndex);
            ParentItemsDGV.DataContext = DT;
        }           //Done
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        { this.Close(); }   // Done Finall Function
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string Where = string.Format("Parent_Item='{0}'", CodeOfparent);
            Classes.DeleteRows(Where, "Setup_ParentItems");
            for (int i = 0; i < ParentItemsDGV.Items.Count; i++)
            {
                string Values = string.Format("'{0}','{1}'", ((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[0], CodeOfparent);
                Classes.InsertRow("Setup_ParentItems", Values);
            }
            MessageBox.Show("Saved Sucsseful");
        }
        private void ItemsDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {

                    String Codeval = (grid.SelectedItem as DataRowView).Row.ItemArray[0].ToString();
                    for (int i = 0; i < ParentItemsDGV.Items.Count; i++)
                    {
                        if (((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[0].ToString() == Codeval)
                        {
                            MessageBox.Show("Item Is Exist !!");
                            return;
                        }
                    }
                    AllItemsView.Visibility = Visibility.Hidden;
                    ParentItemsView.Visibility = Visibility.Visible;
                    LoadToDGVOfBulkItems(Codeval);
                }
            }
        }           //Done Finall Function

        private void LoadToDGVOfBulkItems(string code)
        {
            //Michael's Update
            string Where = "";
            DataTable ParentsItems = new DataTable(); DataTable ParentItemsInfos = new DataTable();
            ParentsItems = ((DataTable)ParentItemsDGV.DataContext);
            Where = string.Format("Code='{0}'", code);
            ParentItemsInfos = Classes.RetrieveData("Code,Name", Where, "Setup_Items");
            ParentsItems.Rows.Add(ParentItemsInfos.Rows[0][0], ParentItemsInfos.Rows[0][1]);
            ParentItemsDGV.DataContext = ParentsItems;
        }               //DOne
        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable AllItems = new DataTable();
            if ((RadioByCode.IsChecked == true || RadioByName.IsChecked == true) && SearchTxt.Text != "")
            {
                if (RadioByCode.IsChecked == true && RadioByName.IsChecked == false)
                {
                    string s = string.Format("Code <> '{0}' AND Active=1 AND (Code Like N'%{1}%' OR [Manual Code] Like N'%{1}%')", CodeOfparent, SearchTxt.Text);
                    AllItems = Classes.RetrieveData("Code,[Manual Code] as 'Manual Code',Name,Category", s, "Setup_Items");
                }
                else if (RadioByName.IsChecked == true && RadioByCode.IsChecked == false)
                {
                    string s = string.Format("Code <> '{0}' AND Active=1 AND (Name Like N'%{1}%' OR Name2 Like N'%{1}%')", CodeOfparent, SearchTxt.Text);
                    AllItems = Classes.RetrieveData("Code,[Manual Code] as 'Manual Code',Name,Category", s, "Setup_Items");
                }
            }
            else
            {
                string s = string.Format("Code <> '{0}' AND Active=1", CodeOfparent);
                AllItems = Classes.RetrieveData("Code,[Manual Code] as 'Manual Code',Name,Category", s, "Setup_Items");
            }
            ItemsDGV.DataContext = AllItems;
        }

        private void BackClickBtn(object sender, RoutedEventArgs e)
        {
            AllItemsView.Visibility = Visibility.Hidden;
            ParentItemsDGV.Visibility = Visibility.Visible;
        }       //Done
    }
}


