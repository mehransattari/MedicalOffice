using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalOffice.Server.Migrations
{
    /// <inheritdoc />
    public partial class change_slider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Property1",
                table: "Sliders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Property2",
                table: "Sliders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Property3",
                table: "Sliders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDesc1",
                table: "Sliders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDesc2",
                table: "Sliders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDesc3",
                table: "Sliders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Property1",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "Property2",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "Property3",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "ShortDesc1",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "ShortDesc2",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "ShortDesc3",
                table: "Sliders");
        }
    }
}
