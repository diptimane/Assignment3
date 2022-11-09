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
    public partial class frm_Search_Student_Details : Form
    {
        public frm_Search_Student_Details()
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
            tb_Roll_No.Clear();
            tb_Name.Clear();
            tb_Mobile_No.Clear();
            dtp_DOB.ResetText();
            cmb_Course.SelectedIndex = -1;
        }



        private void btn_Search_Click(object sender, EventArgs e)
        {
            {
                con_open();
                SqlCommand cmd = new SqlCommand("Select * from Student_Details where Roll_No =@RNo", con);

                cmd.Parameters.Add("RNo", SqlDbType.Int).Value = tb_Roll_No.Text;
                SqlDataReader Dr = cmd.ExecuteReader();

                if (Dr.Read())
                {
                    tb_Name.Text = Dr.GetString(Dr.GetOrdinal("Name"));
                    tb_Mobile_No.Text = Dr["Mobile_No"].ToString();
                    cmb_Course.Text = Dr.GetString(Dr.GetOrdinal("Course"));
                    dtp_DOB.Text = Dr["DOB"].ToString();
                }
                else
                {
                    MessageBox.Show("No Record found");
                    tb_Roll_No.Clear();


                }
                con_close();
            }
        }

         private void btn_Add_New_Student_Click(object sender, EventArgs e)
        {
            frm_Add_New_Student obj = new frm_Add_New_Student();
            obj.Show();
            this.Hide();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Clear_Control();
        }
        private void btn_View_Student_Details_Click(object sender, EventArgs e)
        {
            frm_View_Student_Details obj = new frm_View_Student_Details();
            obj.Show();
            this.Hide();
        }
        
        private void btn_Logout_Click(object sender, EventArgs e)
        {
            frm_Login obj = new frm_Login();
            obj.Show();
            this.Hide();
        }

        private void frm_Search_Student_Details_Load(object sender, EventArgs e)
        {
            tb_Roll_No.Focus();
            lbl_UName.Text = Shared_class.Username;


        }

    
    }
}
