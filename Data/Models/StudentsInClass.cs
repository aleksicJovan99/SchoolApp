﻿using System;
using System.Collections.Generic;

namespace SchoolApp
{
    public partial class StudentsInClass
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int ClassId { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Student? Student { get; set; }
    }
}
