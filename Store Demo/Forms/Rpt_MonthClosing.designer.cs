namespace Food_Cost.Forms
{
    partial class Rpt_MonthClosing
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
            this.CBMyKitchen = new System.Windows.Forms.CheckBox();
            this.TVDates = new System.Windows.Forms.TreeView();
            this.btnRport = new System.Windows.Forms.Button();
            this.UC_TVKitchens2 = new Food_Cost.Forms.UC_TVKitchens();
            this.SuspendLayout();
            // 
            // CBMyKitchen
            // 
            this.CBMyKitchen.AutoSize = true;
            this.CBMyKitchen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.CBMyKitchen.Location = new System.Drawing.Point(248, 398);
            this.CBMyKitchen.Name = "CBMyKitchen";
            this.CBMyKitchen.Size = new System.Drawing.Size(122, 20);
            this.CBMyKitchen.TabIndex = 589;
            this.CBMyKitchen.Text = "For My Kitchen";
            this.CBMyKitchen.UseVisualStyleBackColor = true;
            this.CBMyKitchen.CheckedChanged += new System.EventHandler(this.CBMyKitchen_CheckedChanged);
            // 
            // TVDates
            // 
            this.TVDates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TVDates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TVDates.CheckBoxes = true;
            this.TVDates.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.TVDates.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TVDates.Location = new System.Drawing.Point(12, 12);
            this.TVDates.Name = "TVDates";
            this.TVDates.Size = new System.Drawing.Size(267, 345);
            this.TVDates.TabIndex = 587;
            // 
            // btnRport
            // 
            this.btnRport.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRport.Location = new System.Drawing.Point(240, 424);
            this.btnRport.Name = "btnRport";
            this.btnRport.Size = new System.Drawing.Size(130, 49);
            this.btnRport.TabIndex = 555;
            this.btnRport.Tag = "73";
            this.btnRport.Text = "Report";
            this.btnRport.UseVisualStyleBackColor = true;
            this.btnRport.Click += new System.EventHandler(this.btnRport_Click);
            // 
            // UC_TVKitchens2
            // 
            this.UC_TVKitchens2.Location = new System.Drawing.Point(298, 12);
            this.UC_TVKitchens2.Name = "UC_TVKitchens2";
            this.UC_TVKitchens2.Size = new System.Drawing.Size(306, 345);
            this.UC_TVKitchens2.TabIndex = 591;
            // 
            // Rpt_MonthClosing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 530);
            this.Controls.Add(this.UC_TVKitchens2);
            this.Controls.Add(this.TVDates);
            this.Controls.Add(this.CBMyKitchen);
            this.Controls.Add(this.btnRport);
            this.Name = "Rpt_MonthClosing";
            this.Text = "MonthClosing";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button btnRport;
        private System.Windows.Forms.CheckBox CBMyKitchen;
        private System.Windows.Forms.TreeView TVDates;
        private UC_TVKitchens UC_TVKitchens2;
    }
}