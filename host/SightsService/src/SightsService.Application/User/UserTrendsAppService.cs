using Microsoft.Extensions.Hosting;
using SightsService.CommentManage;
using SightsService.TravelsManage;
using SightsService.User.Dtos;
using SightsService.UserManage.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace SightsService.UserManage
{
    public class UserTrendsAppService : SightsServiceAppService, IUserTrendsAppService
    {
        private readonly ITravelsRepository _travelsRepository;

        private readonly ICommentRepository _commentRepository;

        public UserTrendsAppService(ITravelsRepository travelsRepository, ICommentRepository commentRepository)
        {
            _travelsRepository = travelsRepository;
            _commentRepository = commentRepository;
        }

        public async Task<PagedResultDto<UserTrendsDto>> GetPagedTrendsByUserAsync(PagedUserTrendsByUserDto input)
        {
            var currentTime = new DateTime();
            var endTime = new DateTime();
            var result = new List<UserTrendsDto>();
            if (input.SkipCount <= 0)
            {
                currentTime = DateTime.Now;
            }
            else
            {
                currentTime = DateTime.Now.AddDays(-(input.SkipCount * input.MaxResultCount));

            }
            endTime = currentTime.AddDays(-(input.MaxResultCount - 1));

            var allTime = GetAllDays(endTime, currentTime);
            var travelQuery = await _travelsRepository.GetQueryableAsync();
            var commentQuery = await _commentRepository.GetQueryableAsync();

            foreach (var item in allTime)
            {
                var userTrend = new UserTrendsDto();
                userTrend.TrendsTime = item;
                var travels = travelQuery.AsEnumerable<Travels>()
                    .Where(x => x.ReleaseId.Equals(input.UserId) && x.ReleaseDate.ToString("yyyy-MM-dd").Equals(item))
                    .ToList();
                var comments = commentQuery.AsEnumerable<Comment>()
                    .Where(x => x.UserId.Equals(input.UserId) && x.ReleaseDate.ToString("yyyy-MM-dd").Equals(item))
                    .ToList();

                if (travels.Count > 0)
                {
                    foreach (var travel in travels)
                    {
                        userTrend.ShareTravels.Add(new ShareTravelDto
                        {
                            TravelId = travel.Id,
                            TravelName = travel.TravelsTitle
                        });
                    }
                }
                if (comments.Count > 0)
                {
                    foreach (var comment in comments)
                    {
                        var travel = await _travelsRepository.FindAsync(x => x.Id.Equals(comment.TravelsId));
                        if (travel != null)
                        {
                            userTrend.ReplyComments.Add(new ReplyCommentDto
                            {
                                TravelId = comment.TravelsId,
                                TravelName = travel.TravelsTitle,
                                ReplyContent = comment.Content
                            });
                        }
                    }
                }
                if (userTrend.ShareTravels.Count > 0 || userTrend.ReplyComments.Count > 0)
                    result.Add(userTrend);
            }

            return new PagedResultDto<UserTrendsDto>
            {
                Items = result,
                TotalCount = result.Count
            };
        }

        protected List<string> GetAllDays(DateTime startTime, DateTime endTime)
        {
            var result = new List<string>();
            var currentDate = new DateTime();
            //循环比较，取出日期；
            for (currentDate = startTime; currentDate.CompareTo(endTime) <= 0; currentDate = currentDate.AddDays(1))
            {
                result.Add(currentDate.ToString("yyyy-MM-dd"));
            }
            return result.OrderByDescending(x => x).ToList();
        }
    }
}
