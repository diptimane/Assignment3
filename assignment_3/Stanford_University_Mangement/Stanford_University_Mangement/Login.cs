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
    public partial class frm_Login : Form
    {
        public frm_Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Stanford_University_DB;Integrated Security=True");

        void con_open()
        {
            if (con.State != ConnectionState.Open)
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
        void Clear_Control()
        {
            tb_Username.Clear();
            tb_Password.Clear();
            tb_Password.Enabled = false;
            btn_Submit.Enabled = false;
            tb_Username.Focus();
        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            con_open();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = con;
            Cmd.CommandText = "Select Count(*) From Login_Details Where Username = @Uname And Password = @pwd";
            Cmd.Parameters.Add("UName", SqlDbType.NVarChar).Value = tb_Username.Text;
            Cmd.Parameters.Add("pwd", SqlDbType.NVarChar).Value = tb_Password.Text;

            cnt = Convert.ToInt32(Cmd.ExecuteScalar());

            if (cnt > 0)
            {

                MessageBox.Show("Login Successful", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Shared_class.Username = " Welcome " + tb_Username.Text;
                frm_Add_New_Student obj = new frm_Add_New_Student();
                obj.Show();
                this.Hide();

            }
            else
            {

                MessageBox.Show("Invalid username or Passward", "Retry", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            tb_Username.Clear();
            tb_Password.Clear();
            tb_Password.Enabled = false;
            btn_Submit.Enabled = false;
            tb_Username.Focus();
        }

        private void tb_Username_TextChanged(object sender, EventArgs e)
        {
            tb_Password.Enabled = true;
            lbl_Error.Visible = false;
        }

        private void tb_Password_TextChanged(object sender, EventArgs e)
        {
            btn_Submit.Enabled = true;

        }

     }
}
