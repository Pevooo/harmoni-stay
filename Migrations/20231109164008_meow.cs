using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class meow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guest_Name",
                table: "Guests",
                newName: "GuestName");

            migrationBuilder.AddColumn<string>(
                name: "TransactionDescription",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TransactionFee",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionTime",
                table: "Transactions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RoomBuildingNumber",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoomCategory",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoomTransactionTransactionID",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoomType",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTransactionTransactionID",
                table: "Rooms",
                column: "RoomTransactionTransactionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Transactions_RoomTransactionTransactionID",
                table: "Rooms",
                column: "RoomTransactionTransactionID",
                principalTable: "Transactions",
                principalColumn: "TransactionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Transactions_RoomTransactionTransactionID",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomTransactionTransactionID",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "TransactionDescription",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionFee",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RoomBuildingNumber",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomCategory",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomTransactionTransactionID",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "GuestName",
                table: "Guests",
                newName: "Guest_Name");
        }
    }
}
