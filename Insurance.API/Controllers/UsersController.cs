using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.AppSettings;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Insurance.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : CustomControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService, IOptions<PageSettings> pageSettings)
            : base (pageSettings)
        {
            _usersService = usersService;
        }
        //[Authorize(Policy = "AdminOnly")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest payload)
        {
            if (ModelState.IsValid)
            {
                var response = await _usersService.CreateUserAsync(payload);
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
        //[Authorize(Policy = "AdminAndAgent")]
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                var payload = new SearchByIdRequest { Id = id };
                var response = await _usersService.GetUserByIdAsync(payload);
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
        //[Authorize(Policy = "AdminAndAgent")]
        [HttpGet("page/{index}")]
        public async Task<IActionResult> GetAll([Range(minimum: 1, maximum: 65535)] ushort index)
        {
            if (ModelState.IsValid)
            {
                var payload = new SearchRequest
                {
                    PageIndex = index,
                    PageSize = PageSettings.PageSize
                };
                var response = await _usersService.GetAllUsersAsync(payload);
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
        //[Authorize(Policy = "AdminAndClient")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest payload)
        {
            if (ModelState.IsValid)
            {
                payload.Id = id;
                var response = await _usersService.UpdateUserAsync(payload);
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
        //[Authorize(Policy = "AdminOnly")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var payload = new DeleteRequest { Id = id };
                var response = await _usersService.DeleteUserAsync(payload);
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
    }
}
