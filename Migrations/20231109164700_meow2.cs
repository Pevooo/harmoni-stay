using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class meow2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Events_EventsEventID",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Transactions_RoomTransactionTransactionID",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomTransactionTransactionID",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_EventsEventID",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "RoomTransactionTransactionID",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "EventsEventID",
                table: "Facilities");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Facilities",
                newName: "FacilityWorkStart");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Facilities",
                newName: "FacilityWorkEnd");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Events",
                newName: "EventStart");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Events",
                newName: "EventFee");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Events",
                newName: "EventEnd");

            migrationBuilder.AddColumn<int>(
                name: "TransactionRoomRoomID",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventFacilityFacilityID",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionRoomRoomID",
                table: "Transactions",
                column: "TransactionRoomRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventFacilityFacilityID",
                table: "Events",
                column: "EventFacilityFacilityID");

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
                name: "FK_Events_Facilities_EventFacilityFacilityID",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Rooms_TransactionRoomRoomID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionRoomRoomID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventFacilityFacilityID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TransactionRoomRoomID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "EventFacilityFacilityID",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "FacilityWorkStart",
                table: "Facilities",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "FacilityWorkEnd",
                table: "Facilities",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "EventStart",
                table: "Events",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EventFee",
                table: "Events",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "EventEnd",
                table: "Events",
                newName: "End");

            migrationBuilder.AddColumn<int>(
                name: "RoomTransactionTransactionID",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventsEventID",
                table: "Facilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTransactionTransactionID",
                table: "Rooms",
                column: "RoomTransactionTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_EventsEventID",
                table: "Facilities",
                column: "EventsEventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Events_EventsEventID",
                table: "Facilities",
                column: "EventsEventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Transactions_RoomTransactionTransactionID",
                table: "Rooms",
                column: "RoomTransactionTransactionID",
                principalTable: "Transactions",
                principalColumn: "TransactionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
