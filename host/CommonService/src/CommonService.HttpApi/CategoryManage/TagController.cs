using CommonService.CategoryManage.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace CommonService.CategoryManage
{
    [Area(CommonServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = CommonServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/tag-manage")]
    public class TagController : CommonServiceController, ITagAppService
    {
        private readonly ITagAppService _tagAppService;

        public TagController(ITagAppService tagAppService)
        {
            _tagAppService = tagAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<TagDto> CreateAsync(TagCreateUpdateDto input)
        {
            return _tagAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _tagAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<TagDto> GetAsync(Guid id)
        {
            return _tagAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<TagDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _tagAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("all-by-name")]
        public Task<List<TagDto>> GetListByName(string name)
        {
            return _tagAppService.GetListByName(name);
        }

        [HttpPost]
        [Route("all-by-ids")]
        public Task<List<TagDto>> GetTagListByIdsAsync(List<string> ids)
        {
            return _tagAppService.GetTagListByIdsAsync(ids);
        }

        [HttpPost]
        [Route("update")]
        public Task<TagDto> UpdateAsync(Guid id, TagCreateUpdateDto input)
        {
            return _tagAppService.UpdateAsync(id, input);
        }
    }
}
