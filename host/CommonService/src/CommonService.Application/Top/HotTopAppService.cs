using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonService.Enum;
using CommonService.Extension;
using CommonService.Permissions;
using CommonService.Top.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
namespace CommonService.Top;


public class HotTopAppService : CrudAppService<HotTop, HotTopDto, Guid, PagedAndSortedResultRequestDto, HotTopCreateUpdateDto, HotTopCreateUpdateDto>,
    IHotTopAppService
{
    protected override string GetPolicyName { get; set; } = CommonServicePermissions.HotTop.Default;
    protected override string GetListPolicyName { get; set; } = CommonServicePermissions.HotTop.Default;
    protected override string CreatePolicyName { get; set; } = CommonServicePermissions.HotTop.Create;
    protected override string UpdatePolicyName { get; set; } = CommonServicePermissions.HotTop.Update;
    protected override string DeletePolicyName { get; set; } = CommonServicePermissions.HotTop.Delete;

    private readonly Dictionary<HotTopType, int> _topNum;
    private readonly IHotTopRepository _repository;
    private readonly IEntityFrameworkExtension _entityFrameworkExtension;

    public HotTopAppService(IHotTopRepository repository, IEntityFrameworkExtension entityFrameworkExtension) : base(repository)
    {
        _topNum = new Dictionary<HotTopType, int>();
        _topNum.Add(HotTopType.Activity, 6);
        _topNum.Add(HotTopType.Sights, 6);
        _topNum.Add(HotTopType.Travel, 9);
        _topNum.Add(HotTopType.Tag, 10);
        _repository = repository;
        _entityFrameworkExtension = entityFrameworkExtension;
    }

    public override async Task<HotTopDto> CreateAsync(HotTopCreateUpdateDto input)
    {
        var hotTop = await _repository.FindAsync(x => x.LinkId.Equals(input.LinkId));
        if (hotTop != null)
        {
            return await UpdateAsync(hotTop.Id, new HotTopCreateUpdateDto
            {
                LinkId = hotTop.LinkId,
                TopType = hotTop.TopType,
                Weight = hotTop.Weight + 1
            });
        }
        return await base.CreateAsync(input);
    }

    //[Authorize(CommonServicePermissions.HotTop.Default)]
    public async Task<List<HotTopDto>> GetByHotTopTypeAsync(HotTopType topType)
    {
        var result = new List<HotTopDto>();
        var hotTops = new List<HotTop>();
        switch (topType)
        {
            case HotTopType.Sights:
                var sights = await _repository.GetListAsync(x => x.TopType.Equals(HotTopType.Sights));
                foreach (var item in sights)
                {
                    var sql = string.Format("select count(*) from \"Sights\" where \"IsDeleted\"=false and \"Id\"='{0}'", item.LinkId);
                    var exist = await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(sql);
                    if (Convert.ToInt32(exist) > 0)
                        hotTops.Add(item);
                }
                hotTops = hotTops.OrderByDescending(x => x.Weight).Take(_topNum[HotTopType.Sights]).ToList();
                result = ObjectMapper.Map<List<HotTop>, List<HotTopDto>>(hotTops);
                break;
            case HotTopType.Travel:
                var travels = await _repository.GetListAsync(x => x.TopType.Equals(HotTopType.Travel));
                foreach (var item in travels)
                {
                    var sql = string.Format("select count(*) from \"Travels\" where \"IsDeleted\"=false and \"Id\"='{0}'", item.LinkId);
                    var exist = await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(sql);
                    if (Convert.ToInt32(exist) > 0)
                        hotTops.Add(item);
                }
                hotTops = travels.OrderByDescending(x => x.Weight).Take(_topNum[HotTopType.Travel]).ToList();
                result = ObjectMapper.Map<List<HotTop>, List<HotTopDto>>(hotTops);
                break;
            case HotTopType.Tag:
                var tags = await _repository.GetListAsync(x => x.TopType.Equals(HotTopType.Tag));
                foreach (var item in tags)
                {
                    var sql = string.Format("select count(*) from \"Tags\" where \"IsDeleted\"=false and \"Id\"='{0}'", item.LinkId);
                    var exist = await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(sql);
                    if (Convert.ToInt32(exist) > 0)
                        hotTops.Add(item);
                }
                hotTops = tags.OrderByDescending(x => x.Weight).Take(_topNum[HotTopType.Tag]).ToList();
                result = ObjectMapper.Map<List<HotTop>, List<HotTopDto>>(hotTops);
                break;
            case HotTopType.Activity:
                var activity = await _repository.GetListAsync(x => x.TopType.Equals(HotTopType.Activity));
                foreach (var item in activity)
                {
                    var sql = string.Format("select count(*) from \"Activities\" where \"IsDeleted\"=false and \"Id\"='{0}'", item.LinkId);
                    var exist = await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(sql);
                    if (Convert.ToInt32(exist) > 0)
                        hotTops.Add(item);
                }
                hotTops = activity.OrderByDescending(x => x.Weight).Take(_topNum[HotTopType.Activity]).ToList();
                result = ObjectMapper.Map<List<HotTop>, List<HotTopDto>>(hotTops);
                break;
            default:
                break;
        }
        return result;
    }
}
