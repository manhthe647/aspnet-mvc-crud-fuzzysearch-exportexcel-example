using AutoMapper;
using QuanLySinhVien.WebAppMvc.Models;
using QuanLySinhVien.WebAppMvc.ViewModel;

namespace QuanLySinhVien.WebAppMvc.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg=>
            {
                cfg.AddProfile(new ViewModelToModelMappingProfile());
            });
        }
    }
}
