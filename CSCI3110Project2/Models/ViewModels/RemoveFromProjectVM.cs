using CSCI3110Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.ViewModels
{
    public class RemoveFromProjectVM
    {
		[Required]
		public string Name { get; set; }

		[Required]
		public string Project { get; set; }

		[Required]
		public string Role { get; set; }

		public int PersonId { get; set; }
		public int ProjectId { get; set; }
		public int RoleId { get; set; }
	}
}
