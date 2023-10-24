using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class RentalDbExpanded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Housings_Customers_OwnerId",
                table: "Housings");

            migrationBuilder.DropIndex(
                name: "IX_Housings_OwnerId",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Housings");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Housings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Housings_CustomerId",
                table: "Housings",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Housings_Customers_CustomerId",
                table: "Housings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Housings_Customers_CustomerId",
                table: "Housings");

            migrationBuilder.DropIndex(
                name: "IX_Housings_CustomerId",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Housings");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Housings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Housings_OwnerId",
                table: "Housings",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Housings_Customers_OwnerId",
                table: "Housings",
                column: "OwnerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
