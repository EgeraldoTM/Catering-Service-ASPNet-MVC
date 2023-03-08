using System.Security.Claims;
using CateringService.Web.ViewModels;
using Core;
using Core.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CateringService.Web.Controllers;

//[Authorize(Roles = RoleName.Employee)]
public class OrdersController : Controller
{
	private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    public OrdersController(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<IActionResult> Index(DateTime? date)
	{
		var emplyeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		var filter = date == null ? DateTime.Now.Date : date.Value;

		var order = await _orderRepository.Get(filter, emplyeeId);

		if (order == null)
			return NotFound();

		var viewModel = new OrderVM
		{
			Order = order
		};

		return View(viewModel);
	}

	
	public async Task<IActionResult> Edit(int id)
	{
		var emplyeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var order = await _orderRepository.Get(null, emplyeeId);

		return View(order);
	}

	[HttpDelete]
	public async Task<IActionResult> Delete(int id)
	{
		var order = await _orderRepository.Get(id);

		if (order == null)
			return NotFound();

		order.IsDeleted = true;

		await _unitOfWork.CompleteAsync();

		return RedirectToAction(nameof(Index));
	}
}
