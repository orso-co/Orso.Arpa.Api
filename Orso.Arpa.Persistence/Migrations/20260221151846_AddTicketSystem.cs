using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Removed: image_file_name on news (already in AddNewsImageFileName)
            // Removed: message_type on chat_messages (added via SQL on environments that need it)
            // Removed: chat_live_location_shares table (added via SQL on environments that need it)

            // Add message_type only if it doesn't exist yet
            migrationBuilder.Sql(@"
                DO $$ BEGIN
                    IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='chat_messages' AND column_name='message_type') THEN
                        ALTER TABLE chat_messages ADD COLUMN message_type integer NOT NULL DEFAULT 0;
                    END IF;
                END $$;
            ");

            // Create chat_live_location_shares only if it doesn't exist yet
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS chat_live_location_shares (
                    id uuid NOT NULL PRIMARY KEY,
                    chat_room_id uuid NOT NULL REFERENCES chat_rooms(id) ON DELETE CASCADE,
                    user_id uuid NOT NULL REFERENCES ""AspNetUsers""(id),
                    message_id uuid NOT NULL REFERENCES chat_messages(id) ON DELETE CASCADE,
                    latitude double precision NOT NULL,
                    longitude double precision NOT NULL,
                    accuracy double precision,
                    started_at timestamp without time zone NOT NULL,
                    expires_at timestamp without time zone NOT NULL,
                    last_updated_at timestamp without time zone NOT NULL,
                    is_active boolean NOT NULL,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL
                );
                CREATE INDEX IF NOT EXISTS ix_chat_live_location_shares_chat_room_id_user_id_is_active ON chat_live_location_shares (chat_room_id, user_id, is_active);
                CREATE INDEX IF NOT EXISTS ix_chat_live_location_shares_expires_at ON chat_live_location_shares (expires_at);
                CREATE INDEX IF NOT EXISTS ix_chat_live_location_shares_message_id ON chat_live_location_shares (message_id);
                CREATE INDEX IF NOT EXISTS ix_chat_live_location_shares_user_id ON chat_live_location_shares (user_id);
            ");

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValue: "Open"),
                    admin_priority = table.Column<int>(type: "integer", nullable: true),
                    effort = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    estimated_minutes = table.Column<int>(type: "integer", nullable: true),
                    spent_minutes = table.Column<int>(type: "integer", nullable: true),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tickets", x => x.id);
                    table.ForeignKey(
                        name: "fk_tickets_users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ticket_links",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: false),
                    label = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    url = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_links", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_links_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    sent_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_messages_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_messages_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ticket_read_statuses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_read_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_read_statuses", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_read_statuses_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_read_statuses_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ticket_votes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_votes", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_votes_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_votes_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ticket_attachments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    content_type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    storage_path = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: true),
                    message_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_attachments", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_attachments_ticket_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "ticket_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_attachments_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ticket_reactions",
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
                    table.PrimaryKey("pk_ticket_reactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_reactions_ticket_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "ticket_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ticket_reactions_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            // chat_live_location_shares indexes already created in SQL block above

            migrationBuilder.CreateIndex(
                name: "ix_ticket_attachments_message_id",
                table: "ticket_attachments",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_attachments_ticket_id",
                table: "ticket_attachments",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_links_ticket_id",
                table: "ticket_links",
                column: "ticket_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_messages_ticket_id_sent_at",
                table: "ticket_messages",
                columns: new[] { "ticket_id", "sent_at" });

            migrationBuilder.CreateIndex(
                name: "ix_ticket_messages_user_id",
                table: "ticket_messages",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_reactions_message_id_user_id_emoji",
                table: "ticket_reactions",
                columns: new[] { "message_id", "user_id", "emoji" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ticket_reactions_user_id",
                table: "ticket_reactions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_read_statuses_ticket_id_user_id",
                table: "ticket_read_statuses",
                columns: new[] { "ticket_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ticket_read_statuses_user_id",
                table: "ticket_read_statuses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_votes_ticket_id_user_id",
                table: "ticket_votes",
                columns: new[] { "ticket_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ticket_votes_user_id",
                table: "ticket_votes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_tickets_creator_id",
                table: "tickets",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_tickets_status",
                table: "tickets",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_tickets_type",
                table: "tickets",
                column: "type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chat_live_location_shares");

            migrationBuilder.DropTable(
                name: "ticket_attachments");

            migrationBuilder.DropTable(
                name: "ticket_links");

            migrationBuilder.DropTable(
                name: "ticket_reactions");

            migrationBuilder.DropTable(
                name: "ticket_read_statuses");

            migrationBuilder.DropTable(
                name: "ticket_votes");

            migrationBuilder.DropTable(
                name: "ticket_messages");

            migrationBuilder.DropTable(
                name: "tickets");

            // image_file_name on news removed — already in AddNewsImageFileName migration

            migrationBuilder.DropColumn(
                name: "message_type",
                table: "chat_messages");
        }
    }
}
