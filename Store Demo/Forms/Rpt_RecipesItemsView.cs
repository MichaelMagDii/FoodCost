using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Food_Cost.Report;
namespace Food_Cost.Forms
{
    public partial class Rpt_RecipesItemsView : Form
    {
        public Rpt_RecipesItemsView()
        {
            InitializeComponent();
            LoadData();
        }
        private DataTable RecTable;
        DataTable Dt;
        List<string> CatId = new List<string>();
        List<string> SCatID = new List<string>();
        List<string> RecID = new List<string>();

        private void LoadCategories()
        {
            listAvilableCategories.Items.Clear();
            Dt = Classes.RetrieveData("Code,Name", "Setup_RecipeCategory");
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    listAvilableCategories.Items.Add(Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["Code"].ToString());
                }
            }
        }
        private void LoadSubCategories()
        {
            string StrWhere = "";
            listAvilableSubCategories.Items.Clear();
            if (CatId.Count != 0)
            {
                StrWhere += "";
                foreach (string STR in CatId)
                {
                    StrWhere += STR + ",";
                }
                StrWhere = StrWhere.Remove(StrWhere.Length - 1, 1);
                StrWhere = "Category_ID IN (" + StrWhere + ")";

                Dt = Classes.RetrieveData("Code,Name", StrWhere, "Setup_RecipeSubCategories");
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        listAvilableSubCategories.Items.Add(Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["Code"].ToString());
                    }
                }
            }
        }
        private void LoadRecipes()
        {
            string StrWhere = "";
            listAvilableRecipes.Items.Clear();
            if (SCatID.Count != 0)
            {
                StrWhere += "";
                foreach (string STR in SCatID)
                {
                    StrWhere += STR + ",";
                }
                StrWhere = StrWhere.Remove(StrWhere.Length - 1, 1);
                StrWhere = "SubCategory_ID IN (" + StrWhere + ")";

                Dt = Classes.RetrieveData("Code,Name",StrWhere, "Setup_Recipes");
                if (Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        listAvilableRecipes.Items.Add(Dt.Rows[i]["Name"].ToString(), Dt.Rows[i]["Code"].ToString());
                    }
                }
            }
        }

        private void LoadData()
        {
            try
            {

                listChosenCategories.Items.Clear();
                listChosenSubCategories.Items.Clear();
                listChosenRecipes.Items.Clear();

                LoadCategories();
                //LoadSubCategories();
                //LoadRecipes();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectCat(string type)
        {
            try
            {
                if (listAvilableCategories.Items.Count > 0)
                {
                    listAvilableCategories.Sorting = System.Windows.Forms.SortOrder.None;
                    if (type == "FirstItem")
                    {
                        ListViewItem Ky = listAvilableCategories.Items[0];
                        CatId.Add(Ky.ImageKey);
                        listAvilableCategories.Items.Remove(Ky);
                        listChosenCategories.Items.Add(Ky);
                    }
                    else if (type == "SelectedItem" && listAvilableCategories.SelectedItems.Count > 0)
                    {
                        ListViewItem Ky = listAvilableCategories.SelectedItems[0];
                        CatId.Add(Ky.ImageKey);
                        listAvilableCategories.Items.Remove(Ky);
                        listChosenCategories.Items.Add(Ky);
                    }
                    else if (type == "All")
                    {
                        while (listAvilableCategories.Items.Count > 0)
                        {
                            ListViewItem Ky = listAvilableCategories.Items[0];
                            CatId.Add(Ky.ImageKey);
                            listAvilableCategories.Items.Remove(Ky);
                            listChosenCategories.Items.Add(Ky);
                        }
                    }
                }
                listChosenCategories.Sorting = System.Windows.Forms.SortOrder.Ascending;
                AvilbaleSubCat("All");
                AvilbaleRec("All");
                LoadSubCategories();
                LoadRecipes();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AvilbaleCat(string type)
        {
            try
            {
                if (listChosenCategories.Items.Count > 0)
                {
                    listChosenCategories.Sorting = System.Windows.Forms.SortOrder.None;
                    if (type == "FirstItem")
                    {
                        ListViewItem Ky = listChosenCategories.Items[0];
                        CatId.Remove(Ky.ImageKey);
                        listChosenCategories.Items.Remove(Ky);
                        listAvilableCategories.Items.Add(Ky);
                    }
                    else if (type == "SelectedItem" && listChosenCategories.SelectedItems.Count > 0)
                    {
                        ListViewItem Ky = listChosenCategories.SelectedItems[0];
                        CatId.Remove(Ky.ImageKey);
                        listChosenCategories.Items.Remove(Ky);
                        listAvilableCategories.Items.Add(Ky);
                    }
                    else if (type == "All")
                    {
                        while (listChosenCategories.Items.Count > 0)
                        {
                            ListViewItem Ky = listChosenCategories.Items[0];
                            CatId.Remove(Ky.ImageKey);
                            listChosenCategories.Items.Remove(Ky);
                            listAvilableCategories.Items.Add(Ky);
                        }
                    }
                }
                listAvilableCategories.Sorting = System.Windows.Forms.SortOrder.Ascending;
                AvilbaleSubCat("All");
                AvilbaleRec("All");
                LoadSubCategories();
                LoadRecipes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectSubCat(string type)
        {
            try
            {
                if (listAvilableSubCategories.Items.Count > 0)
                {
                    listAvilableSubCategories.Sorting = System.Windows.Forms.SortOrder.None;
                    if (type == "FirstItem")
                    {
                        ListViewItem Ky = listAvilableSubCategories.Items[0];
                        SCatID.Add(Ky.ImageKey);
                        listAvilableSubCategories.Items.Remove(Ky);
                        listChosenSubCategories.Items.Add(Ky);
                    }
                    else if (type == "SelectedItem" && listAvilableSubCategories.SelectedItems.Count > 0)
                    {
                        ListViewItem Ky = listAvilableSubCategories.SelectedItems[0];
                        SCatID.Add(Ky.ImageKey);
                        listAvilableSubCategories.Items.Remove(Ky);
                        listChosenSubCategories.Items.Add(Ky);
                    }
                    else if (type == "All")
                    {
                        while (listAvilableSubCategories.Items.Count > 0)
                        {
                            ListViewItem Ky = listAvilableSubCategories.Items[0];
                            SCatID.Add(Ky.ImageKey);
                            listAvilableSubCategories.Items.Remove(Ky);
                            listChosenSubCategories.Items.Add(Ky);
                        }
                    }
                }
                listChosenCategories.Sorting = System.Windows.Forms.SortOrder.Ascending;
                AvilbaleRec("All");
                LoadRecipes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AvilbaleSubCat(string type)
        {
            try
            {
                if (listChosenSubCategories.Items.Count > 0)
                {
                    listChosenSubCategories.Sorting = System.Windows.Forms.SortOrder.None;
                    if (type == "FirstItem")
                    {
                        ListViewItem Ky = listChosenSubCategories.Items[0];
                        SCatID.Remove(Ky.ImageKey);
                        listChosenSubCategories.Items.Remove(Ky);
                        listAvilableSubCategories.Items.Add(Ky);
                    }
                    else if (type == "SelectedItem" && listChosenSubCategories.SelectedItems.Count > 0)
                    {
                        ListViewItem Ky = listChosenSubCategories.SelectedItems[0];
                        SCatID.Remove(Ky.ImageKey);
                        listChosenSubCategories.Items.Remove(Ky);
                        listAvilableSubCategories.Items.Add(Ky);
                    }
                    else if (type == "All")
                    {
                        while (listChosenSubCategories.Items.Count > 0)
                        {
                            ListViewItem Ky = listChosenSubCategories.Items[0];
                            SCatID.Remove(Ky.ImageKey);
                            listChosenSubCategories.Items.Remove(Ky);
                            listAvilableSubCategories.Items.Add(Ky);
                        }
                    }
                }
                listAvilableSubCategories.Sorting = System.Windows.Forms.SortOrder.Ascending;
                AvilbaleRec("All");
                LoadRecipes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectsRec(string type)
        {
            try
            {
                if (listAvilableRecipes.Items.Count > 0)
                {
                    listAvilableRecipes.Sorting = System.Windows.Forms.SortOrder.None;
                    if (type == "FirstItem")
                    {
                        ListViewItem Ky = listAvilableRecipes.Items[0];
                        RecID.Add(Ky.ImageKey);
                        listAvilableRecipes.Items.Remove(Ky);
                        listChosenRecipes.Items.Add(Ky);
                    }
                    else if (type == "SelectedItem" && listAvilableRecipes.SelectedItems.Count > 0)
                    {
                        ListViewItem Ky = listAvilableRecipes.SelectedItems[0];
                        RecID.Add(Ky.ImageKey);
                        listAvilableRecipes.Items.Remove(Ky);
                        listChosenRecipes.Items.Add(Ky);
                    }
                    else if (type == "All")
                    {
                        while (listAvilableRecipes.Items.Count > 0)
                        {
                            ListViewItem Ky = listAvilableRecipes.Items[0];
                            RecID.Add(Ky.ImageKey);
                            listAvilableRecipes.Items.Remove(Ky);
                            listChosenRecipes.Items.Add(Ky);
                        }
                    }
                }
                listChosenRecipes.Sorting = System.Windows.Forms.SortOrder.Ascending;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AvilbaleRec(string type)
        {
            try
            {
                if (listChosenRecipes.Items.Count > 0)
                {
                    listChosenRecipes.Sorting = System.Windows.Forms.SortOrder.None;
                    if (type == "FirstItem")
                    {
                        ListViewItem Ky = listChosenRecipes.Items[0];
                        RecID.Remove(Ky.ImageKey);
                        listChosenRecipes.Items.Remove(Ky);
                        listAvilableRecipes.Items.Add(Ky);
                    }
                    else if (type == "SelectedItem" && listChosenRecipes.SelectedItems.Count > 0)
                    {
                        ListViewItem Ky = listChosenRecipes.SelectedItems[0];
                        RecID.Remove(Ky.ImageKey);
                        listChosenRecipes.Items.Remove(Ky);
                        listAvilableRecipes.Items.Add(Ky);
                    }
                    else if (type == "All")
                    {
                        while (listChosenRecipes.Items.Count > 0)
                        {
                            ListViewItem Ky = listChosenRecipes.Items[0];
                            RecID.Remove(Ky.ImageKey);
                            listChosenRecipes.Items.Remove(Ky);
                            listAvilableRecipes.Items.Add(Ky);
                        }
                    }
                }
                listAvilableRecipes.Sorting = System.Windows.Forms.SortOrder.Ascending;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region BTNS       
        private void listAvilableCategories__DoubleClick(object sender, EventArgs e)
        {
            SelectCat("SelectedItem");
        }
        private void listChosenCategories__DoubleClick(object sender, EventArgs e)
        {
            AvilbaleCat("SelectedItem");
        }

        private void listAvilableSubCategories__DoubleClick(object sender, EventArgs e)
        {
            SelectSubCat("SelectedItem");
        }
        private void listChosenSubCategories__DoubleClick(object sender, EventArgs e)
        {
            AvilbaleSubCat("SelectedItem");
        }

        private void listAvilableRecipes__DoubleClick(object sender, EventArgs e)
        {
            SelectsRec("SelectedItem");
        }
        private void listChosenRecipes__DoubleClick(object sender, EventArgs e)
        {
            AvilbaleRec("SelectedItem");
        }
        
        private void btntoSelCat_Click(object sender, EventArgs e)
        {
            SelectCat("FirstItem");
        }
        private void btnalltoSelAllCat_Click(object sender, EventArgs e)
        {
            SelectCat("All");
        }

        private void btntoAvlCat_Click(object sender, EventArgs e)
        {
            AvilbaleCat("FirstItem");
        }
        private void btntoAvlAllCat_Click(object sender, EventArgs e)
        {
            AvilbaleCat("All");
        }


        private void btntoSelSubCat_Click(object sender, EventArgs e)
        {
            SelectSubCat("FirstItem");
        }
        private void btntoSelAllSubCat_Click(object sender, EventArgs e)
        {
            SelectSubCat("All");
        }

        private void btntoAvlSubCat_Click(object sender, EventArgs e)
        {
            AvilbaleSubCat("FirstItem");
        }
        private void btntoAvlAllSubCat_Click(object sender, EventArgs e)
        {
            AvilbaleSubCat("All");
        }


        private void btntoSelRec_Click(object sender, EventArgs e)
        {
            SelectsRec("FirstItem");
        }
        private void btntoSelAllRec_Click(object sender, EventArgs e)
        {
            SelectsRec("All");
        }

        private void btntoAvlRec_Click(object sender, EventArgs e)
        {
            AvilbaleRec("FirstItem");
        }
        private void btntoAvlAllRec_Click(object sender, EventArgs e)
        {
            AvilbaleRec("All");
        }
        #endregion BTNS
        private string get_filter()
        {
            string filter = "";
            if (listChosenCategories.Items.Count > 0) {
                filter += "Categories: ";
                foreach (ListViewItem lc in listChosenCategories.Items)
                {
                    filter += lc.Text + ", ";
                }
                filter = filter.Remove(filter.Length - 2, 2);
            }
            if (listChosenSubCategories.Items.Count > 0)
            {
                filter += " \n "+"Sub Categories: ";
                foreach (ListViewItem lc in listChosenSubCategories.Items)
                {
                    filter += lc.Text + ", ";
                }
                filter = filter.Remove(filter.Length - 2, 2);
            }
            if (listChosenRecipes.Items.Count > 0)
            {
                filter += " \n " + "Recipes: ";
                foreach (ListViewItem lc in listChosenRecipes.Items)
                {
                    filter += lc.Text + ", ";
                }
                filter = filter.Remove(filter.Length - 2, 2);
            }
            return filter;
        }
        private void Show_Click(object sender, EventArgs e)
        {
            RecTable = new DataTable();
            string StrWhere = "";

            /*if (CatId.Count != 0)
            {
                StrWhere += "(";
                foreach (string STR in CatId)
                {
                    StrWhere += "Category_ID = " + STR + " or ";
                }
                if (SCatID.Count == 0 && RecID.Count == 0)
                {
                    StrWhere = StrWhere.Remove(StrWhere.Length - 4, 4);
                    StrWhere += ")";
                }
                else
                {
                    StrWhere = StrWhere.Remove(StrWhere.Length - 4, 4);
                    StrWhere += ") AND (";
                }
            }
            if (SCatID.Count != 0)
            {
                foreach (string STR in SCatID)
                {
                    StrWhere += "SubCategory_ID = " + STR + " or ";
                }

                if (RecID.Count == 0)
                {
                    StrWhere = StrWhere.Remove(StrWhere.Length - 4, 4);
                    StrWhere += ")";
                }
                else
                {
                    StrWhere = StrWhere.Remove(StrWhere.Length - 4, 4);
                    StrWhere += ") AND (";
                }
            }
            if (RecID.Count != 0)
            {
                foreach (string STR in RecID)
                {
                    StrWhere += "Recipe_Code = " + STR + " or ";
                }
                StrWhere = StrWhere.Remove(StrWhere.Length - 4, 4);
                StrWhere += ")";

                
            }
            if (StrWhere == "")
                RecTable = Classes.RetrieveData("*", "RecipesItemsView");
            else
                RecTable = Classes.RetrieveData("*", StrWhere, "RecipesItemsView");

            string Filter = get_filter();
            */
            ReportView Rec = new ReportView();
            Rec.Rpt = new CR_RecipesQty();

            DataTable dt = Classes.RetrieveData("SELECT dbo.RecipeQty.Recipe_ID, dbo.Setup_Recipes.CrossCode AS 'Cross Code', dbo.Setup_Recipes.Name, dbo.Setup_Recipes.Name2, dbo.RecipeQty.Qty, dbo.RecipeQty.Price,(SELECT Name FROM dbo.Setup_Restaurant WHERE(Code = dbo.RecipeQty.Resturant_ID)) AS Restaurant, (SELECT Name FROM dbo.Setup_Kitchens WHERE(Code = dbo.RecipeQty.Kitchen_ID) AND(RestaurantID = dbo.RecipeQty.Resturant_ID)) AS Kitchen FROM dbo.RecipeQty INNER JOIN dbo.Setup_Recipes ON dbo.RecipeQty.Recipe_ID = dbo.Setup_Recipes.Code");
            Rec.Rpt.SetDataSource(dt);
            Rec.Rpt.SetParameterValue("Filter", "");
            Rec.Show();

            //ReportView RV = new ReportView();
            //RV.Rpt = new CR_RecipesQty();
            //RV.Rpt.SetDataSource(RecTable);
            //RV.Rpt.SetParameterValue("Filter", "");
            //RV.Show();
        }
    }
}
