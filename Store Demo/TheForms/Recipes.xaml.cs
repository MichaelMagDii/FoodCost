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
using System.Security.Authentication.ExtendedProtection.Configuration;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for Recipes.xaml
    /// </summary>
    public partial class Recipes : UserControl
    {
        //Michael's Update
        DataTable RecipeItems = new DataTable();
        bool ValtoRemoveRecipe = false;
        List<string> Authenticated = new List<string>();
        int codeTodelete = 0;
        List<TreeViewItem> Recipess = new List<TreeViewItem>();
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
                    loadAllRecipes();
                }
            }
        }       //Done Finall Function
        public void loadAllRecipes()
        {
            treeViewItems.Items.Clear();
            LoadCategoreis();
            Recipess = LoadSubCategoreies();
            LoadRecipes(Recipess);  
        }       //Done Finall Function
        private void LoadRecipes(List<TreeViewItem> classNodes)
        {
            string Where = "";
            List<TreeViewItem> treeViewItemscopy = new List<TreeViewItem>();
            for (int i = 0; i < classNodes.Count; i++)
            {
                Where = string.Format("Category_ID=(select Category_ID from Setup_RecipeSubCategories where Name=N'{0}') AND SubCategory_ID=(Select Code From Setup_RecipeSubCategories where Name=N'{0}')", classNodes[i].Header);
                DataTable AllRecipes = new DataTable();
                AllRecipes = Classes.RetrieveData("Code,Name,Name2", Where, "Setup_Recipes");
                for (int q = 0; q < AllRecipes.Rows.Count; q++)
                {
                    TreeViewItem treeViewItem = new TreeViewItem();
                    treeViewItem.Header = AllRecipes.Rows[q][1].ToString();
                    classNodes[i].Items.Add(treeViewItem);
                    treeViewItemscopy.Add(treeViewItem);
                }

            }
        }           //Done Finall Function
        private List<TreeViewItem> LoadSubCategoreies()
        {
            List<TreeViewItem> treeViewItemscopy = new List<TreeViewItem>();
            for (int i = 0; i < treeViewItems.Items.Count; i++)
            {
                string Where = "";
                DataTable AllSubCategoreies = new DataTable();
                Where = string.Format("Category_ID=(Select Code From Setup_RecipeCategory where Name=N'{0}')", ((TreeViewItem)treeViewItems.Items[i]).Header);
                AllSubCategoreies = Classes.RetrieveData("Name", Where, "Setup_RecipeSubCategories");
                for (int q = 0; q < AllSubCategoreies.Rows.Count; q++)
                {
                    TreeViewItem treeViewItem = new TreeViewItem();
                    treeViewItem.Header = AllSubCategoreies.Rows[q][0];
                    ((TreeViewItem)treeViewItems.Items[i]).Items.Add(treeViewItem);
                    treeViewItemscopy.Add(treeViewItem);

                }
            }
            return treeViewItemscopy;
        }           //Done Finall Function
        private void LoadCategoreis()
        {
            DataTable AllCategoreis = new DataTable();
            AllCategoreis = Classes.RetrieveData("Name", "Setup_RecipeCategory");
            for (int i = 0; i < AllCategoreis.Rows.Count; i++)
            {
                TreeViewItem treeViewItem = new TreeViewItem();
                treeViewItem.Header = AllCategoreis.Rows[i][0].ToString();
                treeViewItems.Items.Add(treeViewItem);
            }

        }       //Done Finall Function
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
        }       //Done Finall Function
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
            //AllRecipesDGV.DataContext = null;
            RecipesDGV.DataContext = null;
            //FillRecpieDGV();
        }       //Done Finall Function
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
        }       //Done Finall Function
        private List<TreeViewItem> FindTreeNodes()
        {
            List<TreeViewItem> treeNodes = new List<TreeViewItem>();

            TreeViewItem currentItem = treeViewItems.SelectedItem as TreeViewItem;
            while (currentItem != null)
            {
                treeNodes.Add(currentItem);

                try
                {
                    currentItem = currentItem.Parent as TreeViewItem;
                }
                catch
                {
                    return treeNodes;
                }
            }

            treeNodes.Reverse();
            while (treeNodes.Count != 5)
            {
                TreeViewItem treeViewItem = new TreeViewItem();
                treeViewItem.Header = "Null";
                treeNodes.Add(treeViewItem);
            }

            return treeNodes;
        }   //Done Finall Function
        private void TreeViewItems_SelectedItemChanged(object sender, MouseButtonEventArgs e)
        {
            List<TreeViewItem> treeNodes = FindTreeNodes();
            TreeView senderObject = sender as TreeView;

            if (senderObject.SelectedItem == treeNodes[2])
            {
                LoadData((senderObject.SelectedItem as TreeViewItem).Header.ToString());
            }
            try
            { (senderObject.SelectedItem as TreeViewItem).IsSelected = false;  }
            catch {}
        }       //Done Finall Function
        private void LoadData(string RecipeName)
        {
            string category = "", SubCategory = "";
            DataTable RecipeInfo = new DataTable();
            DataTable RecipeCat = new DataTable();
            DataTable RecipeSubCat = new DataTable();
            string WhereFiltering = string.Format("Name=N'{0}'", RecipeName);
            RecipeInfo = Classes.RetrieveData("*", WhereFiltering, "Setup_Recipes");

            WhereFiltering = string.Format("Code={0}", RecipeInfo.Rows[0]["Category_ID"]);
            category = Classes.RetrieveData("Name", WhereFiltering, "Setup_RecipeCategory").Rows[0][0].ToString();

            WhereFiltering = string.Format("Code={0}", RecipeInfo.Rows[0]["SubCategory_ID"]);
            SubCategory = Classes.RetrieveData("Name", WhereFiltering, "Setup_RecipeSubCategories").Rows[0][0].ToString();

            codetxt.Text = RecipeInfo.Rows[0]["Code"].ToString();
            Nametxt.Text = RecipeInfo.Rows[0]["Name"].ToString();
            Name2txt.Text = RecipeInfo.Rows[0]["Name2"].ToString();
            CrossCodetxt.Text = RecipeInfo.Rows[0]["CrossCode"].ToString();
            Categorytxt.Text = category;
            SUBCategorytxt.Text = SubCategory;
            Categtxt.Text = RecipeInfo.Rows[0]["Category_ID"].ToString();
            SUBCategtxt.Text = RecipeInfo.Rows[0]["SubCategory_ID"].ToString();
            ActiveChbx.IsChecked = RecipeInfo.Rows[0]["IsActive"].ToString().Equals("True");
            Unittxt.Text = RecipeInfo.Rows[0]["UnitQty"].ToString();
            Unitstxt.Text = RecipeInfo.Rows[0]["Unit"].ToString();
            LoadRecipeItemsToGrid(codetxt.Text);
        }       //Done FInall Function
        public void LoadRecipeItemsToGrid(string Code)
        {
            double TotalOfTotalCost = 0;
            double CostVlue = 0;
            double summationofQTyValuo = 0; double SummationOfItem = 0;
            DataTable RecipeItemsInfo = new DataTable();
            DataTable RecipeItemsCost = new DataTable();
            DataTable RecipeItemsName = new DataTable();
            DataTable TheCost = new DataTable();
            DataTable DT = new DataTable();
            DT.Columns.Add("Item_Code");

            DT.Columns.Add("Recipe_Code");
            DT.Columns.Add("Name");
            DT.Columns.Add("Name2");
            DT.Columns.Add("Qty");
            DT.Columns.Add("Recipe_Unit");
            DT.Columns.Add("Cost");
            DT.Columns.Add("Total_Cost");
            DT.Columns.Add("Cost_Precentage");

            try
            {
                string WhereFiltering = "Recipe_Code='" + Code + "'";
                RecipeItemsInfo = Classes.RetrieveData("Recipe_ID,Item_Code,Qty,Recipe_Unit", WhereFiltering, "Setup_RecipeItems");
                for (int i = 0; i < RecipeItemsInfo.Rows.Count; i++)
                {
                    if (RecipeItemsInfo.Rows[i]["Item_Code"].ToString() != "")
                    {

                        WhereFiltering = string.Format("ItemID='{0}' and RestaurantID=(select Code From Setup_Restaurant where IsMain='True') and KitchenID=(select Code From Setup_Kitchens where IsMain='True' and RestaurantID=(select Code From Setup_Restaurant where IsMain='True'))", RecipeItemsInfo.Rows[i]["Item_Code"].ToString());
                        RecipeItemsCost = Classes.RetrieveData("Current_Cost", WhereFiltering, "Items");
                        if (RecipeItemsCost.Rows.Count != 0)
                        {
                            CostVlue = Convert.ToDouble(RecipeItemsCost.Rows[0][0].ToString());
                            SummationOfItem = CostVlue * Convert.ToDouble(RecipeItemsInfo.Rows[i]["Qty"].ToString());
                        }
                        else
                        {
                            CostVlue = 0;
                            SummationOfItem = 0;
                        }
                        summationofQTyValuo = SummationOfItem;

                        WhereFiltering = "Code='" + RecipeItemsInfo.Rows[i]["Item_Code"].ToString() + "'";
                        RecipeItemsName = Classes.RetrieveData("Name,Name2", WhereFiltering, "Setup_Items");
                    }
                    else
                    {
                        WhereFiltering = string.Format("Recipe_ID={0} and Resturant_ID=(select Code From Setup_Restaurant where IsMain='True') and Kitchen_ID=(select Code From Setup_Kitchens where IsMain='True' and RestaurantID=(select Code From Setup_Restaurant where IsMain='True'))", RecipeItemsInfo.Rows[i]["Recipe_ID"].ToString());
                        RecipeItemsCost = Classes.RetrieveData("Price", WhereFiltering, "RecipeQty");
                        if (RecipeItemsCost.Rows.Count != 0)
                        {
                            CostVlue = Convert.ToDouble(RecipeItemsCost.Rows[0][0].ToString());
                            SummationOfItem = CostVlue * Convert.ToDouble(RecipeItemsInfo.Rows[i]["Qty"].ToString());

                        }
                        else
                        {
                            CostVlue = 0;
                            SummationOfItem = 0;
                        }

                        summationofQTyValuo = SummationOfItem;

                        WhereFiltering = "Code='" + RecipeItemsInfo.Rows[i]["Recipe_ID"].ToString() + "'";
                        RecipeItemsName = Classes.RetrieveData("Name,Name2", WhereFiltering, "Setup_Recipes");
                    }
                    TotalOfTotalCost += summationofQTyValuo;
                    DT.Rows.Add(RecipeItemsInfo.Rows[i][1], RecipeItemsInfo.Rows[i][0], RecipeItemsName.Rows[0][0], RecipeItemsName.Rows[0][1], RecipeItemsInfo.Rows[i][2], RecipeItemsInfo.Rows[i][3], CostVlue, SummationOfItem, "0");
                }
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DT.Rows[i]["Cost_Precentage"] = Math.Round((Convert.ToDouble(DT.Rows[i]["Total_Cost"]) / TotalOfTotalCost) * 100, 3);
                }

                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    DT.Columns[i].ReadOnly = true;
                }
                DT.Columns["Qty"].ReadOnly = false;
                Tottaltxt.Text = TotalOfTotalCost.ToString();
                RecipesDGV.DataContext = DT;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            EnableUiFormat();
            codetxt.IsEnabled = false;
            NewBtn.IsEnabled = false;
            SaveBtn.IsEnabled = false;
        }       //Done FInall Function
        private void GetCatBtn(object sender, RoutedEventArgs e)
        {
            //AllRecipesDGV.SelectedItem = null;
            AllCategories allcategories = new AllCategories(this);
            allcategories.ShowDialog();
        }       //Done Fiinall Function
        private void GetSubCat(object sender, RoutedEventArgs e)
        {
            //AllRecipesDGV.SelectedItem = null;
            if (Categtxt.Text == "")
            {
                MessageBox.Show("Enter the Category First");
                return;
            }
            AllSubCategories allSubCategories = new AllSubCategories(this, (Categtxt.Text).ToString());
            allSubCategories.ShowDialog();
        }       //Done Finall Function
        private void AddRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            //AllRecipesDGV.SelectedItem = null;
            if (Authenticated.IndexOf("AddRecipes") == -1 && Authenticated.IndexOf("CheckAllRecipes") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            { AllRecipes allRecipes = new AllRecipes(this); allRecipes.ShowDialog(); }
        }       //Done Finall Function
        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            //AllRecipesDGV.SelectedItem = null;
            if (Authenticated.IndexOf("AddItemRecipes") == -1 && Authenticated.IndexOf("CheckAllRecipes") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            { Items items = new Items(this); items.ShowDialog(); }
        }           //Done Finall Function
        private void UnitsBtn_Click(object sender, RoutedEventArgs e)
        {
            //AllRecipesDGV.SelectedItem = null;
            AllUnits allUnits = new AllUnits(this, "MainUnit");
            allUnits.ShowDialog();
        }       //Done  Finall Function
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
        }       //Done Finall Fuction
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
        }       //Done Finall Function
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
            codetxt.Text = IncrementCode();
        }       //Done Finall Function
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        { MainUiFormat(); EmptyTexts(); }       //Done Finall Function
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
                        AllUnits allUnits = new AllUnits(this, "DataGrid");
                        allUnits.ShowDialog();
                    }
                }
                else if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1 && ((grid.SelectedItem as DataRowView).Row.ItemArray[1] != ""))
                {
                    if (grid.CurrentCell.Column.Header == "Recipe_Unit")
                    {
                        AllUnits allUnits = new AllUnits(this, "DataGrid");
                        allUnits.ShowDialog();
                    }
                }
            }
        }       //Done Finall Function to get Units
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
                                                   "',Name=N'" + Nametxt.Text +
                                                   "',Name2=N'" + Name2txt.Text +
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
                        string H = "Insert into Setup_RecipeItems (Item_Code,Recipe_ID,Qty,Recipe_Unit,Recipe_Code) Values('" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[0] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[1] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[4] + "',N'" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[5] + "','" + codetxt.Text.ToString() + "')";
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
            loadAllRecipes();
        }       //Done Finall Function
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            RecipeItems = RecipesDGV.DataContext as DataTable;
            if (Authenticated.IndexOf("SaveRecipes") == -1 && Authenticated.IndexOf("CheckAllRecipes") == -1)
            { LogIn logIn = new LogIn(); logIn.ShowDialog(); }
            else
            {
                if (codetxt.Text == "")
                { MessageBox.Show("Code Field Can't Be Empty"); return; }
                if (Nametxt.Text == "")
                { MessageBox.Show("Name Field Can't Be Empty"); return; }
                if (Categtxt.Text == "")
                { MessageBox.Show("Should Enter the Category First"); return; }
                if (SUBCategtxt.Text == "")
                { MessageBox.Show("Should Enter the Sub Category First"); return; }
                if (Unittxt.Text == "")
                { MessageBox.Show("Should Enter the Unites First"); return; }
                if (Unitstxt.Text == "")
                { MessageBox.Show("Should Select the Unites First"); return; }

                DataTable CheckCode = new DataTable();
                string Where = string.Format("Code='{0}'", codetxt.Text);
                CheckCode = Classes.RetrieveData("Code", Where, "Setup_Recipes");
                if(CheckCode.Rows.Count !=0)
                { MessageBox.Show("This Code Is Not Avaliable"); return; }

                DataTable NameofRecipe = new DataTable();
                Where = string.Format("Name=N'{0}'", Nametxt.Text);
                NameofRecipe = Classes.RetrieveData("Name", Where, "Setup_Recipes");
                if (NameofRecipe.Rows.Count != 0)
                {
                    MessageBox.Show("You Entered This Recipe Name Before");
                    return;
                }

                try
                {
                    string FiledSelection = "Code,CrossCode,Name,Name2,Category_ID,SubCategory_ID,IsActive,Unit,UnitQty,CreateDate,UserID,WS";
                    string Values = string.Format("'{0}','{1}',N'{2}',N'{3}','{4}','{5}','{6}',N'{7}','{8}',GETDATE(),'{9}','{10}'", codetxt.Text, CrossCodetxt.Text, Nametxt.Text, Name2txt.Text, Categtxt.Text, SUBCategtxt.Text, ActiveChbx.IsChecked, Unitstxt.Text, Unittxt.Text, MainWindow.UserID, Classes.WS);
                    Classes.InsertRow("Setup_Recipes", FiledSelection, Values);
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }


                try
                {
                    for (int i = 0; i < RecipesDGV.Items.Count; i++)
                    {
                        string FiledSelection = "Item_Code,Recipe_ID,Qty,Recipe_Unit,Recipe_Code";
                        string Values = string.Format("'{0}','{1}','{2}',N'{3}','{4}'", RecipeItems.Rows[i]["Item_Code"], RecipeItems.Rows[i]["Recipe_Code"], RecipeItems.Rows[i]["Qty"], RecipeItems.Rows[i]["Recipe_Unit"], codetxt.Text);
                        Classes.InsertRow("Setup_RecipeItems", FiledSelection, Values);
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

                MainUiFormat();
                EmptyTexts();
                SaveBtn.IsEnabled = false;
                ActiveChbx.IsChecked = false;
                loadAllRecipes();
                MessageBox.Show("Saved Successfully");
            }

        }       //Done Finall Function
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
                loadAllRecipes();
            }
        }       //Done  Finall Function
        private void RecipesDGV_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = RecipesDGV.DataContext as DataTable;
            for (int i = 0; i < dt.Columns.Count; i++)
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
        }       //Done Finall Function
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
                LastVal = Convert.ToInt32(Classes.RetrieveData("top(1)Code", "Setup_Recipes Order by Code DESC").Rows[0][0].ToString());
                LastVal += 1;
                string VAL = LastVal.ToString();
                if (codetxt.Text == "")
                {
                    MessageBox.Show("Code Field Can't Be Empty");
                    return;
                }

                DataTable NameofRecipe = new DataTable();
                string Where = string.Format("Name=N'{0}'", Nametxt.Text);
                NameofRecipe = Classes.RetrieveData("Name", Where, "Setup_Recipes");
                if(NameofRecipe.Rows.Count !=0)
                {
                    MessageBox.Show("You Entered This Recipe Name Before");
                    return;
                }

                string connString = ConfigurationManager.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString;
                SqlConnection con = new SqlConnection(connString);

                try
                {
                    con.Open();
                    string q = "insert into Setup_Recipes (Code,CrossCode,Name,Name2,Category_ID,SubCategory_ID,IsActive,Unit,UnitQty)Values(" + VAL + ",'" + CrossCodetxt.Text + "',N'" + Nametxt.Text + "',N'" + Name2txt.Text + "','" + Categtxt.Text + "','" + SUBCategtxt.Text + "','" + ActiveChbx.IsChecked + "',N'" + Unitstxt.Text + "','" + Unittxt.Text + "')";
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
                        string H = "Insert into Setup_RecipeItems (Item_Code,Recipe_ID,Qty,Recipe_Unit,Recipe_Code) Values('" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[0] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[1] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[4] + "','" + ((DataRowView)RecipesDGV.Items[i]).Row.ItemArray[5] + "','" + VAL + "')";
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
                loadAllRecipes();
                MessageBox.Show("Saved Successfully");
            }
        }       //Done FInall Function
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

        private string IncrementCode()
        {
            int Code = 0;
            try
            {
                Code = Convert.ToInt32(Classes.RetrieveData("top(1)Code", "Setup_Recipes Order by Code DESC").Rows[0][0].ToString());
                Code += 1;
            }
            catch { Code = 1; }
            return Code.ToString();
        } 
    }
}
