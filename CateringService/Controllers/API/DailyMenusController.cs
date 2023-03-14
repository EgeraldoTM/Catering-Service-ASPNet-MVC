using CateringService.Core;
using System.Data;
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
[Authorize(Roles = RoleName.Cook)]
public class DailyMenusController : ControllerBase
{
	private readonly IFoodItemRepository _foodItemRepository;
	private readonly IMenuRepository _menuRepository;
	private readonly IUnitOfWork _unitOfWork;
	public DailyMenusController(IFoodItemRepository foodItemRepository, IMenuRepository menuRepository, IUnitOfWork unitOfWork)
	{
		_foodItemRepository = foodItemRepository;
		_menuRepository = menuRepository;
		_unitOfWork = unitOfWork;
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromForm] NewMenuDto menuDto)
	{
		if (menuDto.Date.Date < DateTime.Now.Date)
			return BadRequest("Cannot create a Menu for a day in the past");

		var menu = new Menu
		{
			Date = menuDto.Date,
		};

		if (menuDto.FoodIds != null)
		{
			var foodItems = await _foodItemRepository.Find(f => menuDto.FoodIds.Contains(f.Id));
			menu.FoodItems = foodItems.ToList();
		}

		_menuRepository.Add(menu);

		await _unitOfWork.CompleteAsync();

		return Ok();
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Edit(int id, [FromForm] NewMenuDto menuDto)
	{
		var menuInDb = await _menuRepository.Get(id);

		if (menuInDb == null)
			return NotFound();

		menuInDb.Date = menuDto.Date;

		if (menuDto.FoodIds != null && menuDto.FoodIds.Any())
		{
			var foodItems = await _foodItemRepository.Find(f => menuDto.FoodIds.Contains(f.Id));

			if (menuInDb.FoodItems == null || !menuInDb.FoodItems.Any())
			{
				menuInDb.FoodItems = foodItems.ToList();
			}
			else
			{
				foreach (var item in foodItems)
				{
					if (!menuInDb.FoodItems.Contains(item))
						menuInDb.FoodItems.Add(item);
				}
			}
		}
		await _unitOfWork.CompleteAsync();

		return Ok();
	}
}
