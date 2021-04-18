using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddAppointmentView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlDirectory = Path.Combine(AppContext.BaseDirectory, "SqlStatements");

            var createMusicianViewSql = File.ReadAllText(Path.Combine(sqlDirectory, "MusicianView.sql"));
            migrationBuilder.Sql(createMusicianViewSql);

            var createListParentSectionsFunctionSql = File.ReadAllText(Path.Combine(sqlDirectory, "ListParentSectionsFunction.sql"));
            migrationBuilder.Sql(createListParentSectionsFunctionSql);

            var createAppointmentsForUserViewSql = File.ReadAllText(Path.Combine(sqlDirectory, "AppointmentsForUserView.sql"));
            migrationBuilder.Sql(createAppointmentsForUserViewSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW appointments_for_user");
            migrationBuilder.Sql("DROP VIEW musician");
            migrationBuilder.Sql("DROP FUNCTION fn_list_parent_sections");
        }
    }
}
