using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace CommonService.CategoryManage
{
    public class Tag : FullAuditedEntity<Guid>
    {
        public string Name { get; protected set; }

        public Guid ParentCategoryId { get; protected set; }

        protected Tag()
        {
        }

        public Tag(
            Guid id,
            string name,
            Guid parentCategoryId
        ) : base(id)
        {
            Name = name;
            ParentCategoryId = parentCategoryId;
        }
    }
}
