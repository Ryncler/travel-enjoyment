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

        [HttpPost]
        [Route("send")]
        public Task SendEmail(SendDto input)
        {
            return _emailAppService.SendEmail(input);
        }
    }
}
