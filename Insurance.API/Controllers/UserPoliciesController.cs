using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.AppSettings;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.UserPolicy;
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
    public class UserPoliciesController : CustomControllerBase
    {
        private readonly IUserPoliciesService _userPoliciesService;
        public UserPoliciesController(IUserPoliciesService userPoliciesService,
            IOptions<PageSettings> pageSettings) : base (pageSettings)
        {
            _userPoliciesService = userPoliciesService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserPolicyRequest payload)
        {
            if (ModelState.IsValid)
            {
                var response = await _userPoliciesService.CreateUserPolicyAsync(payload);
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
                var response = await _userPoliciesService.GetUserPolicyByIdAsync(payload);
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
                var response = await _userPoliciesService.GetAllUserPoliciesAsync(payload);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserPolicyRequest payload)
        {
            if (ModelState.IsValid)
            {
                payload.Id = id;
                var response = await _userPoliciesService.UpdateUserPolicyAsync(payload);
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
                var response = await _userPoliciesService.DeleteUserPolicyAsync(payload);
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
