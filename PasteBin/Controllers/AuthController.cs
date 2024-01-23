using Microsoft.AspNetCore.Mvc;
using PasteBin.Domain.DTOs;
using PasteBin.Services.Interfaces;
using PasteBinApi.Domain.DTOs;

namespace PasteBinApi.Controllers
{
    [ApiController]
    [Route("PasteBin/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILoginService _loginService;

        public AuthController(IRegisterService registerService,
            ILoginService loginService)
        {
            _registerService = registerService;
            _loginService = loginService;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responseRegister = await _registerService.RegisterUser(registerDto);

            if (responseRegister.StatusCode == 400)
                return BadRequest(responseRegister.Description);

            if (responseRegister.StatusCode == 500)
                return Problem(responseRegister.Description);

            return Ok(responseRegister);

        }
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responseLogin = await _loginService.Login(loginDto);

            if (responseLogin.StatusCode == 400)
                return BadRequest(responseLogin.Description);

            if(responseLogin.StatusCode == 404)
                return NotFound(responseLogin.Description);

            if(responseLogin.StatusCode == 500)
                return Problem(responseLogin.Description);

            return Ok(responseLogin);

        }
    }
}
