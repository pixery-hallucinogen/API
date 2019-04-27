using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hallucinogen_API.Data;
using Hallucinogen_API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Hallucinogen_API.Repositories.Implementations
{
    public class PostRepository : IPostRepository
    {
        
        private readonly HallucinogenDbContext _dbContext;
        private readonly ILogger<PostRepository> _logger;

        public PostRepository(HallucinogenDbContext dbContext, ILogger<PostRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occured when saving records on {nameof(PostRepository)}");
                return false;
            }        
        }

        public async Task<bool> CreatePostAsync(PostEntity entity)
        {
            await _dbContext.Posts.AddAsync(entity);

            return await SaveAsync();
        }

        public async Task<List<PostEntity>> GetPostsHomeScreenAsync()
        {
            return await _dbContext.Posts
                .OrderByDescending(pe => pe.PostDate)
                .Take(20)
                .Include(p => p.Likes)
                .ToListAsync();
        }

        public async Task<List<PostEntity>> GetPostOfUserFromUserIdAsync(string userId)
        {
            return await _dbContext.Posts
                .Where(pe => pe.UserId == userId)
                .OrderByDescending(pe => pe.PostDate)
                .Include(p => p.Likes)
                .ToListAsync();
        }

        public async Task<PostEntity> GetPostFromIdAsync(int postId)
        {
            return await _dbContext.Posts
                .Include(p => p.Likes)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<bool> LikePostAsync(PostLikeEntity entity)
        {
            await _dbContext.PostLikes.AddAsync(entity);

            return await SaveAsync();        
        }

        public async Task<bool> CommentPostAsync(PostCommentEntity entity)
        {
            await _dbContext.PostComments.AddAsync(entity);

            return await SaveAsync();        
        }

        public async Task<List<PostCommentEntity>> GetPostCommentsAsync(int postId)
        {
            return await _dbContext.PostComments
                .Where(pc => pc.PostId == postId)
                .OrderByDescending(pc => pc.CommentDate)
                .ToListAsync();
        }
    }
}