using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Rooms_RoomID",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_RoomID",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Facility",
                table: "Employees");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChechIn",
                table: "Guests",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOut",
                table: "Guests",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GuestRoomRoomID",
                table: "Guests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GustRoomType",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_GuestRoomRoomID",
                table: "Guests",
                column: "GuestRoomRoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Rooms_GuestRoomRoomID",
                table: "Guests",
                column: "GuestRoomRoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Rooms_GuestRoomRoomID",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_GuestRoomRoomID",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "ChechIn",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "GuestRoomRoomID",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "GustRoomType",
                table: "Guests");

            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "Guests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facility",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_RoomID",
                table: "Guests",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Rooms_RoomID",
                table: "Guests",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID");
        }
    }
}
