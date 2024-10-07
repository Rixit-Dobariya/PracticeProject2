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

namespace PracticeProject2
{
    public partial class Form1 : Form
    {
        string connnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\01\source\repos\PracticeProject2\PracticeProject2\Student.mdf;Integrated Security = True";
        SqlConnection con;
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(connnectionString);
            con.Open();
        }
        ~Form1()
        {
            con.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                panel1.Visible = false;
                btnEdit.Text = "Edit";
            }
            else
            {
                panel1.Visible = true;
                btnEdit.Text = "Hide";
            }
        }
        void displayData()
        {
            dataGridView1.Rows.Clear();
            string query = "select * from Student";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            displayData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string query = "insert into Student values(@name, @course, @dob, @address, @contact)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name",txtName.Text);
            cmd.Parameters.AddWithValue("@course",txtCourse.Text);
            cmd.Parameters.AddWithValue("@dob",txtDOB.Text);
            cmd.Parameters.AddWithValue("@address",txtAddress.Text);
            cmd.Parameters.AddWithValue("@contact",txtContact.Text);
            cmd.ExecuteNonQuery();
            clear();
            MessageBox.Show("Record inserted successfully!");
            displayData();
        }
        void clear()
        {
            txtName.Text = "";
            txtCourse.Text = "";
            txtDOB.Text = "";
            txtAddress.Text = "";
            txtContact.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "update Student set name=@name, course=@course, dob=@dob, address=@address, mob=@contact where id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", txtName.Text);
            cmd.Parameters.AddWithValue("@course", txtCourse.Text);
            cmd.Parameters.AddWithValue("@dob", txtDOB.Text);
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@contact", txtContact.Text);
            cmd.Parameters.AddWithValue("@id", txtID.Text);
            
            cmd.ExecuteNonQuery();
            clear();
            MessageBox.Show("Record updated successfully!");
            displayData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = $"select * from Student where Id={txtID.Text}";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow dr = ds.Tables[0].Rows[0];
            txtName.Text = dr[1].ToString();
            txtCourse.Text = dr[2].ToString();
            txtDOB.Text = dr[3].ToString();
            txtAddress.Text = dr[4].ToString();
            txtContact.Text = dr[5].ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "delete from Student where id = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", txtID.Text);
            cmd.ExecuteNonQuery();
            clear();
            MessageBox.Show("Record deleted successfully!");
            displayData();
        }
    }
}
