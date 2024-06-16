using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.DTO.Auth;
using SignalRChat.Core.Service.Interfaces;

namespace webapi.Controllers
{
    [Route("api/password")]
    [ApiController]
    public class PassResetController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PassResetController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpPost("get-reset-code")]
        public async Task<ActionResult<OperationResult<bool>>> GetResetCode(string login)
        {
            var response = await _passwordService.GetResetCodeAsync(login);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
        [HttpPost("check-reset-code")]
        public async Task<ActionResult<OperationResult<bool>>> CheckResetCode(CodeRequest request)
        {
            var response = await _passwordService.CheckResetCodeAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
        [HttpPut("change-pass")]
        public async Task<ActionResult<OperationResult<bool>>> ChangePass(ChangePassRequest request)
        {
            var response = await _passwordService.ChangePasswordAsync(request);
            if (response.Success) return Ok(response);
            return BadRequest(response);
        }
    }
}
