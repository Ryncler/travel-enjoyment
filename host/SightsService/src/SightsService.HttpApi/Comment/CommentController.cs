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
    public class CommentController:SightsServiceController,ICommentAppService
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
        [Route("get")]
        public Task<CommentDto> GetAsync(Guid id)
        {
            return _commentAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<CommentDto>> GetListAsync(PageListAndSortedRequestDto input)
        {
            return _commentAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<CommentDto> UpdateAsync(Guid id, CommentCreateUpdateDto input)
        {
            return _commentAppService.UpdateAsync(id, input);
        }
    }
}
