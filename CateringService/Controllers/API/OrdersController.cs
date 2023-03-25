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
	private readonly IOrderRepository _orderRepository;

	public OrdersController(IOrderService orderService, IOrderRepository orderRepository)
	{
		_orderService = orderService;
		_orderRepository = orderRepository;
	}

	[HttpPost]
	public async Task<IActionResult> Save([FromBody] NewOrderDto orderDto)
	{
		var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var order = await _orderRepository.Get(employeeId);

		if (order == null)
		{
			await _orderService.CreateOrder(orderDto, employeeId);
		}
		else
		{
			await _orderService.EditOrder(order.Id, orderDto);
		}

		return Ok();
	}
}
