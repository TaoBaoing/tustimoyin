using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetOffice.WordApi;
using Novacode;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var d =Math.Round( 1.0*3/2,0);
//            var filename = "项目需求.docx";
//            DocX document = DocX.Load(filename);
            var s = ye();
        }

        private int ye()
        {
            var d = 3;
            if (d%2 == 0)
            {
                return d/2;
            }
            else
            {
                return d/2 + 1;
            }
        }
    }
}
