using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class OrderRepo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Housings",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "endDate",
                table: "Housings",
                newName: "EndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Housings",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Housings",
                newName: "endDate");
        }
    }
}
