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
    public class ChuyenMucController : ControllerBase
    {
        private readonly IChuyenMucService _chuyenMucService;
        public ChuyenMucController(IChuyenMucService chuyenMucService)
        {
            _chuyenMucService = chuyenMucService;
        }

        [HttpGet("lang={languageId}&parentId={parentId}")]
        public async Task<IActionResult> GetAll(string languageId, int parentId)
        {
            var result = await _chuyenMucService.GetAll(languageId, parentId);
            return Ok(result);
        }
      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _chuyenMucService.GetById(id);
            return Ok(user);
        }
        [HttpGet("lehoi/lang={languageId}")]
        public async Task<IActionResult> DSDaSuDung(string languageId)
        {
            var result = await _chuyenMucService.DSDaSuDung(languageId);
            return Ok(result);
        }
       
    }
}
