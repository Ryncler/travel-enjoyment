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
    [Route("api/sights-travels-manage")]
    public class SightsTravelsController:SightsServiceController,ISightsTravelsAppService
    {
        private readonly ISightsTravelsAppService _sightsTravelsAppService;

        public SightsTravelsController(ISightsTravelsAppService sightsTravelsAppService)
        {
            _sightsTravelsAppService = sightsTravelsAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<SightsTravelsDto> CreateAsync(SightsTravelsCreateUpdateDto input)
        {
            return _sightsTravelsAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(SightsTravelsKey id)
        {
            return _sightsTravelsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("all-by-sightsid")]
        public Task<List<SightsTravelsDto>> GetAllBySightsId(string id)
        {
            return _sightsTravelsAppService.GetAllBySightsId(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<SightsTravelsDto> GetAsync(SightsTravelsKey id)
        {
            return _sightsTravelsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<SightsTravelsDto>> GetListAsync(PageListAndSortedRequestDto input)
        {
            return _sightsTravelsAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<SightsTravelsDto> UpdateAsync(SightsTravelsKey id, SightsTravelsCreateUpdateDto input)
        {
            return _sightsTravelsAppService.UpdateAsync(id, input);
        }
    }
}
