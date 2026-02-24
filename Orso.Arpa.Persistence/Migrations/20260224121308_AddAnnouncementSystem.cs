using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAnnouncementSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS announcement (
                    id uuid NOT NULL,
                    title character varying(200) NOT NULL,
                    content character varying(2000),
                    priority character varying(20) DEFAULT 'info',
                    link character varying(500),
                    link_text character varying(100),
                    active boolean NOT NULL,
                    valid_until timestamp without time zone,
                    sort_order integer NOT NULL DEFAULT 0,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL,
                    CONSTRAINT pk_announcement PRIMARY KEY (id)
                );
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS announcement_read (
                    id uuid NOT NULL,
                    read_at timestamp without time zone NOT NULL,
                    ticker_pinned boolean NOT NULL,
                    announcement_id uuid NOT NULL,
                    user_id uuid NOT NULL,
                    CONSTRAINT pk_announcement_read PRIMARY KEY (id),
                    CONSTRAINT fk_announcement_read_announcement_announcement_id FOREIGN KEY (announcement_id) REFERENCES announcement (id) ON DELETE CASCADE,
                    CONSTRAINT fk_announcement_read_users_user_id FOREIGN KEY (user_id) REFERENCES ""AspNetUsers"" (id) ON DELETE CASCADE
                );
            ");

            migrationBuilder.Sql(@"
                CREATE UNIQUE INDEX IF NOT EXISTS ix_announcement_read_announcement_id_user_id
                ON announcement_read (announcement_id, user_id);
            ");

            migrationBuilder.Sql(@"
                CREATE INDEX IF NOT EXISTS ix_announcement_read_user_id
                ON announcement_read (user_id);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "announcement_read");
            migrationBuilder.DropTable(name: "announcement");
        }
    }
}
