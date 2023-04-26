using CommonService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using System.Data;
using System.Data.Common;

namespace CommonService.Extension
{
    public class EntityFrameworkExtension : IEntityFrameworkExtension, ITransientDependency
    {
        private readonly IDbContextProvider<ICommonServiceDbContext> _dbContextProvider = null;
        private readonly IDbContextProvider<ICommonServiceBaseDbContext> _baseDbContextProvider = null;

        public EntityFrameworkExtension(IDbContextProvider<ICommonServiceDbContext> dbContextProvider, IDbContextProvider<ICommonServiceBaseDbContext> baseDbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
            _baseDbContextProvider = baseDbContextProvider;
        }

        public async Task<bool> ExecuteSQLAsync(string sql, bool isBase = false)
        {
            var result = 0;
            if (isBase)
            {
                var baseDb = await _baseDbContextProvider.GetDbContextAsync();
                result = await baseDb.Database.ExecuteSqlRawAsync(sql);
            }
            else
            {
                var db = await _dbContextProvider.GetDbContextAsync();
                result = await db.Database.ExecuteSqlRawAsync(sql);
            }
            return result == 1 ? true : false;
        }

        public async Task<object> ExecuteSQLGetFirstAsync(string sql, bool isBase = false)
        {
            var dt = new DataTable();
            DbConnection conn;
            if (isBase)
                conn = (await _baseDbContextProvider.GetDbContextAsync()).Database.GetDbConnection();
            else
                conn = (await _dbContextProvider.GetDbContextAsync()).Database.GetDbConnection();

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            var dr = await cmd.ExecuteReaderAsync();
            dt.Load(dr);
            await dr.DisposeAsync();

            if (dt.Rows.Count > 0)
                return dt.Rows[0].ItemArray.FirstOrDefault();
            return null;
        }

        public async Task<List<T>> ExecuteSQLAsync<T>(string sql, bool isBase = false) where T : class, new()
        {
            var result = new List<T>();
            DbConnection conn;
            if (isBase)
                conn = (await _baseDbContextProvider.GetDbContextAsync()).Database.GetDbConnection();
            else
                conn = (await _dbContextProvider.GetDbContextAsync()).Database.GetDbConnection();

            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            var dr = await cmd.ExecuteReaderAsync();
            var properties = typeof(T).GetProperties().ToList();
            while (dr.Read())
            {
                var obj = new T();
                foreach (var property in properties)
                {
                    var id = dr.GetOrdinal(property.Name.ToLower());
                    if (!dr.IsDBNull(id))
                    {
                        if (dr.GetValue(id) != DBNull.Value)
                        {
                            property.SetValue(obj, dr.GetValue(id));
                        }
                    }
                }
                result.Add(obj);
            }
            await dr.DisposeAsync();
            return result;
        }
    }
}
