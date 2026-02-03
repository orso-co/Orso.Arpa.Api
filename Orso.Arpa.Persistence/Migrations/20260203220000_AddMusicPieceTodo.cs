using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMusicPieceTodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicPieceTodos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MusicPieceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPieceTodos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicPieceTodos_MusicPieces_MusicPieceId",
                        column: x => x.MusicPieceId,
                        principalTable: "MusicPieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicPieceTodoAssignee",
                columns: table => new
                {
                    MusicPieceTodoId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPieceTodoAssignee", x => new { x.MusicPieceTodoId, x.UserId });
                    table.ForeignKey(
                        name: "FK_MusicPieceTodoAssignee_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicPieceTodoAssignee_MusicPieceTodos_MusicPieceTodoId",
                        column: x => x.MusicPieceTodoId,
                        principalTable: "MusicPieceTodos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicPieceTodos_DueDate",
                table: "MusicPieceTodos",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPieceTodos_IsCompleted",
                table: "MusicPieceTodos",
                column: "IsCompleted");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPieceTodos_MusicPieceId",
                table: "MusicPieceTodos",
                column: "MusicPieceId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPieceTodoAssignee_UserId",
                table: "MusicPieceTodoAssignee",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicPieceTodoAssignee");

            migrationBuilder.DropTable(
                name: "MusicPieceTodos");
        }
    }
}
