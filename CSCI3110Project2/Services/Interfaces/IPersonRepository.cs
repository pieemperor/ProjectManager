using CSCI3110Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Services.Interfaces
{
    public interface IPersonRepository
    {
		Person Create(Person person);
		ICollection<Person> ReadAll();
		Person Read(int id);
		void Update(int id, Person person);
		void Delete(int id);
		void AssignToProject(ProjectRole pr);

	}
}
