using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for GenerateBatch.xaml
    /// </summary>
    public partial class GenerateBatch : UserControl
    {
        bool isParent = false;
        public string valofStore = ""; public string valofRecipe = ""; public string ValOfKitchen = "";
        List<string> Authenticated = new List<string>();
        public string[,] RecipeItemDData = new string[100, 4];
        public bool checksofParent = true;
        public bool ToFinishFunction = true; public bool ToCloseFunction = true;
        string[,] RecipesData = new string[100, 4];
        public int CountofRecipeItemData = 0;
        int CountofRecipesData = 0;
        DataTable Data = new DataTable();
        public GenerateBatch()
        {
            if (MainWindow.AuthenticationData.ContainsKey("GenerateBatch"))
            {
                Authenticated = MainWindow.AuthenticationData["GenerateBatch"];
                if (Authenticated.Count == 0)
                {
                    MessageBox.Show("You Havent a Privilage to Open this Page");
                    LogIn logIn = new LogIn();
                    logIn.ShowDialog();
                }
                else
                { InitializeComponent(); }
            }
        }
        // Events
        private void RestaurantBtn_Click(object sender, RoutedEventArgs e)
        {
            All_Resturants allRestaurant = new All_Resturants(this);
            allRestaurant.ShowDialog();
            KitchenBtn.IsEnabled = true;
        }           //Done Finall Function
        private void KitchenBtn_Click(object sender, RoutedEventArgs e)
        {
            All_Kitchens allKitchen = new All_Kitchens(this, StoreIDcbx.Text);
            allKitchen.ShowDialog();
            RecipeBtn.IsEnabled = true;
        }           //Done Finall Function
        private void RecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            AllRecipes allRecipes = new AllRecipes(this);
            allRecipes.ShowDialog();
            if (Recipecbx.Text != "")
            {
                GenerateBtn.IsEnabled = true;
                QtyofRecipetxt.IsEnabled = true;
                UnitofRecipelbl.Visibility = Visibility.Visible;
                RecipesDGV.Visibility = Visibility.Visible;
                NameoftotalCost.Visibility = Visibility.Visible;
                TotalCosttxt.Visibility = Visibility.Visible;
                LoadtoDataGrid();
            }
        }           //Done Finall Function
        private void LoadtoDataGrid()
        {
            double summationofQTyValuo = 0;
            double totalCost = 0;
            double CostVlue = 0;
            string Filed = ""; string Where = "";
            DataTable RecipeItemsData = new DataTable();
            DataTable Names = new DataTable();
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd = new SqlCommand();
            //Michael's Update
            DataTable DT = new DataTable();
            DT.Columns.Add("Item Code");
            DT.Columns.Add("Recipe Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Name2");
            DT.Columns.Add("Qty");
            DT.Columns.Add("Recipe Unit");
            DT.Columns.Add("Cost");
            DT.Columns.Add("Total Cost");
            DT.Columns.Add("Cost Precentage");
            try
            {
                Filed = "Recipe_ID,Item_Code,Qty,Recipe_Unit";
                Where = string.Format("Recipe_Code={0}", valofRecipe);
                RecipeItemsData = Classes.RetrieveData(Filed, Where, "Setup_RecipeItems");
                con.Open();

                for (int i = 0; i < RecipeItemsData.Rows.Count; i++)
                {
                    if (RecipeItemsData.Rows[i][1].ToString() != "")
                    {
                        Where = string.Format("Code='{0}'", RecipeItemsData.Rows[i][1].ToString());
                        Names = Classes.RetrieveData("Name,Name2", Where, "Setup_Items");
                        try
                        {
                            string W = string.Format("select Current_Cost from Items Where ItemID={0} and RestaurantID={1} and KitchenID={2} ", RecipeItemsData.Rows[i][1].ToString(), valofStore, ValOfKitchen);
                            cmd = new SqlCommand(W, con);
                            if (cmd.ExecuteScalar() == null)
                            {
                                CostVlue = 0;
                            }
                            else
                            {
                                CostVlue = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                            }
                        }
                        catch { }
                        summationofQTyValuo += CostVlue * Convert.ToDouble(RecipeItemsData.Rows[i][2].ToString());
                    }
                    else
                    {
                        Where = string.Format("Code='{0}'", RecipeItemsData.Rows[i][0].ToString());
                        Names = Classes.RetrieveData("Name,Name2", Where, "Setup_Recipes");

                        string W = string.Format("select Price from RecipeQty Where Recipe_ID={0} and Resturant_ID={1} and Kitchen_ID={2}", RecipeItemsData.Rows[i][0].ToString(), valofStore, ValOfKitchen);
                        cmd = new SqlCommand(W, con);
                        if (cmd.ExecuteScalar() == null)
                        {
                            CostVlue = 0;
                        }
                        else
                        {
                            CostVlue = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                        }
                        summationofQTyValuo += CostVlue * Convert.ToDouble(RecipeItemsData.Rows[i][2].ToString());
                    }
                    DT.Rows.Add(RecipeItemsData.Rows[i][1].ToString(), RecipeItemsData.Rows[i][0].ToString(), Names.Rows[0][0].ToString(), Names.Rows[0][1].ToString(), RecipeItemsData.Rows[i][2].ToString(), RecipeItemsData.Rows[i][3].ToString(), CostVlue, "0", "0");
                }
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DT.Rows[i]["Total Cost"] = ((Convert.ToDouble(DT.Rows[i]["Qty"])) * (Convert.ToDouble(DT.Rows[i]["Cost"]))).ToString();
                    DT.Rows[i]["Cost Precentage"] = ((Convert.ToDouble(DT.Rows[i]["Total Cost"])) / (summationofQTyValuo) * 100).ToString() + " %";
                    totalCost += (Convert.ToDouble(DT.Rows[i]["Total Cost"]));

                }
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    DT.Columns[i].ReadOnly = true;
                }
                RecipesDGV.DataContext = DT;
                TotalCosttxt.Text = totalCost.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { con.Close(); }
        }             //Done Finall Function
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }  //Done
        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }  //Done
        private void OrderReq_Click(object sender, RoutedEventArgs e)
        {
            //UserControl usc = new Food_Cost.OrderRequesation(valofStore, ValOfKitchen);
            //Parent.Children.Clear();
            //Parent.Children.Add(usc);
        }   //Done  
        private void GenerateBtn_Click(object sender, RoutedEventArgs e)
        {
            CountofRecipeItemData = 0; CountofRecipesData = 0;
            string UnitQty = ""; string Unit = "";
            DataTable RecipeInfo = new DataTable();
            if (Authenticated.IndexOf("GenertaeGenerate") == -1 && Authenticated.IndexOf("CheckAllGenerateBatch") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                if (QtyofRecipetxt.Text == "")
                { MessageBox.Show("Please Enter Qty of Recipe"); return; }

                string Where = string.Format("Code={0}", valofRecipe);
                RecipeInfo = Classes.RetrieveData("UnitQty,Unit", Where, "Setup_Recipes");
                UnitQty = RecipeInfo.Rows[0][0].ToString();
                Unit = RecipeInfo.Rows[0][1].ToString();

                bool boolianCheck = ItsRecipe(valofRecipe, UnitQty, Unit);
                if (boolianCheck == true && ToFinishFunction == false)
                {
                    UpddateQty();
                    MessageBox.Show("Recipe Generated Succesfuly");
                }
                else if ((boolianCheck == false && ToFinishFunction == true) && isParent == false)
                {
                    MessageBoxResult result = MessageBox.Show("Can't Genertare this Recipe Because Item Can't Complete It , You wan't to Order ?", "Confirmation",
                                  MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        UserControl usc = new Food_Cost.OrderRequesation(valofStore, ValOfKitchen);
                        //Parent.Children.Clear();
                        //Parent.Children.Add(usc);
                    }
                }
                else
                {
                    MessageBox.Show("Please Check The Value and The Qty of Items OR Recipes");

                }

                UserControl us = new Food_Cost.GenerateBatch();
                //Parent.Children.Clear();
                //Parent.Children.Add(us);
            }
        }           //Done Finall Function

        public bool ItsRecipe(string Recipe, string RecipeQTy, string Unit)
        {
            bool DoneRecipe = true;
            bool boolianCheckItems; bool boolianCheckRecipes;
            boolianCheckItems = DochecksItems(Recipe, RecipeQTy, Unit);
            boolianCheckRecipes = DochecksRecipes(Recipe, RecipeQTy, Unit);
            if (boolianCheckItems == false || boolianCheckRecipes == false)
            {
                DoneRecipe = false;
            }
            return DoneRecipe;
        }    //Done
        public bool DochecksItems(string valofRecipee, string RecipeQty, string Unit)
        {
            checksofParent = true;
            ToFinishFunction = true;
            string BaseWeight = ""; string BaseUnit = ""; string secondWeight = ""; string SecondUnit = ""; string YeildofQty = "";
            bool boooolCHeck = true; bool CheckIfParentVal = true;
            double Qty = 0;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            try
            {
                con.Open();
                string q = "SELECT Item_Code,Qty,Recipe_Unit FROM Setup_RecipeItems Where Item_Code <> '' AND Recipe_Code=" + valofRecipee;
                SqlCommand cmd = new SqlCommand(q, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CheckIfParentVal = CheckIfParent(reader["Item_Code"].ToString());
                    if (CheckIfParentVal == true)
                    {
                        ParentItemInRecipe parentItem = new ParentItemInRecipe(reader["Item_Code"].ToString(), reader["Qty"].ToString(), reader["Recipe_Unit"].ToString(), RecipeQty, Unit, QtyofRecipetxt.Text, valofStore, ValOfKitchen, this);
                        parentItem.ShowDialog();
                        isParent = true;
                        if (ToFinishFunction == true)
                        {
                            boooolCHeck = false;
                        }
                    }
                    else
                    {
                        con2.Open();
                        string W = "SELECT Weight,Unit,Unit2,ConvUnit2,Yield FROM Setup_Items WHERE Code=" + reader["Item_Code"].ToString();
                        SqlCommand cmd2 = new SqlCommand(W, con2);
                        reader2 = cmd2.ExecuteReader();
                        while (reader2.Read())
                        {
                            YeildofQty = reader2["Yield"].ToString();
                            BaseWeight = reader2["Weight"].ToString();
                            BaseUnit = reader2["Unit"].ToString();
                            SecondUnit = reader2["Unit2"].ToString();
                            secondWeight = reader2["ConvUnit2"].ToString();
                        }
                        reader2.Close();
                        string w = "SELECT Qty,MinNumber,MaxNumber,Units,Current_Cost FROM Items WHERE RestaurantID=" + valofStore + " AND ItemID=" + reader["Item_Code"].ToString() + " AND KitchenID=" + ValOfKitchen;

                        cmd2 = new SqlCommand(w, con2);
                        reader2 = cmd2.ExecuteReader();
                        if (reader2.HasRows == true)
                        {
                            while (reader2.Read())
                            {
                                if (reader["Recipe_Unit"].ToString() == BaseUnit)
                                {
                                    Qty = (((Convert.ToDouble(reader2["Qty"])) - (((Convert.ToDouble(QtyofRecipetxt.Text)) * (Convert.ToDouble(reader["Qty"]))) / Convert.ToDouble(BaseWeight))));
                                    if (Qty < 0)
                                    {
                                        boooolCHeck = false;
                                    }
                                    else
                                    {
                                        RecipeItemDData[CountofRecipeItemData, 0] = reader["Item_Code"].ToString();
                                        RecipeItemDData[CountofRecipeItemData, 1] = ((float.Parse(reader["Qty"].ToString()) / float.Parse(BaseWeight)) * float.Parse(QtyofRecipetxt.Text)).ToString();
                                        RecipeItemDData[CountofRecipeItemData, 2] = (Convert.ToDouble(QtyofRecipetxt.Text) * ((Convert.ToDouble(reader["Qty"]) / Convert.ToDouble(BaseWeight) * Convert.ToDouble(reader2["Current_Cost"])) + ((100 - Convert.ToDouble(YeildofQty)) / 100) / Convert.ToDouble(BaseWeight) * Convert.ToDouble(reader2["Current_Cost"]))).ToString();
                                        RecipeItemDData[CountofRecipeItemData, 3] = Qty.ToString();
                                        CountofRecipeItemData++;
                                    }
                                }
                                else if (reader["Recipe_Unit"].ToString() == SecondUnit)
                                {
                                    Qty = (((Convert.ToDouble(reader2["Qty"])) - (((Convert.ToDouble(QtyofRecipetxt.Text)) * (Convert.ToDouble(reader["Qty"]) / Convert.ToDouble(secondWeight))) / Convert.ToDouble(BaseWeight))));
                                    if (Qty < 0)
                                    {
                                        boooolCHeck = false;
                                    }
                                    else
                                    {
                                        RecipeItemDData[CountofRecipeItemData, 0] = reader["Item_Code"].ToString();
                                        RecipeItemDData[CountofRecipeItemData, 1] = ((float.Parse(reader["Qty"].ToString()) / float.Parse(BaseWeight)) / float.Parse(secondWeight) * float.Parse(QtyofRecipetxt.Text)).ToString();
                                        RecipeItemDData[CountofRecipeItemData, 2] = (Convert.ToDouble(QtyofRecipetxt.Text) * ((Convert.ToDouble(reader["Qty"]) / Convert.ToDouble(secondWeight) / Convert.ToDouble(BaseWeight) * Convert.ToDouble(reader2["Current_Cost"])) + (((100 - (Convert.ToDouble(YeildofQty))) / 100) / Convert.ToDouble(BaseWeight) / Convert.ToDouble(secondWeight) * Convert.ToDouble(reader2["Current_Cost"])))).ToString();
                                        RecipeItemDData[CountofRecipeItemData, 3] = Qty.ToString();
                                        CountofRecipeItemData++;
                                    }
                                }
                            }
                        }
                        else
                            boooolCHeck = false;
                        con2.Close();
                    }
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
            if (boooolCHeck == true)
            {
                ToFinishFunction = false;
            }


            return boooolCHeck;
        }   //Done
        public bool DochecksRecipes(string valofRecipee, string RecipeQty, string Unit)
        {
            string IDofRecipes = "";
            bool boooolCHeck = true;
            double Qty = 0;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            try
            {
                con.Open();
                string q = "SELECT Recipe_ID,Qty,Recipe_Unit FROM Setup_RecipeItems Where Recipe_ID <> '' AND Recipe_Code=" + valofRecipee;
                SqlCommand cmd = new SqlCommand(q, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    IDofRecipes = reader["Recipe_ID"].ToString();
                    con2.Open();
                    string w = string.Format("select Qty,Price from RecipeQty where Recipe_ID='{0}' and Resturant_ID='{1}' and Kitchen_ID='{2}'", reader["Recipe_ID"], valofStore, ValOfKitchen);
                    SqlCommand cmd2 = new SqlCommand(w, con2);
                    reader2 = cmd2.ExecuteReader();
                    if (reader2.HasRows == true)
                    {
                        while (reader2.Read())
                        {
                            if (reader["Recipe_Unit"].ToString() == Unit)
                            {
                                Qty = Convert.ToDouble(reader2["Qty"]) - (Convert.ToDouble(reader["Qty"]) * Convert.ToDouble(QtyofRecipetxt.Text));
                                if (Qty < 0)
                                {
                                    boooolCHeck = false;
                                }
                                else
                                {
                                    RecipesData[CountofRecipesData, 0] = reader["Recipe_ID"].ToString();
                                    RecipesData[CountofRecipesData, 1] = (Convert.ToDouble(reader["Qty"]) * Convert.ToDouble(QtyofRecipetxt.Text)).ToString();
                                    RecipesData[CountofRecipesData, 2] = ((Convert.ToDouble(reader["Qty"]) * Convert.ToDouble(QtyofRecipetxt.Text)) * Convert.ToDouble(reader2["Price"])).ToString();
                                    RecipesData[CountofRecipesData, 3] = Qty.ToString();
                                    CountofRecipesData++;
                                }
                            }
                            else
                            {

                                string FiledSelection = string.Format("value");
                                string WhereFiltering = string.Format("(BaseUnit_Name='{1}' and SecondUnit_Name='{0}')", Unit, reader["Recipe_Unit"]);
                                DataTable TheDT = Classes.RetrieveData(FiledSelection, WhereFiltering, "Units_Conversion");
                                string TheValue = TheDT.Rows[0][0].ToString();

                                Qty = Convert.ToDouble(reader2["Qty"]) - (Convert.ToDouble(reader["Qty"]) * Convert.ToDouble(QtyofRecipetxt.Text) * Convert.ToDouble(TheValue));
                                if (Qty < 0)
                                {
                                    boooolCHeck = false;
                                }
                                else
                                {
                                    RecipesData[CountofRecipesData, 0] = reader["Recipe_ID"].ToString();
                                    RecipesData[CountofRecipesData, 1] = (Convert.ToDouble(reader["Qty"]) * Convert.ToDouble(TheValue) * Convert.ToDouble(QtyofRecipetxt.Text)).ToString();
                                    RecipesData[CountofRecipesData, 2] = ((Convert.ToDouble(reader["Qty"]) * Convert.ToDouble(TheValue) * Convert.ToDouble(QtyofRecipetxt.Text)) * Convert.ToDouble(reader2["Price"])).ToString();
                                    RecipesData[CountofRecipesData, 3] = Qty.ToString();
                                    CountofRecipesData++;
                                }
                            }
                        }
                    }
                    else
                        boooolCHeck = false;

                    //Micahel's Update to Groppi
                    
                    if(boooolCHeck==false && Classes.RetrieveData("SubRecipeCreation", "Setup_Code").Rows[0][0].ToString()=="True")
                    {
                        boooolCHeck = DoChecksOfSubRecipeItems(IDofRecipes, RecipeQty, Unit);
                    }
                    con2.Close();
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
            if (boooolCHeck == true)
            {
                ToFinishFunction = false;
            }


            return boooolCHeck;
        }   //Done
        public bool DoChecksOfSubRecipeItems(string valofRecipee, string RecipeQty, string Unit)
        {
            checksofParent = true;
            ToFinishFunction = true;
            string BaseWeight = ""; string BaseUnit = ""; string secondWeight = ""; string SecondUnit = ""; string YeildofQty = "";
            bool boooolCHeck = true; bool CheckIfParentVal = true;
            double Qty = 0;
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlConnection con2 = new SqlConnection(Classes.DataConnString);
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;
            try
            {
                con.Open();
                string q = "SELECT Item_Code,Qty,Recipe_Unit FROM Setup_RecipeItems Where Item_Code <> '' AND Recipe_Code=" + valofRecipee;
                SqlCommand cmd = new SqlCommand(q, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CheckIfParentVal = CheckIfParent(reader["Item_Code"].ToString());
                    if (CheckIfParentVal == true)
                    {
                        ParentItemInRecipe parentItem = new ParentItemInRecipe(reader["Item_Code"].ToString(), reader["Qty"].ToString(), reader["Recipe_Unit"].ToString(), RecipeQty, Unit, QtyofRecipetxt.Text, valofStore, ValOfKitchen, this);
                        parentItem.ShowDialog();
                        isParent = true;
                        if (ToFinishFunction == true)
                        {
                            boooolCHeck = false;
                        }
                    }
                    else
                    {
                        con2.Open();
                        string W = "SELECT Weight,Unit,Unit2,ConvUnit2,Yield FROM Setup_Items WHERE Code=" + reader["Item_Code"].ToString();
                        SqlCommand cmd2 = new SqlCommand(W, con2);
                        reader2 = cmd2.ExecuteReader();
                        while (reader2.Read())
                        {
                            YeildofQty = reader2["Yield"].ToString();
                            BaseWeight = reader2["Weight"].ToString();
                            BaseUnit = reader2["Unit"].ToString();
                            SecondUnit = reader2["Unit2"].ToString();
                            secondWeight = reader2["ConvUnit2"].ToString();
                        }
                        reader2.Close();
                        string w = "SELECT Qty,MinNumber,MaxNumber,Units,Current_Cost FROM Items WHERE RestaurantID=" + valofStore + " AND ItemID=" + reader["Item_Code"].ToString() + " AND KitchenID=" + ValOfKitchen;

                        cmd2 = new SqlCommand(w, con2);
                        reader2 = cmd2.ExecuteReader();
                        if (reader2.HasRows == true)
                        {
                            while (reader2.Read())
                            {
                                if (reader["Recipe_Unit"].ToString() == BaseUnit)
                                {
                                    Qty = (((Convert.ToDouble(reader2["Qty"])) - (((Convert.ToDouble(QtyofRecipetxt.Text)) * (Convert.ToDouble(reader["Qty"]))) / Convert.ToDouble(BaseWeight))));
                                    if (Qty < 0)
                                    {
                                        boooolCHeck = false;
                                    }
                                    else
                                    {
                                        RecipeItemDData[CountofRecipeItemData, 0] = reader["Item_Code"].ToString();
                                        RecipeItemDData[CountofRecipeItemData, 1] = ((float.Parse(reader["Qty"].ToString()) / float.Parse(BaseWeight)) * float.Parse(QtyofRecipetxt.Text)).ToString();
                                        RecipeItemDData[CountofRecipeItemData, 2] = (Convert.ToDouble(QtyofRecipetxt.Text) * ((Convert.ToDouble(reader["Qty"]) / Convert.ToDouble(BaseWeight) * Convert.ToDouble(reader2["Current_Cost"])) + ((100 - Convert.ToDouble(YeildofQty)) / 100) / Convert.ToDouble(BaseWeight) * Convert.ToDouble(reader2["Current_Cost"]))).ToString();
                                        RecipeItemDData[CountofRecipeItemData, 3] = Qty.ToString();
                                        CountofRecipeItemData++;
                                    }
                                }
                                else if (reader["Recipe_Unit"].ToString() == SecondUnit)
                                {
                                    Qty = (((Convert.ToDouble(reader2["Qty"])) - (((Convert.ToDouble(QtyofRecipetxt.Text)) * (Convert.ToDouble(reader["Qty"]) / Convert.ToDouble(secondWeight))) / Convert.ToDouble(BaseWeight))));
                                    if (Qty < 0)
                                    {
                                        boooolCHeck = false;
                                    }
                                    else
                                    {
                                        RecipeItemDData[CountofRecipeItemData, 0] = reader["Item_Code"].ToString();
                                        RecipeItemDData[CountofRecipeItemData, 1] = ((float.Parse(reader["Qty"].ToString()) / float.Parse(BaseWeight)) / float.Parse(secondWeight) * float.Parse(QtyofRecipetxt.Text)).ToString();
                                        RecipeItemDData[CountofRecipeItemData, 2] = (Convert.ToDouble(QtyofRecipetxt.Text) * ((Convert.ToDouble(reader["Qty"]) / Convert.ToDouble(secondWeight) / Convert.ToDouble(BaseWeight) * Convert.ToDouble(reader2["Current_Cost"])) + (((100 - (Convert.ToDouble(YeildofQty))) / 100) / Convert.ToDouble(BaseWeight) / Convert.ToDouble(secondWeight) * Convert.ToDouble(reader2["Current_Cost"])))).ToString();
                                        RecipeItemDData[CountofRecipeItemData, 3] = Qty.ToString();
                                        CountofRecipeItemData++;
                                    }
                                }
                            }
                        }
                        else
                            boooolCHeck = false;
                        con2.Close();
                    }
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
            if (boooolCHeck == true)
            {
                ToFinishFunction = false;
            }


            return boooolCHeck;
        }



        public void UpddateQty()
        {
            string valOfGenerate = "";
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            SqlCommand cmd = new SqlCommand();
            valOfGenerate = Classes.InCrementTransactionSerial("GenerateRecipe_tbl", "Generate_ID");
            try
            {
                con.Open();
                string s = string.Format("Insert into GenerateRecipe_tbl(Generate_ID,Generate_Date,Resturant_ID,Kitchen_ID,Recipe_ID,Qty,UserID,WS) Values ('{0}',GETDATE(),'{1}','{2}','{3}','{4}','{5}','{6}')", valOfGenerate, valofStore, ValOfKitchen, valofRecipe, QtyofRecipetxt.Text, MainWindow.UserID, Classes.WS);
                cmd = new SqlCommand(s, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { con.Close(); }


            try
            {
                con.Open();
                for (int i = 0; i < CountofRecipesData; i++)
                {
                    string s = string.Format("Update RecipeQty SEt Qty=Qty-{1} Where Recipe_ID='{0}' AND Resturant_ID={2} AND Kitchen_ID={3}", RecipesData[i, 0], Convert.ToDouble(RecipesData[i, 1]), valofStore, ValOfKitchen);
                    cmd = new SqlCommand(s, con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {  }
            finally { con.Close(); }

            double CostOfrecipe = 0;

            for (int i = 0; i < CountofRecipeItemData; i++)
            {
                CostOfrecipe += Convert.ToDouble(RecipeItemDData[i, 2]);
            }
            for (int i = 0; i < CountofRecipesData; i++)
            {
                CostOfrecipe += Convert.ToDouble(RecipesData[i, 2]);
            }
            try
            {
                con.Open();
                string s = "SELECT Recipe_ID FROM RecipeQty WHERE Recipe_ID=" + valofRecipe;
                cmd = new SqlCommand(s, con);
                if (cmd.ExecuteScalar() == null)
                {
                    s = string.Format("INSERT INTO RecipeQty Values({0},{1},{2},{3},{4})", valofRecipe, Convert.ToDouble(QtyofRecipetxt.Text), valofStore, ValOfKitchen, CostOfrecipe / Convert.ToDouble(QtyofRecipetxt.Text));
                    cmd = new SqlCommand(s, con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    s = string.Format("Update RecipeQty SEt Qty=Qty+{1},Price=(((Qty*Price)+({1}*{4}))/Qty+{1}) Where Recipe_ID={0} AND Resturant_ID={2} AND Kitchen_ID={3}", valofRecipe, Convert.ToDouble(QtyofRecipetxt.Text), valofStore, ValOfKitchen, CostOfrecipe / Convert.ToDouble(QtyofRecipetxt.Text));
                    cmd = new SqlCommand(s, con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { }
            finally { con.Close(); }
            ////////////////////////
            try
            {
                con.Open();
                for (int i = 0; i < CountofRecipeItemData; i++)
                {
                    string s = string.Format("insert into GenerateRecipe_Items (Generate_ID,RecipeItem_ID,Type,Qty,Cost,Net_Cost,Current_Qty) values ('{0}','{1}','Item','{2}','{3}','{4}','{5}')", valOfGenerate, RecipeItemDData[i, 0], RecipeItemDData[i, 1], RecipeItemDData[i, 2], (Convert.ToDouble(RecipeItemDData[i, 1]) * Convert.ToDouble(RecipeItemDData[i, 2])), RecipeItemDData[i, 3]);
                    cmd = new SqlCommand(s, con);
                    cmd.ExecuteNonQuery();
                }
                for (int i = 0; i < CountofRecipesData; i++)
                {
                    string s = string.Format("insert into GenerateRecipe_Items (Generate_ID,RecipeItem_ID,Type,Qty,Cost,Net_Cost,Current_Qty) values ('{0}','{1}','Recipe','{2}','{3}','{4}','{5}')", valOfGenerate, RecipesData[i, 0], RecipesData[i, 1], RecipesData[i, 2], (Convert.ToDouble(RecipesData[i, 1]) * Convert.ToDouble(RecipesData[i, 2])), RecipesData[i, 3]);
                    cmd = new SqlCommand(s, con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { con.Close(); }

            try
            {
                con.Open();
                for (int i = 0; i < CountofRecipeItemData; i++)
                {
                    string w = string.Format("update Items set Qty=Qty-'{0}',Net_Cost=((Qty-'{0}')*Current_Cost) where RestaurantID='{1}' and KitchenID='{2}' and ItemID='{3}'", RecipeItemDData[i, 1], valofStore, ValOfKitchen, RecipeItemDData[i, 0]);
                    cmd = new SqlCommand(w, con);
                    cmd.ExecuteNonQuery();

                    w = string.Format("update ItemsYear Set {0}={0}-{1} where ItemID='{2}' and Restaurant_ID='{3}' and Kitchen_ID='{4}' and Year='{5}'", MainWindow.MonthQty, RecipeItemDData[i, 1], RecipeItemDData[i, 0], valofStore, ValOfKitchen, MainWindow.CurrentYear);
                    cmd = new SqlCommand(w, con);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { con.Close(); }
        }   //Done

        private bool CheckIfParent(string Item_Code)
        {
            bool check = true;
            DataTable IfParent = new DataTable();
            string Where = string.Format("Parent_Item='{0}'", Item_Code);
            IfParent = Classes.RetrieveData("Parent_Item", Where, "Setup_ParentItems");
            if (IfParent.Rows.Count > 0)
            { check = true; }
            else
            { check = false; }
            return check;
        }           //Done FInall Function
      

    }
}
