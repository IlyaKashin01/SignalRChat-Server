using Microsoft.AspNetCore.Mvc;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.DTO;
using SignalRChat.Core.DTO.Messages;
using SignalRChat.Core.Service.Impl;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Data.Repositories.Interfaces;
using SignalRChat.Domain.Entities;
using System.Text.RegularExpressions;

namespace webapi
{
    [Route ("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPersonalMessageService _personalMessageService;
        private readonly IGroupService _groupService;
        public AuthController(IAuthService authService, IPersonalMessageService personalMessageService, IGroupService groupService)
        {
            _authService = authService;
            _personalMessageService = personalMessageService;
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
        public async Task<ActionResult<IEnumerable<Dialog>>> Test()
        {
            var response = await _groupService.GetAllGroupsAsync(2);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }
    } 
}
