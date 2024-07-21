using Aplication.DTOs;
using Aplication.DTOs.Orders;
using Aplication.DTOs.Products;
using Aplication.DTOs.Users;
using Aplication.Entities;
using AutoMapper;

namespace Infraestructure.Mappings
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<State, StateDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductBasicInfoDTO>().ReverseMap();
            CreateMap<Product, ProductWithAllDataDTO>().ReverseMap();

            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();

            CreateMap<Review, ReviewDTO>()
                .ReverseMap();

            CreateMap<Order, OrderBasicDTO>().ReverseMap();
            CreateMap<OrdersProducts, OrdersProductDTO>().ReverseMap();
            CreateMap<CreateOrderDTO, Order>().ReverseMap();
            CreateMap<Delivery, DeliveryBasicInfoDTO>().ReverseMap();
            CreateMap<OrdersProducts, OrderProductCompleteInfoDTO>()
                .ReverseMap();
            CreateMap<Order, OrderCompleteInfoDTO>().ReverseMap();
        }
    }
}
