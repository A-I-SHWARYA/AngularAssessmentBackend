using Angularassessment.Dto;
using Angularassessment.Models;
using AutoMapper;

namespace Angularassessment
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Field, Fielddto>();
            CreateMap<Fielddto, Field>();
        }
    }
    
}
