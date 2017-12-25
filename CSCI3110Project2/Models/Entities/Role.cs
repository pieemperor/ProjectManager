using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.Entities
{
    public class Role
    {
		public int Id { get; set; }
		[Required, StringLength(30)]
		public string Name { get; set; }
		[StringLength(450)]
		public string Description { get; set; }
	}
}
