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
    public class MucDoThongThaoNgoaiNguController : ControllerBase
    {
        private readonly IMucDoThongThaoNgoaiNguService _mucDoThongThaoNgoaiNguService;
        public MucDoThongThaoNgoaiNguController(IMucDoThongThaoNgoaiNguService mucDoThongThaoNgoaiNguService)
        {
            _mucDoThongThaoNgoaiNguService = mucDoThongThaoNgoaiNguService;
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetPagingRequest request)
        {
            var result = await _mucDoThongThaoNgoaiNguService.GetPaging(request);
            return Ok(result);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mucDoThongThaoNgoaiNguService.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MucDoThongThaoNgoaiNguModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mucDoThongThaoNgoaiNguService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _mucDoThongThaoNgoaiNguService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mucDoThongThaoNgoaiNguService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MucDoThongThaoNgoaiNguModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mucDoThongThaoNgoaiNguService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
