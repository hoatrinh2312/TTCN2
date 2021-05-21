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
    public partial class FrmChinh : Form
    {
        public FrmChinh()
        {
            InitializeComponent();
        }

        private void FrmChinh_Load(object sender, EventArgs e)
        {
            Functions.Connection();
        }
        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect();
            Application.Exit();
        }

        private void mnuCuaHang_Click(object sender, EventArgs e)
        {
            FrmCuaHang DM_TL = new FrmCuaHang();
            DM_TL.ShowDialog();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSanPham DM_TL = new FrmSanPham();
            DM_TL.ShowDialog();
        }

        private void phiếuNhậpKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPhieuNhapKho DM_TL = new FrmPhieuNhapKho();
            DM_TL.ShowDialog();
        }

        private void mnuBCTK_Click(object sender, EventArgs e)
        {
            FrmBC_TK DM_TL = new FrmBC_TK();
            DM_TL.ShowDialog();
        }
    }
}
