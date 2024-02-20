using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalOffice.Server.Migrations
{
    /// <inheritdoc />
    public partial class addCodeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Reservations");
        }
    }
}
