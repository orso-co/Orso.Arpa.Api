using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChatFolders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create chat_folders table
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chat_folders_chat_folders_parent_id",
                        column: x => x.parent_id,
                        principalTable: "chat_folders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create chat_folder_room_assignments table
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Indexes for chat_folders
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

            // Indexes for chat_folder_room_assignments
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_folder_room_assignments");

            migrationBuilder.DropTable(
                name: "chat_folders");
        }
    }
}
