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
    /// Interaction logic for ParentItemInRecipe.xaml
    /// </summary>
    public partial class ParentItemInRecipe : Window
    {
        GenerateBatch generateBatch;
        string valofRecipe = "";
        string valofResturant = "";
        string valofKitchen = "";
        string ItemQty = "";
        string RecipeUnit = "";
        string RecipeQty = "";
        string RecipeUnitDefination = "";
        string RecipeQeneration = "";

         DataTable Dataa = new DataTable();
        public ParentItemInRecipe(string RecipeItem,string ItemQtyy,string Unitt,string RecipeQtyy,string Recipeunit,string RecipeGenerationQty, string valofResturantt, string valofKitchenn,GenerateBatch generate)
        {
            InitializeComponent();
            generateBatch = generate;
            Dataa.Columns.Add("Select", typeof(bool));
            Dataa.Columns.Add("Code");
            Dataa.Columns.Add("Name");
            Dataa.Columns.Add("Qty");
            Dataa.Columns.Add("Unit");
            Dataa.Columns.Add("SelectedQty");
            valofResturant = valofResturantt;
            valofKitchen = valofKitchenn;
            ItemQty = ItemQtyy;
            RecipeUnit = Unitt;
            RecipeQty = RecipeQtyy;
            RecipeUnitDefination = Recipeunit;
            RecipeQeneration = RecipeGenerationQty;
            ItemQtytxt.Text = (float.Parse(ItemQtyy) * float.Parse(RecipeGenerationQty)).ToString();
            ItemUnittxt.Text = Unitt;
            LoadAllItems(RecipeItem);
        }

        private void LoadAllItems(string Item)
        {
            string valofQtyItem = "";
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader2 = null;
            try
            {
                con.Open();
                string s = "SELECT Code,Name From Setup_ParentItems Where Parent_Item='" + Item + "'";
                SqlCommand cmd = new SqlCommand(s, con);
                reader = cmd.ExecuteReader();
                con2.Open();
                while (reader.Read())
                {
                    try
                    {
                        string W = string.Format("select Qty From Items where RestaurantID={0} and KitchenID={1} and ItemID={2} ", valofResturant, valofKitchen, reader["Code"]);
                        SqlCommand cmd2 = new SqlCommand(W, con2);
                        valofQtyItem = cmd2.ExecuteScalar().ToString();
                    }
                    catch(Exception ex) { MessageBox.Show(ex.ToString()); }
                    try
                    {
                        string W = string.Format("select Weight,Unit,ConvUnit2 From Setup_Items where Code={0} ", reader["Code"]);
                        SqlCommand cmd2 = new SqlCommand(W, con2);
                        reader2 = cmd2.ExecuteReader();
                        reader2.Read();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    float val = float.Parse(valofQtyItem) * float.Parse(reader2["Weight"].ToString());
                    Dataa.Columns["Select"].ReadOnly = false;
                    Dataa.Columns["Code"].ReadOnly = true;
                    Dataa.Columns["Name"].ReadOnly = true;
                    Dataa.Columns["Qty"].ReadOnly = true; 
                    Dataa.Columns["Unit"].ReadOnly = true; 
                    Dataa.Columns["SelectedQty"].ReadOnly = false; 
                    Dataa.Rows.Add(false, reader["Code"], reader["Name"],val, reader2["Unit"]," ");
                    reader2.Close();
                }
                ParentItemsDGV.DataContext = Dataa;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con2.Close();
                con.Close();
            }
        }
        private void ParentItemsDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header != "Select")
            {
                try
                {
                    DataTable Dat = new DataTable();
                    Dat = (ParentItemsDGV.DataContext as DataTable);
                    double Qty = Convert.ToDouble((e.Row.Item as DataRowView).Row["Qty"].ToString());
            
                    if (((e.Row.Item as DataRowView).Row["Select"]).ToString() == "True")
                    {
                        if (Convert.ToDouble((e.EditingElement as TextBox).Text) > Convert.ToDouble((e.Row.Item as DataRowView).Row["Qty"]))
                                MessageBox.Show("Enter the Correct Valuo");

                    }                           
                }
                catch { }
            }
            else
            {
                ItemUnittxt.Focus();
            }
            double val = 0;
            for (int i=0;i<ParentItemsDGV.Items.Count;i++)
            {
                if(((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[0].ToString() == "True")
                {
                    if(e.Row.GetIndex() == i)
                    {
                        try
                        {
                            val += Convert.ToDouble((e.EditingElement as TextBox).Text);
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            val += Convert.ToDouble(((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[5]);
                        }
                        catch { }
                    }
                }
            }
            SelectedQty.Text = val.ToString();
            if(SelectedQty.Text == ItemQtytxt.Text)
            {
                GetItems.IsEnabled = true;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            string BaseWeight = ""; string BaseUnit = ""; string secondWeight = ""; string SecondUnit = ""; string YeildofQty = "";
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = null;
            DataTable Dat = new DataTable();
            Dat = (ParentItemsDGV.DataContext as DataTable);
            for (int i = 0; i < ParentItemsDGV.Items.Count; i++)
            {
                if(((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[0].ToString() == "True")
                {
                    try
                    {
                        con.Open();
                        string W = "SELECT Weight,Unit,Unit2,ConvUnit2,Yield FROM Setup_Items WHERE Code=" + ((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[1];
                        cmd = new SqlCommand(W, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            YeildofQty = reader["Yield"].ToString();
                            BaseWeight = reader["Weight"].ToString();
                            BaseUnit = reader["Unit"].ToString();
                            SecondUnit = reader["Unit2"].ToString();
                            secondWeight = reader["ConvUnit2"].ToString();
                        }
                        reader.Close();
                        string w = "SELECT Qty,Current_Cost FROM Items WHERE RestaurantID=" + valofResturant + " AND ItemID=" + ((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[1] + " AND KitchenID=" + valofKitchen;
                        cmd = new SqlCommand(w, con);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (RecipeUnit == BaseUnit)
                            {
                                generateBatch.RecipeItemDData[generateBatch.CountofRecipeItemData, 0] = ((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[1].ToString();
                                generateBatch.RecipeItemDData[generateBatch.CountofRecipeItemData, 1] = ((Convert.ToDouble((((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[5]))/Convert.ToDouble(BaseWeight))* Convert.ToDouble(RecipeQeneration)).ToString();
                                generateBatch.RecipeItemDData[generateBatch.CountofRecipeItemData, 2] = (Convert.ToDouble(RecipeQeneration) * (Convert.ToDouble(((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[5]) * Convert.ToDouble(reader["Current_Cost"])) + ((100-Convert.ToDouble(YeildofQty)) / 100) * (Convert.ToDouble(reader["Current_Cost"]))).ToString();
                                generateBatch.RecipeItemDData[generateBatch.CountofRecipeItemData, 3] = ((Convert.ToDouble((((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[3])) - Convert.ToDouble((((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[5])))*Convert.ToDouble(BaseWeight)).ToString();
                                generateBatch.CountofRecipeItemData++;
                            }
                            else if (RecipeUnit == SecondUnit)
                            {
                                generateBatch.RecipeItemDData[generateBatch.CountofRecipeItemData, 0] = ((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[1].ToString();
                                generateBatch.RecipeItemDData[generateBatch.CountofRecipeItemData, 1] = (Convert.ToDouble((((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[5])) / Convert.ToDouble(BaseWeight)).ToString();
                                generateBatch.RecipeItemDData[generateBatch.CountofRecipeItemData, 2] = (Convert.ToDouble(RecipeQeneration) * (Convert.ToDouble(((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[5]) * Convert.ToDouble(secondWeight) * Convert.ToDouble(reader["Current_Cost"])) + (Convert.ToDouble(secondWeight)) * ((100-Convert.ToDouble(YeildofQty)) / 100) * (Convert.ToDouble(reader["Current_Cost"]))).ToString();
                                generateBatch.RecipeItemDData[generateBatch.CountofRecipeItemData, 3] = ((Convert.ToDouble((((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[3])) - Convert.ToDouble((((DataRowView)ParentItemsDGV.Items[i]).Row.ItemArray[5])))*Convert.ToDouble(secondWeight)).ToString();
                                generateBatch.CountofRecipeItemData++;
                            }
                        }
                        reader.Close();
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
            if(ItemQtytxt.Text != SelectedQty.Text)
            {
                generateBatch.checksofParent = false;
            }
            generateBatch.ToFinishFunction = false;
            //generateBatch.ToCloseFunction = false;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
           // generateBatch.ToFinishFunction = false;
        }

      
    }
}
