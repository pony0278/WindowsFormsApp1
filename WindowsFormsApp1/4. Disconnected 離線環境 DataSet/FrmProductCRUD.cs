using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1._4._Disconnected_離線環境_DataSet
{
    public partial class FrmProductCRUD : Form
    {
        public FrmProductCRUD()
        {
            InitializeComponent();
        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.nWDataSet);

        }

        private void FrmProductCRUD_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'nWDataSet.Products' 資料表。您可以視需要進行移動或移除。
            this.productsTableAdapter.Fill(this.nWDataSet.Products);

        }

        private void Button15_Click(object sender, EventArgs e)
        {
            this.productsBindingSource.MoveNext();
        }
        int n=0;
        bool Flag = true;
        private void Button23_Click(object sender, EventArgs e)
        {
            if (Flag == true)
            {
                this.productsBindingSource.Sort = "UnitPrice Asc";
            }
            else
            {
                this.productsBindingSource.Sort = "UnitPrice Desc";
            }
            Flag = !Flag;

        }

        private void Button22_Click(object sender, EventArgs e)
        {
            int position = this.productsBindingSource.Find("ProductID", 11);
            this.productsBindingSource.Position = position;

        }

        private void Button21_Click(object sender, EventArgs e)
        {

            this.productsBindingSource.Filter = "UnitPrice > 30";
        }

       
        private void Button16_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(this.nWDataSet.Products);
            dv.RowFilter = "UnitPrice > 30";

            this.dataGridView1.DataSource = dv;
        }
 private void Button20_Click(object sender, EventArgs e)
        {
            this.productsBindingSource.AddNew();
        }

        private void Button19_Click(object sender, EventArgs e)
        {

            this.productsBindingSource.RemoveAt(this.productsBindingSource.Position);
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            FrmAddProduct f = new FrmAddProduct();
            //f.ShowDialog();
            //if (f.DialogResult == DialogResult.OK)
            
            if (f.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("OK");

                ////DataRow - Weak Type  =>C# compiler OK => RunTime Error
                //DataRow dr = this.nWDataSet.Products.NewRow();
                //dr["ProductNam"] = f.PName; //f.textBox1.Text;                      //f.textBox1.Text = "sdfsdf"; f.BackColor = Color.Red;
                //dr["Discontinued"] = f.checkBox1.Checked;                                //NOTE:  存取修飾子    f.Text   錯誤 CS0122  'FrmAddProduct.checkBox1' 由於其保護層級之故，所以無法存取 WindowsFormsApp1    C:\shared\ADO.NET\WindowsFormsApp1\WindowsFormsApp1\4.Disconnected 離線環境 DataSet\FrmProductCRUD.cs  101 作用中
                // this.nWDataSet.Products.Rows.Add(dr);
                //==================================

                //Strong Type
                NWDataSet.ProductsRow prodRow = this.nWDataSet.Products.NewProductsRow();
                prodRow.ProductName = f.PName;
                prodRow.Discontinued = f.checkBox1.Checked;
                this.nWDataSet.Products.AddProductsRow(prodRow);

                this.productsBindingSource.MoveLast();
            }
            else 
            {
                MessageBox.Show("Cancle");
            }
        
        }
    }
}
