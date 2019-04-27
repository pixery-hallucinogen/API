using System;
using System.Threading.Tasks;
using Hallucinogen_API.Contract.Request;
using Hallucinogen_API.Contract.Response;

namespace Hallucinogen_API.Services
{
    public interface IPostService : IDisposable
    {
        Task<GetPostResponse> GetPostFromIdAsync(int postId, string requesterId);
        Task<GetPostsResponse> GetPostOfUserFromUserIdAsync(string userId, string requesterId);
        Task<GetPostsResponse> GetPostsAsync(string requesterId);
        Task<CreatePostResponse> CreatePostAsync(CreatePostRequest request, string userId);
        Task<LikePostResponse> LikePostAsync(LikePostRequest request, string userId);
        Task<CommentPostResponse> CommentPostAsync(CommentPostRequest request, string userId);
        Task<GetCommentsResponse> GetPostCommentsAsync(int postId);
    }
}