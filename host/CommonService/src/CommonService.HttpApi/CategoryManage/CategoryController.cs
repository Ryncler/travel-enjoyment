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
    [Route("api/category-manage")]
    public class CategoryController:CommonServiceController,ICategoryAppService
    {
        private readonly ICategoryAppService _categoryAppService;
        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<CategoryDto> CreateAsync(CategoryCreateUpdateDto input)
        {
            return _categoryAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _categoryAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<CategoryDto> GetAsync(Guid id)
        {
            return _categoryAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("get-tree")]
        public Task<PagedResultDto<CategoryTreeDto>> GetCategoryTrees(PageListAndSortedRequestDto input)
        {
            return _categoryAppService.GetCategoryTrees(input);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<CategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _categoryAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<CategoryDto> UpdateAsync(Guid id, CategoryCreateUpdateDto input)
        {
            return _categoryAppService.UpdateAsync(id, input);
        }
    }
}
