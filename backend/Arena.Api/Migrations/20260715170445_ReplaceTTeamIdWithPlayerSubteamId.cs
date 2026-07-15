using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arena.Api.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceTTeamIdWithPlayerSubteamId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Participants",
                newName: "PlayerSubteamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerSubteamId",
                table: "Participants",
                newName: "TeamId");
        }
    }
}
