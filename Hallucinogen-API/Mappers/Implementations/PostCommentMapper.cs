using Hallucinogen_API.Contract.Models;
using Hallucinogen_API.Data.Entities;

namespace Hallucinogen_API.Mappers.Implementations
{
    public class PostCommentMapper : IPostCommentMapper
    {
        public PostCommentModel ToModel(PostCommentEntity entity)
        {
            var model =  new PostCommentModel
            {
                Id = entity.Id,
                PostId = entity.PostId,
                Comment = entity.Comment,
                CommentDate = entity.CommentDate,
                UserId = entity.UserId
            };

            if (entity.User == null) return model;
            
            model.UserName = entity.User.UserName;
            model.UserPhoto = entity.User.Image;

            return model;
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