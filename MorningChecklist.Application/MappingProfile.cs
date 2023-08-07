using AutoMapper;
using MorningChecklist.Infrastructure.Entities;
using MorningChecklist.Domain.Models;

namespace MorningChecklist.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserMorningChecklistEntity, UserMorningChecklistModel>();
            CreateMap<UserEntity, UserModel>().ForMember(dest => dest.MorningChecklists, opt => opt.MapFrom(src => src.UserMorningChecklists));
            CreateMap<TeamEntity, TeamModel>();
            CreateMap<TeamEntity, TeamUserModel>();
            CreateMap<MorningChecklistItemEntity, MorningChecklistItemModel>();
        }
    }
}
