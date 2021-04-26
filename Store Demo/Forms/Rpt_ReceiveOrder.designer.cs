namespace Food_Cost.Forms
{
    partial class Rpt_ReceiveOrder
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
            this.uC_TVKitchens1 = new Food_Cost.Forms.UC_TVKitchens();
            this.CBMyKitchen = new System.Windows.Forms.CheckBox();
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
            this.GrpDateTimeRange.TabIndex = 600;
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
            this.ShowBtn.Location = new System.Drawing.Point(71, 208);
            this.ShowBtn.Name = "ShowBtn";
            this.ShowBtn.Size = new System.Drawing.Size(107, 54);
            this.ShowBtn.TabIndex = 599;
            this.ShowBtn.Text = "Show";
            this.ShowBtn.UseVisualStyleBackColor = true;
            this.ShowBtn.Click += new System.EventHandler(this.ShowBtn_Click);
            // 
            // uC_TVKitchens1
            // 
            this.uC_TVKitchens1.Location = new System.Drawing.Point(270, 12);
            this.uC_TVKitchens1.Name = "uC_TVKitchens1";
            this.uC_TVKitchens1.Size = new System.Drawing.Size(416, 371);
            this.uC_TVKitchens1.TabIndex = 603;
            // 
            // CBMyKitchen
            // 
            this.CBMyKitchen.AutoSize = true;
            this.CBMyKitchen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.CBMyKitchen.Location = new System.Drawing.Point(71, 159);
            this.CBMyKitchen.Name = "CBMyKitchen";
            this.CBMyKitchen.Size = new System.Drawing.Size(122, 20);
            this.CBMyKitchen.TabIndex = 604;
            this.CBMyKitchen.Text = "For My Kitchen";
            this.CBMyKitchen.UseVisualStyleBackColor = true;
            // 
            // Rpt_ReceiveOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 388);
            this.Controls.Add(this.CBMyKitchen);
            this.Controls.Add(this.uC_TVKitchens1);
            this.Controls.Add(this.GrpDateTimeRange);
            this.Controls.Add(this.ShowBtn);
            this.Name = "Rpt_ReceiveOrder";
            this.Text = "Rpt_ReceiveOrder";
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
        private UC_TVKitchens uC_TVKitchens1;
        private System.Windows.Forms.CheckBox CBMyKitchen;
    }
}