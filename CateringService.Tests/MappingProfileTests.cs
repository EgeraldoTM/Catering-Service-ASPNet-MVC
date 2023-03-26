using AutoMapper;
using CateringService.Core.DTOs;
using CateringService.Infrastructure;
using Core.Models;

namespace CateringService.Tests;

[TestFixture]
public class MappingProfileTests
{
	private IMapper _mapper;
	private NewOrderDto _orderDto;

	[SetUp]
	public void Setup()
	{
		var config = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(typeof(MappingProfile));
		});
		_mapper = config.CreateMapper();

		_orderDto = new NewOrderDto
		{
			Details = new List<NewOrderDetailDto>
			{
				new NewOrderDetailDto{ FoodItemId = 1, Quantity = 1, Price = 1},
				new NewOrderDetailDto{ FoodItemId = 2, Quantity = 1, Price = 1},
				new NewOrderDetailDto{ FoodItemId = 3, Quantity = 1, Price = 1}
			}
		};
	}

	[Test]
	public void NewOrderDtoToOrder_NoOrderOrEmptyOrderDetailsInDB_CreateNewOrderWithDtoDetails()
	{

		var destination = _mapper.Map<NewOrderDto, Order>(_orderDto);

		var sourceDetails = _orderDto.Details.Select(d => d.FoodItemId).ToList();
		var destinationDetails = destination.OrderDetails.Select(d => d.FoodItemId).ToList();

		Assert.That(destination.OrderDetails.Count, Is.EqualTo(_orderDto.Details.Count()));
		Assert.That(destinationDetails, Is.EqualTo(sourceDetails));
	}

	[Test]
	public void NewOrderDtoToOrder_OrderHasDetails_AddNewDetailsAndUpdateQuantityInExistingDetails()
	{
		var destination = new Order
		{
			OrderDetails = new List<OrderDetail> {
			new OrderDetail { FoodItemId = 1, Quantity = 1, Price = 1 } }
		};
		_mapper.Map(_orderDto, destination);

		var overlappingDetail = destination.OrderDetails.First(od => od.FoodItemId == 1);

		Assert.That(overlappingDetail.Quantity, Is.EqualTo(2));
		Assert.That(destination.OrderDetails.Count, Is.EqualTo(_orderDto.Details.Count()));
	}
}