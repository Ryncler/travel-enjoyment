using CommonService.Geo.Dtos;
using CommonService.GeoManage.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace CommonService.GeoManage
{
    [Area(CommonServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = CommonServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/geo-manage")]
    public class GeoController : CommonServiceController, IGeoInfoAppService
    {
        private readonly IGeoInfoAppService _geoInfoAppService;

        public GeoController(IGeoInfoAppService geoInfoAppService)
        {
            _geoInfoAppService = geoInfoAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<GeoInfoDto> CreateAsync(GeoInfoCreateUpdateDto input)
        {
            return _geoInfoAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("create-many")]
        public Task<bool> CreateManyAsync(List<GeoInfoCreateUpdateDto> inputs)
        {
            return _geoInfoAppService.CreateManyAsync(inputs);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _geoInfoAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<GeoInfoDto> GetAsync(Guid id)
        {
            return _geoInfoAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("get-tree")]
        public Task<List<GeoTreeDto>> GetGeoTreeAsync()
        {
            return _geoInfoAppService.GetGeoTreeAsync();
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<GeoInfoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _geoInfoAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<GeoInfoDto> UpdateAsync(Guid id, GeoInfoCreateUpdateDto input)
        {
            return _geoInfoAppService.UpdateAsync(id, input);
        }
    }
}
