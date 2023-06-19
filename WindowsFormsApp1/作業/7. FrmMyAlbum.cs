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
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1.作業
{
    public partial class FrmMyAlbum : Form
    {
        public FrmMyAlbum()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                    {
                        SqlCommand command = new SqlCommand();
                        command.CommandText = "INSERT INTO MyAlbum (photoName, photo) VALUES (@photoName, @photo)";
                        command.Connection = conn;

                        // 讀取選取的圖片並轉換為二進位數組
                        byte[] bytes = System.IO.File.ReadAllBytes(this.openFileDialog1.FileName);

                        command.Parameters.Add("@photoName", SqlDbType.Text).Value = this.textBox1.Text;
                        command.Parameters.Add("@photo", SqlDbType.VarBinary).Value = bytes;

                        conn.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Insert Image successfully");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand($"select photo from MyAlbum", conn);
                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();

                    this.pictureBox1.Image = null;

                    while (dataReader.Read())
                    {
                        byte[] imageData = (byte[])dataReader["photo"];
                        using (MemoryStream memoryStream = new MemoryStream(imageData))
                        {
                            Image image = Image.FromStream(memoryStream);
                            this.pictureBox1.Image = image;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
