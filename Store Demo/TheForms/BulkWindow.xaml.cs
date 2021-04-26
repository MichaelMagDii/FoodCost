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
    /// Interaction logic for BulkWindow.xaml
    /// </summary>
    public partial class BulkWindow : Window
    {
        int codeTodelete = 0; int ToTWeight = 0; int ToTCost = 0;
        string ItemCode = "";
        public BulkWindow(string Code, string Name)
        {
            InitializeComponent();
            FillDGV(Code);
            ItemNametxt.Text = Name;
            ItemCode = Code;
        }           //Done FInall
        private void FillDGV(string Code)
        {
            BulkItemsDGV.DataContext = null;
            DataTable BulkItemsInfos = new DataTable();    DataTable BulkItemsName = new DataTable();     DataTable BulkItems = new DataTable();
            BulkItems.Columns.Add("Code");
            BulkItems.Columns.Add("Manual Code");
            BulkItems.Columns.Add("Name");
            BulkItems.Columns.Add("Weight Precentage");
            BulkItems.Columns.Add("Cost Precentage");
            string Where = ""; ToTWeight = 0; ToTCost = 0;
            Where = string.Format("Item_Code ='{0}'", Code);
            BulkItemsInfos = Classes.RetrieveData("Code,WeightPrecentage as 'Weight Precentage',CostPrecentage as 'Cost Precentage'", Where, "Setup_BulkItems");
            for (int i = 0; i < BulkItemsInfos.Rows.Count; i++)
            {
                Where = string.Format("Code='{0}'", BulkItemsInfos.Rows[i][0].ToString());
                BulkItemsName = Classes.RetrieveData("[Manual Code] as 'Manual Code',Name", Where, "Setup_Items");
                BulkItems.Rows.Add(BulkItemsInfos.Rows[i][0], BulkItemsName.Rows[0][0], BulkItemsName.Rows[0][1], BulkItemsInfos.Rows[i][1], BulkItemsInfos.Rows[i][2]);
            }
            for (int i = 0; i < BulkItems.Columns.Count; i++)
            {
                BulkItems.Columns[i].ReadOnly = true;
            }
            BulkItems.Columns["Weight Precentage"].ReadOnly = false;
            BulkItems.Columns["Cost Precentage"].ReadOnly = false;
            BulkItemsDGV.DataContext = BulkItems;
            if (BulkItems.Rows.Count > 0)
            {
                for (int i = 0; i < BulkItems.Rows.Count; i++)
                {
                    if (BulkItems.Rows[i]["Weight Precentage"].ToString() != "")
                    {   ToTWeight += Convert.ToInt32(BulkItems.Rows[i]["Weight Precentage"]);        }
                    if (BulkItems.Rows[i]["Cost Precentage"].ToString() != "")
                    {   ToTCost += Convert.ToInt32(BulkItems.Rows[i]["Cost Precentage"]);       }
                }
            }
            WaisWeightttxt.Text = ToTWeight.ToString();
            WaistCosttxt.Text = ToTCost.ToString();
        }           //Done
        private void AddItemClick(object sender, RoutedEventArgs e)
        {
            AllItemsView.Visibility = Visibility.Visible;
            BulkItemsView.Visibility = Visibility.Hidden;
            LoadToDGVOfItems();
            ItemsDGV.Visibility = Visibility.Visible;
        }     //Done
        private void LoadToDGVOfItems()
        {
            DataTable AllItems = new DataTable();
            string s = string.Format("Code <> '{0}' AND Active=1", ItemCode);
            AllItems = Classes.RetrieveData("Code,[Manual Code] as 'Manual Code',Name,Category", s, "Setup_Items");
            ItemsDGV.DataContext = AllItems;
        }            //Done
        private void ItemsDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {

                    String Codeval = (grid.SelectedItem as DataRowView).Row.ItemArray[0].ToString();
                    for(int i=0;i<BulkItemsDGV.Items.Count;i++)
                    {
                        if (((DataRowView)BulkItemsDGV.Items[i]).Row.ItemArray[0].ToString() == Codeval)
                        {
                            MessageBox.Show("Item Is Exist !!");
                            return;
                        }
                    }
                    AllItemsView.Visibility = Visibility.Hidden;
                    BulkItemsView.Visibility = Visibility.Visible;
                    LoadToDGVOfBulkItems(Codeval);
                }
            }
        }               //Done
        private void LoadToDGVOfBulkItems(string code)
        {
            //Michael's Update
            ToTWeight = 0; ToTCost = 0;   string Where = "";
            DataTable BulkItems = new DataTable();   DataTable BulkItemsInfos = new DataTable();
            BulkItems = ((DataTable)BulkItemsDGV.DataContext);
            Where = string.Format("Code='{0}'", code);
            BulkItemsInfos = Classes.RetrieveData("Code,[Manual Code] as 'Manual Code',Name", Where, "Setup_Items");
            BulkItems.Rows.Add(BulkItemsInfos.Rows[0][0], BulkItemsInfos.Rows[0][1], BulkItemsInfos.Rows[0][2]);
            BulkItemsDGV.DataContext = BulkItems;
            if (BulkItemsDGV.Items.Count > 0)
            {
                for (int i = 0; i < BulkItemsDGV.Items.Count; i++)
                {
                    DataRowView ToTalItemm = BulkItemsDGV.Items[i] as DataRowView;
                    if (ToTalItemm.Row.ItemArray[3].ToString() != "")
                    {  ToTWeight += Convert.ToInt32(ToTalItemm.Row.ItemArray[3]);      }
                    if (ToTalItemm.Row.ItemArray[4].ToString() != "")
                    {  ToTCost += Convert.ToInt32(ToTalItemm.Row.ItemArray[4]);        }
                }
            }
            WaisWeightttxt.Text = ToTWeight.ToString();
            WaistCosttxt.Text = ToTCost.ToString();
        }               //DOne
        private void BackClickBtn(object sender, RoutedEventArgs e)
        {
            BulkItemsView.Visibility = Visibility.Visible;
            AllItemsView.Visibility = Visibility.Hidden;
        }           //DOne
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }       //Done
        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            ToTWeight = 0;ToTCost = 0;
            DataTable DT = BulkItemsDGV.DataContext as DataTable;
            DT.Rows.RemoveAt(BulkItemsDGV.SelectedIndex);
            BulkItemsDGV.DataContext = DT;
            if (BulkItemsDGV.Items.Count > 0)
            {
                for (int i = 0; i < BulkItemsDGV.Items.Count; i++)
                {
                    DataRowView ToTalItemm = BulkItemsDGV.Items[i] as DataRowView;
                    if (ToTalItemm.Row.ItemArray[3].ToString() != "")
                    {  ToTWeight += Convert.ToInt32(ToTalItemm.Row.ItemArray[3]);        }
                    if (ToTalItemm.Row.ItemArray[4].ToString() != "")
                    {  ToTCost += Convert.ToInt32(ToTalItemm.Row.ItemArray[4]);          }
                }
            }
            WaisWeightttxt.Text = ToTWeight.ToString();
            WaistCosttxt.Text = ToTCost.ToString();
        }           //Done
        private void Changes_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //Michael's Update
            Double TotallWeightPre = 0; Double TotallCostPre = 0;
            if (Convert.ToDouble((e.EditingElement as TextBox).Text) > 0 && Convert.ToDouble((e.EditingElement as TextBox).Text) < 100)
            {
                for (int i = 0; i < BulkItemsDGV.Items.Count; i++)
                {
                    if (e.Row.GetIndex() == i)
                    {
                        if (e.Column.Header.ToString() == "Weight Precentage")
                        {
                            if ((e.EditingElement as TextBox).Text != "")
                            { TotallWeightPre += Convert.ToDouble((e.EditingElement as TextBox).Text); }
                            if ((BulkItemsDGV.Items[i] as DataRowView).Row.ItemArray[4].ToString() != "")
                            { TotallCostPre += Convert.ToDouble((BulkItemsDGV.Items[i] as DataRowView).Row.ItemArray[4]); }
                        }
                        else
                        {
                            if ((e.EditingElement as TextBox).Text != "")
                            { TotallCostPre += Convert.ToDouble((e.EditingElement as TextBox).Text); }
                            if ((BulkItemsDGV.Items[i] as DataRowView).Row.ItemArray[3].ToString() != "")
                            { TotallWeightPre += Convert.ToDouble((BulkItemsDGV.Items[i] as DataRowView).Row.ItemArray[3]); }
                        }
                    }
                    else
                    {
                        if ((BulkItemsDGV.Items[i] as DataRowView).Row.ItemArray[3].ToString() != "")
                        { TotallWeightPre += Convert.ToDouble((BulkItemsDGV.Items[i] as DataRowView).Row.ItemArray[3]); }
                        if ((BulkItemsDGV.Items[i] as DataRowView).Row.ItemArray[4].ToString() != "")
                        { TotallCostPre += Convert.ToDouble((BulkItemsDGV.Items[i] as DataRowView).Row.ItemArray[4]); }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please check The Value at Item " + "'" + ((DataRowView)BulkItemsDGV.Items[e.Row.GetIndex()]).Row.ItemArray[2] + "'" + " that you Entered  That Must be Between 0 To 100");
            }
            BulkItemsDGV.Focus();
            WaisWeightttxt.Text = TotallWeightPre.ToString();
            WaistCosttxt.Text = TotallCostPre.ToString();
        }           //Done
        private bool CheckToSave()
        {
            //Michael's Update 
            int Weight = Convert.ToInt32(WaisWeightttxt.Text);
            int Cost = Convert.ToInt32(WaistCosttxt.Text);
            if (Weight <= 100 && Cost <= 100)
            {
                for (int i = 0; i < BulkItemsDGV.Items.Count; i++)
                {
                    if (((DataRowView)BulkItemsDGV.Items[i]).Row.ItemArray[3].ToString() == "" || ((DataRowView)BulkItemsDGV.Items[i]).Row.ItemArray[4].ToString() == "")
                    {
                        MessageBox.Show("Please Check The Data of Bulks"); return false;
                    }
                }
            }
            else if (Weight > 100 && Cost <= 100)
            {
                MessageBox.Show("Please Check the Weight Precentage Because is Maximum of 100%"); return false;
            }
            else if (Weight <= 100 && Cost > 100)
            {
                MessageBox.Show("Please Check the Cost Precentage Because is Maximum of 100%"); return false;
            }
            else
            {
                MessageBox.Show("Please Check the Weight & Cost Precentage Because is Maximum of 100%"); return false;
            }
            return true;
        }           //Done
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //Michael's UPdate
            string Where = "";string Values = "";
            if(CheckToSave() == true)
            {
                Where = string.Format("Item_Code='{0}'", ItemCode);
                Classes.DeleteRows(Where, "Setup_BulkItems");

                for(int i=0;i<BulkItemsDGV.Items.Count;i++)
                {
                    DataRowView Items = BulkItemsDGV.Items[i] as DataRowView;
                    Values = string.Format("'{0}',{1},{2},'{3}'", Items.Row.ItemArray[0], Items.Row.ItemArray[3], Items.Row.ItemArray[4], ItemCode);
                    Classes.InsertRow("Setup_BulkItems", Values);
                }
                FillDGV(ItemCode);
                MessageBox.Show("Saved Sucssfully");
            }
        }           //Done
        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable AllItems = new DataTable();
            if ((RadioByCode.IsChecked == true || RadioByName.IsChecked == true) && SearchTxt.Text != "")
            {
                if (RadioByCode.IsChecked == true && RadioByName.IsChecked == false)
                {
                    string s = string.Format("Code <> '{0}' AND Active=1 AND (Code Like N'%{1}%' OR [Manual Code] Like N'%{1}%')", ItemCode, SearchTxt.Text);
                    AllItems = Classes.RetrieveData("Code,[Manual Code] as 'Manual Code',Name,Category", s, "Setup_Items");
                }
                else if (RadioByName.IsChecked == true && RadioByCode.IsChecked == false)
                {
                    string s = string.Format("Code <> '{0}' AND Active=1 AND (Name Like N'%{1}%' OR Name2 Like N'%{1}%')", ItemCode,SearchTxt.Text);
                    AllItems = Classes.RetrieveData("Code,[Manual Code] as 'Manual Code',Name,Category", s, "Setup_Items");
                }
            }
            else
            {
                string s = string.Format("Code <> '{0}' AND Active=1", ItemCode);
                AllItems = Classes.RetrieveData("Code,[Manual Code] as 'Manual Code',Name,Category", s, "Setup_Items");
            }
            ItemsDGV.DataContext = AllItems;

        }       //Done
    }


}


      
