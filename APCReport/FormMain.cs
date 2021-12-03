using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APCReport
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void accountVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CEAForm3 form = new CEAForm3();
            form.MdiParent = this;
            form.Show();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CEAForm2 form = new CEAForm2();
            form.MdiParent = this;
            form.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FormLogin loginform = new FormLogin();
            loginform.ShowDialog();
            if (loginform.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("登录成功!");
            }
            else
            {
                this.Close();
            }

            timer1.Interval = 1000;
            timer1.Start();
//            this.toolStripStatusLabel3.Text = "登录时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            this.Width = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width * 0.9);
            this.Height = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.9);
            this.Left = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Width*0.05);
            this.Top = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * 0.05);

/*            ToolStripItem[] tsmi = menuStrip1.Items.Find("System", true);
             if (tsmi != null)
             {
                // tsmi[0].Visible = false;
                menuStrip1.Items[0].Visible = false; 
             }

 */ 
              /*foreach (ToolStripMenuItem item in this.menuStrip1.Items)
            {
                MessageBox.Show(item.Text);
                EnumerateMenu(item); 
            }
            */
        }

        private void getSubItem(ToolStripMenuItem dc)
        {
/*            this.richTextBox1.Text += dc.Text + "\n";
            this.arrayItems.Add(dc);
            if (dc.DropDownItems.Count > 0)
            {
                for (int i = 0; i < dc.DropDownItems.Count; i++)
                {
                    if (dc.DropDownItems[i] is ToolStripMenuItem)
                    {
                        ToolStripMenuItem sdc = dc.DropDownItems[i]
                            as ToolStripMenuItem;
                        getSubItem(sdc);
                    }
                }
            }
*/        }

        private void EnumerateMenu(ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem subItem in item.DropDownItems)
            {
                MessageBox.Show(item.Text);
                //EnumerateMenu(item);
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLogin loginform = new FormLogin();
            loginform.ShowDialog();
            if (loginform.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("登录成功!");
            }
            //else
            //{
            //    this.Close();
            //}

        }

        private void pOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            POForm4 form = new POForm4();
            // POForm1 form = new POForm1();
            form.MdiParent = this;
            form.Show();
        }

        private void goodsReceivedNoInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            POForm2 form = new POForm2();
            form.MdiParent = this;
            form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "您好,欢迎登录系统！" + "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.MdiParent = this;
            form.Show();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HelpForm form = new HelpForm();
            form.MdiParent = this;
            form.Show();
        }

        private void updatePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePassForm form = new UpdatePassForm();
            form.MdiParent = this;
            form.Show();
        }

        private void receiverTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WHForm1 form = new WHForm1();
            form.MdiParent = this;
            form.Show();
        }

        private void salesOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SOForm1 form = new SOForm1();
            form.MdiParent = this;
            form.Show();
        }

        private void packingSlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SOForm3 form = new SOForm3();
            form.MdiParent = this;
            form.Show();
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
                      
        }

        private void outsideOperationPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            POForm3 form = new POForm3();
            form.MdiParent = this;
            form.Show();
        }

        private void pRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            POForm6 form = new POForm6();
            form.MdiParent = this;
            form.Show();
        }

        private void aPPOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            POForm5 form = new POForm5();
            form.MdiParent = this;
            form.Show();
        }
    }
}
