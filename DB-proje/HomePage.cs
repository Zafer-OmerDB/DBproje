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

namespace DB_proje
{
    public partial class HomePage : Form
    {
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
            String command = "SELECT KisiID FROM tbl_Person WHERE Email = @email";
            SqlCommand cmd = new SqlCommand(command, cnn);
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string kisiID = dt.Rows[0][0].ToString();
            int id = Int16.Parse(kisiID);
            MessageBox.Show("Kullanıcı ID "+id.ToString());
           



        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            //loginPage.Show();
            //this.Close();
         
        }

        private void musteriProjeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.musteriProjeBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.projectAppDBDataSet);

        }
    }
}
