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
        int codeTodelete = 0;
        int ToTWeight = 0; int ToTCost = 0;
        string ItemCode = "";
        public BulkWindow(string Code,string Name)
        {
            InitializeComponent();
            FillDGV(Code);
            ItemNametxt.Text = Name;
            ItemCode = Code;
        }

        int DataSavedItems = 0;
        private void FillDGV(string Code)
        {
            BulkItemsDGV.DataContext = null;
            string Where = "";
            ToTWeight = 0; ToTCost = 0;
            DataTable BulkItemsInfos = new DataTable();
            DataTable BulkItemsName = new DataTable();
            DataTable BulkItems = new DataTable();
            BulkItems.Columns.Add("Code");
            BulkItems.Columns.Add("Manual Code");
            BulkItems.Columns.Add("Name");
            BulkItems.Columns.Add("Weight Precentage");
            BulkItems.Columns.Add("Cost Precentage");
            Where = string.Format("Item_Code ='{0}'", Code);
            BulkItemsInfos = Classes.RetrieveData("Code,WeightPrecentage,CostPrecentage", Where, "Setup_BulkItems");
            for(int i=0;i<BulkItemsInfos.Rows.Count;i++)
            {
                Where = string.Format("Code='{0}'", BulkItemsInfos.Rows[i][0].ToString());
                BulkItemsName = Classes.RetrieveData("[Manual Code],Name", Where, "Setup_Items");
                BulkItems.Rows.Add(BulkItemsInfos.Rows[i][0], BulkItemsName.Rows[0][0], BulkItemsName.Rows[0][1], BulkItemsInfos.Rows[i][1], BulkItemsInfos.Rows[i][2]);
                DataSavedItems++;
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
                    if (BulkItems.Rows[i]["Weight Precentage"] != "")
                    {
                        ToTWeight += Convert.ToInt32(BulkItems.Rows[i]["Weight Precentage"]);
                    }
                    if (BulkItems.Rows[i]["Cost Precentage"] != "")
                    {
                        ToTCost += Convert.ToInt32(BulkItems.Rows[i]["Cost Precentage"]);
                    }
                }
            }
            WaisWeightttxt.Text = ToTWeight.ToString();
            WaistCosttxt.Text = ToTCost.ToString();



            //DataTable Data = new DataTable();
            //Data.Columns.Add("Code");
            //Data.Columns.Add("Manual Code");
            //Data.Columns.Add("Name");
            //Data.Columns.Add("Weight Precentage");
            //Data.Columns.Add("Cost Precentage");
            //SqlConnection con = new SqlConnection(connString);
            //SqlCommand cmd = new SqlCommand();
            //SqlDataReader reader = null;
            //SqlConnection con2 = new SqlConnection(connString);
            //SqlCommand cmd2 = new SqlCommand();
            //SqlDataReader reader2 = null;
            //try
            //{
            //    DataSavedItems = 0;
            //    con.Open();
            //    string s = "select * from Setup_BulkItems where Item_Code ='" + Code + "'";
            //    cmd = new SqlCommand(s, con);
            //    reader = cmd.ExecuteReader();
            //    con2.Open();
            //    while (reader.Read())
            //    {
            //        try
            //        {
            //            s = string.Format("select [Manual Code],Name from Setup_Items Where Code='{0}'", reader["Code"]);
            //            cmd2 = new SqlCommand(s, con2);
            //            reader2 = cmd2.ExecuteReader();
            //            reader2.Read();
            //        }
            //        catch (Exception ex){ MessageBox.Show(ex.ToString()); }
            //        Data.Rows.Add(reader["Code"],reader2.GetValue(0),reader2["Name"], reader["WeightPrecentage"], reader["CostPrecentage"]);
            //        reader2.Close();
            //        DataSavedItems++;//That increment the DataSaveItem variable
            //    }
            //    for(int i=0;i<Data.Columns.Count;i++)
            //    {
            //        Data.Columns[i].ReadOnly = true;
            //    }
            //    Data.Columns["Weight Precentage"].ReadOnly=false;
            //    Data.Columns["Cost Precentage"].ReadOnly = false;

            //    BulkItemsDGV.DataContext = Data;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //finally
            //{
            //    reader.Close();
            //    con.Close();
            //}
            //if(Data.Rows.Count>0)
            //{
            //    for(int i=0;i<Data.Rows.Count;i++)
            //    {
            //        if (Data.Rows[i]["Weight Precentage"] != "")
            //        {
            //            ToTWeight += Convert.ToInt32(Data.Rows[i]["Weight Precentage"]);
            //        }
            //        if (Data.Rows[i]["Cost Precentage"] != "")
            //        {
            //            ToTCost += Convert.ToInt32(Data.Rows[i]["Cost Precentage"]);
            //        }
            //    }
            //}
            //WaisWeightttxt.Text = ToTWeight.ToString();
            //WaistCosttxt.Text = ToTCost.ToString();
        }           //Done Finalll Function
        private bool CheckToSave()
        {
            bool Ret = true;
            int Weight = Convert.ToInt32(WaisWeightttxt.Text);
            int Cost = Convert.ToInt32(WaistCosttxt.Text);
            if (Weight <= 100 && Cost <= 100)
            {
                for (int i = 0; i < BulkItemsDGV.Items.Count; i++)
                {
                    if (((DataRowView)BulkItemsDGV.Items[i]).Row.ItemArray[3].ToString() == ""  || ((DataRowView)BulkItemsDGV.Items[i]).Row.ItemArray[4].ToString() =="")
                    {
                        MessageBox.Show("Please Check The Data of Bulks");
                        Ret = false;
                        return false;
                    }
                }
            }
            else if (Weight > 100 && Cost <= 100)
            {
                MessageBox.Show("Please Check the Weight Precentage Because is Maximum of 100%");
                Ret = false;
            }
            else if (Weight <= 100 && Cost > 100)
            {
                MessageBox.Show("Please Check the Cost Precentage Because is Maximum of 100%");
                Ret = false;
            }
            else
            {
                MessageBox.Show("Please Check the Weight & Cost Precentage Because is Maximum of 100%");
                Ret = false;
            }
            return Ret;
        }           //Done Finall Function

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
        }           //Done Finall Function

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }       //Done Finalll Funcrtion
        
        private void Changes_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Calculate Totall Weight and Cost 
            int TotallWeightPre = 0;
            int TotallCostPre = 0;
            if(Convert.ToDouble((e.EditingElement as TextBox).Text) >0 && Convert.ToDouble((e.EditingElement as TextBox).Text) < 100)
            {
                for (int i = 0; i < BulkItemsDGV.Items.Count; i++)
                {
                    if (e.Row.GetIndex() == i)
                    {
                        DataRowView ToTalItemm = BulkItemsDGV.Items[i] as DataRowView;
                        if (e.Column.Header == "Weight Precentage")
                        {
                            if ((e.EditingElement as TextBox).Text != "")
                            {
                                TotallWeightPre += Convert.ToInt32((e.EditingElement as TextBox).Text);
                            }
                            if (ToTalItemm.Row.ItemArray[4].ToString() != "")
                            {
                                TotallCostPre += Convert.ToInt32(ToTalItemm.Row.ItemArray[4]);
                            }
                        }
                        else
                        {
                            if (ToTalItemm.Row.ItemArray[3].ToString() != "")
                            {
                                TotallWeightPre += Convert.ToInt32(ToTalItemm.Row.ItemArray[3]);
                            }
                            if ((e.EditingElement as TextBox).Text != "")
                            {
                                TotallCostPre += Convert.ToInt32((e.EditingElement as TextBox).Text);
                            }
                        }
                    }
                    else
                    {
                        DataRowView ToTalItemm = BulkItemsDGV.Items[i] as DataRowView;
                        if (ToTalItemm.Row.ItemArray[3].ToString() != "")
                        {
                            TotallWeightPre += Convert.ToInt32(ToTalItemm.Row.ItemArray[3]);
                        }
                        if (ToTalItemm.Row.ItemArray[4].ToString() != "")
                        {
                            TotallCostPre += Convert.ToInt32(ToTalItemm.Row.ItemArray[4]);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please check The Value at Item " + "'" + ((DataRowView)BulkItemsDGV.Items[e.Row.GetIndex()]).Row.ItemArray[2] + "'" + " that you Entered  That Must be Between 0 To 100");
            }
            BulkItemsDGV.Focus();
            //ItemNametxt.Focus();
            WaisWeightttxt.Text = TotallWeightPre.ToString();
            WaistCosttxt.Text = TotallCostPre.ToString();
        }           //Done Finall Function

        private void AddItemClick(object sender, RoutedEventArgs e)
        {
            BulkItemsDGV.Visibility = Visibility.Hidden;
            LoadToDGVOfItems();
            ItemsDGV.Visibility = Visibility.Visible;
        }     //Done Finall Function
        private void LoadToDGVOfItems()
        {
            DataTable AllItems = new DataTable();
            string s = string.Format("Code <> '{0}'", ItemCode);
            AllItems = Classes.RetrieveData("Code,[Manual Code],Name,Category",s, "Setup_Items");
            ItemsDGV.DataContext = AllItems;
        }                       //Done Finall Functiuon

        private void ItemsDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    String Codeval = (grid.SelectedItem as DataRowView).Row.ItemArray[0].ToString();
                    ItemsDGV.Visibility = Visibility.Hidden;
                    BulkItemsDGV.Visibility = Visibility.Visible;
                    LoadToDGVOfBulkItems(Codeval);
                }
            }
        }               //DOne Finall Function
        private void LoadToDGVOfBulkItems(string code)
        {
            //Michael's Update
            ToTWeight = 0; ToTCost = 0;
            string Where = "";
            DataTable BulkItems = new DataTable();
            DataTable BulkItemsInfos = new DataTable();
            BulkItems = ((DataTable)BulkItemsDGV.DataContext);
            Where = string.Format("Code='{0}'", code);
            BulkItemsInfos = Classes.RetrieveData("Code,[Manual Code],Name", Where, "Setup_Items");
            BulkItems.Rows.Add(BulkItemsInfos.Rows[0][0], BulkItemsInfos.Rows[0][1], BulkItemsInfos.Rows[0][2]);
            BulkItemsDGV.DataContext = BulkItems;
            if (BulkItemsDGV.Items.Count > 0)
            {
                for (int i = 0; i < BulkItemsDGV.Items.Count; i++)
                {
                    DataRowView ToTalItemm = BulkItemsDGV.Items[i] as DataRowView;
                    if (ToTalItemm.Row.ItemArray[3].ToString() != "")
                    {
                        ToTWeight += Convert.ToInt32(ToTalItemm.Row.ItemArray[3]);
                    }
                    if (ToTalItemm.Row.ItemArray[4].ToString() != "")
                    {
                        ToTCost += Convert.ToInt32(ToTalItemm.Row.ItemArray[4]);
                    }
                }
            }
            WaisWeightttxt.Text = ToTWeight.ToString();
            WaistCosttxt.Text = ToTCost.ToString();
        }               //Done Finall Function
        private void BulkItemsDGV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    codeTodelete = grid.SelectedIndex;
                }
            }
            catch { }
           
        }           //Done Finall Function
        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            DataTable dt = BulkItemsDGV.DataContext as DataTable;
            dt.Rows.RemoveAt(codeTodelete);
            BulkItemsDGV.DataContext = dt;
            if (BulkItemsDGV.Items.Count > 0)
            {
                for (int i = 0; i < BulkItemsDGV.Items.Count; i++)
                {
                    DataRowView ToTalItemm = BulkItemsDGV.Items[i] as DataRowView;
                    if (ToTalItemm.Row.ItemArray[3].ToString() != "")
                    {
                        ToTWeight += Convert.ToInt32(ToTalItemm.Row.ItemArray[3]);
                    }
                    if (ToTalItemm.Row.ItemArray[4].ToString() != "")
                    {
                        ToTCost += Convert.ToInt32(ToTalItemm.Row.ItemArray[4]);
                    }
                }
            }
            WaisWeightttxt.Text = ToTWeight.ToString();
            WaistCosttxt.Text = ToTCost.ToString();
        }           //Done Finall Function
        
    }


}


      
