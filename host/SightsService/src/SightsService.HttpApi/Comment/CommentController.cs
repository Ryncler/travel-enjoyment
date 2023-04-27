using Microsoft.AspNetCore.Mvc;
using SightsService.CommentManage.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace SightsService.CommentManage
{
    [Area(SightsServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = SightsServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/comment-manage")]
    public class CommentController : SightsServiceController, ICommentAppService
    {
        private readonly ICommentAppService _commentAppService;

        public CommentController(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<CommentDto> CreateAsync(CommentCreateUpdateDto input)
        {
            return _commentAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _commentAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("all-by-travel")]
        public Task<List<CommentDto>> GetAllByTravelIdAsync(string id)
        {
            return _commentAppService.GetAllByTravelIdAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<CommentDto> GetAsync(Guid id)
        {
            return _commentAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all-by-comment")]
        public Task<List<CommentDto>> GetCommentByParentAsync(string id)
        {
            return _commentAppService.GetCommentByParentAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<CommentDto>> GetListAsync(PageListAndSortedRequestDto input)
        {
            return _commentAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("get-tree-by-travel")]
        public Task<PagedResultDto<CommentTreeDto>> GetPagedCommentTreeByTravelIdAsync(PagedCommentByTravelDto input)
        {
            return _commentAppService.GetPagedCommentTreeByTravelIdAsync(input);
        }

        [HttpGet]
        [Route("get-count")]
        public Task<int> GetTravelCommentCountAsync(string id)
        {
            return _commentAppService.GetTravelCommentCountAsync(id);
        }

        [HttpPost]
        [Route("update")]
        public Task<CommentDto> UpdateAsync(Guid id, CommentCreateUpdateDto input)
        {
            return _commentAppService.UpdateAsync(id, input);
        }
    }
}
