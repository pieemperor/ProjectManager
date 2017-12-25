using CSCI3110Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.ViewModels
{
    public class CreateProjectVM
    {
		[Required, StringLength(30)]
		public string Name { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public Project CreateProject()
		{
			return new Project
			{
				Name = Name,
				StartDate = StartDate,
				EndDate = EndDate
			};
		}
	}
}
