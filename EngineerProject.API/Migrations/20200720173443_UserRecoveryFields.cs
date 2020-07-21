using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EngineerProject.API.Migrations
{
    public partial class UserRecoveryFields : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecoveryCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RecoveryExpirationDate",
                table: "Users");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecoveryCode",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecoveryExpirationDate",
                table: "Users",
                nullable: true);
        }
    }
}