using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.AppSettings;
using Insurance.Common.Models.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Insurance.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StatusController : CustomControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService, IOptions<PageSettings> pageSettings)
            : base (pageSettings)
        {
            _statusService = statusService;
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                var payload = new SearchByIdRequest { Id = id };
                var response = await _statusService.GetStatusByIdAsync(payload);
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

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            if (ModelState.IsValid)
            {
                var payload = new SearchRequest
                {
                    PageIndex = 1,
                    PageSize = PageSettings.PageSize,
                };
                var response = await _statusService.GetAllStatusAsync(payload);
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
