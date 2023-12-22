using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class EventTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventFee",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventTransactionTransactionID",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTransactionTransactionID",
                table: "Events",
                column: "EventTransactionTransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Transactions_EventTransactionTransactionID",
                table: "Events",
                column: "EventTransactionTransactionID",
                principalTable: "Transactions",
                principalColumn: "TransactionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Transactions_EventTransactionTransactionID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventTransactionTransactionID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventTransactionTransactionID",
                table: "Events");

            migrationBuilder.AddColumn<double>(
                name: "EventFee",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
