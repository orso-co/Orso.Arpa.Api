using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class RemovedIsProfessionalFromMusicianProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProfessional",
                table: "MusicianProfiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProfessional",
                table: "MusicianProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
