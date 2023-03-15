using AutoMapper;
using CateringService.Core.DTOs;
using Core.Models;

namespace CateringService.Infrastructure;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<FoodItem, FoodItemDto>();
		CreateMap<Category, CategoryDto>();
		CreateMap<Menu, MenuDto>();
		CreateMap<Order, OrderDto>();
		CreateMap<OrderDetail, OrderDetailDto>();

		CreateMap<FoodItemDto, FoodItem>();
	}
}
