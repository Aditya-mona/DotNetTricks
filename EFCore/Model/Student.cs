using System;
using System.Collections.Generic;

#nullable disable

namespace EFCore.Model
{
    public partial class Student
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public Guid CourseId { get; set; }
        
    }
}
