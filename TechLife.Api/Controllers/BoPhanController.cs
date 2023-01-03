using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [Route("v1.0/[controller]")]
    [ApiController]
    public class BoPhanController : ControllerBase
    {
        private readonly IBoPhanService _boPhanService;
        public BoPhanController(IBoPhanService boPhanService)
        {
            _boPhanService = boPhanService;
        }
        [HttpGet("{linhvucId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int linhvucId)
        {
            var result = await _boPhanService.GetAll(linhvucId);
            return Ok(result);
        }
    }
}
