using System;
using System.Threading.Tasks;
using Hallucinogen_API.Contract.Request;
using Hallucinogen_API.Contract.Response;

namespace Hallucinogen_API.Services
{
    public interface IAccountService : IDisposable
    {
        Task<LoginResponse> PasswordLoginAsync(LoginRequest request);

        Task<SignUpResponse> SignUpAsync(SignUpRequest request);

    }
}