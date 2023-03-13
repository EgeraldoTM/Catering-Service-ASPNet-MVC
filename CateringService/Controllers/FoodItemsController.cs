using AutoMapper;
using CateringService.Core;
using CateringService.Core.DTOs;
using CateringService.Core.IRepositories;
using CateringService.Web.ViewModels;
using Core;
using Core.IRepositories;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CateringService.Web.Controllers;

public class FoodItemsController : Controller
{
	private readonly IRepository<Category> _categoryRepository;
	private readonly IFoodItemRepository _foodItemRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
    public FoodItemsController(IRepository<Category> categoryRepository, IFoodItemRepository foodItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
		_categoryRepository = categoryRepository;
		_foodItemRepository = foodItemRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
    }
    public IActionResult Index()
	{
		//if (User.IsInRole(RoleName.Cook))
		//	return View();

		//return View("ReadOnlyIndex");
		return View();
	}

	[Authorize(Roles = RoleName.Cook)]
	public async Task<IActionResult> Create()
	{
		var categories = await _categoryRepository.GetAll();

		var viewModel = new ItemWithCategoriesVM
		{
			FoodItemDto = new FoodItemDto(),
			CategoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories)
		};

		return View(viewModel);
	}

	[Authorize(Roles = RoleName.Cook)]
	[HttpPost]
	public async Task<IActionResult> Create(FoodItemDto foodItemDto)
	{
		if (ModelState.IsValid)
		{
			var foodItem = _mapper.Map<FoodItemDto, FoodItem>(foodItemDto);
			_foodItemRepository.Add(foodItem);

			await _unitOfWork.CompleteAsync();

			return RedirectToAction(nameof(Index));
		}

		var categories = await _categoryRepository.GetAll();

		var viewModel = new ItemWithCategoriesVM
		{
			FoodItemDto = foodItemDto,
			CategoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories)
		};

		return View(viewModel);
	}

	[Authorize(Roles = RoleName.Cook)]
	public async Task<IActionResult> Edit(int id)
	{
		var foodItem = await _foodItemRepository.Get(id);

		if (foodItem == null)
			return NotFound();

		var categories = await _categoryRepository.GetAll();

		var viewModel = new ItemWithCategoriesVM
		{
			FoodItemDto = _mapper.Map<FoodItem, FoodItemDto>(foodItem),
			CategoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories)
		};

		return View(viewModel);
	}

	[Authorize(Roles = RoleName.Cook)]
	[HttpPost]
	public async Task<IActionResult> Edit(int id, FoodItemDto foodItemDto)
	{
		if (id != foodItemDto.Id)
			return NotFound();

		if (ModelState.IsValid)
		{
			var foodItemInDb = await _foodItemRepository.Get(id);

			if (foodItemInDb == null)
				return NotFound();

			_mapper.Map(foodItemDto, foodItemInDb);

			await _unitOfWork.CompleteAsync();

			return RedirectToAction(nameof(Index));
		}

		var categories = await _categoryRepository.GetAll();

		var viewModel = new ItemWithCategoriesVM
		{
			FoodItemDto = foodItemDto,
			CategoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories)
		};

		return View(viewModel);
	}
}
