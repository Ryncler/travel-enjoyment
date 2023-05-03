using BaseService.Email.Dtos;
using BaseService.Entities;
using BaseService.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IUserExtensionAppService _userExtensionAppService;

        public EmailAppService(IdentityUserManager identityUserManager, ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration,
         IUserExtensionAppService userExtensionAppService)
        {
            _identityUserManager = identityUserManager;
            _userExtensionAppService = userExtensionAppService;
            _smtpEmailSenderConfiguration = smtpEmailSenderConfiguration;
        }

        //[Authorize]
        public async Task SendEmailAsync(SendDto input)
        {
            var emailSender = new SmtpEmailSender(_smtpEmailSenderConfiguration, null);
            await emailSender.SendAsync(input.Email, input.Subject, input.Data);
        }

        public async Task ActiveAccount(string id)
        {
            var user = await _identityUserManager.GetByIdAsync(Guid.Parse(id));
            if (user == null)
                return;

            await _userExtensionAppService.CreateAsync(new UserExtensionCreateDto
            {
                UserId = user.Id,
                Sex = true,
                Avatar = "",
                Profile = "",
                IsPushCommentMessage = false,
                IsPushPrivateMessage = false
            });

            user.SetIsActive(true);
            await _identityUserManager.SetLockoutEnabledAsync(user, false);
        }
    }
}
