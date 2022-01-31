using System;
using System.Collections.Generic;

namespace SchoolApp
{
    public partial class School
    {
        public School()
        {
            SchoolClasses = new HashSet<SchoolClass>();
            Students = new HashSet<Student>();
        }

        public int SchoolId { get; set; }
        public string? SchoolName { get; set; }

        public virtual ICollection<SchoolClass> SchoolClasses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
