using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.作業
{
    public partial class FrmCategoryProducts_V2 : Form
    {
        public FrmCategoryProducts_V2()
        {
            InitializeComponent();

            this.flowLayoutPanel1.AutoScroll = true;

            for (int i=1; i<=10; i++)
            {
                LinkLabel x = new  LinkLabel();

                x.Text = "Category " + i;
                x.Tag = i;  //Category ID
                x.Width = 130;
                x.Height = 70;

                x.LinkClicked += X_LinkClicked;
                
                this.flowLayoutPanel1.Controls.Add(x);
            }
        }

        private void X_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(((LinkLabel)sender).Text);

        }
    }
}
