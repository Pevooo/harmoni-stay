using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class crud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_AccountEmployeeEmployeeID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Guests_BookingGuestGuestID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_BookingRoomRoomID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Facilities_FacilityEmployeeFacilityID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Facilities_EventFacilityFacilityID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Rooms_TransactionRoomRoomID",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_AccountEmployeeEmployeeID",
                table: "Accounts",
                column: "AccountEmployeeEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Guests_BookingGuestGuestID",
                table: "Bookings",
                column: "BookingGuestGuestID",
                principalTable: "Guests",
                principalColumn: "GuestID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_BookingRoomRoomID",
                table: "Bookings",
                column: "BookingRoomRoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Facilities_FacilityEmployeeFacilityID",
                table: "Employees",
                column: "FacilityEmployeeFacilityID",
                principalTable: "Facilities",
                principalColumn: "FacilityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Facilities_EventFacilityFacilityID",
                table: "Events",
                column: "EventFacilityFacilityID",
                principalTable: "Facilities",
                principalColumn: "FacilityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Rooms_TransactionRoomRoomID",
                table: "Transactions",
                column: "TransactionRoomRoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_AccountEmployeeEmployeeID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Guests_BookingGuestGuestID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_BookingRoomRoomID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Facilities_FacilityEmployeeFacilityID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Facilities_EventFacilityFacilityID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Rooms_TransactionRoomRoomID",
                table: "Transactions");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_AccountEmployeeEmployeeID",
                table: "Accounts",
                column: "AccountEmployeeEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Guests_BookingGuestGuestID",
                table: "Bookings",
                column: "BookingGuestGuestID",
                principalTable: "Guests",
                principalColumn: "GuestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_BookingRoomRoomID",
                table: "Bookings",
                column: "BookingRoomRoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Facilities_FacilityEmployeeFacilityID",
                table: "Employees",
                column: "FacilityEmployeeFacilityID",
                principalTable: "Facilities",
                principalColumn: "FacilityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Facilities_EventFacilityFacilityID",
                table: "Events",
                column: "EventFacilityFacilityID",
                principalTable: "Facilities",
                principalColumn: "FacilityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Rooms_TransactionRoomRoomID",
                table: "Transactions",
                column: "TransactionRoomRoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID");
        }
    }
}
