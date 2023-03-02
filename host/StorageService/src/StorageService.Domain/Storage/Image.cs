using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace StorageService.Storage
{
    public class Image : FullAuditedEntity<Guid>
    {
        public Guid LinkId { get; protected set; }

        public bool IsMain { get; protected set; }

        public string ImageURL { get; protected set; }

        protected Image()
        {
        }

        public Image(
            Guid id,
            Guid linkId,
            bool isMain,
            string imageURL
        ) : base(id)
        {
            LinkId = linkId;
            IsMain = isMain;
            ImageURL = imageURL;
        }
    }
}
