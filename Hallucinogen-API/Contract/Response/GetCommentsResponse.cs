using System.Collections.Generic;
using Hallucinogen_API.Contract.Models;

namespace Hallucinogen_API.Contract.Response
{
    public class GetCommentsResponse: ResponseBase
    {
        public List<PostCommentModel> Comments { get; set; }

    }
}