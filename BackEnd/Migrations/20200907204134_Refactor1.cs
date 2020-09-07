using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class Refactor1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Coaches");

            migrationBuilder.AddColumn<string>(
                name: "CoachName",
                table: "Coaches",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoachName",
                table: "Coaches");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Coaches",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Coaches",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Coaches",
                type: "TEXT",
                maxLength: 200,
                nullable: true);
        }
    }
}
