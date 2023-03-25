using CateringService.Core.DTOs;
using Core;
using Core.IRepositories;
using Core.Models;
using NuGet.Packaging;

namespace CateringService.Core.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;
	private readonly IUnitOfWork _unitOfWork;
	public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
	{
		_orderRepository = orderRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task Create(NewOrderDto orderDto, string employeeId)
	{
		var order = new Order
		{
			EmployeeId = employeeId,
			OrderDetails = new List<OrderDetail>()
		};

		order.OrderDetails = orderDto.Details
			.Select(d => new OrderDetail
			{
				FoodItemId = d.FoodItemId,
				Quantity = d.Quantity,
				Price = d.Price
			}).ToList();

		_orderRepository.Add(order);

		await _unitOfWork.CompleteAsync();
	}

	public async Task Edit(int id, NewOrderDto orderDto)
	{
		var orderInDb = await _orderRepository.Get(id);

		var foodIds = orderInDb!.OrderDetails.Select(d => d.FoodItemId).ToList();
		var newDetails = orderDto.Details
			.Where(d => !foodIds.Contains(d.FoodItemId))
			.Select(d => new OrderDetail
			{
				FoodItemId = d.FoodItemId,
				Quantity = d.Quantity,
				Price = d.Price
			});

		var details = orderDto.Details
			.Where(d => foodIds.Contains(d.FoodItemId))
			.ToDictionary(d => d.FoodItemId, d => d.Quantity);

		orderInDb.OrderDetails.AddRange(newDetails);

		foreach (var orderDetail in orderInDb.OrderDetails)
		{
			if (details.ContainsKey(orderDetail.FoodItemId))
				orderDetail.Quantity += details[orderDetail.FoodItemId];
		}

		await _unitOfWork.CompleteAsync();
	}
}
