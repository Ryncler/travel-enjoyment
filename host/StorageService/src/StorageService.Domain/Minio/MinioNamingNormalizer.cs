using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace StorageService.Minio
{
    public class MinioNamingNormalizer : ITransientDependency
    {
        private readonly ICurrentUser _currentUser;

        public MinioNamingNormalizer(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public string NormalizeBucketName(string bucketName)
        {
            using (CultureHelper.Use(CultureInfo.InvariantCulture))
            {
                // All letters in a container name must be lowercase.
                bucketName = bucketName.ToLower();

                // Container names must be from 3 through 63 characters long.
                if (bucketName.Length > 63)
                {
                    bucketName = bucketName.Substring(0, 63);
                }

                // Bucket names can consist only of lowercase letters, numbers, dots (.), and hyphens (-).
                bucketName = Regex.Replace(bucketName, "[^a-z0-9-.]", string.Empty);

                // Bucket names must begin and end with a letter or number.
                // Bucket names must not be formatted as an IP address (for example, 192.168.5.4).
                // Bucket names can't start or end with hyphens adjacent to period
                // Bucket names can't start or end with dots adjacent to period
                bucketName = Regex.Replace(bucketName, "\\.{2,}", ".");
                bucketName = Regex.Replace(bucketName, "-\\.", string.Empty);
                bucketName = Regex.Replace(bucketName, "\\.-", string.Empty);
                bucketName = Regex.Replace(bucketName, "^-", string.Empty);
                bucketName = Regex.Replace(bucketName, "-$", string.Empty);
                bucketName = Regex.Replace(bucketName, "^\\.", string.Empty);
                bucketName = Regex.Replace(bucketName, "\\.$", string.Empty);
                bucketName = Regex.Replace(bucketName, "^(?:(?:^|\\.)(?:2(?:5[0-5]|[0-4]\\d)|1?\\d?\\d)){4}$", String.Empty);

                if (bucketName.Length < 3)
                {
                    var length = bucketName.Length;
                    for (var i = 0; i < 3 - length; i++)
                    {
                        bucketName += "0";
                    }
                }

                return bucketName;
            }
        }

        public string NormalizeObjectName(string objectName)
        {
            var userId = _currentUser.GetId();
            var suffix = objectName.Substring(objectName.LastIndexOf("."));
            var result = string.Format("{0}-{1}-{2}-{3}{4}", DateTimeOffset.Now, DateTime.Now.ToString("yyyy-MM-dd"), userId, RandomHelper.GetRandom(100000000), suffix);
            return result;
        }
    }
}
