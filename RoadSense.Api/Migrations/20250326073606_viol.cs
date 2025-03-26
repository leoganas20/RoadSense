using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadSense.Api.Migrations
{
    /// <inheritdoc />
    public partial class viol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Violations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlateNumber",
                table: "Violations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "PlateNumber",
                table: "Violations");
        }
    }
}
