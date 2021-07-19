using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddAndUpdateViewsAndFunctions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlDirectory = Path.Combine(AppContext.BaseDirectory, "SqlStatements");

            var files = new List<string>
            {
                "MusicianView.sql",
                "ListParentSectionsFunction.sql",
                "IsPersonInSectionFunction.sql",
                "IsPersonInProjectFunction.sql",
                "UserAppointmentForSectionView.sql",
                "ActiveUserAppointmentsForSectionView.sql",
                "UserAppointmentForProjectView.sql",
                "ActiveUserAppointmentsForProjectView.sql",
                "IsMusicianProfileInProjectFunction.sql",
                "IsActiveMusicianProfileInProjectFunction.sql",
                "IsMusicianProfileInSectionFunction.sql",
                "IsActiveMusicianProfileInSectionFunction.sql",
                "MusicianProfilesForAppointmentFunction.sql",
                "AppointmentsForPersonFunction.sql",
                "ActiveMusicianProfilesForAppointmentFunction.sql"
            };

            foreach (var file in files)
            {
                var sql = File.ReadAllText(Path.Combine(sqlDirectory, file));
                migrationBuilder.Sql(sql);
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
