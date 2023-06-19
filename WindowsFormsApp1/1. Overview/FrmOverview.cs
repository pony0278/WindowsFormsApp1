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
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApp1._1._Overview
{
    public partial class FrmOverview : Form
    {
        public FrmOverview()
        {
            InitializeComponent();

            this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1; // this.tabControl1.TabCount - 1;


            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.customersTableAdapter1.Fill(this.nwDataSet1.Customers);

            this.dataGridView4.DataSource = this.nwDataSet1.Categories;
            this.dataGridView5.DataSource = this.nwDataSet1.Products;
            this.dataGridView6.DataSource = this.nwDataSet1.Customers;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Connected
            //Step 1: SqlConnection
            //Step 2: SqlCommand
            //Step 3: SqlDataReader
            //Step 4: UI Control

            // try
            // {
            //     //...........
            // }    

            // catch (DivideByZeroException ex)
            // {
            //     MessageBox.Show(ex.Message);
            // }
            // catch (OverflowException ex)
            // {
            //     //......
            // }
            //catch (Exception ex)
            // {

            // }
            SqlConnection conn = null;
            int abc = 88;
            try
            {
                conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");

                conn.Open();

                SqlCommand command = new SqlCommand("select * from Product", conn);
                //command.CommandText = "select * from Product";
                //command.Connection = conn;

                command.Connection = conn;

                SqlDataReader dataReader = command.ExecuteReader();

                this.listBox1.Items.Clear();
                while (dataReader.Read())
                {
                    //string.Format(.............)  =>   $"{N,M:C} - {}"   語法糖  syntax sugar
                    this.listBox1.Items.Add($"{dataReader["ProductName"],-40} - {dataReader["UnitPrice"]:c2}");
                }
                //MessageBox.Show( "ProductName " + dataReader["ProductName"]);   
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
                }
                //   MessageBox.Show("" + abc);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //        SqlDataReader dataReader = new SqlDataReader();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Disconnected
            //Step 1: SqlConnection
            //Step 2: SqlDataAdapter
            //Step 3:  DataSet
            //Step 4: UI Control 

            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Categories", conn);
            DataSet ds = new DataSet();

            adapter.Fill(ds); //Auto Connected:  conn.Open()=>...... Command.ExecuteXXX() => SqlDataReader .. while (...Read())....... conn.Close();

            this.dataGridView1.DataSource = ds.Tables[0];

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from  products", conn);
            DataSet ds = new DataSet();

            adapter.Fill(ds); //Auto Connected:  conn.Open()=>...... Command.ExecuteXXX() => SqlDataReader .. while (...Read())....... conn.Close();

            this.dataGridView1.DataSource = ds.Tables[0];

        }

        private void button5_Click(object sender, EventArgs e)
        {

            //TODO .....
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from  products where UnitPrice >30", conn);
            DataSet ds = new DataSet();

            adapter.Fill(ds); //Auto Connected:  conn.Open()=>...... Command.ExecuteXXX() => SqlDataReader .. while (...Read())....... conn.Close();

            this.dataGridView1.DataSource = ds.Tables[0];
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Auto Connected:  conn.Open()=>...... Command.ExecuteXXX() => SqlDataReader .. while (...Read())....... conn.Close();
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            this.dataGridView2.DataSource = this.nwDataSet1.Categories;

        }

        // WindowsFormsApp1.NWDataSetTableAdapters.ProductsTableAdapter  adapter1    =   new WindowsFormsApp1.NWDataSetTableAdapters.ProductsTableAdapter();
        private void button7_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.dataGridView2.DataSource = this.nwDataSet1.Products;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.FillByUnitPrice(this.nwDataSet1.Products, 30);
            this.dataGridView2.DataSource = this.nwDataSet1.Products;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.InsertProduct(DateTime.Now.ToLongTimeString(), true);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Update(this.nwDataSet1.Products);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.customersTableAdapter1.Fill(this.nwDataSet1.Customers);
            this.dataGridView2.DataSource = this.nwDataSet1.Customers;
          
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            //    this.dataGridView3.DataSource = this.nwDataSet1.Categories;

            this.bindingSource1.DataSource = this.nwDataSet1.Categories;
            this.dataGridView3.DataSource = this.bindingSource1;
          
            //===========================
            this.textBox1.DataBindings.Add("Text", this.bindingSource1, "CategoryName");
            this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "Picture", true);

            this.bindingNavigator1.BindingSource = this.bindingSource1;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.bindingSource1.Position = 0;
         //   this.label3.Text = $"{this.bindingSource1.Position+1} / {this.bindingSource1.Count}";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.bindingSource1.Position -= 1;
         //   this.label3.Text = $"{this.bindingSource1.Position + 1} / {this.bindingSource1.Count}";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.bindingSource1.Position = this.bindingSource1.Position + 1;
          //  this.label3.Text = $"{this.bindingSource1.Position + 1} / {this.bindingSource1.Count}";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.bindingSource1.Position = this.bindingSource1.Count-1;
        //    this.label3.Text = $"{this.bindingSource1.Position + 1} / {this.bindingSource1.Count}";
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            this.label3.Text = $"{this.bindingSource1.Position + 1} / {this.bindingSource1.Count}";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FrmTool f =   new FrmTool();
            f.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();

            for (int i=0; i<=this.nwDataSet1.Tables.Count-1; i++)
            {
                DataTable table = this.nwDataSet1.Tables[i];
                this.listBox2.Items.Add(table.TableName);

                //===========================
                //table.Columns  - column schema
                string s = "";
                for (int column=0; column<=table.Columns.Count-1; column++)
                {
                    s += table.Columns[column].ColumnName +" ";
                }
                this.listBox2.Items.Add(s);
                //===========================
                //table.Rows - Data Record
                for (int row =0; row<=table.Rows.Count-1; row++)
                {
                    //DataRow dataRow = table.Rows[row];
                    //this.listBox2.Items.Add(dataRow[0]);      DataRow 索引子屬性

                    //TODO ....
                    this.listBox2.Items.Add(table.Rows[row][0]);
                }
                this.listBox2.Items.Add("==============================");
            }
             
        }

        private void button22_Click(object sender, EventArgs e)
        {

            //if ( this.splitContainer2.Panel1Collapsed == true)
            // {
            //     this.splitContainer2.Panel1Collapsed = false;
            // }
            //else
            // {
            //     this.splitContainer2.Panel1Collapsed = true;
            // }
            this.splitContainer2.Panel1Collapsed = !this.splitContainer2.Panel1Collapsed;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.nwDataSet1.Products.WriteXml("Products.xml", XmlWriteMode.WriteSchema);

        }

        private void button21_Click(object sender, EventArgs e)
        {
            this.nwDataSet1.Products.Clear();
            this.nwDataSet1.Products.ReadXml("Products.xml");
            this.dataGridView5.DataSource = this.nwDataSet1.Products;

        }
    }
}


class class1
{
    class class2
    {

    }
}
