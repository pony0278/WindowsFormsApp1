using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1._4._Disconnected_離線環境_DataSet
{
    public partial class FrmAddProduct : Form
    {
        public FrmAddProduct()
        {
            InitializeComponent();
        }

        public string PName
        {
            get
            {
                return this.textBox1.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //...
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
