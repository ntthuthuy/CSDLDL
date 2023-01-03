using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TechLife.Model;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _groupService.GetAll();
            return Ok(groups);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var groups = await _groupService.GetById(id);
            return Ok(groups);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _groupService.Create(request);
            if (!result.IsSuccessed)
                return BadRequest();
            else
            {
                return Ok(request);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _groupService.Delete(id);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GroupModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _groupService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}