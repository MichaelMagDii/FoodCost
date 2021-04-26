namespace Food_Cost.Forms
{
    partial class Rpt_ReceiveItems
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GrpDateTimeRange = new System.Windows.Forms.GroupBox();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtp_from = new System.Windows.Forms.DateTimePicker();
            this.ShowBtn = new System.Windows.Forms.Button();
            this.BtnItem = new System.Windows.Forms.Button();
            this.TxtItemCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtItemName = new System.Windows.Forms.TextBox();
            this.UC_TVKitchens2 = new Food_Cost.Forms.UC_TVKitchens();
            this.CBMyKitchen = new System.Windows.Forms.CheckBox();
            this.CBKitchenTransfer = new System.Windows.Forms.CheckBox();
            this.CBPurchase = new System.Windows.Forms.CheckBox();
            this.CBRestaurantTransfer = new System.Windows.Forms.CheckBox();
            this.CBAutoPurchase = new System.Windows.Forms.CheckBox();
            this.GrpDateTimeRange.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpDateTimeRange
            // 
            this.GrpDateTimeRange.Controls.Add(this.lblDateTo);
            this.GrpDateTimeRange.Controls.Add(this.dtp_to);
            this.GrpDateTimeRange.Controls.Add(this.lblDateFrom);
            this.GrpDateTimeRange.Controls.Add(this.dtp_from);
            this.GrpDateTimeRange.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrpDateTimeRange.Location = new System.Drawing.Point(12, 12);
            this.GrpDateTimeRange.Name = "GrpDateTimeRange";
            this.GrpDateTimeRange.Size = new System.Drawing.Size(252, 106);
            this.GrpDateTimeRange.TabIndex = 596;
            this.GrpDateTimeRange.TabStop = false;
            this.GrpDateTimeRange.Tag = "160";
            this.GrpDateTimeRange.Text = "Date Time Range";
            // 
            // lblDateTo
            // 
            this.lblDateTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTo.Location = new System.Drawing.Point(5, 67);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(65, 22);
            this.lblDateTo.TabIndex = 52;
            this.lblDateTo.Text = "To";
            this.lblDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_to
            // 
            this.dtp_to.CustomFormat = "dd/MM/yyyy";
            this.dtp_to.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_to.Location = new System.Drawing.Point(76, 67);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(166, 22);
            this.dtp_to.TabIndex = 51;
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFrom.Location = new System.Drawing.Point(5, 30);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(65, 22);
            this.lblDateFrom.TabIndex = 49;
            this.lblDateFrom.Text = "From";
            this.lblDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_from
            // 
            this.dtp_from.CustomFormat = "dd/MM/yyyy";
            this.dtp_from.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_from.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_from.Location = new System.Drawing.Point(76, 30);
            this.dtp_from.Name = "dtp_from";
            this.dtp_from.Size = new System.Drawing.Size(166, 22);
            this.dtp_from.TabIndex = 50;
            // 
            // ShowBtn
            // 
            this.ShowBtn.Location = new System.Drawing.Point(88, 330);
            this.ShowBtn.Name = "ShowBtn";
            this.ShowBtn.Size = new System.Drawing.Size(107, 54);
            this.ShowBtn.TabIndex = 595;
            this.ShowBtn.Text = "Show";
            this.ShowBtn.UseVisualStyleBackColor = true;
            this.ShowBtn.Click += new System.EventHandler(this.ShowBtn_Click);
            // 
            // BtnItem
            // 
            this.BtnItem.Location = new System.Drawing.Point(223, 135);
            this.BtnItem.Name = "BtnItem";
            this.BtnItem.Size = new System.Drawing.Size(41, 23);
            this.BtnItem.TabIndex = 615;
            this.BtnItem.UseVisualStyleBackColor = true;
            this.BtnItem.Click += new System.EventHandler(this.BtnItem_Click);
            // 
            // TxtItemCode
            // 
            this.TxtItemCode.Enabled = false;
            this.TxtItemCode.Location = new System.Drawing.Point(50, 136);
            this.TxtItemCode.Name = "TxtItemCode";
            this.TxtItemCode.Size = new System.Drawing.Size(44, 20);
            this.TxtItemCode.TabIndex = 614;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(15, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 613;
            this.label1.Text = "Item";
            // 
            // TxtItemName
            // 
            this.TxtItemName.Enabled = false;
            this.TxtItemName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtItemName.Location = new System.Drawing.Point(100, 135);
            this.TxtItemName.Name = "TxtItemName";
            this.TxtItemName.Size = new System.Drawing.Size(117, 22);
            this.TxtItemName.TabIndex = 612;
            // 
            // UC_TVKitchens2
            // 
            this.UC_TVKitchens2.Location = new System.Drawing.Point(270, 12);
            this.UC_TVKitchens2.Name = "UC_TVKitchens2";
            this.UC_TVKitchens2.Size = new System.Drawing.Size(338, 441);
            this.UC_TVKitchens2.TabIndex = 617;
            // 
            // CBMyKitchen
            // 
            this.CBMyKitchen.AutoSize = true;
            this.CBMyKitchen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.CBMyKitchen.Location = new System.Drawing.Point(73, 304);
            this.CBMyKitchen.Name = "CBMyKitchen";
            this.CBMyKitchen.Size = new System.Drawing.Size(122, 20);
            this.CBMyKitchen.TabIndex = 616;
            this.CBMyKitchen.Text = "For My Kitchen";
            this.CBMyKitchen.UseVisualStyleBackColor = true;
            // 
            // CBKitchenTransfer
            // 
            this.CBKitchenTransfer.AutoSize = true;
            this.CBKitchenTransfer.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.CBKitchenTransfer.Location = new System.Drawing.Point(12, 260);
            this.CBKitchenTransfer.Name = "CBKitchenTransfer";
            this.CBKitchenTransfer.Size = new System.Drawing.Size(130, 20);
            this.CBKitchenTransfer.TabIndex = 618;
            this.CBKitchenTransfer.Text = "Kitchen Transfer";
            this.CBKitchenTransfer.UseVisualStyleBackColor = true;
            // 
            // CBPurchase
            // 
            this.CBPurchase.AutoSize = true;
            this.CBPurchase.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.CBPurchase.Location = new System.Drawing.Point(12, 179);
            this.CBPurchase.Name = "CBPurchase";
            this.CBPurchase.Size = new System.Drawing.Size(86, 20);
            this.CBPurchase.TabIndex = 619;
            this.CBPurchase.Text = "Purchase";
            this.CBPurchase.UseVisualStyleBackColor = true;
            // 
            // CBRestaurantTransfer
            // 
            this.CBRestaurantTransfer.AutoSize = true;
            this.CBRestaurantTransfer.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.CBRestaurantTransfer.Location = new System.Drawing.Point(12, 234);
            this.CBRestaurantTransfer.Name = "CBRestaurantTransfer";
            this.CBRestaurantTransfer.Size = new System.Drawing.Size(150, 20);
            this.CBRestaurantTransfer.TabIndex = 620;
            this.CBRestaurantTransfer.Text = "Restaurant Transfer";
            this.CBRestaurantTransfer.UseVisualStyleBackColor = true;
            // 
            // CBAutoPurchase
            // 
            this.CBAutoPurchase.AutoSize = true;
            this.CBAutoPurchase.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.CBAutoPurchase.Location = new System.Drawing.Point(12, 208);
            this.CBAutoPurchase.Name = "CBAutoPurchase";
            this.CBAutoPurchase.Size = new System.Drawing.Size(119, 20);
            this.CBAutoPurchase.TabIndex = 621;
            this.CBAutoPurchase.Text = "Auto Purchase";
            this.CBAutoPurchase.UseVisualStyleBackColor = true;
            // 
            // Rpt_ReceiveItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 456);
            this.Controls.Add(this.CBAutoPurchase);
            this.Controls.Add(this.CBRestaurantTransfer);
            this.Controls.Add(this.CBPurchase);
            this.Controls.Add(this.CBKitchenTransfer);
            this.Controls.Add(this.UC_TVKitchens2);
            this.Controls.Add(this.CBMyKitchen);
            this.Controls.Add(this.BtnItem);
            this.Controls.Add(this.TxtItemCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtItemName);
            this.Controls.Add(this.GrpDateTimeRange);
            this.Controls.Add(this.ShowBtn);
            this.Name = "Rpt_ReceiveItems";
            this.Text = "Rpt_ReceiveItems";
            this.GrpDateTimeRange.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox GrpDateTimeRange;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtp_from;
        private System.Windows.Forms.Button ShowBtn;
        private System.Windows.Forms.Button BtnItem;
        private System.Windows.Forms.TextBox TxtItemCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtItemName;
        private UC_TVKitchens UC_TVKitchens2;
        private System.Windows.Forms.CheckBox CBMyKitchen;
        private System.Windows.Forms.CheckBox CBKitchenTransfer;
        private System.Windows.Forms.CheckBox CBPurchase;
        private System.Windows.Forms.CheckBox CBRestaurantTransfer;
        private System.Windows.Forms.CheckBox CBAutoPurchase;
    }
}