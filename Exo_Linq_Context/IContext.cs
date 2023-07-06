using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Linq_Context
{
	public interface IContext
	{
		List<Student> Students { get; }
		List<Section> Sections { get; }
		List<Professor> Professors { get; }
		List<Course> Courses { get; }
		List<Grade> Grades { get; }
	}
}
