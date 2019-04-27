using Hallucinogen_API.Contract.Models;

namespace Hallucinogen_API.Contract.Response
{
    public class GetUserResponse : ResponseBase
    {
        public UserModel User { get; set; }
    }
}