using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            List<StudentViewModel> lst = new List<StudentViewModel>();

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; Integrated Security=True; Database=AchsDB11");

            SqlCommand cmd = new SqlCommand("SELECT * FROM tblStudent", con);
            con.Open();
           
            SqlDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                lst.Add(new StudentViewModel()
                {
                    StudentId = (int)dr["StudentId"],
                    Fullname =(string)dr["Fullname"],
                    Email = (string)dr["Email"],
                    Phone = (string)dr["Phone"]
                });
            }
            dr.Close();
            con.Close();
            return View(lst);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentViewModel stu)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; Integrated Security=true; Database=AchsDB11");
            SqlCommand cmd = new SqlCommand("insert into tblStudent values(@a,@b,@c)", con);
            cmd.Parameters.AddWithValue("@a", stu.Fullname);
            cmd.Parameters.AddWithValue("@b", stu.Email);
            cmd.Parameters.AddWithValue("@c", stu.Phone);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ViewBag.Message = "Created Successfully";
            return View();
        }
        public IActionResult Edit(int id)
        {
            StudentViewModel student = new StudentViewModel();

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; Integrated Security=True; Database=AchsDB11");

            SqlCommand cmd = new SqlCommand("SELECT * FROM tblStudent Where StudentId = @a", con);
            cmd.Parameters.AddWithValue("@a", id);
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();


            if (dr.Read())
            {
                student.StudentId = (int)dr["StudentId"];
                student.Fullname = (string)dr["Fullname"];
                student.Email = (string)dr["Email"];
                student.Phone = (string)dr["Phone"];
            }
            dr.Close();
            con.Close();
            return View(student);
        }
        [HttpGet]
       
        [HttpPost]
        public IActionResult Edit(StudentViewModel stu)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; Integrated Security=true; Database=AchsDB11");
            SqlCommand cmd = new SqlCommand("UPDATE tblStudent set Fullname =@a, Email=@b, Phone=@c WHERE StudentId = @d", con);
            cmd.Parameters.AddWithValue("@a", stu.Fullname);
            cmd.Parameters.AddWithValue("@b", stu.Email);
            cmd.Parameters.AddWithValue("@c", stu.Phone);
            cmd.Parameters.AddWithValue("@d", stu.StudentId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            ViewBag.Message = "Updated Successfully";
            return View();
        }

        public IActionResult Delete(int id)
        {
            StudentViewModel student = new StudentViewModel();

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; Integrated Security=True; Database=AchsDB11");

            SqlCommand cmd = new SqlCommand("SELECT * FROM tblStudent WHERE StudentId=@a", con);
            cmd.Parameters.AddWithValue("@a", id); // passing value of @a paramterer in the SqlCommand
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader(); // data reader

            // adding the property readed from db to student obj
            if (dr.Read()) // if there is data
            {
                student.StudentId = (int)dr["StudentId"];
                student.Fullname = (string)dr["FullName"];
                student.Email = (string)dr["Email"];
                student.Phone = (string)dr["Phone"];
            }
            dr.Close();
            con.Close();

            return View(student);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete_Post(int id)
        {
            StudentViewModel student = new StudentViewModel();

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; Integrated Security=True; Database=AchsDB11");

            SqlCommand cmd = new SqlCommand("DELETE FROM tblStudent Where StudentId = @a", con);
            cmd.Parameters.AddWithValue("@a", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
            return RedirectToAction("Index");
        }
    }
}
