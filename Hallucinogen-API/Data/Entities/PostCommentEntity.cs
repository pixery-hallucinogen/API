using System;
using Hallucinogen_API.Contract;

namespace Hallucinogen_API.Data.Entities
{
    public class PostCommentEntity : EntityBase<int>
    {
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        
        public string UserId { get; set; }
        public int PostId { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual PostEntity Post { get; set; }
    }
}