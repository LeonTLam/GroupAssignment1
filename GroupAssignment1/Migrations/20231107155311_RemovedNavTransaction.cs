using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupAssignment1.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNavTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Housings_Transactions_TransactionId",
                table: "Housings");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Transactions_TransactionId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TransactionId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Housings_TransactionId",
                table: "Housings");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Housings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Orders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Housings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TransactionId",
                table: "Orders",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Housings_TransactionId",
                table: "Housings",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Housings_Transactions_TransactionId",
                table: "Housings",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Transactions_TransactionId",
                table: "Orders",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId");
        }
    }
}
