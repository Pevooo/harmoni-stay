using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Facilities_FacilityEmployeeFacilityID",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "FacilityEmployeeFacilityID",
                table: "Employees",
                newName: "EmployeeFacilityFacilityID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_FacilityEmployeeFacilityID",
                table: "Employees",
                newName: "IX_Employees_EmployeeFacilityFacilityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Facilities_EmployeeFacilityFacilityID",
                table: "Employees",
                column: "EmployeeFacilityFacilityID",
                principalTable: "Facilities",
                principalColumn: "FacilityID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Facilities_EmployeeFacilityFacilityID",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmployeeFacilityFacilityID",
                table: "Employees",
                newName: "FacilityEmployeeFacilityID");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeFacilityFacilityID",
                table: "Employees",
                newName: "IX_Employees_FacilityEmployeeFacilityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Facilities_FacilityEmployeeFacilityID",
                table: "Employees",
                column: "FacilityEmployeeFacilityID",
                principalTable: "Facilities",
                principalColumn: "FacilityID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
