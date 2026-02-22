using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMusicPieceParentChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "parent_id",
                table: "music_pieces",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_music_pieces_parent_id",
                table: "music_pieces",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "fk_music_pieces_music_pieces_parent_id",
                table: "music_pieces",
                column: "parent_id",
                principalTable: "music_pieces",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_music_pieces_music_pieces_parent_id",
                table: "music_pieces");

            migrationBuilder.DropIndex(
                name: "ix_music_pieces_parent_id",
                table: "music_pieces");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "music_pieces");
        }
    }
}
