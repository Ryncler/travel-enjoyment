using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp;

namespace BaseService.Entities
{
    public class UserExtension : AuditedEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual Guid UserId { get; protected set; }

        /// <summary>
        /// 头像
        /// </summary>
        public virtual string Avatar { get; protected set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual bool Sex { get; protected set; }

        /// <summary>
        /// 简介
        /// </summary>
        public virtual string Profile { get; protected set; }

        /// <summary>
        /// 是否推送私信消息
        /// </summary>
        public virtual bool IsPushPrivateMessage { get; protected set; }

        /// <summary>
        /// 是否推送评论消息
        /// </summary>
        public virtual bool IsPushCommentMessage { get; protected set; }

        public override object[] GetKeys()
        {
            return new object[] { UserId };
        }

        protected UserExtension()
        {
        }


        public UserExtension(
            Guid userId,
            string avatar,
            bool sex,
            string profile,
            bool isPushPrivateMessage,
            bool isPushCommentMessage
        )
        {
            UserId = userId;
            Avatar = avatar;
            Sex = sex;
            Profile = profile;
            IsPushPrivateMessage = isPushPrivateMessage;
            IsPushCommentMessage = isPushCommentMessage;
        }
    }
}
