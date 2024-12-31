using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTeacherManagement.Migrations
{
    public partial class UpdateSubjectAndSubjectLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "SubjectLanguages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "SubjectLanguages");
        }
    }
}
