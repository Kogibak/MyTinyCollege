namespace MyTinyCollege.Migrations.CollegeMigrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyTinyCollege.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\CollegeMigrations";
        }

        protected override void Seed(MyTinyCollege.DAL.SchoolContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //1. Add some students
            var Students = new List<Student>
            {
                new Student {FirstMidName = "Carson",
                    LastName = "Alexander",
                    EnrollementDate = DateTime.Parse("2015-02-01"),
                    Email = "calexander@tinycollege.com"},
                new Student {FirstMidName = "Alonso",
                    LastName = "Arturo",
                    EnrollementDate = DateTime.Parse("2015-02-01"),
                    Email = "aarturo@tinycollege.com"},
                new Student {FirstMidName = "John",
                    LastName = "Smith",
                    EnrollementDate = DateTime.Parse("2015-03-01"),
                    Email = "jsmith@tinycollege.com"},
                new Student {FirstMidName = "Frank",
                    LastName = "Bekkering",
                    EnrollementDate = DateTime.Parse("2015-02-20"),
                    Email = "fbekkering@tinycollege.com"},
                new Student {FirstMidName = "Laura",
                    LastName = "Norman",
                    EnrollementDate = DateTime.Parse("2015-02-15"),
                    Email = "lnorman@tinycollege.com"}
            };
            //Loop students and add to teh database only if they are not already there
            //using the add or update method
            //The first parameter of this method specifies the property to check if a row already exists
            Students.ForEach(s => context.Students.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();

            //2. Add some instructors
            var instructors = new List<Instructor>
            {
                new Instructor {FirstMidName = "Denis",
                    LastName = "Ouellette",
                    HireDate = DateTime.Parse("2015-08-4"),
                    Email = "douellette@tinycollege.com"},
                new Instructor {FirstMidName = "Nadine",
                    LastName = "Savoie",
                    HireDate = DateTime.Parse("2015-01-21"),
                    Email = "nsavoie@tinycollege.com"}
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();

            //3. Add some departments
            var departments = new List<Department>
            {
                new Department {Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2010-09-01"), InstructorID = 1},
                new Department {Name = "English", Budget = 150000,
                    StartDate = DateTime.Parse("2010-09-01"), InstructorID = 2},
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            //4. Add some courses
            var courses = new List<Course>
            {
                new Course {CourseID = 1045, Title = "Chemistry", DepartmentID = 1, Credits = 3 },
                new Course {CourseID = 4022, Title = "Physics", DepartmentID = 1, Credits = 3 },
                new Course {CourseID = 3141, Title = "Calculus", DepartmentID = 1, Credits = 3 },
                new Course {CourseID = 4011, Title = "Banana", DepartmentID = 2, Credits = 3 }
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

            //5. Add some enrollments
            var enrollments = new List<Enrollment>
            {
                new Enrollment {StudentID=1, CourseID=1045, Grade=Grade.A},
                new Enrollment {StudentID=1, CourseID=3141, Grade=Grade.C},
                new Enrollment {StudentID=2, CourseID=3141, Grade=Grade.B},
                new Enrollment {StudentID=2, CourseID=1045, Grade=Grade.A},
                new Enrollment {StudentID=2, CourseID=4022, Grade=Grade.A},
                new Enrollment {StudentID=3, CourseID=4011, Grade=Grade.B},
                new Enrollment {StudentID=4, CourseID=1045, Grade=Grade.A}
            };
            foreach(Enrollment e in enrollments)
            {
                var enrollmentInDatabase = context.Enrollments.Where(
                    s =>
                    s.StudentID == e.StudentID &&
                    s.course.CourseID == e.CourseID).SingleOrDefault();

                //SingleOrDefault
                //Use this when you're expecting a 0 or 1 item
                //if you get 0 when 0 or 1 is expected, that's ok
                //if you get 1 when 0 or 1 is expected, that's ok
                //if you get 2 or more, that's not ok

                //Single
                //returns a single, specific element of a sequence
                //used when 1 is expected only
                //if you get 0 when 1 is expected, that's not
                //if you get 1 when 1 is expected, that's ok
                //if you get 2 or more, that's not ok

                if(enrollmentInDatabase == null)
                {
                    //enrollment was not found - add it to db context
                    context.Enrollments.Add(e);
                }
            }//end of foreach
            context.SaveChanges();
        }
    }
}
