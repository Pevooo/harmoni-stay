using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class jessy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Facilities",
                newName: "FacilityName");

            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "Guests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "EmployeeSalary",
                table: "Employees",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Facility",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FacilityEmployeeFacilityID",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "WorkingHours",
                table: "Employees",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "AccountEmloyeeEmployeeID",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_RoomID",
                table: "Guests",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FacilityEmployeeFacilityID",
                table: "Employees",
                column: "FacilityEmployeeFacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountEmloyeeEmployeeID",
                table: "Accounts",
                column: "AccountEmloyeeEmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_AccountEmloyeeEmployeeID",
                table: "Accounts",
                column: "AccountEmloyeeEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Facilities_FacilityEmployeeFacilityID",
                table: "Employees",
                column: "FacilityEmployeeFacilityID",
                principalTable: "Facilities",
                principalColumn: "FacilityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Rooms_RoomID",
                table: "Guests",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_AccountEmloyeeEmployeeID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Facilities_FacilityEmployeeFacilityID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Rooms_RoomID",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_RoomID",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Employees_FacilityEmployeeFacilityID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountEmloyeeEmployeeID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "EmployeeSalary",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Facility",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FacilityEmployeeFacilityID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "WorkingHours",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AccountEmloyeeEmployeeID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "FacilityName",
                table: "Facilities",
                newName: "Name");
        }
    }
}
