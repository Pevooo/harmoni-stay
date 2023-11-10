using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class meow8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_AccountEmloyeeEmployeeID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "GuestRoomType",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "AccountEmloyeeEmployeeID",
                table: "Accounts",
                newName: "AccountEmployeeEmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_AccountEmloyeeEmployeeID",
                table: "Accounts",
                newName: "IX_Accounts_AccountEmployeeEmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_AccountEmployeeEmployeeID",
                table: "Accounts",
                column: "AccountEmployeeEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_AccountEmployeeEmployeeID",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "AccountEmployeeEmployeeID",
                table: "Accounts",
                newName: "AccountEmloyeeEmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_AccountEmployeeEmployeeID",
                table: "Accounts",
                newName: "IX_Accounts_AccountEmloyeeEmployeeID");

            migrationBuilder.AddColumn<string>(
                name: "GuestRoomType",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_AccountEmloyeeEmployeeID",
                table: "Accounts",
                column: "AccountEmloyeeEmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
