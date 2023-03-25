using AutoMapper;
using CateringService.Core.DTOs;
using Core.Models;
using NuGet.Packaging;

namespace CateringService.Infrastructure;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		// Domain to Dto
		CreateMap<FoodItem, FoodItemDto>();
		CreateMap<Category, CategoryDto>();
		CreateMap<Menu, MenuDto>();
		CreateMap<Order, OrderDto>();
		CreateMap<OrderDetail, OrderDetailDto>();

		//Dto to Domain
		CreateMap<FoodItemDto, FoodItem>();
		CreateMap<NewOrderDetailDto, OrderDetail>();
		CreateMap<NewOrderDto, Order>()
			.AfterMap((dto, o) =>
			{
				if (o.OrderDetails == null || !o.OrderDetails.Any())
				{
					o.OrderDetails = dto.Details.Select(
					d => new OrderDetail
					{
						FoodItemId = d.FoodItemId,
						Quantity = d.Quantity,
						Price = d.Price
					}).ToList();
				}
				else
				{
					var foodIds = o.OrderDetails.Select(d => d.FoodItemId).ToList();
					var newDetails = dto.Details
						.Where(d => !foodIds.Contains(d.FoodItemId))
						.Select(d => new OrderDetail
						{
							FoodItemId = d.FoodItemId,
							Quantity = d.Quantity,
							Price = d.Price
						});
					o.OrderDetails.AddRange(newDetails);

					var details = dto.Details
						.Where(d => foodIds.Contains(d.FoodItemId))
						.ToDictionary(d => d.FoodItemId, d => d.Quantity);


					foreach (var orderDetail in o.OrderDetails)
					{
						if (details.ContainsKey(orderDetail.FoodItemId))
							orderDetail.Quantity += details[orderDetail.FoodItemId];
					}

				}
			});
	}
}
