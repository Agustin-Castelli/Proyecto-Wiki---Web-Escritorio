using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByUserToArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUser",
                table: "Articles",
                type: "varchar(60)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUser",
                table: "Articles");
        }
    }
}
