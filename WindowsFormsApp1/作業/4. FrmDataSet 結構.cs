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
    public partial class FrmDataSet : Form
    {
        public FrmDataSet()
        {
            InitializeComponent();
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.customersTableAdapter1.Fill(this.nwDataSet1.Customers);

            this.dataGridView1.DataSource = this.nwDataSet1.Categories;
            this.dataGridView2.DataSource = this.nwDataSet1.Products;
            this.dataGridView3.DataSource = this.nwDataSet1.Customers;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            for (int i = 0; i <= this.nwDataSet1.Tables.Count - 1; i++)
            {
                DataTable dataTable = this.nwDataSet1.Tables[i];
                this.listBox1.Items.Add(dataTable.TableName);
                string col = "";
                for (int j = 0; j <= dataTable.Columns.Count - 1; j++)
                {
                    col += dataTable.Columns[j].ColumnName + "　";
                }
                this.listBox1.Items.Add(col);

                for (int k = 0; k <= dataTable.Rows.Count - 1; k++)
                {
                    var rowValues = new List<string>();
                    for (int l = 0; l <= dataTable.Columns.Count - 1; l++)
                    {
                        rowValues.Add(dataTable.Rows[k][l].ToString());
                    }
                    string rowString = string.Join("　　　", rowValues);
                    this.listBox1.Items.Add(rowString);
                }

                this.listBox1.Items.Add("=======================");
            }
        }
    }
}
