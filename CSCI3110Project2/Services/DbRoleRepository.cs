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
	public class DbRoleRepository : IRoleRepository
	{
		//Create private variable of DbContext
		private PersonProjectRoleDbContext _db;

		//Inject DbContext
		public DbRoleRepository(PersonProjectRoleDbContext db)
		{
			_db = db;
		}

		//Create a new Role
		public Role Create(Role role)
		{
			_db.Roles.Add(role);
			_db.SaveChanges();
			return role;
		}

		//Delete a Role
		public void Delete(int id)
		{
			var role = _db.Roles.Find(id);
			_db.Remove(role);
			_db.SaveChanges();
		}

		//Read 1 Role
		public Role Read(int id)
		{
			return _db.Roles.FirstOrDefault(r => r.Id == id);
		}

		//Return all Roles
		public ICollection<Role> ReadAll()
		{
			return _db.Roles.ToList();
		}

		//Update a Role
		public void Update(int id, Role role)
		{
			_db.Entry(role).State = EntityState.Modified;
			_db.SaveChanges();
		}
	}
}
