using CSCI3110Project2.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.DbContexts
{
    public class PersonProjectRoleDbContext : DbContext
    {

			public PersonProjectRoleDbContext(DbContextOptions options) : base(options)
			{
			}

			public DbSet<Person> People { get; set; }
			public DbSet<Project> Projects { get; set; }
			public DbSet<Role> Roles { get; set; }
			public DbSet<ProjectRole> ProjectRoles { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.Entity<ProjectRole>()
					   .HasKey(g => new { g.PersonId, g.ProjectId, g.RoleId});
			}
	}
}
