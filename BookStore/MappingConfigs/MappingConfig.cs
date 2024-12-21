using AutoMapper;
using BookStore.DTOs.AdminDTOs;
using BookStore.DTOs.CustomerDTOs;
using BookStore.Models;

namespace BookStore.MppingConfigs
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Customer, AddCustomerDTO>().ReverseMap();
            CreateMap<Customer, DisplayCustomerDTO>().ReverseMap();
            CreateMap<Admin, AddAdminDTO>().ReverseMap();
           // CreateMap<Employee, DisplayEmployeeDTO>().ReverseMap();
            //CreateMap<Employee, DisplayEmployeeDTO>().AfterMap(
            //    (src, dest) =>
            //    {
            //        dest.departmentName = src.dept.name;
            //    });
        }
    }
}
