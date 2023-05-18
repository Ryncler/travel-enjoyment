using BaseService.Email;
using BaseService.Email.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;

namespace BaseService.Entities
{
    [Area(BaseServiceRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = BaseServiceRemoteServiceConsts.RemoteServiceName)]
    [Route("/")]
    public class EmailController : BaseServiceController, IEmailAppService
    {
        private readonly IEmailAppService _emailAppService;
        public EmailController(IEmailAppService emailAppService)
        {
            _emailAppService = emailAppService;
        }

        [HttpGet]
        [Route("verify")]
        public Task ActiveAccount(string id)
        {
            return _emailAppService.ActiveAccount(id);
        }

        [HttpGet]
        [Route("verify-active")]
        public async Task<IActionResult> VerifyActive(string id)
        {
            await _emailAppService.ActiveAccount(id);
            return Redirect("http://192.168.110.100:8090/user-web");
        }

        [HttpPost]
        [Route("send")]
        public Task SendEmailAsync(SendDto input)
        {
            return _emailAppService.SendEmailAsync(input);
        }
    }
}
