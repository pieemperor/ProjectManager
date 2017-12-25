using CSCI3110Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.ViewModels
{
    public class CreatePersonVM
    {
		[Required, StringLength(30)]
		public string FirstName { get; set; }
		[StringLength(30)]
		public string MiddleName { get; set; }
		[Required, StringLength(30)]
		public string LastName { get; set; }
		[Required]
		public string Email { get; set; }

		public Person CreatePerson()
		{
			return new Person
			{
				FirstName = FirstName,
				MiddleName = MiddleName,
				LastName = LastName,
				Email = Email
			};
		}
	}
}
