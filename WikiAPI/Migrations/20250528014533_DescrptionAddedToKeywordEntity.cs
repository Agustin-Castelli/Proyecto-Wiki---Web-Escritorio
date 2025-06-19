using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiAPI.Migrations
{
    /// <inheritdoc />
    public partial class DescrptionAddedToKeywordEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Keywords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Keywords");
        }
    }
}
