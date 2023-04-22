using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDBAssignment4C2.Models
{
    public class Teacher
    {
        // Teacher description
        public int Id;
        public string TeacherFirstName;
        public string TeacherLastName;
        public string TeacherFullName;
        public string EmployeeNumber;
        public DateTime HireDate;
        public Decimal Salary;
    }
}