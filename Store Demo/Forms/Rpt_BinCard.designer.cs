namespace Food_Cost.Forms
{
    partial class Rpt_BinCard
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CBMyKitchen = new System.Windows.Forms.CheckBox();
            this.BtnItem = new System.Windows.Forms.Button();
            this.TxtItemCode = new System.Windows.Forms.TextBox();
            this.btnRport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtItemName = new System.Windows.Forms.TextBox();
            this.GrpDateTimeRange = new System.Windows.Forms.GroupBox();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtp_from = new System.Windows.Forms.DateTimePicker();
            this.uC_TVKitchens1 = new Food_Cost.Forms.UC_TVKitchens();
            this.groupBox1.SuspendLayout();
            this.GrpDateTimeRange.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CBMyKitchen);
            this.groupBox1.Controls.Add(this.BtnItem);
            this.groupBox1.Controls.Add(this.TxtItemCode);
            this.groupBox1.Controls.Add(this.btnRport);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TxtItemName);
            this.groupBox1.Controls.Add(this.GrpDateTimeRange);
            this.groupBox1.Location = new System.Drawing.Point(2, -3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(279, 381);
            this.groupBox1.TabIndex = 554;
            this.groupBox1.TabStop = false;
            // 
            // CBMyKitchen
            // 
            this.CBMyKitchen.AutoSize = true;
            this.CBMyKitchen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.CBMyKitchen.Location = new System.Drawing.Point(70, 188);
            this.CBMyKitchen.Name = "CBMyKitchen";
            this.CBMyKitchen.Size = new System.Drawing.Size(122, 20);
            this.CBMyKitchen.TabIndex = 601;
            this.CBMyKitchen.Text = "For My Kitchen";
            this.CBMyKitchen.UseVisualStyleBackColor = true;
            this.CBMyKitchen.CheckedChanged += new System.EventHandler(this.CBMyKitchen_CheckedChanged);
            // 
            // BtnItem
            // 
            this.BtnItem.Location = new System.Drawing.Point(218, 143);
            this.BtnItem.Name = "BtnItem";
            this.BtnItem.Size = new System.Drawing.Size(41, 23);
            this.BtnItem.TabIndex = 600;
            this.BtnItem.UseVisualStyleBackColor = true;
            this.BtnItem.Click += new System.EventHandler(this.BtnItem_Click);
            // 
            // TxtItemCode
            // 
            this.TxtItemCode.Enabled = false;
            this.TxtItemCode.Location = new System.Drawing.Point(45, 144);
            this.TxtItemCode.Name = "TxtItemCode";
            this.TxtItemCode.Size = new System.Drawing.Size(44, 20);
            this.TxtItemCode.TabIndex = 599;
            // 
            // btnRport
            // 
            this.btnRport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRport.Location = new System.Drawing.Point(69, 223);
            this.btnRport.Name = "btnRport";
            this.btnRport.Size = new System.Drawing.Size(123, 49);
            this.btnRport.TabIndex = 555;
            this.btnRport.Tag = "73";
            this.btnRport.Text = "Report";
            this.btnRport.UseVisualStyleBackColor = true;
            this.btnRport.Click += new System.EventHandler(this.btnRport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(10, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 598;
            this.label1.Text = "Item";
            // 
            // TxtItemName
            // 
            this.TxtItemName.Enabled = false;
            this.TxtItemName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtItemName.Location = new System.Drawing.Point(95, 143);
            this.TxtItemName.Name = "TxtItemName";
            this.TxtItemName.Size = new System.Drawing.Size(117, 22);
            this.TxtItemName.TabIndex = 597;
            // 
            // GrpDateTimeRange
            // 
            this.GrpDateTimeRange.Controls.Add(this.lblDateTo);
            this.GrpDateTimeRange.Controls.Add(this.dtp_to);
            this.GrpDateTimeRange.Controls.Add(this.lblDateFrom);
            this.GrpDateTimeRange.Controls.Add(this.dtp_from);
            this.GrpDateTimeRange.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrpDateTimeRange.Location = new System.Drawing.Point(6, 10);
            this.GrpDateTimeRange.Name = "GrpDateTimeRange";
            this.GrpDateTimeRange.Size = new System.Drawing.Size(260, 105);
            this.GrpDateTimeRange.TabIndex = 584;
            this.GrpDateTimeRange.TabStop = false;
            this.GrpDateTimeRange.Tag = "160";
            this.GrpDateTimeRange.Text = "Date Time Range";
            // 
            // lblDateTo
            // 
            this.lblDateTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTo.Location = new System.Drawing.Point(2, 67);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(65, 22);
            this.lblDateTo.TabIndex = 52;
            this.lblDateTo.Text = "To";
            this.lblDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_to
            // 
            this.dtp_to.Checked = false;
            this.dtp_to.CustomFormat = "";
            this.dtp_to.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_to.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_to.Location = new System.Drawing.Point(73, 67);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(166, 22);
            this.dtp_to.TabIndex = 51;
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFrom.Location = new System.Drawing.Point(2, 30);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(65, 22);
            this.lblDateFrom.TabIndex = 49;
            this.lblDateFrom.Text = "From";
            this.lblDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_from
            // 
            this.dtp_from.AllowDrop = true;
            this.dtp_from.Checked = false;
            this.dtp_from.CustomFormat = "";
            this.dtp_from.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_from.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_from.Location = new System.Drawing.Point(73, 30);
            this.dtp_from.Name = "dtp_from";
            this.dtp_from.Size = new System.Drawing.Size(166, 22);
            this.dtp_from.TabIndex = 50;
            // 
            // uC_TVKitchens1
            // 
            this.uC_TVKitchens1.Location = new System.Drawing.Point(287, 7);
            this.uC_TVKitchens1.Name = "uC_TVKitchens1";
            this.uC_TVKitchens1.Size = new System.Drawing.Size(416, 371);
            this.uC_TVKitchens1.TabIndex = 555;
            // 
            // Rpt_BinCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(711, 386);
            this.Controls.Add(this.uC_TVKitchens1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Rpt_BinCard";
            this.Text = "Rpt_BinCard";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GrpDateTimeRange.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox GrpDateTimeRange;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtItemName;
        private System.Windows.Forms.Button BtnItem;
        private System.Windows.Forms.TextBox TxtItemCode;
        internal System.Windows.Forms.Button btnRport;
        private System.Windows.Forms.DateTimePicker dtp_from;
        private UC_TVKitchens uC_TVKitchens1;
        private System.Windows.Forms.CheckBox CBMyKitchen;
    }
}