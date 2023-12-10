using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class LastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityWorkStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    FacilityWorkEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityID);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    GuestID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GuestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuestPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuestNationality = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.GuestID);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomBuildingNumber = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomCategory = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeSalary = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    WorkingHours = table.Column<double>(type: "float", nullable: false),
                    FacilityEmployeeFacilityID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Facilities_FacilityEmployeeFacilityID",
                        column: x => x.FacilityEmployeeFacilityID,
                        principalTable: "Facilities",
                        principalColumn: "FacilityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventFee = table.Column<double>(type: "float", nullable: false),
                    EventStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    EventEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    EventFacilityFacilityID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_Facilities_EventFacilityFacilityID",
                        column: x => x.EventFacilityFacilityID,
                        principalTable: "Facilities",
                        principalColumn: "FacilityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckIn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime", nullable: false),
                    BookingRoomRoomID = table.Column<int>(type: "int", nullable: true),
                    BookingGuestGuestID = table.Column<string>(type: "nvarchar(450)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionFee = table.Column<double>(type: "float", nullable: false),
                    TransactionTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    TransactionRoomRoomID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Rooms_TransactionRoomRoomID",
                        column: x => x.TransactionRoomRoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AccountEmployeeEmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_Employees_AccountEmployeeEmployeeID",
                        column: x => x.AccountEmployeeEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountEmployeeEmployeeID",
                table: "Accounts",
                column: "AccountEmployeeEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingGuestGuestID",
                table: "Bookings",
                column: "BookingGuestGuestID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingRoomRoomID",
                table: "Bookings",
                column: "BookingRoomRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FacilityEmployeeFacilityID",
                table: "Employees",
                column: "FacilityEmployeeFacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventFacilityFacilityID",
                table: "Events",
                column: "EventFacilityFacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionRoomRoomID",
                table: "Transactions",
                column: "TransactionRoomRoomID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Facilities");
        }
    }
}
