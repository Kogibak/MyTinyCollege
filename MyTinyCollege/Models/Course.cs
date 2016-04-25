using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;    //UI, requirements
using System.ComponentModel.DataAnnotations.Schema; //Structure, database

namespace MyTinyCollege.Models
{
    public class Course
    {
        /* By default the ID property will become the PK of the database table
         * that corresponds to this class. By default the EF (ENtity Framework)
         * interprets a property that's named ID or ClassNameID as the PK.
         * Also, this PK will have an IDENTITY property, you can override it using
         * the DatabaseGeneratedOption enum:
         * - Computed: Database generates a value when row is inserted or updated
         * - Identity: Database generates a value when row is inserted
         * - None: Database does not generate value
         */
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Course ID")]
        public int CourseID { get; set; }   //PK with no identity property

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0,5)] //Credits can be between 0 and 5
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