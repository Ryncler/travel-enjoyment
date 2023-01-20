using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace SightsService.ActivityManage
{
    public class Activity : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; protected set; }

        public DateTime ChangeTime { get; protected set; }

        public string Content { get; protected set; }

        protected Activity()
        {
        }

        public Activity(
            Guid id,
            string name,
            DateTime changeTime,
            string content
        ) : base(id)
        {
            Name = name;
            ChangeTime = changeTime;
            Content = content;
        }
    }
}
