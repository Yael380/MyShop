using AutoMapper;
using Entities;
using DTO;

namespace MyShop
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Category, GetCategoryDTO>();
            CreateMap<Order, GetOrderDTO>();
            CreateMap<Order, PostOrderDTO>();
            CreateMap<Product, GetProductDTO>();
            //CreateMap<Product, GetProductIdDTO>();
            CreateMap<User, GetUserDTO>();
            CreateMap<OrderItem, GetOrderItemDTO>();
        }
    }
}
