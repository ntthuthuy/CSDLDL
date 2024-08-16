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
    public class LoaiHinhController : ControllerBase
    {
        private readonly ILoaiHinhService _loaiHinhService;
        public LoaiHinhController(ILoaiHinhService loaiHinhService)
        {
            _loaiHinhService = loaiHinhService;
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetPagingRequest request)
        {
            var result = await _loaiHinhService.GetPaging(request);
            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _loaiHinhService.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoaiHinhModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loaiHinhService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _loaiHinhService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _loaiHinhService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoaiHinhModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loaiHinhService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
