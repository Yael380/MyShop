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
            CreateMap<PostOrderDTO, Order>();
            CreateMap<Product, GetProductDTO>();
            //CreateMap<Product, GetProductIdDTO>();
            CreateMap<User, GetUserDTO>();
            CreateMap<User, PostUserDTO>().ReverseMap();
            CreateMap<OrderItem, GetOrderItemDTO>().ReverseMap();
        }
    }
}
