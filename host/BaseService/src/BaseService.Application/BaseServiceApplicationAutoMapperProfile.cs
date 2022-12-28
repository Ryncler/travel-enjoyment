using BaseService.Entities;
using BaseService.Entities.Dtos;
using AutoMapper;
using Volo.Abp.OpenIddict.Scopes;
using BaseService.OpenIddicts.Dtos;
using Volo.Abp.OpenIddict.Applications;

namespace BaseService;

public class BaseServiceApplicationAutoMapperProfile : Profile
{
    public BaseServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<UserExtension, UserExtensionDto>();
        CreateMap<UserExtensionCreateDto, UserExtension>(MemberList.Source);
        CreateMap<UserExtensionUpdateDto, UserExtension>(MemberList.Source);

        CreateMap<OpenIddictScope, OpenIddictScopeDto>();
        CreateMap<OpenIddictScopeCreateDto, OpenIddictScope>(MemberList.Source);
        CreateMap<OpenIddictScopeUpdateDto, OpenIddictScope>(MemberList.Source);

     }
}
