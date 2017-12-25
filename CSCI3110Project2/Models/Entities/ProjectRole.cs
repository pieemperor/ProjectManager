using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.Entities
{
    public class ProjectRole
    {
		public int ProjectId { get; set; }
		public int PersonId { get; set; }
		public int RoleId { get; set; }

		public Person Person { get; set; }
		public Project Project { get; set; }
		public Role Role { get; set; }
	}
}
