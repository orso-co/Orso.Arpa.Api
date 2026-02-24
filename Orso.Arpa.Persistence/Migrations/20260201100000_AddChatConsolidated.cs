using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChatConsolidated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ========================================================
            // 1. chat_rooms (from AddChatTables)
            // ========================================================
            migrationBuilder.CreateTable(
                name: "chat_rooms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    project_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_message_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_rooms", x => x.id);
                    table.ForeignKey(
                        name: "fk_chat_rooms_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            // ========================================================
            // 2. chat_room_members (from AddChatTables)
            // ========================================================
            migrationBuilder.CreateTable(
                name: "chat_room_members",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chat_room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    joined_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    last_read_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    history_visible_from = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    is_muted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_room_members", x => x.id);
                    table.ForeignKey(
                        name: "fk_chat_room_members_chat_rooms_chat_room_id",
                        column: x => x.chat_room_id,
                        principalTable: "chat_rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chat_room_members_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // ========================================================
            // 3. chat_messages (from AddChatTables + AddChatLocationSharing)
            //    Includes message_type column from the start
            // ========================================================
            migrationBuilder.CreateTable(
                name: "chat_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chat_room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    message_type = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    sent_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    edited_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    reply_to_message_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_chat_messages_chat_rooms_chat_room_id",
                        column: x => x.chat_room_id,
                        principalTable: "chat_rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chat_messages_users_sender_id",
                        column: x => x.sender_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_chat_messages_chat_messages_reply_to_message_id",
                        column: x => x.reply_to_message_id,
                        principalTable: "chat_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            // ========================================================
            // 4. chat_message_attachments (from AddChatTables)
            // ========================================================
            migrationBuilder.CreateTable(
                name: "chat_message_attachments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    chat_message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    storage_path = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    content_type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    thumbnail_path = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_message_attachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_chat_message_attachments_chat_messages_chat_message_id",
                        column: x => x.chat_message_id,
                        principalTable: "chat_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // ========================================================
            // 5. message_reactions (from AddChatTables)
            // ========================================================
            migrationBuilder.CreateTable(
                name: "message_reactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    emoji = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_reactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_reactions_chat_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "chat_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_reactions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // ========================================================
            // 6. message_read_receipts (from AddChatTables)
            // ========================================================
            migrationBuilder.CreateTable(
                name: "message_read_receipts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    read_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_read_receipts", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_read_receipts_chat_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "chat_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_read_receipts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // ========================================================
            // 7. chat_folders (from AddChatFolders)
            // ========================================================
            migrationBuilder.CreateTable(
                name: "chat_folders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_system = table.Column<bool>(type: "boolean", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: true),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_folders", x => x.id);
                    table.ForeignKey(
                        name: "fk_chat_folders_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chat_folders_chat_folders_parent_id",
                        column: x => x.parent_id,
                        principalTable: "chat_folders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // ========================================================
            // 8. chat_folder_room_assignments (from AddChatFolders)
            // ========================================================
            migrationBuilder.CreateTable(
                name: "chat_folder_room_assignments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    folder_id = table.Column<Guid>(type: "uuid", nullable: false),
                    chat_room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chat_folder_room_assignments", x => x.id);
                    table.ForeignKey(
                        name: "fk_chat_folder_room_assignments_chat_folders_folder_id",
                        column: x => x.folder_id,
                        principalTable: "chat_folders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chat_folder_room_assignments_chat_rooms_chat_room_id",
                        column: x => x.chat_room_id,
                        principalTable: "chat_rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chat_folder_room_assignments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // ========================================================
            // 9. chat_live_location_shares (from AddChatLocationSharing)
            // ========================================================
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
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_chat_live_location_shares_chat_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "chat_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // ========================================================
            // INDEXES: chat_rooms
            // ========================================================
            migrationBuilder.CreateIndex(
                name: "ix_chat_rooms_project_id",
                table: "chat_rooms",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_chat_rooms_last_message_at",
                table: "chat_rooms",
                column: "last_message_at");

            // ========================================================
            // INDEXES: chat_room_members
            // ========================================================
            migrationBuilder.CreateIndex(
                name: "ix_chat_room_members_chat_room_id_user_id",
                table: "chat_room_members",
                columns: new[] { "chat_room_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_chat_room_members_user_id",
                table: "chat_room_members",
                column: "user_id");

            // ========================================================
            // INDEXES: chat_messages
            // ========================================================
            migrationBuilder.CreateIndex(
                name: "ix_chat_messages_chat_room_id_sent_at",
                table: "chat_messages",
                columns: new[] { "chat_room_id", "sent_at" });

            migrationBuilder.CreateIndex(
                name: "ix_chat_messages_sender_id",
                table: "chat_messages",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_chat_messages_reply_to_message_id",
                table: "chat_messages",
                column: "reply_to_message_id");

            // ========================================================
            // INDEXES: chat_message_attachments
            // ========================================================
            migrationBuilder.CreateIndex(
                name: "ix_chat_message_attachments_chat_message_id",
                table: "chat_message_attachments",
                column: "chat_message_id");

            // ========================================================
            // INDEXES: message_reactions
            // ========================================================
            migrationBuilder.CreateIndex(
                name: "ix_message_reactions_message_id_user_id_emoji",
                table: "message_reactions",
                columns: new[] { "message_id", "user_id", "emoji" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_message_reactions_message_id",
                table: "message_reactions",
                column: "message_id");

            // ========================================================
            // INDEXES: message_read_receipts
            // ========================================================
            migrationBuilder.CreateIndex(
                name: "ix_message_read_receipts_message_id_user_id",
                table: "message_read_receipts",
                columns: new[] { "message_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_message_read_receipts_message_id",
                table: "message_read_receipts",
                column: "message_id");

            // ========================================================
            // INDEXES: chat_folders
            // ========================================================
            migrationBuilder.CreateIndex(
                name: "ix_chat_folders_owner_id",
                table: "chat_folders",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_chat_folders_is_system",
                table: "chat_folders",
                column: "is_system");

            migrationBuilder.CreateIndex(
                name: "ix_chat_folders_parent_id",
                table: "chat_folders",
                column: "parent_id");

            // ========================================================
            // INDEXES: chat_folder_room_assignments
            // ========================================================
            migrationBuilder.CreateIndex(
                name: "ix_chat_folder_room_assignments_folder_id_chat_room_id_user_id",
                table: "chat_folder_room_assignments",
                columns: new[] { "folder_id", "chat_room_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_chat_folder_room_assignments_folder_id",
                table: "chat_folder_room_assignments",
                column: "folder_id");

            migrationBuilder.CreateIndex(
                name: "ix_chat_folder_room_assignments_chat_room_id",
                table: "chat_folder_room_assignments",
                column: "chat_room_id");

            migrationBuilder.CreateIndex(
                name: "ix_chat_folder_room_assignments_user_id",
                table: "chat_folder_room_assignments",
                column: "user_id");

            // ========================================================
            // INDEXES: chat_live_location_shares
            // ========================================================
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

            migrationBuilder.DropTable(
                name: "chat_folder_room_assignments");

            migrationBuilder.DropTable(
                name: "chat_folders");

            migrationBuilder.DropTable(
                name: "message_read_receipts");

            migrationBuilder.DropTable(
                name: "message_reactions");

            migrationBuilder.DropTable(
                name: "chat_message_attachments");

            migrationBuilder.DropTable(
                name: "chat_messages");

            migrationBuilder.DropTable(
                name: "chat_room_members");

            migrationBuilder.DropTable(
                name: "chat_rooms");
        }
    }
}
