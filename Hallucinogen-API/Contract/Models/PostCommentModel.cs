using System;

namespace Hallucinogen_API.Contract.Models
{
    public class PostCommentModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        
        public string UserId { get; set; }
        public int PostId { get; set; }
    }
}