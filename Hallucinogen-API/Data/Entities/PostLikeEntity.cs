using Hallucinogen_API.Contract;

namespace Hallucinogen_API.Data.Entities
{
    public class PostLikeEntity: EntityBase<int>
    {
        public string UserId { get; set; }
        public int PostId { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual PostEntity Post { get; set; }
    }
}