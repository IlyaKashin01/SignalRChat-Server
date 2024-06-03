using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SignalRChat.Common.Auth;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Data.Repositories.Interfaces;
using SignalRChat.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;

namespace SignalRChat.Core.Service.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly AuthOptions _authOptions;
        public AuthService(IPersonRepository personRepository, IMapper mapper, IOptions<AuthOptions> authOptions)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _authOptions = authOptions.Value;
        }

        public async Task<OperationResult<AuthResponse>> AuthenticateAsync(AuthRequest request)
        {
            var person = await _personRepository.FindByLoginAsync(request.Login);

            if (person == null)
                return OperationResult<AuthResponse>.Fail(
                    OperationCode.Unauthorized,
                    $"Пользователь с логином {request.Login} не зарегистрирован");

            if(BC.Verify(request.Password, person.PasswordHash))
            {
                var token = GenerateJwtTokenAsync(person);
                var response = new AuthResponse
                {
                    Person = _mapper.Map<PersonResponse>(person),
                    Token = token
                };

                return new OperationResult<AuthResponse>(response);
            }
            return OperationResult<AuthResponse>.Fail(OperationCode.ValidationError, "Неверный пароль");
        }

        public async Task<OperationResult<int>> SingupAsync(SignupRequest request)
        {
            if (await _personRepository.FindByLoginAsync(request.Login) is not null)
                return OperationResult<int>.Fail(
                    OperationCode.AlreadyExists,
                    $"Пользователь с логином {request.Login} уже существует");

            var person = _mapper.Map<Person>(request);

            person.PasswordHash = BC.HashPassword(request.Password);
            person.Role = "user";
           
            var result = await _personRepository.CreateAsync(person);

            return new OperationResult<int>(result);
        }

        private string GenerateJwtTokenAsync(Person person)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Sid, person.Id.ToString()),
                new Claim(ClaimTypes.Role, person!.Role)
            };
            var jwt = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_authOptions.TokenLifeTime),  // действие токена истекает через 7 дней
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
