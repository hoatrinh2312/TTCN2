using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mixue_Okela
{
    public partial class FrmBC_TK : Form
    {
        DataTable tblBaoCao;
        public FrmBC_TK()
        {
            InitializeComponent();
        }

        private void FrmBC_TK_Load(object sender, EventArgs e)
        {

        }

        private void DataGridView_BC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSP.Text = DataGridView_BC.CurrentRow.Cells["MaSP"].Value.ToString();
            txtSoLuong.Text = DataGridView_BC.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtTenNCC.Text = DataGridView_BC.CurrentRow.Cells["TenNCC"].Value.ToString();
            txtDonGia.Text = DataGridView_BC.CurrentRow.Cells["DonGia"].Value.ToString();
            txtDonVi.Text = DataGridView_BC.CurrentRow.Cells["DonVi"].Value.ToString();
            txtTenSP.Text = DataGridView_BC.CurrentRow.Cells["TenSP"].Value.ToString();
        }

        private void btnBC_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "select a.MaSP,a.TenSP,a.SoLuong,a.DonGia,a.DonVi,b.TenNCC from tblSanPham as a join tblNhaCungCap as b" +
                " on a.MaNCC=b.MaNCC where a.SoLuong >0 order by a.SoLuong asc";
            tblBaoCao = Functions.GetDataToTable(sql);
            DataGridView_BC.DataSource = tblBaoCao;
        }
    }
}
