using Microsoft.AspNetCore.Cors.Infrastructure;

namespace WebApplication1.Models
{
    public class Trainee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; } 
        public string Address { get; set; }
        public int Grade { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<crsResult> CourseResults { get; set; }
    }
}
