using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    public class TienNghiController : ControllerBase
    {
        private readonly ITienNghiService _tienNghiService;
        public TienNghiController(ITienNghiService tienNghiService)
        {
            _tienNghiService = tienNghiService;
        }

        [HttpGet("linhvuc={linhvucId}")]
        public async Task<IActionResult> GetAll(int linhvucId)
        {
            var result = await _tienNghiService.GetAll(linhvucId);
            return Ok(result);
        }
    }
}
