using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLife.Common;
using TechLife.Model;
using TechLife.Model.BinhLuan;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
  
    [ApiController]
    [Route("v1.0/[controller]")]
    public class BinhLuanController : ControllerBase
    {
        private readonly IBinhLuanService _binhLuanService;
        public BinhLuanController(IBinhLuanService binhLuanService)
        {
            _binhLuanService = binhLuanService;
        }

        [HttpGet("{type}/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(string type, int id, [FromQuery]GetPagingRequest request)
        {
            var result = await _binhLuanService.GetPaging(type, id, request);
            return Ok(result);
        }
        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Create(BinhLuanCreateRequest request)
        {
            var result = await _binhLuanService.Create(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(int id, BinhLuanUpdateRequest request)
        {
            var result = await _binhLuanService.Update(id, request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _binhLuanService.GetById(id);
            return Ok(user);
        }
    }
}
