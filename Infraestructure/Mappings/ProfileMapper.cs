using Aplication.DTOs;
using Aplication.Entities;
using AutoMapper;

namespace Infraestructure.Mappings
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Role, RoleDTO>().ReverseMap();

        }
    }
}
