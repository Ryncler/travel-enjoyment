using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace SightsService.SightsManage
{
    public class Sights : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; protected set; }

        public DateTime ChangeTime { get; protected set; }

        public Guid MapId { get; protected set; }

        public string Description { get; protected set; }

        public string OpenTime { get; protected set; }

        public string Address { get; protected set; }

        public string Ticket { get; protected set; }

        public string TrafficGuide { get; protected set; }

        public string SelfDrivingGuide { get; protected set; }

        protected Sights()
        {
        }

        public Sights(
            Guid id,
            string name,
            Guid mapId,
            string description,
            string openTime,
            string address,
            string ticket,
            string trafficGuide,
            string selfDrivingGuide
        ) : base(id)
        {
            Name = name;
            ChangeTime = DateTime.Now;
            MapId = mapId;
            Description = description;
            OpenTime = openTime;
            Address = address;
            Ticket = ticket;
            TrafficGuide = trafficGuide;
            SelfDrivingGuide = selfDrivingGuide;
        }
    }
}
