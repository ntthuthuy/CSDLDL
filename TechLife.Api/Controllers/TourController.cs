using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model;
using TechLife.Model.Tour;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    [Authorize]
    public class TourController : ControllerBase
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPaging([FromQuery] TourRequets request)
        {
            var result = await _tourService.GetPaging(request);
            return Ok(result);
        }

        [HttpGet("getall/{luhanhId}")]
        public async Task<IActionResult> GetAll(int luhanhId)
        {
            var result = await _tourService.GetAll(luhanhId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TourModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tourService.Create(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("hanhtrinh")]
        public async Task<IActionResult> AddItem([FromBody] HanhTrinhModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tourService.AddItem(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _tourService.GetById(id);
            return Ok(user);
        }
        [HttpGet("{id}/ngay")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNgayHanhTrinhById(int id)
        {
            var user = await _tourService.GetListNgayHanhTrinhByTour(id);
            return Ok(user);
        }

        [HttpGet("hanhtrinh/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetItemById(int id)
        {
            var user = await _tourService.GetItemById(id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tourService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TourVm request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tourService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("hanhtrinh/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HanhTrinhModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tourService.UpdateItem(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("{tourId}/upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage(int tourId, [FromForm] ImageUploadRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tourService.UploadImage(tourId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}