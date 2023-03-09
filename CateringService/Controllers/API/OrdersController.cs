using System.Security.Claims;
using CateringService.Core.DTOs;
using CateringService.Core.Services;
using Core;
using Core.IRepositories;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NuGet.Packaging;

namespace CateringService.Web.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
	private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService)
    {
		_orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] OrderDto orderDto)
	{
		var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		await _orderService.Save(orderDto, employeeId);

		return Ok();
	}
}
