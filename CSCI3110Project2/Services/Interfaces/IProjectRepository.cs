using CSCI3110Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Services.Interfaces
{
    public interface IProjectRepository
    {
		Project Create(Project project);
		ICollection<Project> ReadAll();
		Project Read(int id);
		void Update(int id, Project project);
		void Delete(int id);
		Project Find(string projectName);
	}
}
