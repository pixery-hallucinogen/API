using Hallucinogen_API.Contract.Models;
using Hallucinogen_API.Data.Entities;

namespace Hallucinogen_API.Mappers.Implementations
{
    public class PostCommentMapper : IPostCommentMapper
    {
        public PostCommentModel ToModel(PostCommentEntity entity)
        {
            return new PostCommentModel
            {
                Id = entity.Id,
                PostId = entity.PostId,
                Comment = entity.Comment,
                CommentDate = entity.CommentDate,
                UserId = entity.UserId
            };
        }

        public PostCommentEntity ToEntity(PostCommentModel model)
        {
            return new PostCommentEntity
            {
                Id = model.Id,
                PostId = model.PostId,
                Comment = model.Comment,
                CommentDate = model.CommentDate,
                UserId = model.UserId
            };        
        }
    }
}