using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class DeleteBehaviors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_SelectValueMappings_TypeId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ResultId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_CategoryId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_SelectValueMappings_GenreId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Venues_Addresses_AddressId",
                table: "Venues");

            migrationBuilder.DropTable(
                name: "ConcertRooms");

            migrationBuilder.DropTable(
                name: "RehearsalRooms");

            migrationBuilder.DropIndex(
                name: "IX_Venues_AddressId",
                table: "Venues");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "Venues",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_AddressId",
                table: "Venues",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                table: "Addresses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_SelectValueMappings_TypeId",
                table: "Addresses",
                column: "TypeId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ResultId",
                table: "AppointmentParticipations",
                column: "ResultId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SelectValueMappings_CategoryId",
                table: "Appointments",
                column: "CategoryId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_SelectValueMappings_GenreId",
                table: "Projects",
                column: "GenreId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_Addresses_AddressId",
                table: "Venues",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_SelectValueMappings_TypeId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentParticipations_SelectValueMappings_ResultId",
                table: "AppointmentParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SelectValueMappings_CategoryId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_SelectValueMappings_GenreId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Venues_Addresses_AddressId",
                table: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Venues_AddressId",
                table: "Venues");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "Venues",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ConcertRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RehearsalRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RehearsalRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RehearsalRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Venues_AddressId",
                table: "Venues",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertRooms_RoomId",
                table: "ConcertRooms",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalRooms_RoomId",
                table: "RehearsalRooms",
                column: "RoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Regions_RegionId",
                table: "Addresses",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Persons_PersonId",
                table: "Addresses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_SelectValueMappings_TypeId",
                table: "Addresses",
                column: "TypeId",
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
                name: "FK_Appointments_Venues_VenueId",
                table: "Appointments",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_SelectValueMappings_GenreId",
                table: "Projects",
                column: "GenreId",
                principalTable: "SelectValueMappings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_Addresses_AddressId",
                table: "Venues",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
