using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTinyCollege.Models
{
    public abstract class Person
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string Email { get; set; }

        public string FullName
        //FullName is a calculated proprety that calculates a value created by concatenating other propreties
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
    }
}