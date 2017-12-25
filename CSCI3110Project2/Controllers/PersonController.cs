using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSCI3110Project2.Services.Interfaces;
using CSCI3110Project2.Models.Entities;
using CSCI3110Project2.Models.ViewModels;

namespace CSCI3110Project2.Controllers
{
    public class PersonController : Controller
    {
		//create private attribute for IPersonRepository
		private IPersonRepository _people;

		//Inject people
		public PersonController(IPersonRepository people)
		{
			_people = people;
		}

		//Display list of all people
        public IActionResult Index()
        {
			var model = _people.ReadAll()
			   .Select(p => new PersonListVM
			   {
				   Id = p.Id,
				   FirstName = p.FirstName,
				   LastName = p.LastName,
				   MiddleName = p.MiddleName,
				   Email = p.Email
			   });
			return View(model);
        }

		//Show the create view
		public IActionResult Create()
		{
			return View();
		}

		//Create the perosn
		[HttpPost]
		public IActionResult Create(CreatePersonVM personVM)
		{
			if (ModelState.IsValid)
			{
				_people.Create(personVM.CreatePerson());
				return RedirectToAction("Index");
			}
			return View();
		}

		//Show the person details
		public IActionResult Details(int id)
		{
			 Person person = _people.Read(id);
			if(person == null)
			{
				return RedirectToAction("Index");
			}
			PersonDetailsVM pdm = new PersonDetailsVM
			{
				Id = person.Id,
				FirstName = person.FirstName,
				MiddleName = person.MiddleName,
				LastName = person.LastName,
				Email = person.Email
			};
			return View(pdm);
		}

		//Show the edit page
		public IActionResult Edit(int id)
		{
			Person person = _people.Read(id);
			if (person == null)
			{
				return RedirectToAction("Index");
			}
			UpdatePersonVM upVM = new UpdatePersonVM
			{
				FirstName = person.FirstName,
				MiddleName = person.MiddleName,
				LastName = person.LastName,
				Email = person.Email
			};
			return View(upVM);
		}

		//Update the person
		[HttpPost]
		public IActionResult Edit(UpdatePersonVM updatePersonVM)
		{
			if (ModelState.IsValid)
			{
				var person = updatePersonVM.CreatePerson();
				_people.Update(person.Id, person);
				return RedirectToAction("Index");
			}
			return View(updatePersonVM);
		}

		//Show person to delete
		public IActionResult Delete(int id)
		{
			var person = _people.Read(id);
			if(person == null)
			{
				return RedirectToAction("Index");
			}
			PersonDeleteVM pdm = new PersonDeleteVM
			{
				Id = person.Id,
				FirstName = person.FirstName,
				MiddleName = person.MiddleName,
				LastName = person.LastName,
				Email = person.Email
			};
			return View(pdm);
		}

		//Confirm delete of person
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			_people.Delete(id);
			return RedirectToAction("Index");
		}

    }
}