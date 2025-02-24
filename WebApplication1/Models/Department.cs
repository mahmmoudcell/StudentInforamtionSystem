using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(100, ErrorMessage = "Department name cannot be longer than 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Manager name is required")]
        [StringLength(100, ErrorMessage = "Manager name cannot be longer than 100 characters")]
        public string? Manager { get; set; }


        public virtual ICollection<Instructor> instructors { get; set; }
        public virtual ICollection<Course> courses { get; set; }
        public virtual ICollection<Trainee> traines { get; set; }
    }
}
