using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1._4._Disconnected_離線環境_DataSet;

namespace Starter
{
    public partial class FrmDisConnected_離線DataSet : Form
    {
        public FrmDisConnected_離線DataSet()
        {
            InitializeComponent();     
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.TabControl1.SelectedIndex = this.TabControl1.TabCount - 1;
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            // this.dataGridView1.DataSource = this.nwDataSet1.Categories;
            this.dataGridView1.DataSource = this.nwDataSet1;
            this.dataGridView1.DataMember = "Categories";

            this.dataGridView7.DataSource = this.nwDataSet1.Products;

        }

        private void Button29_Click(object sender, EventArgs e)
        {
            this.dataGridView7.AllowUserToAddRows = false;
        }

        private void Button28_Click(object sender, EventArgs e)
        {
            this.dataGridView7.Columns[2].Frozen = true;
            this.dataGridView7.Rows[2].Frozen = true;
        }

        private void Button26_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.dataGridView7.CurrentCell.Value.ToString());
        }

        private void Button27_Click(object sender, EventArgs e)
        {
            MessageBox.Show( this.dataGridView7.CurrentRow.Cells[2].Value.ToString());
        }

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) 
            {
                int productID = (int) this.dataGridView7.CurrentRow.Cells["ProductID"].Value;
                int P1 = 999;
                // MessageBox.Show("Product ID =" + productID);
                FrmProductDetails f = new FrmProductDetails(P1);
                 f.ProductID = productID;
                f.Show();
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            this.dataGridView8.DataSource = this.nwDataSet1.Products;

        }

        private void button5_Click(object sender, EventArgs e)
        {   
            DataColumn column = new DataColumn("TotalPrice", typeof(decimal));
            column.Expression = "UnitPrice*UnitsInStock";

            this.nwDataSet1.Products.Columns.Add(column);
            this.dataGridView8.CellFormatting += dataGridView8_CellFormatting;
        }

        #region Demo


        DataTable empTable = new DataTable("Employees");


        private void button32_Click(object sender, EventArgs e)
        {

            //column schema
            DataColumn column1 = empTable.Columns.Add("EmpID", typeof(string));

            column1.AutoIncrement = true;
            column1.AutoIncrementSeed = 1;
            column1.AutoIncrementStep = 1;

            empTable.Columns.Add("Name", typeof(string));
            empTable.Columns.Add("Salary", typeof(decimal));

            empTable.Columns.Add("Salary+", typeof(decimal)).Expression = "Salary * 1.1";

            this.dataGridView9.DataSource = empTable;

            //=====================
            //DataRow dr = new DataRow();

            DataRow dr = empTable.NewRow();
            dr["Name"] = "aaaa";
            dr["Salary"] = 30000;
            empTable.Rows.Add(dr);


        }

        private void button31_Click(object sender, EventArgs e)
        {
            empTable.WriteXml("Employees.xml");
        }


        private void dataGridView8_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView8.Columns[e.ColumnIndex].Name == "TotalPrice")
            {
                if (e.Value is DBNull || e.Value == null) return;

                decimal Totalprice = (decimal)e.Value;


                e.CellStyle.Format = "c2";

                if (Totalprice > 80)
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmProductCRUD f = new FrmProductCRUD();
            f.Show();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            FrmRelation f = new FrmRelation();
            f.Show();

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            this.DataGridView6.DataSource = this.nwDataSet1.Products;

        }

        private void Button10_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Update(this.nwDataSet1.Products);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            this.DataGridView5.DataSource = this.nwDataSet1.Products.GetChanges(DataRowState.Added);
            this.DataGridView3.DataSource = this.nwDataSet1.Products.GetChanges(DataRowState.Modified);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                this.productsTableAdapter1.Update(this.nwDataSet1.Products);
            }
            catch (DBConcurrencyException ex)
            {
                MessageBox.Show(ex.Message);
                //.....
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
