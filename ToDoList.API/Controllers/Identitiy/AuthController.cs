using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Abstractions.IService;
using ToDoList.Domain.Entities.DTOs;

namespace ToDoList.API.Controllers.Identitiy
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm]RequestLogin login)
        {
            var res = await _authService.GenerateToken(login);

            return Ok(res);
        }
    }
}
