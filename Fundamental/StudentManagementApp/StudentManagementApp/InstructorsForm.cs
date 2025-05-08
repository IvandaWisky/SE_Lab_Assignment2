using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class InstructorsForm : Form
    {
        private readonly string connectionString;

        public InstructorsForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager
                .ConnectionStrings["SchoolDBConnectionString"]
                .ConnectionString;
        }

        private void InstructorsForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(
                    "SELECT * FROM tblInstructors", connection);
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
                    "INSERT INTO tblInstructors (InstructorName, DepartmentID) " +
                    "VALUES (@InstructorName, @DepartmentID)",
                    connection
                );

                cmd.Parameters.AddWithValue("@InstructorName", "New Instructor");
                cmd.Parameters.AddWithValue("@DepartmentID", 1);

                cmd.ExecuteNonQuery();
                LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int instructorID = Convert.ToInt32(
                    dataGridView1.SelectedRows[0].Cells["InstructorID"].Value
                );

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE tblInstructors " +
                        "SET InstructorName=@InstructorName, DepartmentID=@DepartmentID " +
                        "WHERE InstructorID=@InstructorID",
                        connection
                    );

                    cmd.Parameters.AddWithValue("@InstructorID", instructorID);
                    cmd.Parameters.AddWithValue("@InstructorName", "Updated Instructor");
                    cmd.Parameters.AddWithValue("@DepartmentID", 2);

                    cmd.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int instructorID = Convert.ToInt32(
                    dataGridView1.SelectedRows[0].Cells["InstructorID"].Value
                );

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM tblInstructors WHERE InstructorID=@InstructorID",
                        connection
                    );
                    cmd.Parameters.AddWithValue("@InstructorID", instructorID);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
