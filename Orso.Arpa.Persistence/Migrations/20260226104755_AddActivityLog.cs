using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS activity_logs (
                    id uuid NOT NULL,
                    created_at timestamp without time zone NOT NULL,
                    user_id uuid NOT NULL,
                    username character varying(256) NOT NULL,
                    action character varying(100) NOT NULL,
                    category character varying(100) NOT NULL,
                    entity_type character varying(100),
                    entity_id uuid,
                    entity_label character varying(500),
                    path character varying(2000),
                    metadata jsonb,
                    CONSTRAINT pk_activity_logs PRIMARY KEY (id)
                );

                CREATE INDEX IF NOT EXISTS ix_activity_logs_action_category ON activity_logs (action, category);
                CREATE INDEX IF NOT EXISTS ix_activity_logs_created_at ON activity_logs (created_at);
                CREATE INDEX IF NOT EXISTS ix_activity_logs_user_id ON activity_logs (user_id);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activity_logs");
        }
    }
}
