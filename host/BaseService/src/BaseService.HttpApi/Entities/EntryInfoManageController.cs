using BaseService.EntryInfos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace BaseService.Entities
{
    [Area(BaseServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = BaseServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/entryinfo-manage")]
    public class EntryInfoManageController : BaseServiceController, IEntryInfoManageAppService
    {
        private readonly IEntryInfoManageAppService _entryInfoManageAppService;
        public EntryInfoManageController(IEntryInfoManageAppService entryInfoManageAppService)
        {
            _entryInfoManageAppService = entryInfoManageAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<EntryInfoDto> CreateAsync(EntryInfoCreateUpdateDto input)
        {
            return _entryInfoManageAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _entryInfoManageAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get-status")]
        public List<EnumInfoDto> GetApplyStatus()
        {
            return _entryInfoManageAppService.GetApplyStatus();
        }

        [HttpGet]
        [Route("get")]
        public Task<EntryInfoDto> GetAsync(Guid id)
        {
            return _entryInfoManageAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<EntryInfoDto>> GetListAsync(PageListAndSortedRequestDto input)
        {
            return _entryInfoManageAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<EntryInfoDto> UpdateAsync(Guid id, EntryInfoCreateUpdateDto input)
        {
            return _entryInfoManageAppService.UpdateAsync(id, input);
        }
    }
}
