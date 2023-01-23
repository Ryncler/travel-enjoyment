using SightsService.SightsManage;
using SightsService.SightsManage.Dtos;
using SightsService.TravelsManage;
using SightsService.TravelsManage.Dtos;
using SightsService.ActivityManage;
using SightsService.ActivityManage.Dtos;
using SightsService.CommentManage;
using SightsService.CommentManage.Dtos;
using AutoMapper;

namespace SightsService;

public class SightsServiceApplicationAutoMapperProfile : Profile
{
    public SightsServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Sights, SightsDto>();
        CreateMap<SightsCreateUpdateDto, Sights>(MemberList.Source);
        CreateMap<Travels, TravelsDto>();
        CreateMap<TravelsCreateUpdateDto, TravelsDto>(MemberList.Source);

        CreateMap<TravelsCreateUpdateDto, Travels>(MemberList.Source);

        CreateMap<Activity, ActivityDto>();
        CreateMap<ActivityCreateUpdateDto, Activity>(MemberList.Source);

        CreateMap<Comment, CommentDto>();
        CreateMap<CommentCreateUpdateDto, Comment>(MemberList.Source);

        CreateMap<SightsTravels, SightsTravelsDto>();
        CreateMap<SightsTravelsCreateUpdateDto, SightsTravels>(MemberList.Source);

        CreateMap<SightsActivity, SightsActivityDto>();
        CreateMap<SightsActivityCreateUpdateDto, SightsActivity>(MemberList.Source);

        CreateMap<TravelsExtention, TravelsExtentionDto>();
        CreateMap<TravelsExtentionCreateUpdateDto, TravelsExtention>(MemberList.Source);
    }
}
