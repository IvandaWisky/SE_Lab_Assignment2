using System;
using System.Windows.Forms;
using CourseManagementApp;

namespace StudentManagementApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void viewStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentsForm studentsForm = new StudentsForm();
            studentsForm.Show();
        }

        private void viewCoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CoursesForm coursesForm = new CoursesForm();
            coursesForm.Show();
        }

        private void viewInstructorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstructorsForm instructorsForm = new InstructorsForm();
            instructorsForm.Show();
        }

        private void viewDepartmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepartmentsForm departmentsForm = new DepartmentsForm();
            departmentsForm.Show();
        }

        private void viewStudentEnrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentEnrollmentsForm enrollmentsForm = new StudentEnrollmentsForm();
            enrollmentsForm.Show();
        }
    }
}

