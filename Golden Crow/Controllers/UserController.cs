using Golden_Crow.DTOs;
using Golden_Crow.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Golden_Crow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")] //POST localhost:8080/ api/user/register
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.RegisterAsync(request.Login, request.Name, request.Password);

            if (result)
            {
                return Ok();
            }

            return BadRequest(new { Message = "User registration failed" });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid == false)
            { 
                return BadRequest(ModelState);
            }

            var result = await _userService.LoginAsync(request.Login, request.Password);
            if (result)
            {
                return Ok(new { Token = result.Value });
            }

            return NotFound();
        
        }



        
    }
}
