using CSCI3110Project2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSCI3110Project2.Models.Entities;
using CSCI3110Project2.Models.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110Project2.Services
{
	public class DbProjectRepository : IProjectRepository
	{
		//CReate private variable to store DbContext
		private PersonProjectRoleDbContext _db;

		//Inject DbContext
		public DbProjectRepository(PersonProjectRoleDbContext db)
		{
			_db = db;
		}

		//Create new Project
		public Project Create(Project project)
		{
			_db.Projects.Add(project);
			_db.SaveChanges();
			return project;
		}

		//Delete Project
		public void Delete(int id)
		{
			var project = _db.Projects.Find(id);
			_db.Remove(project);
			_db.SaveChanges();
		}

		//Read 1 Project
		public Project Read(int id)
		{
			return _db.Projects.Include(p => p.Roles).FirstOrDefault(p => p.Id == id);
		}

		//Return all Projects
		public ICollection<Project> ReadAll()
		{
			return _db.Projects.Include(p => p.Roles).ToList();
		}

		//Update a Project
		public void Update(int id, Project project)
		{
			_db.Entry(project).State = EntityState.Modified;
			_db.SaveChanges();
		}

		//Find a project by name
		public Project Find(string projectName)
		{
			return _db.Projects.Include(p => p.Roles).FirstOrDefault(p => p.Name == projectName);
		}
	}
}
