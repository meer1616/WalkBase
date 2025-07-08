using Authentication.Models.Domain;
using Authentication.Models.DTO;
using AutoMapper;

namespace Authentication.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionReqDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();
            CreateMap<AddWalkRequestDto,Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<UpdateWalkReqDto,Walk>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

        }
    }
}
