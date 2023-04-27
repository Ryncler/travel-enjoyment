using CommonService.Enum;
using CommonService.GeoManage;
using CommonService.Top;
using CommonService.Top.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace CommonService.HotTopManage
{
    [Area(CommonServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = CommonServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/hottop-manage")]
    public class HotTopController : CommonServiceController, IHotTopAppService
    {
        private readonly IHotTopAppService _hotTopAppService;

        public HotTopController(IHotTopAppService hotTopAppService)
        {
            _hotTopAppService = hotTopAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<HotTopDto> CreateAsync(HotTopCreateUpdateDto input)
        {
            return _hotTopAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _hotTopAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<HotTopDto> GetAsync(Guid id)
        {
            return _hotTopAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("get-by-type")]
        public Task<List<HotTopDto>> GetByHotTopTypeAsync(HotTopType topType)
        {
            return _hotTopAppService.GetByHotTopTypeAsync(topType);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<HotTopDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _hotTopAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<HotTopDto> UpdateAsync(Guid id, HotTopCreateUpdateDto input)
        {
            return _hotTopAppService.UpdateAsync(id, input);
        }
    }
}
