using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Service.Interfaces;

namespace webapi.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPut("addAvatar"), Authorize]
        public async Task<ActionResult<OperationResult<bool>>> AddAvatarToPerson(IFormFile image)
        {
            string authHeader = Request!.Headers["Authorization"]!;
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var response = await _personService.AddAvatarAsync(image, authHeader.Substring("Bearer ".Length));
                if (response.Success) return Ok(response);
                return BadRequest(response);
            }

            return BadRequest(OperationResult<bool>.Fail(OperationCode.Unauthorized, "Пользователь не авторизован"));
        }
    }
}
