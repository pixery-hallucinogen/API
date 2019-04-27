using System.Security.Claims;
using System.Threading.Tasks;
using Hallucinogen_API.Common;
using Hallucinogen_API.Contract.Request;
using Hallucinogen_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hallucinogen_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("v1/posts")]
    public class PostController : ApiControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
      

        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest();

            var response = await _postService.CreatePostAsync(request, userId);
            
            return GenerateResponse(response);
        }
        
        [HttpPost("like")]
        public async Task<IActionResult> LikePost([FromBody] LikePostRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest();

            var response = await _postService.LikePostAsync(request, userId);
            
            return GenerateResponse(response);
        }
        
        [HttpPost("comment")]
        public async Task<IActionResult> CommentPost([FromBody] CommentPostRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return BadRequest();

            var response = await _postService.CommentPostAsync(request, userId);
            
            return GenerateResponse(response);
        }
        
        [HttpGet("comments/{postId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostComments(int postId)
        {
            
            var response = await _postService.GetPostCommentsAsync(postId);

            return GenerateResponse(response);
        }

        

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPosts()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var response = await _postService.GetPostsAsync(userId);

            return GenerateResponse(response);
        }

        [AllowAnonymous]
        [HttpGet("coordinates/{latitude}/{longitude}")]
        public async Task<IActionResult> GetPostsWithCoordinates(double latitude, double longitude)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var response = await _postService.GetPostsFromLocationAsync(latitude, longitude, userId);

            return GenerateResponse(response);
        }
        
        
        [HttpGet("{postId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostWithId(int postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var response = await _postService.GetPostFromIdAsync(postId, userId);

            return GenerateResponse(response);
        }

        [HttpGet("user/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostOfUserFromUserId(string userId)
        {
            var requesterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var response = await _postService.GetPostOfUserFromUserIdAsync(userId, requesterId);

            return GenerateResponse(response);
        }
        
    }
}