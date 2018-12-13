using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yoga.Data;
using Yoga.Models;
using Yoga.Models.Interfaces;
using Yoga.Models.EventViewModels.TableViewModels;

namespace Yoga.Controllers
{
	[Authorize(Policy = "AdminsOnly")]
	public class TablesController : Controller
	{
		private readonly IPeople _people;
		private readonly ITable _tables;

		public TablesController(
			IPeople people,
			ITable tables)
		{
			_people = people;
			_tables = tables;
		}

		[HttpGet]
		public IActionResult Create(int Id)
		{

			AddTableViewModel model = new AddTableViewModel();
			Table newTable = new Table();
			newTable.EventId = Id;
			model.newTable = newTable;
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int id, [Bind("Id,newTable,Captain")] AddTableViewModel tvm)
		{
			if (ModelState.IsValid)
			{
				tvm.newTable.DateAdded = DateTime.Now;
				
				await _tables.CreateTable(tvm.newTable);
				return RedirectToAction("Details", "Event", new { id = tvm.newTable.Id });
			}
			return View(tvm);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var table = await _tables.GetTable(id);
			return View(table);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Table,Captain")] TableViewModel tvm)
		{
			if (ModelState.IsValid)
			{
				await _tables.UpdateTable(tvm.Table);

				var tables = await _tables.GetTables();
	
				
				return RedirectToAction("Details", "Tables", new { id = tvm.Table.Id });
			}
			return View(tvm);
		}

	}
}