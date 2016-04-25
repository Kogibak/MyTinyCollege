using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTinyCollege.Models
{
    public class Instructor: Person
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        public virtual ICollection<Course> Courses { get; set; } //1 instructor to many courses

        public virtual OfficeAssignment OfficeAssignment { get; set; } //1 instructor to 1 office
    }
}