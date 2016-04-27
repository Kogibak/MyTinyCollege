using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyTinyCollege.ViewModels
{
    public class EnrollmentDateGroup
    {
        //This will be used to show student body stats report
        //counting how many students enrolled on a particular EnrollmentDate
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }
        public int StudentCount { get; set; }
    }
}