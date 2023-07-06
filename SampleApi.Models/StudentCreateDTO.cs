using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApi.Models
{
	public class StudentCreateDTO
	{ 
		public string First_Name { get; set; }
		public string Last_Name { get; set; }
		public DateTime BirthDate { get; set; }
		public string Login { get; set; }
		public int Section_ID { get; set; }
		public int Year_Result { get; set; }
		public string Course_ID { get; set; }
	}
}
