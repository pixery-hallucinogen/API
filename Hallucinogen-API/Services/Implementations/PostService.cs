using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hallucinogen_API.Contract.Request;
using Hallucinogen_API.Contract.Response;
using Hallucinogen_API.Data.Entities;
using Hallucinogen_API.Mappers;
using Hallucinogen_API.Repositories;
using Microsoft.Extensions.Logging;

namespace Hallucinogen_API.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostMapper _postMapper;

        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepository postRepository,
            IPostMapper postMapper, ILogger<PostService> logger)
        {
            _postRepository = postRepository;
            _postMapper = postMapper;
            _logger = logger;
        }

        
        public void Dispose()
        {
            _postRepository?.Dispose();
        }

        public async Task<GetPostResponse> GetPostFromIdAsync(int postId, string requesterId = null)
        {
            var entity = await _postRepository.GetPostFromIdAsync(postId);
            
            var response = new GetPostResponse();
            
            if (entity == null)
            {
                _logger.LogWarning($"Post with Id {postId} not found");
                response.StatusCode = (int) HttpStatusCode.NotFound;
                return response;
            }

            var postModel = _postMapper.ToModel(entity, requesterId);
            
            response.StatusCode = (int) HttpStatusCode.OK;
            response.Post = postModel;

            return response;
        }

        public async Task<GetPostsResponse> GetPostOfUserFromUserIdAsync(string userId, string requesterId = null)
        {
            var postEntities = await _postRepository.GetPostOfUserFromUserIdAsync(userId);
            
            var response = new GetPostsResponse
            {
                StatusCode = (int) HttpStatusCode.OK,
                Posts = postEntities.Select(p => _postMapper.ToModel(p, requesterId)).ToList()
            };

            return response;
            
        }

        public async Task<GetPostsResponse> GetPostsAsync(string requesterId = null)
        {
            var postEntities = await _postRepository.GetPostsHomeScreenAsync();
            
            var response = new GetPostsResponse
            {
                StatusCode = (int) HttpStatusCode.OK,
                Posts = postEntities.Select(p => _postMapper.ToModel(p, requesterId)).ToList()
            };

            return response;        
        }

        public async Task<CreatePostResponse> CreatePostAsync(CreatePostRequest request, string userId)
        {
            var entity = _postMapper.ToEntity(request.Post);
            entity.UserId = userId;
            var result = await _postRepository.CreatePostAsync(entity);

            if (!result)
                return new CreatePostResponse
                {
                    StatusCode = (int) HttpStatusCode.Unauthorized
                };

            var response = new CreatePostResponse
            {
                StatusCode = (int) HttpStatusCode.Created
            };

            return response;                
        }

        public async Task<LikePostResponse> LikePostAsync(LikePostRequest request, string userId)
        {
            var entity = new PostLikeEntity
            {
                PostId = request.PostId,
                UserId = userId
            };

            var result = await _postRepository.LikePostAsync(entity);
            
            if (!result)
                return new LikePostResponse
                {
                    StatusCode = (int) HttpStatusCode.Unauthorized
                };

            var response = new LikePostResponse
            {
                StatusCode = (int) HttpStatusCode.Created
            };

            return response;        
        }

        public async Task<CommentPostResponse> CommentPostAsync(CommentPostRequest request, string userId)
        {
            var entity = new PostCommentEntity
            {
                PostId = request.PostId,
                UserId = userId,
                Comment = request.Comment,
                CommentDate = DateTime.UtcNow
            };

            var result = await _postRepository.CommentPostAsync(entity);
            
            if (!result)
                return new CommentPostResponse
                {
                    StatusCode = (int) HttpStatusCode.Unauthorized
                };

            var response = new CommentPostResponse
            {
                StatusCode = (int) HttpStatusCode.Created
            };

            return response;                
        }
    }
}