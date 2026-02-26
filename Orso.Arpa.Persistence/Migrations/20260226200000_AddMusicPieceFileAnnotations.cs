using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMusicPieceFileAnnotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS music_piece_file_annotations (
                    id uuid NOT NULL,
                    music_piece_file_id uuid NOT NULL,
                    user_id uuid NOT NULL,
                    annotation_data jsonb,
                    modified_at timestamp without time zone NOT NULL,
                    CONSTRAINT pk_music_piece_file_annotations PRIMARY KEY (id),
                    CONSTRAINT fk_music_piece_file_annotations_music_piece_files_music_piece_file_id FOREIGN KEY (music_piece_file_id) REFERENCES music_piece_files (id) ON DELETE CASCADE,
                    CONSTRAINT fk_music_piece_file_annotations_users_user_id FOREIGN KEY (user_id) REFERENCES ""AspNetUsers"" (id) ON DELETE CASCADE
                );
            ");

            migrationBuilder.Sql(@"
                CREATE UNIQUE INDEX IF NOT EXISTS ix_music_piece_file_annotations_music_piece_file_id_user_id
                ON music_piece_file_annotations (music_piece_file_id, user_id);
            ");

            migrationBuilder.Sql(@"
                CREATE INDEX IF NOT EXISTS ix_music_piece_file_annotations_user_id
                ON music_piece_file_annotations (user_id);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "music_piece_file_annotations");
        }
    }
}
