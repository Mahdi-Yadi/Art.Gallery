using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Art.Gallery.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixphonesite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                schema: "ArtGallery",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "ArtGallery",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                schema: "ArtGallery",
                table: "SiteSettings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                schema: "ArtGallery",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
