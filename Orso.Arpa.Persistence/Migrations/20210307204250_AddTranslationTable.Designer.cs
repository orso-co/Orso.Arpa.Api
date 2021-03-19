// <auto-generated />
using System;
using Localization.SqlLocalizer.DbStringLocalizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orso.Arpa.Persistence.DataAccess;

namespace Orso.Arpa.Persistence.Migrations
{
    [DbContext(typeof(ArpaContext))]
    [Migration("20210307204250_AddTranslationTable")]
    partial class AddTranslationTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("Orso.Arpa.Domain.Entities.Translations", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalizationCulture")
                        .IsRequired()
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("ResourceKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy");

                    b.Property<Guid>("PersonId");

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("Deleted");

                    //b.HasAlternateKey("Key", "LocalizationCulture", "ResourceKey");

                    b.ToTable("Translations");
                });
#pragma warning restore 612, 618
        }
    }
}

