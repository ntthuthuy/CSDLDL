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
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMucService _danhMucService;
        public DanhMucController(IDanhMucService danhMucService)
        {
            _danhMucService = danhMucService;
        }
        [HttpGet("paging/{loaiId}")]
        public async Task<IActionResult> GetAllPaging(int loaiId, [FromQuery] GetPagingRequest request)
        {
            var result = await _danhMucService.GetPaging(loaiId, request);
            return Ok(result);
        }
        [HttpGet("getall/{loaiId}")]
        public async Task<IActionResult> GetAll(int loaiId)
        {
            var result = await _danhMucService.GetAll(loaiId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DanhMucModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _danhMucService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _danhMucService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _danhMucService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DanhMucModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _danhMucService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
