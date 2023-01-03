using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLife.Model.Order;
using TechLife.Service;

namespace TechLife.Api.Controllers
{
    [ApiController]
    [Route("v1.0/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(string ma, string loai)
        {
            var result = await _orderService.GetAll(ma, loai);
            return Ok(result);
        }
        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Create(OrderCreateRequest request)
        {
            var result = await _orderService.Create(request);
            return Ok(result);
        }
        [HttpDelete()]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderService.Delete(id);
            
            return Ok(result);
        }
    }
}