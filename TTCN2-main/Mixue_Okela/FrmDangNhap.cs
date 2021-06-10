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

namespace Mixue_Okela
{
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }
         SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-S30D2EK;Initial Catalog=QuanLyKhoHang;Integrated Security=True");

        private string getId(string user, string pass)
        {
            string id = "";
            SqlCommand cmd = new SqlCommand("Select * from tblTaiKhoan where TenDangNhap = N'" + txtDangNhap.Text + "'" +
                "and MatKhau=N'" + txtMatKhau.Text + "'", conn);
            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    id = dr["Id"].ToString();
                }
            }
            return id;
        }

        public static string ID = "";
        //public static string IdNhomQH = "";
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string sql = "Select * from tblTaiKhoan where TenDangNhap=N'" + txtDangNhap.Text + "'" +
                "and MatKhau=N'" + txtMatKhau.Text + "'";
            //string id_user;
            ID = getId(txtDangNhap.Text, txtMatKhau.Text);
            if (ID != "")
            {
                MessageBox.Show("Đăng nhập thành công","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Hide();
                Form main = new FrmChinh(); 
                main.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDangNhap.Text = "";
                txtMatKhau.Text = "";
                txtDangNhap.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {

        }
        
    }
}
