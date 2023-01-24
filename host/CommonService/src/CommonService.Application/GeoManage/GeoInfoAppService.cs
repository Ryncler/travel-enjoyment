using System;
using CommonService.Permissions;
using CommonService.GeoManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CommonService.GeoManage;


public class GeoInfoAppService : CrudAppService<GeoInfo, GeoInfoDto, Guid, PagedAndSortedResultRequestDto, GeoInfoCreateUpdateDto, GeoInfoCreateUpdateDto>,
    IGeoInfoAppService
{
    //protected override string GetPolicyName { get; set; } = CommonServicePermissions.GeoInfo.Default;
    //protected override string GetListPolicyName { get; set; } = CommonServicePermissions.GeoInfo.Default;
    //protected override string CreatePolicyName { get; set; } = CommonServicePermissions.GeoInfo.Create;
    //protected override string UpdatePolicyName { get; set; } = CommonServicePermissions.GeoInfo.Update;
    //protected override string DeletePolicyName { get; set; } = CommonServicePermissions.GeoInfo.Delete;

    private readonly IGeoInfoRepository _repository;

    public GeoInfoAppService(IGeoInfoRepository repository) : base(repository)
    {
        _repository = repository;
    }

}
