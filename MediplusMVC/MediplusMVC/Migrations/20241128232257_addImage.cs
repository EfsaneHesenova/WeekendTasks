using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediplusMVC.Migrations
{
    /// <inheritdoc />
    public partial class addImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "İmageUrl",
                table: "SliderItems",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "SliderItems",
                newName: "İmageUrl");
        }
    }
}
