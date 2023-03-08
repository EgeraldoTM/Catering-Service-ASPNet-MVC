﻿using System.Security.Claims;
using CateringService.Core.DTOs;
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
	private readonly IOrderRepository _orderRepository;
	private readonly IUnitOfWork _unitOfWork;
    public OrdersController(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
		_unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] OrderDto orderDto)
	{
		var employeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var orderInDb = await _orderRepository.GetWithDetails(employeeId);

		if (orderInDb == null)
		{
			var order = new Order
			{
				EmployeeId = employeeId,
				OrderDetails = new List<OrderDetail>()
			};

			order.OrderDetails = orderDto.Details.Select(d => new OrderDetail { FoodItemId = d.FoodItemId, Quantity = d.Quantity, Price = d.Price }).ToList();

			_orderRepository.Add(order);
		}
		else
		{
			var foodIds = orderInDb.OrderDetails.Select(d => d.FoodItemId).ToList();
			var newDetails = orderDto.Details.Where(d => !foodIds.Contains(d.FoodItemId)).Select(d => new OrderDetail { FoodItemId = d.FoodItemId, Quantity = d.Quantity, Price = d.Price });
			var details = orderDto.Details.Where(d => foodIds.Contains(d.FoodItemId)).ToDictionary(d => d.FoodItemId, d => d.Quantity);

			orderInDb.OrderDetails.AddRange(newDetails);

			foreach (var orderDetail in orderInDb.OrderDetails)
			{
				if (details.ContainsKey(orderDetail.FoodItemId))
					orderDetail.Quantity += details[orderDetail.FoodItemId];
			}
		}
		await _unitOfWork.CompleteAsync();

		return Ok();
	}
}
