using AutoMapper;
using CateringService.Core.DTOs;
using Core.Models;

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
	}
}
