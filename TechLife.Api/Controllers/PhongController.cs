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
    public class PhongController : ControllerBase
    {
        private readonly IPhongService _phongService;
        public PhongController(IPhongService phongService)
        {
            _phongService = phongService;
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetPagingRequest request)
        {
            var result = await _phongService.GetPaging(request);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _phongService.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PhongModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _phongService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _phongService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _phongService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PhongModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _phongService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
