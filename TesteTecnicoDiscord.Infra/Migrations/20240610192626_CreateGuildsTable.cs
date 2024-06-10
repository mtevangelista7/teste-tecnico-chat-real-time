using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteTecnicoDiscord.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CreateGuildsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MembersCount",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MessagesCount",
                table: "Guilds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Guilds",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerUserId",
                table: "Guilds",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_OwnerUserId",
                table: "Guilds",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_Users_OwnerUserId",
                table: "Guilds",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_Users_OwnerUserId",
                table: "Guilds");

            migrationBuilder.DropIndex(
                name: "IX_Guilds_OwnerUserId",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "MembersCount",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "MessagesCount",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Guilds");
        }
    }
}
