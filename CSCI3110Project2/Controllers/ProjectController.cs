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
    public class ProjectController : Controller
    {
		//Create private attributes for projects, people, roles, and projectRoles
		private IProjectRepository _projects;
		private IPersonRepository _people;
		private IRoleRepository _roles;
		private IPersonProjectRoleRepository _projectRoles;

		//Inject projects, people, roles, and projectRoles
		public ProjectController(IProjectRepository projects, IPersonRepository people, IRoleRepository roles, IPersonProjectRoleRepository projectRoles)
		{
			_projects = projects;
			_people = people;
			_roles = roles;
			_projectRoles = projectRoles;
		}

		//Display a list of all the projects
        public IActionResult Index()
        {
			var model = _projects.ReadAll()
			   .Select(p => new ProjectListVM
			   {
				   Id = p.Id,
				   Name = p.Name,
				   StartDate = p.StartDate,
				   EndDate = p.EndDate
			   });
			return View(model);
		}

		//Show create project view
		public IActionResult Create()
		{
			return View();
		}

		//Create new project
		[HttpPost]
		public IActionResult Create(CreateProjectVM projectVM)
		{
			if (ModelState.IsValid)
			{
				_projects.Create(projectVM.CreateProject());
				return RedirectToAction("Index");
			}
			return View(projectVM);
		}

		//Show details of a project
		public IActionResult Details(int id)
		{
			Project project = _projects.Read(id);
			if (project == null)
			{
				return RedirectToAction("Index");
			}
			ProjectDetailsVM pdm = new ProjectDetailsVM
			{
				Id = project.Id,
				Name = project.Name,
				StartDate = project.StartDate,
				EndDate = project.EndDate
			};

			return View(pdm);
		}

		//Show Edit page
		public IActionResult Edit(int id)
		{
			Project project = _projects.Read(id);
			if (project == null)
			{
				return RedirectToAction("Index");
			}
			UpdateProjectVM upVM = new UpdateProjectVM
			{
				Name = project.Name,
				StartDate = project.StartDate,
				EndDate = project.EndDate
			};
			return View(upVM);
		}

		//HTTPPost for Edit function 
		[HttpPost]
		public IActionResult Edit(UpdateProjectVM updateProjectVM)
		{
			if (ModelState.IsValid)
			{
				var project = updateProjectVM.CreateProject();
				_projects.Update(project.Id, project);
				return RedirectToAction("Index");
			}
			return View(updateProjectVM);
		}

		//Show person to delete
		public IActionResult Delete(int id)
		{
			var project = _projects.Read(id);
			if (project == null)
			{
				return RedirectToAction("Index");
			}
			ProjectDeleteVM pdm = new ProjectDeleteVM
			{
				Id = project.Id,
				Name = project.Name,
				StartDate = project.StartDate,
				EndDate = project.EndDate
			};
			return View(pdm);
		}

		//Confirm delete of project
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			_projects.Delete(id);
			return RedirectToAction("Index");
		}

		//Choose a person and role to assign to the project
		public IActionResult SelectPerson(string Id)
		{
			var names = _people.ReadAll()
				.Select( s => new PersonNameVM
				{
					Id = s.Id,
					Name = s.LastName + ", " + s.FirstName
				}).ToList();

			var roles = _roles.ReadAll()
				.Select(r => new RoleNameVM
				{
					Id = r.Id,
					Name = r.Name
				}).ToList();

			var model = new SelectPersonVM
			{
				ProjectId = Id,
				PersonNames = names,
				RoleNames = roles
			};
			return View(model);
		}

		//Assign the selected person and role to the project
		public IActionResult AssignToProject(int projectId, int personId, int roleId)
		{
			var project = _projects.Read(projectId);
			var person = _people.Read(personId);
			var role = _roles.Read(roleId);

			var pr = new ProjectRole
			{
				Project = project,
				Person = person,
				Role = role
			};
			_people.AssignToProject(pr);
			return RedirectToAction("Index");
		}

		//Display all Projects with people and roles grouped by project
		public IActionResult ProjectsByProject()
		{
			var projects = _projectRoles.ReadAllProjects();
			var people = _projectRoles.ReadAllPeople();
			var projectRoles = _projectRoles.ReadAllPersonRoles();

			var query = projects
			   .GroupJoin(
				  projectRoles,
				  pr => new { ProjectId = pr.Id },
				  sg => new { ProjectId = sg.ProjectId },
				  (pr, roles) => new { Project = pr, Roles = roles })
			   .Select(ppr => new ProjectGroupVM
			   {
				   ProjectName = ppr.Project.Name,
				   ProjectRoles = ppr.Roles
			   });

			var model = query.ToList();
			return View(model);
		}

		//Display all Projects with people and roles grouped by people
		public IActionResult ProjectsByPerson()
		{
			var projects = _projectRoles.ReadAllProjects();
			var people = _projectRoles.ReadAllPeople();
			var projectRoles = _projectRoles.ReadAllPersonRoles();

			var query = people
			   .GroupJoin(
				  projectRoles,
				  p => new { PersonId = p.Id},
				  sg => new { PersonId = sg.PersonId },
				  (p, roles) => new { Person = p, Roles = roles })
			   .Select(ppr => new PersonGroupVM
			   {
				   PersonName = ppr.Person.FirstName + " " + ppr.Person.LastName,
				   ProjectRoles = ppr.Roles
			   });

			var model = query.ToList();
			return View(model);
		}

		//Show confirm removal of person from a project
		public IActionResult RemoveFromProject(int personId, int projectId, int roleId)
		{
			var projectRole = _projectRoles.Read(personId, projectId, roleId);
			if (projectRole == null)
			{
				return RedirectToAction("Index");
			}
			RemoveFromProjectVM rm = new RemoveFromProjectVM
			{
				Name = projectRole.Person.FirstName +  " " + projectRole.Person.LastName,
				Project = projectRole.Project.Name,
				Role = projectRole.Role.Name,
				PersonId = projectRole.PersonId,
				ProjectId = projectRole.ProjectId,
				RoleId = projectRole.RoleId
			};
			return View(rm);
		}


		//Remove the person from the project
		[HttpPost, ActionName("RemoveFromProject")]
		public IActionResult ConfirmRemove(int personId, int projectId, int roleId)
		{
			var projectRole = _projectRoles.Read(personId, projectId, roleId);
			_projectRoles.RemoveFromProject(projectRole);
			return RedirectToAction("Index");
		}

		//Show the page to edit which role the person has
		public IActionResult EditRole(int personId, int projectId, int roleId)
		{
			var  projectRole = _projectRoles.Read(personId, projectId, roleId);
			if (projectRole == null)
			{
				return RedirectToAction("Index");
			}
			UpdateProjectRoleVM upVM = new UpdateProjectRoleVM
			{
				PersonName = projectRole.Person.FirstName + " " + projectRole.Person.LastName,
				ProjectName = projectRole.Project.Name,
				RoleName = projectRole.Role.Name,
				PersonId = projectRole.PersonId,
				ProjectId = projectRole.ProjectId,
				RoleId = projectRole.RoleId,
				RoleNames = _roles.ReadAll()
				.Select(r => new RoleNameVM
				{
					Id = r.Id,
					Name = r.Name
				}).ToList()
			};
			return View(upVM);
		}

		//Update the person's role
		[HttpPost]
		public IActionResult EditRole(UpdateProjectRoleVM upVM)
		{
			var projectRole = upVM.CreateProjectRole();
			_projectRoles.Update(projectRole);
			return RedirectToAction("Index");
		}

	}
}