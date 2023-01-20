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

        [HttpPost]
        [Route("update")]
        public Task<SightsDto> UpdateAsync(Guid id, SightsCreateUpdateDto input)
        {
            return _sightsAppService.UpdateAsync(id, input);
        }
    }
}
