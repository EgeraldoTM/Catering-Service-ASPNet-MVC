using AutoMapper;
using CateringService.Core.IRepositories;
using CateringService.Web.Controllers;
using Core.IRepositories;
using Core.Models;
using Core;
using Moq;
using CateringService.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CateringService.Tests;

[TestFixture]
public class FoodItemsControllerTests
{
	private Mock<IRepository<Category>> _categoryRepository;
	private Mock<IFoodItemRepository> _foodItemRepository;
	private Mock<IUnitOfWork> _unitOfWork;
	private Mock<IMapper> _mapper;
	private FoodItemsController _controller;

	[SetUp]
	public void SetUp()
	{
		_categoryRepository = new Mock<IRepository<Category>>();
		_foodItemRepository = new Mock<IFoodItemRepository>();
		_unitOfWork = new Mock<IUnitOfWork>();
		_mapper = new Mock<IMapper>();
		_controller = new FoodItemsController(
			_categoryRepository.Object,
			_foodItemRepository.Object,
			_unitOfWork.Object,
			_mapper.Object);
	}

	[Test]
	public async Task HttpPostCreate_ModelStateIsInValid_ReturnViewResult()
	{
		var foodItemDto = new FoodItemDto { Id = 1, Name = "a", CategoryId = 1, Description = "b", Price = 10};
		_controller.ModelState.AddModelError("Name", "error msg");

		var result = await _controller.Create(foodItemDto);

		_unitOfWork.Verify(uow => uow.CompleteAsync(), Times.Never());
		Assert.That(result, Is.TypeOf<ViewResult>());
	}

	[Test]
	public async Task HttpPostCreate_ModelStateIsValid_ReturnRedirectToActionResult()
	{
		var foodItemDto = new FoodItemDto { Id = 1, Name = "a", CategoryId = 1, Description = "b", Price = 10};

		var result = await  _controller.Create(foodItemDto);

		_foodItemRepository.Verify(fr => fr.Add(It.IsAny<FoodItem>()));
		_unitOfWork.Verify(uow => uow.CompleteAsync());
		Assert.That(result, Is.TypeOf<RedirectToActionResult>());
	}

	[Test]
	public async Task HttpPostEdit_IdIsDifferentFromDtoId_ReturnNotFound()
	{
		var foodItemDto = new FoodItemDto { Id = 1, Name = "a", CategoryId = 1, Description = "b", Price = 10 };

		var result = await _controller.Edit(2, foodItemDto);

		Assert.That(result, Is.TypeOf<NotFoundResult>());
	}

	[Test]
	public async Task HttpPostEdit_ModelStateIsInvalid_ReturnViewResult()
	{
		var foodItemDto = new FoodItemDto { Id = 1, Name = "a", CategoryId = 1, Description = "b", Price = 10 };
		_controller.ModelState.AddModelError("Name", "error msg");

		var result = await _controller.Edit(1, foodItemDto);

		Assert.That(result, Is.TypeOf<ViewResult>());
	}
	
	[Test]
	public async Task HttpPostEdit_ModelStateIsValidButNoFoodItemInDB_ReturnViewResult()
	{
		var foodItemDto = new FoodItemDto { Id = 1, Name = "a", CategoryId = 1, Description = "b", Price = 10 };
		
		var result = await _controller.Edit(1, foodItemDto);
		_foodItemRepository.Setup(fr => fr.Get(foodItemDto.Id)).Returns(() => null);

		Assert.That(result, Is.TypeOf<NotFoundResult>());
	}

	[Test]
	public async Task HttpPostEdit_ModelStateIsValid_ReturnRedirectToActionResult()
	{
		var foodItemDto = new FoodItemDto { Id = 1, Name = "a", CategoryId = 1, Description = "b", Price = 10 };
		
		_foodItemRepository.Setup(fr => fr.Get(foodItemDto.Id).Result).Returns(new FoodItem { Id = foodItemDto.Id});
		var result = await _controller.Edit(1, foodItemDto);

		_foodItemRepository.Verify(fr => fr.Get(foodItemDto.Id));
		_unitOfWork.Verify(uow => uow.CompleteAsync());
		Assert.That(result, Is.TypeOf<RedirectToActionResult>());
	}
}
