using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "Facilities");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Facilities",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Facilities");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
