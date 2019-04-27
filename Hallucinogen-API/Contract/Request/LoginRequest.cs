namespace Hallucinogen_API.Contract.Request
{
    public class LoginRequest : RequestBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}