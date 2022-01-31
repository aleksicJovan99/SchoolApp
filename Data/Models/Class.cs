using System;
using System.Collections.Generic;

namespace SchoolApp
{
    public partial class Class
    {
        public Class()
        {
            SchoolClasses = new HashSet<SchoolClass>();
            StudentsInClasses = new HashSet<StudentsInClass>();
        }

        public int ClassId { get; set; }
        public string? ClassName { get; set; }

        public virtual ICollection<SchoolClass> SchoolClasses { get; set; }
        public virtual ICollection<StudentsInClass> StudentsInClasses { get; set; }
    }
}
