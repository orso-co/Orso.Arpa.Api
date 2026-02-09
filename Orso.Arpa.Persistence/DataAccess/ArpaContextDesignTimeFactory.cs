using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.LocalizationDomain.Model;
using Orso.Arpa.Misc;

namespace Orso.Arpa.Persistence.DataAccess;

/// <summary>
/// Design-time factory for EF Core Migrations.
/// This class is used by "dotnet ef migrations add" and "dotnet ef database update".
/// </summary>
public class ArpaContextDesignTimeFactory : IDesignTimeDbContextFactory<ArpaContext>
{
    public ArpaContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ArpaContext>();
        optionsBuilder
            .UseNpgsql("host=localhost;database=orso-arpa;User Id=postgres;Password=postgres;")
            .UseSnakeCaseNamingConvention();

        return new ArpaContext(
            optionsBuilder.Options,
            new DesignTimeTokenAccessor(),
            new DesignTimeDateTimeProvider(),
            DesignTimeTranslationCallBack);
    }

    private static Task DesignTimeTranslationCallBack() => Task.CompletedTask;

    private class DesignTimeTokenAccessor : ITokenAccessor
    {
        public string UserName => "design-time";
        public string DisplayName => "Design Time";
        public Guid UserId => Guid.Empty;
        public Guid PersonId => Guid.Empty;
        public IList<string> GetUserRoles() => [];
    }

    private class DesignTimeDateTimeProvider : IDateTimeProvider
    {
        public DateTime GetUtcNow() => DateTime.UtcNow;
    }
}
