using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace mastersheet
{
    public partial class Form1 : Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='mastersheet';username=root;password=");
        MySqlCommand cmd;
        MySqlDataAdapter adapt;
        public Form1()
        {
            InitializeComponent();
            DisplayData();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            // SUM The Total
            if (Say.Text != "" && Dor1.Text != "" && Dor2.Text == "" && Dor3.Text == "")
            {
                txtTotal.Text = (Convert.ToInt32(Say.Text) + Convert.ToInt32(Dor1.Text)).ToString();

            }
            else if (Say.Text != "" && Dor1.Text != "" && Dor2.Text != "" && Dor3.Text == "")
            {
                txtTotal.Text = (Convert.ToInt32(Say.Text) + Convert.ToInt32(Dor2.Text)).ToString();
            }
            else if (Say.Text != "" && Dor1.Text != "" && Dor2.Text != "" && Dor3.Text != "")
            {
                txtTotal.Text = (Convert.ToInt32(Say.Text) + Convert.ToInt32(Dor3.Text)).ToString();
          
            }
            
            // Checks if Username Exists
            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM mastersheet.students WHERE الاسم_الكامل = @fullname", con);
            cmd1.Parameters.AddWithValue("@fullname", this.txtFullName.Text);
            con.Open();
            bool userExists = false;
            using (var dr1 = cmd1.ExecuteReader())
                if (userExists = dr1.HasRows)
                    MessageBox.Show("Sorry Dupicated name", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            con.Close();





            if (!(userExists))
            {
                // Adds a User in the Database


                if (txtFullName.Text != "" && Say.Text != "" && Dor1.Text != "")
                {
                    cmd = new MySqlCommand("INSERT INTO `students` VALUES (NULL,@fullname,@say,@dor1,@dor2,@dor3,@total)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@fullname", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@say", Say.Text);
                    cmd.Parameters.AddWithValue("@dor1", Dor1.Text);
                    cmd.Parameters.AddWithValue("@dor2", Dor2.Text);
                    cmd.Parameters.AddWithValue("@dor3", Dor3.Text);
                    cmd.Parameters.AddWithValue("@total", txtTotal.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Successfully Added", "INSERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayData();
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Fill out all the information needed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }

        }
        // Displays the data in Data Grid View  
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new MySqlDataAdapter("select * from mastersheet.students", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        // Clears the Data  
        private void ClearData()
        {
            txtID.Text = "";
            txtFullName.Text = "";
            Say.Text = "";
            Dor1.Text = "";
            Dor2.Text = "";
            Dor3.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtFullName.Text != "" )
            {
                cmd = new MySqlCommand("update mastersheet.students set الاسم_الكامل = @fullname, السعي = @say , الدور_الاول=@dor1 , الدور_الثاني=@dor2 , الدور_الثالث=@dor3 where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.Parameters.AddWithValue("@fullname", txtFullName.Text);
                cmd.Parameters.AddWithValue("@say", Say.Text);
                cmd.Parameters.AddWithValue("@dor1", Dor1.Text);
                cmd.Parameters.AddWithValue("@dor2", Dor2.Text);
                cmd.Parameters.AddWithValue("@dor3", Dor3.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Successfully Updated", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();

                // SUM The Total
                if (Say.Text != "" && Dor1.Text != "" && Dor2.Text == "" && Dor3.Text == "")
                {
                    txtTotal.Text = (Convert.ToInt32(Say.Text) + Convert.ToInt32(Dor1.Text)).ToString();

                }
                else if (Say.Text != "" && Dor1.Text != "" && Dor2.Text != "" && Dor3.Text == "")
                {
                    txtTotal.Text = (Convert.ToInt32(Say.Text) + Convert.ToInt32(Dor2.Text)).ToString();
                }
                else if (Say.Text != "" && Dor1.Text != "" && Dor2.Text != "" && Dor3.Text != "")
                {
                    txtTotal.Text = (Convert.ToInt32(Say.Text) + Convert.ToInt32(Dor3.Text)).ToString();

                }
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Select the record you want to Update", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (txtID.Text != "")
            {
                cmd = new MySqlCommand("delete from mastersheet.students where ID=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Successfully Deleted", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Select the record you want to Delete", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtFullName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            Say.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            Dor1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            Dor2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            Dor3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtTotal_Click(object sender, EventArgs e)
        {

        }

        
    }
}
