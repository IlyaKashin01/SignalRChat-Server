using Microsoft.AspNetCore.Mvc;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.Service.Interfaces;

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
    } 
}
