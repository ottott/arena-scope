using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arena.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipantItemsAndAugments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Augment1Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Augment2Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Augment3Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Augment4Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Augment5Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Augment6Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item0Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item1Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item2Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item3Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item4Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item5Id",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Augment1Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Augment2Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Augment3Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Augment4Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Augment5Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Augment6Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Item0Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Item1Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Item2Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Item3Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Item4Id",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Item5Id",
                table: "Participants");
        }
    }
}
