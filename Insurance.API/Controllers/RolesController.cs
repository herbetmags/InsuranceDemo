using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.AppSettings;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Role;
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
    public class RolesController : CustomControllerBase
    {
        private readonly IRolesService _roleService;

        public RolesController(IRolesService roleService, IOptions<PageSettings> pageSettings)
            : base (pageSettings)
        {
            _roleService = roleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequest payload)
        {
            if (ModelState.IsValid)
            {
                var response = await _roleService.CreateRoleAsync(payload);
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

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                var payload = new SearchByIdRequest { Id = id };
                var response = await _roleService.GetRoleByIdAsync(payload);
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
                var response = await _roleService.GetAllRolesAsync(payload);
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleRequest payload)
        {
            if (ModelState.IsValid)
            {
                payload.Id = id;
                var response = await _roleService.UpdateRoleAsync(payload);
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var payload = new DeleteRequest { Id = id };
                var response = await _roleService.DeleteRoleAsync(payload);
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