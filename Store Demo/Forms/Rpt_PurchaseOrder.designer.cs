namespace Food_Cost.Forms
{
    partial class Rpt_PurchaseOrder
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
            this.BtnDeliveryDate = new System.Windows.Forms.RadioButton();
            this.BtnCreateDate = new System.Windows.Forms.RadioButton();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtp_to = new System.Windows.Forms.DateTimePicker();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtp_from = new System.Windows.Forms.DateTimePicker();
            this.show_btn = new System.Windows.Forms.Button();
            this.UC_TVKitchens2 = new Food_Cost.Forms.UC_TVKitchens();
            this.CBMyKitchen = new System.Windows.Forms.CheckBox();
            this.GrpDateTimeRange.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpDateTimeRange
            // 
            this.GrpDateTimeRange.Controls.Add(this.BtnDeliveryDate);
            this.GrpDateTimeRange.Controls.Add(this.BtnCreateDate);
            this.GrpDateTimeRange.Controls.Add(this.lblDateTo);
            this.GrpDateTimeRange.Controls.Add(this.dtp_to);
            this.GrpDateTimeRange.Controls.Add(this.lblDateFrom);
            this.GrpDateTimeRange.Controls.Add(this.dtp_from);
            this.GrpDateTimeRange.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrpDateTimeRange.Location = new System.Drawing.Point(12, 12);
            this.GrpDateTimeRange.Name = "GrpDateTimeRange";
            this.GrpDateTimeRange.Size = new System.Drawing.Size(282, 135);
            this.GrpDateTimeRange.TabIndex = 587;
            this.GrpDateTimeRange.TabStop = false;
            this.GrpDateTimeRange.Tag = "160";
            this.GrpDateTimeRange.Text = "Date Time Range";
            // 
            // BtnDeliveryDate
            // 
            this.BtnDeliveryDate.AutoSize = true;
            this.BtnDeliveryDate.Location = new System.Drawing.Point(132, 23);
            this.BtnDeliveryDate.Name = "BtnDeliveryDate";
            this.BtnDeliveryDate.Size = new System.Drawing.Size(111, 20);
            this.BtnDeliveryDate.TabIndex = 54;
            this.BtnDeliveryDate.TabStop = true;
            this.BtnDeliveryDate.Text = "Delivery Date";
            this.BtnDeliveryDate.UseVisualStyleBackColor = true;
            this.BtnDeliveryDate.CheckedChanged += new System.EventHandler(this.BtnDeliveryDate_CheckedChanged);
            // 
            // BtnCreateDate
            // 
            this.BtnCreateDate.AutoSize = true;
            this.BtnCreateDate.Location = new System.Drawing.Point(19, 23);
            this.BtnCreateDate.Name = "BtnCreateDate";
            this.BtnCreateDate.Size = new System.Drawing.Size(101, 20);
            this.BtnCreateDate.TabIndex = 53;
            this.BtnCreateDate.TabStop = true;
            this.BtnCreateDate.Text = "Create Date";
            this.BtnCreateDate.UseVisualStyleBackColor = true;
            this.BtnCreateDate.CheckedChanged += new System.EventHandler(this.BtnCreateDate_CheckedChanged);
            // 
            // lblDateTo
            // 
            this.lblDateTo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTo.Location = new System.Drawing.Point(2, 96);
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
            this.dtp_to.Location = new System.Drawing.Point(73, 96);
            this.dtp_to.Name = "dtp_to";
            this.dtp_to.Size = new System.Drawing.Size(166, 22);
            this.dtp_to.TabIndex = 51;
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFrom.Location = new System.Drawing.Point(2, 59);
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
            this.dtp_from.Location = new System.Drawing.Point(73, 59);
            this.dtp_from.Name = "dtp_from";
            this.dtp_from.Size = new System.Drawing.Size(166, 22);
            this.dtp_from.TabIndex = 50;
            // 
            // show_btn
            // 
            this.show_btn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.show_btn.Location = new System.Drawing.Point(85, 221);
            this.show_btn.Name = "show_btn";
            this.show_btn.Size = new System.Drawing.Size(95, 38);
            this.show_btn.TabIndex = 586;
            this.show_btn.Text = "Show";
            this.show_btn.UseVisualStyleBackColor = true;
            this.show_btn.Click += new System.EventHandler(this.show_btn_Click);
            // 
            // UC_TVKitchens2
            // 
            this.UC_TVKitchens2.Location = new System.Drawing.Point(313, 12);
            this.UC_TVKitchens2.Name = "UC_TVKitchens2";
            this.UC_TVKitchens2.Size = new System.Drawing.Size(306, 345);
            this.UC_TVKitchens2.TabIndex = 595;
            // 
            // CBMyKitchen
            // 
            this.CBMyKitchen.AutoSize = true;
            this.CBMyKitchen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.CBMyKitchen.Location = new System.Drawing.Point(74, 180);
            this.CBMyKitchen.Name = "CBMyKitchen";
            this.CBMyKitchen.Size = new System.Drawing.Size(122, 20);
            this.CBMyKitchen.TabIndex = 594;
            this.CBMyKitchen.Text = "For My Kitchen";
            this.CBMyKitchen.UseVisualStyleBackColor = true;
            // 
            // Rpt_PurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 375);
            this.Controls.Add(this.UC_TVKitchens2);
            this.Controls.Add(this.CBMyKitchen);
            this.Controls.Add(this.GrpDateTimeRange);
            this.Controls.Add(this.show_btn);
            this.Name = "Rpt_PurchaseOrder";
            this.Text = "Rpt_PurchaseOrder";
            this.GrpDateTimeRange.ResumeLayout(false);
            this.GrpDateTimeRange.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpDateTimeRange;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtp_to;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtp_from;
        private System.Windows.Forms.Button show_btn;
        private System.Windows.Forms.RadioButton BtnDeliveryDate;
        private System.Windows.Forms.RadioButton BtnCreateDate;
        private UC_TVKitchens UC_TVKitchens2;
        private System.Windows.Forms.CheckBox CBMyKitchen;
    }
}