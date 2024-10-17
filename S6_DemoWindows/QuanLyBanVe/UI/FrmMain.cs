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
    public partial class FrmMain : Form
    {
        //khởi tạo danh sách lưu các Button (các vị trí ghế đã chọn)
        List<Button> danhSachGheChon = new List<Button>();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnChonViTri_Click(object sender, EventArgs e)
        {
            //todo 
            //chuyển sender thành Button 
            Button btnViTriGhe = (Button)sender;
            //MessageBox.Show($"Chọn vị trí ghế {btnViTriGhe.Text}");
            //nếu vị trí chọn là màu vàng thì thông báo "ghế đã được mua, không thể chọn!!!"
            if (btnViTriGhe.BackColor == Color.Yellow)
            {
                MessageBox.Show("Ghế đã được mua, không thể chọn!!!");
                return; // dừng thao tác tiếp theo
            }


            // Chuyển trạng thái ghế chọn (màu xanh: đang chọn; màu trắng: bỏ chọn; màu vàng: không chọn được)
            if (btnViTriGhe.BackColor == Color.White)
            {
                btnViTriGhe.BackColor = Color.Blue;
                danhSachGheChon.Add(btnViTriGhe);
            }
            else
            {
                btnViTriGhe.BackColor = Color.White;
                danhSachGheChon.Remove(btnViTriGhe);
            }

        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            // Nếu người dùng chưa chọn vị trí ghế nào thì thông báo lỗi và kết thúc chương trình 
            if (danhSachGheChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ghế để thanh toán!");
                return;
            }



            //Tính tổng tiền dựa vào danhSachGheChon
            //từ 1 -> 5: 3000
            //và tương tự 
            int tongTien = 0;
            foreach (Button btn in danhSachGheChon)
            {
                int soGhe = Convert.ToInt32(btn.Text);
                if (soGhe <= 5)
                {
                    tongTien += 3000;
                }
                else if (soGhe <= 10)
                {
                    tongTien += 4000;
                }
                else if (soGhe <= 15)
                {
                    tongTien += 5000;
                }
                else
                {
                    tongTien += 8000;
                }
                btn.BackColor = Color.Yellow;
            }
            txtTongTien.Text = $"{tongTien} VNĐ";

            //lưu thông tin vào cơ sở dữ liệu
            try
            {
                using (var context = new S6_QuanLyBanVeEntities())
                {
                    // Bước 1: Tạo và lưu thông tin khách hàng
                    var khachHang = new KhachHang
                    {
                        Ten = txtTenKhachHang.Text,
                        SDT = txtSDT.Text,
                        GioiTinh = (rdoNam.Checked) ? "Nam" : "Nữ",
                        KhuVuc = cmbKhuVuc.Text
                    };
                    context.KhachHang.Add(khachHang);
                    context.SaveChanges();
                    // Lưu khách hàng vào cơ sở dữ liệu để lấy MaKhachHang

                    // Bước 2: Tạo và lưu thông tin hóa đơn
                    var hoaDon = new HoaDon
                    {
                        MaKhachHang = khachHang.Ma, // Sử dụng MaKhachHang vừa được lưu
                        NgayMua = DateTime.Now,
                        TongTien = (decimal)(tongTien)
                    };
                    context.HoaDon.Add(hoaDon);
                    context.SaveChanges();
                    // Lưu hóa đơn vào cơ sở dữ liệu để lấy MaHoaDon

                    // Bước 3: Lưu chi tiết hóa đơn (thông tin ghế ngồi)
                    foreach (Button btnViTriGhe in danhSachGheChon)
                    {
                        var soGhe = Convert.ToInt32(btnViTriGhe.Text);
                        int giaTien = 0;
                        if (soGhe <= 5)
                        {
                            giaTien = 3000;
                        }
                        else if (soGhe <= 10)
                        {
                            giaTien = 4000;
                        }
                        else if (soGhe <= 15)
                        {
                            giaTien = 5000;
                        }
                        else
                        {
                            giaTien = 8000;
                        }

                        var chiTietHoaDon = new ChiTietHoaDon
                        {
                            MaHoaDon = hoaDon.Ma, // Sử dụng MaHoaDon vừa được lưu
                            ViTriGhe = soGhe.ToString(),
                            GiaTien = giaTien
                        };
                        context.ChiTietHoaDon.Add(chiTietHoaDon);

                    }

                    // Lưu chi tiết hóa đơn vào cơ sở dữ liệu
                    context.SaveChanges();

                    MessageBox.Show("Lưu thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //xoa danh sach

            danhSachGheChon.Clear();
            LoadDanhSachDatVe();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //reset danh sách chọn 
            foreach (Button btn in danhSachGheChon)
            {
                btn.BackColor = Color.White;
            }
            danhSachGheChon.Clear();
            txtTongTien.Text = "0 VN?";
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            //xác nhận đóng chương trình hay không? 
            DialogResult result = MessageBox.Show("Ban có muon thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load danh sách khu vực 
            LoadKhuVuc();
            LoadDanhSachDatVe();
        }

        private void LoadKhuVuc()
        {
            using (var context = new S6_QuanLyBanVeEntities())
            {
                var danhSachKhuVuc = context.KhuVuc.ToList();
                foreach (var khuVuc in danhSachKhuVuc)
                {
                    cmbKhuVuc.Items.Add(khuVuc.Ten);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //xác nhận đóng chương trình hay không? 
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng chương trình không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Nếu người dùng chọn No, hủy sự kiện đóng form
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }

        }

        private void LoadDanhSachDatVe()
        {
            using (var context = new S6_QuanLyBanVeEntities())
            {
                // Truy vấn để lấy thông tin hóa đơn và tên khách hàng
                var danhSachDatVe = (from hd in context.HoaDon
                                     join kh in context.KhachHang on hd.MaKhachHang equals kh.Ma
                                     select new
                                     {
                                         MaHoaDon = hd.Ma,
                                         TenKhachHang = kh.Ten,
                                         TongTien = hd.TongTien,
                                         NgayMua = hd.NgayMua
                                     }).ToList();

                // Gán dữ liệu vào DataGridView
                dgvDanhSachDatVe.DataSource = danhSachDatVe;

                // Thiết lập tên cột hiển thị (tùy chỉnh cột)
                dgvDanhSachDatVe.Columns["MaHoaDon"].HeaderText = "Mã Hóa Đơn";
                dgvDanhSachDatVe.Columns["TenKhachHang"].HeaderText = "Tên Khách Hàng";
                dgvDanhSachDatVe.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dgvDanhSachDatVe.Columns["NgayMua"].HeaderText = "Ngày Mua";

                // Định dạng số tiền (nếu cần)
                dgvDanhSachDatVe.Columns["TongTien"].DefaultCellStyle.Format = "N0"; // Định dạng tiền tệ không có chữ số thập phân
            }
        }

        private void btnThemKhuVuc_Click(object sender, EventArgs e)
        {
            string danhSachKhuVuc = "";
            // lấy thông tin của khu vực đã có trong combobox cmbKhuVuc
            foreach (var item in cmbKhuVuc.Items)
            {
                danhSachKhuVuc+= item.ToString() + ", ";
            }
            //Khởi tạo FrmThemKhuVuc và truyền vào thông tin các khu vực đã có ở trong cmbKhuVuc 
            var frmThemKhuVuc = new FrmThemKhuVuc(danhSachKhuVuc);

            frmThemKhuVuc.TenKhuVuc += FrmThemKhuVuc_TenKhuVuc;


            frmThemKhuVuc.ShowDialog();

            //cmbKhuVuc.Items.Add(frmThemKhuVuc.TenKhuVuc);
        }

        private void FrmThemKhuVuc_TenKhuVuc(string tenKhuVucMoi)
        {
            //throw new NotImplementedException();
            cmbKhuVuc.Items.Add(tenKhuVucMoi);
        }
    }
}
