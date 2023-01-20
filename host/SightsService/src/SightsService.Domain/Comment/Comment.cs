using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace SightsService.CommentManage
{
    public class Comment : FullAuditedAggregateRoot<Guid>
    {
        public Guid TravelsId { get; protected set; }

        public Guid UserId { get; protected set; }

        public DateTime ReleaseDate { get; protected set; }

        public Guid? ParentId { get; protected set; }

        public string Content { get; protected set; }

        protected Comment()
        {
        }

        public Comment(
            Guid id,
            Guid travelsId,
            Guid userId,
            DateTime releaseDate,
            Guid? parentId,
            string content
        ) : base(id)
        {
            TravelsId = travelsId;
            UserId = userId;
            ReleaseDate = releaseDate;
            ParentId = parentId;
            Content = content;
        }
    }
}
