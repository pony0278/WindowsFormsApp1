using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Security;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WindowsFormsApp1.Properties;

namespace Starter
{
    public partial class FrmConnected : Form
    {
        public FrmConnected()
        {
            InitializeComponent();

            LoadCountryToComboBox();

            this.listView1.View = View.Details;
            CreateListViewColumnHeaders();

            this.tabControl1.SelectedIndex = 3;

            this.tabControl2.SelectedIndex = 1;

            //========================
            this.pictureBox1.AllowDrop = true;
     
            this.pictureBox1.DragEnter += PictureBox1_DragEnter;
            this.pictureBox1.DragDrop += PictureBox1_DragDrop;

            //========================
            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.DragEnter += FlowLayoutPanel1_DragEnter;
            this.flowLayoutPanel1.DragDrop += FlowLayoutPanel1_DragDrop;

        }

        private void FlowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        
            for (int i=0; i<=files.Length-1;i++)
            {
                PictureBox pic =  new PictureBox();
                pic.Image = Image.FromFile(files[i]);
                pic.SizeMode = PictureBoxSizeMode.StretchImage;

                pic.Width = 150; pic.Height = 100;
                pic.Click += Pic_Click;
                this.flowLayoutPanel1.Controls.Add(pic);
            }
        }

        private void Pic_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            //f.BackgroundImage = ((PictureBox)sender).Image;
            //f.BackgroundImageLayout = ImageLayout.Zoom;
            
            PictureBox pic =  sender as PictureBox;
            if (pic != null)
            {
                f.BackgroundImage = pic.Image;
                f.BackgroundImageLayout = ImageLayout.Zoom;

            }


