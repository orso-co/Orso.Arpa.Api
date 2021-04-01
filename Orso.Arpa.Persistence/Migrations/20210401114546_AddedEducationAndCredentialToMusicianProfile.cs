using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedEducationAndCredentialToMusicianProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfileSection_MusicianProfiles_MusicianProfileId",
                table: "MusicianProfileSection");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfileSection_Sections_SectionId",
                table: "MusicianProfileSection");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSection_Persons_PersonId",
                table: "PersonSection");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSection_Sections_SectionId",
                table: "PersonSection");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonSection",
                table: "PersonSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MusicianProfileSection",
                table: "MusicianProfileSection");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "PersonSection",
                newName: "PersonSections");

            migrationBuilder.RenameTable(
                name: "MusicianProfileSection",
                newName: "MusicianProfileSections");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonSection_SectionId",
                table: "PersonSections",
                newName: "IX_PersonSections_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicianProfileSection_SectionId",
                table: "MusicianProfileSections",
                newName: "IX_MusicianProfileSections_SectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonSections",
                table: "PersonSections",
                columns: new[] { "PersonId", "SectionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MusicianProfileSections",
                table: "MusicianProfileSections",
                columns: new[] { "MusicianProfileId", "SectionId" });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timespan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Keyword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SortOrder = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timespan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Institution = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SortOrder = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusicianProfileCredentials",
                columns: table => new
                {
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicianProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianProfileCredentials", x => new { x.MusicianProfileId, x.CredentialId });
                    table.ForeignKey(
                        name: "FK_MusicianProfileCredentials_Credentials_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianProfileCredentials_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicianProfileEducations",
                columns: table => new
                {
                    EducationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicianProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianProfileEducations", x => new { x.MusicianProfileId, x.EducationId });
                    table.ForeignKey(
                        name: "FK_MusicianProfileEducations_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianProfileEducations_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfileCredentials_CredentialId",
                table: "MusicianProfileCredentials",
                column: "CredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfileEducations_EducationId",
                table: "MusicianProfileEducations",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfileSections_MusicianProfiles_MusicianProfileId",
                table: "MusicianProfileSections",
                column: "MusicianProfileId",
                principalTable: "MusicianProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfileSections_Sections_SectionId",
                table: "MusicianProfileSections",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSections_Persons_PersonId",
                table: "PersonSections",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSections_Sections_SectionId",
                table: "PersonSections",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfileSections_MusicianProfiles_MusicianProfileId",
                table: "MusicianProfileSections");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfileSections_Sections_SectionId",
                table: "MusicianProfileSections");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSections_Persons_PersonId",
                table: "PersonSections");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSections_Sections_SectionId",
                table: "PersonSections");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "MusicianProfileCredentials");

            migrationBuilder.DropTable(
                name: "MusicianProfileEducations");

            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonSections",
                table: "PersonSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MusicianProfileSections",
                table: "MusicianProfileSections");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "PersonSections",
                newName: "PersonSection");

            migrationBuilder.RenameTable(
                name: "MusicianProfileSections",
                newName: "MusicianProfileSection");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonSections_SectionId",
                table: "PersonSection",
                newName: "IX_PersonSection_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicianProfileSections_SectionId",
                table: "MusicianProfileSection",
                newName: "IX_MusicianProfileSection_SectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonSection",
                table: "PersonSection",
                columns: new[] { "PersonId", "SectionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MusicianProfileSection",
                table: "MusicianProfileSection",
                columns: new[] { "MusicianProfileId", "SectionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfileSection_MusicianProfiles_MusicianProfileId",
                table: "MusicianProfileSection",
                column: "MusicianProfileId",
                principalTable: "MusicianProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfileSection_Sections_SectionId",
                table: "MusicianProfileSection",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSection_Persons_PersonId",
                table: "PersonSection",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSection_Sections_SectionId",
                table: "PersonSection",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
