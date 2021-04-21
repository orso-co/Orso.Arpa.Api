using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class RelationImprovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_SelectValueMapping_TypeId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipation_Appointments_AppointmentId",
                table: "AppointmentParticipation");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipation_SelectValueMapping_ExpectationId",
                table: "AppointmentParticipation");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipation_Persons_PersonId",
                table: "AppointmentParticipation");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipation_SelectValueMapping_PredictionId",
                table: "AppointmentParticipation");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipation_SelectValueMapping_ResultId",
                table: "AppointmentParticipation");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMapping_CategoryId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMapping_EmolumentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMapping_EmolumentPatternId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMapping_StatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfile_Persons_PersonId",
                table: "MusicianProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfile_Registers_RegisterId",
                table: "MusicianProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAppointment_Appointments_AppointmentId",
                table: "ProjectAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAppointment_Projects_ProjectId",
                table: "ProjectAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_SelectValueMapping_GenreId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterAppointment_Appointments_AppointmentId",
                table: "RegisterAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterAppointment_Registers_RegisterId",
                table: "RegisterAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_RehearsalRooms_Venues_VenueId",
                table: "RehearsalRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectValueMapping_SelectValueCategories_SelectValueCategoryId",
                table: "SelectValueMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectValueMapping_SelectValues_SelectValueId",
                table: "SelectValueMapping");

            migrationBuilder.DropIndex(
                name: "IX_RehearsalRooms_VenueId",
                table: "RehearsalRooms");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_VenueId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SelectValueMapping",
                table: "SelectValueMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterAppointment",
                table: "RegisterAppointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectAppointment",
                table: "ProjectAppointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MusicianProfile",
                table: "MusicianProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentParticipation",
                table: "AppointmentParticipation");

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "SelectValueMapping",
                newName: "SelectValueMappings");

            migrationBuilder.RenameTable(
                name: "RegisterAppointment",
                newName: "RegisterAppointments");

            migrationBuilder.RenameTable(
                name: "ProjectAppointment",
                newName: "ProjectAppointments");

            migrationBuilder.RenameTable(
                name: "MusicianProfile",
                newName: "MusicianProfiles");

            migrationBuilder.RenameTable(
                name: "AppointmentParticipation",
                newName: "AppointmentParticipations");

            migrationBuilder.RenameColumn(
                name: "VenueId",
                table: "RehearsalRooms",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_SelectValueMapping_SelectValueId",
                table: "SelectValueMappings",
                newName: "IX_SelectValueMappings_SelectValueId");

            migrationBuilder.RenameIndex(
                name: "IX_SelectValueMapping_SelectValueCategoryId",
                table: "SelectValueMappings",
                newName: "IX_SelectValueMappings_SelectValueCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterAppointment_AppointmentId",
                table: "RegisterAppointments",
                newName: "IX_RegisterAppointments_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectAppointment_AppointmentId",
                table: "ProjectAppointments",
                newName: "IX_ProjectAppointments_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicianProfile_RegisterId",
                table: "MusicianProfiles",
                newName: "IX_MusicianProfiles_RegisterId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicianProfile_PersonId",
                table: "MusicianProfiles",
                newName: "IX_MusicianProfiles_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentParticipation_ResultId",
                table: "AppointmentParticipations",
                newName: "IX_AppointmentParticipations_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentParticipation_PredictionId",
                table: "AppointmentParticipations",
                newName: "IX_AppointmentParticipations_PredictionId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentParticipation_PersonId",
                table: "AppointmentParticipations",
                newName: "IX_AppointmentParticipations_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentParticipation_ExpectationId",
                table: "AppointmentParticipations",
                newName: "IX_AppointmentParticipations_ExpectationId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentParticipation_AppointmentId",
                table: "AppointmentParticipations",
                newName: "IX_AppointmentParticipations_AppointmentId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Addresses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrbanDistrict",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SelectValueMappings",
                table: "SelectValueMappings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterAppointments",
                table: "RegisterAppointments",
                columns: new[] { "RegisterId", "AppointmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectAppointments",
                table: "ProjectAppointments",
                columns: new[] { "ProjectId", "AppointmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MusicianProfiles",
                table: "MusicianProfiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentParticipations",
                table: "AppointmentParticipations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProjectParticipations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    MusicianProfileId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectParticipations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectParticipations_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectParticipations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    VenueId = table.Column<Guid>(nullable: false),
                    Building = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentRooms",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentRooms", x => new { x.AppointmentId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_AppointmentRooms_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConcertRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcertRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"),
                column: "Table",
                value: "PersonAddress");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalRooms_RoomId",
                table: "RehearsalRooms",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentRooms_RoomId",
                table: "AppointmentRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertRooms_RoomId",
                table: "ConcertRooms",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectParticipations_MusicianProfileId",
                table: "ProjectParticipations",
                column: "MusicianProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectParticipations_ProjectId",
                table: "ProjectParticipations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_VenueId",
                table: "Rooms",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_SelectValueMappings_TypeId",
                table: "Addresses",
                column: "TypeId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_Appointments_AppointmentId",
                table: "AppointmentParticipations",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ExpectationId",
                table: "AppointmentParticipations",
                column: "ExpectationId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_CategoryId",
                table: "Appointments",
                column: "CategoryId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Appointments_SelectValueMappings_StatusId",
                table: "Appointments",
                column: "StatusId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Persons_PersonId",
                table: "MusicianProfiles",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfiles_Registers_RegisterId",
                table: "MusicianProfiles",
                column: "RegisterId",
                principalTable: "Registers",
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
                name: "FK_Projects_SelectValueMappings_GenreId",
                table: "Projects",
                column: "GenreId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterAppointments_Appointments_AppointmentId",
                table: "RegisterAppointments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterAppointments_Registers_RegisterId",
                table: "RegisterAppointments",
                column: "RegisterId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RehearsalRooms_Rooms_RoomId",
                table: "RehearsalRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_SelectValueMappings_TypeId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_Appointments_AppointmentId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ExpectationId",
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
                name: "FK_Appointments_SelectValueMappings_CategoryId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_EmolumentPatternId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_StatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Persons_PersonId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_MusicianProfiles_Registers_RegisterId",
                table: "MusicianProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAppointments_Appointments_AppointmentId",
                table: "ProjectAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAppointments_Projects_ProjectId",
                table: "ProjectAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_SelectValueMappings_GenreId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterAppointments_Appointments_AppointmentId",
                table: "RegisterAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisterAppointments_Registers_RegisterId",
                table: "RegisterAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_RehearsalRooms_Rooms_RoomId",
                table: "RehearsalRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectValueMappings_SelectValueCategories_SelectValueCategoryId",
                table: "SelectValueMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectValueMappings_SelectValues_SelectValueId",
                table: "SelectValueMappings");

            migrationBuilder.DropTable(
                name: "AppointmentRooms");

            migrationBuilder.DropTable(
                name: "ConcertRooms");

            migrationBuilder.DropTable(
                name: "ProjectParticipations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_RehearsalRooms_RoomId",
                table: "RehearsalRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SelectValueMappings",
                table: "SelectValueMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterAppointments",
                table: "RegisterAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectAppointments",
                table: "ProjectAppointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MusicianProfiles",
                table: "MusicianProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentParticipations",
                table: "AppointmentParticipations");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UrbanDistrict",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "SelectValueMappings",
                newName: "SelectValueMapping");

            migrationBuilder.RenameTable(
                name: "RegisterAppointments",
                newName: "RegisterAppointment");

            migrationBuilder.RenameTable(
                name: "ProjectAppointments",
                newName: "ProjectAppointment");

            migrationBuilder.RenameTable(
                name: "MusicianProfiles",
                newName: "MusicianProfile");

            migrationBuilder.RenameTable(
                name: "AppointmentParticipations",
                newName: "AppointmentParticipation");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "RehearsalRooms",
                newName: "VenueId");

            migrationBuilder.RenameIndex(
                name: "IX_SelectValueMappings_SelectValueId",
                table: "SelectValueMapping",
                newName: "IX_SelectValueMapping_SelectValueId");

            migrationBuilder.RenameIndex(
                name: "IX_SelectValueMappings_SelectValueCategoryId",
                table: "SelectValueMapping",
                newName: "IX_SelectValueMapping_SelectValueCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterAppointments_AppointmentId",
                table: "RegisterAppointment",
                newName: "IX_RegisterAppointment_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectAppointments_AppointmentId",
                table: "ProjectAppointment",
                newName: "IX_ProjectAppointment_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicianProfiles_RegisterId",
                table: "MusicianProfile",
                newName: "IX_MusicianProfile_RegisterId");

            migrationBuilder.RenameIndex(
                name: "IX_MusicianProfiles_PersonId",
                table: "MusicianProfile",
                newName: "IX_MusicianProfile_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentParticipations_ResultId",
                table: "AppointmentParticipation",
                newName: "IX_AppointmentParticipation_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentParticipations_PredictionId",
                table: "AppointmentParticipation",
                newName: "IX_AppointmentParticipation_PredictionId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentParticipations_PersonId",
                table: "AppointmentParticipation",
                newName: "IX_AppointmentParticipation_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentParticipations_ExpectationId",
                table: "AppointmentParticipation",
                newName: "IX_AppointmentParticipation_ExpectationId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentParticipations_AppointmentId",
                table: "AppointmentParticipation",
                newName: "IX_AppointmentParticipation_AppointmentId");

            migrationBuilder.AddColumn<Guid>(
                name: "VenueId",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SelectValueMapping",
                table: "SelectValueMapping",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterAppointment",
                table: "RegisterAppointment",
                columns: new[] { "RegisterId", "AppointmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectAppointment",
                table: "ProjectAppointment",
                columns: new[] { "ProjectId", "AppointmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MusicianProfile",
                table: "MusicianProfile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentParticipation",
                table: "AppointmentParticipation",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"),
                column: "Table",
                value: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalRooms_VenueId",
                table: "RehearsalRooms",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VenueId",
                table: "Appointments",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_SelectValueMapping_TypeId",
                table: "Addresses",
                column: "TypeId",
                principalTable: "SelectValueMapping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipation_Appointments_AppointmentId",
                table: "AppointmentParticipation",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipation_SelectValueMapping_ExpectationId",
                table: "AppointmentParticipation",
                column: "ExpectationId",
                principalTable: "SelectValueMapping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipation_Persons_PersonId",
                table: "AppointmentParticipation",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipation_SelectValueMapping_PredictionId",
                table: "AppointmentParticipation",
                column: "PredictionId",
                principalTable: "SelectValueMapping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipation_SelectValueMapping_ResultId",
                table: "AppointmentParticipation",
                column: "ResultId",
                principalTable: "SelectValueMapping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMapping_CategoryId",
                table: "Appointments",
                column: "CategoryId",
                principalTable: "SelectValueMapping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMapping_EmolumentId",
                table: "Appointments",
                column: "EmolumentId",
                principalTable: "SelectValueMapping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMapping_EmolumentPatternId",
                table: "Appointments",
                column: "EmolumentPatternId",
                principalTable: "SelectValueMapping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMapping_StatusId",
                table: "Appointments",
                column: "StatusId",
                principalTable: "SelectValueMapping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfile_Persons_PersonId",
                table: "MusicianProfile",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicianProfile_Registers_RegisterId",
                table: "MusicianProfile",
                column: "RegisterId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAppointment_Appointments_AppointmentId",
                table: "ProjectAppointment",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAppointment_Projects_ProjectId",
                table: "ProjectAppointment",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_SelectValueMapping_GenreId",
                table: "Projects",
                column: "GenreId",
                principalTable: "SelectValueMapping",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterAppointment_Appointments_AppointmentId",
                table: "RegisterAppointment",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterAppointment_Registers_RegisterId",
                table: "RegisterAppointment",
                column: "RegisterId",
                principalTable: "Registers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RehearsalRooms_Venues_VenueId",
                table: "RehearsalRooms",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectValueMapping_SelectValueCategories_SelectValueCategoryId",
                table: "SelectValueMapping",
                column: "SelectValueCategoryId",
                principalTable: "SelectValueCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SelectValueMapping_SelectValues_SelectValueId",
                table: "SelectValueMapping",
                column: "SelectValueId",
                principalTable: "SelectValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
