using api_dapper.Dto;
using api_dapper.Models;
using AutoMapper;

namespace api_dapper.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper() 
        {
            CreateMap<Usuario, UsuarioListarDto>();
        }
    }
}
