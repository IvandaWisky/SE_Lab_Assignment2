using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class LoginForm : Form 
    {
        private string connectionString;

        public LoginForm()
        {
            InitializeComponent();

            connectionString =
                ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM tblUsers WHERE Username=@Username AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 1)
                {
                    this.Hide(); 
                    MainForm mainForm = new MainForm();
                    mainForm.Show(); 
                }
                else
                {
                    MessageBox.Show("Login failed. Please check your username and password.");
                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

    }
}
