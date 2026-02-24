using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFinanceClubAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename announcement tables if they still have old names
            migrationBuilder.Sql(@"
                DO $$ BEGIN
                    IF EXISTS (SELECT FROM pg_tables WHERE tablename = 'announcement_read') THEN
                        ALTER TABLE announcement_read RENAME TO announcement_reads;
                    END IF;
                    IF EXISTS (SELECT FROM pg_tables WHERE tablename = 'announcement') THEN
                        ALTER TABLE announcement RENAME TO announcements;
                    END IF;
                END $$;
            ");

            // Rename indexes if they exist with old names
            migrationBuilder.Sql(@"
                DO $$ BEGIN
                    IF EXISTS (SELECT 1 FROM pg_indexes WHERE indexname = 'ix_announcement_read_user_id') THEN
                        ALTER INDEX ix_announcement_read_user_id RENAME TO ix_announcement_reads_user_id;
                    END IF;
                    IF EXISTS (SELECT 1 FROM pg_indexes WHERE indexname = 'ix_announcement_read_announcement_id_user_id') THEN
                        ALTER INDEX ix_announcement_read_announcement_id_user_id RENAME TO ix_announcement_reads_announcement_id_user_id;
                    END IF;
                END $$;
            ");

            // Rename constraints if needed
            migrationBuilder.Sql(@"
                DO $$ BEGIN
                    IF EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'pk_announcement_read') THEN
                        ALTER TABLE announcement_reads RENAME CONSTRAINT pk_announcement_read TO pk_announcement_reads;
                    END IF;
                    IF EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'pk_announcement') THEN
                        ALTER TABLE announcements RENAME CONSTRAINT pk_announcement TO pk_announcements;
                    END IF;
                    IF EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'fk_announcement_read_announcement_announcement_id') THEN
                        ALTER TABLE announcement_reads RENAME CONSTRAINT fk_announcement_read_announcement_announcement_id TO fk_announcement_reads_announcements_announcement_id;
                    END IF;
                    IF EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'fk_announcement_read_users_user_id') THEN
                        ALTER TABLE announcement_reads RENAME CONSTRAINT fk_announcement_read_users_user_id TO fk_announcement_reads_users_user_id;
                    END IF;
                END $$;
            ");

            // Add ClubId to organization_bank_accounts
            migrationBuilder.Sql(@"
                DO $$ BEGIN
                    IF NOT EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'organization_bank_accounts' AND column_name = 'club_id') THEN
                        ALTER TABLE organization_bank_accounts ADD COLUMN club_id uuid;
                    END IF;
                END $$;
            ");

            // Create finance_club_accesses table
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS finance_club_accesses (
                    id uuid NOT NULL,
                    user_id uuid NOT NULL,
                    club_id uuid NOT NULL,
                    granted_by character varying(200),
                    granted_at timestamp without time zone NOT NULL,
                    created_by character varying(110),
                    created_at timestamp without time zone NOT NULL,
                    modified_by character varying(110),
                    modified_at timestamp without time zone,
                    deleted boolean NOT NULL DEFAULT false,
                    CONSTRAINT pk_finance_club_accesses PRIMARY KEY (id),
                    CONSTRAINT fk_finance_club_accesses_clubs_club_id FOREIGN KEY (club_id) REFERENCES clubs(id) ON DELETE CASCADE,
                    CONSTRAINT fk_finance_club_accesses_users_user_id FOREIGN KEY (user_id) REFERENCES ""AspNetUsers""(id) ON DELETE CASCADE
                );
            ");

            // Create indexes
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS ix_organization_bank_accounts_club_id ON organization_bank_accounts (club_id);");
            migrationBuilder.Sql(@"CREATE INDEX IF NOT EXISTS ix_finance_club_accesses_club_id ON finance_club_accesses (club_id);");
            migrationBuilder.Sql(@"CREATE UNIQUE INDEX IF NOT EXISTS ix_finance_club_accesses_user_id_club_id ON finance_club_accesses (user_id, club_id);");

            // Add FK for organization_bank_accounts.club_id
            migrationBuilder.Sql(@"
                DO $$ BEGIN
                    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'fk_organization_bank_accounts_clubs_club_id') THEN
                        ALTER TABLE organization_bank_accounts ADD CONSTRAINT fk_organization_bank_accounts_clubs_club_id
                            FOREIGN KEY (club_id) REFERENCES clubs(id) ON DELETE SET NULL;
                    END IF;
                END $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_organization_bank_accounts_clubs_club_id",
                table: "organization_bank_accounts");

            migrationBuilder.DropTable(
                name: "finance_club_accesses");

            migrationBuilder.DropIndex(
                name: "ix_organization_bank_accounts_club_id",
                table: "organization_bank_accounts");

            migrationBuilder.DropColumn(
                name: "club_id",
                table: "organization_bank_accounts");
        }
    }
}
