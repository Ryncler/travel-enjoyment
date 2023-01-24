using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CommonService.CategoryManage
{
    public class Category : FullAuditedEntity<Guid>
    {
        public string Name { get; protected set; }

        public Guid ParentId { get; protected set; }

        protected Category()
        {
        }

        public Category(
            Guid id,
            string name,
            Guid parentId
        ) : base(id)
        {
            Name = name;
            ParentId = parentId;
        }
    }
}
