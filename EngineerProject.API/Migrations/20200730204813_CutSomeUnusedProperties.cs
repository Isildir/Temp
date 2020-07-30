using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineerProject.API.Migrations
{
    public partial class CutSomeUnusedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Edited",
                table: "Posts",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDate",
                table: "Comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Edited",
                table: "Comments",
                type: "bit",
                nullable: true);
        }
    }
}
