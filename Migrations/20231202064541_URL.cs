using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class URL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URl",
                table: "Facilities",
                newName: "URL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Facilities",
                newName: "URl");
        }
    }
}
