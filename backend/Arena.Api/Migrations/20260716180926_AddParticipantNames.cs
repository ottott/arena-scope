using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arena.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipantNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameName",
                table: "Participants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TagLine",
                table: "Participants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "TagLine",
                table: "Participants");
        }
    }
}
