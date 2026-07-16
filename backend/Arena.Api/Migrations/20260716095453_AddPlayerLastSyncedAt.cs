using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arena.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerLastSyncedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncedAt",
                table: "Players",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSyncedAt",
                table: "Players");
        }
    }
}
