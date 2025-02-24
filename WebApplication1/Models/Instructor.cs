using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Course selection is required.")]
        [Remote(action: "VerifyUniqueInstructor", controller: "Instructors", AdditionalFields = "DepartmentId", ErrorMessage = "This course is already assigned to the department.")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Department selection is required.")]
        public int DepartmentId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Department Department { get; set; }
    }
}
