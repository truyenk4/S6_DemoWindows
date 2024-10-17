using QuanLyBanVe.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVe.BLL
{
    public class KhuVucBLL
    {
        private KhuVucDAL khuVucDAL;

        public KhuVucBLL()
        {
            khuVucDAL = new KhuVucDAL();
        }

        // Lấy danh sách khu vực
        public List<KhuVuc> LayDanhSachKhuVuc()
        {
            return khuVucDAL.GetAllKhuVuc();
        }

        // Thêm mới khu vực
        public void ThemKhuVuc(string maDinhDanh, string ten)
        {
            if (string.IsNullOrEmpty(maDinhDanh) || string.IsNullOrEmpty(ten))
            {
                throw new Exception("Mã định danh và tên khu vực không được để trống");
            }

            KhuVuc khuVuc = new KhuVuc
            {
                MaDinhDanh = maDinhDanh,
                Ten = ten
            };

            khuVucDAL.AddKhuVuc(khuVuc);
        }

        // Sửa thông tin khu vực
        public void SuaKhuVuc(int maKhuVuc, string maDinhDanh, string ten)
        {
            KhuVuc khuVuc = new KhuVuc
            {
                Ma = maKhuVuc,
                MaDinhDanh = maDinhDanh,
                Ten = ten
            };

            khuVucDAL.UpdateKhuVuc(khuVuc);
        }

        // Xóa khu vực
        public void XoaKhuVuc(int maKhuVuc)
        {
            khuVucDAL.DeleteKhuVuc(maKhuVuc);
        }

        public void CapNhatBang(DataGridView dgvKhuVuc, Label lblKhuVuc)
        {
            khuVucDAL.BindGrid(dgvKhuVuc);
            khuVucDAL.FillKhuVucDaCo(lblKhuVuc);
        }
    }

}
