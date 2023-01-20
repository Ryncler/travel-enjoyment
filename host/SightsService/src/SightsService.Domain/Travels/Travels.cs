using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace SightsService.TravelsManage
{
    public class Travels : FullAuditedAggregateRoot<Guid>
    {
        public Guid ReleaseId { get; protected set; }

        public DateTime ReleaseDate { get; protected set; }

        public string ReleaseSite { get; protected set; }

        public string TravelsTitle { get; protected set; }

        public DateTime DepartureTime { get; protected set; }

        public int TravelDayNum { get; protected set; }

        public decimal TravelExpenses { get; protected set; }

        public string Content { get; protected set; }

        protected Travels()
        {
        }

        public Travels(
            Guid id,
            Guid releaseId,
            DateTime releaseDate,
            string releaseSite,
            string travelsTitle,
            DateTime departureTime,
            int travelDayNum,
            decimal travelExpenses,
            string travelsContent
        ) : base(id)
        {
            ReleaseId = releaseId;
            ReleaseDate = releaseDate;
            ReleaseSite = releaseSite;
            TravelsTitle = travelsTitle;
            DepartureTime = departureTime;
            TravelDayNum = travelDayNum;
            TravelExpenses = travelExpenses;
            Content = travelsContent;
        }
    }
}
