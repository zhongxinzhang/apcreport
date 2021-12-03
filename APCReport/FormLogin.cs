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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private DataSet dscea;

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入用户名");
                textBox1.Focus();//获取焦点
                return;
            }
            //else if (textBox2.Text.Trim() == "")
            //{
            //    MessageBox.Show("请输入密码");
            //    textBox2.Focus();//获取焦点
            //    return;
            //}
            else
            {
                GetData();

                if (dscea.Tables["DataTable1"].Rows.Count == 1)
                {   
                    //dscea.Tables["DataTable1"].Columns[0].ToString == 1
                    if (dscea.Tables[0].Rows[0]["passwd"].ToString().Trim() == textBox2.Text.Trim())
                    {
                        //MessageBox.Show("登录成功！");
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("密码错误，请重输！");
                        return;
                    }
                }
                else 
                {
                    MessageBox.Show("用户名不存在，请重输！");
                    return;
                }
            }
        }
        private void GetData()
        {
            String tb1 = textBox1.Text;

            this.dscea = new DataSet();

            this.dscea.Tables.Add("DataTable1");

            //连接数据库
            string connstr = SqlHelper.connString;
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            string sqlstr = null;
            if (textBox1.Text != "")
            {
                sqlstr = "select * from userlog where auditpass = 1 and userid = '" + textBox1.Text + "'";
            }

            SqlDataAdapter sda1 = new SqlDataAdapter(sqlstr, conn);

            sda1.Fill(dscea,"DataTable1");

            conn.Close();
            conn.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            //Form2事件注册
            registerForm.SendEvent += new RegisterForm.SendMessage(GetMessage);
            registerForm.ShowDialog();
            if (registerForm.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("注册成功！");
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        //代理调用的方法
        public void GetMessage(string[] str)
        {
            textBox1.Text = str[0];
            textBox2.Text = str[1];
        }

        private void FormLogin_Enter(object sender, EventArgs e)
        {
            //this.textBox1.Focus();   
        }
    }
}
