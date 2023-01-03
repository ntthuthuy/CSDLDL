using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    [Authorize]

    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IRoleGroupService _roleGroupService;

        public RolesController(IRoleService roleService,
            IRoleGroupService roleGroupService)
        {
            _roleService = roleService;
            _roleGroupService = roleGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetPagingRequest request)
        {
            var result = await _roleService.GetRolesPaging(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _roleService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _roleService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RoleModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("assign/{id}")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] List<RoleModel> request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleService.RoleAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("unassign/{roleId}/{userId}")]
        public async Task<IActionResult> RoleUnAssign(Guid roleId, Guid userId)
        {
            var user = await _roleService.RoleUnAssign(roleId, userId);
            return Ok(user);
        }
        [HttpGet("rolegroup/{groupId}")]
        public async Task<IActionResult> GetAll(int groupId)
        {
            var roles = await _roleGroupService.GetAll(groupId);
            return Ok(roles);
        }

        [HttpPost("rolegroup/{groupId}")]
        public async Task<IActionResult> Create(int groupId, [FromBody] List<RoleModel> request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _roleGroupService.Create(groupId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("rolegroup/{groupId}")]
        public async Task<IActionResult> Remove(int groupId)
        {
            var result = await _roleGroupService.Remove(groupId);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("groupunassign/{roleId}/{groupId}")]
        public async Task<IActionResult> GroupRoleUnAssign(Guid roleId, int groupId)
        {
            var user = await _roleGroupService.RoleUnAssign(roleId, groupId);
            return Ok(user);
        }

    }
}