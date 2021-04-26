namespace Food_Cost.Forms
{
    partial class Rpt_InventoryStats
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
            this.show_btn = new System.Windows.Forms.Button();
            this.BtnItem = new System.Windows.Forms.Button();
            this.TxtItemCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtItemName = new System.Windows.Forms.TextBox();
            this.uC_TVKitchens1 = new Food_Cost.Forms.UC_TVKitchens();
            this.CBMyKitchen = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // show_btn
            // 
            this.show_btn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.show_btn.Location = new System.Drawing.Point(313, 427);
            this.show_btn.Name = "show_btn";
            this.show_btn.Size = new System.Drawing.Size(122, 59);
            this.show_btn.TabIndex = 605;
            this.show_btn.Text = "Show";
            this.show_btn.UseVisualStyleBackColor = true;
            this.show_btn.Click += new System.EventHandler(this.show_btn_Click);
            // 
            // BtnItem
            // 
            this.BtnItem.Location = new System.Drawing.Point(226, 444);
            this.BtnItem.Name = "BtnItem";
            this.BtnItem.Size = new System.Drawing.Size(41, 23);
            this.BtnItem.TabIndex = 611;
            this.BtnItem.UseVisualStyleBackColor = true;
            this.BtnItem.Click += new System.EventHandler(this.BtnItem_Click);
            // 
            // TxtItemCode
            // 
            this.TxtItemCode.Enabled = false;
            this.TxtItemCode.Location = new System.Drawing.Point(53, 445);
            this.TxtItemCode.Name = "TxtItemCode";
            this.TxtItemCode.Size = new System.Drawing.Size(44, 20);
            this.TxtItemCode.TabIndex = 610;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(18, 448);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 609;
            this.label1.Text = "Item";
            // 
            // TxtItemName
            // 
            this.TxtItemName.Enabled = false;
            this.TxtItemName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtItemName.Location = new System.Drawing.Point(103, 444);
            this.TxtItemName.Name = "TxtItemName";
            this.TxtItemName.Size = new System.Drawing.Size(117, 22);
            this.TxtItemName.TabIndex = 608;
            // 
            // uC_TVKitchens1
            // 
            this.uC_TVKitchens1.Location = new System.Drawing.Point(21, 12);
            this.uC_TVKitchens1.Name = "uC_TVKitchens1";
            this.uC_TVKitchens1.Size = new System.Drawing.Size(416, 371);
            this.uC_TVKitchens1.TabIndex = 612;
            // 
            // CBMyKitchen
            // 
            this.CBMyKitchen.AutoSize = true;
            this.CBMyKitchen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.CBMyKitchen.Location = new System.Drawing.Point(167, 389);
            this.CBMyKitchen.Name = "CBMyKitchen";
            this.CBMyKitchen.Size = new System.Drawing.Size(122, 20);
            this.CBMyKitchen.TabIndex = 613;
            this.CBMyKitchen.Text = "For My Kitchen";
            this.CBMyKitchen.UseVisualStyleBackColor = true;
            // 
            // Rpt_InventoryStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 504);
            this.Controls.Add(this.CBMyKitchen);
            this.Controls.Add(this.uC_TVKitchens1);
            this.Controls.Add(this.BtnItem);
            this.Controls.Add(this.TxtItemCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtItemName);
            this.Controls.Add(this.show_btn);
            this.Name = "Rpt_InventoryStats";
            this.Text = "InventoryStats";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button show_btn;
        private System.Windows.Forms.Button BtnItem;
        private System.Windows.Forms.TextBox TxtItemCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtItemName;
        private UC_TVKitchens uC_TVKitchens1;
        private System.Windows.Forms.CheckBox CBMyKitchen;
    }
}