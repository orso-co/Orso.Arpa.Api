using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS user_settings (
                    user_id uuid NOT NULL,
                    is_dark_mode boolean NOT NULL DEFAULT true,
                    language character varying(10) DEFAULT 'de',
                    sound_on_user_online boolean NOT NULL DEFAULT false,
                    sound_on_announcement boolean NOT NULL DEFAULT false,
                    CONSTRAINT pk_user_settings PRIMARY KEY (user_id),
                    CONSTRAINT fk_user_settings_asp_net_users_user_id FOREIGN KEY (user_id)
                        REFERENCES ""AspNetUsers"" (id) ON DELETE CASCADE
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_settings");
        }
    }
}
