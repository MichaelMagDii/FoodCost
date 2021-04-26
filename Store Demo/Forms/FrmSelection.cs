using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
namespace Food_Cost.Forms
{
    public partial class FrmSelection : Form
    {
        public FrmSelection()
        {
            InitializeComponent();
        }
        private SqlCommand MyComm;
        private SqlDataAdapter MyAdapt;
        private DataTable MyTable;
        private DataView MyView;
        public string Code;
        public string Name2;
        public DataGridViewRow selrow;
        public string Code_desc, Name_desc;
        public string TableName;
        public SqlConnection MyConnection;
        public string DataConnString = Properties.Settings.Default.FoodCostDB.ToString();


        public void loaddata(string _Code, string _Name, string _TableName)
        {
            Code_desc = _Code;
            Name_desc = _Name;
            TableName = _TableName;
            MyTable = Classes.RetrieveData(Code_desc + "," + Name_desc, TableName);
            MyView = new DataView(MyTable);
            dataGridView1.DataSource = Classes.RetrieveData(Code_desc + "," + Name_desc, TableName);
                       
        }
        
        private void Frm_Selection_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            Code = "";
            Name2 = "";

        }

        private void Txt_Input_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Radio_Code.Checked == true)
                {
                    if (Txt_Input.Text != "")
                        MyView.RowFilter = Code_desc + " = '" + Txt_Input.Text + "'";
                    else
                        MyView.RowFilter = Name_desc + " like '%" + Txt_Input.Text + "%'";

                }
                else if (Radio_Name.Checked == true)
                {
                    MyView.RowFilter = Name_desc + " like '%" + Txt_Input.Text + "%'";
                }
                else
                    MyView.RowFilter = "";
                dataGridView1.DataSource = MyView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount != 0)
            {
                int i = dataGridView1.CurrentRow.Index;
                Code = dataGridView1[0, i].Value.ToString();
                Name = dataGridView1[1, i].Value.ToString();
                selrow = dataGridView1.Rows[i];
                this.Close();
            }
        }  

        private void Radio_Code_CheckedChanged(object sender, EventArgs e)
        {

        }
            
    }
}
