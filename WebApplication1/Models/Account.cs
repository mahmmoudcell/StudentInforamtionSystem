using System.ComponentModel.DataAnnotations;



namespace WebApplication1.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; } // Primary Key

        public string? Name { get; set; }

        public string? PasswordHash { get; set; }

        //public string HashPassword(string password)
        //{
        //    return BCrypt.Net.BCrypt.HashPassword(password); // Ensure this namespace is correctly referenced
        //}

        //public static bool VerifyPassword(string password, string hashedPassword)
        //{
            
        //    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        //}

    }
}
