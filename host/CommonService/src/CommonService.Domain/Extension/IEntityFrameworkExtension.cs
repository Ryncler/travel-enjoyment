using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CommonService.Extension
{
    public interface IEntityFrameworkExtension : ITransientDependency
    {
        Task<bool> ExecuteSQLAsync(string sql, bool isBase = false);

        Task<object> ExecuteSQLGetFirstAsync(string sql, bool isBase = false);

        Task<List<T>> ExecuteSQLAsync<T>(string sql, bool isBase = false) where T : class, new();
    }
}
