﻿using CSCI3110Project2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSCI3110Project2.Models.ViewModels
{
    public class PersonGroupVM
    {
		public string PersonName { get; set; }
		public IEnumerable<ProjectRole> ProjectRoles { get; set; }
	}
}
