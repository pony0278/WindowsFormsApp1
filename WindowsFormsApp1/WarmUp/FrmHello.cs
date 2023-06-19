using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmHello : Form
    {
        public FrmHello()
        {
           
            InitializeComponent();

            //初始化
             //button1.Text = "123";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // hello
            System.Windows.Forms.MessageBox.Show("Hello " + textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //
      System.Windows.Forms.      MessageBox.Show("Hi, " + textBox1.Text);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmHello      f=  new FrmHello();
            f.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
           // int i = 999;

            Form2 f = new Form2();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Set property
            label1.Text = "123";
            label1.BackColor = Color.Yellow;
            label1.BorderStyle = BorderStyle.FixedSingle;  //enum
            button2.Visible = false;

            //Get Property
            string s =   textBox1.Text;
            MessageBox.Show(s);

            //Call method
            button1.SetBounds(0, 0, 100, 100);
            button1.BringToFront();

            //event - delegate
            //+=
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //static property - 不用 new - shared one instance

            MessageBox.Show(SystemInformation.ComputerName);

            //  嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
            //錯誤  CS0200 無法指派為屬性或索引子 'SystemInformation.ComputerName'-- 其為唯讀 WindowsFormsApp1    C:\shared\ADO.NET\WindowsFormsApp1\WindowsFormsApp1\Form1.cs    77  作用中

            //  SystemInformation.ComputerName = "III";

            //========================================
            //instance property - new 

            button1.Text = "aaa";
            button2.Text = "bbb";

        }

        private void label4_Click(object sender, EventArgs e)
        {
           // Access Modifier(存取修飾子)
           //private  internal  public 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Error Demo 1
            //            嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
            //錯誤  CS0120 需要有物件參考，才可使用非靜態欄位、方法或屬性 'Form.Text' WindowsFormsApp1 C:\shared\ADO.NET\WindowsFormsApp1\WindowsFormsApp1\Form1.cs    100 作用中

            //            Form1.Text= "Hello";

            // Error Demo 2
            //Form1 f =   new Form1(); ;
            //f.Text = "Hello , " + textBox1.Text;
            //f.Show();

            this.Text = "Hello, " + this.textBox1.Text;
       
         //   Text = "Hello, " + textBox1.Text;


        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
           //. Close();
        }
    }
}
