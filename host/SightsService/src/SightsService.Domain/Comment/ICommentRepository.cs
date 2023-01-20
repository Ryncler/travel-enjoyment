using System;
using Volo.Abp.Domain.Repositories;

namespace SightsService.CommentManage;

public interface ICommentRepository : IRepository<Comment, Guid>
{
}
