using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class InitialSqlServerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    GivenName = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registers_Registers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Registers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SelectValueCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Table = table.Column<string>(nullable: true),
                    Property = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectValueCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicianProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    IsProfessional = table.Column<bool>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    RegisterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicianProfile_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianProfile_Registers_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Registers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SelectValueMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    SelectValueId = table.Column<Guid>(nullable: false),
                    SelectValueCategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectValueMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectValueMapping_SelectValueCategories_SelectValueCategoryId",
                        column: x => x.SelectValueCategoryId,
                        principalTable: "SelectValueCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectValueMapping_SelectValues_SelectValueId",
                        column: x => x.SelectValueId,
                        principalTable: "SelectValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    RegionId = table.Column<Guid>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: true),
                    TypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_SelectValueMapping_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SelectValueMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    GenreId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_SelectValueMapping_GenreId",
                        column: x => x.GenreId,
                        principalTable: "SelectValueMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Projects_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AddressId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venues_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    EndTime = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PublicDetails = table.Column<string>(nullable: true),
                    InternalDetails = table.Column<string>(nullable: true),
                    StatusId = table.Column<Guid>(nullable: true),
                    EmolumentId = table.Column<Guid>(nullable: true),
                    EmolumentPatternId = table.Column<Guid>(nullable: true),
                    VenueId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_SelectValueMapping_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "SelectValueMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_SelectValueMapping_EmolumentId",
                        column: x => x.EmolumentId,
                        principalTable: "SelectValueMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_SelectValueMapping_EmolumentPatternId",
                        column: x => x.EmolumentPatternId,
                        principalTable: "SelectValueMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_SelectValueMapping_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SelectValueMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RehearsalRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    VenueId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RehearsalRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RehearsalRooms_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentParticipation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false),
                    ResultId = table.Column<Guid>(nullable: true),
                    PredictionId = table.Column<Guid>(nullable: true),
                    ExpectationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentParticipation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentParticipation_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentParticipation_SelectValueMapping_ExpectationId",
                        column: x => x.ExpectationId,
                        principalTable: "SelectValueMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppointmentParticipation_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentParticipation_SelectValueMapping_PredictionId",
                        column: x => x.PredictionId,
                        principalTable: "SelectValueMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppointmentParticipation_SelectValueMapping_ResultId",
                        column: x => x.ResultId,
                        principalTable: "SelectValueMapping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectAppointment",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAppointment", x => new { x.ProjectId, x.AppointmentId });
                    table.ForeignKey(
                        name: "FK_ProjectAppointment_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectAppointment_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegisterAppointment",
                columns: table => new
                {
                    RegisterId = table.Column<Guid>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterAppointment", x => new { x.RegisterId, x.AppointmentId });
                    table.ForeignKey(
                        name: "FK_RegisterAppointment_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegisterAppointment_Registers_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Registers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Registers",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Choir", null },
                    { new Guid("308659d6-6014-4d2c-a62a-be75bf202e62"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Orchestra", null },
                    { new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Soloist", null },
                    { new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Other", null },
                    { new Guid("a19fa9af-dcba-48e3-bc21-be2130fa528c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Miscellaneous", null }
                });

            migrationBuilder.InsertData(
                table: "SelectValueCategories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "Property", "Table" },
                values: new object[,]
                {
                    { new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Result", "Result", "AppointmentParticipation" },
                    { new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Prediction Participant", "Prediction", "AppointmentParticipation" },
                    { new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Expectation KBB", "Expectation", "AppointmentParticipation" },
                    { new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Emolument Pattern", "EmolumentPattern", "Appointment" },
                    { new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Emolument", "Emolument", "Appointment" },
                    { new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Category", "Category", "Appointment" },
                    { new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Address Type", "Type", "Address" },
                    { new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Status", "Status", "Appointment" },
                    { new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Genre", "Genre", "Project" }
                });

            migrationBuilder.InsertData(
                table: "SelectValues",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "Description", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("71779748-6d3c-496a-9842-8dc508de6676"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Concert" },
                    { new Guid("4418bfea-0e79-4f76-9e20-527644f654e0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Sectional Rehearsal" },
                    { new Guid("dfe6e73e-9a15-4094-80a5-151a64f3b4db"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "See Comment" },
                    { new Guid("52d67a48-e99f-4c2f-ac9b-0302d5d3e518"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Show" },
                    { new Guid("2567e7be-5a5a-4671-b5ad-765c1e80fd41"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Special Case" },
                    { new Guid("61dd102e-d449-40e1-8c6b-4ead99403ac1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Stage Briefing" },
                    { new Guid("3a6218de-6dfc-4aa9-a2a7-f1da20fd61cb"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Transfer" },
                    { new Guid("a0b98a79-7c74-4093-8f5f-79003cad219a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Voice Formation" },
                    { new Guid("d6848ef8-51c6-44e3-bc29-af1df87afcc1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Watch Show" },
                    { new Guid("5d50c5c3-e85a-4810-ac46-49572e1ca2f5"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Workshop" },
                    { new Guid("75a017d3-dca5-49ec-9bbd-3b01b159ba5b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Yes" },
                    { new Guid("608b5583-a8dc-48d7-8afa-ef87ca0327f0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Private" },
                    { new Guid("db1d2c88-a7b3-41c3-a17f-4fd7fe9faca5"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Work" },
                    { new Guid("43d8eafa-ef3f-4034-8c88-9a0b68c33ab1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Chamber Music" },
                    { new Guid("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Awaiting Poll" },
                    { new Guid("a85738d9-e68e-4584-bac8-ccca8d539636"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Audition" },
                    { new Guid("7c894293-82c2-4320-82f5-f77955feae5a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Assembly" },
                    { new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Ambiguous" },
                    { new Guid("f0f26735-b796-4a70-a20c-92e0e2910bb4"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Absent" },
                    { new Guid("1e60dfdf-e7c9-4378-b1af-dcb53fe20022"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Partly" },
                    { new Guid("c76de830-3746-449a-8f1f-bd5d9233655c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Scheduled" },
                    { new Guid("87a541e7-706a-47f3-99b3-8b2d6de7a134"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Classical Music" },
                    { new Guid("efb2b680-c904-481a-ba7c-9e6a64a998c3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Rehearsal Weekend Choir" },
                    { new Guid("99d192e1-332a-494e-b821-075be14211be"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Refused" },
                    { new Guid("34a52363-4a57-4019-abcf-0c9880246891"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Confirmed" },
                    { new Guid("5b57a267-f331-41df-995a-93b60fc206ff"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Crossover" },
                    { new Guid("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Don't know yet" },
                    { new Guid("5d31f1f7-73fd-42a4-a429-33fab925b15d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Awaiting Scan" },
                    { new Guid("b60d04e0-9841-41c9-9d24-976c8363a33d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Glöckner 2018" },
                    { new Guid("86bf6480-787a-4fe0-9d79-0f8d0d36acc4"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Inapplicable" },
                    { new Guid("a4734d39-cbb9-4635-b3ae-f4e1192a6bd1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Mandatory" },
                    { new Guid("ae6dc389-93eb-4d96-bd66-c61dd81155ea"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Meeting" },
                    { new Guid("88b763ac-8093-4c5d-a881-85be1fb8a24d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "No" },
                    { new Guid("9c0295b7-1b16-4fd6-a7de-ecd724c823b3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Optional" },
                    { new Guid("d91def3e-4c55-42c7-ac56-147846be6bfa"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Orchestra Concert Lump Sum 9€/11€ at 10h" },
                    { new Guid("a10ce98a-b903-4dca-801d-3afb07711877"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Orchestra Concert Lump Sum 9 €/11€ at 12h" },
                    { new Guid("ddb23793-af96-4ea6-9b27-5e2dcfc90b65"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Orchestra Concert Lump Sum 1808" },
                    { new Guid("717a27d5-2ef3-4266-92a8-84b3600115eb"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Orchestra Rehearsal Hourly Rate 9/11" },
                    { new Guid("8f64e072-6523-4158-b92e-5c38c8ebca59"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Choreography Rehearsal" },
                    { new Guid("79de43be-57cc-484f-bff2-57f3ba78dbe9"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Photo Session" },
                    { new Guid("362efd25-e1d2-496d-b7fa-884371a58682"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Pending" },
                    { new Guid("404f1bfd-2819-47c2-a78b-f3dbd4bc8953"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Photo Session" },
                    { new Guid("313445ca-57fa-45f0-8515-325949d60726"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Present" },
                    { new Guid("130f63c3-9d2f-4301-b062-236c78663e3b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Rehearsal" },
                    { new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, "", null, null, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Registers",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Female Voices", new Guid("c2cfb7a0-4981-4dda-b988-8ba74957f6a4") },
                    { new Guid("4599103d-f220-4744-92d1-7c6993e9bda4"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Male Voices", new Guid("c2cfb7a0-4981-4dda-b988-8ba74957f6a4") },
                    { new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Wind Section", new Guid("308659d6-6014-4d2c-a62a-be75bf202e62") },
                    { new Guid("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Strings", new Guid("308659d6-6014-4d2c-a62a-be75bf202e62") },
                    { new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Percussion", new Guid("308659d6-6014-4d2c-a62a-be75bf202e62") }
                });

            migrationBuilder.InsertData(
                table: "SelectValueMapping",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "SelectValueCategoryId", "SelectValueId" },
                values: new object[,]
                {
                    { new Guid("f15b88b2-395d-4195-af25-8b8879640baf"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new Guid("a10ce98a-b903-4dca-801d-3afb07711877") },
                    { new Guid("74278b65-fd54-49d2-9cd2-061dd6318c7d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new Guid("ddb23793-af96-4ea6-9b27-5e2dcfc90b65") },
                    { new Guid("836c69d6-46f1-40d4-b75d-6aa86f9ec066"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new Guid("717a27d5-2ef3-4266-92a8-84b3600115eb") },
                    { new Guid("867622fa-7aa5-43f3-b3ef-5290d1f61734"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"), new Guid("362efd25-e1d2-496d-b7fa-884371a58682") },
                    { new Guid("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"), new Guid("34a52363-4a57-4019-abcf-0c9880246891") },
                    { new Guid("b09bc4a6-06ab-4d45-8b82-7971e662ccb5"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"), new Guid("a4734d39-cbb9-4635-b3ae-f4e1192a6bd1") },
                    { new Guid("d64abb04-dc1c-4e17-bed5-a62196a59c49"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"), new Guid("9c0295b7-1b16-4fd6-a7de-ecd724c823b3") },
                    { new Guid("319d508e-a6e2-437e-b48b-6be51e3459bd"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("75a017d3-dca5-49ec-9bbd-3b01b159ba5b") },
                    { new Guid("c9225a82-0348-41bb-a591-7726f8079cde"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("1e60dfdf-e7c9-4378-b1af-dcb53fe20022") },
                    { new Guid("17d201fc-777b-43bb-9c46-0d07737a8ab7"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("88b763ac-8093-4c5d-a881-85be1fb8a24d") },
                    { new Guid("50e6049b-a9cd-400b-a475-e2563147aebc"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03") },
                    { new Guid("ade78d45-b010-4ed7-87f0-e30e0558f151"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("f0f26735-b796-4a70-a20c-92e0e2910bb4") },
                    { new Guid("104fc525-bb0b-49dc-b2b2-9a8f63e45c92"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new Guid("d91def3e-4c55-42c7-ac56-147846be6bfa") },
                    { new Guid("ff994b2c-a3bd-4676-a974-f53d4fa562ba"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("86bf6480-787a-4fe0-9d79-0f8d0d36acc4") },
                    { new Guid("8b7d7f26-b7e5-42e2-afc1-cedddbae841a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2") },
                    { new Guid("7fb30d45-1faf-4f6a-ac5d-436204ad8e69"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("5d31f1f7-73fd-42a4-a429-33fab925b15d") },
                    { new Guid("36176b7e-0926-43d6-b19a-72838ccd2acd"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("34a52363-4a57-4019-abcf-0c9880246891") },
                    { new Guid("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("c76de830-3746-449a-8f1f-bd5d9233655c") },
                    { new Guid("0126fded-0a82-4b53-85e4-1c04a4f79296"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("99d192e1-332a-494e-b821-075be14211be") },
                    { new Guid("b6cf76a5-ec3f-4e81-9499-174d33bb7249"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2") },
                    { new Guid("4dc9db05-357a-43a6-ba20-f2a9e5033802"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95") },
                    { new Guid("d733e38d-1d80-4054-b654-4ea4a128b0a8"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("87a541e7-706a-47f3-99b3-8b2d6de7a134") },
                    { new Guid("3801aa69-cc4e-4fd5-947c-bfdd4e95d48e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("313445ca-57fa-45f0-8515-325949d60726") },
                    { new Guid("8b51c75f-d597-48ef-8451-5f5fc32d57d1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new Guid("b60d04e0-9841-41c9-9d24-976c8363a33d") },
                    { new Guid("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"), new Guid("88b763ac-8093-4c5d-a881-85be1fb8a24d") },
                    { new Guid("5b936e5f-3743-4cc3-91af-0cc8742c846e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"), new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2") },
                    { new Guid("fb44b625-7086-48e6-bcc6-a004dd472012"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"), new Guid("608b5583-a8dc-48d7-8afa-ef87ca0327f0") },
                    { new Guid("63437ce4-b63b-4558-9f91-1474b896bf1c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"), new Guid("db1d2c88-a7b3-41c3-a17f-4fd7fe9faca5") },
                    { new Guid("f81c698e-0017-41c0-8ff9-78dbaa3d2676"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"), new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0") },
                    { new Guid("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("ae6dc389-93eb-4d96-bd66-c61dd81155ea") },
                    { new Guid("dd4556b3-d8b3-4002-8bde-68e327945916"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("61dd102e-d449-40e1-8c6b-4ead99403ac1") },
                    { new Guid("ac1ccdd4-39aa-4767-95ea-099a829f275b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("8f64e072-6523-4158-b92e-5c38c8ebca59") },
                    { new Guid("d8c99a34-025d-455b-b317-92453da36631"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("52d67a48-e99f-4c2f-ac9b-0302d5d3e518") },
                    { new Guid("e9c79ae9-5498-459d-8852-9f135da7afae"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("404f1bfd-2819-47c2-a78b-f3dbd4bc8953") },
                    { new Guid("0c8af1d2-ae39-464d-9e03-a1487cfd7321"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("71779748-6d3c-496a-9842-8dc508de6676") },
                    { new Guid("cfc62012-4d74-4cf6-a06e-7fc3dbacbff8"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("5d50c5c3-e85a-4810-ac46-49572e1ca2f5") },
                    { new Guid("466aa422-0ef2-4e7f-a6a8-d188d80491f6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("79de43be-57cc-484f-bff2-57f3ba78dbe9") },
                    { new Guid("86672779-5e70-4965-b59c-032086d00914"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("130f63c3-9d2f-4301-b062-236c78663e3b") },
                    { new Guid("5b89cf6e-0194-4e01-bb32-8b1813a51e16"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("efb2b680-c904-481a-ba7c-9e6a64a998c3") },
                    { new Guid("642cc60f-e582-47ed-a40f-ea490dd3d950"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("d6848ef8-51c6-44e3-bc29-af1df87afcc1") },
                    { new Guid("a39a92fe-bea2-40fa-817b-e7272bfc9d4b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("dfe6e73e-9a15-4094-80a5-151a64f3b4db") },
                    { new Guid("b62cc155-f1a9-4904-8e6a-95e85339da83"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("a0b98a79-7c74-4093-8f5f-79003cad219a") },
                    { new Guid("2634c0cc-31d2-4f61-813d-7ec60fc8ab34"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("4418bfea-0e79-4f76-9e20-527644f654e0") },
                    { new Guid("4e9d4a1b-cae7-4002-93a1-cef3f209146b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("3a6218de-6dfc-4aa9-a2a7-f1da20fd61cb") },
                    { new Guid("547b561e-cea7-4296-9b1d-4dd41e0d5179"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("7c894293-82c2-4320-82f5-f77955feae5a") },
                    { new Guid("9cf090a3-680d-4770-b929-0a0d080576a0"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("a85738d9-e68e-4584-bac8-ccca8d539636") },
                    { new Guid("609f9ece-42ce-4cc9-a89b-1fec1ddbc5fe"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0") },
                    { new Guid("88da1c17-9efc-4f69-ba0f-39c76592845b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"), new Guid("75a017d3-dca5-49ec-9bbd-3b01b159ba5b") },
                    { new Guid("e7e78e76-3850-4eb5-892f-d90be6c256a4"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("5b57a267-f331-41df-995a-93b60fc206ff") },
                    { new Guid("bbe90120-55f3-4474-a059-1334d0adaa57"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"), new Guid("2567e7be-5a5a-4671-b5ad-765c1e80fd41") },
                    { new Guid("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("43d8eafa-ef3f-4034-8c88-9a0b68c33ab1") }
                });

            migrationBuilder.InsertData(
                table: "Registers",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("48337b78-70f0-493e-911b-296632b06ef8"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Deep Female Voices", new Guid("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6") },
                    { new Guid("5d469fc5-b3e6-40b8-9fa9-542981083ce3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "High Female Voices", new Guid("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6") },
                    { new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Deep Male Voices", new Guid("4599103d-f220-4744-92d1-7c6993e9bda4") },
                    { new Guid("7924daef-2542-4648-a42f-4c4374ee09db"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "High Male Voices", new Guid("4599103d-f220-4744-92d1-7c6993e9bda4") },
                    { new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Woodwind", new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863") },
                    { new Guid("f4c70178-d069-44dc-8956-7160c5fef52e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Brass", new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863") }
                });

            migrationBuilder.InsertData(
                table: "Registers",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Alto", new Guid("48337b78-70f0-493e-911b-296632b06ef8") },
                    { new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Soprano", new Guid("5d469fc5-b3e6-40b8-9fa9-542981083ce3") },
                    { new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Basso", new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a") },
                    { new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Tenor", new Guid("7924daef-2542-4648-a42f-4c4374ee09db") }
                });

            migrationBuilder.InsertData(
                table: "Registers",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Deleted", "ModifiedAt", "ModifiedBy", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("e809ee90-23f9-44de-b80e-2fddd5ee3683"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Alto 1", new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940") },
                    { new Guid("50dfa2be-85e2-4638-aa53-22dadc97a844"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Alto 2", new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940") },
                    { new Guid("8470ddf0-43ab-477e-b3bc-47ede014b359"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Soprano 1", new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d") },
                    { new Guid("22d7cf92-7b29-4cf1-a6fa-2954377589b4"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Soprano 2", new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d") },
                    { new Guid("bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Basso 1", new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca") },
                    { new Guid("61fa66ec-3103-43fe-800c-930547dff82c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Basso 2", new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca") },
                    { new Guid("3db46ff0-9165-46cc-8f28-6a1d52dee518"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Tenor 1", new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da") },
                    { new Guid("afef89cf-90e1-4d4f-83ab-d2b47e97af0f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, false, null, null, "Tenor 2", new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonId",
                table: "Addresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RegionId",
                table: "Addresses",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TypeId",
                table: "Addresses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentParticipation_AppointmentId",
                table: "AppointmentParticipation",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentParticipation_ExpectationId",
                table: "AppointmentParticipation",
                column: "ExpectationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentParticipation_PersonId",
                table: "AppointmentParticipation",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentParticipation_PredictionId",
                table: "AppointmentParticipation",
                column: "PredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentParticipation_ResultId",
                table: "AppointmentParticipation",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CategoryId",
                table: "Appointments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmolumentId",
                table: "Appointments",
                column: "EmolumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmolumentPatternId",
                table: "Appointments",
                column: "EmolumentPatternId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StatusId",
                table: "Appointments",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VenueId",
                table: "Appointments",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonId",
                table: "AspNetUsers",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfile_PersonId",
                table: "MusicianProfile",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianProfile_RegisterId",
                table: "MusicianProfile",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAppointment_AppointmentId",
                table: "ProjectAppointment",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_GenreId",
                table: "Projects",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ParentId",
                table: "Projects",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterAppointment_AppointmentId",
                table: "RegisterAppointment",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_ParentId",
                table: "Registers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalRooms_VenueId",
                table: "RehearsalRooms",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectValueMapping_SelectValueCategoryId",
                table: "SelectValueMapping",
                column: "SelectValueCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectValueMapping_SelectValueId",
                table: "SelectValueMapping",
                column: "SelectValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_AddressId",
                table: "Venues",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentParticipation");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MusicianProfile");

            migrationBuilder.DropTable(
                name: "ProjectAppointment");

            migrationBuilder.DropTable(
                name: "RegisterAppointment");

            migrationBuilder.DropTable(
                name: "RehearsalRooms");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "SelectValueMapping");

            migrationBuilder.DropTable(
                name: "SelectValueCategories");

            migrationBuilder.DropTable(
                name: "SelectValues");
        }
    }
}
