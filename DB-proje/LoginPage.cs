using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace DB_proje
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();

        }
        string connectionString = "Data Source=DESKTOP-7AVOOGO\\SQLEXPRESS;" +
                "Initial Catalog=ProjectAppDB;" +
                "Integrated Security=SSPI;";
        SqlConnection cnn;


        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Lütfen Email Adresinizi Giriniz!";
            textBox2.Text = "Lütfen Şifrenizi Giriniz!";
            textBox2.PasswordChar = '*';
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Lütfen Email Adresinizi Giriniz!")
            {
                textBox1.Text = "";
            }

        }

        private void textBox1_Leave_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Lütfen Email Adresinizi Giriniz!";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Lütfen Şifrenizi Giriniz!";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Lütfen Şifrenizi Giriniz!")
            {
                textBox2.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool durum = IsValidEmail(textBox1.Text);
            if (textBox1.Text.Length > 3 && textBox1.Text!= "Lütfen Email Adresinizi Giriniz!")
            {
                if (durum) label1.Text = "";
                else label1.Text = "Hatalı E-Posta";
            }
            else label1.Text = "Bilgilerinizi Girin!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            cnn = new SqlConnection(connectionString);
            try
            {
                bool durum = IsValidEmail(textBox1.Text);
                
                    if (durum)
                {
                    String command = "SELECT COUNT(*) FROM tbl_Person WHERE Email = @email and Password = @password";
                    SqlCommand cmd = new SqlCommand(command, cnn);
                    cmd.Parameters.AddWithValue("@email", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    { 
                        HomePage homePage = new HomePage(this, email);
                        homePage.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Bilgilerini Kontrol Et!");
                    }
                }
                       
                    else MessageBox.Show("Geçerli Bir Eposta Adresi Giriniz!");
               
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata" + ex);
            }

            
        }
    }
}
