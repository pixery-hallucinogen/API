using Hallucinogen_API.Contract.Models;
using Hallucinogen_API.Data.Entities;
using System.Linq;

namespace Hallucinogen_API.Mappers.Implementations
{
    public class PostMapper : IPostMapper
    {
        public PostModel ToModel(PostEntity entity, string requesterId)
        {
            var model = new PostModel
            {
                Id = entity.Id,
                Media = entity.Media,
                UserId = entity.UserId,
                PostDate = entity.PostDate,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                Description = entity.Description,
                Location = entity.Location
            };
            
            if (entity.Likes != null && entity.Likes.Any())
            {
                model.LikeCount = entity.Likes.Count;
            }

            if (entity.User != null)
            {
                model.UserName = entity.User.UserName;
                model.UserPhoto = entity.User.Image;
            }
            
            model.AlreadyLiked = entity.Likes != null && entity.Likes.Any(l => l.UserId == requesterId);

            return model;
        }

        public PostEntity ToEntity(PostModel model)
        {
            return new PostEntity
            {
                Id = model.Id,
                Media = model.Media,
                UserId = model.UserId,
                PostDate = model.PostDate,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Description = model.Description,
                Location = model.Location
            };
        }
    }
}