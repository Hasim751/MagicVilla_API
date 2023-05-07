using AutoMapper;
using MagicVilla_VillaAAPI.Model;
using MagicVilla_VillaAAPI.Models.Dto;

namespace MagicVilla_VillaAAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();
            CreateMap<VillaDto, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDTO>().ReverseMap();
        }
    }
}
