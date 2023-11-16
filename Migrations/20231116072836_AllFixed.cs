using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class AllFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Rooms_GuestRoomRoomID",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_GuestRoomRoomID",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "GuestRoomRoomID",
                table: "Guests");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckIn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime", nullable: false),
                    BookingRoomRoomID = table.Column<int>(type: "int", nullable: false),
                    BookingGuestGuestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_Guests_BookingGuestGuestID",
                        column: x => x.BookingGuestGuestID,
                        principalTable: "Guests",
                        principalColumn: "GuestID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_BookingRoomRoomID",
                        column: x => x.BookingRoomRoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingGuestGuestID",
                table: "Bookings",
                column: "BookingGuestGuestID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingRoomRoomID",
                table: "Bookings",
                column: "BookingRoomRoomID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckIn",
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
    }
}
