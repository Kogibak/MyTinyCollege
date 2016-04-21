using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTinyCollege.Models
{
    public class Student: Person
    {
        public DateTime EnrollementDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        /*
         * Within the Entity framework, the Enrollements proprety is called a navigation proprety. Navigation
         * propreties hold other entities that are related to this entity. In this case, the enrollments
         * proprety of a student entity will hold all of the enrollment entities that are related to that
         * student entity.
         * In other words, if a given student row in a database has two related enrollement rows ()PK-FK,
         * that student's entity enrollement navigation proprety will contain those two enrollement entities. 
        */
    }
}