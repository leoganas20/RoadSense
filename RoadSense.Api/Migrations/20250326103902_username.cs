using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadSense.Api.Migrations
{
    /// <inheritdoc />
    public partial class username : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ManageUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ManageUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "ManageUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suffix",
                table: "ManageUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ManageUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ManageUsers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "ManageUsers");

            migrationBuilder.DropColumn(
                name: "Suffix",
                table: "ManageUsers");
        }
    }
}
