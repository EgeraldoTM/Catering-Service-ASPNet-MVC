using System.Data;
using AutoMapper;
using CateringService.Core;
using CateringService.Core.DTOs;
using Core;
using Core.IRepositories;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CateringService.Web.Controllers.API;

[Route("api/[controller]")]
[ApiController]
public class FoodItemsController : ControllerBase
{
	private readonly IFoodItemRepository _foodItemRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
    public FoodItemsController(IFoodItemRepository foodItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _foodItemRepository = foodItemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IActionResult> Get(string? query = null)
    {
        var foodItems = await _foodItemRepository.GetWithCategory(query);
        var foodItemDtos = _mapper.Map<IEnumerable<FoodItem>, IEnumerable<FoodItemDto>>(foodItems);

        return Ok(foodItemDtos);
	}

	//[Authorize(Roles = RoleName.Cook)]
	[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
		var foodItem = await _foodItemRepository.Get(id);

		if (foodItem == null)
			return NotFound();

		foodItem.IsDeleted = true;

		await _unitOfWork.CompleteAsync();

		return NoContent();
	}
}
