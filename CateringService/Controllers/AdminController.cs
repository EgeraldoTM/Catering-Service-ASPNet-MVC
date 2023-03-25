using CateringService.Core;
using CateringService.Web.ViewModels.Admin;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CateringService.Web.Controllers
{
	public class AdminController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
        }

        public async Task<IActionResult> Index(string? filter, string? roleFilter)
		{
			var usersInDb = _userManager.Users.Where(u => u.FirstName != RoleName.Admin);

			if (!string.IsNullOrWhiteSpace(filter))
			{
				usersInDb = usersInDb
					.Where(u => u.FirstName.Contains(filter) ||
					u.LastName.Contains(filter) ||
					u.Email.Contains(filter) ||
					u.PhoneNumber.Contains(filter));
			}

			var users = await usersInDb.ToListAsync();

			var viewModel = new IndexVM
			{
				Roles = new SelectList(await _roleManager.Roles.ToListAsync()),
				Users = new List<AdminVM>()
			};

			foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				var role = roles.SingleOrDefault();

				var userWithRole = new AdminVM
				{
					User = user,
					Role = role
				};

				if (!string.IsNullOrWhiteSpace(roleFilter))
				{
					if (!await _userManager.IsInRoleAsync(user, roleFilter))
						continue;
				}

				viewModel.Users.Add(userWithRole);
			}

			return View(viewModel);
		}

		public async Task<IActionResult> Details(string id)
		{
			var user = await _userManager.FindByIdAsync(id);

			if (user == null)
				return NotFound("User not found");

			var roles = await _userManager.GetRolesAsync(user);
			var role = roles.SingleOrDefault();

			var viewModel = new AdminVM
			{
				User = user,
				Role = role ?? "No Role"
			};

			return View(viewModel);
		}

		public async Task<IActionResult> Create()
		{
            var viewModel = new CreateVM
            {
                Roles = new SelectList(await _roleManager.Roles.ToListAsync())
            };

            return View(viewModel);
        }

		[HttpPost]
		public async Task<IActionResult> Create(CreateVM input)
		{
			if (ModelState.IsValid)
			{
				var role = _roleManager.FindByNameAsync(input.Role).Result;
				var user = new ApplicationUser
				{
					UserName = input.Email,
					FirstName = input.FirstName,
					LastName = input.LastName,
					IdentificationNumber = input.ID,
					Email = input.Email,
					BirthDay = input.Birthday,
					PhoneNumber = input.PhoneNumber,
					RegistrationDate = DateTime.Now
				};

				var result = await _userManager.CreateAsync(user, input.Password);

				await _userManager.AddToRoleAsync(user, role.Name);

				if (result.Succeeded)
					return RedirectToAction(nameof(Index));

				return BadRequest(result.Errors);
			}

			return View(input);
		}
	}
}
