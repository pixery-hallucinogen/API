namespace Hallucinogen_API.Contract.Request
{
    public class CommentPostRequest: RequestBase
    {
        public int PostId { get; set; }
        public string Comment { get; set; }
    }
}