using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.ViewModels
{
    public class ProjectListVM
    {
		public int Id { get; set; }
		[Required, StringLength(30)]
		public string Name { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
