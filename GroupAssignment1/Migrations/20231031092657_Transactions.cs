using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class Transactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            


            migrationBuilder.DropForeignKey(
                name: "FK_Housings_Orders_OrderId",
                table: "Housings");

            

           

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Housings_CustomerId",
                table: "Housings");

            migrationBuilder.DropIndex(
                name: "IX_Housings_OrderId",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Housings");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Housings",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Including",
                table: "Housings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Housings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Housings",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));


            

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransactionDate = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "Including",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Housings");

            

            

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Housings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Housings",
                type: "INTEGER",
                nullable: true);

           

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Housings_CustomerId",
                table: "Housings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Housings_OrderId",
                table: "Housings",
                column: "OrderId");

            

            

            migrationBuilder.AddForeignKey(
                name: "FK_Housings_Orders_OrderId",
                table: "Housings",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");

            

        }
    }
}
