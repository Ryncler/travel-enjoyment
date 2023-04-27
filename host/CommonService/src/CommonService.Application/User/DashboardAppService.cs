using CommonService.Extension;
using CommonService.Permissions;
using CommonService.User.Dtos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging;

namespace CommonService.User
{
    public class DashboardAppService : CommonServiceAppService, IDashboardAppService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IAuditLogActionsRepository _auditLogActionsRepository;
        private readonly IUserTravelRepository _userTravelRepository;
        private readonly IEntityFrameworkExtension _entityFrameworkExtension;

        public DashboardAppService(IAuditLogActionsRepository auditLogActionsRepository, IEntityFrameworkExtension entityFrameworkExtension,
            IAuditLogRepository auditLogRepository, IUserTravelRepository userTravelRepository)
        {
            _auditLogRepository = auditLogRepository;
            _auditLogActionsRepository = auditLogActionsRepository;
            _userTravelRepository = userTravelRepository;
            _entityFrameworkExtension = entityFrameworkExtension;
        }

        [Authorize(CommonServicePermissions.Dashboard.Default)]
        public async Task<List<RecentHotDto>> GetRecentHotSightsAsync()
        {
            var result = new List<RecentHotDto>();
            if (!CurrentUser.Id.HasValue)
                return result;
            if (CurrentUser.Roles.Contains("admin"))
            {
                var auditActions = (await _auditLogActionsRepository.GetListAsync(x => x.ExecutionTime > DateTime.Now.AddDays(-7) && x.ExecutionTime < DateTime.Now && x.ServiceName.Equals("SightsService.SightsManage.SightsActivityController") && x.MethodName.Equals("GetAllBySightsId")))
                    .GroupBy(x => x.Parameters).OrderByDescending(x => x.Count()).Take(10).ToList();
                foreach (var item in auditActions)
                {
                    if (string.IsNullOrWhiteSpace(item.Key))
                        continue;
                    var data = new RecentHotDto();
                    var id = string.Concat(item.Key.Split(":")[1].Split(new char[] { '"', '}' }, StringSplitOptions.None));
                    var sql = string.Format("select \"Name\" from \"Sights\" where \"Id\"='{0}'", id);
                    data.Name = (await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(sql)).ToString();
                    var hours = item.GroupBy(x => x.ExecutionTime.Hour);
                    foreach (var hour in hours)
                    {
                        if (hour.Key == 0)
                            data.HourValue[24] = hour.Count();
                        else
                            data.HourValue[hour.Key] = hour.Count();
                    }
                    result.Add(data);
                }
            }
            else
            {
                var sql = string.Format("select * from \"Sights\" where \"CreatorId\"='{0}' and \"IsDeleted\"=false", CurrentUser.Id.Value);
                var sights = await _entityFrameworkExtension.ExecuteSQLAsync<SightsDto>(sql);
                foreach (var item in sights)
                {
                    var auditActions = (await _auditLogActionsRepository.GetListAsync(x => x.ExecutionTime > DateTime.Now.AddDays(-7) && x.ExecutionTime < DateTime.Now && x.ServiceName.Equals("SightsService.SightsManage.SightsActivityController") && x.MethodName.Equals("GetAllBySightsId") && x.Parameters.Contains(item.Id.ToString())));
                    var data = new RecentHotDto();
                    data.Name = item.Name;
                    var hours = auditActions.GroupBy(x => x.ExecutionTime.Hour);
                    foreach (var hour in hours)
                    {
                        if (hour.Key == 0)
                            data.HourValue[24] = hour.Count();
                        else
                            data.HourValue[hour.Key] = hour.Count();
                    }
                    result.Add(data);
                }
                result = result.OrderByDescending(x => x.HourValue.Sum()).Take(10).ToList();
            }
            return result;
        }

