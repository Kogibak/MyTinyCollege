﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyTinyCollege.Models;

namespace MyTinyCollege.ViewModels
{
    public class InstructorIndexData
    {
        //This is for reading related data: instructors, courses, enrollments
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }

    }
}