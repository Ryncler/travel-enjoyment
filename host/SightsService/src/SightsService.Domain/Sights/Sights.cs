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

        public DateTime OpenTime { get; protected set; }

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
            DateTime changeTime,
            Guid mapId,
            string description,
            DateTime openTime,
            string address,
            string ticket,
            string trafficGuide,
            string selfDrivingGuide
        ) : base(id)
        {
            Name = name;
            ChangeTime = changeTime;
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
