using SightsService.TravelsManage.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SightsService.UserManage.Dtos
{
    public class UserTrendsDto
    {
        public string TrendsTime { get; set; }

        public List<ShareTravelDto> ShareTravels { get; set; } = new List<ShareTravelDto>();

        public List<ReplyCommentDto> ReplyComments { get; set; } = new List<ReplyCommentDto>();
    }

    public class ShareTravelDto
    {
        public Guid? TravelId { get; set; } = null;

        public string TravelName { get; set; } = "";
    }

    public class ReplyCommentDto
    {
        public Guid? TravelId { get; set; } = null;

        public string TravelName { get; set; } = "";

        public string ReplyContent { get; set; } = "";
    }
}
