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
    public partial class FrmPhieuNhapKho : Form
    {
        DataTable tblChiTietNK;
        public FrmPhieuNhapKho()
        {
            InitializeComponent();
        }
        private void FrmPhieuNhapKho_Load(object sender, EventArgs e)
        {
            //btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnXoa.Enabled = false;
            txtMaNK.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenNCC.ReadOnly = true;
            txtThanhTien.Enabled = true;
            txtTongTien.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtTongTien.Text = "0";
            txtThanhTien.Text = "";
            Functions.FillCombo("Select MaNV from tblNhanVien", cboMaNV, "MaNV", "HoTen");
            cboMaNV.SelectedIndex = -1;
            Functions.FillCombo("Select MaNCC from tblNhaCungCap", cboMaNCC, "MaNCC", "TenNCC");
            cboMaNCC.SelectedIndex = -1;
            Functions.FillCombo("Select MaSP from tblSanPham", cboMaSP, "MaSP", "TenSP");
            cboMaSP.SelectedIndex = -1;
            if (txtMaNK.Text != "")
            {
                LoadInfoHoaDon();
                btnXoa.Enabled = true;
                btnHuy.Enabled = true;
            }
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "Select a.MaSP, b.TenSP, a.SoLuong, a.DonGia,a.DonVi, a.ThanhTien from tblChiTietNhapKho as a, tblSanPham as b where a.MaNK = N'"
                + txtMaNK.Text + "'AND a.MaSP= b.MaSP";
            tblChiTietNK = Functions.GetDataToTable(sql);
            DataGridView_NK.DataSource = tblChiTietNK;
            DataGridView_NK.AllowUserToAddRows = false;
            DataGridView_NK.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void LoadInfoHoaDon()
        {
            string str;
            str = "Select NgayNhap from tblPhieuNhapKho where MaNK = N'" + txtMaNK.Text + "'";
            dtpNgayNhap.Value = DateTime.Parse(Functions.GetFieldValues(str));
            str = "Select MaNV from tblPhieuNhapKho where MaNK = N'" + txtMaNK.Text + "'";
            cboMaNV.Text = Functions.GetFieldValues(str);
            str = "Select MaNCC from tblPhieuNhapKho where MaNK = N'" + txtMaNK.Text + "'";
            cboMaNCC.Text = Functions.GetFieldValues(str);
            str = "Select TongTien from tblPhieuNhapKho where MaNK = N'" + txtMaNK.Text + "'";
            txtTongTien.Text = Functions.GetFieldValues(str);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = false;
            btnThem.Enabled = false;
            ResetValues();
            txtMaNK.Text = Functions.CreateKey("HDN");
            LoadDataGridView();
        }
        private void ResetValues()
        {
            txtMaNK.Text = "";
            dtpNgayNhap.Value = DateTime.Now;
            cboMaNV.Text = "";
            cboMaNCC.Text = "";
            txtTongTien.Text = "";
            cboMaSP.Text = "";
            txtSoLuong.Text = "";
            txtDonVi.Text = "";
            txtDonGia.Text = "";
            txtThanhTien.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcapnhat, tong, Tongmoi;
            double dg, dgt, dgb;
            sql = "SELECT MaNK FROM tblPhieuNhapKho WHERE MaNK=N'" + txtMaNK.Text + "'";
            if (!Functions.CheckKey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDN được sinh tự động do đó không có trường hợp trùng khóa             
                if (cboMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNV.Focus();
                    return;
                }
                if (cboMaNCC.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNCC.Focus();
                    return;
                }
                //Giá nhập trong sản phẩm tự động cập nhật khi nhập hàng
                dg = Convert.ToDouble(txtDonGia.Text);
                dgt = Convert.ToDouble(Functions.GetFieldValues("SELECT DonGia FROM tblSanPham WHERE MaSP = N'" + cboMaSP.SelectedValue + "'"));
                if (dg != dgt)
                {
                    sql = "UPDATE tblSanPham SET DonGia =" + dg + " WHERE MaSP= N'" + cboMaSP.SelectedValue + "'";
                    Functions.RunSQL(sql);
                }
                sql = "INSERT INTO tblPhieuNhapKho(MaNK, NgayNhap, MaNV, MaNCC, Tongtien) VALUES (N'" + txtMaNK.Text.Trim() + "','" + dtpNgayNhap.Value + "',N'"
                    + cboMaNV.SelectedValue + "',N'" + cboMaNCC.SelectedValue + "',N'" + txtTongTien.Text + "'" + ")";
                Functions.RunSQL(sql);
                /*Giá bán trong sản phẩm tự động cập nhật khi nhập hàng
                dgb = Convert.ToDouble(Functions.GetFieldValues("SELECT DonGiaBan FROM SanPham WHERE MaGD = N'" + cboMaGD.SelectedValue + "'"));
                dgb = dg * 1.1;
                sql = "UPDATE SanPham SET DonGiaBan =" + dgt + " WHERE MaGD= N'" + cboMaGD.SelectedValue + "'";
                Functions.RunSQL(sql);*/
            }
                if (cboMaSP.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaSP.Focus();
                    return;
                }
                if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
                {
                    MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSoLuong.Text = "";
                    txtSoLuong.Focus();
                    return;
                }
                if (txtDonVi.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập đơn vị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDonVi.Focus();
                    return;
                }
                if (txtDonGia.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập đơn giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDonGia.Focus();
                    return;
                }
                sql = "SELECT MaSP FROM tblChiTietNhapKho WHERE MaSP=N'" + cboMaSP.SelectedValue + "' AND MaNK = N'" + txtMaNK.Text.Trim() + "'";
                if (Functions.CheckKey(sql))
                {
                    MessageBox.Show("Mã sản phẩm này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValuesSanPham();
                    cboMaSP.Focus();
                    return;
                }
                //số lượng ở sản phẩm tự động tăng khi nhập hàng
                sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tblSanPham WHERE MaSP = N'" + cboMaSP.SelectedValue + "'"));
                sql = "INSERT INTO tblChiTietNhapKho(MaNK,MaSP,SoLuong,DonGia, DonVi,ThanhTien) VALUES(N'" + txtMaNK.Text.Trim() +
                    "',N'" + cboMaSP.SelectedValue + "'," + txtSoLuong.Text + "," + txtDonGia.Text + ",N'"
                    + txtDonVi.Text + "'," + txtThanhTien.Text + ")";
                Functions.RunSQL(sql);
                LoadDataGridView();
                // Cập nhật lại số lượng mới vào bảng Sản phẩm
                SLcapnhat = sl + Convert.ToDouble(txtSoLuong.Text);
                sql = "UPDATE tblSanPham SET SoLuong =" + SLcapnhat + " WHERE MaSP= N'" + cboMaSP.SelectedValue + "'";
                Functions.RunSQL(sql);
                // Cập nhật lại tổng tiền cho hóa đơn nhập
                tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tblPhieuNhapKho WHERE MaNK = N'" + txtMaNK.Text + "'"));
                Tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
                sql = "UPDATE tblPhieuNhapKho SET TongTien =" + Tongmoi + " WHERE MaNK = N'" + txtMaNK.Text + "'";
                Functions.RunSQL(sql);
                txtTongTien.Text = Tongmoi.ToString();
                //lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChuoi(Double.Parse(Tongmoi.ToString()));
                ResetValuesSanPham();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnHuy.Enabled = true;
            
        
        }
        private void ResetValuesSanPham()
        {
            cboMaSP.Text = "";
            txtSoLuong.Text = "0";
            txtDonVi.Text = "";
            txtDonGia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MaSP,SoLuong FROM tblChiTietNhapKho WHERE MaNK = N'" + txtMaNK.Text + "'";
                DataTable SanPham = Functions.GetDataToTable(sql);
                for (int sanpham = 0; sanpham <= SanPham.Rows.Count - 1; sanpham++)
                {
                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tblSanPham WHERE MaSP = N'" + SanPham.Rows[sanpham][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(SanPham.Rows[sanpham][1].ToString());
                    slcon = sl - slxoa;
                    sql = "UPDATE tblSanPham SET SoLuong =" + slcon + " WHERE MaSP= N'" + SanPham.Rows[sanpham][0].ToString() + "'";
                    Functions.RunSQL(sql);
                }

                //Xóa chi tiết hóa đơn
                sql = "DELETE tblChiTietNhapKho WHERE MaNK=N'" + txtMaNK.Text + "'";
                Functions.RunSqlDel(sql);

                //Xóa hóa đơn
                sql = "DELETE tblPhieuNhapKho WHERE MaNK=N'" + txtMaNK.Text + "'";
                Functions.RunSqlDel(sql);
                ResetValues();
                LoadDataGridView();
                btnXoa.Enabled = false;
                btnHuy.Enabled = false;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            txtMaNK.Enabled = false;
        }

        private void DataGridView_NK_DoubleClick(object sender, EventArgs e)
        {
            string MaSPxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi;
            if (tblChiTietNK.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng sản phẩm 
                MaSPxoa = DataGridView_NK.CurrentRow.Cells["MaSP"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(DataGridView_NK.CurrentRow.Cells["SoLuong"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(DataGridView_NK.CurrentRow.Cells["ThanhTien"].Value.ToString());
                sql = "DELETE tblChiTietNhapKho WHERE MaNK=N'" + txtMaNK.Text + "' AND MaSP = N'" + MaSPxoa + "'";
                Functions.RunSQL(sql);
                // Cập nhật lại số lượ1ng cho các mặt sản phẩm
                sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tblSanPham WHERE MaSP = N'" + MaSPxoa + "'"));
                slcon = sl - SoLuongxoa;
                sql = "UPDATE tblSanPham SET SoLuong =" + slcon + " WHERE MaSP= N'" + MaSPxoa + "'";
                Functions.RunSQL(sql);
                // Cập nhật lại tổng tiền cho hóa đơn nhập
                tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tblPhieuNhapKho WHERE MaNK = N'" + txtMaNK.Text + "'"));
                tongmoi = tong - ThanhTienxoa;
                sql = "UPDATE tblPhieuNhapKho SET TongTien =" + tongmoi + " WHERE MaNK = N'" + txtMaNK.Text + "'";
                Functions.RunSQL(sql);
                txtTongTien.Text = tongmoi.ToString();
                //lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChuoi(Double.Parse(tongmoi.ToString()));
                LoadDataGridView();
            }
        }

        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNCC.Text == "")
            {
                txtTenNCC.Text = "";
            }
            //Khi chọn Mã nhà cung cấp thì các thông tin của nhà cung cấp sẽ hiện ra
            str = "Select TenNCC from tblNhaCungCap where MaNCC = N'" + cboMaNCC.Text + "'";
            txtTenNCC.Text = Functions.GetFieldValues(str);
            /*str = "Select DiaChi from NhaCungCap where MaNCC = N'" + cboMaNCC.Text + "'";
            txtDiaChi.Text = Functions.GetFieldValues(str);
            str = "Select DienThoai from NhaCungCap where MaNCC = N'" + cboMaNCC.Text + "'";
            mskDienThoai.Text = Functions.GetFieldValues(str);*/
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNV.Text == "")
                txtTenNV.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select HoTen from tblNhanVien where MaNV =N'" + cboMaNV.Text + "'";
            txtTenNV.Text = Functions.GetFieldValues(str);
        }

        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaSP.Text == "")
                txtTenSP.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select TenSP from tblSanPham where MaSP =N'" + cboMaSP.Text + "'";
            txtTenSP.Text = Functions.GetFieldValues(str);
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg ;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
                e.Handled = false;
            else e.Handled = true;
        }

        private void DataGridView_NK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cboMaSP.Text = DataGridView_NK.CurrentRow.Cells["MaSP"].Value.ToString();
            txtTenSP.Text = DataGridView_NK.CurrentRow.Cells["TenSP"].Value.ToString();
            txtDonGia.Text = DataGridView_NK.CurrentRow.Cells["DonGia"].Value.ToString();
            txtDonVi.Text = DataGridView_NK.CurrentRow.Cells["DonVi"].Value.ToString();
            txtThanhTien.Text = DataGridView_NK.CurrentRow.Cells["ThanhTien"].Value.ToString();
            txtSoLuong.Text = DataGridView_NK.CurrentRow.Cells["SoLuong"].Value.ToString();
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi đơn giá thì tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg;
            txtThanhTien.Text = tt.ToString();
        }

        private void cboMaNK_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("SELECT MaNK FROM tblPhieuNhapKho", cboMaNK, "MaNK", "MaNK");
            cboMaNK.SelectedIndex = -1;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboMaNK.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã phiếu nhập kho để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaNK.Focus();
                return;
            }
            txtMaNK.Text = cboMaNK.Text;
            LoadInfoHoaDon();
            LoadDataGridView();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            cboMaNK.SelectedIndex = -1;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn có chắc chắn muốn thoát chương trình không", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
}
