using BaseService.Email.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Smtp;
using Volo.Abp.Identity;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Settings;

namespace BaseService.Email
{
    public class EmailAppService : BaseServiceAppService, IEmailAppService
    {
        private readonly IdentityUserManager _identityUserManager;
        private readonly ISmtpEmailSenderConfiguration _smtpEmailSenderConfiguration;
        public EmailAppService(IdentityUserManager identityUserManager, ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration)
        {
            _identityUserManager = identityUserManager;
            _smtpEmailSenderConfiguration = smtpEmailSenderConfiguration;
        }

        public async Task SendEmail(SendDto input)
        {
            var emailSender = new SmtpEmailSender(_smtpEmailSenderConfiguration, null);
            await emailSender.SendAsync(input.Email, input.Subject, input.Data);
        }

        public async Task ActiveAccount(string id)
        {
            var user = await _identityUserManager.GetByIdAsync(Guid.Parse(id));
            if (user == null)
                return;

            user.SetIsActive(true);
            await _identityUserManager.SetLockoutEnabledAsync(user, false);
        }
    }
}
