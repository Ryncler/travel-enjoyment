using BaseService.Email.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseService.Email
{
    public interface IEmailAppService
    {
        Task SendEmailAsync(SendDto input);

        Task ActiveAccount(string id);
    }
}
