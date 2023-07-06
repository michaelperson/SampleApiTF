using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Models
{
	public class StudentDTO
	{
		public int Student_ID { get; set; }
		public string FullName { get; set; } 
		public DateTime BirthDate { get; set; }
		public string Login { get; set; }
		public int Section_ID { get; set; }
		public int Year_Result { get; set; }
		public string Course_ID { get; set; }
	}
}
