using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class RegisterAddedHousingremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Housings_AspNetUsers_HousingUserId",
                table: "Housings");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_HousingUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_HousingUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Housings_HousingUserId",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "HousingUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "HousingUserId",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HousingUserId",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HousingUserId",
                table: "Housings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_HousingUserId",
                table: "Orders",
                column: "HousingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Housings_HousingUserId",
                table: "Housings",
                column: "HousingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Housings_AspNetUsers_HousingUserId",
                table: "Housings",
                column: "HousingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_HousingUserId",
                table: "Orders",
                column: "HousingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
