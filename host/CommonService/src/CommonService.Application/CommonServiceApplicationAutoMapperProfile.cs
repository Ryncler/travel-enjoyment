using CommonService.GeoManage;
using CommonService.GeoManage.Dtos;
using AutoMapper;
using CommonService.CategoryManage.Dtos;
using CommonService.CategoryManage;
using CommonService.Geo.Dtos;

namespace CommonService;

public class CommonServiceApplicationAutoMapperProfile : Profile
{
    public CommonServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<GeoInfo, GeoInfoDto>();
        CreateMap<GeoInfo, GeoTreeDto>(MemberList.None);
        CreateMap<GeoInfoCreateUpdateDto, GeoInfo>(MemberList.Source);
        CreateMap<Tag, TagDto>();
        CreateMap<TagCreateUpdateDto, Tag>(MemberList.Source);
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryCreateUpdateDto, Category>(MemberList.Source);
    }
}
