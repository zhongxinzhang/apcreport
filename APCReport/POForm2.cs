using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using APCReport.SQLserver;
using FastReport;

namespace APCReport
{
    public partial class POForm2 : Form
    {
        public POForm2()
        {
            InitializeComponent();
        }

        private DataSet dscea;

        private void POForm2_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";

            dateTimePicker1.Text = DateTime.Now.AddMonths(-1).ToString("s");
            dateTimePicker2.Text = DateTime.Now.ToString("s");

            this.Top = this.Parent.Top - 20;
            this.Left = this.Parent.Left + 1;
            this.Width = this.Parent.Width - 20;
            this.Height = this.Parent.Height - 10;

            this.dscea = new DataSet();

            this.dscea.Tables.Add("DataTable1");

            GetData();

            this.dataGridView1.DataSource = dscea.Tables[0];

            dataGridView1HeadSet();

            this.dataGridView1.ReadOnly = true;
            //定义子报表处理方法

            this.dataGridView1.Focus();
        }

        private void dataGridView1HeadSet()
        {
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.Columns[0].HeaderText = "Vendor Number";
            dataGridView1.Columns[1].HeaderText = "Vendor Name";
            dataGridView1.Columns[2].HeaderText = "Phone Number";
            dataGridView1.Columns[3].HeaderText = "Rax";
            dataGridView1.Columns[4].HeaderText = "Rax Type";
            dataGridView1.Columns[5].HeaderText = "Buyer";
            dataGridView1.Columns[6].HeaderText = "Location";
            dataGridView1.Columns[7].HeaderText = "P.O. Numer";
            dataGridView1.Columns[8].HeaderText = "Item Number";
            dataGridView1.Columns[9].HeaderText = "Deacription";
            dataGridView1.Columns[10].HeaderText = "Currency";
            dataGridView1.Columns[11].HeaderText = "Quantity";
            dataGridView1.Columns[12].HeaderText = "Rcv'd Qty";
        }

        private void GetData()
        {
            String tb1 = textBox1.Text;
            String tb2 = textBox2.Text;
            String tb3 = dateTimePicker1.Value.ToString("yyyy-MM-dd").Replace("-", "");
            String tb4 = dateTimePicker2.Value.ToString("yyyy-MM-dd").Replace("-", "");

            //连接数据库
            string connstr = SqlHelper.connString;
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            //实例化SqlCommand
            SqlCommand cmd = new SqlCommand("apc_po_report3", conn);
            cmd.CommandType = CommandType.StoredProcedure;//设置调用的类型为存储过程
            SqlParameter sqlParme;

            //参数1  
            sqlParme = cmd.Parameters.Add("@instring1", SqlDbType.NVarChar);
            sqlParme.Direction = ParameterDirection.Input;
            sqlParme.Value = tb1;
            //参数2  
            sqlParme = cmd.Parameters.Add("@instring2", SqlDbType.NVarChar);
            sqlParme.Direction = ParameterDirection.Input;
            sqlParme.Value = tb2;
            //参数3
            sqlParme = cmd.Parameters.Add("@instring3", SqlDbType.NVarChar);
            sqlParme.Direction = ParameterDirection.Input;
            sqlParme.Value = tb3;
            //参数4  
            sqlParme = cmd.Parameters.Add("@instring4", SqlDbType.NVarChar);
            sqlParme.Direction = ParameterDirection.Input;
            sqlParme.Value = tb4;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dscea.Tables["DataTable1"]);

            conn.Close();
            conn.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dscea = new DataSet();

            this.dscea.Tables.Add("DataTable1");

            GetData();

            this.dataGridView1.DataSource = dscea.Tables[0];

            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new PrintForm(this.dscea, "POFORM2").ShowDialog();
        }
    }
}
