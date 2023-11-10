using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GustRoomType",
                table: "Guests",
                newName: "GuestRoomType");

            migrationBuilder.AddColumn<string>(
                name: "GuestNationality",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuestPhoneNumber",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestNationality",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "GuestPhoneNumber",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "GuestRoomType",
                table: "Guests",
                newName: "GustRoomType");
        }
    }
}
