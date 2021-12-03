using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using APCReport.SQLserver;


namespace APCReport
{
    public partial class POForm1 : Form
    {
        public POForm1()
        {
            InitializeComponent();
        }

        private DataSet dscea;
        private void POForm1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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
            this.dscea.Tables.Add("DataTable2");

            GetData();
            GetData1();

            DataRelation relation = new DataRelation("parent2child", dscea.Tables[0].Columns[0], dscea.Tables[1].Columns[0]);//这个Level2要与GridView的关系相同

            //将约束关系添加到子表ChildTable中
            dscea.Tables[1].ParentRelations.Add(relation);
            //dscea.Relations.Add(relation);

            this.dataGridView1.DataSource = dscea.Tables[0];

            this.dataGridView2.DataSource = dscea.Tables[1];

            dataGridView1HeadSet();
            dataGridView2HeadSet();

            this.dataGridView1.ReadOnly = true;
            this.dataGridView2.ReadOnly = true;
            //定义子报表处理方法

            dataGridView1_CellClick(dataGridView1, new DataGridViewCellEventArgs(0, 0));

            this.dataGridView1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dscea = new DataSet();

            this.dscea.Tables.Add("DataTable1");
            this.dscea.Tables.Add("DataTable2");

            GetData();
            GetData1();

            DataRelation relation = new DataRelation("parent2child", dscea.Tables[0].Columns[0], dscea.Tables[1].Columns[0]);//这个Level2要与GridView的关系相同

            //将约束关系添加到子表ChildTable中
            dscea.Tables[1].ParentRelations.Add(relation);
            //dscea.Relations.Add(relation);

            this.dataGridView1.DataSource = dscea.Tables[0];

            this.dataGridView2.DataSource = dscea.Tables[1];

            dataGridView1_CellClick(dataGridView1, new DataGridViewCellEventArgs(0, 0));

            this.Refresh();
        }

        private void dataGridView1HeadSet()
        {
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.Columns[0].HeaderText = "P.O. Number";
            dataGridView1.Columns[1].HeaderText = "PO Date";
            dataGridView1.Columns[2].HeaderText = "The Seller:";
            dataGridView1.Columns[3].HeaderText = "     ";
            dataGridView1.Columns[4].HeaderText = "     ";
            dataGridView1.Columns[5].HeaderText = "     ";
            dataGridView1.Columns[6].HeaderText = "     ";
            dataGridView1.Columns[7].HeaderText = "     ";
            dataGridView1.Columns[8].HeaderText = "     ";
            dataGridView1.Columns[9].HeaderText = "     ";
            dataGridView1.Columns[10].HeaderText = "Terms";
            dataGridView1.Columns[11].HeaderText = "Currency:";
            dataGridView1.Columns[12].HeaderText = "Revision Date";            
        }

        private void dataGridView2HeadSet()
        {
            dataGridView2.ColumnHeadersVisible = true;
            dataGridView2.Columns[0].HeaderText = "P.O. Number";
            dataGridView2.Columns[1].HeaderText = "Line";
            dataGridView2.Columns[2].HeaderText = "Vendor Item";
            dataGridView2.Columns[3].HeaderText = "Item Number";
            dataGridView2.Columns[4].HeaderText = "Description";
            dataGridView2.Columns[5].HeaderText = "Rev";
            dataGridView2.Columns[6].HeaderText = "Unit";
            dataGridView2.Columns[7].HeaderText = "Qty";
            dataGridView2.Columns[8].HeaderText = "Unit Price";
            dataGridView2.Columns[9].HeaderText = "Amount";
            dataGridView2.Columns[10].HeaderText = "Customer CO/LN";
            //dataGridView2.Columns[11].HeaderText = "APC CO/ LN";
        }

        private void GetData()
        {
            String tb1 = textBox1.Text;
            String tb2 = textBox2.Text;
            String tb3 = dateTimePicker1.Value.ToString("yyyy-MM-dd").Replace("-", "");
            String tb4 = dateTimePicker2.Value.ToString("yyyy-MM-dd").Replace("-", "");
            String tb5 = "1";

            //连接数据库
            string connstr = SqlHelper.connString;
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            //实例化SqlCommand
            SqlCommand cmd = new SqlCommand("apc_po_report2", conn);
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
            //参数5  
            sqlParme = cmd.Parameters.Add("@instring5", SqlDbType.NVarChar);
            sqlParme.Direction = ParameterDirection.Input;
            sqlParme.Value = tb5;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dscea.Tables["DataTable1"]);

            conn.Close();
            conn.Dispose();
        }

        private void GetData1()
        {
            String tb1 = textBox1.Text;
            String tb2 = textBox2.Text;
            String tb3 = dateTimePicker1.Value.ToString("yyyy-MM-dd").Replace("-", "");
            String tb4 = dateTimePicker2.Value.ToString("yyyy-MM-dd").Replace("-", "");
            String tb5 = "2";

            //连接数据库
            string connstr = SqlHelper.connString;
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            //实例化SqlCommand
            SqlCommand cmd = new SqlCommand("apc_po_report2", conn);
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
            //参数5  
            sqlParme = cmd.Parameters.Add("@instring5", SqlDbType.NVarChar);
            sqlParme.Direction = ParameterDirection.Input;
            sqlParme.Value = tb5;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;

            sda.Fill(this.dscea.Tables["DataTable2"]);

            conn.Close();
            conn.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string pid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (pid != "")
            {
                DataView dv = dscea.Tables["DataTable2"].DefaultView;
                //设置搜索条件
                dv.RowFilter = "phord = '" + pid + "'";
                //指定数据源
                dataGridView2.DataSource = dv;
            }
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            string pid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (pid != "")
            {
                DataView dv = dscea.Tables["DataTable2"].DefaultView;
                //设置搜索条件
                dv.RowFilter = "phord = '" + pid + "'";
                //指定数据源
                dataGridView2.DataSource = dv;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new PrintForm(this.dscea,"POFORM1").ShowDialog();
            //Report report = new FastReport.Report();
            //try
            //{
            //    // load the existing report
            //    string filename = @"Reports\pofastreport2.frx";
            //    report.Load(filename);
            //    report.RegisterData(dscea.Tables[0], "tb_so");
            //    report.RegisterData(dscea.Tables[1], "tb_sos");
            //    //找到 Databind绑定数据一定要先注册数据才可以邦定 
            //    report.Prepare();
            //    report.Show();        //显示 
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{// free resources used by report
            //    report.Dispose();      //释放资源 
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
