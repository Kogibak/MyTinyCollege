using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyTinyCollege.DAL;
using MyTinyCollege.Models;
using PagedList;
using MyTinyCollege.ViewModels; //for student body stat report

namespace MyTinyCollege.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Student
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            //Prepare sort order 
            ViewBag.CurrentSort = sortOrder;//get current sort from UI
            ViewBag.LNameSortParm = string.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
            ViewBag.FNameSortParm = sortOrder == "fname" ? "fname_desc" : "fname";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";

            //for filtering
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var students = from s in db.Students select s;

            //check for filder (serchString)
            if(!string.IsNullOrEmpty(searchString))
            {
                //apply filter on first and last name
                students = students.Where(s => s.LastName.Contains(searchString) ||
                s.FirstMidName.Contains(searchString));
            }

            //apply the sord order
            switch (sortOrder)
            {
                //firstName ASC
                case "fname":
                    students = students.OrderBy(s => s.FirstMidName);
                    break;

                //FirstName DESC
                                case "fname_desc":
                    students = students.OrderByDescending(s => s.FirstMidName);
                    break;

                //EnrollmentDate Asc
                case "date":
                    students = students.OrderBy(s => s.EnrollementDate);
                    break;

                //EnrollmentDate Desc
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollementDate);
                    break;

                //Email Asc
                case "email":
                    students = students.OrderBy(s => s.Email);
                    break;

                //Email Desc
                case "email_desc":
                    students = students.OrderByDescending(s => s.Email);
                    break;
                //LastName Desc
                case "lname_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;

                //default LastName ASC
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            //return the students object as an enumerable (list)
            //return View(db.Students.ToList());

            //setup pager
            int pageSize = 3; //determines how many records per page
            int pageNumber = (page ?? 1);
            /* The two question marks represent the null-coalescing operator.
             * The null-Coalescing operator defines a default value for a nullable type;
             * the expression (page ?? 1) means return the value of page if it has a value,
             * or return 1 if page is null
             */
            return View(students.ToPagedList(pageNumber, pageSize));

        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstMidName,Email,EnrollementDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.People.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception /*ex*/)
            {
                //We could log the error by uncommenting the ex
                ModelState.AddModelError("", "Unable to save changes. Try again later!");
                throw;
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,Email,EnrollementDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.People.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //stats
        public ActionResult Stats()
        {
            IQueryable<ViewModels.EnrollmentDateGroup> data =
                from student in db.Students
                group student by student.EnrollementDate into dateGroup
                select new ViewModels.EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            //the LINQ statement above groups the student entities by enrollment date,
            //calculating the number of entities in each group and storing the results
            //in a collection of EnrollementDateGroup view model objects.
            return View(data.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
