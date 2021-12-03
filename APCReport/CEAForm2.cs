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
    public partial class CEAForm2 : Form
    {
        public CEAForm2()
        {
            InitializeComponent();
        }

        private DataSet dscea;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }


        private void CEAForm2_Load(object sender, EventArgs e)
        {
            textBox1.Text = "MJ0000001";
            textBox2.Text = "MJ0000001";

            comboBox1.Items.Add("");
            comboBox2.Items.Add("");

            for (DateTime dat1 = DateTime.Now; dat1.ToString("s").Substring(0, 7).Replace("-", "") != "201403"; dat1 = dat1.AddMonths(-1))
            {
                comboBox1.Items.Add(dat1.ToString("s").Substring(0, 7).Replace("-", ""));
                comboBox2.Items.Add(dat1.ToString("s").Substring(0, 7).Replace("-", ""));
            }

            comboBox1.Text = DateTime.Now.AddDays(-7).ToString("s").Substring(0, 7).Replace("-", "");
            comboBox2.Text = DateTime.Now.AddDays(-7).ToString("s").Substring(0, 7).Replace("-", "");

            this.dscea = new DataSet();
            this.dscea.Tables.Add("tb_so");
            this.dscea.Tables.Add("tb_sos");

            GetData();
            GetData1();

            this.Top = this.Parent.Top - 25;
            this.Left = this.Parent.Left + 1;
            this.Width = this.Parent.Width - 10;
            this.Height = this.Parent.Height - 5;
             
            previewControl1.Width = CEAForm2.ActiveForm.Width - 20;
            previewControl1.Height = CEAForm2.ActiveForm.Height - 100;

            FastReport.Report report = new FastReport.Report();
            string filename = @"Reports\ceafastreport1.frx";

            report.Load(filename);

            report.Preview = this.previewControl1;//让报表显示在窗体的控件中

            report.RegisterData(dscea);

//            report.SetParameterValue("time", DateTime.Now.Date.ToString("yyyy-MM-dd"));//报表里的参数赋值
            report.Prepare();
            report.ShowPrepared();
        }

        private void GetData()
        {
            String tb1 = textBox1.Text;
            String tb2 = textBox2.Text;
            String tb3 = comboBox1.Text;
            String tb4 = comboBox2.Text;
            String tb5 = ""; // dateTimePicker1.Text.Replace("-", "");
            String tb6 = ""; // dateTimePicker2.Text.Replace("-", "");

            //连接数据库
            string connstr = SqlHelper.connString;
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            //实例化SqlCommand
            SqlCommand cmd = new SqlCommand("apc_cea_report3_1", conn);
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
            //参数6  
            sqlParme = cmd.Parameters.Add("@instring6", SqlDbType.NVarChar);
            sqlParme.Direction = ParameterDirection.Input;
            sqlParme.Value = tb6;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(this.dscea.Tables["tb_so"]);

            conn.Close();
            conn.Dispose();
        }

        private void GetData1()
        {
            String tb1 = textBox1.Text;
            String tb2 = textBox2.Text;
            String tb3 = comboBox1.Text;
            String tb4 = comboBox2.Text;
            String tb5 = "";    // dateTimePicker1.Text.Replace("-", "");
            String tb6 = "";    // dateTimePicker2.Text.Replace("-", "");

            //连接数据库
            string connstr = SqlHelper.connString;
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            //实例化SqlCommand
            SqlCommand cmd = new SqlCommand("apc_cea_report3_2", conn);
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
            //参数6
            sqlParme = cmd.Parameters.Add("@instring6", SqlDbType.NVarChar);
            sqlParme.Direction = ParameterDirection.Input;
            sqlParme.Value = tb6;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            //DataTable dt = new DataTable();
            sda.Fill(this.dscea.Tables["tb_sos"]);

            conn.Close();
            conn.Dispose();

            //return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dscea = new DataSet();

            this.dscea.Tables.Add("tb_so");
            this.dscea.Tables.Add("tb_sos");

            GetData();
            GetData1();

            FastReport.Report report = new FastReport.Report();
            string filename = @"Reports\ceafastreport1.frx";

            report.Load(filename);

            report.Preview = this.previewControl1;//让报表显示在窗体的控件中

            report.RegisterData(dscea);

            report.Prepare();
            report.ShowPrepared();
        }

        private void CEAForm2_Resize(object sender, EventArgs e)
        {
            previewControl1.Width = CEAForm2.ActiveForm.Width - 20;
            previewControl1.Height = CEAForm2.ActiveForm.Height - 100;
            //MessageBox.Show(CEAForm2.ActiveForm.Width.ToString());

        }
    }
}
