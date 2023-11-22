using Microsoft.AspNetCore.Mvc;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.DTO;
using SignalRChat.Core.Service.Impl;
using SignalRChat.Core.Service.Interfaces;
using System.Text.RegularExpressions;

namespace webapi
{
    [Route ("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPersonService _groupService;
        public AuthController(IAuthService authService, IPersonService groupService)
        {
            _authService = authService;
            _groupService = groupService;
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


        [HttpGet("test")]
        public async Task<ActionResult<IEnumerable<PersonResponse>>> Test(int id) 
        {
            var members = await _groupService.GetAllUsersAsync(id);
            if (members != null)
            {
               
                return Ok(members);
            }
            return BadRequest();
        }
    } 
}
