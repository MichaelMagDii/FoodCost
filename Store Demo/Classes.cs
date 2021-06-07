using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace Food_Cost
{
    public class Classes
    {
        public static string TheLanguage = "";
        public static string RestaurantId, KitchenId, WorkstationId;
        public static string WS;
        public static string IDs;
        private static DataTable MyTable;
        private static SqlDataAdapter MyAdapt;
        public static SqlConnection MyConnection;
        public static SqlCommand MyComm;
        public static string DataConnString;
        public static string sysDateFormat;
        public static string sysDateTimeFormat;

        public static void Language()
        {
            try
            {
                SqlConnection con = new SqlConnection(DataConnString);
                con.Open();
                string s = "select Language from Setup_Code ";
                SqlCommand cmd = new SqlCommand(s, con);
                if (cmd.ExecuteScalar().ToString() == "Arabic")
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ar-EG");
                    //CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;
                }
                else
                { System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US"); }
                TheLanguage = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch { }
        }

        public static void TheConnectionString()
        {
            try
            {
                string connString = System.IO.File.ReadAllText("MyCon.txt");

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["Food_Cost.Properties.Settings.FoodCostDB"].ConnectionString = connString;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                DataConnString = Properties.Settings.Default.FoodCostDB.ToString();
                SqlConnection con = new SqlConnection(connString);
                con.Open();
                con.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Of Connectionstring");
                System.Windows.Application.Current.Shutdown();
            }
        }

        public static void UpdateDateFormat()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }

        public static void GetDateFormate()
        {
            try
            {
                sysDateFormat = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            }
            catch { MessageBox.Show("Get Date Formate Error!"); }
        }
        public static void GetDateTimeFormate()
        {
            try
            {
                string TimeFormat = CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern;
                sysDateTimeFormat = Classes.sysDateFormat + " " + TimeFormat;
            }
            catch { MessageBox.Show("Get Date and Time Formate Error!"); }
        }

        public static void GetWS()
        {
            try
            {
                string WorkStationData = File.ReadAllText("Workstation.dll");
                string[] splitArr = WorkStationData.Split(',');
                WorkstationId = splitArr[0];
                RestaurantId = splitArr[1];
                KitchenId = splitArr[2];
                IDs = RestaurantId + KitchenId + WorkstationId;
                WS = WorkstationId;
            }
            catch { MessageBox.Show("Get Work Station Error"); }
        }

        public static DataTable RetrieveData(string FieldSelected, string TableName)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyAdapt = new SqlDataAdapter("select " + FieldSelected + " from " + TableName, MyConnection);
            MyTable = new DataTable();
            MyAdapt.Fill(MyTable);
            return MyTable;
        }

        public static DataTable RetrieveData(string SqlStr)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyAdapt = new SqlDataAdapter(SqlStr, MyConnection);
            MyTable = new DataTable();
            MyAdapt.Fill(MyTable);
            return MyTable;
        }

        public static DataTable RetrieveData(string FieldSelected, string WhereFiltering, string TableName)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyAdapt = new SqlDataAdapter("select " + FieldSelected + " from " + TableName + " where " + WhereFiltering, MyConnection);
            MyTable = new DataTable();
            MyAdapt.Fill(MyTable);
            return MyTable;
        }

        public static void UpdateCell(string FieldSelected, string Value, string TableName)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();
            MyComm = new SqlCommand("Update " + TableName + " set " + FieldSelected + " = " + Value, MyConnection);
            MyComm.ExecuteNonQuery();
            MyConnection.Close();
        }
        public static void UpdateCell(string FieldSelected, string Value, string WhereFiltering, string TableName)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();
            MyComm = new SqlCommand("Update " + TableName + " set " + FieldSelected + " = '" + Value + "' where " + WhereFiltering, MyConnection);
            MyComm.ExecuteNonQuery();
            MyConnection.Close();
        }

        public static void UpdateRow(string FieldSelected, string Value, string WhereFiltering, string TableName)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();
            string[] FS = FieldSelected.Split(',');
            string[] V = Value.Split(',');
            string FSV = "";
            string comma = "";
            for (int i =0; i< FS.Length; i++)
            {
                FSV += comma + FS[i] + " = " + V[i];
                comma = ",";
            }
            MyComm = new SqlCommand("Update " + TableName + " set " + FSV + " where " + WhereFiltering, MyConnection);
            MyComm.ExecuteNonQuery();
            MyConnection.Close();
        }

        public static void InsertRow(string TableName, string FieldSelected, string values)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();
            MyComm = new SqlCommand("insert " + TableName + "(" + FieldSelected + ")" + " values (" + values + ")", MyConnection);
            MyComm.ExecuteNonQuery();
            MyConnection.Close();
        }

        public static void InsertRows(string TableName, string FieldSelected, string values)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();
            MyComm = new SqlCommand("insert " + TableName + "(" + FieldSelected + ")" + " values " + values + "", MyConnection);
            MyComm.ExecuteNonQuery();
        }


        public static void InsertRow(string TableName, string values)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();
            MyComm = new SqlCommand("insert into " + TableName + " values (" + values + ")", MyConnection);
            MyComm.ExecuteNonQuery();
            MyConnection.Close();
        }

        public static DataTable RetrieveStored(string name)
        {
            MyTable = new DataTable();
            MyConnection = new SqlConnection(DataConnString);
            MyComm = new SqlCommand(name, MyConnection);
            MyComm.CommandType = CommandType.StoredProcedure;
            MyAdapt = new SqlDataAdapter(MyComm);
            MyAdapt.Fill(MyTable);
            return MyTable;

        }

        public static DataTable RetrieveStoredWithParamaeters(string name, string parameter, string values)
        {
            MyTable = new DataTable();
            MyConnection = new SqlConnection(DataConnString);
            MyComm = new SqlCommand(name, MyConnection);
            MyComm.CommandType = CommandType.StoredProcedure;
            string[] P = parameter.Split(',');
            string[] V = values.Split(',');
            for (int i = 0; i < P.Length; i++)
            {
                MyComm.Parameters.AddWithValue(P[i], Convert.ToDateTime(V[i]).ToString(Classes.sysDateTimeFormat));
            }
            MyAdapt = new SqlDataAdapter(MyComm);
            MyAdapt.Fill(MyTable);
            return MyTable;

        }

        public static void CreateTable(string TableName, string Coulmns)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();

            string SQLCOMMAND = "IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = '" + TableName + "' ) " +
                "CREATE TABLE[dbo]." + TableName + "( " + Coulmns + " ); ";

            MyComm = new SqlCommand(SQLCOMMAND, MyConnection);
            MyComm.ExecuteNonQuery();
        }

        public static void DeleteTable(string TableName)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();

            string SQLCOMMAND = "IF EXISTS(SELECT * FROM sysobjects WHERE name = '" + TableName + "' ) " +
                "DELETE " + TableName;

            MyComm = new SqlCommand(SQLCOMMAND, MyConnection);
            MyComm.ExecuteNonQuery();
        }

        public static void DeleteRows(string WhereFiltering, string TableName)
        {
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();

            string SQLCOMMAND = "Delete from " + TableName + " where " + WhereFiltering;

            MyComm = new SqlCommand(SQLCOMMAND, MyConnection);
            MyComm.ExecuteNonQuery();
        }

        public static TreeView LoadStores(TreeView TVKitchens)
        {
            DataTable DT = Classes.RetrieveData("Code,Name", "Setup_Restaurant");
            foreach (DataRow DR in DT.Rows)
            {
                TVKitchens.Nodes.Add(DR["Code"].ToString(), DR["Name"].ToString());
            }

            DT = Classes.RetrieveData("Code,Name,RestaurantID", "Setup_Kitchens order by RestaurantID,Code");
            foreach (DataRow DR in DT.Rows)
            {
                TVKitchens.Nodes[DR["RestaurantID"].ToString()].Nodes.Add(DR["Code"].ToString(), DR["Name"].ToString());
            }
            return TVKitchens;
        }

        public static TreeView LoadDates(TreeView TVDates)
        {
            DataTable DT = Classes.RetrieveData("*", "isClosed = 'True'", "Setup_Fiscal_Period");

            foreach (DataRow DR in DT.Rows)
            {
                if (!TVDates.Nodes.ContainsKey(DR["Year"].ToString()))
                {
                    TVDates.Nodes.Add(DR["Year"].ToString(), DR["Year"].ToString());

                }
                TVDates.Nodes[DR["Year"].ToString()].Nodes.Add("Month" + DR["Month"].ToString());
            }
            return TVDates;
        }

        public static string InCrementTransactionSerial(string TableName,string TransColumName)
        {
            string Val = "";
            SqlConnection con = new SqlConnection(Classes.DataConnString);
            try
            {
                con.Open();
                string s = string.Format("select Top(1) {1} from {0} where {1} like '{2}%' order by {1} desc", TableName, TransColumName, Classes.IDs);
                SqlCommand cmd = new SqlCommand(s, con);
                if (cmd.ExecuteScalar() == null)
                {
                    Val = Classes.IDs + "0000001";
                }
                else
                {
                    Val= "0" + (Int64.Parse(cmd.ExecuteScalar().ToString()) + 1).ToString();
                }
            }
            catch
            {
                con.Close();
            }
            return Val;
        }
        public static DataTable RetriveCostAndQtyRecipes(string Restaurant, string Kitchen, string Item)
        {
            MyConnection = new SqlConnection(DataConnString);
            string s = string.Format("select Qty,Price From RecipeQty Where Resturant_ID=(select code from Setup_Restaurant where Name = '{1}') and Kitchen_ID=(select Code from Setup_Kitchens where Name='{2}' and RestaurantID= (select code from Setup_Restaurant where Name = '{1}')) and Recipe_ID='{0}'", Item, Restaurant, Kitchen);
            MyAdapt = new SqlDataAdapter(s, MyConnection);
            MyTable = new DataTable();
            MyAdapt.Fill(MyTable);
            return MyTable;
        }
        public static DataTable RetriveCostAndQty(string Restaurant,string Kitchen,string Item)
        {
            MyConnection = new SqlConnection(DataConnString);
            string s = string.Format("select Qty,Current_Cost From Items Where RestaurantID=(select code from Setup_Restaurant where Name = '{1}') and KitchenID=(select Code from Setup_Kitchens where Name='{2}' and RestaurantID= (select code from Setup_Restaurant where Name = '{1}')) and ItemID='{0}'", Item, Restaurant, Kitchen);
            MyAdapt = new SqlDataAdapter(s, MyConnection);
            MyTable = new DataTable();
            MyAdapt.Fill(MyTable);
            return MyTable;
        }

        public static string RetrieveRestaurantCode(string RestaurantName)
        {
            string RestaurantCode = "";
            try
            {
                MyConnection = new SqlConnection(DataConnString);
                MyConnection.Open();
                string s = string.Format("select Code from Setup_Restaurant where Name = '{0}'", RestaurantName);
                SqlCommand cmd = new SqlCommand(s, MyConnection);
                RestaurantCode = cmd.ExecuteScalar().ToString();
            }
            catch { }
            return RestaurantCode;
        }


        public static string RetrieveKitchenCode(string KitchenName,string RestaurantName)
        {
            string KitchenCode = "";
            try
            {
                MyConnection = new SqlConnection(DataConnString);
                MyConnection.Open();
                if (RestaurantName == "")
                {
                    string s = string.Format("select Code from Setup_Kitchens where Name='{0}'", KitchenName);
                    SqlCommand cmd = new SqlCommand(s, MyConnection);
                    KitchenCode = cmd.ExecuteScalar().ToString();
                }
                else
                {
                    string s = string.Format("select Code from Setup_Kitchens where Name='{0}' and RestaurantID=(select Code from Setup_Restaurant where Name='{1}')", KitchenName, RestaurantName);
                    SqlCommand cmd = new SqlCommand(s, MyConnection);
                    KitchenCode = cmd.ExecuteScalar().ToString();
                }
            }
            catch { }
            return KitchenCode;
        }

        public static DataTable RetrieveResturants()
        {
            DataTable Restaurants = new DataTable();
            Restaurants.Columns.Add("Resturants");
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();
            string s = string.Format("select Name from Setup_Restaurant where IsActive='True'");
            SqlCommand cmd = new SqlCommand(s, MyConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Restaurants.Rows.Add(reader["Name"].ToString());
            }
            reader.Close();
            return Restaurants;
        }
        public static DataTable RetrieveKitchens(string ResturantName)
        {
            DataTable Kitchens = new DataTable();
            Kitchens.Columns.Add("Kitchens");
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();
            string s = string.Format("select Name from Setup_Kitchens where IsActive='True' and RestaurantID=(select Code from Setup_Restaurant where Name='{0}')", ResturantName);
            SqlCommand cmd = new SqlCommand(s, MyConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Kitchens.Rows.Add(reader["Name"].ToString());
            }
            reader.Close();
            return Kitchens;
        }

        public static void RetrieveTheYearAndMonth()
        {
            MyConnection = new SqlConnection(DataConnString);
            MyConnection.Open();
            string s = string.Format("select top(1) Year,Month from Setup_Fiscal_Period where isCLosed='False' ORDER BY [From]");
            MyAdapt = new SqlDataAdapter(s, MyConnection);
            MyTable = new DataTable();
            MyAdapt.Fill(MyTable);
            MainWindow.CurrentYear = MyTable.Rows[0][0].ToString();
            MainWindow.CurrentMonth = MyTable.Rows[0][1].ToString();
            MainWindow.MonthQty = "Month" + MainWindow.CurrentMonth + "_Qty";
            MainWindow.MonthCost = "Month" + MainWindow.CurrentMonth + "_Cost";
        }

        public static string AddTheOldStringQuotes(string TheCommand)
        {
            bool CHeck = false;
            for(int i=0;i<TheCommand.Length;i++)
            {
                if(TheCommand[i].ToString() == "'" && CHeck == false)
                {
                    TheCommand = TheCommand.Insert(i + 1, "'");
                    i+=1;
                }
                else if(TheCommand[i].ToString() == "'" )
                {
                    TheCommand = TheCommand.Insert(i + 1, "+'");
                    i += 2;
                    CHeck = false;
                }
                if(TheCommand[i].ToString() =="N" && TheCommand[i+1].ToString() =="'")
                {
                    TheCommand = TheCommand.Insert(i , "'+");
                    CHeck = true;
                    i += 3;
                }
            }
            return TheCommand;
        }

        public static void LogTable(string TheTransaction,string TransactionID, string TranactionTable,string ActionType)
        {
            string TheOldTransaction = "";
            string Where = string.Format("Transactions='{0}' and TransactionTable='{1}' and Restaurant='{2}' and Kitchen='{3}' and WS='{4}'",TransactionID, TranactionTable, RestaurantId, KitchenId, WS);
            DataTable TheLog = RetrieveData("New_Action", Where, "Log_Table");
            if (TheLog.Rows.Count != 0)
            {
                TheOldTransaction = TheLog.Rows[TheLog.Rows.Count-1][0].ToString();
                TheOldTransaction = AddTheOldStringQuotes(TheOldTransaction);
            }
            else
                TheOldTransaction = "";

            TheTransaction = AddTheOldStringQuotes(TheTransaction);
            string FiledSelection = "UserID,Transactions,TransactionTable,ActionType,Old_Action,New_Action,Action_DateTime,Restaurant,Kitchen,WS";
            string ValuesToInert = string.Format("'{0}','{1}','{2}','{3}','{4}','{5}',GETDATE(),'{6}','{7}','{8}'", MainWindow.UserID, TransactionID, TranactionTable, ActionType, TheOldTransaction, TheTransaction, RestaurantId, KitchenId, WS);
            InsertRow("Log_Table", FiledSelection, ValuesToInert);
        }
    }
}
