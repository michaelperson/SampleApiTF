using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Models
{
	public class CourseDTO
	{
		public string Course_ID { get; set; }
		public string Course_Name { get; set; }
		public decimal Course_Ects { get; set; }
		public int Professor_ID { get; set; }
	}
}
