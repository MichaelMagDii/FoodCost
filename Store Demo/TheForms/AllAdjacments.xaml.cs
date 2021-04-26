using System;
using System.Collections.Generic;
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

namespace Food_Cost.TheForms
{
    /// <summary>
    /// Interaction logic for AllAdjacments.xaml
    /// </summary>
    public partial class AllAdjacments : Window
    {
        AdjacmentInventory adjacemnts;
        public AllAdjacments(AdjacmentInventory _adjacments)
        {
            InitializeComponent();
            adjacemnts = _adjacments;
            SqlConnection con = new SqlConnection(Classes.DataConnString);

            try
            {
                con.Open();
                string s = string.Format("select Adjacment_ID AS ID, Adjacment_Num as Number,(Select Name From Setup_Restaurant Where Code=Resturant_ID) AS Resturant, (Select Name From Setup_Kitchens Where Code=KitchenID AND Setup_Kitchens.RestaurantID=Resturant_ID) AS Kitchen from Adjacment_tbl where Post is NULL and Resturant_ID='{0}' and KitchenID='{1}'", adjacemnts.ValOfResturant,adjacemnts.ValOfKitchen);
                SqlDataAdapter adapter = new SqlDataAdapter(s, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                PO_DGV.DataContext = dt;
            }
            catch { con.Close(); }
            con.Close();
        }

        private void PO_DGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    SqlConnection con = new SqlConnection(Classes.DataConnString); SqlConnection con2 = new SqlConnection(Classes.DataConnString);
                    try
                    {
                        DataTable dt = PO_DGV.DataContext as DataTable;

                        con.Open();
                        SqlCommand cmd = new SqlCommand(string.Format("select Adjacment_ID as ID,Adjacment_Num as Number ,(select Name from Setup_AdjacmentReasons_tbl where Setup_AdjacmentReasons_tbl.Code = Adjacment_tbl.Adjacment_Reason) as Reason, Adjacment_Date as Date,Comment FROM Adjacment_tbl where Post is NULL and Adjacment_ID='{0}'", dt.Rows[PO_DGV.SelectedIndex]["ID"]), con);
                        SqlDataReader reader = cmd.ExecuteReader();

                        reader.Read();
                        string PO_id = reader["ID"].ToString();
                        adjacemnts.Serial_Adjacment_NO.Text = reader["ID"].ToString();
                        adjacemnts.Adjacment_NO.Text = reader["Number"].ToString();
                        adjacemnts.Reasoncbx.Text = reader["Reason"].ToString();
                        DateTime TheDatetime = Convert.ToDateTime(reader["Date"].ToString());
                        adjacemnts.Adjacment_Date.Text = TheDatetime.ToString("yyyy-MM-dd");
                        adjacemnts.commenttxt.Text = reader["Comment"].ToString();
                        reader.Close();

                        SqlDataAdapter adapter = new SqlDataAdapter(string.Format("(select Item_ID as Code,(select Name from Setup_Items where Code= Item_ID) as Name,(select Name2 from Setup_Items where Code = Item_ID) as Name2,Qty,AdjacmentableQty as 'Adjacmentable Qty',Variance,Cost,(select Unit from Setup_Items where Code= Item_ID) as Unit,(AdjacmentableQty * Cost) as 'Total Cost',Recipe from Adjacment_Items where Adjacment_ID = '{0}' AND Recipe=0) union (select Item_ID as Code,(select Name from Setup_Recipes where Code= Item_ID) as Name,(select Name2 from Setup_Recipes where Code = Item_ID) as Name2,Qty,AdjacmentableQty as 'Adjacmentable Qty',Variance,Cost,(select Unit from Setup_Recipes where Code= Item_ID) as Unit,(AdjacmentableQty * Cost) as 'Total Cost',Recipe from Adjacment_Items where Adjacment_ID = '{0}' AND Recipe=1)", PO_id), con);
                        dt = new DataTable();

                        adapter.Fill(dt);
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            dt.Columns[i].ReadOnly = true;
                        }
                        dt.Columns["Adjacmentable Qty"].ReadOnly = false;
                        dt.Columns["Cost"].ReadOnly = false;
                        adjacemnts.ItemsDGV.DataContext = dt;
                    }
                    catch (Exception ex) { con.Close(); MessageBox.Show(ex.ToString()); }

                    adjacemnts.Adjact.IsEnabled = true;
                    adjacemnts.Adjact.Visibility = Visibility.Visible;
                    adjacemnts.searchBtn.Visibility = Visibility.Visible;
                    adjacemnts.Serial_Adjacment_NO.IsEnabled = false;
                    adjacemnts.Adjacment_NO.IsEnabled = false;
                    adjacemnts.Reasoncbx.IsEnabled = false;
                    adjacemnts.Adjacment_Date.IsEnabled = false;
                    con.Close();
                    this.Close();
                }
            }
        }
    }
}
