using Microsoft.AspNetCore.Mvc;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.DTO;
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
        private readonly IPersonService _personService;
        private readonly IGroupMessageService _groupMessageService;
        private readonly IPersonalMessageService _personalMessageService;
        private readonly IPersonalMessageRepository _personalMessageRepository;
        private readonly IGroupService _groupService;
        public AuthController(IAuthService authService, IGroupService groupService, IPersonalMessageService personalMessageService, IPersonalMessageRepository personalMessageRepository, IPersonService personService, IGroupMessageService groupMessageService)
        {
            _authService = authService;
            _groupService = groupService;
            _personalMessageService = personalMessageService;
            _personalMessageRepository = personalMessageRepository;
            _personService = personService;
            _groupMessageService = groupMessageService;
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
        public async Task<ActionResult<IEnumerable<GroupMessageDto>>> Test(int id) 
        {
            var dialogs = await _groupMessageService.GetAllGroupMessagesAsync(id);

            if (dialogs != null)
            {
               
                return Ok(dialogs);
            }
            return BadRequest();
        }
    } 
}
