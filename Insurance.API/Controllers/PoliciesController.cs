using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.AppSettings;
using Insurance.Common.Models.Bases;
using Insurance.Common.Models.DTOs.Policy;
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
    public class PoliciesController : CustomControllerBase
    {
        private readonly IPoliciesService _policyService;
        public PoliciesController(IPoliciesService policyService, IOptions<PageSettings> pageSettings)
            : base (pageSettings)
        {
            _policyService = policyService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePolicyRequest payload)
        {
            if (ModelState.IsValid)
            {
                var response = await _policyService.CreatePolicyAsync(payload);
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
                var response = await _policyService.GetPolicyByIdAsync(payload);
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
                var response = await _policyService.GetAllPoliciesAsync(payload);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePolicyRequest payload)
        {
            if (ModelState.IsValid)
            {
                payload.Id = id;
                var response = await _policyService.UpdatePolicyAsync(payload);
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
                var response = await _policyService.DeletePolicyAsync(payload);
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