using System;
using System.Collections.Generic;

namespace SampleApi.EF;

public partial class Course
{
    public string CourseId { get; set; } = null!;

    public string CourseName { get; set; } = null!;

    public decimal CourseEcts { get; set; }

    public int ProfessorId { get; set; }

    public virtual Professor Professor { get; set; } = null!;
}
