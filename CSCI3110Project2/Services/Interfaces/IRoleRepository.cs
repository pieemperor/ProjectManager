using CSCI3110Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Services.Interfaces
{
    public interface IRoleRepository
    {
		Role Create(Role role);
		ICollection<Role> ReadAll();
		Role Read(int id);
		void Update(int id, Role role);
		void Delete(int id);
	}
}
