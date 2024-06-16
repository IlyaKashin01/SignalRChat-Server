using Microsoft.AspNetCore.Mvc;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.DTO;
using SignalRChat.Core.DTO.Auth;
using SignalRChat.Core.DTO.Messages;
using SignalRChat.Core.Service.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace webapi
{
    [Route ("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signin")]
        public async Task<ActionResult<OperationResult<AuthResponse>>> Signin(AuthRequest request)
        {
            var response = await _authService.AuthenticateAsync(request);
            if (response.Success)  return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<OperationResult<int>>> Signup( SignupRequest request)
        {
            var response = await _authService.SingupAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }

        private readonly string _botToken = "6509249568:AAFjLS0mh8zGY_sGQecmTdErQmduMCDABVg";

        [HttpGet("tg-auth")]
        public async Task<ActionResult<OperationResult<AuthResponse>>> TelegramAuth(TelegramAuthRequest model)
        {
            if (ValidateTelegramAuth(model, _botToken))
            {
                var request = new AuthRequest { Login = model.Username, Password = model.Hash };
                var response = await _authService.AuthenticateAsync(request);
                if (response.Success) return Ok(response);
                return BadRequest(response);
            }
            else
            {
                return Unauthorized();
            }
        }
        [HttpPost("tg-signup")]
        public async Task<ActionResult<OperationResult<int>>> TelegramSignup(TelegramAuthRequest model)
        {
            if (ValidateTelegramAuth(model, _botToken))
            {
                var request = new SignupRequest {FirstName=model.FirstName, LastName=model.LastName, Login = model.Username, Password = model.Hash };
                var response = await _authService.SingupAsync(request);
                if (response.Success) return Ok(response);
                return BadRequest(response);
            }
            else
            {
                return Unauthorized();
            }
        }

        private bool ValidateTelegramAuth(TelegramAuthRequest model, string botToken)
        {
            var dataCheckString =
                $"auth_date={model.AuthDate}\nfirst_name={model.FirstName}\nid={model.Id}\nlast_name={model.LastName}\nusername={model.Username}";
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(botToken)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));
                var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();

                return hashString == model.Hash;
            }
        }
    } 
}
