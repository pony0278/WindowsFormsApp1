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

using System.Configuration;
using WindowsFormsApp1.Properties;
using System.Windows.Forms.VisualStyles;
using System.Threading;

namespace Starter

{
    public partial class FrmMySqlConnection : Form
    {
        public FrmMySqlConnection()
        {
            InitializeComponent();

            this.BackColor = this.listBox1.BackColor = Settings.Default.MyColor;

            this.tabControl1.SelectedIndex = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
   try
            {
                //connected
                string connString = ConfigurationManager.ConnectionStrings["WindowsFormsApp1.Properties.Settings.NorthwindConnectionString"].ConnectionString;
                
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

        private void button58_Click(object sender, EventArgs e)
        {
            try
            {
                //加密

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConfigurationSection section = config.Sections["connectionStrings"];
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
                MessageBox.Show("加密成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button59_Click(object sender, EventArgs e)
        {
            try
            {
                //解密
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConfigurationSection section = config.Sections["connectionStrings"];
                section.SectionInformation.UnprotectSection();
                config.Save();

                MessageBox.Show("解密成功");
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
                string connString = Settings.Default.NorthwindConnectionString;
                
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

        private void button5_Click(object sender, EventArgs e)
        {
           if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
              Settings.Default.MyColor =   this.BackColor =  this.listBox1.BackColor=  this.colorDialog1.Color;
                Settings.Default.Save(); //save .config     (IO....FileStream, StreamWriter....StreamReader）
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                //connected
                string connString = Settings.Default.NorthwindConnectionString;

                using (    SqlConnection conn = new SqlConnection(connString)  )
                {
                    conn.StateChange += Conn_StateChange;

                    conn.Open();
              
                    SqlCommand command = new SqlCommand("select * from Products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();

                    this.listBox2.Items.Clear();
                    while (dataReader.Read())
                    {
                        //string.Format(.............)  =>   $"{N,M:C} - {}"   語法糖  syntax sugar
                        this.listBox2.Items.Add($"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}");
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
           // this.toolStripStatusLabel1.Text = e.CurrentState.ToString();
            this.statusStrip1.Items[0].Text = e.CurrentState.ToString();

            Application.DoEvents();
           Thread.Sleep(700);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Connection.StateChange += Conn_StateChange;
            //auto conn.Open().......conn.Close();
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.dataGridView1.DataSource = this.nwDataSet1.Products;

        }

        private void button9_Click(object sender, EventArgs e)
        {
            int[] nums = new int[4]; // { 1, 3, 45, 6 };

            SqlConnection[] conns = new SqlConnection[100];

            for (int i=0; i<=conns.Length-1;i++)
            {
                conns[i] = new SqlConnection(Settings.Default.NorthwindConnectionString);
                conns[i].Open();

                this.label3.Text = (i + 1).ToString();

                Application.DoEvents();
                Thread.Sleep(100);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //Pooling =True
            const int MAX = 100;

            SqlConnection[] conns = new SqlConnection[MAX];
            SqlDataReader[] dataReaders = new SqlDataReader[MAX];

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Settings.Default.NorthwindConnectionString);
            builder.MaxPoolSize = MAX;
            builder.ConnectTimeout = 2;
            builder.Pooling = true;

            System.Diagnostics.Stopwatch watcher1 = new System.Diagnostics.Stopwatch();
            watcher1.Start();
            for (int i = 0; i <= conns.Length - 1; i++)
            {
                conns[i] = new SqlConnection();

                conns[i].ConnectionString = builder.ConnectionString;
                conns[i].Open();


                SqlCommand command = new SqlCommand("Select * from Products", conns[i]);

                dataReaders[i] = command.ExecuteReader();

                while (dataReaders[i].Read())
                {
                    this.listBox3.Items.Add(dataReaders[i]["ProductName"]);
                }

                conns[i].Close();  //Return to POOL
            }

            watcher1.Stop();
            this.label1.Text = $"{watcher1.Elapsed.TotalSeconds} 秒";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //Pooling =false
            const int MAX = 100;

            SqlConnection[] conns = new SqlConnection[MAX];
            SqlDataReader[] dataReaders = new SqlDataReader[MAX];

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Settings.Default.NorthwindConnectionString);
            builder.MaxPoolSize = MAX;
            builder.ConnectTimeout = 2;
            builder.Pooling = false;

            System.Diagnostics.Stopwatch watcher1 = new System.Diagnostics.Stopwatch();
            watcher1.Start();
            for (int i = 0; i <= conns.Length - 1; i++)
            {
                conns[i] = new SqlConnection();

                conns[i].ConnectionString = builder.ConnectionString;
                conns[i].Open();


                SqlCommand command = new SqlCommand("Select * from Products", conns[i]);

                dataReaders[i] = command.ExecuteReader();

                while (dataReaders[i].Read())
                {
                    this.listBox3.Items.Add(dataReaders[i]["ProductName"]);
                }

                conns[i].Close();  //NOT Return to POOL
            }
            watcher1.Stop();
            this.label2.Text = $"{watcher1.Elapsed.TotalSeconds} 秒";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            const int MAX = 200;

            SqlConnection[] conns = new SqlConnection[MAX];

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Settings.Default.MyAWConnectionString);
            builder.MaxPoolSize = MAX;
            builder.ConnectTimeout = 1;
        
            for (int i = 0; i <= conns.Length - 1; i++)
            {
                conns[i] = new SqlConnection(builder.ConnectionString);
                conns[i].Open();

                this.label3.Text = (i + 1).ToString();

                Application.DoEvents();
                Thread.Sleep(10);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;


            try

            {
                string connString = "Data Source=.;Initial Catalog=Northwindxxx;Integrated Security=True";

                conn = new SqlConnection(connString);

                SqlCommand command = new SqlCommand("Select * from Products", conn);
                SqlDataReader dr = null;
                conn.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    this.comboBox2.Items.Add(dr["ProductName"]);
                }

                this.comboBox2.SelectedIndex = 0;
            }


            catch (SqlException ex)
            {
                //ex.Number
                string s = "";
                for (int i = 0; i <= ex.Errors.Count - 1; i++)
                {
                    //$"{}{}{}"
                    s += string.Format("{0} : {1}", ex.Errors[i].Number, ex.Errors[i].Message) + Environment.NewLine;
                }
                MessageBox.Show("error count:" + ex.Errors.Count + Environment.NewLine + s);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }

        private void button27_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=.;Initial Catalog=Northwindxx;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand command = new SqlCommand("Select * from Products", conn);
            SqlDataReader dr = null;

            try
            {
                conn.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    this.comboBox2.Items.Add(dr["ProductName"]);
                }

                this.comboBox2.SelectedIndex = 0;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 17:
                        MessageBox.Show("Wrong Server");
                        break;
                    case 4060:
                        MessageBox.Show("Wrong DataBase");
                        break;
                    case 18456:
                        MessageBox.Show("Wrong User");
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
    }
}
