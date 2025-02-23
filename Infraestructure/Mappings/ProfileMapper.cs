using Aplication.DTOs;
using Aplication.DTOs.Address;
using Aplication.DTOs.Cards;
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
            CreateMap<User, UserWithAddressDTO>().ReverseMap();
            CreateMap<User, UserMinimalDTO>().ReverseMap();

            CreateMap<UserDirection, AddressDTO>().ReverseMap();
            CreateMap<UserDirection, AddressWithUserDTO>().ReverseMap();
            CreateMap<City, CityDTO>()
                .ForMember(dest => dest.NameCity, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            CreateMap<Departament, DepartamentDTO>()
                .ForMember(departament => departament.NameDepartament, dto => dto.MapFrom(src => src.Name))
                .ReverseMap();
            CreateMap<Country, CountryDTO>()
                .ForMember(country => country.CountryName, dto => dto.MapFrom(src => src.Name))
                .ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<State, StateDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductBasicInfoDTO>().ReverseMap();
            CreateMap<Product, ProductWithAllDataDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<ProductCategory, CategoryProductDTO>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();

            CreateMap<Review, ReviewDTO>()
                .ReverseMap();

            CreateMap<Order, OrderBasicDTO>().ReverseMap();
            CreateMap<OrdersProducts, OrdersProductDTO>().ReverseMap();
            CreateMap<CreateOrderDTO, Order>().ReverseMap();
            CreateMap<Delivery, DeliveryBasicInfoDTO>().ReverseMap();
            CreateMap<OrdersProducts, OrderProductCompleteInfoDTO>()
                .ReverseMap();
            CreateMap<Order, OrderCompleteInfoDTO>().ReverseMap();
            CreateMap<Order, CreateOrderWithFactusDTO>().ReverseMap();

            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<CreateCardDTO, Card>().ReverseMap();
            CreateMap<CardType, CardTypeDTO>().ReverseMap();
            CreateMap<CardInfoDTO, Card>().ReverseMap();
        }
    }
}
