using System;
using System.Collections.Generic;

namespace SchoolApp
{
    public partial class Student
    {
        public Student()
        {
            StudentsInClasses = new HashSet<StudentsInClass>();
        }

        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public int? SchoolId { get; set; }

        public virtual School? School { get; set; }
        public virtual ICollection<StudentsInClass> StudentsInClasses { get; set; }
    }
}
