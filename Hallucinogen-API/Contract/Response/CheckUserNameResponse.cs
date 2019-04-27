namespace Hallucinogen_API.Contract.Response
{
    public class CheckUserNameResponse: ResponseBase
    {
        public bool IsUserNameAvailable { get; set; }
    }
}