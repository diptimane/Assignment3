using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Stanford_University_Mangement
{
    public partial class frm_Add_New_Student : Form
    {
        public frm_Add_New_Student()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Stanford_University_DB;Integrated Security=True");

        void con_open()
        {
            if(con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        void con_close()
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
        void Clear_Controls()
        {
            tb_Roll_No.Clear();
            tb_Name.Clear();
            tb_Mobile_No.Clear();
            dtp_DOB.ResetText();
            cmb_Course.SelectedIndex = -1;
        }

        private void Only_Numeric(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void Only_Text(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Space)))
            {
                e.Handled = true;

            }
        }
        int Auto_Incr()
        {
            int cnt = 0;
            con_open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select Count(*) From Student_Details";

            cnt = Convert.ToInt32(cmd.ExecuteScalar());

            if (cnt > 0)
            {
                cmd.CommandText = "Select Max(Roll_No) From Student_Details";
                cnt = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            }
            else 
            {
                cnt = 1;
            }
            con_close();
            return cnt;
        }
        private void btn_Save_Click_1(object sender, EventArgs e)
        {
            con_open();

            if (tb_Roll_No.Text != "" && tb_Name.Text != "" && tb_Mobile_No.TextLength == 10 && cmb_Course.Text != "")
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                cmd.CommandText = "Insert Into Student_Details(Roll_No,Name,DOB,Mobile_No,Course)values (@RNo, @Name, @DOB, @MNo, @Course)";

                cmd.Parameters.Add("RNo", SqlDbType.Int).Value = tb_Roll_No.Text;
                cmd.Parameters.Add("Name", SqlDbType.VarChar).Value = tb_Name.Text;
                cmd.Parameters.Add("DOB", SqlDbType.Date).Value = dtp_DOB.Value.Date;
                cmd.Parameters.Add("MNo", SqlDbType.Decimal).Value = tb_Mobile_No.Text;
                cmd.Parameters.Add("Course", SqlDbType.NVarChar).Value = cmb_Course.Text;

                cmd.ExecuteNonQuery();

                MessageBox.Show("Record inserted successfully", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("First fill all fields", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Clear_Controls();
            con_close();
        }
         private void btn_Search_Click_1(object sender, EventArgs e)
        {

            frm_Search_Student_Details obj = new frm_Search_Student_Details();
            obj.Show();
            this.Hide();
        }
        private void btn_View_Student_Details_Click(object sender, EventArgs e)
        {
            frm_View_Student_Details obj = new frm_View_Student_Details();
            obj.Show();
            this.Hide();
        }

        

        private void btn_Search_Student_Click(object sender, EventArgs e)
        {

            frm_Search_Student_Details obj = new frm_Search_Student_Details();
            obj.Show();
            this.Hide();
        }

        private void frm_Add_New_Student_Load_1(object sender, EventArgs e)
        {
            lbl_UName.Text = Shared_class.Username;
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            frm_Login obj = new frm_Login();
            obj.Show();
            this.Hide();
        }
    }
}

