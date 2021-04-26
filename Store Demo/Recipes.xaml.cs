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
using System.Text.RegularExpressions;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for Recipes.xaml
    /// </summary>
    public partial class Recipes : UserControl
    {
        DataTable RecipeItems = new DataTable();
        bool ValtoRemoveRecipe = false;
        int CountOfItems = 0;
        int codeTodelete = 0;
        string ValofStore = "";
        string ValofKitchen = "";
        List<string> Authenticated = new List<string>();

        public Recipes()
        {
            if (MainWindow.AuthenticationData.ContainsKey("Recipes"))
            {
                Authenticated = MainWindow.AuthenticationData["Recipes"];
                if (Authenticated.Count == 0)
                {
                    MessageBox.Show("You Havent a Privilage to Open this Page");
                    LogIn logIn = new LogIn();
                    logIn.ShowDialog();
                }
                else
                {
                    InitializeComponent();
                    MainUiFormat();
                    FillRecpieDGV();
                }
            }         
        }

        //public void LoadUnits()
        //{
        //    SqlConnection con = new SqlConnection(Classes.DataConnString);

        //    try
        //    {
        //        string s = "select Name from Units";
        //        DataTable dt = new DataTable();
        //        using (SqlDataAdapter da = new SqlDataAdapter(s, con))
        //            da.Fill(dt);

        //        Unitstxt.Items.Clear();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            Unitstxt.Items.Add(dt.Rows[i]["Name"]);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        public void FillRecpieDGV()
        {
            DataTable AllRecipes = new DataTable();
            AllRecipes = Classes.RetrieveData("Code,Name,Name2,(select Name from Setup_RecipeCategory where Code=Category_ID)", "Setup_Recipes");
            AllRecipesDGV.DataContext = AllRecipes;
        }

        //Event 
        public void MainUiFormat()
        {
            codetxt.IsEnabled = false;
            CrossCodetxt.IsEnabled = false;
            Nametxt.IsEnabled = false;
            Name2txt.IsEnabled = false;
            Categtxt.IsEnabled = false;
            SUBCategtxt.IsEnabled = false;
            Categorytxt.IsEnabled = false;
            SUBCategorytxt.IsEnabled = false;
            CategoryBtn.IsEnabled = false;
            SubCategoryBtn.IsEnabled = false;
            //Yiledtxt.IsEnabled = false;
            Unittxt.IsEnabled = false;
            Unitstxt.IsEnabled = false;
            ActiveChbx.IsEnabled = false;
            RemoveBtn.IsEnabled = false;
            AddRecipeBtn.IsEnabled = false;
            AddItemBtn.IsEnabled = false;
            SearchBtn.IsEnabled = false;
            RenewBtn.IsEnabled = false;
            SaveBtn.IsEnabled = false;
            EditBtn.IsEnabled = false;
            UndoBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            NewBtn.IsEnabled = true;
            UnitsBtn.IsEnabled = false;
            Categtxt.IsReadOnly = true;
            Categorytxt.IsReadOnly = true;
            SUBCategtxt.IsReadOnly = true;
            SUBCategorytxt.IsReadOnly = true;
        }
        public void EmptyTexts()
        {
            codetxt.Text = "";
            CrossCodetxt.Text = "";
            Nametxt.Text = "";
            Name2txt.Text = "";
            Categtxt.Text = "";
            SUBCategtxt.Text = "";
            Categorytxt.Text = "";
            SUBCategorytxt.Text = "";
            //Yiledtxt.Text = "";
            Unittxt.Text = "";
            Unitstxt.Text = "";
            ActiveChbx.IsEnabled = false;
            AllRecipesDGV.DataContext = null;
            RecipesDGV.DataContext = null;
            FillRecpieDGV();
        }
        public void EnableUiFormat()
        {
            codetxt.IsEnabled = true;
            CrossCodetxt.IsEnabled = true;
            Nametxt.IsEnabled = true;
            Name2txt.IsEnabled = true;
            Categtxt.IsEnabled = true;
            SUBCategtxt.IsEnabled = true;
            Categorytxt.IsEnabled = true;
            SUBCategorytxt.IsEnabled = true;
            CategoryBtn.IsEnabled = true;
            SubCategoryBtn.IsEnabled = true;
            //Yiledtxt.IsEnabled = true;
            Unittxt.IsEnabled = true;
            Unitstxt.IsEnabled = true;
            ActiveChbx.IsEnabled = true;
            RemoveBtn.IsEnabled = true;
            AddRecipeBtn.IsEnabled = true;
            AddItemBtn.IsEnabled = true;
            SearchBtn.IsEnabled = true;
            RenewBtn.IsEnabled = true;
            SaveBtn.IsEnabled = true;
            EditBtn.IsEnabled = true;
            UndoBtn.IsEnabled = true;
            DeleteBtn.IsEnabled = true;
            NewBtn.IsEnabled = false;
            UnitsBtn.IsEnabled = true;
            Categtxt.IsReadOnly = true;
            Categorytxt.IsReadOnly = true;
            SUBCategtxt.IsReadOnly = true;
            SUBCategorytxt.IsReadOnly = true;
        }
        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            //New Btn That can create a New Recipe
            codetxt.Text = "";
            CrossCodetxt.Text = "";
            Nametxt.Text = "";
            Name2txt.Text = "";
            ActiveChbx.IsChecked = false;
            Categtxt.Text = "";
            Categorytxt.Text = "";
            SUBCategtxt.Text = "";
            SUBCategorytxt.Text = "";
            //Yiledtxt.Text = "";
            Unittxt.Text = "";
            Unitstxt.Text = "";
            ActiveChbx.IsChecked = true;
            EnableUiFormat();
            SearchBtn.IsEnabled = false;
            RenewBtn.IsEnabled = false;
            EditBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            RecipeItems = RecipesDGV.DataContext as DataTable;
            if (Authenticated.IndexOf("SaveRecipes") == -1 && Authenticated.IndexOf("CheckAllRecipes") == -1)
            {   LogIn logIn = new LogIn();  logIn.ShowDialog();  }
            else
            {
                if (codetxt.Text == "")
                {   MessageBox.Show("Code Field Can't Be Empty");  return;   }
                if (Nametxt.Text == "")
                {   MessageBox.Show("Name Field Can't Be Empty");  return;   }
                if (Categtxt.Text == "")
                {   MessageBox.Show("Should Enter the Category First");  return;   }
                if (SUBCategtxt.Text == "")
                {   MessageBox.Show("Should Enter the Sub Category First");   return;   }
                if (Unittxt.Text == "")
                {   MessageBox.Show("Should Enter the Unites First");  return;  }
                if (Unitstxt.Text == "")
                {   MessageBox.Show("Should Select the Unites First");   return;   }

                for(int i=0;i<AllRecipesDGV.Items.Count;i++)
                {
                    if( ((DataRowView)AllRecipesDGV.Items[i]).Row.ItemArray[0].ToString().Equals(codetxt.Text))
                    {   MessageBox.Show("This Code Is Not Avaliable");  return;    }
                }
                
                try
                {
                    string FiledSelection = "Code,CrossCode,Name,Name2,Category_ID,SubCategory_ID,IsActive,Unit,UnitQty,CreateDate,UserID,WS";
                    string Values = string.Format("'{0}','{1}',N'{2}',N'{3}','{4}','{5}','{6}','{7}','{8}',GETDATE(),'{9}','{10}'", codetxt.Text, CrossCodetxt.Text,Nametxt.Text,Name2txt.Text,Categtxt.Text, SUBCategtxt.Text, ActiveChbx.IsChecked, Unitstxt.Text,Unittxt.Text, MainWindow.UserID, Classes.WS);
                    Classes.InsertRow("Setup_Recipes", FiledSelection, Values);
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());    }
                

                try
                {
                    for (int i = 0; i < RecipesDGV.Items.Count; i++)
                    {
                        string FiledSelection = "Item_Code,Recipe_ID,Qty,Recipe_Unit,Cost,Total_Cost,Cost_Precentage,Recipe_Code";
                        string Values = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'", RecipeItems.Rows[i]["Item_Code"], RecipeItems.Rows[i]["Recipe_ID"], RecipeItems.Rows[i]["Qty"], RecipeItems.Rows[i]["Recipe_Unit"], RecipeItems.Rows[i]["Cost"], RecipeItems.Rows[i]["Total_Cost"], RecipeItems.Rows[i]["Cost_Precentage"], RecipeItems.Rows[i]["Recipe_Code"]);
                        Classes.InsertRow("Setup_RecipeItems", FiledSelection, Values);
                    }
                }
                catch (Exception ex)
                {   MessageBox.Show(ex.ToString());    }
               
                MainUiFormat();
                EmptyTexts();
                SaveBtn.IsEnabled = false;
                ActiveChbx.IsChecked = false;
                MessageBox.Show("Saved Successfully");
            }

        }
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {  MainUiFormat();   EmptyTexts();  }
        private void RowClicked(object sender, MouseButtonEventArgs e)
        {
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd = new SqlCommand();
            DataTable RecipeInfo = new DataTable();
            string category = "", SubCategory = "";
            if (sender != null)
            {
                DataGrid data = sender as DataGrid;
                if(data != null && data.SelectedItem != null && data.SelectedItems.Count == 1)
                {
                    try
                    {
                        string WhereFiltering  ="Code="+ ((DataRowView)AllRecipesDGV.SelectedItems[0]).Row.ItemArray[0].ToString();
                        RecipeInfo = Classes.RetrieveData("*",WhereFiltering, "Setup_Recipes");
                        try
                        {
                            con.Open();
                            string s = "SELECT Name From Setup_RecipeCategory Where Code =" + RecipeInfo.Rows[0]["Category_ID"];
                            cmd = new SqlCommand(s, con);
                            category = (cmd.ExecuteScalar()).ToString();

                            s = "SELECT Name From Setup_RecipeSubCategories Where Code =" + RecipeInfo.Rows[0]["SubCategory_ID"];
                            cmd = new SqlCommand(s, con);
                            SubCategory = (cmd.ExecuteScalar()).ToString();
                            con.Close();
                        }
                        catch(Exception ex) { MessageBox.Show(ex.ToString()); }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    codetxt.Text = RecipeInfo.Rows[0]["Code"].ToString();
                    Nametxt.Text =RecipeInfo.Rows[0]["Name"].ToString();
                    Name2txt.Text =RecipeInfo.Rows[0]["Name2"].ToString();
                    Categorytxt.Text = category;
                    SUBCategorytxt.Text = SubCategory;
                    Categtxt.Text =RecipeInfo.Rows[0]["Category_ID"].ToString();
                    SUBCategtxt.Text =RecipeInfo.Rows[0]["SubCategory_ID"].ToString();
                    ActiveChbx.IsChecked = RecipeInfo.Rows[0]["IsActive"].ToString().Equals("True");
                    Unittxt.Text = RecipeInfo.Rows[0]["UnitQty"].ToString();
                    Unitstxt.Text =RecipeInfo.Rows[0]["Unit"].ToString();
                }
            }
            LoadRecipeItemsToGrid(codetxt.Text);
        }
        public void LoadRecipeItemsToGrid(string Code)
        {
            //
            DataTable RecipeItemsInfo = new DataTable();
            DataTable TheCost = new DataTable();
            //
            double summationofQTyValuo = 0;
            double totalCost = 0;
            double CostVlue = 0;
            string CodeOfRecipe = "";
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd = new SqlCommand();
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd2 = new SqlCommand();
            SqlDataReader reader = null;
            CountOfItems = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("Item_Code");
            dt.Columns.Add("Recipe_Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("Name2");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Recipe_Unit");
            dt.Columns.Add("Cost");
            dt.Columns.Add("Total_Cost");
            dt.Columns.Add("Cost_Precentage");
            try
            {
                string WhereFiltering = "Recipe_Code=" + Code;
                RecipeItemsInfo = Classes.RetrieveData("Recipe_ID,Item_Code,Qty,Recipe_Unit", WhereFiltering, "Setup_Recipes");
                for (int i = 0; i < RecipeItemsInfo.Rows.Count; i++)
                {
                    if (RecipeItemsInfo.Rows[i]["Item_Code"].ToString() != "")
                    {
                        WhereFiltering = string.Format("ItemID={0} and RestaurantID=(select Code From Setup_Restaurant where IsMain='True') and KitchenID=(select Code From Setup_Kitchens where IsMain='True' and RestaurantID=(select Code From Setup_Restaurant where IsMain='True'))", RecipeItemsInfo.Rows[i]["Item_Code"].ToString());
                        RecipeItemsInfo = Classes.RetrieveData("Current_Cost", WhereFiltering, "Setup_Recipes");
                        if (TheCost.Rows.Count != 0)
                        {
                            CostVlue = Convert.ToDouble(TheCost.Rows[0][0].ToString());
                        }
                        else { CostVlue = 0; }
                        summationofQTyValuo += CostVlue * Convert.ToDouble(RecipeItemsInfo.Rows[i]["Qty"].ToString());
                    }
                    else
                    {
                        WhereFiltering = string.Format("Recipe_ID={0} and Resturant_ID=(select Code From Setup_Restaurant where IsMain='True') and Kitchen_ID=(select Code From Setup_Kitchens where IsMain='True' and RestaurantID=(select Code From Setup_Restaurant where IsMain='True'))", RecipeItemsInfo.Rows[i]["Recipe_ID"].ToString());
                        RecipeItemsInfo = Classes.RetrieveData("Price", WhereFiltering, "RecipeQty");
                        if (TheCost.Rows.Count != 0)
                        {
                            CostVlue = Convert.ToDouble(TheCost.Rows[0][0].ToString());
                        }
                        else { CostVlue = 0; }
                        summationofQTyValuo += CostVlue * Convert.ToDouble(RecipeItemsInfo.Rows[i]["Qty"].ToString());
                    }
                    //ana wa2f hna :'D
                }
            }
            catch { }
            try
            {
                con.Open();
                string q = "SELECT Recipe_ID,Item_Code,Name,Name2,Qty,Recipe_Unit,Cost,Total_Cost,Cost_Precentage FROM Setup_RecipeItems WHERE Recipe_Code=" + Code;
                cmd = new SqlCommand(q, con);
                reader = cmd.ExecuteReader();
                con2.Open();
                while (reader.Read())
                {
                    if (reader["Item_Code"].ToString() != "")
                    {
                        try
                        {
                            string W = string.Format("select Current_Cost from Items Where ItemID={0} and RestaurantID={1} and KitchenID={2} ", reader["Item_Code"], "1", "1");
                            cmd2 = new SqlCommand(W, con2);
                            if (cmd2.ExecuteScalar() == null)
                            {
                                CostVlue = 0;
                            }
                            else
                            {
                                CostVlue = Convert.ToDouble(cmd2.ExecuteScalar().ToString());
                            }
                        }
                        catch { }
                        summationofQTyValuo += CostVlue * Convert.ToDouble(reader["Qty"].ToString());
                    }
                    else
                    {
                        string W = string.Format("select Price from RecipeQty Where Recipe_ID={0} and Resturant_ID={1} and Kitchen_ID={2}", reader["Recipe_ID"], "1", "1");
                        cmd2 = new SqlCommand(W, con2);
                        if (cmd2.ExecuteScalar() == null)
                        {
                            CostVlue = 0;
                        }
                        else
                        {
                            CostVlue = Convert.ToDouble(cmd2.ExecuteScalar().ToString());
                        }
                        summationofQTyValuo += CostVlue * Convert.ToDouble(reader["Qty"].ToString());

                    }
                    dt.Rows.Add(reader["Item_Code"], reader["Recipe_ID"].ToString(), reader["Name"].ToString(), reader["Name2"].ToString(), reader["Qty"].ToString(), reader["Recipe_Unit"].ToString(), CostVlue, reader["Total_Cost"].ToString(), reader["Cost_Precentage"].ToString());
                }
                double sum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Total_Cost"] = ((Convert.ToDouble(dt.Rows[i]["Qty"])) * (Convert.ToDouble(dt.Rows[i]["Cost"]))).ToString();
                    sum += Convert.ToDouble(dt.Rows[i]["Total_Cost"]);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Cost_Precentage"] = ((Convert.ToDouble(dt.Rows[i]["Total_Cost"])) / (sum) * 100).ToString() + " %";
                    totalCost += (Convert.ToDouble(dt.Rows[i]["Total_Cost"]));
                    
                }
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = true;
                }
                dt.Columns["Qty"].ReadOnly = false;
                Tottaltxt.Text = totalCost.ToString();
                RecipesDGV.DataContext = dt;
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
            EnableUiFormat();
            codetxt.IsEnabled = false;
            NewBtn.IsEnabled = false;
            SaveBtn.IsEnabled = false;
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("AddItemRecipes") == -1 && Authenticated.IndexOf("CheckAllRecipes") == -1)
            {   LogIn logIn = new LogIn();  logIn.ShowDialog();  }
            else
            {   Items items = new Items(this);   items.ShowDialog();   }
        }           //Done

        private void AddRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("AddRecipes") == -1 && Authenticated.IndexOf("CheckAllRecipes") == -1)
            {   LogIn logIn = new LogIn();  logIn.ShowDialog();   }
            else
            {   AllRecipes allRecipes = new AllRecipes(this);    allRecipes.ShowDialog();  }
        }       //Done

        private void ItemRowClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                ValtoRemoveRecipe = true;
                DeleteBtn.IsEnabled = true;
                codeTodelete = grid.SelectedIndex;
            }
            else
            {
                DeleteBtn.IsEnabled = false;
            }
        }       //Done

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("RemoveItemRecipes") == -1 && Authenticated.IndexOf("CheckAllRecipes") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                if (ValtoRemoveRecipe == true)
                {
                    DataTable dt = new DataTable();
                    dt = ((DataView)RecipesDGV.ItemsSource).ToTable();
                    dt.Rows.RemoveAt(codeTodelete);
                    RecipesDGV.DataContext = dt;
                    ValtoRemoveRecipe = false;
                }
            }
        }       //Done

        private void GetCatBtn(object sender, RoutedEventArgs e)
        {
            AllCategories allcategories = new AllCategories(this);
            allcategories.ShowDialog();
        }       //Done

        private void GetSubCat(object sender, RoutedEventArgs e)
        {
            if (Categtxt.Text == "")
            {
                MessageBox.Show("Enter the Category First");
                return;
            }
            AllSubCategories allSubCategories = new AllSubCategories(this, (Categtxt.Text).ToString());
            allSubCategories.ShowDialog();
        }       //Done

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("Edit") == -1 && Authenticated.IndexOf("CheckAllRecipes") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                if (Nametxt.Text == "")
                {
                    MessageBox.Show("Name Field Can't Be Empty");
                    return;
                }
                if (Categtxt.Text == "")
                {
                    MessageBox.Show("Should Enter the Category First");
                    return;
                }
                if (SUBCategtxt.Text == "")
                {
                    MessageBox.Show("Should Enter the Sub Category First");
                    return;
                }
                //if (Yiledtxt.Text == "")
                //{
                //    MessageBox.Show("Should Enter the Yiled Qty First");
                //    return;
                //}

                if (Unittxt.Text == "")
                {
                    MessageBox.Show("Should Enter the Unites First");
                    return;
                }
                if (Unitstxt.Text == "")
                {
                    MessageBox.Show("Should Select the Unites First");
                    return;
                }

                SqlConnection con = new SqlConnection(Classes.DataConnString);
                try
                {
                    con.Open();
                    string s = "Update Setup_Recipes SET CrossCode='" + CrossCodetxt.Text +
                                                   "',Name='" + Nametxt.Text +
                                                   "',Name2='" + Name2txt.Text +
                                                   "',Category_ID='" + Convert.ToInt32(Categtxt.Text) +
                                                   "',SubCategory_ID='" + Convert.ToInt32(SUBCategtxt.Text) +
                                                   "',IsActive='" + ActiveChbx.IsChecked +
                                                   "',Unit  ='" + Unitstxt.Text.ToString() +
                                                   "',UnitQty='" + Unittxt.Text +
                                                   "'Where Code =" + codetxt.Text;
                    SqlCommand cmd = new SqlCommand(s, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                //Delete All RecipeItems
                try
                {
                    con.Open();
                    string q1 = "Delete Setup_RecipeItems Where Recipe_Code=" + codetxt.Text;
                    SqlCommand cmd = new SqlCommand(q1, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }

                try
                {
                    con.Open();
                    for (int i = 0; i < RecipesDGV.Items.Count; i++)
                    {
                        string H = "Insert into Setup_RecipeItems (Item_Code,Recipe_ID,Name,Name2,Qty,Recipe_Unit,Cost,Total_Cost,Cost_Precentage,Recipe_Code) Values('" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[0] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[1] + "','" + (((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[2]) + "','" + (((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[3]) + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[4] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[5] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[6] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[7] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[8] + "','" + codetxt.Text.ToString() + "')";
                        SqlCommand cmd = new SqlCommand(H, con);
                        cmd.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {


                    MainUiFormat();
                    EmptyTexts();
                    ActiveChbx.IsChecked = false;
                    MessageBox.Show("Edited Successfully");
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("DeleteRecipes") == -1 && Authenticated.IndexOf("CheckAllRecipes") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                SqlConnection con = new SqlConnection(Classes.DataConnString);
                try
                {
                    con.Open();
                    string q1 = "Delete Setup_RecipeItems Where Recipe_Code=" + codetxt.Text;
                    SqlCommand cmd = new SqlCommand(q1, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }

                try
                {
                    con.Open();
                    string q = "Delete Setup_Recipes Where Code=" + codetxt.Text;
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }

                MainUiFormat();
                EmptyTexts();
            }
        }

        private void RenewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("ReNewRecipes") == -1 && Authenticated.IndexOf("CheckAllRecipes") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                int LastVal = 0;
                for (int i = 0; i < AllRecipesDGV.Items.Count; i++)
                {
                    if (Convert.ToInt64(((DgvData)AllRecipesDGV.Items[i]).Code) > LastVal)
                    {
                        LastVal = Convert.ToInt32(((DgvData)AllRecipesDGV.Items[i]).Code);
                    }
                }
                LastVal += 1;
                string VAL = LastVal.ToString();
                codetxt.Text = VAL;
                if (codetxt.Text == "")
                {
                    MessageBox.Show("Code Field Can't Be Empty");
                    return;
                }

                foreach (DgvData item in AllRecipesDGV.Items)
                {
                    if (item.Code.Equals(codetxt.Text))
                    {
                        MessageBox.Show("This Code Is Not Avaliable And Click Save Button");
                        codetxt.IsEnabled = true;
                        codetxt.IsReadOnly = false;
                        return;
                    }
                }

                string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
                SqlConnection con = new SqlConnection(connString);

                try
                {
                    con.Open();
                    string q = "insert into Setup_Recipes (Code,CrossCode,Name,Name2,Category_ID,SubCategory_ID,IsActive,Unit,UnitQty)Values(" + codetxt.Text + ",'" + CrossCodetxt.Text + "','" + Nametxt.Text + "','" + Name2txt.Text + "','" + Categtxt.Text + "','" + SUBCategtxt.Text + "','" + ActiveChbx.IsChecked + "','" + Unitstxt.Text + "','" + Unittxt.Text + "')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }

                try
                {
                    con.Open();
                    for (int i = 0; i < RecipesDGV.Items.Count; i++)
                    {
                        string H = "Insert into Setup_RecipeItems (Item_Code,Recipe_ID,Name,Name2,Qty,Recipe_Unit,Cost,Total_Cost,Cost_Precentage,Recipe_Code) Values('" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[0] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[1] + "','" + (((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[2]) + "','" + (((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[3]) + "','" + Convert.ToInt32(((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[4]) + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[5] + "','" + Convert.ToDouble(((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[6]) + "','" + Convert.ToDouble(((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[7]) + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[8] + "','" + codetxt.Text.ToString() + "')";
                        SqlCommand cmd = new SqlCommand(H, con);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                MainUiFormat();
                EmptyTexts();
                MessageBox.Show("Saved Successfully");
            }
        }

        private void RecipesDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = RecipesDGV.DataContext as DataTable;
            for (int i=0;i<dt.Columns.Count;i++)
            {
                dt.Columns[i].ReadOnly = false;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((RecipesDGV.Items[i] as DataRowView) == RecipesDGV.SelectedItem)
                {
                    dt.Rows[i]["Total_Cost"] = Convert.ToDouble((e.EditingElement as TextBox).Text) * Convert.ToDouble(dt.Rows[i]["Cost"]);
                    dt.Rows[i]["Qty"] = Convert.ToDouble((e.EditingElement as TextBox).Text);
                }
                else
                    dt.Rows[i]["Total_Cost"] = Convert.ToDouble(dt.Rows[i]["Cost"]) * Convert.ToDouble(dt.Rows[i]["Qty"]);

            }

            double sum = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dt.Rows[i]["Total_Cost"]);
            }

            double _sum = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _sum = (Convert.ToDouble(dt.Rows[i]["Total_Cost"]) / sum) * 100;
                dt.Rows[i]["Cost_Precentage"] = _sum.ToString() + " %";
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = true;
            }
            dt.Columns["Qty"].ReadOnly = false;
            Tottaltxt.Text = sum.ToString();
            RecipesDGV.DataContext = dt;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }       //Done

        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }       //Done

        private void RecipesDGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataTable dt = new DataTable();
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1 && ((grid.SelectedItem as DataRowView).Row.ItemArray[0] != ""))
                {
                    if (grid.CurrentCell.Column.Header == "Recipe_Unit")
                    {
                        AllUnits allUnits = new AllUnits(this,"DataGrid");
                        allUnits.ShowDialog();
                    }
                }
                else if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1 && ((grid.SelectedItem as DataRowView).Row.ItemArray[1] != ""))
                {
                    if (grid.CurrentCell.Column.Header == "Recipe_Unit")
                    {
                        AllUnits allUnits = new AllUnits(this,"DataGrid");
                        allUnits.ShowDialog();
                    }
                }
            }
        }

        private void UnitsBtn_Click(object sender, RoutedEventArgs e)
        {
            AllUnits allUnits = new AllUnits(this,"MainUnit");
            allUnits.ShowDialog();
        }       //Done
    }
}
