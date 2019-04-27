using Hallucinogen_API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hallucinogen_API.Data
{
    public class HallucinogenDbContext : IdentityDbContext<UserEntity>
    {
        public HallucinogenDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}