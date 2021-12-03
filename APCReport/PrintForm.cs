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

namespace APCReport
{
    public partial class PrintForm : Form
    {
        private DataSet dscea;
        private string formstring;

        private Report pReport; //新建一个私有变量
        public PrintForm(DataSet dscea1,String string1)
        {
            InitializeComponent();
            dscea = dscea1;
            formstring = string1;
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }

            pReport = new Report();
            String reportFile;
            switch (formstring)
            {
                case "POFORM1":
                    reportFile = @"Reports/pofastreport2.frx";
                    break;
                case "POFORM3":
                    reportFile = @"Reports/pofastreport1.frx";
                    break;
                case "SOFORM2":
                    reportFile = @"Reports/sofastreport2.frx";
                    break;
                default:
                    reportFile = @"Reports/pofastreport3.frx";
                    break;
            }

            pReport.Load(reportFile);
            switch (formstring)
            {
                case "POFORM1":
                    pReport.RegisterData(dscea.Tables[0], "tb_so");
                    pReport.RegisterData(dscea.Tables[1], "tb_sos");
                    break;
                case "POFORM3":
                    pReport.RegisterData(dscea.Tables[0], "tb_so");
                    break;
                case "SOFORM2":
                    pReport.RegisterData(dscea.Tables[0], "tb_so");
                    break;
                default:
                    pReport.RegisterData(dscea.Tables[0], "tb_so");
                    break;
            }

            pReport.Preview = previewControl1; //设置报表的Preview控件（这里的previewControl1就是我们之前拖进去的那个）
            pReport.Prepare();   //准备
            pReport.ShowPrepared();  //显示            DataSet ds = new DataSet();
        }
    }
}
