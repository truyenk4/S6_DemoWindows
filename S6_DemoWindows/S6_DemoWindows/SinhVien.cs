using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S6_DemoWindows
{
    internal class SinhVien
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Faculty { get; set; }
        public double GPA { get; set; }

        public SinhVien(string id, string fullName, string faculty, double gpa)
        {
            ID = id;
            FullName = fullName;
            Faculty = faculty;
            GPA = gpa;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Mã số: {ID}, Họ tên: {FullName}, Khoa: {Faculty}, Điểm trung bình: {GPA}");
        }

    }
}
