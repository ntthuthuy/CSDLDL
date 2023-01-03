using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLife.Common;
using TechLife.Model;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    [Authorize]
    public class DanhGiaController : ControllerBase
    {
        private readonly IDanhGiaService _DanhGiaService;
        public DanhGiaController(IDanhGiaService DanhGiaService)
        {
            _DanhGiaService = DanhGiaService;
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] DanhGiaModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _DanhGiaService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
     
    }
}
