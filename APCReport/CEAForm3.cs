using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using APCReport.SQLserver;
using FastReport;

namespace APCReport
{
    public partial class CEAForm3 : Form
    {
        public CEAForm3()
        {
            InitializeComponent();
        }

        private DataSet dscea;

        private void CEAForm3_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            textBox1.Text = "";
            textBox2.Text = "";

            dateTimePicker1.Text = DateTime.Now.AddMonths(-1).ToString("s");
            dateTimePicker2.Text = DateTime.Now.ToString("s");

            //comboBox1.Items.Add("");
            //comboBox2.Items.Add("");

            //for (DateTime dat1 = DateTime.Now; dat1.ToString("s").Substring(0, 7).Replace("-", "") != "201403"; dat1 = dat1.AddMonths(-1))
            //{
            //    comboBox1.Items.Add(dat1.ToString("s").Substring(0, 7).Replace("-", ""));
            //    comboBox2.Items.Add(dat1.ToString("s").Substring(0, 7).Replace("-", ""));
            //}

            //comboBox1.SelectedValue = DateTime.Now.AddDays(-7).ToString("s").Substring(0, 7).Replace("-", "");
            //comboBox2.SelectedValue = DateTime.Now.AddDays(-7).ToString("s").Substring(0, 7).Replace("-", "");

            this.Top = this.Parent.Top-20;
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

        private void dataGridView1HeadSet()
        {
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.Columns[0].HeaderText = "Invoice NO.";
            dataGridView1.Columns[1].HeaderText = "Invoice Date";
            dataGridView1.Columns[2].HeaderText = "SOLD TO:";
            dataGridView1.Columns[3].HeaderText = "    ";
            dataGridView1.Columns[4].HeaderText = "    ";
            dataGridView1.Columns[5].HeaderText = "SHIP TO:";
            dataGridView1.Columns[6].HeaderText = "    ";
            dataGridView1.Columns[7].HeaderText = "    ";
            dataGridView1.Columns[8].HeaderText = "Terms:";
            dataGridView1.Columns[9].HeaderText = "Customer NO.";
            dataGridView1.Columns[10].HeaderText = "Currency:";
        }

        private void dataGridView2HeadSet()
        {
            dataGridView2.ColumnHeadersVisible = true;
            dataGridView2.Columns[0].HeaderText = "Invoice NO.";
            dataGridView2.Columns[1].HeaderText = "Customer PO";
            dataGridView2.Columns[2].HeaderText = "Customer PO LN";
            dataGridView2.Columns[3].HeaderText = "Item No.";
            dataGridView2.Columns[4].HeaderText = "Customer Part No.";
            dataGridView2.Columns[5].HeaderText = "LOT #";
            dataGridView2.Columns[6].HeaderText = "UM";
            dataGridView2.Columns[7].HeaderText = "Qty";
            dataGridView2.Columns[8].HeaderText = "Unit Price";
            dataGridView2.Columns[9].HeaderText = "Amount";
            dataGridView2.Columns[10].HeaderText = "Customer CO/LN";
            dataGridView2.Columns[12].HeaderText = "APC CO/ LN";            
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
            SqlCommand cmd = new SqlCommand("apc_bil_report2", conn);
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

        private void button2_Click(object sender, EventArgs e)
        {
            //System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
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
            SqlCommand cmd = new SqlCommand("apc_bil_report2", conn);
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

            if (dscea.Tables[1].Rows.Count > 0) { dataGridView1_CellClick(dataGridView1, new DataGridViewCellEventArgs(0, 0)); }

            

            this.Refresh(); 
        }

        private void CEAForm3_Resize(object sender, EventArgs e)
        {
//            panel1.Width = CEAForm3.ActiveForm.Width - 40;
//            panel2.Width = CEAForm3.ActiveForm.Width - 40;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string pid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (pid != "")
            {
                DataView dv = dscea.Tables["DataTable2"].DefaultView;
                //设置搜索条件
                dv.RowFilter = "siinvn = '" + pid + "'";
                //指定数据源
                dataGridView2.DataSource = dv;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename = @"Reports\ceafastreport2.frx";
            Report report = new FastReport.Report();
            
            report.Load(filename);
            report.RegisterData(dscea.Tables[0],"tb_so");
            report.RegisterData(dscea.Tables[1], "tb_sos");
            //找到 Databind绑定数据一定要先注册数据才可以邦定 
            report.Prepare();
            report.Show();        //显示 
            report.Dispose();      //释放资源 
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            string pid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (pid != "")
            {
                DataView dv = dscea.Tables["DataTable2"].DefaultView;
                //设置搜索条件
                dv.RowFilter = "siinvn = '" + pid + "'";
                //指定数据源
                dataGridView2.DataSource = dv;
            }
        }

        private void CEAForm3_Activated(object sender, EventArgs e)
        {
       
        }

        private void CEAForm3_Enter(object sender, EventArgs e)
        {
            //MessageBox.Show("this is a demo");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
