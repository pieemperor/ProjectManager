using CSCI3110Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Services.Interfaces
{
    public interface IPersonProjectRoleRepository
    {
		IQueryable<Project> ReadAllProjects();
		IQueryable<ProjectRole> ReadAllPersonRoles();
		IQueryable<Person> ReadAllPeople();
		ProjectRole Read(int personId, int projectId, int roleId);
		void RemoveFromProject(ProjectRole projectRole);
		void Update(ProjectRole projectRole);
	}
}
