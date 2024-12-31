using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTeacherManagement.Migrations
{
    public partial class AddClassesToTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Classes",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classes",
                table: "Teachers");
        }
    }
}
