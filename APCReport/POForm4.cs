using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport;
using System.Data.SqlClient;
using APCReport.SQLserver;

namespace APCReport
{
    public partial class POForm4 : Form
    {
        public POForm4()
        {
            InitializeComponent();
        }

        private DataSet dscea;

        private Report pReport; //新建一个私有变量
        private void button1_Click(object sender, EventArgs e)
        {
            this.dscea = new DataSet();

            this.dscea.Tables.Add("DataTable1");
            this.dscea.Tables.Add("DataTable2");

            GetData();
            GetData1();

            pReport = new Report();
            String reportFile = "Reports/pofastreport2.frx";
            pReport.Load(reportFile);

            pReport.RegisterData(dscea.Tables[0], "tb_so");
            pReport.RegisterData(dscea.Tables[1], "tb_sos");

            pReport.Preview = previewControl1; //设置报表的Preview控件（这里的previewControl1就是我们之前拖进去的那个）

            pReport.Prepare();   //准备

            pReport.ShowPrepared();  //显示            DataSet ds = new DataSet();

        }

        private void GetData()
        {
            String tb1 = textBox1.Text;
            String tb2 = textBox1.Text;
            String tb3 = "";
            String tb4 = "";
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
            String tb2 = textBox1.Text;
            String tb3 = "";
            String tb4 = "";
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

        private void POForm4_Load(object sender, EventArgs e)
        {
            this.Top = this.Parent.Top - 20;
            this.Left = this.Parent.Left + 1;
            this.Width = this.Parent.Width - 20;
            this.Height = this.Parent.Height - 10;

            textBox1.Text = "99999";

            this.dscea = new DataSet();

            this.dscea.Tables.Add("DataTable1");

            GetData();

            textBox1.Text = dscea.Tables["DataTable1"].Rows[0]["phord"].ToString();
        }
    }
}
