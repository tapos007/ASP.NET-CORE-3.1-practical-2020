using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class UpdateStudentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RollNo",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RollNo",
                table: "Students");
        }
    }
}
