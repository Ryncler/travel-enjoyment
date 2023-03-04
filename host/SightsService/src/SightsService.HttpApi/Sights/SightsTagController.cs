using Microsoft.AspNetCore.Mvc;
using SightsService.SightsManage;
using SightsService.SightsManage.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace SightsService.Sights
{
    [Area(SightsServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = SightsServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/sights-tag-manage")]
    public class SightsTagController : SightsServiceController, ISightsTagAppService
    {
        private readonly ISightsTagAppService _sightsTagAppService;
        public SightsTagController(ISightsTagAppService sightsTagAppService)
        {
            _sightsTagAppService = sightsTagAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<SightsTagDto> CreateAsync(SightsTagCreateUpdateDto input)
        {
            return _sightsTagAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("create-many")]
        public Task<List<SightsTagDto>> CreateManyAsync(List<SightsTagCreateUpdateDto> input)
        {
            return _sightsTagAppService.CreateManyAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(SightsTagKey id)
        {
            return _sightsTagAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("all-by-sightsid")]
        public Task<List<SightsTagDto>> GetAllBySightsId(string id)
        {
            return _sightsTagAppService.GetAllBySightsId(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<SightsTagDto> GetAsync(SightsTagKey id)
        {
            return _sightsTagAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<SightsTagDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _sightsTagAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<SightsTagDto> UpdateAsync(SightsTagKey id, SightsTagCreateUpdateDto input)
        {
            return _sightsTagAppService.UpdateAsync(id, input);
        }
    }
}
