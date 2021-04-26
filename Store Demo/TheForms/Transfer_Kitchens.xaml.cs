using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Food_Cost
{
    /// <summary>
    /// Interaction logic for Transfer_Kitchens.xaml
    /// </summary>
    public partial class Transfer_Kitchens : UserControl
    {
        List<string> Authenticated = new List<string>();
        int codeTodelete = 0;
        public Transfer_Kitchens()
        {
            if (MainWindow.AuthenticationData.Count != 0)
            {
                if (MainWindow.AuthenticationData.ContainsKey("TransferKitchen"))
                {
                    Authenticated = MainWindow.AuthenticationData["TransferKitchen"];
                    if (Authenticated.Count == 0)
                    {
                        MessageBox.Show("You Havent a Privilage to Open this Page");
                        LogIn logIn = new LogIn();
                        logIn.ShowDialog();
                    }
                    else
                    {
                        InitializeComponent();
                        transfer_No.Text = Classes.InCrementTransactionSerial("Transfer_Kitchens", "Transfer_Serial");
                    }
                }
            }
            else { MessageBox.Show("You should Login First !"); LogIn logIn = new LogIn(); logIn.ShowDialog(); }
        }       //Done
        private bool DoSomeChecks()
        {
            if (transfer_No.Text.Equals(""))
            {
                MessageBox.Show("Transfer No. Can't Be Empty");
            }
            else if (Manual_transfer_No.Text.Equals(""))
            {
                MessageBox.Show("Manual Transfer No. Can't Be Empty");
            }
            else if (Transfer_dt.Text.Equals(""))
            {
                MessageBox.Show("Transfer Date Can't Be Empty");
            }
            //else if (Transfer_Time.Text == null)
            //{
            //    MessageBox.Show("Transfer Time Can't Be Empty");
            //}
            else if (Statustxt.Text.Equals(""))
            {
                MessageBox.Show("Status Can't Be Empty");
            }
            else if (From_Kitchen.Text.Equals(""))
            {
                MessageBox.Show("Choose a Resturant To Transfer From");
            }
            else if (To_Kitchen.Text.Equals(""))
            {
                MessageBox.Show("Choose a Resturant To Transfer To");
            }
            else if (ItemsDGV.Items.Count == 0)
            {
                MessageBox.Show("Items can not be empty");
            }
            else
            {
                DataTable dt = ItemsDGV.DataContext as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Qty"].ToString() == "")
                    {
                        ItemsDGV.CurrentCell = new DataGridCellInfo(ItemsDGV.Items[i], ItemsDGV.Columns[4]);
                        ItemsDGV.BeginEdit();
                        MessageBox.Show(string.Format("Qty Input of Item {0} is Null", i + 1));
                        return false;
                    }
                }

                return true;
            }
            return false;
        }               //Done
        private void ClearData()
        {
            Manual_transfer_No.Text = "";
            Transfer_dt.Text = "";
            Transfer_Time.Text = "";
            commenttxt.Text = "";
            Resturant.Text = "";
            From_Kitchen.Text = "";
            To_Kitchen.Text = "";
            NUmberOfItems.Text = "0";
            Total_Price.Text = "0";
            ItemsDGV.DataContext = null;
        }                   //Done
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("AddItemTransferKitchen") == -1 && Authenticated.IndexOf("CheckAllTransferKitchen") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                Items itemswindow = new Items(this);
                itemswindow.ShowDialog();
            }
        }       //Done
        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.IsEnabled = true;
            TransferBtn.IsEnabled = true;
            NewBtn.IsEnabled = false;
            transfer_No.Text = Classes.InCrementTransactionSerial("Transfer_Kitchens", "Transfer_Serial");
            transfer_No.IsReadOnly = true;
            Manual_transfer_No.IsReadOnly = false;
            Manual_transfer_No.IsEnabled = true;
            Transfer_dt.IsEnabled = true;
            Transfer_Time.IsEnabled = true;
            commenttxt.IsReadOnly = false;
            commenttxt.IsEnabled = true;
            Resturant.IsReadOnly = true;
            Resturant.IsEnabled = true;
            Statustxt.IsEnabled = true;
            Statustxt.IsReadOnly = true;

            From_Kitchen.IsReadOnly = true;
            From_Kitchen.IsEnabled = true;

            To_Kitchen.IsReadOnly = true;
            To_Kitchen.IsEnabled = true;

            resturantBtn.IsEnabled = true;
            FromKitchenBtn.IsEnabled = true;
            ToKitchenBtn.IsEnabled = true;
            ItemsDGV.IsReadOnly = false;
            AddItemsBtn.IsEnabled = true;
            RemoveItemBtn.IsEnabled = true;
            SearchBtn.IsEnabled = true;
            NewBtn.IsEnabled = false;
            UndoBtn.IsEnabled = true;
            TransferBtn.IsEnabled = true;
            ClearData();
            Transfer_dt.SelectedDate = DateTime.Now;
        }           //Done
        private void Save_TIK_Items()
        { 
            try
            {
                DataTable dt = ItemsDGV.DataContext as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    float NetCost = float.Parse(dt.Rows[i]["Qty"].ToString()) * float.Parse(dt.Rows[i][7].ToString());
                    string Values = "'" + dt.Rows[i]["Code"] + "','" + transfer_No.Text + "','" + dt.Rows[i]["Qty"] + "','" + dt.Rows[i]["Unit"] + "','" + i + "','" + dt.Rows[i][7] + "','" + NetCost + "','" + dt.Rows[i][To_Kitchen.Text + " Qty"] + "','" + dt.Rows[i][To_Kitchen.Text + " Unit Cost"] + "','" + dt.Rows[i][From_Kitchen.Text + " Qty"] + "','" + dt.Rows[i][From_Kitchen.Text + " Unit Cost"] + "','" + dt.Rows[i]["Recipe"] + "'";
                    Classes.InsertRow("Transfer_Kitchens_Items", Values);
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), transfer_No.Text, "Transfer_Kitchens_Items", "New");
                }
            }
            catch
            {
                MessageBox.Show("Items Input Error");
            }
        }           //Done
        private void Save_TIK()
        {
            try
            {
                string FiledSelection = "Transfer_Serial,Manual_Transfer_No,Transfer_Date,Comment,From_Resturant_ID,To_Resturant_ID,From_Kitchen_ID,To_Kitchen_ID,Create_Date,Type,UserID,WS,Status,Total_Cost";
                string values = string.Format("'{0}', '{1}', '{2}', '{3}', (select Code from Setup_Restaurant where Name = '{4}'),(select Code from Setup_Restaurant where Name = '{5}'),(select Code from Setup_Kitchens where Name = '{6}'),(select Code from Setup_Kitchens where Name = '{7}'), GETDATE(),'{8}','{9}',{10},'{11}','{12}'", transfer_No.Text, Manual_transfer_No.Text,Convert.ToDateTime(Transfer_dt.Text).ToString("MM-dd-yyyy") , commenttxt.Text, Resturant.Text, Resturant.Text, From_Kitchen.Text, To_Kitchen.Text, "Transfer_Kitchen", MainWindow.UserID, Classes.WS, Statustxt.Text, Total_Price.Text);
                Classes.InsertRow("Transfer_Kitchens", FiledSelection, values);
                Classes.LogTable(Classes.MyComm.CommandText.ToString(), transfer_No.Text, "Transfer_Kitchens", "New");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }           //Done
        private void Edit_TKI()
        {
            try
            {
                string FiledSlection = "Manual_Transfer_No,Transfer_Date,Comment,From_Resturant_ID,To_Resturant_ID,From_Kitchen_ID,To_Kitchen_ID,Type,Status,Modifiled_Date,Total_Cost";
                string Values = string.Format("'{0}','{1}','{2}',(select Code From Setup_Restaurant where Name='{3}'),(select Code From Setup_Restaurant where Name='{3}'),(select Code From Setup_Kitchens where Name='{4}' and RestaurantID=(select Code From Setup_Restaurant where Name='{3}')),(select Code From Setup_Kitchens where Name='{5}' and RestaurantID=(select Code From Setup_Restaurant where Name='{3}')),'{6}','{7}',GETDATE(),'{8}'", Manual_transfer_No.Text,Convert.ToDateTime(Transfer_dt.Text).ToString("MM-dd-yyyy") , commenttxt.Text, Resturant.Text, From_Kitchen.Text, To_Kitchen.Text, "Transfer_Kitchen", Statustxt.Text, Total_Price.Text);
                string Where = string.Format("Transfer_Serial={0}", transfer_No.Text);
                Classes.UpdateRow(FiledSlection, Values, Where, "Transfer_Kitchens");
                Classes.LogTable(Classes.MyComm.CommandText.ToString(), transfer_No.Text, "Transfer_Kitchens", "Update");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }           //Done
        private void Edit_TIK_Items()
        {
            try
            {
                DataTable dt = ItemsDGV.DataContext as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    float NetCost = float.Parse(dt.Rows[i]["Qty"].ToString()) * float.Parse(dt.Rows[i][7].ToString());
                    string Values = "'" + dt.Rows[i]["Code"] + "','" + transfer_No.Text + "','" + dt.Rows[i]["Qty"] + "','" + dt.Rows[i]["Unit"] + "','" + i + "','" + dt.Rows[i][7] + "','" + NetCost + "','" + dt.Rows[i][To_Kitchen.Text + " Qty"] + "','" + dt.Rows[i][To_Kitchen.Text + " Unit Cost"] + "','" + dt.Rows[i][From_Kitchen.Text + " Qty"] + "','" + dt.Rows[i][From_Kitchen.Text + " Unit Cost"] + "','" + dt.Rows[i]["Recipe"] + "'";
                    Classes.InsertRow("Transfer_Kitchens_Items", Values);
                    Classes.LogTable(Classes.MyComm.CommandText.ToString(), transfer_No.Text, "Transfer_Kitchens_Items", "Update");
                }
            }
            catch
            {
                MessageBox.Show("Items Input Error");
            }
        }
        private void TransferBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("TransferTrnsferKitchen") == -1 && Authenticated.IndexOf("CheckAllTransferKitchen") == -1)
            {   LogIn logIn = new LogIn();   logIn.ShowDialog();   }
            else
            {
                if (!DoSomeChecks())
                    return;

                try
                {
                    string WhereFiltering = string.Format("Transfer_Serial='{0}'", transfer_No.Text);
                    DataTable TheSerial = Classes.RetrieveData("Transfer_Serial", WhereFiltering, "Transfer_Kitchens");
                    if(TheSerial.Rows.Count ==0)
                    { 
                        Save_TIK_Items();
                        Save_TIK();
                        MessageBox.Show("Transfer saved Sussesfully");
                        MainGrid.IsEnabled = false;
                        TransferBtn.IsEnabled = false;
                        NewBtn.IsEnabled = true;
                    }
                    else
                    {
                        string where = "Transfer_ID = " + transfer_No.Text;
                        Classes.DeleteRows(where, "Transfer_Kitchens_Items");
                        Edit_TKI();
                        Edit_TIK_Items();
                        MessageBox.Show("Transfer Edited Sussesfully");
                        MainGrid.IsEnabled = false;
                        TransferBtn.IsEnabled = false;
                        NewBtn.IsEnabled = true;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
        }      
        private void Row_Changed(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header == "Qty")
            {
                DataTable Dat = ItemsDGV.DataContext as DataTable;
                float to_rest_Qty = 0; float to_rest_Cost = 0; float from_rest_Qty = 0; float from_rest_Cost = 0;
                for (int i = 0; i < Dat.Columns.Count; i++)
                {     Dat.Columns[i].ReadOnly = false;   }
                string ItemCode = (e.Row.Item as DataRowView).Row["Code"].ToString();

                try
                {
                    if ((bool)(e.Row.Item as DataRowView).Row["Recipe"] == true)
                    {
                        DataTable TheValues = Classes.RetriveCostAndQtyRecipes(Resturant.Text, From_Kitchen.Text, ItemCode);
                        try
                        {
                            from_rest_Qty = float.Parse(TheValues.Rows[0][0].ToString());
                            from_rest_Cost = float.Parse(TheValues.Rows[0][1].ToString());
                        }
                        catch
                        {
                            from_rest_Qty = 0;
                            from_rest_Cost = 0;
                        }
                        TheValues = Classes.RetriveCostAndQtyRecipes(Resturant.Text, To_Kitchen.Text, ItemCode);
                        try
                        {
                            to_rest_Qty = float.Parse(TheValues.Rows[0][0].ToString());
                            to_rest_Cost = float.Parse(TheValues.Rows[0][1].ToString());
                        }
                        catch
                        {

                            to_rest_Qty = 0;
                            to_rest_Cost = 0;
                        }

                        Dat.Rows[e.Row.GetIndex()]["Qty"] = (float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][From_Kitchen.Text + " Qty"] = (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][From_Kitchen.Text + " Unit Cost"] = from_rest_Cost.ToString();
                        Dat.Rows[e.Row.GetIndex()][From_Kitchen.Text + " Total Cost"] = (from_rest_Cost * (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text))).ToString();

                        Dat.Rows[e.Row.GetIndex()][To_Kitchen.Text + " Qty"] = (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][To_Kitchen.Text + " Unit Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text)))).ToString();
                        Dat.Rows[e.Row.GetIndex()][To_Kitchen.Text + " Total Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text))) * (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text))).ToString();
                    }
                    else
                    {
                        DataTable TheValues = Classes.RetriveCostAndQty(Resturant.Text, From_Kitchen.Text, ItemCode);
                        try
                        {
                            from_rest_Qty = float.Parse(TheValues.Rows[0][0].ToString());
                            from_rest_Cost = float.Parse(TheValues.Rows[0][1].ToString());
                        }
                        catch
                        {
                            from_rest_Qty = 0;
                            from_rest_Cost = 0;
                        }
                        TheValues = Classes.RetriveCostAndQty(Resturant.Text, To_Kitchen.Text, ItemCode);
                        try
                        {
                            to_rest_Qty = float.Parse(TheValues.Rows[0][0].ToString());
                            to_rest_Cost = float.Parse(TheValues.Rows[0][1].ToString());
                        }
                        catch
                        {

                            to_rest_Qty = 0;
                            to_rest_Cost = 0;
                        }

                        Dat.Rows[e.Row.GetIndex()]["Qty"] = (float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][From_Kitchen.Text + " Qty"] = (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][From_Kitchen.Text + " Unit Cost"] = from_rest_Cost.ToString();
                        Dat.Rows[e.Row.GetIndex()][From_Kitchen.Text + " Total Cost"] = (from_rest_Cost * (from_rest_Qty - float.Parse((e.EditingElement as TextBox).Text))).ToString();

                        Dat.Rows[e.Row.GetIndex()][To_Kitchen.Text + " Qty"] = (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text)).ToString();
                        Dat.Rows[e.Row.GetIndex()][To_Kitchen.Text + " Unit Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text)))).ToString();
                        Dat.Rows[e.Row.GetIndex()][To_Kitchen.Text + " Total Cost"] = (((to_rest_Cost * to_rest_Qty) + (float.Parse((e.EditingElement as TextBox).Text) * from_rest_Cost)) / (to_rest_Qty + (float.Parse((e.EditingElement as TextBox).Text))) * (to_rest_Qty + float.Parse((e.EditingElement as TextBox).Text))).ToString();
                    }
                    try
                    {
                        double totalPrice = 0;
                        for (int i = 0; i < ItemsDGV.Items.Count; i++)
                        {
                            try
                            {
                                totalPrice += (Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[5]) * Convert.ToDouble(((DataRowView)ItemsDGV.Items[i]).Row.ItemArray[7]));
                            }
                            catch
                            {

                            }
                        }
                        NUmberOfItems.Text = (ItemsDGV.Items.Count).ToString();
                        Total_Price.Text = (totalPrice).ToString();
                    }
                    catch { }
                    for (int i = 0; i < Dat.Columns.Count; i++)
                    {
                        Dat.Columns[i].ReadOnly = true;
                    }
                    Dat.Columns["Qty"].ReadOnly = false;
                    ItemsDGV.DataContext = Dat;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    (e.EditingElement as TextBox).Text = "";
                }

                
            }
        }       //Done
        private void Resturant_Clicked(object sender, RoutedEventArgs e)
        {
            All_Resturants resturants = new All_Resturants(this, "Resturant");
            resturants.ShowDialog();
        }       //DOne
        private void From_Kitchen_Clicked(object sender, RoutedEventArgs e)
        {
            All_Kitchens resturants = new All_Kitchens(this, "From_Kitchen", Resturant.Text);
            resturants.ShowDialog();
        }       //Done
        private void To_Kitchen_Clicked(object sender, RoutedEventArgs e)
        {
            All_Kitchens resturants = new All_Kitchens(this, "To_Kitchen", Resturant.Text);
            resturants.ShowDialog();
        }           //Done
        private void ItemsDGV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
            {
                codeTodelete = grid.SelectedIndex;
            }
        }       //Done
        private void RemoveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("RemoveItemTransferKitchen") == -1 && Authenticated.IndexOf("CheckAllTransferKitchen") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                DataTable dt = ItemsDGV.DataContext as DataTable;
                dt.Rows.RemoveAt(codeTodelete);
                ItemsDGV.DataContext = dt;
            }
        }       //Done
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Authenticated.IndexOf("SearchTransferKitchen") == -1 && Authenticated.IndexOf("CheckAllTransferKitchen") == -1)
            {
                LogIn logIn = new LogIn();
                logIn.ShowDialog();
            }
            else
            {
                NewBtn.IsEnabled = false;
                UpdateBtn.IsEnabled = false;
                DeleteBtn.IsEnabled = false;
                SearchBtn.IsEnabled = false;

                All_Purchase_Orders all_Purchase_Orders = new All_Purchase_Orders(this);
                all_Purchase_Orders.ShowDialog();
            }
        }       //Done
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            transfer_No.IsReadOnly = true;
            Manual_transfer_No.IsReadOnly = false;
            Transfer_dt.IsEnabled = true;
            Transfer_Time.IsEnabled = true;
            commenttxt.IsReadOnly = false;
            Resturant.IsReadOnly = true;
            From_Kitchen.IsReadOnly = true;
            To_Kitchen.IsReadOnly = true;
            resturantBtn.IsEnabled = true;
            FromKitchenBtn.IsEnabled = true;
            ToKitchenBtn.IsEnabled = true;
            ItemsDGV.IsReadOnly = false;
            AddItemsBtn.IsEnabled = true;
            RemoveItemBtn.IsEnabled = true;
            SearchBtn.IsEnabled = true;
            NewBtn.IsEnabled = true;
            UndoBtn.IsEnabled = true;
            Statustxt.IsEnabled = false;
            ClearData();
        }               //Done
        private void NeglectWhiteSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }               //Done
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }           //Done

        private void AddRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            AllRecipes TheRecipes = new AllRecipes(this);
            TheRecipes.ShowDialog();
        }
    }
}
