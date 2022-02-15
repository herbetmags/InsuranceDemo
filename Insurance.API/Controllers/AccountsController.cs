using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.AppSettings;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Insurance.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : CustomControllerBase
    {
        private readonly IAccountsService _accountsService;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        public AccountsController(IAccountsService accountsService,
            IJwtAuthenticationService jwtAuthenticationService,
            IOptions<PageSettings> pageSettings) : base (pageSettings)
        {
            _accountsService = accountsService;
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest payload)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountsService.RegisterUserAsync(payload);
                if (!response.IsSuccess)
                {
                    return Ok(response.ErrorMessage);
                }
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest payload)
        {
            if (ModelState.IsValid)
            {
                var response = await _jwtAuthenticationService.GetAuthenticationTokenAsync(payload);
                if (!response.IsSuccess)
                {
                    return Unauthorized(response.ErrorMessage);
                }
                if (response.Token == null)
                {
                    return Unauthorized();
                }
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}