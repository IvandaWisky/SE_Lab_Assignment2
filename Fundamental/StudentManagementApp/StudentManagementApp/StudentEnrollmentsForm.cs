using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CourseManagementApp
{
    public partial class StudentEnrollmentsForm : Form
    {
        private readonly string connectionString;

        public StudentEnrollmentsForm()
        {
            InitializeComponent();
            connectionString =
                ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
        }

        private void StudentEnrollmentsForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadData();
        }

        private void LoadCourses()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetCourses", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                comboBox1.DataSource = table;
                comboBox1.DisplayMember = "CourseName";
                comboBox1.ValueMember = "CourseID";
            }
        }

        private void LoadData()
        {
            if (comboBox1.SelectedValue != null)
            {
                int courseID = Convert.ToInt32(comboBox1.SelectedValue);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(
                        "SELECT * FROM tblStudentCourses WHERE CourseID=@CourseID",
                        connection
                    );

                    adapter.SelectCommand.Parameters.AddWithValue("@CourseID", courseID);

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                int courseID = Convert.ToInt32(comboBox1.SelectedValue);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO tblStudentCourses (StudentID, CourseID) VALUES (@StudentID, @CourseID)",
                        connection
                    );

                    cmd.Parameters.AddWithValue("@StudentID", 1);
                    cmd.Parameters.AddWithValue("@CourseID", courseID);

                    cmd.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && comboBox1.SelectedValue != null)
            {
                int studentID = Convert.ToInt32(
                    dataGridView1.SelectedRows[0].Cells["StudentID"].Value
                );
                int courseID = Convert.ToInt32(comboBox1.SelectedValue);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(
                        "UPDATE tblStudentCourses SET CourseID=@CourseID WHERE StudentID=@StudentID",
                        connection
                    );

                    cmd.Parameters.AddWithValue("@StudentID", studentID);
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
                int studentID = Convert.ToInt32(
                    dataGridView1.SelectedRows[0].Cells["StudentID"].Value
                );

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM tblStudentCourses WHERE StudentID=@StudentID",
                        connection
                    );

                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    cmd.ExecuteNonQuery();
                    LoadData();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void comboBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void StudentEnrollmentsForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
