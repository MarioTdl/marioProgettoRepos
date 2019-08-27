using AutoMapper;
using marioProgetto.Controllers.Resource;
using marioProgetto.Models;

namespace marioProgetto.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make,MakeResource>();
            CreateMap<Model,ModelResource>();
         }
    }
}