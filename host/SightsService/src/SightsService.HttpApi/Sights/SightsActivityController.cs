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
    [Route("api/sights-activity-manage")]
    public class SightsActivityController : SightsServiceController, ISightsActivityAppService
    {
        private readonly ISightsActivityAppService _sightsActivityAppService;

        public SightsActivityController(ISightsActivityAppService sightsActivityAppService)
        {
            _sightsActivityAppService = sightsActivityAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<SightsActivityDto> CreateAsync(SightsActivityCreateUpdateDto input)
        {
            return _sightsActivityAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(SightsActivityKey id)
        {
            return _sightsActivityAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("all-by-sightsid")]
        public Task<List<SightsActivityDto>> GetAllBySightsIdAsync(string id)
        {
            return _sightsActivityAppService.GetAllBySightsIdAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<SightsActivityDto> GetAsync(SightsActivityKey id)
        {
            return _sightsActivityAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<SightsActivityDto>> GetListAsync(PageListAndSortedRequestDto input)
        {
            return _sightsActivityAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<SightsActivityDto> UpdateAsync(SightsActivityKey id, SightsActivityCreateUpdateDto input)
        {
            return _sightsActivityAppService.UpdateAsync(id, input);
        }
    }
}
