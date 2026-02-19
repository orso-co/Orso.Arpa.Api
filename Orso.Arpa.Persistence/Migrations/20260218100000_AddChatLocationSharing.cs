using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChatLocationSharing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add message_type column to chat_messages
            migrationBuilder.AddColumn<int>(
                name: "message_type",
                table: "chat_messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Create chat_live_location_shares table
            migrationBuilder.CreateTable(
                name: "chat_live_location_shares",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chat_room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    accuracy = table.Column<double>(type: "double precision", nullable: true),
                    started_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    last_updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_live_location_shares", x => x.id);
                    table.ForeignKey(
                        name: "fk_chat_live_location_shares_chat_rooms_chat_room_id",
                        column: x => x.chat_room_id,
                        principalTable: "chat_rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chat_live_location_shares_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_chat_live_location_shares_chat_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "chat_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Indexes
            migrationBuilder.CreateIndex(
                name: "ix_chat_live_location_shares_chat_room_id_user_id_is_active",
                table: "chat_live_location_shares",
                columns: new[] { "chat_room_id", "user_id", "is_active" });

            migrationBuilder.CreateIndex(
                name: "ix_chat_live_location_shares_expires_at",
                table: "chat_live_location_shares",
                column: "expires_at");

            migrationBuilder.CreateIndex(
                name: "ix_chat_live_location_shares_message_id",
                table: "chat_live_location_shares",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "ix_chat_live_location_shares_user_id",
                table: "chat_live_location_shares",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_live_location_shares");

            migrationBuilder.DropColumn(
                name: "message_type",
                table: "chat_messages");
        }
    }
}
