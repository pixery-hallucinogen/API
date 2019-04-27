using System.Collections.Generic;
using Hallucinogen_API.Contract.Models;

namespace Hallucinogen_API.Contract.Response
{
    public class GetPostsResponse: ResponseBase
    {
        public List<PostModel> Posts { get; set; }
    }
}