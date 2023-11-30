namespace QuanLySinhVien.WebAppMvc.ViewModel
{
    public class StudentVm
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }

        public decimal GPA { get; set; }
        public int AdvisorId { get; set; }
    }
}
