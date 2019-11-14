using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Configurations;

namespace Orso.Arpa.Persistence.DataAccess
{
    public class ArpaContext : IdentityDbContext<User, Role, Guid>
    {
        public ArpaContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<RehearsalRoom> RehearsalRooms { get; set; }
        public DbSet<SelectValueCategory> SelectValueCategories { get; set; }
        public DbSet<SelectValue> SelectValues { get; set; }
        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SetDeletedQueryFilter(builder);

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }

        private static void SetDeletedQueryFilter(ModelBuilder builder)
        {
            foreach (IMutableEntityType entity in builder.Model.GetEntityTypes())
            {
                if (entity.ClrType.GetProperty(ArpaContextUtility.IsDeletedProperty) != null)
                {
                    builder.Entity(entity.ClrType)
                        .HasQueryFilter(ArpaContextUtility.GetIsDeletedRestriction(entity.ClrType));
                }
            }
        }
    }
}
