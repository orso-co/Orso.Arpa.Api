using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTodoFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "todo_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    completed_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    estimated_time = table.Column<int>(type: "integer", nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    assignee_id = table.Column<Guid>(type: "uuid", nullable: true),
                    parent_todo_id = table.Column<Guid>(type: "uuid", nullable: true),
                    entity_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: true),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: true),
                    project_id = table.Column<Guid>(type: "uuid", nullable: true),
                    appointment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    chat_room_id = table.Column<Guid>(type: "uuid", nullable: true),
                    source_chat_message_id = table.Column<Guid>(type: "uuid", nullable: true),
                    reminder_days_before = table.Column<int>(type: "integer", nullable: true),
                    reminder_sent_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_todo_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_todo_items_appointments_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "appointments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_todo_items_chat_messages_source_chat_message_id",
                        column: x => x.source_chat_message_id,
                        principalTable: "chat_messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_todo_items_chat_rooms_chat_room_id",
                        column: x => x.chat_room_id,
                        principalTable: "chat_rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_todo_items_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_todo_items_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_todo_items_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_todo_items_todo_items_parent_todo_id",
                        column: x => x.parent_todo_id,
                        principalTable: "todo_items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_todo_items_users_assignee_id",
                        column: x => x.assignee_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_todo_items_users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "todo_comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    todo_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    posted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_todo_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_todo_comments_todo_items_todo_item_id",
                        column: x => x.todo_item_id,
                        principalTable: "todo_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_todo_comments_users_author_id",
                        column: x => x.author_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "todo_dependencies",
                columns: table => new
                {
                    dependent_task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    depends_on_task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lag_days = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_todo_dependencies", x => new { x.dependent_task_id, x.depends_on_task_id });
                    table.ForeignKey(
                        name: "fk_todo_dependencies_todo_items_dependent_task_id",
                        column: x => x.dependent_task_id,
                        principalTable: "todo_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_todo_dependencies_todo_items_depends_on_task_id",
                        column: x => x.depends_on_task_id,
                        principalTable: "todo_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_todo_comments_author_id",
                table: "todo_comments",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_comments_todo_item_id",
                table: "todo_comments",
                column: "todo_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_dependencies_depends_on_task_id",
                table: "todo_dependencies",
                column: "depends_on_task_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_appointment_id",
                table: "todo_items",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_assignee_id",
                table: "todo_items",
                column: "assignee_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_chat_room_id",
                table: "todo_items",
                column: "chat_room_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_creator_id",
                table: "todo_items",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_due_date",
                table: "todo_items",
                column: "due_date");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_entity_type",
                table: "todo_items",
                column: "entity_type");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_organization_id",
                table: "todo_items",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_parent_todo_id",
                table: "todo_items",
                column: "parent_todo_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_person_id",
                table: "todo_items",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_priority",
                table: "todo_items",
                column: "priority");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_project_id",
                table: "todo_items",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_source_chat_message_id",
                table: "todo_items",
                column: "source_chat_message_id");

            migrationBuilder.CreateIndex(
                name: "ix_todo_items_status",
                table: "todo_items",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "todo_comments");

            migrationBuilder.DropTable(
                name: "todo_dependencies");

            migrationBuilder.DropTable(
                name: "todo_items");
        }
    }
}
