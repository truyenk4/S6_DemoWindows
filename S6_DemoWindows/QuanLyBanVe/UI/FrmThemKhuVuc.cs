using QuanLyBanVe.BLL;
using QuanLyBanVe.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVe
{
    public partial class FrmThemKhuVuc : Form
    {
        private KhuVucBLL khuVucBLL;

        // Thuộc tính để lưu dữ liệu trả về
        //public string TenKhuVuc { get; set; }


        // Khai báo sự kiện
        public event Action<string> TenKhuVuc;

        public FrmThemKhuVuc()
        {
            InitializeComponent();
            khuVucBLL = new KhuVucBLL();
            khuVucBLL.CapNhatBang(dgvKhuVuc, lblKhuVuc);

        }
        
        public FrmThemKhuVuc(string danhSachKhuVuc)
        {
            InitializeComponent();


            lblKhuVuc.Text = danhSachKhuVuc;
            khuVucBLL = new KhuVucBLL();
            khuVucBLL.CapNhatBang(dgvKhuVuc, lblKhuVuc);

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                khuVucBLL.ThemKhuVuc(txtMaDinhDanh.Text, txtTenKhuVuc.Text);
                MessageBox.Show("Thêm thành công!");
                TenKhuVuc?.Invoke(txtTenKhuVuc.Text);
                khuVucBLL.CapNhatBang(dgvKhuVuc, lblKhuVuc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(dgvKhuVuc.SelectedRows[0].Cells[0].Value.ToString());
            khuVucBLL.XoaKhuVuc(ma);
            khuVucBLL.CapNhatBang(dgvKhuVuc, lblKhuVuc);
            MessageBox.Show("Xóa thành công!");
        }

        private void dgvKhuVuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaDinhDanh.Text = dgvKhuVuc.SelectedRows[0].Cells[1].Value.ToString();
            txtTenKhuVuc.Text = dgvKhuVuc.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(dgvKhuVuc.SelectedRows[0].Cells[0].Value.ToString());
            khuVucBLL.SuaKhuVuc(ma, txtMaDinhDanh.Text, txtTenKhuVuc.Text);
            khuVucBLL.CapNhatBang(dgvKhuVuc, lblKhuVuc);
            MessageBox.Show("Sửa thành công!");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
