using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class opd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert Admin role if it doesn't already exist
            migrationBuilder.Sql(@"
        IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE NormalizedName = 'ADMIN')
        BEGIN
            INSERT INTO AspNetRoles (Id, Name, NormalizedName) 
            VALUES (NEWID(), 'Admin', 'ADMIN')
        END");

            // Insert Student role if it doesn't already exist
            migrationBuilder.Sql(@"
        IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE NormalizedName = 'STUDENT')
        BEGIN
            INSERT INTO AspNetRoles (Id, Name, NormalizedName) 
            VALUES (NEWID(), 'Student', 'STUDENT')
        END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove Admin role if it exists
            migrationBuilder.Sql(@"
        DELETE FROM AspNetRoles 
        WHERE NormalizedName = 'ADMIN'");

            // Remove Student role if it exists
            migrationBuilder.Sql(@"
        DELETE FROM AspNetRoles 
        WHERE NormalizedName = 'STUDENT'");
        }

    }
}
