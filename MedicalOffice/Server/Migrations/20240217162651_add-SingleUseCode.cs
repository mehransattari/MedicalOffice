using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalOffice.Server.Migrations
{
    /// <inheritdoc />
    public partial class addSingleUseCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SingleUseCode",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SingleUseCode",
                table: "Users");
        }
    }
}
