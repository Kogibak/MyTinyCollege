using System.Collections;
using System.Collections.Generic;

namespace MyTinyCollege.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public int DepartmentID { get; set; }   //FK to department

        //Navigation propreties
        public virtual ICollection<Enrollment> Enrollments { get; set; } //1 course to many enrollments
        
        public virtual ICollection<Instructor> Instructors { get; set; } // 1 course to many instructors

        public virtual Department Department { get; set; } //course belongs to a department

        //Combine courseID and title in one proprety
        public string CourseIDTitle
        {
            get
            {
                return CourseID + ": " + Title;
            }
        }
    }
}