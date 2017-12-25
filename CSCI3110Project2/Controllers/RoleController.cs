using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSCI3110Project2.Services.Interfaces;
using CSCI3110Project2.Models.ViewModels;
using CSCI3110Project2.Models.Entities;

namespace CSCI3110Project2.Controllers
{
    public class RoleController : Controller
    {
		//Create private attributes for roles
		private IRoleRepository _roles;

		//Inject roles
		public RoleController(IRoleRepository roles)
		{
			_roles = roles;
		}

		//Display a list of all the roles
		public IActionResult Index()
        {
			var model = _roles.ReadAll()
			   .Select(r => new RoleListVM
			   {
				   Id = r.Id,
				   Name = r.Name,
				   Description = r.Description
			   });
			return View(model);
		}

		//Show create role view
		public IActionResult Create()
		{
			return View();
		}

		//Create new role
		[HttpPost]
		public IActionResult Create(CreateRoleVM roleVM)
		{
			if (ModelState.IsValid)
			{
				_roles.Create(roleVM.CreateRole());
				return RedirectToAction("Index");
			}
			return View(roleVM);
		}

		//Show details of a role
		public IActionResult Details(int id)
		{
			Role role = _roles.Read(id);
			if (role == null)
			{
				return RedirectToAction("Index");
			}
			RoleDetailsVM rdm = new RoleDetailsVM
			{
				Id = role.Id,
				Name = role.Name,
				Description = role.Description
			};
			return View(rdm);
		}

		//Show Edit page
		public IActionResult Edit(int id)
		{
			Role role = _roles.Read(id);
			if (role == null)
			{
				return RedirectToAction("Index");
			}
			UpdateRoleVM urVM = new UpdateRoleVM
			{
				Name = role.Name,
				Description = role.Description
			};
			return View(urVM);
		}

		//HTTPPost for Edit function - updates role
		[HttpPost]
		public IActionResult Edit(UpdateRoleVM updateRoleVM)
		{
			if (ModelState.IsValid)
			{
				var role = updateRoleVM.CreateRole();
				_roles.Update(role.Id, role);
				return RedirectToAction("Index");
			}
			return View(updateRoleVM);
		}

		//Show role to delete
		public IActionResult Delete(int id)
		{
			var role = _roles.Read(id);
			if (role == null)
			{
				return RedirectToAction("Index");
			}
			RoleDeleteVM rdm = new RoleDeleteVM
			{
				Id = role.Id,
				Name = role.Name,
				Description = role.Description
			};
			return View(rdm);
		}

		//Confirm delete of role
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			_roles.Delete(id);
			return RedirectToAction("Index");
		}


	}
}