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
    public partial class FrmProductDetails : Form
    {
        public int ProductID { get; set; }
        public int P1 { get; set; }

        public FrmProductDetails()
        {
            InitializeComponent();
        }

        public FrmProductDetails(int p1)
        {
            InitializeComponent();
            P1 = p1;
        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.nWDataSet);

        }

        private void FrmProductDetails_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'nWDataSet.Products' 資料表。您可以視需要進行移動或移除。
            //    this.productsTableAdapter.Fill(this.nWDataSet.Products);
          //  int productID = 6;
            this.productsTableAdapter.FillByProductID(this.nWDataSet.Products, this.ProductID);


        }
    }
}
