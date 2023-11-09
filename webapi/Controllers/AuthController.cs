using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.Service.Interfaces;

namespace webapi
{
    [Route ("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPersonalMessageService _personalMessageService;

        public AuthController(IAuthService authService, IPersonalMessageService personalMessageService)
        {
            _authService = authService;
            _personalMessageService = personalMessageService;
        }

        [HttpPost("signin")]
        public async Task<ActionResult<OperationResult<AuthResponse>>> Signin(AuthRequest request)
        {
            var response = await _authService.AuthenticateAsync(request);
            if (response.Success)  return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<OperationResult<AuthResponse>>> Signup( SignupRequest request)
        {
            var response = await _authService.SingupAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
        [Authorize]
        [HttpPost("test")]
        public async Task<IEnumerable<PersonalMessageDto>> Test()
        {
            return await _personalMessageService.GetAllMessageInDialogAsync(1,2);
        }
    } 
}
