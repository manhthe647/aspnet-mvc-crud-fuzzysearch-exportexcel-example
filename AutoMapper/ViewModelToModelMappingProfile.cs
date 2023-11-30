using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using QuanLySinhVien.WebAppMvc.Models;
using QuanLySinhVien.WebAppMvc.ViewModel;

namespace QuanLySinhVien.WebAppMvc.AutoMapper
{
    public class ViewModelToModelMappingProfile: Profile
    {
        public ViewModelToModelMappingProfile()
        {
            CreateMap<ProfessorVm, Professor>();
            CreateMap<StudentVm, Student>();
            CreateMap<CourseVm, Course>();



        }
    }
}
