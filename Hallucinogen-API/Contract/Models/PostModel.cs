using System;

namespace Hallucinogen_API.Contract.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public DateTime PostDate { get; set; }
        public string UserId { get; set; }
        
        public int LikeCount { get; set; }
        public bool? AlreadyLiked { get; set; }
    }
}