using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Art.Gallery.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateuserAddActiveCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActiveCode",
                schema: "ArtGallery",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveCode",
                schema: "ArtGallery",
                table: "Users");
        }
    }
}
