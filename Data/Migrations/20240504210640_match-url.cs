using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_new.Data.Migrations
{
    /// <inheritdoc />
    public partial class matchurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatchUrl",
                table: "Match",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchUrl",
                table: "Match");
        }
    }
}
