using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddedAuditionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuditionId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Audition",
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
                    table.PrimaryKey("PK_Audition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audition_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Audition_SelectValueMappings_RepetitorStatusId",
                        column: x => x.RepetitorStatusId,
                        principalTable: "SelectValueMappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Audition_SelectValueMappings_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SelectValueMappings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "SelectValueCategories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "Property", "Table" },
                values: new object[,]
                {
                    { new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Status", "Status", "Audition" },
                    { new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Repetitor status", "RepetitorStatus", "Audition" }
                });

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("166edc65-9915-4836-b0a3-3c60ad0bcc04"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Passed" },
                    { new Guid("33e57595-2166-4cce-aa34-60d7148ae9f7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Failed" },
                    { new Guid("42f546ab-1b96-4eab-88a4-753cad8392c1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Awaiting" },
                    { new Guid("45d534e3-6605-42f0-ae57-1a943e18a9cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Is asking for pianist" },
                    { new Guid("6bdf5666-65ef-475a-9c48-9a38f18de041"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "No pianist needed" },
                    { new Guid("0141e712-7080-4e3d-8145-44a3080aa274"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Brings pianist" },
                    { new Guid("6307ec0e-482a-4777-8b2e-4e8cd5d1f252"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Unnecessary" }
                });

            migrationBuilder.InsertData(
                table: "SelectValueMappings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[,]
                {
                    { new Guid("be152c92-b807-4850-8327-9d1916dabead"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("166edc65-9915-4836-b0a3-3c60ad0bcc04") },
                    { new Guid("7b8defe6-9922-43d6-8df0-3a73f47d6980"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("33e57595-2166-4cce-aa34-60d7148ae9f7") },
                    { new Guid("0e997440-53f2-4823-8581-4d4716525885"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("42f546ab-1b96-4eab-88a4-753cad8392c1") },
                    { new Guid("fab42540-8c9d-4b18-9341-660f60dd7644"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("33bbdccf-59a9-4b05-bdac-af50137cecb3") },
                    { new Guid("3acd5be1-5fbc-4de4-a45c-2e230c413c85"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("6307ec0e-482a-4777-8b2e-4e8cd5d1f252") },
                    { new Guid("24c5bbe1-37eb-4368-ac7c-a6061058bbef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("a88b874f-9879-482f-85ec-1ddda9bb545c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("45d534e3-6605-42f0-ae57-1a943e18a9cd") },
                    { new Guid("9808c1f6-0bbd-4054-acca-779d56a8a934"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("0141e712-7080-4e3d-8145-44a3080aa274") },
                    { new Guid("0d1b888f-0f45-4f02-806b-480d5594bd27"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("6bdf5666-65ef-475a-9c48-9a38f18de041") },
                    { new Guid("98addc5f-f2fa-4640-8441-d4220b7daea2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audition_AppointmentId",
                table: "Audition",
                column: "AppointmentId",
                unique: true,
                filter: "[AppointmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Audition_RepetitorStatusId",
                table: "Audition",
                column: "RepetitorStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Audition_StatusId",
                table: "Audition",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audition");

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
                keyValue: new Guid("24c5bbe1-37eb-4368-ac7c-a6061058bbef"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("3acd5be1-5fbc-4de4-a45c-2e230c413c85"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("7b8defe6-9922-43d6-8df0-3a73f47d6980"));

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
                keyValue: new Guid("a88b874f-9879-482f-85ec-1ddda9bb545c"));

            migrationBuilder.DeleteData(
                table: "SelectValueMappings",
                keyColumn: "Id",
                keyValue: new Guid("be152c92-b807-4850-8327-9d1916dabead"));

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
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("0141e712-7080-4e3d-8145-44a3080aa274"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("166edc65-9915-4836-b0a3-3c60ad0bcc04"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("33e57595-2166-4cce-aa34-60d7148ae9f7"));

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
                keyValue: new Guid("6307ec0e-482a-4777-8b2e-4e8cd5d1f252"));

            migrationBuilder.DeleteData(
                table: "SelectValues",
                keyColumn: "Id",
                keyValue: new Guid("6bdf5666-65ef-475a-9c48-9a38f18de041"));

            migrationBuilder.DropColumn(
                name: "AuditionId",
                table: "Appointments");
        }
    }
}
