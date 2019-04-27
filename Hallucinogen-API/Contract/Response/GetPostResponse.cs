using Hallucinogen_API.Contract.Models;

namespace Hallucinogen_API.Contract.Response
{
    public class GetPostResponse: ResponseBase
    {
        public PostModel Post { get; set; }
    }
}