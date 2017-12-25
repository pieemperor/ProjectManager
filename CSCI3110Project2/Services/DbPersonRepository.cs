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
	public class DbPersonRepository : IPersonRepository
	{
		private PersonProjectRoleDbContext _db;

		public DbPersonRepository(PersonProjectRoleDbContext db)
		{
			_db = db;
		}

		//Create a new person
		public Person Create(Person person)
		{
			_db.People.Add(person);
			_db.SaveChanges();
			return person;
		}

		//Delete a person
		public void Delete(int id)
		{
			var person = _db.People.Find(id);
			_db.Remove(person);
			_db.SaveChanges();
		}

		//Read 1 person
		public Person Read(int id)
		{
			return _db.People.Include(p => p.Roles).FirstOrDefault(p => p.Id == id);
		}

		//Return all people
		public ICollection<Person> ReadAll()
		{
			return _db.People.Include(p => p.Roles).ToList();
		}

		//Update a person
		public void Update(int id, Person person)
		{
			_db.Entry(person).State = EntityState.Modified;
			_db.SaveChanges();
		}

		//Assign a person to a project with a role
		public void AssignToProject(ProjectRole pr)
		{
				_db.ProjectRoles.Add(pr);
				pr.Project.Roles.Add(pr);
				_db.Entry(pr.Project).State = EntityState.Modified;
				pr.Person.Roles.Add(pr);
				_db.Entry(pr.Person).State = EntityState.Modified;
				_db.SaveChanges();
		}
	}
}
