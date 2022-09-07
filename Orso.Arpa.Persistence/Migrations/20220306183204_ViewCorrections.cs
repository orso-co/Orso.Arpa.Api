using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class ViewCorrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlDirectory = Path.Combine(AppContext.BaseDirectory, "SqlStatements");

            var files = new List<string>
            {
                "ActiveUserAppointmentsForProjectView.sql",
                "ActiveUserAppointmentsForSectionView.sql",
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
