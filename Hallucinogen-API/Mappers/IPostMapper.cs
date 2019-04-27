using Hallucinogen_API.Contract.Models;
using Hallucinogen_API.Data.Entities;

namespace Hallucinogen_API.Mappers
{
    public interface IPostMapper
    {
        PostModel ToModel(PostEntity entity, string requesterId);
        PostEntity ToEntity(PostModel model);

    }
}