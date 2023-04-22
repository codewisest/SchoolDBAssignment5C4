using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using SchoolDBAssignment4C2.Models;
using System.Web.Http.Cors;

namespace SchoolDBAssignment4C2.Controllers
{
    public class TeacherDataController : ApiController
    {
        // allow access to our Database.
        private SchoolDBContext School = new SchoolDBContext();

        /// <summary>
        /// returns a list of teachers in the database
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <returns>
        /// A list of Teacher Objects with fields mapped to the database column values
        /// </returns>
        /// 
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new cmd (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where lower(Teacherfname) like lower(@key) or lower(Teacherlname) like lower(@key) or lower(concat(Teacherfname, ' ', Teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row in the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["Teacherfname"].ToString();
                string TeacherLname = ResultSet["Teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                Decimal Salary = (Decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.Id = TeacherId;
                NewTeacher.TeacherFirstName = TeacherFname;
                NewTeacher.TeacherLastName = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teacher names
            return Teachers;
        }

        /// <summary>
        /// uses the id to get a teacher from the database
        /// </summary>
        /// <param name="id"> ID of the teacher</param>
        /// <returns>
        /// A teacher object that has the corresponding ID
        /// </returns>
        [HttpGet]
        public Teacher ShowTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new cmd (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // sql query
            cmd.CommandText = "Select * from Teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            // assign result of query to a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                // use DB column name as index to access column information
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFirstName = (string)ResultSet["teacherfname"];
                string TeacherLastName = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                Decimal Salary = (Decimal)ResultSet["salary"];

                NewTeacher.Id = TeacherId;
                NewTeacher.TeacherFirstName = TeacherFirstName;
                NewTeacher.TeacherLastName = TeacherLastName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }
            Conn.Close();
            return NewTeacher;
        }

        /// <summary>
        /// Adds a teacher to the database
        /// </summary>
        /// <param name="NewTeacher">A teacher object with fields that correspond to the columns of the Teacher's table</param>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new cmd (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // sql query
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFirstName, @TeacherLastName, @EmployeeNumber, @HireDate, @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFirstName", NewTeacher.TeacherFirstName);
            cmd.Parameters.AddWithValue("@TeacherLastName", NewTeacher.TeacherLastName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        /// <summary>
        /// uses the corresponding id to delete the teacher's information from the database
        /// </summary>
        /// <param name="id">The teacher's id</param>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }
    }
}

