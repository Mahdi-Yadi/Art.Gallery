using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Art.Gallery.Data.Migrations
{
    /// <inheritdoc />
    public partial class addiscomplete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                schema: "ArtGallery",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                schema: "ArtGallery",
                table: "Orders");
        }
    }
}
