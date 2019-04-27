using Hallucinogen_API.Contract.Models;
using Hallucinogen_API.Data.Entities;

namespace Hallucinogen_API.Mappers.Implementations
{
    public class PostMapper : IPostMapper
    {
        public PostModel ToModel(PostEntity entity)
        {
            return new PostModel
            {
                Id = entity.Id,
                Media = entity.Media,
                UserId = entity.UserId,
                PostDate = entity.PostDate,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                Description = entity.Description
            };
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
                Description = model.Description
            };
        }
    }
}