            f.Show();

        
        }

        private void FlowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void PictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            //MessageBox.Show("Drag Drop");

             string[] files = (string[])    e.Data.GetData(DataFormats.FileDrop);

            this.pictureBox1.Image = Image.FromFile(files[0]);
        }

        private void PictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        #region ListView TreeView
        private void CreateListViewColumnHeaders()
        {
              //Select ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand("select * from customers", conn);

                    conn.Open();
                    SqlDataReader dataReader =    command.ExecuteReader();

                    DataTable table = dataReader.GetSchemaTable();
                    this.dataGridView1.DataSource = table;
                    
                    for (int i=0; i<=table.Rows.Count-1; i++)
                    {
                        this.listView1.Columns.Add(table.Rows[i][0].ToString());
                    }

                    this.listView1.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize);


                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void LoadCountryToComboBox()
        {
            //Select ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString)) 
                {
                    SqlCommand command = new SqlCommand("select distinct Country from customers", conn);

                    conn.Open();
                    SqlDataReader dataReader =    command.ExecuteReader();

                    this.comboBox1.Items.Clear();
                    while (dataReader.Read()) 
                    {
                        this.comboBox1.Items.Add(dataReader["Country"]);
                    }
                    this.comboBox1.SelectedIndex = 0;

                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          //Select ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand($"select * from customers where country='{this.comboBox1.Text}'", conn);

                    conn.Open();
                    SqlDataReader dataReader =    command.ExecuteReader();

                    this.listView1.Items.Clear();
                    Random r = new Random();
                    while (dataReader.Read())
                    {
                         ListViewItem lvi=   this.listView1.Items.Add(dataReader["CustomerID"].ToString());
                        lvi.ImageIndex = r.Next(0, this.ImageList1.Images.Count);
                        if ( lvi.Index %2 ==0)
                        {
                            lvi.BackColor = Color.Orange;
                        }
                        else
                        {
                            lvi.BackColor = Color.Gray;    lvi.ForeColor = Color.White;
                        }    
                    
                        for (int i=1; i<=dataReader.FieldCount-1;i++)
                        {
                            if (dataReader.IsDBNull(i))
                            {
                                lvi.SubItems.Add("空值");
                            }
                            else
                            {
                                lvi.SubItems.Add(dataReader[i].ToString());
                            }
                           
                        }
                    }
                
                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button22_Click(object sender, EventArgs e)
        {
            // divide and conquer

            //LoadData();
            //ShowData();
            //PrintData();

            this.treeView1.Nodes.Add("xxx");


        }

        private void largeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.listView1.View = View.LargeIcon;
        }

        private void smallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.SmallIcon;
        }

        private void detailsViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.Details;
        }

        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
      //Insert  ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string userName = this.textBox1.Text;
                    string password = this.textBox2.Text;

                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"Insert into MyMember (UserName, Password) values ('{userName}', '{password}')";
                    command.Connection = conn;

                    conn.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Inser member successfully");

                } //auto conn.Close()=>conn.Dispose()
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
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string userName = this.textBox1.Text;
                    string password = this.textBox2.Text;

                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"select * from MyMember where UserName='{userName}' and Password='{password}'";
                    command.Connection = conn;
                    MessageBox.Show(command.CommandText);

                    conn.Open();
                    SqlDataReader dataReder =  command.ExecuteReader();
                    if (dataReder.HasRows)
                    {
                         MessageBox.Show("Logon successfully");
                    }
                    else
                    {
                        MessageBox.Show("Logon Failed");
                    }    
                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Insert  ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string userName = this.textBox1.Text;
                    string password = this.textBox2.Text;

                    SqlCommand command = new SqlCommand();
                    command.CommandText = "Insert into MyMember (UserName, Password) values (@UserName, @Password)";
                    command.Connection = conn;

                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value = userName;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = password;

                    //=====================
                    //SqlParameter p1 = new SqlParameter();
                    //p1.ParameterName = "@Password";
                    //p1.SqlDbType = SqlDbType.NVarChar;
                    //p1.Size = 40;
                    //p1.Direction = ParameterDirection.Input;
                    //p1.Value = password;
                    //command.Parameters.Add(p1);
                    //======================

                    conn.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Inser member successfully");

                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Insert  ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string userName = this.textBox1.Text;
                    string password = this.textBox2.Text;

                    SqlCommand command = new SqlCommand();
                    command.CommandText = "InsertMember";
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value = userName;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = password;

                    //===============
                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@Return_Value";
                    p1.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(p1);
                    //===================

                    conn.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Inser member successfully  MemberID=" + p1.Value);
                    

                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Insert  ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    string userName = this.textBox1.Text;
                    string password = FormsAuthentication.HashPasswordForStoringInConfigFile(this.textBox2.Text,  "sha1");

                    SqlCommand command = new SqlCommand();
                    command.CommandText = "InsertMember";
                    command.Connection = conn;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value = userName;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = password;

                    //===============
                    SqlParameter p1 = new SqlParameter();
                    p1.ParameterName = "@Return_Value";
                    p1.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(p1);
                    //===================

                    conn.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Inser member successfully  MemberID=" + p1.Value);


                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ComputeHash("abcd"));
        }

        //for FormsAuthentication.HashPasswordForStoringInConfigFile  過時 Solution
        public string ComputeHash(string value)
        {
            //MD5 algorithm = MD5.Create();
            SHA1 algorithm = SHA1.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            string hashString = "";
            for (int i = 0; i < data.Length; i++)
            {
                hashString += data[i].ToString("x2").ToUpperInvariant();
            }
            return hashString; //"abcd" =>81FE8BFE87576C3ECB22426F8E57847382917ACF
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {

                    using (SqlCommand command = new SqlCommand())
                    {
                        string UserName = textBox1.Text;
                        string Password = textBox2.Text;

                        //=====================
                        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
                        byte[] buf = new byte[15];
                        rng.GetBytes(buf); //要將在密碼編譯方面強式的隨機位元組填入的陣列。 
                        string salt = Convert.ToBase64String(buf);

                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Password + salt, "sha1");
                        //======================

                        command.CommandText = "Insert into MyMember (UserName, Password, Salt) values (@UserName, @Password, @Salt)";
                        command.Connection = conn;

                        command.Parameters.Add("@UserName", SqlDbType.NVarChar, 16).Value = UserName;
                        command.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = Password;
                        command.Parameters.Add("@Salt", SqlDbType.NVarChar).Value = salt;

                        conn.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Insert successfully");

                    }
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.myMemberTableAdapter1.Insert("777", "7777", "");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("select * from categories", conn);
                    SqlDataReader dataReader = command.ExecuteReader();

                    this.listBox1.Items.Clear();

                    while (dataReader.Read())
                    {
                        string s = $"{dataReader[1]}";
                        this.listBox1.Items.Add(s);
                    }
                    //=======================

                    command.CommandText = "select * from Products";

                    // Exception: Message "已經開啟一個與這個 Command 相關的 DataReader，必須先將它關閉。" string
                    dataReader = command.ExecuteReader();

                    this.listBox2.Items.Clear();
                    while (dataReader.Read())
                    {
                        string s = $"{dataReader[1]}";
                        this.listBox2.Items.Add(s);
                    }


                } //Auto Conn.Close();=> Conn.Dispose() 釋放 System.ComponentModel.Component 所使用的所有資源。
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("select * from categories", conn);
                    SqlDataReader dataReader = command.ExecuteReader();

                    this.listBox1.Items.Clear();

                    while (dataReader.Read())
                    {
                        string s = $"{dataReader[1]}";
                        this.listBox1.Items.Add(s);
                    }
                    //=======================
                    //Solution 1:
                    dataReader.Close();

                    command.CommandText = "select * from Products";

                        dataReader = command.ExecuteReader();

                    this.listBox2.Items.Clear();
                    while (dataReader.Read())
                    {
                        string s = $"{dataReader[1]}";
                        this.listBox2.Items.Add(s);
                    }


                } //Auto Conn.Close();=> Conn.Dispose() 釋放 System.ComponentModel.Component 所使用的所有資源。
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {

                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    conn.Open();

                    command.CommandText = "select * from categories";

                    //Solution 2:
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {

                        while (dataReader.Read())
                        {
                            this.listBox1.Items.Add(dataReader["CategoryName"]);
                        }
                    }//Auto call   dataReader.Close();

                    //===================================
               
                    command.CommandText = "select * from Products";

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {

                        while (dataReader.Read())
                        {
                            this.listBox2.Items.Add(dataReader["ProductName"]);
                        }
                    }


                } //Auto conn.close();conn.dispose(0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("select * from categories; select * from Products", conn);
                    SqlDataReader dataReader = command.ExecuteReader();

                    this.listBox1.Items.Clear();

                    while (dataReader.Read())
                    {
                        string s = $"{dataReader[1]}";
                        this.listBox1.Items.Add(s);
                    }
                    //====================================================

                    dataReader.NextResult();

                    this.listBox2.Items.Clear();

                    while (dataReader.Read())
                    {
                        string s = $"{dataReader[1]}";
                        this.listBox2.Items.Add(s);
                    }


                } //Auto Conn.Close();=> Conn.Dispose() 釋放 System.ComponentModel.Component 所使用的所有資源。
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
      //Select ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                   
                    conn.Open();
    
                    command.CommandText = "select Max(UnitPrice) from products";
                    this.listBox1.Items.Add($"Max UnitPrice = {command.ExecuteScalar():c2}"  );

                    command.CommandText = "select Min(UnitPrice) from products";
                    this.listBox1.Items.Add("Min UnitPrice = " + command.ExecuteScalar());

                    command.CommandText = "select Avg(UnitPrice) from products";
                    this.listBox1.Items.Add("Avg UnitPrice = " + command.ExecuteScalar());


                    command.CommandText = "select Count(*) from products";
                    this.listBox1.Items.Add("Count  = " + command.ExecuteScalar());

                    command.CommandText = "select Sum(UnitsInStock) from products";
                    this.listBox1.Items.Add("Sum UnitsInStock = " + command.ExecuteScalar());

                    //command.CommandText = "select Median(UnitPrice) from products";
                    //this.listBox1.Items.Add("Median UnitPrice = " + command.ExecuteScalar());
                    this.listBox1.Items.Add($"NOW ={DateTime.Now:yyyy/MMM/dd} ");

                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
      //Select ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    MessageBox.Show("aaa\r\nbbb\tccc");
                     string commandText = "CREATE TABLE [dbo].[MyImageTable](\r\n\t[ImageID] [int] IDENTITY(1,1) NOT NULL,\r\n\t[Description] [text] NULL,\r\n\t[Image] [image] NULL,\r\n CONSTRAINT [PK_MyImageTable] PRIMARY KEY CLUSTERED \r\n(\r\n\t[ImageID] ASC\r\n)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]\r\n) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                    SqlCommand command = new SqlCommand();
                    command.CommandText = commandText;
                    command.Connection = conn;

                    conn.Open();
                    command.ExecuteNonQuery();

                
                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            //Insert  ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "Insert into MyImageTable (Description, Image) values (@Description, @Image)";
                    command.Connection = conn;

                    //=========================
                    byte[] bytes;//= { 1, 3 };
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    this.pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bytes = ms.GetBuffer();
                    //==========================

                    command.Parameters.Add("@Description", SqlDbType.Text).Value = this.textBox4.Text;
                    command.Parameters.Add("@Image", SqlDbType.VarBinary).Value = bytes;

                    conn.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Inser Image successfully");

                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
    
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand($"select * from MyImageTable", conn);

                    conn.Open();
                    SqlDataReader dataReader =    command.ExecuteReader();

                    this.listBox3.Items.Clear();
                    this.listBox4.Items.Clear(); // List<int> myItems
                  
                    while (dataReader.Read())
                    {
                        this.listBox3.Items.Add(dataReader["Description"]);
                        this.listBox4.Items.Add(dataReader["ImageID"]);
                    }
                
                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
             int imageID =(int) this.listBox4.Items[this.listBox3.SelectedIndex]; // 4;
          
            ShowImage(imageID);
        }

        private void ShowImage(int imageID)
        {
                 //Select ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"Select * from MyImageTable where ImageID={imageID}";
                    command.Connection = conn;

                    conn.Open();
                    SqlDataReader dataReader =    command.ExecuteReader();

                    if (dataReader.HasRows)
                    { 
                        dataReader.Read();
                        byte[] bytes = (byte[])  dataReader["Image"];
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                        this.pictureBox2.Image = Image.FromStream(ms);
                    }
                    else
                    {
                        MessageBox.Show("Error ImageID .....");
                    }

                
                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.pictureBox2.Image = this.pictureBox2.ErrorImage;

            }
        }

        private void button27_Click(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand($"select * from MyImageTable", conn);

                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();

                    this.listBox5.Items.Clear();
                    while (dataReader.Read())
                    {
                        MyImage x = new MyImage();
                        //x.P1 = 100;   //set
                        //int n = x.P1;  //get
                        x.ImageID = (int) dataReader["ImageID"];
                        x.ImageDesc = dataReader["Description"].ToString();

                        this.listBox5.Items.Add(x);
                    }

                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if  (this.listBox5.SelectedIndex ==-1)
            {
                //exit mothod
                return;
            }
            int imageID = ((MyImage) this.listBox5.SelectedItem).ImageID; //this.listBox5.Items[this.listBox5.SelectedIndex]
            ShowImage(imageID);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            //Insert  ....
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {

                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    conn.Open();

                    command.CommandText = "Insert into Region (RegionID, RegionDescription) values (100, 'xxx')";
                    command.ExecuteNonQuery();

                    ////Exception
                    command.CommandText = "Insert into Region (RegionID, RegionDescription) values (100, 'yyy')";
                    command.ExecuteNonQuery();


                    command.CommandText = "Insert into Region (RegionID, RegionDescription) values (101, 'yyy')";
                    command.ExecuteNonQuery();

                    MessageBox.Show("Inser region successfully");

                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            //Insert  ....
            SqlTransaction txn = null;
            SqlConnection conn = null;
                try
            {
               conn=    new SqlConnection(Settings.Default.NorthwindConnectionString);
                {

                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    conn.Open();
                  txn=   conn.BeginTransaction();
                    command.Transaction = txn;

                    command.CommandText = "Insert into Region (RegionID, RegionDescription) values (100, 'xxx')";
                    command.ExecuteNonQuery();

                    ////Exception
                    command.CommandText = "Insert into Region (RegionID, RegionDescription) values (100, 'yyy')";
                    command.ExecuteNonQuery();

                    command.CommandText = "Insert into Region (RegionID, RegionDescription) values (101, 'yyy')";
                    command.ExecuteNonQuery();

                    MessageBox.Show("Inser region successfully");
                    txn.Commit();

                } //auto conn.Close()=>conn.Dispose()
            }
            catch (Exception ex)
            {
                txn.Rollback();
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        #region Test DTC 分散式交易

        void TestDTC()
        {
            //以前 分散式交易用 COM+
            //現在用  TransactionScope
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    //'測試不同DB　DTC 用
                    //'觀察　Transaction.Current.TransactionInformation.DistributedIdentifier　變化

                    using (global::System.Data.SqlClient.SqlConnection conn1 = new System.Data.SqlClient.SqlConnection(Settings.Default.NorthwindConnectionString))
                    {
                        conn1.Open();


                        ShowCurrentTransaction();

                        SqlCommand command1 = new SqlCommand();
                        command1.Connection = conn1;
                        //command1.CommandText = "...."
                        //command1.ExecuteNonQuery();

                        conn1.Close();

                    }


                    //'測試不同DB　DTC 用
                    //'觀察　Transaction.Current.TransactionInformation.DistributedIdentifier　變化

                    using (SqlConnection conn1 = new SqlConnection(Settings.Default.NorthwindConnectionString))
                    {
                        using (SqlCommand myCommand = new SqlCommand())
                        {
                            myCommand.Connection = conn1;

                            conn1.Open();

                            ShowCurrentTransaction();

                            //Restore database to near it's original condition so sample will work correctly.
                            myCommand.CommandText = "DELETE FROM Region WHERE (RegionID = 100) OR (RegionID = 101)";
                            myCommand.ExecuteNonQuery();

                            //Insert the first record.
                            myCommand.CommandText = "Insert into Region (RegionID, RegionDescription) VALUES (100, 'MidWestern')";
                            myCommand.ExecuteNonQuery();


                            myCommand.CommandText = "Insert into Region (RegionID, RegionDescription) VALUES (100, 'MidWestern')";
                            myCommand.ExecuteNonQuery();

                            //Insert the second record.
                            myCommand.CommandText = "Insert into Region (RegionID, RegionDescription) VALUES (101, 'MidEastern')";
                            myCommand.ExecuteNonQuery();

                        }
                    }

                    MessageBox.Show("trans. Successfully");

                    scope.Complete();

                }   // call TransactionScope.Dispose()  //如果 TransactionScope 物件一開始就建立了交易，則由交易管理員認可交易的實際作業，

            }

            catch (TransactionException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {

                MessageBox.Show("trans. roll back " + ex.Message);
            }
        }
        private void ShowCurrentTransaction()
        {

            //Transaction.Current
            string s = "transaction.Current.IsolationLevel: " + Transaction.Current.IsolationLevel.ToString() + Environment.NewLine +
                    "TransactionInformation.CreationTime: " + Transaction.Current.TransactionInformation.CreationTime + Environment.NewLine +
                    "TransactionInformation.Status: " + Transaction.Current.TransactionInformation.Status.ToString() + Environment.NewLine +
                    "TransactionInformation.DistributedIdentifier: " + Transaction.Current.TransactionInformation.DistributedIdentifier.ToString() + Environment.NewLine +
                    "TransactionInformation.LocalIdentifier: " + Transaction.Current.TransactionInformation.LocalIdentifier.ToString();
            MessageBox.Show(s);


        }



        private void button82_Click(object sender, EventArgs e)
        {
            TestDTC();
        }



        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                    {

                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = conn;
                            conn.Open();

                            command.CommandText = "Delete from Region where RegionID=100 or RegionID = 101";
                            command.ExecuteNonQuery();

                            command.CommandText = "Insert into Region (RegionID, RegionDescription) Values (100, 'xxx')";
                            command.ExecuteNonQuery();


                            //Exception
                            //===================
                            command.CommandText = "Insert into Region (RegionID, RegionDescription) Values (100, 'xxx')";
                            command.ExecuteNonQuery();
                            //===================

                            command.CommandText = "Insert into Region (RegionID, RegionDescription) Values (101, 'xxx')";
                            command.ExecuteNonQuery();

                        }

                    }

                    scope.Complete();

                } // 則由交易管理員認可交易的實際作業，
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button84_Click(object sender, EventArgs e)
        {
            CommittableTransaction tx = null;
            try
            {

                using (tx = new CommittableTransaction())
                {
                    using (SqlConnection conn1 = new SqlConnection(Settings.Default.NorthwindConnectionString))
                    {

                        using (SqlCommand myCommand = new SqlCommand())
                        {

                            myCommand.Connection = conn1;

                            conn1.Open();
                            conn1.EnlistTransaction(tx);


                            //Restore database to near it's original condition so sample will work correctly.
                            myCommand.CommandText = "DELETE FROM Region WHERE (RegionID = 100) OR (RegionID = 101)";
                            myCommand.ExecuteNonQuery();

                            //Insert the first record.
                            myCommand.CommandText = "Insert into Region (RegionID, RegionDescription) VALUES (100, 'MidWestern')";
                            myCommand.ExecuteNonQuery();


                            //測試交易失敗用
                            //Insert the first record.
                            myCommand.CommandText = "Insert into Region (RegionID, RegionDescription) VALUES (100, 'MidWestern')";
                            myCommand.ExecuteNonQuery();


                            //Insert the second record.
                            myCommand.CommandText = "Insert into Region (RegionID, RegionDescription) VALUES (101, 'MidEastern')";
                            myCommand.ExecuteNonQuery();


                            tx.Commit();

                            MessageBox.Show("trans. Successfully");
                        }
                    }
                }

            }


            catch (TransactionException ex)
            {
                MessageBox.Show(ex.Message);
                tx.Rollback();
            }
            catch (Exception ex)
            {

                MessageBox.Show("trans. roll back " + ex.Message);
            }
        }



        private void button82_Click_1(object sender, EventArgs e)
        {
            TestDTC();
        }

        #endregion


    }

    public class MyImage:Object
{
    public int ImageID { get; set; }
     public string ImageDesc { get; set; }

        //.......

    private int m_P1;
    public int P1
    {
        get
        {
            //.......logic
            return m_P1;
        }
        set
        {
            //.......logic
            m_P1 = value;
        }
    }

        public override string ToString()
        {
            return $"{this.ImageID} **** {this.ImageDesc}";
        }

    }
}
