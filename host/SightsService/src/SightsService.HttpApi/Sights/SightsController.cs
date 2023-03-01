using Microsoft.AspNetCore.Mvc;
using SightsService.SightsManage.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace SightsService.SightsManage
{
    [Area(SightsServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = SightsServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/sights-manage")]
    public class SightsController : SightsServiceController, ISightsAppService
    {
        private readonly ISightsAppService _sightsAppService;

        public SightsController(ISightsAppService sightsAppService)
        {
            _sightsAppService = sightsAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<SightsDto> CreateAsync(SightsCreateUpdateDto input)
        {
            return _sightsAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _sightsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<SightsDto> GetAsync(Guid id)
        {
            return _sightsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<SightsDto>> GetListAsync(PageListAndSortedRequestDto input)
        {
            return _sightsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("all-by-createid")]
        public Task<PagedResultDto<SightsDto>> GetListByCreateIdAsync(Guid createId, PageListAndSortedRequestDto input)
        {
            return _sightsAppService.GetListByCreateIdAsync(createId, input);
        }

        [HttpGet]
        [Route("get-by-activity")]
        public Task<SightsDto> GetSightsByActivityId(string id)
        {
            return _sightsAppService.GetSightsByActivityId(id);
        }

        [HttpGet]
        [Route("get-by-search")]
        public Task<List<SightsDto>> GetSightsBySearch(SightsSerachDto input)
        {
            return _sightsAppService.GetSightsBySearch(input);
        }

        [HttpPost]
        [Route("all-by-ids")]
        public Task<List<SightsDto>> GetSightsListByIdsAsync(List<string> ids)
        {
            return _sightsAppService.GetSightsListByIdsAsync(ids);
        }

        [HttpPost]
        [Route("update")]
        public Task<SightsDto> UpdateAsync(Guid id, SightsCreateUpdateDto input)
        {
            return _sightsAppService.UpdateAsync(id, input);
        }
    }
}
