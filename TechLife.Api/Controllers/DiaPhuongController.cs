using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    public class DiaPhuongController : ControllerBase
    {
        private readonly IDiaPhuongService _diaPhuongService;
        public DiaPhuongController(IDiaPhuongService diaPhuongService)
        {
            _diaPhuongService = diaPhuongService;
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetPagingRequest request)
        {
            var result = await _diaPhuongService.GetPaging(request);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _diaPhuongService.GetAll();
            return Ok(result);
        }
       
        [AllowAnonymous]
        [HttpGet("parent/{id}")]
        [EnableCors]
        public async Task<IActionResult> GetAll(int id)
        {
            var result = await _diaPhuongService.GetAllByParent(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DiaPhuongModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _diaPhuongService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _diaPhuongService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _diaPhuongService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DiaPhuongModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _diaPhuongService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
