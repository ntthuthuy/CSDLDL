using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLife.Common;
using TechLife.Model;
using TechLife.Model.ChuyenDi;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
  
    [ApiController]
    [Route("v1.0/[controller]")]
    public class ChuyenDiController : ControllerBase
    {
        private readonly IChuyenDiService _chuyenDiService;
        public ChuyenDiController(IChuyenDiService chuyenDiService)
        {
            _chuyenDiService = chuyenDiService;
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(string ma)
        {
            var result = await _chuyenDiService.GetAll(ma);
            return Ok(result);
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Create(ChuyenDiCreateRequest request)
        {
            var result = await _chuyenDiService.Create(request);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _chuyenDiService.Delete(id);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _chuyenDiService.GetListHanhTrinh(id);
            return Ok(result);
        }
        [HttpGet("{id}/ngayhanhtrinh")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHanhTrinh(int id)
        {
            var result = await _chuyenDiService.GetListNgayHanhTrinh(id);
            return Ok(result);
        }
        [HttpGet("{id}/ngay={day}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, int day)
        {
            var result = await _chuyenDiService.GetHanhTrinhTheoNgay(id, day);
            return Ok(result);
        }
        [HttpPost("{id}/themdiadiem")]
        [AllowAnonymous]
        public async Task<IActionResult> AddItem(int id, HanhTrinhChuyenDiUpdateRequest requets)
        {
            var result = await _chuyenDiService.AddItem(id, requets);
            return Ok(result);
        }
        [HttpPut("suadiadiem")]
        [AllowAnonymous]
        public async Task<IActionResult> EditItem(List<HanhTrinhChuyenDiUpdateRequest> requets)
        {
            var result = await _chuyenDiService.EditItem(requets);
            return Ok(result);
        }
        [HttpDelete("xoadiadiem/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DelItem(int id)
        {
            var result = await _chuyenDiService.DeleteItem(id);
            return Ok(result);
        }
        [HttpDelete("xoangay")]
        [AllowAnonymous]
        public async Task<IActionResult> DelDate(int chuyendiId, int ngay)
        {
            var result = await _chuyenDiService.DeleteDate(chuyendiId, ngay);
            return Ok(result);
        }
    }
}