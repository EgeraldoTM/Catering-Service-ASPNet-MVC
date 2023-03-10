using System.Security.Claims;
using CateringService.Core.IRepositories;
using CateringService.Web.ViewModels;
using Core;
using Core.IRepositories;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CateringService.Web.Controllers;

//[Authorize(Roles = RoleName.Employee)]
public class OrdersController : Controller
{
	private readonly IOrderRepository _orderRepository;
	private readonly IRepository<OrderDetail> _detailRepository;
    private readonly IUnitOfWork _unitOfWork;
    public OrdersController(IOrderRepository orderRepository, IRepository<OrderDetail> detailRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
		_detailRepository = detailRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<IActionResult> Index(DateTime? date)
	{
		var emplyeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		var filter = date == null ? DateTime.Now.Date : date.Value;

		var order = await _orderRepository.Get(filter, emplyeeId);

		var viewModel = new OrderVM
		{
			Order = order
		};

		return View(viewModel);
	}

	
	public async Task<IActionResult> Edit()
	{
		var emplyeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var order = await _orderRepository.Get(null, emplyeeId);

		return View(order);
	}

	public async Task<IActionResult> SubtractQuantity(int id)
	{
		var orderDetail = await _detailRepository.Get(id);

		if (orderDetail != null)
		{
			orderDetail.Quantity--;
		}

		await _unitOfWork.CompleteAsync();

		return RedirectToAction(nameof(Edit));
	}

	public async Task<IActionResult> AddQuantity(int id)
	{
		var orderDetail = await _detailRepository.Get(id);

		if (orderDetail != null)
		{
			orderDetail.Quantity++;
		}

		await _unitOfWork.CompleteAsync();

		return RedirectToAction(nameof(Edit));
	}

	public async Task<IActionResult> RemoveItem(int id)
	{
		var orderDetail = await _detailRepository.Get(id);

		if(orderDetail != null) 
			_detailRepository.Delete(orderDetail);

		await _unitOfWork.CompleteAsync();

		return RedirectToAction(nameof(Edit));
	}

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
