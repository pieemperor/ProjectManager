using CSCI3110Project2.Models.DbContexts;
using CSCI3110Project2.Models.Entities;
using CSCI3110Project2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Services
{
    public class DbPersonProjectRoleRepository : IPersonProjectRoleRepository
    {
		private PersonProjectRoleDbContext _db;

		public DbPersonProjectRoleRepository(PersonProjectRoleDbContext db)
		{
			_db = db;
		}

		//Get all ProjectRoles from the database
		public ICollection<ProjectRole> ReadAll()
		{
			return _db.ProjectRoles.Include(p => p.Person).Include(p => p.Project).Include(p => p.Role).ToList();
		}

		//Get all people from database
		public IQueryable<Person> ReadAllPeople()
		{
			return _db.People
			   .Include(s => s.Roles);
		}

		//Read all Roles from DB
		public IQueryable<ProjectRole> ReadAllPersonRoles()
		{
			return _db.ProjectRoles
				.Include(sg => sg.Person)
				.Include(sg => sg.Project)
				.Include(sg => sg.Role);
		}

		//Read all Projects from DB
		public IQueryable<Project> ReadAllProjects()
		{
			return _db.Projects
				.Include(c => c.Roles);
		}

		//Find a specific ProjectRole in the DB
		public ProjectRole Read(int personId, int projectId, int roleId)
		{
			return _db.ProjectRoles
				.Include(sg => sg.Person)
				.Include(sg => sg.Project)
				.Include(sg => sg.Role)
				.FirstOrDefault(p => p.PersonId == personId && p.ProjectId == projectId && p.RoleId == roleId);
		}

		//Remove a person from a project
		public void RemoveFromProject(ProjectRole projectRole)
		{
			projectRole.Project.Roles.Remove(projectRole);
			_db.Entry(projectRole.Project).State = EntityState.Modified;
			projectRole.Person.Roles.Remove(projectRole);
			_db.Entry(projectRole.Person).State = EntityState.Modified;
			_db.ProjectRoles.Remove(projectRole);
			_db.SaveChanges();
		}

		//Update a person's role
		public void Update(ProjectRole projectRole)
		{
			_db.Entry(projectRole).State = EntityState.Modified;
			_db.SaveChanges();
		}
	}
}
