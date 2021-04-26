using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for OutletRecipeSubtraction.xaml
    /// </summary>
    public partial class OutletRecipeSubtraction : UserControl
    {
        string ValOfResturant = "";
        string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
        public OutletRecipeSubtraction()
        {
            InitializeComponent();
            LoadAllResturant();
        }

        public void LoadAllResturant()
        {
            SqlConnection con = new SqlConnection(connString);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "select Name from Setup_Restaurant";
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var data = reader["Name"].ToString();
                    StoreIDcbx.Items.Add(data);
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
        }   // Done
        
        private void ResturantComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kitchencbx.Items.Clear();
            SqlConnection con = new SqlConnection(connString);
            try
            {
                con.Open();
                string s = "select Code from Setup_Restaurant Where Name='" + StoreIDcbx.SelectedItem + "'";
                SqlCommand cmd = new SqlCommand(s, con);
                ValOfResturant = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            LoadAllKitchen(ValOfResturant);
        }  //Done

        public void LoadAllKitchen(string CodeOfResturant)
        {
            SqlConnection con = new SqlConnection(connString);
            SqlDataReader reader = null;
            try
            {
                con.Open();
                string s = "select Name from Setup_Kitchens Where RestaurantID=" + CodeOfResturant;
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var data = reader["Name"].ToString();
                    Kitchencbx.Items.Add(data);
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
        }  //Done
        private bool DoChecks()
        {
            bool check=true;
            if(StoreIDcbx.Text == "")
            {
                check = false;
                MessageBox.Show("Store Can Not be Empty !");
                return check;
            }
            if (Kitchencbx.Text == "")
            {
                check = false;
                MessageBox.Show("Kitchen Can Not be Empty !");
                return check;
            }
            if (From.Text == "")
            {
                check = false;
                MessageBox.Show("Form Date Can Not be Empty !");
                return check;
            }
            if (To.Text == "")
            {
                check = false;
                MessageBox.Show("To Date Can Not be Empty !");
                return check;
            }
            return check;
        }

        private void SubtractionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DoChecks() == false)
                return;

            string text = File.ReadAllText("PosCon.txt");
            SqlConnection con = new SqlConnection(text);
            con.Open();

            SqlConnection con2 = new SqlConnection(text);
            con2.Open();

            SqlConnection con3 = new SqlConnection(connString);
            con3.Open();

            string s = string.Format("select Code from Setup_Kitchens where Name = '{0}'", Kitchencbx.Text);
            SqlCommand cmd = new SqlCommand(s, con3);
            string kit = cmd.ExecuteScalar().ToString();

            s = string.Format(" select Code from Setup_Restaurant where Name = '{0}'", StoreIDcbx.Text);
            cmd = new SqlCommand(s, con3);
            string rest = cmd.ExecuteScalar().ToString();

            s = string.Format("select ID,Rest_ID_Active,OutLet_ID from Checks where MyStatus in ('Close','Voided')" +
                "and Rest_ID_Active = {0} and OutLet_ID = {1}",rest,kit);
            cmd = new SqlCommand(s, con);
            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                s = string.Format("select Item_ID,SUM(QTY) as Qty,(select CrossCode from Items where Items.ID = Item_ID) as CrossCode" +
                                    " from ChecksItems where Check_ID in ({0}) group by Item_ID", reader["ID"]);
                SqlCommand cmd2 = new SqlCommand(s, con2);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();

                s = string.Format("Update RecipeQty set Qty = Qty - {0} where Recipe_ID in (select Code from Setup_Recipes where CrossCode in ({1}))",reader2["Qty"],reader2["CrossCode"]);
                cmd2 = new SqlCommand(s, con3);
                cmd2.ExecuteNonQuery();
                reader2.Close();

            }
        }
    }
}
