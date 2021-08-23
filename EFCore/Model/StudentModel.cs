using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Model
{
    public class StudentModel : Student
    {
        public string CourseName { get; set; }
    }
}
