﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Entities
{
	public class ProfessorPOCO
	{
		public int Professor_ID { get; set; }
		public string Professor_Name { get; set; }
		public string Professor_Surname { get; set; }
		public int Section_ID { get; set; }
		public int Professor_Office { get; set; }
		public string Professor_Email { get; set; }
		public DateTime Professor_HireDate { get; set; }
		public int Professor_Wage { get; set; }
	}
}
