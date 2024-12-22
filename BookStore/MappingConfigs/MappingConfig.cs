using AutoMapper;
using BookStore.DTOs.AdminDTOs;
using BookStore.DTOs.BookDTOs;
using BookStore.DTOs.CustomerDTOs;
using BookStore.DTOs.OrderDTOs;
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
            CreateMap<Book, AddBookDTO>().ReverseMap();
            CreateMap<Book, DisplayBookDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.AuthorName = src.Author.Name;
                    dest.CatalogName = src.Catalog.Name;
                });
            CreateMap<Order, EditOrderDTO>().ReverseMap();
            CreateMap<Order, DisplayOrderDTO>().AfterMap(
                (src, dest) =>
                {
                    dest.cust_name = src.Customer.Name;
                });

        }
    }
}
