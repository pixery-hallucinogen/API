using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hallucinogen_API.Contract.Request;
using Hallucinogen_API.Contract.Response;
using Hallucinogen_API.Data.Entities;
using Hallucinogen_API.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Hallucinogen_API.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountService> _logger;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IUserMapper _userMapper;
        private readonly UserManager<UserEntity> _userManager;

        public AccountService(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager,
            IUserMapper userMapper, ILogger<AccountService> logger, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userMapper = userMapper;
            _logger = logger;
            _configuration = configuration;
        }

        public void Dispose()
        {
            _userManager?.Dispose();
        }

        public async Task<GetUserResponse> GetUserFromIdAsync(string userId)
        {
            var response = new GetUserResponse();

            var userEntity = await _userManager.FindByIdAsync(userId);

            if (userEntity == null)
            {
                _logger.LogWarning($"User with id {userId} not found");
                response.StatusCode = (int) HttpStatusCode.NotFound;
                return response;
            }

            var userModel = _userMapper.ToModel(userEntity);

            response.StatusCode = (int) HttpStatusCode.OK;
            response.User = userModel;

            return response;
        }

        public async Task<CheckUserNameResponse> CheckUserNameAsync(CheckUserNameRequest request)
        {
            var response = new CheckUserNameResponse();

            var userEntity = await _userManager.FindByNameAsync(request.UserName);

            response.IsUserNameAvailable = userEntity == null;
            response.StatusCode = (int) HttpStatusCode.OK;

            return response;
        }


        public async Task<LoginResponse> PasswordLoginAsync(LoginRequest request)
        {
            var response = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (!response.Succeeded)
                return new LoginResponse
                {
                    StatusCode = (int) HttpStatusCode.Unauthorized
                };

            var userEntity = await _userManager.FindByNameAsync(request.UserName);
            var claims = await _userManager.GetClaimsAsync(userEntity);
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, userEntity.Email));
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, userEntity.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userEntity.Id));

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(3),
                signingCredentials: credentials
            );

            return new LoginResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                StatusCode = (int) HttpStatusCode.OK
            };
        }

        public async Task<SignUpResponse> SignUpAsync(SignUpRequest request)
        {
            // Create the user using userManager 
            var response = await _userManager.CreateAsync(new UserEntity
            {
                Email = request.Email,
                UserName = request.UserName,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                RegistrationDate = DateTime.UtcNow
            }, request.Password);

            // return if creation failed
            if (!response.Succeeded)
                return new SignUpResponse
                {
                    StatusCode = (int) HttpStatusCode.Unauthorized
                };

            return new SignUpResponse
            {
                StatusCode = (int) HttpStatusCode.Created
            };
        }
    }
}