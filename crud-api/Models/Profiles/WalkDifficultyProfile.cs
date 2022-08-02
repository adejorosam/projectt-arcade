using AutoMapper;
namespace crud_api.Models.Profiles
{
    public class WalkDifficultyProfile:Profile
    {
        public WalkDifficultyProfile()
        {
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.Walk>()
    .ReverseMap();

            //    CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>()
            //        .ReverseMap();
            //}
        }
    }
}

