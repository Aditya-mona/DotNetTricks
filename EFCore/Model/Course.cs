using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore.Model
{
    public partial class Course
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
