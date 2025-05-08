using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class CoursesForm : Form
    {
        private readonly string connectionString;

        public CoursesForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager
                .ConnectionStrings["SchoolDBConnectionString"]
                .ConnectionString;
        }

        private void FormCourses_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(
                    "SELECT * FROM tblCourses", connection);
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
                    "INSERT INTO tblCourses (CourseName, Credits, DepartmentID, InstructorID) " +
                    "VALUES (@CourseName, @Credits, @DepartmentID, @InstructorID)",
                    connection
                );

                cmd.Parameters.AddWithValue("@CourseName", "New Course");
                cmd.Parameters.AddWithValue("@Credits", 3);
                cmd.Parameters.AddWithValue("@DepartmentID", 1);
                cmd.Parameters.AddWithValue("@InstructorID", 1);

                cmd.ExecuteNonQuery();
                LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int courseID = Convert.ToInt32(
                    dataGridView1.SelectedRows[0].Cells["CourseID"].Value
                );

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE tblCourses " +
                        "SET CourseName=@CourseName, Credits=@Credits, " +
                        "DepartmentID=@DepartmentID, InstructorID=@InstructorID " +
                        "WHERE CourseID=@CourseID",
                        connection
                    );

                    cmd.Parameters.AddWithValue("@CourseName", "Updated Course");
                    cmd.Parameters.AddWithValue("@Credits", 4);
                    cmd.Parameters.AddWithValue("@DepartmentID", 2);
                    cmd.Parameters.AddWithValue("@InstructorID", 2);
                    cmd.Parameters.AddWithValue("@CourseID", courseID);

                    cmd.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int courseID = Convert.ToInt32(
                    dataGridView1.SelectedRows[0].Cells["CourseID"].Value
                );

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM tblCourses WHERE CourseID=@CourseID",
                        connection
                    );

                    cmd.Parameters.AddWithValue("@CourseID", courseID);
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

        private void FormCourses_Load_1(object sender, EventArgs e)
        {

        }
    }
}
