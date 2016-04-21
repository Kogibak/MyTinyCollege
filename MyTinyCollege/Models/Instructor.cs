﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTinyCollege.Models
{
    public class Instructor: Person
    {
        public DateTime HireDate { get; set; }
        public virtual ICollection<Course> Courses { get; set; } //1 instructor to many courses

        public virtual OfficeAssignment OfficeAssignment { get; set; } //1 instructor to 1 office
    }
}