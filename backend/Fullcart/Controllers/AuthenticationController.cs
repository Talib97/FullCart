using FullCart.Application.Model;
using FullCart.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fullcart.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthenticationController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] UserCreateRequest request)
        {
            return Ok(await _authService.Register(request));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] UserCreateRequest request)
        {
            return Ok(await _authService.Login(request));
        }
    }
}
