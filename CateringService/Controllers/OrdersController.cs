using System.Security.Claims;
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

[Authorize(Roles = RoleName.Employee)]
public class OrdersController : Controller
{
	private readonly IOrderRepository _orderRepository;
	private readonly IRepository<OrderDetail> _detailRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	public OrdersController(IOrderRepository orderRepository, IRepository<OrderDetail> detailRepository, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_orderRepository = orderRepository;
		_detailRepository = detailRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}
	public async Task<IActionResult> Index(DateTime? date)
	{
		var emplyeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		DateTime? filter = null;

		if (date != null)
			filter = date.Value;

		var order = await _orderRepository.Get(emplyeeId, filter);

		var viewModel = new OrderVM
		{
			OrderDto = order != null ? _mapper.Map<Order, OrderDto>(order) : null
		};

		return View(viewModel);
	}

	public async Task<IActionResult> Edit()
	{
		var emplyeeId = User.FindFirstValue(ClaimTypes.NameIdentifier);

		var order = await _orderRepository.Get(emplyeeId);

		var orderDto = order != null ? _mapper.Map<Order, OrderDto>(order) : null;

		return View(orderDto);
	}

	public async Task<IActionResult> SubtractQuantity(int id)
	{
		var orderDetail = await _detailRepository.Get(id);

		if (orderDetail != null && orderDetail.Quantity > 1)
		{
			orderDetail.Quantity--;
			await _unitOfWork.CompleteAsync();
		}

		return RedirectToAction(nameof(Edit));
	}

	public async Task<IActionResult> AddQuantity(int id)
	{
		var orderDetail = await _detailRepository.Get(id);

		if (orderDetail != null)
		{
			orderDetail.Quantity++;
			await _unitOfWork.CompleteAsync();
		}

		return RedirectToAction(nameof(Edit));
	}

	public async Task<IActionResult> RemoveItem(int id)
	{
		var orderDetail = await _detailRepository.Get(id);

		if (orderDetail != null)
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
