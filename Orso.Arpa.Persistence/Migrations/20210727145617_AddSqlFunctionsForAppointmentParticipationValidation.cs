using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class AddSqlFunctionsForAppointmentParticipationValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlDirectory = Path.Combine(AppContext.BaseDirectory, "SqlStatements");

            var files = new List<string>
            {
                "IsActivePersonInProjectFunction.sql",
                "IsActivePersonInSectionFunction.sql",
                "ActiveAppointmentsForPersonFunction.sql",
                "IsPersonEligibleForAppointmentFunction.sql"
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
