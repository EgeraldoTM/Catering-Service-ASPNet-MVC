using CateringService.Core;
using CateringService.Web.ViewModels;
using Core;
using Core.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CateringService.Web.Controllers;

public class MenusController : Controller
{
    private readonly IFoodItemRepository _foodItemRepository;
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;
    public MenusController(IFoodItemRepository foodItemRepository, IMenuRepository menuRepository, IUnitOfWork unitOfWork)
    {
        _foodItemRepository = foodItemRepository;
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var menu = await _menuRepository.GetForToday();

        if (menu == null)
            return View("Empty");

        var categories = menu.FoodItems.Select(x => x.Category!.Name).Distinct().ToList();

        var viewModel = new MenuVM
        {
            Menu = menu,
            Categories = categories
        };

        if (User.IsInRole(RoleName.Cook))
            return View("Cook", viewModel);

        return View("Employee", viewModel);
    }

    [Authorize(Roles = RoleName.Cook)]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = RoleName.Cook)]
    public async Task<IActionResult> Edit(int id)
    {
        var menu = await _menuRepository.Get(id);

        if (menu == null)
            return NotFound("Menu not found");

        return View(menu);
    }

    [Authorize(Roles = RoleName.Cook)]
    [HttpDelete]
    public async Task<IActionResult> DeleteItem(int id, int foodItemId)
    {
        var menu = await _menuRepository.Get(id);

        if (menu == null)
            return NotFound("Menu not found");

        var item = await _foodItemRepository.Get(foodItemId);

        if (item == null)
            return BadRequest("Invalid food item Id");

        menu.FoodItems.Remove(item);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [Authorize(Roles = RoleName.Cook)]
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var menu = await _menuRepository.Get(id);

        if (menu == null)
            return NotFound("Menu not found");

        menu.IsDeleted = true;

        await _unitOfWork.CompleteAsync();

        return RedirectToAction(nameof(Index));
    }
}
