using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AdditionalPropertiesForMusicianProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_SelectValueMappings_TypeId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_Appointments_AppointmentId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_Persons_PersonId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_PredictionId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ResultId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentRooms_Appointments_AppointmentId",
                table: "AppointmentRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentRooms_Rooms_RoomId",
                table: "AppointmentRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_CategoryId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentPatternId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_ExpectationId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_StatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Persons_PersonId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Persons_PersonId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Sections_InstrumentId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_QualificationId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSection_Persons_PersonId",
                table: "PersonSection");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSection_Sections_SectionId",
                table: "PersonSection");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAppointments_Appointments_AppointmentId",
                table: "ProjectAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAppointments_Projects_ProjectId",
                table: "ProjectAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParticipations_MusicianProfiles_MusicianProfileId",
                table: "ProjectParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParticipations_Projects_ProjectId",
                table: "ProjectParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Projects_ParentId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Venues_VenueId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionAppointments_Appointments_AppointmentId",
                table: "SectionAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionAppointments_Sections_SectionId",
                table: "SectionAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Sections_ParentId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectValueMappings_SelectValueCategories_SelectValueCategoryId",
                table: "SelectValueMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectValueMappings_SelectValues_SelectValueId",
                table: "SelectValueMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_UrlRoles_AspNetRoles_RoleId",
                table: "UrlRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UrlRoles_Urls_UrlId",
                table: "UrlRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Urls_Projects_ProjectId",
                table: "Urls");

            migrationBuilder.DropForeignKey(
                name: "FK_Venues_Addresses_AddressId",
                table: "Venues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonSection",
                table: "PersonSection");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "PersonSection",
                newName: "PersonSections");

            migrationBuilder.RenameColumn(
                name: "IsProfessional",
                table: "MusicianProfiles",
                newName: "IsMainProfile");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonSection_SectionId",
                table: "PersonSections",
                newName: "IX_PersonSections_SectionId");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Venues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Venues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Urls",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Urls",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "UrlRoles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UrlRoles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "SelectValues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "SelectValues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "SelectValueMappings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "SelectValueMappings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "SelectValueCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "SelectValueCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Sections",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Sections",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "SectionAppointments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "SectionAppointments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Rooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Rooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Regions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Regions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ProjectParticipations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProjectParticipations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ProjectAppointments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProjectAppointments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Persons",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Persons",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "MusicianProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "MusicianProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "MusicianProfiles",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ExperienceLevel",
                table: "MusicianProfiles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "InqueryId",
                table: "MusicianProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "LevelInnerASsessment",
                table: "MusicianProfiles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "LevelSelfAssessment",
                table: "MusicianProfiles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredPositionId",
                table: "MusicianProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ProfileFavorizitation",
                table: "MusicianProfiles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "SalaryId",
                table: "MusicianProfiles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Appointments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Appointments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuditionId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "AppointmentRooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AppointmentRooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "AppointmentParticipations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AppointmentParticipations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "PersonSections",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PersonSections",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonSections",
                table: "PersonSections",
                columns: new[] { "PersonId", "SectionId" });

            migrationBuilder.CreateTable(
                name: "Auditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RepetitorStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InnerComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InternalComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Repertoire = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auditions_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Auditions_SelectValueMappings_RepetitorStatusId",
                        column: x => x.RepetitorStatusId,
                        principalTable: "SelectValueMappings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Auditions_SelectValueMappings_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SelectValueMappings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AvailableDocuments",
                columns: table => new
                {
                    SelectValueMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicianProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableDocuments", x => new { x.MusicianProfileId, x.SelectValueMappingId });
                    table.ForeignKey(
                        name: "FK_AvailableDocuments_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AvailableDocuments_SelectValueMappings_SelectValueMappingId",
                        column: x => x.SelectValueMappingId,
                        principalTable: "SelectValueMappings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timespan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Keyword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SortOrder = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusicianProfileSections",
                columns: table => new
                {
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicianProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianProfileSections", x => new { x.MusicianProfileId, x.SectionId });
                    table.ForeignKey(
                        name: "FK_MusicianProfileSections_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MusicianProfileSections_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreferredGenre",
                columns: table => new
                {
                    SelectValueMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicianProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferredGenre", x => new { x.MusicianProfileId, x.SelectValueMappingId });
                    table.ForeignKey(
                        name: "FK_PreferredGenre_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PreferredGenre_SelectValueMappings_SelectValueMappingId",
                        column: x => x.SelectValueMappingId,
                        principalTable: "SelectValueMappings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SphereOfActivityConcerts",
                columns: table => new
                {
                    VenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicianProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<byte>(type: "tinyint", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SphereOfActivityConcerts", x => new { x.MusicianProfileId, x.VenueId });
                    table.ForeignKey(
                        name: "FK_SphereOfActivityConcerts_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SphereOfActivityConcerts_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SphereOfActivityRehearsals",
                columns: table => new
                {
                    VenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicianProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<byte>(type: "tinyint", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SphereOfActivityRehearsals", x => new { x.MusicianProfileId, x.VenueId });
                    table.ForeignKey(
                        name: "FK_SphereOfActivityRehearsals_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SphereOfActivityRehearsals_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MusicianProfileCredentials",
                columns: table => new
                {
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicianProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MusicianProfileCredentials_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MusicianProfileEducations",
                columns: table => new
                {
                    EducationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicianProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MusicianProfileEducations_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "SelectValueCategories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "Property", "Table" },
                values: new object[,]
                {
                    { new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Repetitor status", "RepetitorStatus", "Audition" },
                    { new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Status", "Status", "Audition" },
                    { new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Available document status", "AvailableDocumentStatus", "MusicianProfile" },
                    { new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Inquery", "Inquery", "MusicianProfile" },
                    { new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Salary", "Salary", "MusicianProfile" }
                });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[,]
                {
                    { new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("35d63f30-8704-47d5-865a-ee713a082433") },
                    { new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("e20ff004-aafc-4e28-87f9-0d9c6372951c") },
                    { new Guid("f036bca9-95d4-4526-b845-fff9208ab103"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1") },
                    { new Guid("30f592f6-485a-468a-bfb2-4854be733e74"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("f52b9170-c6f6-4828-b96c-df5dfbe9bd73") }
                });

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("3550443d-5acf-4159-bd59-d7da04dd9434"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Audio" },
                    { new Guid("d075dda3-ba29-472b-a699-1f92c1af13a9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Video" },
                    { new Guid("e340f76d-074b-40e8-85b0-1bb66a596a06"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Photo" },
                    { new Guid("c1951202-0e6e-41f7-bf07-5cefe47efade"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Diploma" },
                    { new Guid("0cf5b2e2-4f01-441a-adc8-a975c7494fd7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Letter of recommendation" },
                    { new Guid("c0911d95-0c6d-4834-840c-43cddf3c51a0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "CV" },
                    { new Guid("6307ec0e-482a-4777-8b2e-4e8cd5d1f252"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Unnecessary" },
                    { new Guid("6bdf5666-65ef-475a-9c48-9a38f18de041"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "No pianist needed" },
                    { new Guid("45d534e3-6605-42f0-ae57-1a943e18a9cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Is asking for pianist" },
                    { new Guid("42f546ab-1b96-4eab-88a4-753cad8392c1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Awaiting" },
                    { new Guid("33e57595-2166-4cce-aa34-60d7148ae9f7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Failed" },
                    { new Guid("166edc65-9915-4836-b0a3-3c60ad0bcc04"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Passed" },
                    { new Guid("2ecfb104-feb3-406a-b741-0ac9fdd3e8d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Contemporary Music" },
                    { new Guid("982a9947-c6f8-4c9a-b96f-2a4825a11496"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Dance Performance" },
                    { new Guid("a3be7b91-7548-492e-99dc-2788497f2930"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Film Music" },
                    { new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "For contacts only" },
                    { new Guid("5db547d6-c115-4409-8db7-59374ca2af83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Never again" },
                    { new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Emergency only" },
                    { new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Gladly" },
                    { new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "With - negotiable" },
                    { new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "With - strict" },
                    { new Guid("0141e712-7080-4e3d-8145-44a3080aa274"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Brings pianist" },
                    { new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Without" }
                });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[,]
                {
                    { new Guid("5578f637-14b7-4c11-85a8-0b94d83da678"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("a3be7b91-7548-492e-99dc-2788497f2930") },
                    { new Guid("9808c1f6-0bbd-4054-acca-779d56a8a934"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("0141e712-7080-4e3d-8145-44a3080aa274") },
                    { new Guid("a88b874f-9879-482f-85ec-1ddda9bb545c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("45d534e3-6605-42f0-ae57-1a943e18a9cd") },
                    { new Guid("24c5bbe1-37eb-4368-ac7c-a6061058bbef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("3acd5be1-5fbc-4de4-a45c-2e230c413c85"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("6307ec0e-482a-4777-8b2e-4e8cd5d1f252") },
                    { new Guid("fab42540-8c9d-4b18-9341-660f60dd7644"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("33bbdccf-59a9-4b05-bdac-af50137cecb3") },
                    { new Guid("0e997440-53f2-4823-8581-4d4716525885"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("42f546ab-1b96-4eab-88a4-753cad8392c1") },
                    { new Guid("7b8defe6-9922-43d6-8df0-3a73f47d6980"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("33e57595-2166-4cce-aa34-60d7148ae9f7") },
                    { new Guid("be152c92-b807-4850-8327-9d1916dabead"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("166edc65-9915-4836-b0a3-3c60ad0bcc04") },
                    { new Guid("4298e1f5-ea1d-4a83-9b32-e5dc3a7cbca9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0") },
                    { new Guid("887e7e2e-0c90-4c4c-9504-3f2a5af7fbcb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("e340f76d-074b-40e8-85b0-1bb66a596a06") },
                    { new Guid("f1626a63-6bf1-442a-86ad-8a86242bde94"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("d075dda3-ba29-472b-a699-1f92c1af13a9") },
                    { new Guid("edfad6f1-6584-4798-a09a-9f6146127d82"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("3550443d-5acf-4159-bd59-d7da04dd9434") },
                    { new Guid("0d1b888f-0f45-4f02-806b-480d5594bd27"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("6bdf5666-65ef-475a-9c48-9a38f18de041") },
                    { new Guid("1b53d96a-f9a1-4037-b103-f7aae9b278d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("c1951202-0e6e-41f7-bf07-5cefe47efade") },
                    { new Guid("f9cc5445-8a6e-480b-bffb-410089f55896"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("c0911d95-0c6d-4834-840c-43cddf3c51a0") },
                    { new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a") },
                    { new Guid("a15014ad-582e-4388-9b58-aceb52cf41bf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("5db547d6-c115-4409-8db7-59374ca2af83") },
                    { new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc") },
                    { new Guid("68e947c0-9450-4b64-90d7-553850396a3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb") },
                    { new Guid("d80bf2be-de2f-4d72-ba02-6081b5ba77d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e") },
                    { new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987") },
                    { new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a") },
                    { new Guid("4ef47024-d8a5-4b2d-8584-aeb29263dddb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("2ecfb104-feb3-406a-b741-0ac9fdd3e8d7") },
                    { new Guid("8daa5ae4-3885-4739-803a-693c7cfdf314"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("982a9947-c6f8-4c9a-b96f-2a4825a11496") },
                    { new Guid("a3e5843b-05c3-452c-a29d-da8de738181a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("0cf5b2e2-4f01-441a-adc8-a975c7494fd7") },
                    { new Guid("98addc5f-f2fa-4640-8441-d4220b7daea2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfiles_InqueryId",
                table: "MusicianProfiles",
                column: "InqueryId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfiles_PreferredPositionId",
                table: "MusicianProfiles",
                column: "PreferredPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfiles_SalaryId",
                table: "MusicianProfiles",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Auditions_AppointmentId",
                table: "Auditions",
                column: "AppointmentId",
                unique: true,
                filter: "[AppointmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Auditions_RepetitorStatusId",
                table: "Auditions",
                column: "RepetitorStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Auditions_StatusId",
                table: "Auditions",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailableDocuments_SelectValueMappingId",
                table: "AvailableDocuments",
                column: "SelectValueMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfileCredentials_CredentialId",
                table: "MusicianProfileCredentials",
                column: "CredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfileEducations_EducationId",
                table: "MusicianProfileEducations",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfileSections_SectionId",
                table: "MusicianProfileSections",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SectionId",
                table: "Positions",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferredGenre_SelectValueMappingId",
                table: "PreferredGenre",
                column: "SelectValueMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_SphereOfActivityConcerts_VenueId",
                table: "SphereOfActivityConcerts",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_SphereOfActivityRehearsals_VenueId",
                table: "SphereOfActivityRehearsals",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                table: "Addresses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_SelectValueMappings_TypeId",
                table: "Addresses",
                column: "TypeId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_Appointments_AppointmentId",
                table: "AppointmentParticipations",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_Persons_PersonId",
                table: "AppointmentParticipations",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_PredictionId",
                table: "AppointmentParticipations",
                column: "PredictionId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ResultId",
                table: "AppointmentParticipations",
                column: "ResultId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRooms_Appointments_AppointmentId",
                table: "AppointmentRooms",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRooms_Rooms_RoomId",
                table: "AppointmentRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_CategoryId",
                table: "Appointments",
                column: "CategoryId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentId",
                table: "Appointments",
                column: "EmolumentId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentPatternId",
                table: "Appointments",
                column: "EmolumentPatternId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_ExpectationId",
                table: "Appointments",
                column: "ExpectationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_StatusId",
                table: "Appointments",
                column: "StatusId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Persons_PersonId",
                table: "AspNetUsers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Persons_PersonId",
                table: "MusicianProfiles",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Positions_PreferredPositionId",
                table: "MusicianProfiles",
                column: "PreferredPositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Sections_InstrumentId",
                table: "MusicianProfiles",
                column: "InstrumentId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_InqueryId",
                table: "MusicianProfiles",
                column: "InqueryId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_QualificationId",
                table: "MusicianProfiles",
                column: "QualificationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_SalaryId",
                table: "MusicianProfiles",
                column: "SalaryId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSections_Persons_PersonId",
                table: "PersonSections",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSections_Sections_SectionId",
                table: "PersonSections",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAppointments_Appointments_AppointmentId",
                table: "ProjectAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAppointments_Projects_ProjectId",
                table: "ProjectAppointments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParticipations_MusicianProfiles_MusicianProfileId",
                table: "ProjectParticipations",
                column: "MusicianProfileId",
                principalTable: "MusicianProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParticipations_Projects_ProjectId",
                table: "ProjectParticipations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Projects_ParentId",
                table: "Projects",
                column: "ParentId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Venues_VenueId",
                table: "Rooms",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionAppointments_Appointments_AppointmentId",
                table: "SectionAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionAppointments_Sections_SectionId",
                table: "SectionAppointments",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Sections_ParentId",
                table: "Sections",
                column: "ParentId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectValueMappings_SelectValueCategories_SelectValueCategoryId",
                table: "SelectValueMappings",
                column: "SelectValueCategoryId",
                principalTable: "SelectValueCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectValueMappings_SelectValues_SelectValueId",
                table: "SelectValueMappings",
                column: "SelectValueId",
                principalTable: "SelectValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlRoles_AspNetRoles_RoleId",
                table: "UrlRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlRoles_Urls_UrlId",
                table: "UrlRoles",
                column: "UrlId",
                principalTable: "Urls",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_Projects_ProjectId",
                table: "Urls",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_Addresses_AddressId",
                table: "Venues",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_SelectValueMappings_TypeId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_Appointments_AppointmentId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_Persons_PersonId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_PredictionId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ResultId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentRooms_Appointments_AppointmentId",
                table: "AppointmentRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentRooms_Rooms_RoomId",
                table: "AppointmentRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_CategoryId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentPatternId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_ExpectationId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_StatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Persons_PersonId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Persons_PersonId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Positions_PreferredPositionId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Sections_InstrumentId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_InqueryId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_QualificationId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_SalaryId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSections_Persons_PersonId",
                table: "PersonSections");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonSections_Sections_SectionId",
                table: "PersonSections");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAppointments_Appointments_AppointmentId",
                table: "ProjectAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAppointments_Projects_ProjectId",
                table: "ProjectAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParticipations_MusicianProfiles_MusicianProfileId",
                table: "ProjectParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParticipations_Projects_ProjectId",
                table: "ProjectParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Projects_ParentId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Venues_VenueId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionAppointments_Appointments_AppointmentId",
                table: "SectionAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionAppointments_Sections_SectionId",
                table: "SectionAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Sections_ParentId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectValueMappings_SelectValueCategories_SelectValueCategoryId",
                table: "SelectValueMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectValueMappings_SelectValues_SelectValueId",
                table: "SelectValueMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_UrlRoles_AspNetRoles_RoleId",
                table: "UrlRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UrlRoles_Urls_UrlId",
                table: "UrlRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Urls_Projects_ProjectId",
                table: "Urls");

            migrationBuilder.DropForeignKey(
                name: "FK_Venues_Addresses_AddressId",
                table: "Venues");

            migrationBuilder.DropTable(
                name: "Auditions");

            migrationBuilder.DropTable(
                name: "AvailableDocuments");

            migrationBuilder.DropTable(
                name: "MusicianProfileCredentials");

            migrationBuilder.DropTable(
                name: "MusicianProfileEducations");

            migrationBuilder.DropTable(
                name: "MusicianProfileSections");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "PreferredGenre");

            migrationBuilder.DropTable(
                name: "SphereOfActivityConcerts");

            migrationBuilder.DropTable(
                name: "SphereOfActivityRehearsals");

            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_MusicianProfiles_InqueryId",
                table: "MusicianProfiles");

            migrationBuilder.DropIndex(
                name: "IX_MusicianProfiles_PreferredPositionId",
                table: "MusicianProfiles");

            migrationBuilder.DropIndex(
                name: "IX_MusicianProfiles_SalaryId",
                table: "MusicianProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonSections",
                table: "PersonSections");

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("0d1b888f-0f45-4f02-806b-480d5594bd27"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("0e997440-53f2-4823-8581-4d4716525885"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("1b53d96a-f9a1-4037-b103-f7aae9b278d7"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("24c5bbe1-37eb-4368-ac7c-a6061058bbef"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("30f592f6-485a-468a-bfb2-4854be733e74"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("3acd5be1-5fbc-4de4-a45c-2e230c413c85"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("4298e1f5-ea1d-4a83-9b32-e5dc3a7cbca9"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("4ef47024-d8a5-4b2d-8584-aeb29263dddb"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("5578f637-14b7-4c11-85a8-0b94d83da678"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("68e947c0-9450-4b64-90d7-553850396a3f"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("7b8defe6-9922-43d6-8df0-3a73f47d6980"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("887e7e2e-0c90-4c4c-9504-3f2a5af7fbcb"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("8daa5ae4-3885-4739-803a-693c7cfdf314"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("9808c1f6-0bbd-4054-acca-779d56a8a934"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("98addc5f-f2fa-4640-8441-d4220b7daea2"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("a15014ad-582e-4388-9b58-aceb52cf41bf"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("a3e5843b-05c3-452c-a29d-da8de738181a"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("a88b874f-9879-482f-85ec-1ddda9bb545c"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("be152c92-b807-4850-8327-9d1916dabead"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("d80bf2be-de2f-4d72-ba02-6081b5ba77d2"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("edfad6f1-6584-4798-a09a-9f6146127d82"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("f036bca9-95d4-4526-b845-fff9208ab103"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("f1626a63-6bf1-442a-86ad-8a86242bde94"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("f9cc5445-8a6e-480b-bffb-410089f55896"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("fab42540-8c9d-4b18-9341-660f60dd7644"));

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("072c2a9a-a492-4190-9a49-505ff7056528"));

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"));

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"));

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"));

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("0141e712-7080-4e3d-8145-44a3080aa274"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("0cf5b2e2-4f01-441a-adc8-a975c7494fd7"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("166edc65-9915-4836-b0a3-3c60ad0bcc04"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("2ecfb104-feb3-406a-b741-0ac9fdd3e8d7"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("33e57595-2166-4cce-aa34-60d7148ae9f7"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("3550443d-5acf-4159-bd59-d7da04dd9434"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("42f546ab-1b96-4eab-88a4-753cad8392c1"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("45d534e3-6605-42f0-ae57-1a943e18a9cd"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("5db547d6-c115-4409-8db7-59374ca2af83"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("6307ec0e-482a-4777-8b2e-4e8cd5d1f252"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("6bdf5666-65ef-475a-9c48-9a38f18de041"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("982a9947-c6f8-4c9a-b96f-2a4825a11496"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("a3be7b91-7548-492e-99dc-2788497f2930"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("c0911d95-0c6d-4834-840c-43cddf3c51a0"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("c1951202-0e6e-41f7-bf07-5cefe47efade"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("d075dda3-ba29-472b-a699-1f92c1af13a9"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("e340f76d-074b-40e8-85b0-1bb66a596a06"));

            migrationBuilder.DropColumn(
                name: "Background",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "ExperienceLevel",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "InqueryId",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "LevelInnerASsessment",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "LevelSelfAssessment",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "PreferredPositionId",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "ProfileFavorizitation",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "SalaryId",
                table: "MusicianProfiles");

            migrationBuilder.DropColumn(
                name: "AuditionId",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "PersonSections",
                newName: "PersonSection");

            migrationBuilder.RenameColumn(
                name: "IsMainProfile",
                table: "MusicianProfiles",
                newName: "IsProfessional");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonSections_SectionId",
                table: "PersonSection",
                newName: "IX_PersonSection_SectionId");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "UrlRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UrlRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "SelectValues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "SelectValues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "SelectValueMappings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "SelectValueMappings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "SelectValueCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "SelectValueCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Sections",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "SectionAppointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "SectionAppointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ProjectParticipations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProjectParticipations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "ProjectAppointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "ProjectAppointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "MusicianProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "MusicianProfiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "AppointmentRooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AppointmentRooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "AppointmentParticipations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AppointmentParticipations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "PersonSection",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PersonSection",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonSection",
                table: "PersonSection",
                columns: new[] { "PersonId", "SectionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                table: "Addresses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_SelectValueMappings_TypeId",
                table: "Addresses",
                column: "TypeId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_Appointments_AppointmentId",
                table: "AppointmentParticipations",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_Persons_PersonId",
                table: "AppointmentParticipations",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_PredictionId",
                table: "AppointmentParticipations",
                column: "PredictionId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ResultId",
                table: "AppointmentParticipations",
                column: "ResultId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRooms_Appointments_AppointmentId",
                table: "AppointmentRooms",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentRooms_Rooms_RoomId",
                table: "AppointmentRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_CategoryId",
                table: "Appointments",
                column: "CategoryId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentId",
                table: "Appointments",
                column: "EmolumentId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentPatternId",
                table: "Appointments",
                column: "EmolumentPatternId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_ExpectationId",
                table: "Appointments",
                column: "ExpectationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_StatusId",
                table: "Appointments",
                column: "StatusId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Persons_PersonId",
                table: "AspNetUsers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Persons_PersonId",
                table: "MusicianProfiles",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Sections_InstrumentId",
                table: "MusicianProfiles",
                column: "InstrumentId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_SelectValueMappings_QualificationId",
                table: "MusicianProfiles",
                column: "QualificationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_ProjectAppointments_Appointments_AppointmentId",
                table: "ProjectAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAppointments_Projects_ProjectId",
                table: "ProjectAppointments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParticipations_MusicianProfiles_MusicianProfileId",
                table: "ProjectParticipations",
                column: "MusicianProfileId",
                principalTable: "MusicianProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParticipations_Projects_ProjectId",
                table: "ProjectParticipations",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Projects_ParentId",
                table: "Projects",
                column: "ParentId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Venues_VenueId",
                table: "Rooms",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionAppointments_Appointments_AppointmentId",
                table: "SectionAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionAppointments_Sections_SectionId",
                table: "SectionAppointments",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Sections_ParentId",
                table: "Sections",
                column: "ParentId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectValueMappings_SelectValueCategories_SelectValueCategoryId",
                table: "SelectValueMappings",
                column: "SelectValueCategoryId",
                principalTable: "SelectValueCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectValueMappings_SelectValues_SelectValueId",
                table: "SelectValueMappings",
                column: "SelectValueId",
                principalTable: "SelectValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UrlRoles_AspNetRoles_RoleId",
                table: "UrlRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UrlRoles_Urls_UrlId",
                table: "UrlRoles",
                column: "UrlId",
                principalTable: "Urls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_Projects_ProjectId",
                table: "Urls",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_Addresses_AddressId",
                table: "Venues",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
