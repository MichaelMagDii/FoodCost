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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for ExpireDate.xaml
    /// </summary>
    public partial class ExpireDate : Window
    {
        string ExpireForm = "";
        string sysFormat = "";
        public static Dictionary<string, List<Tuple<string, string>>> ItemExpireDate = new Dictionary<string, List<Tuple<string, string>>>();
        DataTable DT = new DataTable();
        string Code = ""; string Qty = "";
             
        // Recieve Order OR Auto Recieve 
        public ExpireDate(string TheCode, string TheQty)
        {
            InitializeComponent();
            RoExpire.Visibility = Visibility.Visible;
            TransferExpire.Visibility = Visibility.Hidden;
            ExpireForm = "RO";
            Code = TheCode;
            Qty = TheQty;
            TheQtytxt.Text = Qty;

            if (ItemExpireDate.Count == 0)
            {
                LoadTODataGrid(Code, Qty);
            }
            else
            {
                if (ItemExpireDate.ContainsKey(Code))
                {
                    PutDataToGrid(Code);
                }
                else
                {
                    LoadTODataGrid(Code, Qty);
                }
            }

        }
        private void PutDataToGrid(string Code)
        {
            string ManualCode = ""; string Name = ""; string Name2 = "";
            DataTable Dt = new DataTable();
            DT.Columns.Add("Code");
            DT.Columns.Add("Manual Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Name2");
            DT.Columns.Add("Qty");
            DT.Columns.Add("Expire Date");
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = string.Format("select [Manual Code],Name,Name2 from Setup_Items where Code='{0}'", Code);
                SqlCommand cmd = new SqlCommand(s, con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                ManualCode = reader["Manual Code"].ToString();
                Name = reader["Name"].ToString();
                Name2 = reader["Name2"].ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            foreach (Tuple<string, string> tuple in ItemExpireDate[Code])
            {
                DT.Rows.Add(Code, ManualCode, Name, Name2, tuple.Item1, Convert.ToDateTime(tuple.Item2).ToString("dd-MM-yyyy"));
            }
            for (int i = 0; i < DT.Columns.Count; i++)
            {
                DT.Columns[i].ReadOnly = true;
            }
            DT.Columns["Qty"].ReadOnly = false;
            ItemsExpireDGV.DataContext = DT;         
        }
        private void LoadTODataGrid(string TheCode, string TheQty)
        {
            DataTable Dt = new DataTable();
            DT.Columns.Add("Code");
            DT.Columns.Add("Manual Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Name2");
            DT.Columns.Add("Qty");
            DT.Columns.Add("Expire Date");
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = string.Format("select [Manual Code],Name,Name2 from Setup_Items where Code='{0}'", TheCode);
                SqlCommand cmd = new SqlCommand(s, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DT.Rows.Add(Code, reader["Manual Code"], reader["Name"], reader["Name2"], "", "");
                    for (int i = 0; i < DT.Columns.Count; i++)
                    {
                        DT.Columns[i].ReadOnly = true;
                    }
                    DT.Columns["Qty"].ReadOnly = false;
                    ItemsExpireDGV.DataContext = DT;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }           
        }
        private void ProductionRdo_Checked(object sender, RoutedEventArgs e)
        {
            ExpireGrid.Visibility = Visibility.Hidden;
            ProductionGrid.Visibility = Visibility.Visible;
        }
        private void ExpireRdo_Checked(object sender, RoutedEventArgs e)
        {
            ProductionGrid.Visibility = Visibility.Hidden;
            ExpireGrid.Visibility = Visibility.Visible;

        }
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            int TheValue = 0;
            if (ItemsExpireDGV.SelectedIndex >= 0)
            {
                if (!DoSomeChecksOFDate(e))
                    return;

                DT = ItemsExpireDGV.DataContext as DataTable;
                DT.Columns["Expire Date"].ReadOnly = false;
                if ((e.OriginalSource as Button).Name.ToString() == "ExpireDone")
                {
                    DT.Rows[ItemsExpireDGV.SelectedIndex]["Expire Date"] =Convert.ToDateTime(ExpireDatePkr.Text).ToString("dd-MM-yyyy");
                }
                else if ((e.OriginalSource as Button).Name.ToString() == "ProductionDone")
                {
                    string[] TheDate =Convert.ToDateTime(ProductionDatepkr.Text).ToString("MM-dd-yyyy").Split('-');

                    if ((PreiodicCbx.SelectedItem as ComboBoxItem).Content.ToString() == "Days")
                    {
                        TheValue = Convert.ToInt32(Priodictxt.Text) + Convert.ToInt32(TheDate[1]);
                        if (TheValue > 30)
                        {
                            TheDate[0] = (Convert.ToInt32(TheDate[0]) + ((TheValue / 30))).ToString();
                            TheDate[1] = (TheValue % 30).ToString();
                            if ((Convert.ToInt32(TheDate[0])) > 12)
                            {
                                TheValue = (Convert.ToInt32(TheDate[0]));
                                TheDate[2] = (Convert.ToInt32(TheDate[2]) + ((TheValue / 12))).ToString();
                                TheDate[0] = ((Convert.ToInt32(TheDate[0]) + ((TheValue % 12) - 1)) - 12).ToString();
                            }
                        }
                        else
                        {
                            TheDate[1] = TheValue.ToString();
                        }
                    }

                    else if ((PreiodicCbx.SelectedItem as ComboBoxItem).Content.ToString() == "Months")
                    {
                        TheValue = Convert.ToInt32(Priodictxt.Text) + Convert.ToInt32(TheDate[0]);
                        if (TheValue > 12)
                        {
                            TheDate[2] = (Convert.ToInt32(TheDate[2]) + ((TheValue / 12))).ToString();
                            TheDate[0] = (TheValue % 12).ToString();
                        }
                        else
                        {
                            TheDate[0] = TheValue.ToString();
                        }
                    }

                    else if ((PreiodicCbx.SelectedItem as ComboBoxItem).Content.ToString() == "Years")
                    {
                        TheValue = Convert.ToInt32(Priodictxt.Text) + Convert.ToInt32(TheDate[2]);
                        TheDate[2] = TheValue.ToString();
                    }

                    DT.Rows[ItemsExpireDGV.SelectedIndex]["Expire Date"] =Convert.ToDateTime(TheDate[2] + "/" + TheDate[0] + "/" + TheDate[1]).ToString("dd-MM-yyyy");

                }
                DT.Columns["Expire Date"].ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Please Choose The Item");
            }
        }
        private bool DoSomeChecksOFSaving()
        {
            if (ExpireForm == "RO")
            {
                int QtyColumns = 0;
                int QtyValue = 0;
                int DateColumns = 0;
                for (int i = 0; i < ItemsExpireDGV.Items.Count; i++)
                {
                    if ((ItemsExpireDGV.Items[i] as DataRowView).Row.ItemArray[4].ToString() != "")
                    {
                        QtyColumns++;
                        QtyValue += Convert.ToInt32((ItemsExpireDGV.Items[i] as DataRowView).Row.ItemArray[4].ToString());
                    }

                    if ((ItemsExpireDGV.Items[i] as DataRowView).Row.ItemArray[5].ToString() != "")
                        DateColumns++;
                }

                if (QtyColumns != ItemsExpireDGV.Items.Count || DateColumns != ItemsExpireDGV.Items.Count)
                {
                    MessageBox.Show("Please Check The Data !");
                    return false;
                }

                if (QtyValue.ToString() != TheQtytxt.Text)
                {
                    MessageBox.Show("You have a Wrong Number at Qty");
                    return false;
                }
            }
            else if(ExpireForm== "Requests")
            {
                int TheValue = 0;
                for (int i = 0; i < ItemsExpireTransferDGV.Items.Count; i++)
                {
                    if((ItemsExpireTransferDGV.Items[i] as DataRowView).Row.ItemArray[0].ToString() == "True")
                    {
                        if ((ItemsExpireTransferDGV.Items[i] as DataRowView).Row.ItemArray[7].ToString() != "")
                        {
                            TheValue += Convert.ToInt32((ItemsExpireTransferDGV.Items[i] as DataRowView).Row.ItemArray[7].ToString());
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                if (TheValue.ToString() != TheExpireQtytxt.Text)
                {
                    MessageBox.Show("You have a Wrong Number at Qty");
                    return false;
                }
            }

            return true;
        }
        private void ADD_Click(object sender, RoutedEventArgs e)
        {
            DT = ItemsExpireDGV.DataContext as DataTable;
            for (int i = 0; i < DT.Columns.Count; i++)
            {
                DT.Columns[i].ReadOnly = false;
            }
            DT.Rows.Add(Code, DT.Rows[0]["Manual Code"].ToString(), DT.Rows[0]["Name"].ToString(), DT.Rows[0]["Name2"].ToString(), " ", "");
            for (int i = 0; i < DT.Columns.Count; i++)
            {
                DT.Columns[i].ReadOnly = true;
            }
            DT.Columns["Qty"].ReadOnly = false;
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DT = ItemsExpireDGV.DataContext as DataTable;
            if (DT.Rows.Count > 1)
            {
                DT.Rows.RemoveAt(DT.Rows.Count - 1);
            }
            ItemsExpireDGV.DataContext = DT;
        }

        /// Requests
        public ExpireDate(string TheCode,string TheQty,string ResturantCode,string FromKitchenCode)
        {
            InitializeComponent();
            RoExpire.Visibility = Visibility.Hidden;
            TransferExpire.Visibility = Visibility.Visible;
            ExpireForm = "Requests";
            Code = TheCode;
            Qty = TheQty;
            TheExpireQtytxt.Text = TheQty;

            if (ItemExpireDate.Count == 0)
            {
                LoadTODataGrid(TheCode,TheQty,ResturantCode,FromKitchenCode);
            }
            else
            {
                if (ItemExpireDate.ContainsKey(Code))
                {
                    PutDataToGrid(Code,ResturantCode,FromKitchenCode);
                }
                else
                {
                    LoadTODataGrid(TheCode, TheQty, ResturantCode, FromKitchenCode);

                }
            }
        }

        private void PutDataToGrid(string TheCode,string Restaurantid,string Kitchenid)
        {
            DataTable Dt = new DataTable();
            DT.Columns.Add("Selected", typeof(bool));
            DT.Columns.Add("Code");
            DT.Columns.Add("Manual Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Name2");
            DT.Columns.Add("Qty");
            DT.Columns.Add("Expire Date");
            DT.Columns.Add("Selected Qty");
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = string.Format("select * from ItemsExpireDate where RestaurantID='{0}' and KitchenID='{1}' and ItemID='{2}'", Restaurantid, Kitchenid, TheCode);
                SqlCommand cmd = new SqlCommand(s, con);
                SqlDataReader reader = cmd.ExecuteReader();
                con2.Open();
                while (reader.Read())
                {
                    string Q = string.Format("select [Manual Code],Name,Name2 from Setup_Items where Code='{0}'", TheCode);
                    SqlCommand cmd2 = new SqlCommand(Q, con2);
                    //SqlDataReader reader2 = null;
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    reader2.Read();
                    DT.Rows.Add(false, TheCode, reader2["Manual Code"], reader2["Name"], reader2["Name2"], reader["Qty"],Convert.ToDateTime(reader["ExpireDate"]).ToString("dd-MM-yyyy"), " ");
                    reader2.Close();
                }               
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { con.Close(); con2.Close(); }

            try
            {
                foreach (Tuple<string, string> tuple in ItemExpireDate[TheCode])
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if(tuple.Item2 ==DT.Rows[i]["Expire Date"].ToString())
                        {
                            DT.Rows[i]["Selected"] = true;
                            DT.Rows[i]["Selected Qty"] = tuple.Item1;
                        }
                    }
                }
            }
            catch { }
            for (int i = 0; i < DT.Columns.Count; i++)
            {
                DT.Columns[i].ReadOnly = true;
            }
            DT.Columns["Selected"].ReadOnly = false;
            DT.Columns["Selected Qty"].ReadOnly = false;
            ItemsExpireTransferDGV.DataContext = DT;



        }
        private void LoadTODataGrid(string TheCode, string TheQty,string ResturantCode,string FromKitchenCode)
        {
            DataTable Dt = new DataTable();
            DT.Columns.Add("Selected",typeof(bool));
            DT.Columns.Add("Code");
            DT.Columns.Add("Manual Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Name2");
            DT.Columns.Add("Qty");
            DT.Columns.Add("Expire Date");
            DT.Columns.Add("Selected Qty");
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = string.Format("select * from ItemsExpireDate where RestaurantID='{0}' and KitchenID='{1}' and ItemID='{2}'", ResturantCode, FromKitchenCode, TheCode);
                SqlCommand cmd = new SqlCommand(s, con);
                SqlDataReader reader = cmd.ExecuteReader();
                con2.Open();
                while (reader.Read())
                {
                    string Q = string.Format("select [Manual Code],Name,Name2 from Setup_Items where Code='{0}'", TheCode);
                    SqlCommand cmd2 = new SqlCommand(Q, con2);
                    //SqlDataReader reader2 = null;
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    reader2.Read();
                    DT.Rows.Add(false, TheCode, reader2["Manual Code"], reader2["Name"], reader2["Name2"],reader["Qty"],Convert.ToDateTime(reader["ExpireDate"]).ToString("dd-MM-yyyy"), " ");
                    reader2.Close();
                }
                for(int i=0;i<DT.Columns.Count;i++)
                {
                    DT.Columns[i].ReadOnly = true;
                }
                DT.Columns["Selected"].ReadOnly = false;
                DT.Columns["Selected Qty"].ReadOnly = false;
                ItemsExpireTransferDGV.DataContext = DT;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { con.Close();con2.Close(); }
        }

        /// Transfers
        public ExpireDate(string TransactionID,string TheQTy,string TheCode)
        {
            InitializeComponent();
            RoExpire.Visibility = Visibility.Visible;
            TransferExpire.Visibility = Visibility.Hidden;
            ExpireForm = "RO";
            Code = TheCode;
            Qty = TheQTy;
            TheQtytxt.Text = TheQTy;

            if (ItemExpireDate.Count == 0)
            {
                LoadTODataGrid(TransactionID,TheQTy,TheCode);
            }
            else
            {
                if (ItemExpireDate.ContainsKey(Code))
                {
                    PutDataToGrid(TransactionID);
                }
                else
                {
                    LoadTODataGrid(TransactionID,TheQTy,TheCode);

                }
            }
        }

        private void LoadTODataGrid(string TransactionID,string TheQty,string TheCode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Code");
            dt.Columns.Add("Manual Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("Name2");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Expire Date");
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
          
            try
            {
                con.Open();
                string s = string.Format("select Item_ID,Qty,ExpireDate from Requests_ItemsExpireDate where Item_ID='{0}' and Request_ID='{1}'", TheCode,TransactionID);
                SqlCommand cmd = new SqlCommand(s, con);
                SqlDataReader reader = cmd.ExecuteReader();
                con2.Open();
                while (reader.Read())
                {
                    string q = string.Format("select [Manual code],Name,Name2 from Setup_items where code='{0}'", TheCode);
                    SqlCommand cmd2 = new SqlCommand(q, con2);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    reader2.Read();
                    dt.Rows.Add(TheCode, reader2["Manual code"], reader2["Name"], reader2["Name2"], reader["Qty"], Convert.ToDateTime(reader["Expiredate"]).ToString("dd-MM-yyyy"));
                    reader2.Close();
                }
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = true;
                }
                dt.Columns["Qty"].ReadOnly = false;
                ItemsExpireDGV.DataContext = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { con.Close(); con2.Close(); }
        }


        //Events Or Butoons

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ExpireForm == "RO")
            {
                if (!DoSomeChecksOFSaving())
                    return;

                if (ItemExpireDate.ContainsKey(Code))
                {
                    ItemExpireDate.Remove(Code);
                    ItemExpireDate[Code] = new List<Tuple<string, string>>();
                    for (int i = 0; i < ItemsExpireDGV.Items.Count; i++)
                    {
                        ItemExpireDate[Code].Add(new Tuple<string, string>((ItemsExpireDGV.Items[i] as DataRowView).Row.ItemArray[4].ToString(),(ItemsExpireDGV.Items[i] as DataRowView).Row.ItemArray[5].ToString()));
                    }
                }
                else
                {
                    ItemExpireDate[Code] = new List<Tuple<string, string>>();
                    for (int i = 0; i < ItemsExpireDGV.Items.Count; i++)
                    {
                        ItemExpireDate[Code].Add(new Tuple<string, string>((ItemsExpireDGV.Items[i] as DataRowView).Row.ItemArray[4].ToString(), (ItemsExpireDGV.Items[i] as DataRowView).Row.ItemArray[5].ToString()));
                    }
                }
                MessageBox.Show("Items Saved sussesfully");
                this.Close();
            }
            else if (ExpireForm== "Requests")
            {
                if (!DoSomeChecksOFSaving())
                    return;

                if (ItemExpireDate.ContainsKey(Code))
                {
                    ItemExpireDate.Remove(Code);
                    ItemExpireDate[Code] = new List<Tuple<string, string>>();
                    for (int i = 0; i < ItemsExpireTransferDGV.Items.Count; i++)
                    {
                        if ((ItemsExpireTransferDGV.Items[i] as DataRowView).Row.ItemArray[0].ToString() == "True")
                        {
                            ItemExpireDate[Code].Add(new Tuple<string, string>((ItemsExpireTransferDGV.Items[i] as DataRowView).Row.ItemArray[7].ToString(),(ItemsExpireTransferDGV.Items[i] as DataRowView).Row.ItemArray[6].ToString()));
                        }
                    }
                }
                else
                {
                    ItemExpireDate[Code] = new List<Tuple<string, string>>();
                    for (int i = 0; i < ItemsExpireTransferDGV.Items.Count; i++)
                    {
                        if ((ItemsExpireTransferDGV.Items[i] as DataRowView).Row.ItemArray[0].ToString() == "True")
                        {
                            ItemExpireDate[Code].Add(new Tuple<string, string>((ItemsExpireTransferDGV.Items[i] as DataRowView).Row.ItemArray[7].ToString(),(ItemsExpireTransferDGV.Items[i] as DataRowView).Row.ItemArray[6].ToString()));

                        }
                    }
                }
                MessageBox.Show("Items Saved sussesfully");
                this.Close();
            }
        }
        private bool DoSomeChecksOFDate(RoutedEventArgs e)
        {
            if ((e.OriginalSource as Button).Name.ToString() == "ExpireDone")
            {
                if (ExpireDatePkr.Text.Equals(""))
                {
                    MessageBox.Show("Expire Date Can't be Empty");
                    return false;
                }
            }
            else if ((e.OriginalSource as Button).Name.ToString() == "ProductionDone")
            {
                if (ProductionDatepkr.Text.Equals(""))
                {
                    MessageBox.Show("Production Date Can't be Empty");
                    return false;
                }

                if (Priodictxt.Text.Equals(""))
                {
                    MessageBox.Show("Periodic can't be Empty");
                    return false;
                }

                if (PreiodicCbx.Text.Equals(""))
                {
                    MessageBox.Show("You Should Choose The Perriodic Value");
                    return false;
                }
            }
            return true;
        }
    }
}