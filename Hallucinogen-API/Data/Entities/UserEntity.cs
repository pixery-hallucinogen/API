using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Hallucinogen_API.Data.Entities
{
    public class UserEntity: IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public DateTime RegistrationDate { get; set; }
        
        public ICollection<PostEntity> Posts { get; set; }
        public ICollection<PostLikeEntity> PostLikes { get; set; }
    }
}