using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Models
{
	public class ProfessorDTO
	{
		public int Prof_ID { get; set; }
		public string Name { get; set; } 
		public string Section_name { get; set; }
		public int Office { get; set; }
		public string Email { get; set; }

		public IEnumerable<CourseDTO> Courses { get; set; }
	}
}
