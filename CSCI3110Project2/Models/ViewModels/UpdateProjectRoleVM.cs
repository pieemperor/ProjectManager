using CSCI3110Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.ViewModels
{
    public class UpdateProjectRoleVM
    {
		public string PersonName { get; set; }
		public string ProjectName { get; set; }
		public string RoleName { get; set; }

		public int PersonId { get; set; }
		public int ProjectId { get; set; }
		public int RoleId { get; set; }

		public IEnumerable<RoleNameVM> RoleNames { get; set; }

		public ProjectRole CreateProjectRole()
		{
			return new ProjectRole
			{
				PersonId = PersonId,
				ProjectId = ProjectId,
				RoleId = RoleId
			};
		}
	}
}
