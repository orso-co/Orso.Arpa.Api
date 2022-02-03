using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Orso.Arpa.Persistence.Migrations
{
    public partial class InitialPostgresMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    level = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timespan = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    keyword = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    details = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    sort_order = table.Column<byte>(type: "smallint", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_credentials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "educations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timespan = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    institution = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    sort_order = table.Column<byte>(type: "smallint", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_educations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    given_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    about_me = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    reliability = table.Column<byte>(type: "smallint", nullable: false),
                    favorization = table.Column<byte>(type: "smallint", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_persons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_regions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_instrument = table.Column<bool>(type: "boolean", nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sections", x => x.id);
                    table.ForeignKey(
                        name: "fk_sections_sections_parent_id",
                        column: x => x.parent_id,
                        principalTable: "sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "select_value_categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    table = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    property = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_select_value_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "select_values",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_select_values", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_users_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "person_sections",
                columns: table => new
                {
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_sections", x => new { x.person_id, x.section_id });
                    table.ForeignKey(
                        name: "fk_person_sections_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_person_sections_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_positions", x => x.id);
                    table.ForeignKey(
                        name: "fk_positions_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "select_value_mappings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_id = table.Column<Guid>(type: "uuid", nullable: false),
                    select_value_category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_select_value_mappings", x => x.id);
                    table.ForeignKey(
                        name: "fk_select_value_mappings_select_value_categories_select_value_",
                        column: x => x.select_value_category_id,
                        principalTable: "select_value_categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_select_value_mappings_select_values_select_value_id",
                        column: x => x.select_value_id,
                        principalTable: "select_values",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    expiry_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by_ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    revoked_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    revoked_by_ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_refresh_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    address1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    address2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    zip = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    urban_district = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    state = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    region_id = table.Column<Guid>(type: "uuid", nullable: true),
                    discriminator = table.Column<string>(type: "text", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: true),
                    type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_addresses_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_addresses_regions_region_id",
                        column: x => x.region_id,
                        principalTable: "regions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_addresses_select_value_mappings_type_id",
                        column: x => x.type_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    level_self_assessment = table.Column<byte>(type: "smallint", nullable: false),
                    level_inner_assessment = table.Column<byte>(type: "smallint", nullable: false),
                    profile_favorizitation = table.Column<byte>(type: "smallint", nullable: false),
                    is_main_profile = table.Column<bool>(type: "boolean", nullable: false),
                    background = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    experience_level = table.Column<byte>(type: "smallint", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    instrument_id = table.Column<Guid>(type: "uuid", nullable: false),
                    qualification_id = table.Column<Guid>(type: "uuid", nullable: true),
                    salary_id = table.Column<Guid>(type: "uuid", nullable: true),
                    inquery_id = table.Column<Guid>(type: "uuid", nullable: true),
                    preferred_position_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_musician_profiles_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profiles_positions_preferred_position_id",
                        column: x => x.preferred_position_id,
                        principalTable: "positions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profiles_sections_instrument_id",
                        column: x => x.instrument_id,
                        principalTable: "sections",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profiles_select_value_mappings_inquery_id",
                        column: x => x.inquery_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profiles_select_value_mappings_qualification_id",
                        column: x => x.qualification_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profiles_select_value_mappings_salary_id",
                        column: x => x.salary_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    short_title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    genre_id = table.Column<Guid>(type: "uuid", nullable: true),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    state_id = table.Column<Guid>(type: "uuid", nullable: true),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    is_completed = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
                    table.ForeignKey(
                        name: "fk_projects_projects_parent_id",
                        column: x => x.parent_id,
                        principalTable: "projects",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_projects_select_value_mappings_genre_id",
                        column: x => x.genre_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_projects_select_value_mappings_state_id",
                        column: x => x.state_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_projects_select_value_mappings_type_id",
                        column: x => x.type_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "venues",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    address_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_venues", x => x.id);
                    table.ForeignKey(
                        name: "fk_venues_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "available_documents",
                columns: table => new
                {
                    select_value_mapping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_available_documents", x => new { x.musician_profile_id, x.select_value_mapping_id });
                    table.ForeignKey(
                        name: "fk_available_documents_musician_profiles_musician_profile_id",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_available_documents_select_value_mappings_select_value_mapp",
                        column: x => x.select_value_mapping_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profile_credentials",
                columns: table => new
                {
                    credential_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_credentials", x => new { x.musician_profile_id, x.credential_id });
                    table.ForeignKey(
                        name: "fk_musician_profile_credentials_credentials_credential_id",
                        column: x => x.credential_id,
                        principalTable: "credentials",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_credentials_musician_profiles_musician_pro",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profile_educations",
                columns: table => new
                {
                    education_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_educations", x => new { x.musician_profile_id, x.education_id });
                    table.ForeignKey(
                        name: "fk_musician_profile_educations_educations_education_id",
                        column: x => x.education_id,
                        principalTable: "educations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_educations_musician_profiles_musician_prof",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "musician_profile_sections",
                columns: table => new
                {
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_musician_profile_sections", x => new { x.musician_profile_id, x.section_id });
                    table.ForeignKey(
                        name: "fk_musician_profile_sections_musician_profiles_musician_profil",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_musician_profile_sections_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "preferred_genre",
                columns: table => new
                {
                    select_value_mapping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preferred_genre", x => new { x.musician_profile_id, x.select_value_mapping_id });
                    table.ForeignKey(
                        name: "fk_preferred_genre_musician_profiles_musician_profile_id",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_preferred_genre_select_value_mappings_select_value_mapping_",
                        column: x => x.select_value_mapping_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "project_participations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_participations", x => x.id);
                    table.ForeignKey(
                        name: "fk_project_participations_musician_profiles_musician_profile_id",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_project_participations_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "urls",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    href = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    anchor_text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_urls", x => x.id);
                    table.ForeignKey(
                        name: "fk_urls_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    public_details = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    internal_details = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status_id = table.Column<Guid>(type: "uuid", nullable: true),
                    emolument_id = table.Column<Guid>(type: "uuid", nullable: true),
                    emolument_pattern_id = table.Column<Guid>(type: "uuid", nullable: true),
                    venue_id = table.Column<Guid>(type: "uuid", nullable: true),
                    expectation_id = table.Column<Guid>(type: "uuid", nullable: true),
                    audition_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_appointments", x => x.id);
                    table.ForeignKey(
                        name: "fk_appointments_select_value_mappings_category_id",
                        column: x => x.category_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointments_select_value_mappings_emolument_id",
                        column: x => x.emolument_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointments_select_value_mappings_emolument_pattern_id",
                        column: x => x.emolument_pattern_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointments_select_value_mappings_expectation_id",
                        column: x => x.expectation_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointments_select_value_mappings_status_id",
                        column: x => x.status_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointments_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    building = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    floor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rooms", x => x.id);
                    table.ForeignKey(
                        name: "fk_rooms_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sphere_of_activity_concerts",
                columns: table => new
                {
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<byte>(type: "smallint", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sphere_of_activity_concerts", x => new { x.musician_profile_id, x.venue_id });
                    table.ForeignKey(
                        name: "fk_sphere_of_activity_concerts_musician_profiles_musician_prof",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_sphere_of_activity_concerts_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sphere_of_activity_rehearsals",
                columns: table => new
                {
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    musician_profile_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<byte>(type: "smallint", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sphere_of_activity_rehearsals", x => new { x.musician_profile_id, x.venue_id });
                    table.ForeignKey(
                        name: "fk_sphere_of_activity_rehearsals_musician_profiles_musician_pr",
                        column: x => x.musician_profile_id,
                        principalTable: "musician_profiles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_sphere_of_activity_rehearsals_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "url_roles",
                columns: table => new
                {
                    url_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_url_roles", x => new { x.url_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_url_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_url_roles_urls_url_id",
                        column: x => x.url_id,
                        principalTable: "urls",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "appointment_participations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    appointment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    result_id = table.Column<Guid>(type: "uuid", nullable: true),
                    prediction_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_appointment_participations", x => x.id);
                    table.ForeignKey(
                        name: "fk_appointment_participations_appointments_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "appointments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointment_participations_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointment_participations_select_value_mappings_prediction",
                        column: x => x.prediction_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointment_participations_select_value_mappings_result_id",
                        column: x => x.result_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "auditions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    appointment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status_id = table.Column<Guid>(type: "uuid", nullable: true),
                    repetitor_status_id = table.Column<Guid>(type: "uuid", nullable: true),
                    inner_comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    internal_comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    repertoire = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auditions", x => x.id);
                    table.ForeignKey(
                        name: "fk_auditions_appointments_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "appointments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_auditions_select_value_mappings_repetitor_status_id",
                        column: x => x.repetitor_status_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_auditions_select_value_mappings_status_id",
                        column: x => x.status_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "project_appointments",
                columns: table => new
                {
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    appointment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_appointments", x => new { x.project_id, x.appointment_id });
                    table.ForeignKey(
                        name: "fk_project_appointments_appointments_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "appointments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_project_appointments_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "section_appointments",
                columns: table => new
                {
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    appointment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_section_appointments", x => new { x.section_id, x.appointment_id });
                    table.ForeignKey(
                        name: "fk_section_appointments_appointments_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "appointments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_section_appointments_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "appointment_rooms",
                columns: table => new
                {
                    room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    appointment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_appointment_rooms", x => new { x.appointment_id, x.room_id });
                    table.ForeignKey(
                        name: "fk_appointment_rooms_appointments_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "appointments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_appointment_rooms_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "persons",
                columns: new[] { "id", "about_me", "created_at", "created_by", "deleted", "favorization", "given_name", "modified_at", "modified_by", "reliability", "surname" },
                values: new object[] { new Guid("56ed7c20-ba78-4a02-936e-5e840ef0748c"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, (byte)0, "Initial", null, null, (byte)0, "Admin" });

            migrationBuilder.InsertData(
                table: "regions",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("3e6c559e-8d50-488d-a1ea-5dbc0f44ba9b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Freiburg" },
                    { new Guid("ac9544e3-e756-486c-a1dc-62988a882ac2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Stuttgart" },
                    { new Guid("ca3c9cce-1aee-4c50-93e1-be963542741a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Berlin" }
                });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "is_instrument", "modified_at", "modified_by", "name", "parent_id" },
                values: new object[,]
                {
                    { new Guid("6a107070-daae-41fc-b27d-416d44d36374"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Contractors", null },
                    { new Guid("75f593aa-fd20-4c05-9300-b31dbb90712e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Volunteers", null },
                    { new Guid("13802d8b-4c73-4a52-8748-20bf3ba0c2b1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Suppliers", null },
                    { new Guid("067647c0-3f25-449e-9212-03f39fa88f0f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Members", null },
                    { new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Performers", null },
                    { new Guid("b58d047f-ec04-41e9-a728-06a8a160f55b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Visitors", null }
                });

            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[,]
                {
                    { new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Result", "Result", "AppointmentParticipation" },
                    { new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Category", "Category", "Appointment" },
                    { new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Emolument", "Emolument", "Appointment" },
                    { new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Emolument Pattern", "EmolumentPattern", "Appointment" },
                    { new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Expectation KBB", "Expectation", "Appointment" },
                    { new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Prediction Participant", "Prediction", "AppointmentParticipation" },
                    { new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Status", "Status", "Appointment" },
                    { new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Salary", "Salary", "MusicianProfile" },
                    { new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Type", "Type", "Project" },
                    { new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "State", "State", "Project" },
                    { new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Qualification", "Qualification", "MusicianProfile" },
                    { new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Address Type", "Type", "PersonAddress" },
                    { new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Inquery", "Inquery", "MusicianProfile" },
                    { new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Available document status", "AvailableDocumentStatus", "MusicianProfile" },
                    { new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Genre", "Genre", "Project" },
                    { new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Status", "Status", "Audition" },
                    { new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Repetitor status", "RepetitorStatus", "Audition" }
                });

            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    { new Guid("5b57a267-f331-41df-995a-93b60fc206ff"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Crossover" },
                    { new Guid("6307ec0e-482a-4777-8b2e-4e8cd5d1f252"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Unnecessary" },
                    { new Guid("0141e712-7080-4e3d-8145-44a3080aa274"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Brings pianist" },
                    { new Guid("6bdf5666-65ef-475a-9c48-9a38f18de041"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "No pianist needed" },
                    { new Guid("45d534e3-6605-42f0-ae57-1a943e18a9cd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Is asking for pianist" },
                    { new Guid("42f546ab-1b96-4eab-88a4-753cad8392c1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Awaiting" },
                    { new Guid("33e57595-2166-4cce-aa34-60d7148ae9f7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Failed" },
                    { new Guid("166edc65-9915-4836-b0a3-3c60ad0bcc04"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Passed" },
                    { new Guid("c0911d95-0c6d-4834-840c-43cddf3c51a0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "CV" },
                    { new Guid("2ecfb104-feb3-406a-b741-0ac9fdd3e8d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Contemporary Music" },
                    { new Guid("a3be7b91-7548-492e-99dc-2788497f2930"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Film Music" },
                    { new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "For contacts only" },
                    { new Guid("5db547d6-c115-4409-8db7-59374ca2af83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Never again" },
                    { new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Emergency only" },
                    { new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Gladly" },
                    { new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "With - negotiable" },
                    { new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "With - strict" },
                    { new Guid("982a9947-c6f8-4c9a-b96f-2a4825a11496"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Dance Performance" },
                    { new Guid("0cf5b2e2-4f01-441a-adc8-a975c7494fd7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Letter of recommendation" },
                    { new Guid("c1951202-0e6e-41f7-bf07-5cefe47efade"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Diploma" },
                    { new Guid("e340f76d-074b-40e8-85b0-1bb66a596a06"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Photo" },
                    { new Guid("313445ca-57fa-45f0-8515-325949d60726"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Present" },
                    { new Guid("f0f26735-b796-4a70-a20c-92e0e2910bb4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Absent" },
                    { new Guid("86bf6480-787a-4fe0-9d79-0f8d0d36acc4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Inapplicable" },
                    { new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Ambiguous" },
                    { new Guid("5d31f1f7-73fd-42a4-a429-33fab925b15d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Awaiting Scan" },
                    { new Guid("75a017d3-dca5-49ec-9bbd-3b01b159ba5b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Yes" },
                    { new Guid("88b763ac-8093-4c5d-a881-85be1fb8a24d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "No" },
                    { new Guid("1e60dfdf-e7c9-4378-b1af-dcb53fe20022"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Partly" },
                    { new Guid("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Don't know yet" },
                    { new Guid("362efd25-e1d2-496d-b7fa-884371a58682"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Pending" },
                    { new Guid("34a52363-4a57-4019-abcf-0c9880246891"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Confirmed" },
                    { new Guid("33bbdccf-59a9-4b05-bdac-af50137cecb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Cancelled" },
                    { new Guid("bd0f37e1-ec14-4d87-8380-117b4480d7a4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Postponed" },
                    { new Guid("425f1526-0513-4535-bdd8-47632d82956f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Archived" },
                    { new Guid("a4734d39-cbb9-4635-b3ae-f4e1192a6bd1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Mandatory" },
                    { new Guid("3550443d-5acf-4159-bd59-d7da04dd9434"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Audio" },
                    { new Guid("d075dda3-ba29-472b-a699-1f92c1af13a9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Video" },
                    { new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Without" },
                    { new Guid("87a541e7-706a-47f3-99b3-8b2d6de7a134"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Classical Music" },
                    { new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Unknown" },
                    { new Guid("35d63f30-8704-47d5-865a-ee713a082433"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Semi-Professional" },
                    { new Guid("dfe6e73e-9a15-4094-80a5-151a64f3b4db"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "See Comment" },
                    { new Guid("d6848ef8-51c6-44e3-bc29-af1df87afcc1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Watch Show" },
                    { new Guid("52d67a48-e99f-4c2f-ac9b-0302d5d3e518"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Show" },
                    { new Guid("efb2b680-c904-481a-ba7c-9e6a64a998c3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Rehearsal Weekend Choir" },
                    { new Guid("130f63c3-9d2f-4301-b062-236c78663e3b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Rehearsal" },
                    { new Guid("79de43be-57cc-484f-bff2-57f3ba78dbe9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Photo Session" },
                    { new Guid("5d50c5c3-e85a-4810-ac46-49572e1ca2f5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Workshop" },
                    { new Guid("a0b98a79-7c74-4093-8f5f-79003cad219a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Voice Formation" },
                    { new Guid("71779748-6d3c-496a-9842-8dc508de6676"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Concert" },
                    { new Guid("8f64e072-6523-4158-b92e-5c38c8ebca59"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Choreography Rehearsal" },
                    { new Guid("61dd102e-d449-40e1-8c6b-4ead99403ac1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Stage Briefing" },
                    { new Guid("ae6dc389-93eb-4d96-bd66-c61dd81155ea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Meeting" },
                    { new Guid("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Awaiting Poll" },
                    { new Guid("99d192e1-332a-494e-b821-075be14211be"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Refused" },
                    { new Guid("c76de830-3746-449a-8f1f-bd5d9233655c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Scheduled" },
                    { new Guid("43d8eafa-ef3f-4034-8c88-9a0b68c33ab1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Chamber Music" },
                    { new Guid("404f1bfd-2819-47c2-a78b-f3dbd4bc8953"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Photo Session" },
                    { new Guid("4418bfea-0e79-4f76-9e20-527644f654e0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Sectional Rehearsal" },
                    { new Guid("3a6218de-6dfc-4aa9-a2a7-f1da20fd61cb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Transfer" },
                    { new Guid("7c894293-82c2-4320-82f5-f77955feae5a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Assembly" },
                    { new Guid("e20ff004-aafc-4e28-87f9-0d9c6372951c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Student" },
                    { new Guid("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Amateur" },
                    { new Guid("95de5380-4027-4b73-b4db-3697aba5ba38"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Contest" },
                    { new Guid("52fad37d-23a7-4515-9b77-3ee3bda03b9a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "CD recording" },
                    { new Guid("f2a6ef3d-bb32-4505-83a5-2cb9f611ce0f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Special project" },
                    { new Guid("63a6b9a9-30a8-4cdb-983b-336b587069cb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Rehearsal weekend" },
                    { new Guid("7f6b69f3-4fe8-4b0c-a586-38a661c60af5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Concert tour" },
                    { new Guid("db1d2c88-a7b3-41c3-a17f-4fd7fe9faca5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Work" },
                    { new Guid("608b5583-a8dc-48d7-8afa-ef87ca0327f0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Private" },
                    { new Guid("717a27d5-2ef3-4266-92a8-84b3600115eb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Orchestra Rehearsal Hourly Rate 9/11" },
                    { new Guid("a10ce98a-b903-4dca-801d-3afb07711877"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Orchestra Concert Rate: 9€/11€ at 12h" },
                    { new Guid("9c0295b7-1b16-4fd6-a7de-ecd724c823b3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Optional" },
                    { new Guid("ddb23793-af96-4ea6-9b27-5e2dcfc90b65"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Orchestra Concert Rate: 1808" },
                    { new Guid("b60d04e0-9841-41c9-9d24-976c8363a33d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Glöckner 2018" },
                    { new Guid("2567e7be-5a5a-4671-b5ad-765c1e80fd41"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Special Case" },
                    { new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Other" },
                    { new Guid("a85738d9-e68e-4584-bac8-ccca8d539636"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Audition" },
                    { new Guid("f52b9170-c6f6-4828-b96c-df5dfbe9bd73"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Professional" },
                    { new Guid("d91def3e-4c55-42c7-ac56-147846be6bfa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Orchestra Concert Rate: 9€/11€ at 10h" }
                });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "is_instrument", "modified_at", "modified_by", "name", "parent_id" },
                values: new object[,]
                {
                    { new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Conductor", new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668") },
                    { new Guid("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Choir", new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668") },
                    { new Guid("308659d6-6014-4d2c-a62a-be75bf202e62"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Orchestra", new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668") },
                    { new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Band", new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668") },
                    { new Guid("e0fdb057-c9b7-4477-be75-cbf920a26af6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Soloists", new Guid("8bba816f-2315-43c0-b18e-99a27b1c9668") }
                });

            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id" },
                values: new object[,]
                {
                    { new Guid("30f592f6-485a-468a-bfb2-4854be733e74"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("f52b9170-c6f6-4828-b96c-df5dfbe9bd73") },
                    { new Guid("20f9698c-2f3d-41fd-9f33-1feeababfb1c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("35d63f30-8704-47d5-865a-ee713a082433") },
                    { new Guid("6304b935-633d-4bba-a90f-9bd864c867c6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("e20ff004-aafc-4e28-87f9-0d9c6372951c") },
                    { new Guid("f036bca9-95d4-4526-b845-fff9208ab103"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("3f93768e-ac24-4741-9eb8-49d1e8e4a6e1") },
                    { new Guid("75f2d1c3-4ba2-4acc-8fd3-6b01ca66d1c9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("425f1526-0513-4535-bdd8-47632d82956f") },
                    { new Guid("bc29bf0a-2ebb-4db8-8765-a5f835492552"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("bd0f37e1-ec14-4d87-8380-117b4480d7a4") },
                    { new Guid("65975857-ab27-480d-87c3-dba74d45cb63"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("33bbdccf-59a9-4b05-bdac-af50137cecb3") },
                    { new Guid("b793fa86-2025-4258-8993-8045f4c312d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("34a52363-4a57-4019-abcf-0c9880246891") },
                    { new Guid("725a4f4a-37cb-46ba-93a3-7b9cc2b015cb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9804d695-d8c7-40bd-814f-8458b55fb583"), new Guid("362efd25-e1d2-496d-b7fa-884371a58682") },
                    { new Guid("5c3f5e18-7afd-4404-98db-658e852901dc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new Guid("95de5380-4027-4b73-b4db-3697aba5ba38") },
                    { new Guid("d8f337d0-84fc-4a4d-b75c-fbe2208808ea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new Guid("63a6b9a9-30a8-4cdb-983b-336b587069cb") },
                    { new Guid("574e0c4b-cbb3-4750-926b-3df4c377fc5e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new Guid("f2a6ef3d-bb32-4505-83a5-2cb9f611ce0f") },
                    { new Guid("42d76464-4b4b-4884-b8e3-1f69baaced4c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9648daa0-c2b2-4b97-912b-7ce30b9534a8"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("3f166c3c-c85e-404b-aad3-c8996f4fb75f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new Guid("130f63c3-9d2f-4301-b062-236c78663e3b") },
                    { new Guid("44710a6b-93c0-4aac-b552-e0423f1b106a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new Guid("79de43be-57cc-484f-bff2-57f3ba78dbe9") },
                    { new Guid("ae2f10ff-39ae-427e-a5e8-ddcd89422d44"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new Guid("5d50c5c3-e85a-4810-ac46-49572e1ca2f5") },
                    { new Guid("7f76d426-cab7-4f4f-aba3-bd430bcec003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new Guid("7f6b69f3-4fe8-4b0c-a586-38a661c60af5") },
                    { new Guid("34f05f05-ef23-4f36-94e7-73b917530c51"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new Guid("71779748-6d3c-496a-9842-8dc508de6676") },
                    { new Guid("4ef47024-d8a5-4b2d-8584-aeb29263dddb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("2ecfb104-feb3-406a-b741-0ac9fdd3e8d7") },
                    { new Guid("8daa5ae4-3885-4739-803a-693c7cfdf314"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("982a9947-c6f8-4c9a-b96f-2a4825a11496") },
                    { new Guid("5578f637-14b7-4c11-85a8-0b94d83da678"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("a3be7b91-7548-492e-99dc-2788497f2930") },
                    { new Guid("679116ec-7840-4c6d-bb45-fa2d89d6e779"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("53ed1791-36d7-4534-867c-15175e6f4584"), new Guid("52fad37d-23a7-4515-9b77-3ee3bda03b9a") },
                    { new Guid("58a0d16f-2dac-4836-930e-1dd320430ef4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("3c014654-b4c9-4c7a-a251-ae88ad504c8a") },
                    { new Guid("2fbb99cd-d14a-4b7c-b7fb-9b676f961e2c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("d53b4a35-f472-42a1-ab22-c7afb1e7d77e") },
                    { new Guid("29e1142f-aa9e-4b94-ae21-9a63f7b65c15"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("43d8eafa-ef3f-4034-8c88-9a0b68c33ab1") },
                    { new Guid("9808c1f6-0bbd-4054-acca-779d56a8a934"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("0141e712-7080-4e3d-8145-44a3080aa274") },
                    { new Guid("a88b874f-9879-482f-85ec-1ddda9bb545c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("45d534e3-6605-42f0-ae57-1a943e18a9cd") },
                    { new Guid("24c5bbe1-37eb-4368-ac7c-a6061058bbef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("3acd5be1-5fbc-4de4-a45c-2e230c413c85"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("6307ec0e-482a-4777-8b2e-4e8cd5d1f252") },
                    { new Guid("fab42540-8c9d-4b18-9341-660f60dd7644"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("33bbdccf-59a9-4b05-bdac-af50137cecb3") },
                    { new Guid("0e997440-53f2-4823-8581-4d4716525885"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("42f546ab-1b96-4eab-88a4-753cad8392c1") },
                    { new Guid("7b8defe6-9922-43d6-8df0-3a73f47d6980"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("33e57595-2166-4cce-aa34-60d7148ae9f7") },
                    { new Guid("be152c92-b807-4850-8327-9d1916dabead"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("072c2a9a-a492-4190-9a49-505ff7056528"), new Guid("166edc65-9915-4836-b0a3-3c60ad0bcc04") },
                    { new Guid("4298e1f5-ea1d-4a83-9b32-e5dc3a7cbca9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0") },
                    { new Guid("887e7e2e-0c90-4c4c-9504-3f2a5af7fbcb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("e340f76d-074b-40e8-85b0-1bb66a596a06") },
                    { new Guid("f1626a63-6bf1-442a-86ad-8a86242bde94"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("d075dda3-ba29-472b-a699-1f92c1af13a9") },
                    { new Guid("edfad6f1-6584-4798-a09a-9f6146127d82"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("3550443d-5acf-4159-bd59-d7da04dd9434") },
                    { new Guid("1b53d96a-f9a1-4037-b103-f7aae9b278d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("c1951202-0e6e-41f7-bf07-5cefe47efade") },
                    { new Guid("a3e5843b-05c3-452c-a29d-da8de738181a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("0cf5b2e2-4f01-441a-adc8-a975c7494fd7") },
                    { new Guid("f9cc5445-8a6e-480b-bffb-410089f55896"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("c4ff62bb-9f40-4499-b237-d7b87b2b36f7"), new Guid("c0911d95-0c6d-4834-840c-43cddf3c51a0") },
                    { new Guid("90b5cfa9-890b-4b89-a750-646f3a26db23"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("0d1073cd-f6d5-4572-87ac-98ab6f15c05a") },
                    { new Guid("a15014ad-582e-4388-9b58-aceb52cf41bf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("ab5c5904-2683-47c4-a436-319303b8e62f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("5db547d6-c115-4409-8db7-59374ca2af83") },
                    { new Guid("60c1a391-59b4-4cea-ba83-59e09f7512b6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("5850e103-4ac9-472e-85f2-cddc08732ccc") },
                    { new Guid("68e947c0-9450-4b64-90d7-553850396a3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d1ca913c-dee7-46d8-9fd4-ea564af8005f"), new Guid("1f0e9a86-4641-4d7e-8413-a1beba0e8afb") },
                    { new Guid("d80bf2be-de2f-4d72-ba02-6081b5ba77d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") },
                    { new Guid("459dc1ea-de92-4a26-9b7b-848d67154cae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("9c6b7ba0-f672-433f-b1e3-a80a2eb0a3ea"), new Guid("dec26aef-f0de-4c9f-a164-e23e2543c987") },
                    { new Guid("e7e78e76-3850-4eb5-892f-d90be6c256a4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("5b57a267-f331-41df-995a-93b60fc206ff") },
                    { new Guid("b6cf76a5-ec3f-4e81-9499-174d33bb7249"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2") },
                    { new Guid("4dc9db05-357a-43a6-ba20-f2a9e5033802"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("5e3edcf4-863b-433b-ae72-b6bb7e4dfc95") },
                    { new Guid("609f9ece-42ce-4cc9-a89b-1fec1ddbc5fe"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0") },
                    { new Guid("9cf090a3-680d-4770-b929-0a0d080576a0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("a85738d9-e68e-4584-bac8-ccca8d539636") },
                    { new Guid("547b561e-cea7-4296-9b1d-4dd41e0d5179"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("7c894293-82c2-4320-82f5-f77955feae5a") },
                    { new Guid("4e9d4a1b-cae7-4002-93a1-cef3f209146b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("3a6218de-6dfc-4aa9-a2a7-f1da20fd61cb") },
                    { new Guid("2634c0cc-31d2-4f61-813d-7ec60fc8ab34"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("4418bfea-0e79-4f76-9e20-527644f654e0") },
                    { new Guid("b62cc155-f1a9-4904-8e6a-95e85339da83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("a0b98a79-7c74-4093-8f5f-79003cad219a") },
                    { new Guid("a39a92fe-bea2-40fa-817b-e7272bfc9d4b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("dfe6e73e-9a15-4094-80a5-151a64f3b4db") },
                    { new Guid("642cc60f-e582-47ed-a40f-ea490dd3d950"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("d6848ef8-51c6-44e3-bc29-af1df87afcc1") },
                    { new Guid("5b89cf6e-0194-4e01-bb32-8b1813a51e16"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("efb2b680-c904-481a-ba7c-9e6a64a998c3") },
                    { new Guid("86672779-5e70-4965-b59c-032086d00914"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("130f63c3-9d2f-4301-b062-236c78663e3b") },
                    { new Guid("466aa422-0ef2-4e7f-a6a8-d188d80491f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("79de43be-57cc-484f-bff2-57f3ba78dbe9") },
                    { new Guid("cfc62012-4d74-4cf6-a06e-7fc3dbacbff8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("5d50c5c3-e85a-4810-ac46-49572e1ca2f5") },
                    { new Guid("0c8af1d2-ae39-464d-9e03-a1487cfd7321"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("71779748-6d3c-496a-9842-8dc508de6676") },
                    { new Guid("e9c79ae9-5498-459d-8852-9f135da7afae"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("404f1bfd-2819-47c2-a78b-f3dbd4bc8953") },
                    { new Guid("d8c99a34-025d-455b-b317-92453da36631"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("52d67a48-e99f-4c2f-ac9b-0302d5d3e518") },
                    { new Guid("ac1ccdd4-39aa-4767-95ea-099a829f275b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("8f64e072-6523-4158-b92e-5c38c8ebca59") },
                    { new Guid("dd4556b3-d8b3-4002-8bde-68e327945916"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("61dd102e-d449-40e1-8c6b-4ead99403ac1") },
                    { new Guid("c1b6d08b-f31e-4f38-a8c0-761e42fbd2b7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("791c7439-c72a-47ca-ad8d-193e2cad09e1"), new Guid("ae6dc389-93eb-4d96-bd66-c61dd81155ea") },
                    { new Guid("f81c698e-0017-41c0-8ff9-78dbaa3d2676"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"), new Guid("e030b53e-3615-4cd6-9fe6-0d818632a4b0") },
                    { new Guid("63437ce4-b63b-4558-9f91-1474b896bf1c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"), new Guid("db1d2c88-a7b3-41c3-a17f-4fd7fe9faca5") },
                    { new Guid("fb44b625-7086-48e6-bcc6-a004dd472012"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("d438c160-0588-41fa-93c3-cd33c0f97063"), new Guid("608b5583-a8dc-48d7-8afa-ef87ca0327f0") },
                    { new Guid("88da1c17-9efc-4f69-ba0f-39c76592845b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"), new Guid("75a017d3-dca5-49ec-9bbd-3b01b159ba5b") },
                    { new Guid("aedc27f3-e2e8-4368-ad69-1ab1c3dd7974"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"), new Guid("88b763ac-8093-4c5d-a881-85be1fb8a24d") },
                    { new Guid("5b936e5f-3743-4cc3-91af-0cc8742c846e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"), new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2") },
                    { new Guid("bbe90120-55f3-4474-a059-1334d0adaa57"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("1d62ed51-c99e-4b55-83d7-f9f9a5b8234e"), new Guid("2567e7be-5a5a-4671-b5ad-765c1e80fd41") },
                    { new Guid("0d1b888f-0f45-4f02-806b-480d5594bd27"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("6bdf5666-65ef-475a-9c48-9a38f18de041") },
                    { new Guid("0126fded-0a82-4b53-85e4-1c04a4f79296"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("99d192e1-332a-494e-b821-075be14211be") },
                    { new Guid("93033f7e-a3c1-45e3-8a17-021d0a4abe5a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("c76de830-3746-449a-8f1f-bd5d9233655c") },
                    { new Guid("36176b7e-0926-43d6-b19a-72838ccd2acd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("09be8eff-72e4-40a8-a1ed-717deb185a69"), new Guid("34a52363-4a57-4019-abcf-0c9880246891") },
                    { new Guid("7fb30d45-1faf-4f6a-ac5d-436204ad8e69"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("5d31f1f7-73fd-42a4-a429-33fab925b15d") },
                    { new Guid("8b7d7f26-b7e5-42e2-afc1-cedddbae841a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("66a6446a-7191-4f14-9c5d-052891b9c5a2") },
                    { new Guid("ff994b2c-a3bd-4676-a974-f53d4fa562ba"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("86bf6480-787a-4fe0-9d79-0f8d0d36acc4") },
                    { new Guid("ade78d45-b010-4ed7-87f0-e30e0558f151"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("f0f26735-b796-4a70-a20c-92e0e2910bb4") },
                    { new Guid("3801aa69-cc4e-4fd5-947c-bfdd4e95d48e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("f5d4763e-5862-4b62-ab92-2748ad213b10"), new Guid("313445ca-57fa-45f0-8515-325949d60726") },
                    { new Guid("50e6049b-a9cd-400b-a475-e2563147aebc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("4ee7d317-6d71-4d6e-b45a-954c8c7dcf03") },
                    { new Guid("d733e38d-1d80-4054-b654-4ea4a128b0a8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("4649b6b9-1362-41c2-ac5c-884f216dff30"), new Guid("87a541e7-706a-47f3-99b3-8b2d6de7a134") },
                    { new Guid("17d201fc-777b-43bb-9c46-0d07737a8ab7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("88b763ac-8093-4c5d-a881-85be1fb8a24d") },
                    { new Guid("319d508e-a6e2-437e-b48b-6be51e3459bd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("75a017d3-dca5-49ec-9bbd-3b01b159ba5b") },
                    { new Guid("d64abb04-dc1c-4e17-bed5-a62196a59c49"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"), new Guid("9c0295b7-1b16-4fd6-a7de-ecd724c823b3") },
                    { new Guid("b09bc4a6-06ab-4d45-8b82-7971e662ccb5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"), new Guid("a4734d39-cbb9-4635-b3ae-f4e1192a6bd1") },
                    { new Guid("647f674a-bc2f-4d3a-9ce4-f0aefa98cd9d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"), new Guid("34a52363-4a57-4019-abcf-0c9880246891") },
                    { new Guid("867622fa-7aa5-43f3-b3ef-5290d1f61734"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("0fdb6218-54fa-4e94-a880-2a65fc1cf9d7"), new Guid("362efd25-e1d2-496d-b7fa-884371a58682") },
                    { new Guid("836c69d6-46f1-40d4-b75d-6aa86f9ec066"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new Guid("717a27d5-2ef3-4266-92a8-84b3600115eb") },
                    { new Guid("74278b65-fd54-49d2-9cd2-061dd6318c7d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new Guid("ddb23793-af96-4ea6-9b27-5e2dcfc90b65") },
                    { new Guid("f15b88b2-395d-4195-af25-8b8879640baf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new Guid("a10ce98a-b903-4dca-801d-3afb07711877") },
                    { new Guid("104fc525-bb0b-49dc-b2b2-9a8f63e45c92"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new Guid("d91def3e-4c55-42c7-ac56-147846be6bfa") },
                    { new Guid("8b51c75f-d597-48ef-8451-5f5fc32d57d1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("e4ff93b9-318e-41ed-b067-51ee94f41adf"), new Guid("b60d04e0-9841-41c9-9d24-976c8363a33d") },
                    { new Guid("c9225a82-0348-41bb-a591-7726f8079cde"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("5cf52155-927f-4d64-a482-348f952bab21"), new Guid("1e60dfdf-e7c9-4378-b1af-dcb53fe20022") },
                    { new Guid("98addc5f-f2fa-4640-8441-d4220b7daea2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("747ef1be-2445-4c3f-8e6c-856ea4aac6b7"), new Guid("b67d1ac5-80ec-4b7d-bcb8-72e3da55f201") }
                });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "is_instrument", "modified_at", "modified_by", "name", "parent_id" },
                values: new object[,]
                {
                    { new Guid("18f1e750-f50d-4f06-8205-21203981bff6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Assistant Conductor", new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2") },
                    { new Guid("6fc908f0-da26-4237-80ca-dfe30453123c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Répétiteur", new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2") },
                    { new Guid("94c42496-fdb6-4341-b82f-735fd1706d39"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Vocal Coach", new Guid("4e7a61c5-d2e4-4e3b-b21d-34a90cf958b2") },
                    { new Guid("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Female Voices", new Guid("c2cfb7a0-4981-4dda-b988-8ba74957f6a4") },
                    { new Guid("4599103d-f220-4744-92d1-7c6993e9bda4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Male Voices", new Guid("c2cfb7a0-4981-4dda-b988-8ba74957f6a4") },
                    { new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Winds", new Guid("308659d6-6014-4d2c-a62a-be75bf202e62") },
                    { new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Percussion", new Guid("308659d6-6014-4d2c-a62a-be75bf202e62") },
                    { new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Others", new Guid("308659d6-6014-4d2c-a62a-be75bf202e62") },
                    { new Guid("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Strings", new Guid("308659d6-6014-4d2c-a62a-be75bf202e62") },
                    { new Guid("48833c1b-cbc1-43b2-a4c5-f1fa4289f5ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Electric Guitar (Band)", new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab") },
                    { new Guid("454c2ad6-e3c8-428a-b74e-c73873159c0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Electric Bass (Band)", new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab") },
                    { new Guid("d787fe9a-2283-43f6-bbc8-8a098e1f1c81"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Drum Set (Band)", new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab") },
                    { new Guid("7f811b88-e7db-461a-af5d-e249b1ce9e7d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Keyboards (Band)", new Guid("1994cb6c-877e-4d7c-aeca-26e68967c2ab") }
                });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "is_instrument", "modified_at", "modified_by", "name", "parent_id" },
                values: new object[,]
                {
                    { new Guid("5d469fc5-b3e6-40b8-9fa9-542981083ce3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "High Female Voices", new Guid("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6") },
                    { new Guid("8903b8c5-0ef8-48fd-9c2b-71fbae827965"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Didgeridoo", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("0031e6f5-2d51-4e88-9e82-7bd2c8340cac"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Bagpipes", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("08bc313b-d0dd-4b78-bdbf-d976682d965e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "GlassHarp", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Guitars", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("d7ff1f62-e5c5-4662-823b-f77ff7706b4e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Bandoneon", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("76891771-b5f2-4666-8972-ba7f494fc9de"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Accordion", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Keyboards", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "High Strings", new Guid("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb") },
                    { new Guid("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Harp", new Guid("c9403ca4-6b75-44c3-b567-e53bbd78fb75") },
                    { new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Mallets", new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c") },
                    { new Guid("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Timpani", new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c") },
                    { new Guid("f4c70178-d069-44dc-8956-7160c5fef52e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Brass", new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863") },
                    { new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Woodwinds", new Guid("b289cfe7-d66e-48d8-83a9-f4b1f7710863") },
                    { new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Low Male Voices", new Guid("4599103d-f220-4744-92d1-7c6993e9bda4") },
                    { new Guid("7924daef-2542-4648-a42f-4c4374ee09db"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "High Male Voices", new Guid("4599103d-f220-4744-92d1-7c6993e9bda4") },
                    { new Guid("48337b78-70f0-493e-911b-296632b06ef8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Low Female Voices", new Guid("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6") },
                    { new Guid("c15c3649-d7bb-4bbf-bdd3-f6146ebc825c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Drum Set (Orchestra)", new Guid("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c") },
                    { new Guid("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Low Strings", new Guid("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb") }
                });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "is_instrument", "modified_at", "modified_by", "name", "parent_id" },
                values: new object[,]
                {
                    { new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Soprano", new Guid("5d469fc5-b3e6-40b8-9fa9-542981083ce3") },
                    { new Guid("df541ea1-a5fd-4975-b6fd-7cd652a79073"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Viola", new Guid("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8") },
                    { new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Violins", new Guid("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8") },
                    { new Guid("9cd74865-f82a-4be9-afc1-384fb25b7fe4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Electric Bass (Orchestra)", new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd") },
                    { new Guid("ed0829d0-d978-430e-96ec-b93cf75f3fd6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Electric Guitar (Orchestra)", new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd") },
                    { new Guid("1d0ed0b3-b87b-439f-932e-616d7e03a0d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Acoustic Guitar (Orchestra)", new Guid("a22b6f19-3e9c-4389-824b-22db7b8cf8fd") },
                    { new Guid("d22fb8aa-7d38-42c4-9586-30e559f63799"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Synthesizer", new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a") },
                    { new Guid("182019da-bde2-44d7-8c77-88cfb0ce428c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Organ", new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a") },
                    { new Guid("f6af00f5-e81c-4d85-aadd-1e33748e9a64"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Cembalo", new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a") },
                    { new Guid("bc6cfeb7-569d-4c22-8e80-647aed560bf0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Celesta", new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a") },
                    { new Guid("8ed82e0e-0354-4192-8f26-5a2437e9208d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Piano", new Guid("614a8fd0-acfa-4268-b716-3b35a6a17b7a") },
                    { new Guid("bb0715dc-7f9d-4ddb-b5f5-9e7806e1069f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Marimbaphone", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("2804ed14-7b73-4e17-bd21-edd048a60cb4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Xylophone", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("d8686f68-78da-4022-b0b8-97e0c263d694"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Violoncello", new Guid("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f") },
                    { new Guid("852d8129-a5b7-4378-ad9c-df89dc878b4f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Vibraphone", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Low Brass", new Guid("f4c70178-d069-44dc-8956-7160c5fef52e") },
                    { new Guid("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "High Brass", new Guid("f4c70178-d069-44dc-8956-7160c5fef52e") },
                    { new Guid("566260fb-b6be-41dc-956d-4070d30fa88d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Saxophone", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Bassoon", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Clarinet", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Oboe", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Flute", new Guid("a6abdeec-8185-40ac-a418-2e422bb9adbd") },
                    { new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Bass", new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a") },
                    { new Guid("bb647161-8394-47d3-9f43-825762a70fc2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Baritone", new Guid("b9673cfd-7cdb-472c-86e0-1304cbb3840a") },
                    { new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Tenor", new Guid("7924daef-2542-4648-a42f-4c4374ee09db") },
                    { new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Alto", new Guid("48337b78-70f0-493e-911b-296632b06ef8") },
                    { new Guid("eb42b2f7-413e-4c1a-ab79-23c74b02d054"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Mezzo Soprano", new Guid("5d469fc5-b3e6-40b8-9fa9-542981083ce3") },
                    { new Guid("dcf267e6-5b58-4534-8e4b-a8c5747b1816"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Glockenspiel", new Guid("d12ebc93-4b55-455c-a9db-a826fca9a1f2") },
                    { new Guid("e45ec6fa-7595-4084-9e01-991746b7f5e9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Double Bass", new Guid("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f") }
                });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "is_instrument", "modified_at", "modified_by", "name", "parent_id" },
                values: new object[,]
                {
                    { new Guid("8470ddf0-43ab-477e-b3bc-47ede014b359"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Soprano 1", new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d") },
                    { new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Tuba", new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041") },
                    { new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Euphonium", new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041") },
                    { new Guid("e20ce055-5715-42f4-97e6-4025559b15f7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Trombone", new Guid("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041") },
                    { new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Trumpet", new Guid("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f") },
                    { new Guid("b9532add-efec-4510-831c-902c32ef7dbb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, null, "Horn", new Guid("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f") },
                    { new Guid("fb4f9841-294a-4b6c-bfec-02d3735b1ea0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Bass Saxophone", new Guid("566260fb-b6be-41dc-956d-4070d30fa88d") },
                    { new Guid("e4622ea3-f6a0-40b2-ac80-a2c9df099aeb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Baritone Saxophone", new Guid("566260fb-b6be-41dc-956d-4070d30fa88d") },
                    { new Guid("da998fcb-92b9-4828-976e-826e97e05cb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Tenor Saxophone", new Guid("566260fb-b6be-41dc-956d-4070d30fa88d") },
                    { new Guid("4a31447d-63c2-4e28-ab39-255a956fbe18"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Alto Saxophone", new Guid("566260fb-b6be-41dc-956d-4070d30fa88d") },
                    { new Guid("b5d01e60-af61-4d29-bfb3-2f0dbac1e2fb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Soprano Saxophone", new Guid("566260fb-b6be-41dc-956d-4070d30fa88d") },
                    { new Guid("7cb00d2e-5a98-4b68-b775-3b5d1f267d96"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Contraforte", new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3") },
                    { new Guid("8d01524c-7c22-4a20-8f26-711d11addbfd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Contra Bassoon", new Guid("5c14f673-13f2-488f-8c21-7286d3ee10c3") },
                    { new Guid("a5cc5e9d-b318-4edc-af84-ff3d701d0bcb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Double Bass Clarinet", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("5109e464-7b01-40bd-a5e0-398ac3d1bb83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Bass Clarinet", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("8c0a80d1-5889-4794-89b6-b80a3828aa5b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Basset Horn", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("be75913a-9703-4a8d-9e07-7a8d32c459f8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Alto Clarinet", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("d2551427-d727-42d9-be0e-dea2ae82f2d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Eb Clarinet", new Guid("cdc390d5-0649-441d-a086-df2e3b9d3512") },
                    { new Guid("22d7cf92-7b29-4cf1-a6fa-2954377589b4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Soprano 2", new Guid("7daa1394-a70d-4a24-88a6-ccf511d75c4d") },
                    { new Guid("e809ee90-23f9-44de-b80e-2fddd5ee3683"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Alto 1", new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940") },
                    { new Guid("50dfa2be-85e2-4638-aa53-22dadc97a844"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Alto 2", new Guid("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940") },
                    { new Guid("3db46ff0-9165-46cc-8f28-6a1d52dee518"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Tenor 1", new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da") },
                    { new Guid("afef89cf-90e1-4d4f-83ab-d2b47e97af0f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Tenor 2", new Guid("1579d7e7-4f55-4532-a078-69fd1ec939da") },
                    { new Guid("bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Bass 1", new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca") },
                    { new Guid("eb5728b5-b1fd-4a70-8894-7bb152087837"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Violin I", new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513") },
                    { new Guid("61fa66ec-3103-43fe-800c-930547dff82c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Bass 2", new Guid("e7dd10ef-1c39-4440-9a6c-65d397f010ca") },
                    { new Guid("f9c1924b-2b45-459c-b919-99059cb41e73"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Alto Flute", new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b") },
                    { new Guid("d0a18a79-ad5a-450d-92cc-20a58496aaf0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Tenor Flute", new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b") },
                    { new Guid("fc66c8b8-d9de-4ff0-a695-37e717103686"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Bass Flute", new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b") },
                    { new Guid("4e71ffc3-e086-4c16-a932-3d80fd302971"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Oboe d'Amore", new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42") },
                    { new Guid("abe0d27b-2c99-4755-891c-fb0b91f19bb6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "English Horn", new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42") },
                    { new Guid("2f8d732f-bf82-4a62-86a1-62bffd708189"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Bariton Oboe", new Guid("2327a9c3-2c6f-41b7-9045-bb00af798b42") },
                    { new Guid("ec8aeaf8-f370-4ac8-bd12-ccce0cbcfa0f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Piccolo Flute", new Guid("d6961f83-e792-4ddf-b91a-ae0867caeb3b") },
                    { new Guid("f3ee3c42-4e4e-411d-a839-6e0420bc35a3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Violin II", new Guid("fab9a49a-9fa4-4af3-9e40-e13bdc930513") }
                });

            migrationBuilder.InsertData(
                table: "sections",
                columns: new[] { "id", "created_at", "created_by", "deleted", "is_instrument", "modified_at", "modified_by", "name", "parent_id" },
                values: new object[,]
                {
                    { new Guid("c42591db-4e41-413f-8b98-6607e2f12e39"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Wagner Tuba", new Guid("b9532add-efec-4510-831c-902c32ef7dbb") },
                    { new Guid("69e64d64-419f-4f9c-9948-a117b02ff198"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Flugelhorn", new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5") },
                    { new Guid("2393549e-5b16-4414-a896-3cebb7bcc9df"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Piccolo Trumpet", new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5") },
                    { new Guid("290f84d4-bb3f-41c3-9f42-c649c8eeea26"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Soprano Cornet", new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5") },
                    { new Guid("305c06e0-b99f-4f91-ae83-869d8b25c63d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Cornet", new Guid("205b0a0e-1a36-48e9-8b45-df37dc5effa5") },
                    { new Guid("80f15184-6417-476a-87ac-0f752d011391"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Alto Trombone", new Guid("e20ce055-5715-42f4-97e6-4025559b15f7") },
                    { new Guid("da660c21-0151-4255-a81b-4d25fede199b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Bass Trombone", new Guid("e20ce055-5715-42f4-97e6-4025559b15f7") },
                    { new Guid("32f3fdd9-9517-4db5-856e-376e9fa52b84"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Double Bass Trombone", new Guid("e20ce055-5715-42f4-97e6-4025559b15f7") },
                    { new Guid("803219aa-1a32-4a68-95ae-348bd487135a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Tenor Horn", new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a") },
                    { new Guid("b525e539-7fa4-49d7-ae93-ec0748022d4d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Baritone Horn", new Guid("554fd3db-110b-4335-bc2a-1d5070f6621a") },
                    { new Guid("2fabd3a1-d398-4108-a74f-2665710133d1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "Eb Tuba", new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9") },
                    { new Guid("31a2b9bf-0c2b-47ec-b8bc-34c9423b74d4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, null, "F Tuba", new Guid("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_person_id",
                table: "addresses",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_region_id",
                table: "addresses",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_type_id",
                table: "addresses",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointment_participations_appointment_id",
                table: "appointment_participations",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointment_participations_person_id",
                table: "appointment_participations",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointment_participations_prediction_id",
                table: "appointment_participations",
                column: "prediction_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointment_participations_result_id",
                table: "appointment_participations",
                column: "result_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointment_rooms_room_id",
                table: "appointment_rooms",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointments_category_id",
                table: "appointments",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointments_emolument_id",
                table: "appointments",
                column: "emolument_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointments_emolument_pattern_id",
                table: "appointments",
                column: "emolument_pattern_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointments_expectation_id",
                table: "appointments",
                column: "expectation_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointments_status_id",
                table: "appointments",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_appointments_venue_id",
                table: "appointments",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_users_person_id",
                table: "AspNetUsers",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_auditions_appointment_id",
                table: "auditions",
                column: "appointment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_auditions_repetitor_status_id",
                table: "auditions",
                column: "repetitor_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_auditions_status_id",
                table: "auditions",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_available_documents_select_value_mapping_id",
                table: "available_documents",
                column: "select_value_mapping_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_credentials_credential_id",
                table: "musician_profile_credentials",
                column: "credential_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_educations_education_id",
                table: "musician_profile_educations",
                column: "education_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profile_sections_section_id",
                table: "musician_profile_sections",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_inquery_id",
                table: "musician_profiles",
                column: "inquery_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_instrument_id",
                table: "musician_profiles",
                column: "instrument_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_person_id",
                table: "musician_profiles",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_preferred_position_id",
                table: "musician_profiles",
                column: "preferred_position_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_qualification_id",
                table: "musician_profiles",
                column: "qualification_id");

            migrationBuilder.CreateIndex(
                name: "ix_musician_profiles_salary_id",
                table: "musician_profiles",
                column: "salary_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_sections_section_id",
                table: "person_sections",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_positions_section_id",
                table: "positions",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "ix_preferred_genre_select_value_mapping_id",
                table: "preferred_genre",
                column: "select_value_mapping_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_appointments_appointment_id",
                table: "project_appointments",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_participations_musician_profile_id",
                table: "project_participations",
                column: "musician_profile_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_participations_project_id",
                table: "project_participations",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_projects_genre_id",
                table: "projects",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "ix_projects_number",
                table: "projects",
                column: "number");

            migrationBuilder.CreateIndex(
                name: "ix_projects_parent_id",
                table: "projects",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_projects_state_id",
                table: "projects",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "ix_projects_type_id",
                table: "projects",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_rooms_venue_id",
                table: "rooms",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_section_appointments_appointment_id",
                table: "section_appointments",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "ix_sections_parent_id",
                table: "sections",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_select_value_categories_property",
                table: "select_value_categories",
                column: "property");

            migrationBuilder.CreateIndex(
                name: "ix_select_value_categories_table",
                table: "select_value_categories",
                column: "table");

            migrationBuilder.CreateIndex(
                name: "ix_select_value_mappings_select_value_category_id",
                table: "select_value_mappings",
                column: "select_value_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_select_value_mappings_select_value_id",
                table: "select_value_mappings",
                column: "select_value_id");

            migrationBuilder.CreateIndex(
                name: "ix_sphere_of_activity_concerts_venue_id",
                table: "sphere_of_activity_concerts",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_sphere_of_activity_rehearsals_venue_id",
                table: "sphere_of_activity_rehearsals",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_url_roles_role_id",
                table: "url_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_urls_project_id",
                table: "urls",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_venues_address_id",
                table: "venues",
                column: "address_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment_participations");

            migrationBuilder.DropTable(
                name: "appointment_rooms");

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
                name: "auditions");

            migrationBuilder.DropTable(
                name: "available_documents");

            migrationBuilder.DropTable(
                name: "musician_profile_credentials");

            migrationBuilder.DropTable(
                name: "musician_profile_educations");

            migrationBuilder.DropTable(
                name: "musician_profile_sections");

            migrationBuilder.DropTable(
                name: "person_sections");

            migrationBuilder.DropTable(
                name: "preferred_genre");

            migrationBuilder.DropTable(
                name: "project_appointments");

            migrationBuilder.DropTable(
                name: "project_participations");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "section_appointments");

            migrationBuilder.DropTable(
                name: "sphere_of_activity_concerts");

            migrationBuilder.DropTable(
                name: "sphere_of_activity_rehearsals");

            migrationBuilder.DropTable(
                name: "url_roles");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "credentials");

            migrationBuilder.DropTable(
                name: "educations");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "musician_profiles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "urls");

            migrationBuilder.DropTable(
                name: "venues");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "sections");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "regions");

            migrationBuilder.DropTable(
                name: "select_value_mappings");

            migrationBuilder.DropTable(
                name: "select_value_categories");

            migrationBuilder.DropTable(
                name: "select_values");
        }
    }
}
