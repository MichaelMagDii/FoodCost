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
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for OrderRequesation.xaml
    /// </summary>
    public partial class OrderRequesation : UserControl
    {
        string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
        DataTable Dataa = new DataTable();
        string valOfResturant = "";string ValOfkitchen="";

        public OrderRequesation(string ValResturant ,string ValKitchen)
        {
            InitializeComponent();
            Dataa.Columns.Add("Order",typeof(bool));
            Dataa.Columns.Add("Code");
            Dataa.Columns.Add("Name");
            Dataa.Columns.Add("Qty");
            Dataa.Columns.Add("maximum Qty");
            LoadAllMinQty(ValResturant,ValKitchen);
            valOfResturant = ValResturant;
            ValOfkitchen = ValKitchen;
        }
        
        private void ItemsDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
                SqlConnection con = new SqlConnection(connString);
                DataTable Dat = new DataTable();
                Dat = (ItemsDGV.DataContext as DataTable);
                if (e.Column.Header.ToString() == "Qty")
                {
                    if (double.Parse((e.EditingElement as TextBox).Text) > (Convert.ToDouble((ItemsDGV.Items[e.Row.GetIndex()] as DataRowView).Row.ItemArray[4])))
                    {
                        MessageBox.Show("The Max Qty Is =" + (Convert.ToDouble((ItemsDGV.Items[e.Row.GetIndex()] as DataRowView).Row.ItemArray[4])));
                        (e.EditingElement as TextBox).Text = (ItemsDGV.Items[e.Row.GetIndex()] as DataRowView).Row.ItemArray[4].ToString();
                    }
                }
            }
            catch{ }
        }

        public void LoadAllMinQty(string ValOfResturant, string ValOfKitchen)
        {
            string NameOfItems = "";
            SqlConnection con2 = new SqlConnection(connString);
            SqlCommand cmd2 = new SqlCommand();
            SqlConnection con = new SqlConnection(connString);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "SELECT ItemID,Qty,MinNumber,MaxNumber FROM Items Where RestaurantID=" + ValOfResturant+ " AND KitchenID="+ ValOfKitchen+ "AND Qty < MinNumber";
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        con2.Open();
                        string W = "SELECT Name FROM Setup_Items Where Code=" + reader["ItemID"].ToString();
                        cmd2 = new SqlCommand(W, con2);
                        if(cmd2.ExecuteScalar() != null)
                            NameOfItems = cmd2.ExecuteScalar().ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        con2.Close();
                    }
                   Dataa.Rows.Add(false,reader["ItemID"].ToString(), NameOfItems, "", reader["MaxNumber"].ToString());
                }
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

            for(int i=0;i<Dataa.Columns.Count;i++)
            {
                Dataa.Columns[i].ReadOnly = true;
            }
            Dataa.Columns["Qty"].ReadOnly = false;
            Dataa.Columns["Order"].ReadOnly = false;
            ItemsDGV.DataContext = Dataa;
        }
        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            int CountOfItems = 0;
            string ValOfOrderID = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString);
            SqlCommand _CMD= new SqlCommand();
            SqlDataReader reader = null;
            DataTable dt = ItemsDGV.DataContext as DataTable;
            try
            {
                con.Open();
                string s = "Select top(1) Order_ID From OrderRequesion_tbl ORDER BY Order_ID Desc";
                _CMD = new SqlCommand(s, con);
                if (_CMD.ExecuteScalar() == null)
                {
                    ValOfOrderID = "1";
                }
                else
                {
                    ValOfOrderID = _CMD.ExecuteScalar().ToString();
                }
                con.Close();
            }
            catch { }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i]["Order"].ToString() == "False")
                {
                    CountOfItems++;
                }
            }
            if (CountOfItems != dt.Rows.Count)
            {
                try
                {
                    con.Open();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Order"].ToString() == "True" && dt.Rows[i]["Qty"].ToString() !="")
                        {
                            string W = string.Format("insert into OrderRequesion_Items(Order_ID,Item_ID,ItemQty) values ({0},{1},{2})", ValOfOrderID, dt.Rows[i]["Code"], dt.Rows[i]["Qty"]);
                            _CMD = new SqlCommand(W, con);
                            _CMD.ExecuteNonQuery();
                        }
                    }

                    string s = string.Format("insert into OrderRequesion_tbl(Order_ID,Order_Date,Vendor_ID,Resturant_ID,Kitchen_ID) values ({0},GETDATE(),'{3}',{1},{2})", ValOfOrderID, valOfResturant, ValOfkitchen, " ");
                    _CMD = new SqlCommand(s, con);
                    _CMD.ExecuteNonQuery();

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
          
    }
}