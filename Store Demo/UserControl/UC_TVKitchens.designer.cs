namespace Food_Cost.Forms
{
    partial class UC_TVKitchens
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TVKitchens = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // TVKitchens
            // 
            this.TVKitchens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TVKitchens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TVKitchens.CheckBoxes = true;
            this.TVKitchens.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.TVKitchens.Location = new System.Drawing.Point(0, 3);
            this.TVKitchens.Name = "TVKitchens";
            this.TVKitchens.Size = new System.Drawing.Size(413, 365);
            this.TVKitchens.TabIndex = 589;
            this.TVKitchens.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TVKitchens_AfterCheck);
            // 
            // UC_TVKitchens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TVKitchens);
            this.Name = "UC_TVKitchens";
            this.Size = new System.Drawing.Size(416, 371);
            this.Load += new System.EventHandler(this.UC_TVKitchens_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TVKitchens;
    }
}
