using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class DepartmentsForm : Form
    {
        private readonly string connectionString;

        public DepartmentsForm()
        {
            InitializeComponent();

            connectionString = ConfigurationManager
                .ConnectionStrings["SchoolDBConnectionString"]
                .ConnectionString;
        }

        private void DepartmentsForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(
                    "SELECT * FROM tblDepartments",
                    connection
                );

                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO tblDepartments (DepartmentName) VALUES (@Name)",
                    connection
                );

                cmd.Parameters.AddWithValue("@Name", "New Department");

                cmd.ExecuteNonQuery();
                LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int departmentID = Convert.ToInt32(
                    dataGridView1.SelectedRows[0].Cells["DepartmentID"].Value
                );

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE tblDepartments " +
                        "SET DepartmentName=@Name " +
                        "WHERE DepartmentID=@ID",
                        connection
                    );

                    cmd.Parameters.AddWithValue("@ID", departmentID);
                    cmd.Parameters.AddWithValue("@Name", "Updated Department");

                    cmd.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int departmentID = Convert.ToInt32(
                    dataGridView1.SelectedRows[0].Cells["DepartmentID"].Value
                );

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM tblDepartments WHERE DepartmentID=@ID",
                        connection
                    );

                    cmd.Parameters.AddWithValue("@ID", departmentID);
                    cmd.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
