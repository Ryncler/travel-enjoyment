﻿using BaseService.OpenIddict;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NETCore.Encrypt;
using System;
using System.IO;
using System.Text;
using Volo.Abp.OpenIddict.Applications;

namespace BaseService.RSA
{
    public static class RsaKeyExtenstion
    {
        public static void Create(this RsaKeyOptions options)
        {
            using (TextReader reader = new StreamReader(@"./RsaFile/privateKey.txt", Encoding.UTF8))
            {
                options.PrivateKey = reader.ReadToEnd();
            }
            using (TextReader reader = new StreamReader(@"./RsaFile/publicKey.txt", Encoding.UTF8))
            {
                options.PublicKey = reader.ReadToEnd();
            }
        }

        public static bool Verify(string encrypt, string compare, string privateKey)
        {
            var decrypt = EncryptProvider.RSADecrypt(privateKey, encrypt);
            return decrypt.Equals(compare);
        }

        public static string GetDecrypt(string privateKey, string encrypt)
        {
            if (string.IsNullOrWhiteSpace(encrypt))
                return "";
            return EncryptProvider.RSADecrypt(privateKey, encrypt);
        }

        public static IServiceCollection ReplaceTransient(this IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Transient<IAbpApplicationManager, CustomOpenIddictApplicationManager>());
            return services;
        }
    }
}
