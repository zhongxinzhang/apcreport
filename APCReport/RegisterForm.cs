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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        public delegate void SendMessage(string[] str);
        //事件声明
        public event SendMessage SendEvent;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("密码不能为空!");
                return;
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("确认密码不能为空!");
                return;
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("密码和确认密码不相符!");
                //textBox2.Text = "";
                //textBox1.Text = "";
                return;
            }

            //连接数据库
            string connstr = SqlHelper.connString;
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                conn.Open();

                string sql = string.Format("select count(*) from userlog where userid = '{0}'", textBox1.Text);
                SqlCommand cmd = new SqlCommand(sql, conn);

                int a = (int)cmd.ExecuteScalar();//返回一个值，看用户是否存在
                StringBuilder strsql = new StringBuilder();
                if (a == 0)
                {
                    strsql.Append("insert into userlog (userid,passwd,username)");
                    strsql.Append("values(");
                    strsql.Append("'" + textBox1.Text.Trim().ToString() + "',");
                    strsql.Append("'" + textBox2.Text.Trim().ToString() + "',");
                    strsql.Append("'" + textBox4.Text.Trim().ToString() + "'");
                    strsql.Append(")");
                    using (SqlCommand cmd2 = new SqlCommand(strsql.ToString(),conn))
                    {
                        cmd2.ExecuteNonQuery();
                    }
                    MessageBox.Show("注册成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    string[] str1 = { textBox1.Text, textBox2.Text };

                    SendEvent(str1);

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户已存在！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Application.Exit();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }           
        }
    }
}
