using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.WebAppMvc.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }

        public decimal GPA { get; set; }
        public int AdvisorId { get; set; }

    }
}
