﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DB_proje
{
    public partial class HomePage : Form
    {
        int MusteriID,CalisanID;
        string connectionString = "Data Source=DESKTOP-7AVOOGO\\SQLEXPRESS;" +
                "Initial Catalog=ProjectAppDB;" +
                "Integrated Security=SSPI;";
        SqlConnection cnn;
        Form opener;
        string email;
        public HomePage(Form parentForm, string email)
        {
            InitializeComponent();
            opener = parentForm;
            this.email = email;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            label1.Text="E posta : "+email;
            cnn = new SqlConnection(connectionString);
            String command = "SELECT FirstName,LastName,MusteriID,CalisanID FROM tbl_Person WHERE Email = @email";
            SqlCommand cmd = new SqlCommand(command, cnn);
            cmd.Parameters.AddWithValue("@email", email);
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string musteriID = dt.Rows[0][2].ToString();
            string firstName = dt.Rows[0][0].ToString();
            string lastName = dt.Rows[0][1].ToString();
            string calisanID = dt.Rows[0][3].ToString();

            label2.Text = "Hoşgeldiniz Sayın : "+firstName + " " + lastName;
            if (musteriID == "" )
            {
                MusteriID = 0;
          
            }
            else MusteriID = Int16.Parse(musteriID);
            if (calisanID != "")
            {
                CalisanID = Int16.Parse(calisanID);
            }
            
            MessageBox.Show("Müşteri ID "+MusteriID.ToString()+"Calisan ID : "+CalisanID.ToString());
           



        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cnn = new SqlConnection(connectionString);
            String command = "Select ProjeAdi,ProjeAciklama from MusteriProje as k inner join tbl_Proje as p on k.ProjeID=p.ProjeID inner join tbl_Musteri as m on m.KisiID=1 and k.MusteriID=@id";
            SqlCommand cmd = new SqlCommand(command, cnn);
            cmd.Parameters.AddWithValue("@id", MusteriID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
