using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class RemoveInitialAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Removed deletion of admin person to prevent foreign key constraint failures. Please delete admin user via frontend and restart service again
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "persons",
                columns: new[] { "id", "about_me", "birth_name", "birthplace", "contact_via_id", "created_at", "created_by", "date_of_birth", "deleted", "experience_level", "gender_id", "general_preference", "given_name", "modified_at", "modified_by", "moving_box", "reliability", "surname" },
                values: new object[] { new Guid("56ed7c20-ba78-4a02-936e-5e840ef0748c"), null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, (byte)0, new Guid("88d680fe-b6cc-486f-8f79-2525189b8b13"), (byte)0, "Initial", null, null, null, (byte)0, "Admin" });
        }
    }
}
