using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechLife.Model.ThietBi;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    public class ThietBiController : ControllerBase
    {
        private readonly IThietBiService _thietBiService;
        public ThietBiController(IThietBiService thietBiService)
        {
            _thietBiService = thietBiService;
        }

        [HttpPost()]
        public async Task<IActionResult> Create(ThietBiCreateRequest request)
        {
            var result = await _thietBiService.Create(request);
            
            return Ok(result);
        }
        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var result = await _thietBiService.Count();

            return Ok(result);
        }
    }
}