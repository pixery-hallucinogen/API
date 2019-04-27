using Hallucinogen_API.Contract.Models;
using Hallucinogen_API.Data.Entities;

namespace Hallucinogen_API.Mappers.Implementations
{
    public class UserMapper: IUserMapper
    {
        public UserModel ToModel(UserEntity entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                Email = entity.Email,
                FirstName = entity.FirstName,
                Image = entity.Image,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName,
                UserName = entity.UserName
            };
        }

        public UserEntity ToEntity(UserModel model)
        {
            return new UserEntity
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                Image = model.Image,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                UserName = model.UserName
            };
        }
    }
}