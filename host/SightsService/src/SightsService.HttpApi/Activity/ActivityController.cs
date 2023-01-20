using Microsoft.AspNetCore.Mvc;
using SightsService.ActivityManage.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace SightsService.ActivityManage
{
    [Area(SightsServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = SightsServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/activity-manage")]
    public class ActivityController:SightsServiceController,IActivityAppService
    {
        private readonly IActivityAppService _activityAppService;

        public ActivityController(IActivityAppService activityAppService)
        {
            _activityAppService = activityAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<ActivityDto> CreateAsync(ActivityCreateUpdateDto input)
        {
            return _activityAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _activityAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<ActivityDto> GetAsync(Guid id)
        {
            return _activityAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<ActivityDto>> GetListAsync(PageListAndSortedRequestDto input)
        {
            return _activityAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<ActivityDto> UpdateAsync(Guid id, ActivityCreateUpdateDto input)
        {
            return _activityAppService.UpdateAsync(id, input);
        }
    }
}
