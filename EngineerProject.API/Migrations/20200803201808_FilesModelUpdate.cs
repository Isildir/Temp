using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineerProject.API.Migrations
{
    public partial class FilesModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Files",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "FileType",
                table: "Files",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Files",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Files");
        }
    }
}
