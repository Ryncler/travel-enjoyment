using Microsoft.AspNetCore.Mvc;
using SightsService.TravelsManage;
using SightsService.TravelsManage.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace SightsService.Travels
{
    [Area(SightsServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = SightsServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("api/user-choice-travel-manage")]
    public class UserChoiceTravelController : SightsServiceController, IUserChoiceTravelsAppService
    {
        private readonly IUserChoiceTravelsAppService _userChoiceTravelsAppService;

        public UserChoiceTravelController(IUserChoiceTravelsAppService userChoiceTravelsAppService)
        {
            _userChoiceTravelsAppService = userChoiceTravelsAppService;
        }

        [HttpPost]
        [Route("create")]
        public Task<UserChoiceTravelsDto> CreateAsync(UserChoiceTravelsCreateUpdateDto input)
        {
            return _userChoiceTravelsAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("delete")]
        public Task DeleteAsync(Guid id)
        {
            return _userChoiceTravelsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("get")]
        public Task<UserChoiceTravelsDto> GetAsync(Guid id)
        {
            return _userChoiceTravelsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("get-by-user")]
        public Task<UserChoiceTravelListDto> GetChoiceTravelAsync(string id)
        {
            return _userChoiceTravelsAppService.GetChoiceTravelAsync(id);
        }

        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<UserChoiceTravelsDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _userChoiceTravelsAppService.GetListAsync(input);
        }

        [HttpPost]
        [Route("update")]
        public Task<UserChoiceTravelsDto> UpdateAsync(Guid id, UserChoiceTravelsCreateUpdateDto input)
        {
            return _userChoiceTravelsAppService.UpdateAsync(id, input);
        }
    }
}
