using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S6_DemoWindows
{
    internal class Program
    {
        static List<SinhVien> sinhViens = new List<SinhVien>();
        
        static void Main(string[] args)
        {

            ////thiết lập hiển thị Tiếng Việt 
            Console.OutputEncoding = Encoding.Unicode;

            int choice;
            do
            {
                Console.WriteLine("\n--- Menu Quản Lý Sinh Viên ---");
                Console.WriteLine("1 - Thêm sinh viên");
                Console.WriteLine("2 - Xuất danh sách sinh viên");
                Console.WriteLine("0 - Thoát");
                Console.Write("Chọn chức năng: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        DisplayStudents();
                        break;
                    case 0:
                        Console.WriteLine("Thoát chương trình.");
                        break;
                    default:
                        Console.WriteLine("Chọn không hợp lệ, vui lòng thử lại.");
                        break;
                }
            } while (choice != 0);



            ////thiết lập hiển thị Tiếng Việt 
            //Console.OutputEncoding = Encoding.Unicode;

            //// Khởi tạo random và phát sinh số ngẫu nhiên từ 100 đến 999
            //Random random = new Random();
            //int targetNumber = random.Next(100, 1000);
            //string targetString = targetNumber.ToString();

            //int attempts = 7;
            //bool isGuessedCorrectly = false;

            //Console.WriteLine("Chào mừng bạn đến với trò chơi đoán số! Bạn có 7 lần đoán. ");

            //while (attempts > 0 && !isGuessedCorrectly)
            //{
            //    Console.Write($"Nhập số có 3 chữ số (Còn {attempts} lần đoán): ");
            //    string userGuess = Console.ReadLine();

            //    // Kiểm tra đầu vào hợp lệ
            //    if (userGuess.Length != 3 || !int.TryParse(userGuess, out int guessedNumber) || guessedNumber < 100 || guessedNumber > 999)
            //    {
            //        Console.WriteLine("Vui lòng nhập số có 3 chữ số hợp lệ.");
            //        continue;
            //    }

            //    string feedback = GetFeedback(targetString, userGuess);
            //    Console.WriteLine("Phản hồi: " + feedback);

            //    if (feedback == "+++")
            //    {
            //        isGuessedCorrectly = true;
            //    }

            //    attempts--;
            //}

            //if (isGuessedCorrectly)
            //{
            //    Console.WriteLine($"Chúc mừng! Bạn đã đoán đúng số {targetNumber}.");
            //}
            //else
            //{
            //    Console.WriteLine($"Rất tiếc, bạn đã hết lượt. Số đúng là {targetNumber}.");
            //}

            //dừng màn hình 
            Console.ReadLine();
        }

        static void AddStudent()
        {
            Console.Write("Nhập mã số sinh viên: ");
            string id = Console.ReadLine();
            Console.Write("Nhập họ tên sinh viên: ");
            string fullName = Console.ReadLine();
            Console.Write("Nhập khoa: ");
            string faculty = Console.ReadLine();
            Console.Write("Nhập điểm trung bình: ");
            double gpa = double.Parse(Console.ReadLine());

            SinhVien newStudent = new SinhVien(id, fullName, faculty, gpa);
            sinhViens.Add(newStudent);
            Console.WriteLine("Thêm sinh viên thành công!");
        }

        static void DisplayStudents()
        {
            if (sinhViens.Count == 0)
            {
                Console.WriteLine("Danh sách sinh viên trống.");
            }
            else
            {
                Console.WriteLine("\n--- Danh Sách Sinh Viên ---");
                foreach (var student in sinhViens)
                {
                    student.DisplayInfo();
                }
            }
        }

        static string GetFeedback(string target, string guess)
        {
            string feedback = "";
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] == guess[i])
                {
                    feedback += "+";
                }
                else if (target.Contains(guess[i].ToString()))
                {
                    feedback += "?";
                }
            }

            return feedback;
        }
    }
}
