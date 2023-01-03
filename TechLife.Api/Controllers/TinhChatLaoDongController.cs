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
    public class TinhChatLaoDongController : ControllerBase
    {
        private readonly ITinhChatLaoDongService _tinhChatLaoDongService;
        public TinhChatLaoDongController(ITinhChatLaoDongService tinhChatLaoDongService)
        {
            _tinhChatLaoDongService = tinhChatLaoDongService;
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetPagingRequest request)
        {
            var result = await _tinhChatLaoDongService.GetPaging(request);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tinhChatLaoDongService.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TinhChatLaoDongModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tinhChatLaoDongService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _tinhChatLaoDongService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tinhChatLaoDongService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TinhChatLaoDongModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tinhChatLaoDongService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
