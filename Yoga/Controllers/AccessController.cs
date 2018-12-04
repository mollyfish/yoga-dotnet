using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Data;
using Yoga.Models;
using Yoga.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class AccessController : Controller
	{

		private readonly IVIP _vip;
		private readonly YogaUsersDbContext _userContext;
		private readonly UserManager<User> _userManager;

		public AccessController(IVIP vip, YogaUsersDbContext userContext, UserManager<User> userManager)
		{
			_vip = vip;
			_userContext = userContext;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var vips = await _vip.GetVIPs();
			return View(vips);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int Id, [Bind("Id,FirstName,LastName,Email")] VIP vip)
		{
			CheckUserRolesExist();
			if (ModelState.IsValid)
			{
				var user = _userContext.Users
					.Where(m => m.Email == vip.Email)
					.Select(m => m)
					.SingleOrDefault();
				if (user.Id == null)
				{
					await _vip.CreateVIP(vip);
					return RedirectToAction(nameof(Index));
				}
				else
				{
					await _vip.CreateVIP(vip);
					await _userManager.AddToRoleAsync(user, UserRole.Admin);
					return RedirectToAction(nameof(Index));
				}
			}
			return View(vip);
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int Id)
		{
			var vip = await _vip.GetVIP(Id);
			return View(vip);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int Id, [Bind("Id,FirstName,LastName,Email")] VIP vip)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _vip.UpdateVIP(vip);
				}
				catch (Exception e)
				{

					throw;
				}
				return RedirectToAction(nameof(Index));
			}
			return View(vip);
		}

		public void CheckUserRolesExist()
		{
			if (!_userContext.Roles.Any())
			{
				List<IdentityRole> Roles = new List<IdentityRole>
				{
					new IdentityRole {Name = UserRole.Admin, NormalizedName = UserRole.Admin.ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() },
					new IdentityRole {Name = UserRole.Instructor, NormalizedName = UserRole.Instructor.ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() },
				};

				foreach (var role in Roles)
				{
					_userContext.Roles.Add(role);
					_userContext.SaveChanges();
				}
			}
		}
	}

}