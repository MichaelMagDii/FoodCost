using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Food_Cost.Forms;
using Food_Cost.Report;

namespace Food_Cost.Forms
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void show_btn_Click(object sender, EventArgs e)
        {
            Rpt_PurchaseOrder Frm = new Rpt_PurchaseOrder();
            Frm.Show();
           
        }

        private void PItemsBtn_Click(object sender, EventArgs e)
        {
            Rpt_PurchaseItems Frm = new Rpt_PurchaseItems();
            Frm.Show();
        }

        private void Recipes_items_Click(object sender, EventArgs e)
        {
            Rpt_RecipesItemsView Rec = new Rpt_RecipesItemsView();
            Rec.Show();
        }

        private void BtnStores_Click(object sender, EventArgs e)
        {
            Rpt_StoresItems Frm = new Rpt_StoresItems();
            Frm.Show();
        }

       
        private void BtnBincard_Click(object sender, EventArgs e)
        {
            Rpt_BinCard Frm = new Rpt_BinCard();
            Frm.Show();
        }

        private void BtnRecItem_Click(object sender, EventArgs e)
        {
            Rpt_ReceiveItems Frm = new Rpt_ReceiveItems();
            Frm.Show();
        }

        private void BntRecOrder_Click(object sender, EventArgs e)
        {
            Rpt_ReceiveOrder Frm = new Rpt_ReceiveOrder();
            Frm.Show();
        }

        private void Btn_InvStats_Click(object sender, EventArgs e)
        {
            Rpt_InventoryStats Frm = new Rpt_InventoryStats();
            Frm.Show();
        }

        private void BtnKitchenTrans_Click(object sender, EventArgs e)
        {
            Rpt_TransferItems Frm = new Rpt_TransferItems();
            Frm.Show();
        }

        private void BtnInKitchenTrans_Click(object sender, EventArgs e)
        {
            Rpt_InterKitchenTransfer Frm = new Rpt_InterKitchenTransfer();
            Frm.Show();
        }

        private void BtnVendors_Click(object sender, EventArgs e)
        {
            Rpt_Vendors Frm = new Rpt_Vendors();

            Frm.Show();
        }

        private void BtnEndMonth_Click(object sender, EventArgs e)
        {
            Rpt_MonthClosing Frm = new Rpt_MonthClosing();
            Frm.Show();
        }

        private void BtnGenRecipes_Click(object sender, EventArgs e)
        {
            Rpt_GeneratedRecipescs Frm = new Rpt_GeneratedRecipescs();
            Frm.Show();
        }
    }
}
