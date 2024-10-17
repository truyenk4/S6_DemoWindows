using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVe.DAL
{
    public class KhuVucDAL
    {
        private S6_QuanLyBanVeEntities dbContext;

        public KhuVucDAL()
        {
            dbContext = new S6_QuanLyBanVeEntities();
        }

        // Lấy danh sách khu vực
        public List<KhuVuc> GetAllKhuVuc()
        {
            return dbContext.KhuVuc.ToList();
        }

        // Thêm mới khu vực
        public void AddKhuVuc(KhuVuc khuVuc)
        {
            //kiểm tra khu vực đã tồn tại chưa
            foreach (var item in GetAllKhuVuc())
            {
                if(item.MaDinhDanh == khuVuc.MaDinhDanh || item.Ten == khuVuc.Ten)
                {
                    throw new Exception("Trùng");
                }
            }
            
            dbContext.KhuVuc.Add(khuVuc);
            dbContext.SaveChanges();
            
            
        }

        // Sửa thông tin khu vực
        public void UpdateKhuVuc(KhuVuc khuVuc)
        {
            var existingKhuVuc = dbContext.KhuVuc.FirstOrDefault(kv => kv.Ma == khuVuc.Ma);
            if (existingKhuVuc != null)
            {
                existingKhuVuc.MaDinhDanh = khuVuc.MaDinhDanh;
                existingKhuVuc.Ten = khuVuc.Ten;
                dbContext.SaveChanges();
            }
        }

        // Xóa khu vực
        public void DeleteKhuVuc(int maKhuVuc)
        {
            var khuVuc = dbContext.KhuVuc.FirstOrDefault(kv => kv.Ma == maKhuVuc);
            if (khuVuc != null)
            {
                dbContext.KhuVuc.Remove(khuVuc);
                dbContext.SaveChanges();
            }
        }

        //Cập nhật bảng DataGridView Khu Vực
        public void BindGrid(DataGridView dgvKhuVuc)
        {
            dgvKhuVuc.Rows.Clear();
            foreach(var item in dbContext.KhuVuc.ToList())
            {
                int selectedRow = dgvKhuVuc.Rows.Add();
                dgvKhuVuc.Rows[selectedRow].Cells[0].Value = item.Ma;
                dgvKhuVuc.Rows[selectedRow].Cells[1].Value = item.MaDinhDanh;
                dgvKhuVuc.Rows[selectedRow].Cells[2].Value = item.Ten;
            }
            
        }

        public void FillKhuVucDaCo(Label lbl)
        {
            lbl.Text = "CÁC KHU VỰC ĐÃ CÓ: ";
            foreach (var item in dbContext.KhuVuc.ToList())
            {
                lbl.Text += item.Ten + ", ";
            }
        }
    }

}
