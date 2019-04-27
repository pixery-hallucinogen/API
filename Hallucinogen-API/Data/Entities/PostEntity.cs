using System;
using System.Collections.Generic;
using Hallucinogen_API.Contract;

namespace Hallucinogen_API.Data.Entities
{
    public class PostEntity : EntityBase<int>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public DateTime PostDate { get; set; }

        public string UserId { get; set; }
        public virtual UserEntity User { get; set; }
        
        public ICollection<PostLikeEntity> Likes { get; set; }
        
    }
}