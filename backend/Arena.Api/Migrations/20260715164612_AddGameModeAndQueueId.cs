using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arena.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddGameModeAndQueueId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameMode",
                table: "Matches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QueueId",
                table: "Matches",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameMode",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "QueueId",
                table: "Matches");
        }
    }
}
