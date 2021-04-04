using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedNavigationTablesForMusicianProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audition_Appointments_AppointmentId",
                table: "Audition");

            migrationBuilder.DropForeignKey(
                name: "FK_Audition_SelectValueMappings_RepetitorStatusId",
                table: "Audition");

            migrationBuilder.DropForeignKey(
                name: "FK_Audition_SelectValueMappings_StatusId",
                table: "Audition");

            migrationBuilder.DropTable(
                name: "MusicianProfileSelectValueMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Audition",
                table: "Audition");

            migrationBuilder.RenameTable(
                name: "Audition",
                newName: "Auditions");

            migrationBuilder.RenameIndex(
                name: "IX_Audition_StatusId",
                table: "Auditions",
                newName: "IX_Auditions_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Audition_RepetitorStatusId",
                table: "Auditions",
                newName: "IX_Auditions_RepetitorStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Audition_AppointmentId",
                table: "Auditions",
                newName: "IX_Auditions_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auditions",
                table: "Auditions",
                column: "Id");

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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvailableDocuments_SelectValueMappings_SelectValueMappingId",
                        column: x => x.SelectValueMappingId,
                        principalTable: "SelectValueMappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferredGenre_SelectValueMappings_SelectValueMappingId",
                        column: x => x.SelectValueMappingId,
                        principalTable: "SelectValueMappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SphereOfActivityConcerts_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SphereOfActivityRehearsals_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SelectValueCategories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "Property", "Table" },
                values: new object[] { new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Available document status", "AvailableDocumentStatus", "MusicianProfile" });

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("c0911d95-0c6d-4834-840c-43cddf3c51a0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "CV" },
                    { new Guid("0cf5b2e2-4f01-441a-adc8-a975c7494fd7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Letter of recommendation" },
                    { new Guid("c1951202-0e6e-41f7-bf07-5cefe47efade"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Diploma" },
                    { new Guid("e340f76d-074b-40e8-85b0-1bb66a596a06"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Photo" },
                    { new Guid("d075dda3-ba29-472b-a699-1f92c1af13a9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Video" },
                    { new Guid("3550443d-5acf-4159-bd59-d7da04dd9434"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Audio" }
                });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[,]
                {
                    { new Guid("f9cc5445-8a6e-480b-bffb-410089f55896"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("c0911d95-0c6d-4834-840c-43cddf3c51a0") },
                    { new Guid("a3e5843b-05c3-452c-a29d-da8de738181a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("0cf5b2e2-4f01-441a-adc8-a975c7494fd7") },
                    { new Guid("1b53d96a-f9a1-4037-b103-f7aae9b278d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("c1951202-0e6e-41f7-bf07-5cefe47efade") },
                    { new Guid("edfad6f1-6584-4798-a09a-9f6146127d82"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("3550443d-5acf-4159-bd59-d7da04dd9434") },
                    { new Guid("f1626a63-6bf1-442a-86ad-8a86242bde94"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("d075dda3-ba29-472b-a699-1f92c1af13a9") },
                    { new Guid("887e7e2e-0c90-4c4c-9504-3f2a5af7fbcb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("e340f76d-074b-40e8-85b0-1bb66a596a06") },
                    { new Guid("4298e1f5-ea1d-4a83-9b32-e5dc3a7cbca9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableDocuments_SelectValueMappingId",
                table: "AvailableDocuments",
                column: "SelectValueMappingId");

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
                name: "FK_Auditions_Appointments_AppointmentId",
                table: "Auditions",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Auditions_SelectValueMappings_RepetitorStatusId",
                table: "Auditions",
                column: "RepetitorStatusId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Auditions_SelectValueMappings_StatusId",
                table: "Auditions",
                column: "StatusId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditions_Appointments_AppointmentId",
                table: "Auditions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auditions_SelectValueMappings_RepetitorStatusId",
                table: "Auditions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auditions_SelectValueMappings_StatusId",
                table: "Auditions");

            migrationBuilder.DropTable(
                name: "AvailableDocuments");

            migrationBuilder.DropTable(
                name: "PreferredGenre");

            migrationBuilder.DropTable(
                name: "SphereOfActivityConcerts");

            migrationBuilder.DropTable(
                name: "SphereOfActivityRehearsals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auditions",
                table: "Auditions");

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("1b53d96a-f9a1-4037-b103-f7aae9b278d7"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("4298e1f5-ea1d-4a83-9b32-e5dc3a7cbca9"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("887e7e2e-0c90-4c4c-9504-3f2a5af7fbcb"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("a3e5843b-05c3-452c-a29d-da8de738181a"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("edfad6f1-6584-4798-a09a-9f6146127d82"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("f1626a63-6bf1-442a-86ad-8a86242bde94"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("f9cc5445-8a6e-480b-bffb-410089f55896"));

            migrationBuilder.DeleteData(
                table: "SelectValueCategories",
                keyColumn: "Id",
                keyValue: new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("0cf5b2e2-4f01-441a-adc8-a975c7494fd7"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("3550443d-5acf-4159-bd59-d7da04dd9434"));

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
                keyValue: new Guid("e340f76d-074b-40e8-85b0-1bb66a596a06"));

            migrationBuilder.RenameTable(
                name: "Auditions",
                newName: "Audition");

            migrationBuilder.RenameIndex(
                name: "IX_Auditions_StatusId",
                table: "Audition",
                newName: "IX_Audition_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Auditions_RepetitorStatusId",
                table: "Audition",
                newName: "IX_Audition_RepetitorStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Auditions_AppointmentId",
                table: "Audition",
                newName: "IX_Audition_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Audition",
                table: "Audition",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MusicianProfileSelectValueMappings",
                columns: table => new
                {
                    MusicianProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SelectValueMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianProfileSelectValueMappings", x => new { x.MusicianProfileId, x.SelectValueMappingId });
                    table.ForeignKey(
                        name: "FK_MusicianProfileSelectValueMappings_MusicianProfiles_MusicianProfileId",
                        column: x => x.MusicianProfileId,
                        principalTable: "MusicianProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianProfileSelectValueMappings_SelectValueMappings_SelectValueMappingId",
                        column: x => x.SelectValueMappingId,
                        principalTable: "SelectValueMappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfileSelectValueMappings_SelectValueMappingId",
                table: "MusicianProfileSelectValueMappings",
                column: "SelectValueMappingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Audition_Appointments_AppointmentId",
                table: "Audition",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Audition_SelectValueMappings_RepetitorStatusId",
                table: "Audition",
                column: "RepetitorStatusId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Audition_SelectValueMappings_StatusId",
                table: "Audition",
                column: "StatusId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
