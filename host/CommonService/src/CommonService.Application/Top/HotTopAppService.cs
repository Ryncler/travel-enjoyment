using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonService.Enum;
using CommonService.Top.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
namespace CommonService.Top;


public class HotTopAppService : CrudAppService<HotTop, HotTopDto, Guid, PagedAndSortedResultRequestDto, HotTopCreateUpdateDto, HotTopCreateUpdateDto>,
    IHotTopAppService
{
    private readonly Dictionary<HotTopType, int> _topNum;
    private readonly IHotTopRepository _repository;

    public HotTopAppService(IHotTopRepository repository) : base(repository)
    {
        _topNum = new Dictionary<HotTopType, int>();
        _topNum.Add(HotTopType.Activity, 6);
        _topNum.Add(HotTopType.Sights, 6);
        _topNum.Add(HotTopType.Travel, 9);
        _topNum.Add(HotTopType.Tag, 10);
        _repository = repository;
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

    public async Task<List<HotTopDto>> GetByHotTopType(HotTopType topType)
    {
        var result = new List<HotTopDto>();
        var hotTops = new List<HotTop>();
        switch (topType)
        {
            case HotTopType.Sights:
                var sights = await _repository.GetListAsync(x => x.TopType.Equals(HotTopType.Sights));
                hotTops = sights.OrderByDescending(x => x.Weight).Take(_topNum[HotTopType.Sights]).ToList();
                result = ObjectMapper.Map<List<HotTop>, List<HotTopDto>>(hotTops);
                break;
            case HotTopType.Travel:
                var travels = await _repository.GetListAsync(x => x.TopType.Equals(HotTopType.Travel));
                hotTops = travels.OrderByDescending(x => x.Weight).Take(_topNum[HotTopType.Travel]).ToList();
                result = ObjectMapper.Map<List<HotTop>, List<HotTopDto>>(hotTops);
                break;
            case HotTopType.Tag:
                var tags = await _repository.GetListAsync(x => x.TopType.Equals(HotTopType.Tag));
                hotTops = tags.OrderByDescending(x => x.Weight).Take(_topNum[HotTopType.Tag]).ToList();
                result = ObjectMapper.Map<List<HotTop>, List<HotTopDto>>(hotTops);
                break;
            case HotTopType.Activity:
                var activity = await _repository.GetListAsync(x => x.TopType.Equals(HotTopType.Activity));
                hotTops = activity.OrderByDescending(x => x.Weight).Take(_topNum[HotTopType.Activity]).ToList();
                result = ObjectMapper.Map<List<HotTop>, List<HotTopDto>>(hotTops);
                break;
            default:
                break;
        }
        return result;
    }
}
