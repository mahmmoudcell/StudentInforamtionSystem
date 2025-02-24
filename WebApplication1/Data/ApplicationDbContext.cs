//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using WebApplication1.Models;

//namespace WebApplication1
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<WebApplication1.Models.Trainee> Trainee { get; set; } = default!;
//        public DbSet<WebApplication1.Models.Course> Course { get; set; } = default!;
//        public DbSet<WebApplication1.Models.crsResult> crsResult { get; set; } = default!;
//        public DbSet<WebApplication1.Models.Department> Department { get; set; } = default!;
//        public DbSet<WebApplication1.Models.Instructor> Instructor { get; set; } = default!;
//    }
//}
