using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.Entities
{
    public class Person
    {
		public int Id { get; set; }
		[Required, StringLength(30)]
		public string FirstName { get; set; }
		[StringLength(30)]
		public string MiddleName { get; set; }
		[Required, StringLength(30)]
		public string LastName { get; set; }
		[Required]
		public string Email { get; set; }

		public ICollection<ProjectRole> Roles { get; set; }
	}
}
