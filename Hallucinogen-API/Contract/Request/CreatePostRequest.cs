using Hallucinogen_API.Contract.Models;

namespace Hallucinogen_API.Contract.Request
{
    public class CreatePostRequest: RequestBase
    {
        public PostModel Post { get; set; }
    }
}