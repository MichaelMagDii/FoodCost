namespace Food_Cost.Forms
{
    partial class FrmSelection
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Txt_Input = new System.Windows.Forms.TextBox();
            this.Radio_Name = new System.Windows.Forms.RadioButton();
            this.Radio_Code = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(42, 79);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.Size = new System.Drawing.Size(475, 234);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Txt_Input
            // 
            this.Txt_Input.Location = new System.Drawing.Point(139, 34);
            this.Txt_Input.Name = "Txt_Input";
            this.Txt_Input.Size = new System.Drawing.Size(195, 20);
            this.Txt_Input.TabIndex = 9;
            this.Txt_Input.TextChanged += new System.EventHandler(this.Txt_Input_TextChanged);
            // 
            // Radio_Name
            // 
            this.Radio_Name.AutoSize = true;
            this.Radio_Name.Location = new System.Drawing.Point(11, 46);
            this.Radio_Name.Name = "Radio_Name";
            this.Radio_Name.Size = new System.Drawing.Size(105, 17);
            this.Radio_Name.TabIndex = 8;
            this.Radio_Name.TabStop = true;
            this.Radio_Name.Text = "Search By Name";
            this.Radio_Name.UseVisualStyleBackColor = true;
            // 
            // Radio_Code
            // 
            this.Radio_Code.AutoSize = true;
            this.Radio_Code.Location = new System.Drawing.Point(11, 23);
            this.Radio_Code.Name = "Radio_Code";
            this.Radio_Code.Size = new System.Drawing.Size(102, 17);
            this.Radio_Code.TabIndex = 7;
            this.Radio_Code.TabStop = true;
            this.Radio_Code.Text = "Search By Code";
            this.Radio_Code.UseVisualStyleBackColor = true;
            // 
            // FrmSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 343);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Txt_Input);
            this.Controls.Add(this.Radio_Name);
            this.Controls.Add(this.Radio_Code);
            this.Name = "FrmSelection";
            this.Text = "FrmSelection";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox Txt_Input;
        private System.Windows.Forms.RadioButton Radio_Name;
        private System.Windows.Forms.RadioButton Radio_Code;
    }
}