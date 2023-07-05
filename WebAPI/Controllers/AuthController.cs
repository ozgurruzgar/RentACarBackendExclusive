using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);
            var newUser = new SuccessDataResult<IDataResult<User>>(userToLogin);
            if (!newUser.Success)
            {
                return BadRequest(newUser.Message);
            }
            var result = _authService.CreateAccessToken(newUser.Data.Data);
            var login = new SuccessDataResult<IDataResult<AccessToken>>(result);
            if (login.Success)
            {
                return Ok(login.Data);
            }

            return BadRequest(login.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result != null)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
