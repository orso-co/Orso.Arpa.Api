using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedPreferredGenresToMusicianProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicianProfileSelectValueMappings",
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

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[] { new Guid("a3be7b91-7548-492e-99dc-2788497f2930"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Film Music" });

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[] { new Guid("982a9947-c6f8-4c9a-b96f-2a4825a11496"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Dance Performance" });

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[] { new Guid("2ecfb104-feb3-406a-b741-0ac9fdd3e8d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Contemporary Music" });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[] { new Guid("5578f637-14b7-4c11-85a8-0b94d83da678"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("a3be7b91-7548-492e-99dc-2788497f2930") });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[] { new Guid("8daa5ae4-3885-4739-803a-693c7cfdf314"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("982a9947-c6f8-4c9a-b96f-2a4825a11496") });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[] { new Guid("4ef47024-d8a5-4b2d-8584-aeb29263dddb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("2ecfb104-feb3-406a-b741-0ac9fdd3e8d7") });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfileSelectValueMappings_SelectValueMappingId",
                table: "MusicianProfileSelectValueMappings",
                column: "SelectValueMappingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicianProfileSelectValueMappings");

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
                keyValue: new Guid("8daa5ae4-3885-4739-803a-693c7cfdf314"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("2ecfb104-feb3-406a-b741-0ac9fdd3e8d7"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("982a9947-c6f8-4c9a-b96f-2a4825a11496"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("a3be7b91-7548-492e-99dc-2788497f2930"));
        }
    }
}
