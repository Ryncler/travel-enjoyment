﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using System.Threading.Tasks;
using System.Threading;
using System;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.DependencyInjection;
using NETCore.Encrypt;
using AuthService.RSA;

namespace AuthService.OpenIddict
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAbpApplicationManager))]
    public class CustomOpenIddictApplicationManager : AbpApplicationManager
    {
        private readonly IOptions<RsaKeyOptions> _rsaKeyOptions;

        public CustomOpenIddictApplicationManager(IOpenIddictApplicationCache<OpenIddictApplicationModel> cache,
            ILogger<AbpApplicationManager> logger, IOptionsMonitor<OpenIddictCoreOptions> options, IOpenIddictApplicationStoreResolver resolver, IOptions<RsaKeyOptions> rsaKeyOptions)
            : base(cache, logger, options, resolver)
        {
            _rsaKeyOptions = rsaKeyOptions;
        }

        protected override ValueTask<string> ObfuscateClientSecretAsync(string secret, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("The secret cannot be null or empty.", nameof(secret));
            }
            var result = EncryptProvider.RSAEncrypt(_rsaKeyOptions.Value.PublicKey, secret);
            return new ValueTask<string>(result);
        }

        public override ValueTask<bool> ValidateClientSecretAsync(OpenIddictApplicationModel application, string secret, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("The secret cannot be null or empty.", nameof(secret));
            }

            if (string.IsNullOrEmpty(application.ClientSecret))
            {
                throw new ArgumentException("The application secret cannot be null or empty.", nameof(application.ClientSecret));
            }

            try
            {
                return new ValueTask<bool>(RsaKeyExtenstion.Verify(application.ClientSecret, secret, _rsaKeyOptions.Value.PrivateKey));
            }

            catch (Exception exception)
            {
                Logger.LogWarning(exception, "An error occurred while trying to verify a client secret. " +
                                             "This may indicate that the hashed entry is corrupted or malformed.");

                return new ValueTask<bool>(false);
            }
        }
    }
}
