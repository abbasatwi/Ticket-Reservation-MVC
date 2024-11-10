using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_new.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateTeams2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Stadium_CaptainId",
                table: "Teams");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_StadiumId",
                table: "Teams",
                column: "StadiumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Stadium_StadiumId",
                table: "Teams",
                column: "StadiumId",
                principalTable: "Stadium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Stadium_StadiumId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_StadiumId",
                table: "Teams");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Stadium_CaptainId",
                table: "Teams",
                column: "CaptainId",
                principalTable: "Stadium",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
