using AutoMapper;
using QuanLySinhVien.WebAppMvc.Models;
using QuanLySinhVien.WebAppMvc.ViewModel;

namespace QuanLySinhVien.WebAppMvc.AutoMapper
{
    public class ModelToViewModelMappingProfile: Profile
    {
        public ModelToViewModelMappingProfile()
        {
            CreateMap<Professor, ProfessorVm>();
            CreateMap<Student, StudentVm>();
            CreateMap<Course, CourseVm>();


        }
    }
}
