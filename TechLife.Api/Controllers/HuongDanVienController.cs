using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model;
using TechLife.Model.HuongDanVien;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    [Authorize]
    public class HuongDanVienController : ControllerBase
    {
        private readonly IHuongDanVienService _huongDanVienService;

        public HuongDanVienController(IHuongDanVienService huongDanVienService)
        {
            _huongDanVienService = huongDanVienService;
        }

        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetPagingRequest request)
        {
            var result = await _huongDanVienService.GetPaging(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _huongDanVienService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HuongDanVienModel request)

        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _huongDanVienService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _huongDanVienService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _huongDanVienService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HuongDanVienModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _huongDanVienService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("{id}/upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage(int id, [FromForm] ImageUploadRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _huongDanVienService.UploadImage(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}