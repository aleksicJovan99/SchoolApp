using System;
using System.Collections.Generic;

namespace SchoolApp
{
    public partial class SchoolClass
    {
        public int Id { get; set; }
        public int? SchoolId { get; set; }
        public int? ClassId { get; set; }

        public virtual Class? Class { get; set; }
        public virtual School? School { get; set; }
    }
}
