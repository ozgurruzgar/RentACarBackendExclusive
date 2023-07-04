using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync() 
        {
            var result = await _userService.GetAllAsync();
            if(result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            var result = await _userService.GetbyIdAsync(userId);
            if (result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
