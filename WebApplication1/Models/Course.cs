using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [Remote(action: "VerifyCourseName", controller: "Courses", ErrorMessage = "Course name already exists")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Course degree is required")]
        public int degree { get; set; }

        [Required(ErrorMessage = "Minimum degree is required")]
        public int Mindegree { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int departmentId { get; set; }

        public ICollection<Instructor> instructors { get; set; }
        public Department Department { get; set; }
    }
}
