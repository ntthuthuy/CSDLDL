using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechLife.Common;
using TechLife.Model;
using TechLife.Model.DuLieuDuLich;
using TechLife.Model.HoSoVanBan;
using TechLife.Service;
using TechLife.Service.HueCIT;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    [Authorize]
    public class DuLieuDuLichController : ControllerBase
    {

        private readonly IDuLieuDuLichService _duLieuDuLichService;
        private readonly IHoSoService _hoSoService;

        public DuLieuDuLichController(IDuLieuDuLichService duLieuDuLichService, IHoSoService hoSoService)
        {
            _duLieuDuLichService = duLieuDuLichService;
            _hoSoService = hoSoService;
        }

        [HttpGet("luutru")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCoSoLuuTru()
        {
            var result = await _duLieuDuLichService.GetAll_CSLT();
            return Ok(result);
        }
        [HttpGet("nhahang")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllNhaHang()
        {
            var result = await _duLieuDuLichService.GetAll_NhaHang();
            return Ok(result);
        }
        [HttpGet("muasam")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMuaSam()
        {
            var result = await _duLieuDuLichService.GetAll_CoSoMuaSam();
            return Ok(result);
        }
        [HttpGet("vanchuyen")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllVanChuyen()
        {
            var result = await _duLieuDuLichService.GetAll_VanChuyen();
            return Ok(result);
        }
        [HttpGet("luhanh")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLuHanh()
        {
            var result = await _duLieuDuLichService.GetAll_LuHanh();
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("paging/langId={langId}/linhvucId={linhvucId}")]
        public async Task<IActionResult> GetAllPaging(string langId, int linhvucId, [FromQuery] RptFromRequets request)
        {
            var result = await _duLieuDuLichService.DuLieuDuLich(langId, linhvucId, request);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("loaihinh/{linhvucId}")]
        public async Task<IActionResult> GetAllLoaiHinh(int linhvucId)
        {
            var result = await _duLieuDuLichService.LoaiHinh(linhvucId);
            return Ok(result);
        }
        [HttpGet("{linhvucId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int linhvucId)
        {
            var result = await _duLieuDuLichService.GetAll(linhvucId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DuLieuDuLichModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _duLieuDuLichService.Create(Request.GetLanguageId(), request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("{hosoId}/upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage(int hosoId, [FromForm] ImageUploadRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _duLieuDuLichService.UploadImage(hosoId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("{hosoId}/uploadfile")]
        [Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFile(int hosoId, [FromForm] DocumentUploadRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _duLieuDuLichService.UploadFile(hosoId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("{hosoId}/vanban")]
        [Consumes("multipart/form-data")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadDoc(int hosoId, [FromForm] HoSoVanBanCreateRequets request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _duLieuDuLichService.UploadDoc(hosoId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _duLieuDuLichService.DuLieuDuLichById(0, id);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _duLieuDuLichService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DuLieuDuLichModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _duLieuDuLichService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("category")]
        public IActionResult GetCategory()
        {
            var result = _duLieuDuLichService.GetCategory();
            return Ok(result);
        }

        //HueCIT
        [HttpPost("{hosoId}/uploadext")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImageExt(int hosoId, [FromForm] ImageUploadExtRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _hoSoService.UploadImage(hosoId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}