        [Authorize(CommonServicePermissions.Dashboard.Default)]
        public async Task<List<RecentHotDto>> GetRecentHotTagsAsync()
        {
            var result = new List<RecentHotDto>();
            var month = DateTime.Now.AddMonths(-1);
            var tags = await _entityFrameworkExtension.ExecuteSQLAsync<SightsDto>("SELECT * FROM \"Tags\" where \"IsDeleted\"=false");
            foreach (var item in tags)
            {
                var auditActions = await _auditLogActionsRepository.GetListAsync(x => x.ExecutionTime > DateTime.Now.AddMonths(-7) && x.ExecutionTime < DateTime.Now && x.ServiceName.Equals("SightsService.Sights.SightsTagController") && x.MethodName.Equals("CreateManyAsync") && x.Parameters.Contains(item.Name));
                var data = new RecentHotDto();
                data.Name = item.Name;
                var hours = auditActions.GroupBy(x => x.ExecutionTime.Hour);
                foreach (var hour in hours)
                {
                    if (hour.Key == 0)
                        data.HourValue[24] = hour.Count();
                    else
                        data.HourValue[hour.Key] = hour.Count();
                }
                result.Add(data);
            }
            result = result.OrderByDescending(x => x.HourValue.Sum()).Take(10).ToList();
            return result;
        }

        [Authorize(CommonServicePermissions.Dashboard.Default)]
        public async Task<List<TravelsDto>> GetRecentHotTravelAsync()
        {
            var result = new List<TravelsDto>();
            var auditActions = await _auditLogActionsRepository.GetListAsync(x => x.ExecutionTime > DateTime.Now.AddMonths(-7) && x.ExecutionTime < DateTime.Now && x.ServiceName.Equals("SightsService.TravelsManage.TravelsController") && x.MethodName.Equals("GetAsync"));
            var data = auditActions.GroupBy(x => x.Parameters).OrderByDescending(x => x.Count()).Take(3).ToList();
            foreach (var item in data)
            {
                var id = string.Concat(item.Key.Split(":")[1].Split(new char[] { '"', '}' }, StringSplitOptions.None));
                var travelSql = string.Format("select * from \"Travels\" where \"Id\"='{0}' and \"IsDeleted\"=false", id);
                var travel = await _entityFrameworkExtension.ExecuteSQLAsync<TravelsDto>(travelSql);
                if (travel.Count > 0)
                    result.Add(travel.FirstOrDefault());
            }
            return result;
        }

        [Authorize(CommonServicePermissions.Dashboard.Default)]
        public async Task<List<RecentHotUserDto>> GetRecentHotUserAsync()
        {
            var result = new List<RecentHotUserDto>();
            var users = (await _auditLogRepository.GetListAsync(x => x.ExecutionTime > DateTime.Now.AddMonths(-7) && x.ExecutionTime < DateTime.Now && x.UserId != null)).GroupBy(x => x.UserId.Value).OrderByDescending(x => x.Count()).Take(3);
            foreach (var item in users)
            {
                var avatarSql = string.Format("select \"Avatar\" from \"UserExtensions\" where \"UserId\"='{0}'", item.Key);
                var avatar = await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(avatarSql, true);
                var starNum = (await _userTravelRepository.GetListAsync(x => x.UserId.Equals(item.Key) && x.CreationTime > DateTime.Now.AddMonths(-7) && x.CreationTime < DateTime.Now && !x.IsDeleted)).Count;
                var sql = "select count(*) from \"{0}\" where \"CreatorId\" ='{1}' and \"IsDeleted\"=false and \"CreationTime\">CURRENT_DATE - INTERVAL '7 day' and \"CreationTime\" < CURRENT_DATE";
                var travelNum = await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(string.Format(sql, "Travels", item.Key));
                var commmentNum = await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(string.Format(sql, "Comments", item.Key));
                result.Add(new RecentHotUserDto
                {
                    UserId = item.Key.ToString(),
                    UserName = item.FirstOrDefault().UserName,
                    Avatar = avatar.ToString(),
                    TravelNum = Convert.ToInt32(travelNum),
                    CommentNum = Convert.ToInt32(commmentNum),
                    StarNum = starNum,
                    LastTime = item.OrderByDescending(x => x.ExecutionTime).FirstOrDefault().ExecutionTime.ToShortDateString()
                });
            }
            return result;
        }

        [Authorize(CommonServicePermissions.Dashboard.Default)]
        public async Task<List<object>> GetNewlyInfoAsync()
        {
            var result = new List<object>();
            var sql = "select count(*) from \"{0}\" where \"CreationTime\">CURRENT_DATE - INTERVAL '7 day' and \"CreationTime\" < CURRENT_DATE";
            result.Add(await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(string.Format(sql, "Users"), true));
            result.Add(await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(string.Format(sql, "EntryInfos"), true));
            result.Add(await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(string.Format(sql, "Sights")));
            result.Add(await _entityFrameworkExtension.ExecuteSQLGetFirstAsync(string.Format(sql, "Travels")));
            return result;
        }
    }
}
