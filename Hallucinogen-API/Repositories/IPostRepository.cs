using System.Collections.Generic;
using System.Threading.Tasks;
using Hallucinogen_API.Data.Entities;

namespace Hallucinogen_API.Repositories
{
    public interface IPostRepository : IRepository
    {
        Task<bool> CreatePostAsync(PostEntity entity);
        Task<List<PostEntity>> GetPostsHomeScreenAsync();
        Task<List<PostEntity>> GetPostOfUserFromUserIdAsync(string userId);
        Task<PostEntity> GetPostFromIdAsync(int postId);
        Task<bool> LikePostAsync(PostLikeEntity entity);
        Task<bool> CommentPostAsync(PostCommentEntity entity);
        Task<List<PostCommentEntity>> GetPostCommentsAsync(int postId);
    }
}