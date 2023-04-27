using System;
using CommonService.Permissions;
using CommonService.GeoManage.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonService.Geo.Dtos;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CommonService.GeoManage;


public class GeoInfoAppService : CrudAppService<GeoInfo, GeoInfoDto, Guid, PagedAndSortedResultRequestDto, GeoInfoCreateUpdateDto, GeoInfoCreateUpdateDto>,
    IGeoInfoAppService
{
    protected override string GetPolicyName { get; set; } = CommonServicePermissions.GeoInfo.Default;
    protected override string GetListPolicyName { get; set; } = CommonServicePermissions.GeoInfo.Default;
    protected override string CreatePolicyName { get; set; } = CommonServicePermissions.GeoInfo.Create;
    protected override string UpdatePolicyName { get; set; } = CommonServicePermissions.GeoInfo.Update;
    protected override string DeletePolicyName { get; set; } = CommonServicePermissions.GeoInfo.Delete;

    private readonly IGeoInfoRepository _repository;

    public GeoInfoAppService(IGeoInfoRepository repository) : base(repository)
    {
        _repository = repository;
    }

    [Authorize(CommonServicePermissions.GeoInfo.Create)]
    public async Task<bool> CreateManyAsync(List<GeoInfoCreateUpdateDto> inputs)
    {
        foreach (var item in inputs)
        {
            await CreateAsync(item);
        }
        return true;
    }

    [Authorize(CommonServicePermissions.GeoInfo.Default)]
    public async Task<List<GeoTreeDto>> GetGeoTreeAsync()
    {
        var data = await _repository.GetListAsync();
        var result = new List<GeoTreeDto>();
        result = data.Where(x => x.City.IsNullOrWhiteSpace() && x.Area.IsNullOrWhiteSpace()).Select(item1 =>
        {
            var province = ObjectMapper.Map<GeoInfo, GeoTreeDto>(item1);
            if (province.Name.Contains("市"))
            {
                province.Children = data.Where(y => y.Province.Equals(province.Province) && !y.City.IsNullOrWhiteSpace() && !y.Area.IsNullOrWhiteSpace()).Select(item2 =>
                {
                    return ObjectMapper.Map<GeoInfo, GeoTreeDto>(item2);
                }).ToList();
            }
            else
            {
                province.Children = data.Where(y => y.Province.Equals(province.Province) && !y.City.IsNullOrWhiteSpace() && y.Area.IsNullOrWhiteSpace()).Select(item2 =>
                {
                    var city = ObjectMapper.Map<GeoInfo, GeoTreeDto>(item2);
                    city.Children = data.Where(z => z.Province.Equals(city.Province) && !z.City.IsNullOrWhiteSpace() && z.City.Equals(city.City) && !z.Area.IsNullOrWhiteSpace()).Select(item3 =>
                    {
                        return ObjectMapper.Map<GeoInfo, GeoTreeDto>(item3);
                    }).ToList();

                    return city;
                }).ToList();
            }
            return province;
        }).ToList();
        return result;
    }

}
