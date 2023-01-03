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

    public class TinTucController : ControllerBase
    {
        private readonly ITinTucService _tinTucService;
        public TinTucController(ITinTucService tinTucService)
        {
            _tinTucService = tinTucService;
        }

        [HttpGet("lang={languageId}/category={categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(string languageId, int categoryId)
        {
            var result = await _tinTucService.GetAll(languageId, categoryId);
            return Ok(result);
        }

        [HttpGet("paging/lang={languageId}")]
        public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery] TinTucPagingRequest request)
        {
            var result = await _tinTucService.GetPaging(languageId, request);
            return Ok(result);
        }

        [HttpGet("festival/lang={languageId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFestiavals(string languageId, [FromQuery] TinTucPagingRequest request)
        {
            var result = await _tinTucService.GetFestivals(languageId, request);
            return Ok(result);
        }

        [HttpGet("relating/{id}/category={categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRelated(int id, int categoryId)
        {
            var result = await _tinTucService.GetRelating(id, categoryId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _tinTucService.GetById(id);
            return Ok(user);
        }
    }
}
