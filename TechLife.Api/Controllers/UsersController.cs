using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLife.Common;
using TechLife.Model;
using TechLife.Model.User;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1.0/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFileUploadService _fileUploadService;
        public UsersController(IUserService userService
            , IFileUploadService fileUploadService
            , IRoleService roleService)
        {
            _userService = userService;
            _fileUploadService = fileUploadService;
        }


        [HttpGet("authenticate/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Authencate(username);

            if (string.IsNullOrEmpty(result.ResultObj))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Login(request);

            if (string.IsNullOrEmpty(result.ResultObj))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return Ok();
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetPagingRequest request)
        {
            var result = await _userService.GetUsersPaging(request);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }
        [HttpGet("get/{username}")]
        public async Task<IActionResult> GetByUserName(string username)
        {
            var user = await _userService.GetByUserName(username);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("social")]
        [AllowAnonymous]
        public async Task<IActionResult> Social([FromBody] UserSocialRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegiterSocial(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("changepass/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePass(Guid id, [FromBody] UserChangePassRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.ChangePass(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("regiter")]
        [AllowAnonymous]
        public async Task<IActionResult> Regiter([FromBody] UserRegiterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("upload/avata/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAvata(Guid id, [FromForm] IFormFile file)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var upload = await _fileUploadService.SaveFile(file);

            var result = await _userService.UploadAvata(id, upload);

            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
