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
    public class LoaiPhongController : ControllerBase
    {
        private readonly ILoaiPhongService _loaiPhongService;
        public LoaiPhongController(ILoaiPhongService LoaiPhongService)
        {
            _loaiPhongService = LoaiPhongService;
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetPagingRequest request)
        {
            var result = await _loaiPhongService.GetPaging(request);
            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _loaiPhongService.GetAll(0);
            return Ok(result);
        }
        [HttpGet("organ/{organId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int organId)
        {
            var result = await _loaiPhongService.GetAll(organId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoaiPhongModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loaiPhongService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("organ/{organId}")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateByOrganId(int organId, [FromBody] LoaiPhongModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loaiPhongService.CreateByOrgan(organId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _loaiPhongService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _loaiPhongService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(int id, [FromBody] LoaiPhongModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _loaiPhongService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
