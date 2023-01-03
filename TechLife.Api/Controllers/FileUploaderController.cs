using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TechLife.Model.HueCIT;
using TechLife.Service;
using TechLife.Service.HueCIT;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    [Authorize]
    public class FileUploaderController : ControllerBase
    {
        private readonly IFileUploaderService _fileUploadService;

        public FileUploaderController(IFileUploaderService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("upload/{doituong}/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadFile(string doituong, string id, [FromForm] FileUploadRequest data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _fileUploadService.UploadFile(data, doituong, id);

            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}