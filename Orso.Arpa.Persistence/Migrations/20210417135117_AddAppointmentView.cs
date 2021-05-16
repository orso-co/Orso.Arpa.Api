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

            var createIsPersonInSectionFunctionSql = File.ReadAllText(Path.Combine(sqlDirectory, "IsPersonInSectionFunction.sql"));
            migrationBuilder.Sql(createIsPersonInSectionFunctionSql);

            var createIsPersonInProjectFunctionSql = File.ReadAllText(Path.Combine(sqlDirectory, "IsPersonInProjectFunction.sql"));
            migrationBuilder.Sql(createIsPersonInProjectFunctionSql);

            var createUserAppointmentForSectionViewSql = File.ReadAllText(Path.Combine(sqlDirectory, "UserAppointmentForSectionView.sql"));
            migrationBuilder.Sql(createUserAppointmentForSectionViewSql);

            var createUserAppointmentForProjectViewSql = File.ReadAllText(Path.Combine(sqlDirectory, "UserAppointmentForProjectView.sql"));
            migrationBuilder.Sql(createUserAppointmentForProjectViewSql);

            var createAppointmentsForPersonFunctionSql = File.ReadAllText(Path.Combine(sqlDirectory, "AppointmentsForPersonFunction.sql"));
            migrationBuilder.Sql(createAppointmentsForPersonFunctionSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION fn_appointments_for_person");
            migrationBuilder.Sql("DROP VIEW appointments_for_user");
            migrationBuilder.Sql("DROP VIEW musician");
            migrationBuilder.Sql("DROP FUNCTION fn_list_parent_sections");
        }
    }
}
