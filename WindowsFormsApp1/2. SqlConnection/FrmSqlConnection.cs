using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1._2._SqlConnection
{
    public partial class FrmSqlConnection : Form
    {
        public FrmSqlConnection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //connected
                string connString = "Data Source=.;Initial Catalog=Northwind;Integrated Security=true";
                
                // using (.....) {.......} 語法糖 syntax sugar
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select * from Products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();

                    this.listBox1.Items.Clear();
                    while (dataReader.Read())
                    {
                        //string.Format(.............)  =>   $"{N,M:C} - {}"   語法糖  syntax sugar
                        this.listBox1.Items.Add($"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}");
                    }
                }   //Auto conn.Close()=>conn.Dispose() .....釋放  所使用的所有資源。
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //connected
                string connString = "Data Source=.;Initial Catalog=Northwind;User ID=sa;Password=sa";

                using (     SqlConnection conn = new SqlConnection(connString)   )
                {
                       conn.Open();
                    SqlCommand command = new SqlCommand("select * from Products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();

                    this.listBox1.Items.Clear();
                    while (dataReader.Read())
                    {
                        //string.Format(.............)  =>   $"{N,M:C} - {}"   語法糖  syntax sugar
                        this.listBox1.Items.Add($"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}");
                    }
                }   //Auto conn.Close()=>conn.Dispose() .....釋放  所使用的所有資源。

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

         
   try
            {
                //connected
                string connString = "Data Source=.;Initial Catalog=Northwind;Integrated Security=true";

                // using (.....) {.......} 語法糖 syntax sugar
                SqlConnection conn = new SqlConnection(connString);
            
                using (conn)
                {
                    conn.Open();
                    MessageBox.Show(conn.State.ToString());

                    SqlCommand command = new SqlCommand("select * from Products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();

                    this.listBox1.Items.Clear();
                    while (dataReader.Read())
                    {
                        //string.Format(.............)  =>   $"{N,M:C} - {}"   語法糖  syntax sugar
                        this.listBox1.Items.Add($"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}");
                    }
                    
                }   //Auto conn.Close()=>conn.Dispose() .....釋放  所使用的所有資源。
             
                MessageBox.Show(conn.State.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
   try
            {
                //connected
                string connString = "Data Source=.;Initial Catalog=Northwind;Integrated Security=true";
                
                // using (.....) {.......} 語法糖 syntax sugar
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.StateChange += Conn_StateChange;

                    conn.Open();

                    SqlCommand command = new SqlCommand("select * from Products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();

                    this.listBox1.Items.Clear();
                    while (dataReader.Read())
                    {
                        //string.Format(.............)  =>   $"{N,M:C} - {}"   語法糖  syntax sugar
                        this.listBox1.Items.Add($"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}");
                    }
                }   //Auto conn.Close()=>conn.Dispose() .....釋放  所使用的所有資源。

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void Conn_StateChange(object sender, StateChangeEventArgs e)
        {
            MessageBox.Show(e.CurrentState.ToString());
        }
    }
}