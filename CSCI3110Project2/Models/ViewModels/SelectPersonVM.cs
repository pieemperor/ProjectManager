using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.ViewModels
{
    public class SelectPersonVM
    {
		public string ProjectId { get; set; }
		public IEnumerable<PersonNameVM> PersonNames { get; set; }
		public IEnumerable<RoleNameVM> RoleNames { get; set; }

	}
}
