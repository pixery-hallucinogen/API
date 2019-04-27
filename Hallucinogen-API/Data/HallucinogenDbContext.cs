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
        
        public virtual DbSet<PostEntity> Posts { get; set; }
        public virtual DbSet<PostLikeEntity> PostLikes { get; set; }
        public virtual DbSet<PostCommentEntity> PostComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Post Index over UserId
            builder.Entity<PostEntity>()
                .HasIndex(pe => pe.UserId)
                .HasFilter(null);
            // Post Index over PostDate
            builder.Entity<PostEntity>()
                .HasIndex(pe => pe.PostDate)
                .HasFilter(null);

            // Post Index over lat & long
            builder.Entity<PostEntity>()
                .HasIndex(pe => pe.Latitude);

            builder.Entity<PostEntity>()
                .HasIndex(pe => pe.Longitude);

            
            // PostLike Many-Many
            builder.Entity<PostLikeEntity>()
                .HasKey(pl => new {pl.UserId, pl.PostId});
            builder.Entity<PostLikeEntity>()
                .HasOne(pl => pl.User)
                .WithMany(u => u.PostLikes)
                .HasForeignKey(pl => pl.UserId);
            builder.Entity<PostLikeEntity>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(pl => pl.PostId);
            
            // PostComment Index over PostId
            builder.Entity<PostCommentEntity>()
                .HasIndex(pce => pce.PostId)
                .HasFilter(null);
            
            base.OnModelCreating(builder);
        }
    }
}