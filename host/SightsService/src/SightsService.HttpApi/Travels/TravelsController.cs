using Microsoft.AspNetCore.Mvc;
using SightsService.TravelsManage.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace SightsService.TravelsManage
{
    [Area(SightsServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = SightsServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/travels-manage")]
    public class TravelsController : SightsServiceController, ITravelsAppService
    {
        private readonly ITravelsAppService _travelsAppService;

        public TravelsController(ITravelsAppService travelsAppService)
        {
            _travelsAppService = travelsAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<TravelsDto> CreateAsync(TravelsCreateUpdateDto input)
        {
            return _travelsAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _travelsAppService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("all-by-ids")]
        public Task<List<TravelsDto>> GetActivityListByIdsAsync(List<string> ids)
        {
            return _travelsAppService.GetActivityListByIdsAsync(ids);
        }

        [HttpGet]
        [Route("get")]
        public Task<TravelsDto> GetAsync(Guid id)
        {
            return _travelsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("get-count-by-user")]
        public Task<int> GetCountByUserIdAsync(string id)
        {
            return _travelsAppService.GetCountByUserIdAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<TravelsDto>> GetListAsync(PageListAndSortedRequestDto input)
        {
            return _travelsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("all-by-createid")]
        public Task<PagedResultDto<TravelsDto>> GetPagedByCreateIdAsync(Guid createId, PageListAndSortedRequestDto input)
        {
            return _travelsAppService.GetPagedByCreateIdAsync(createId, input);
        }

        [HttpPost]
        [Route("update")]
        public Task<TravelsDto> UpdateAsync(Guid id, TravelsCreateUpdateDto input)
        {
            return _travelsAppService.UpdateAsync(id, input);
        }
    }
}
