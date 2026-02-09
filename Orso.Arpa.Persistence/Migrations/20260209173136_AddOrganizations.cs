using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orso.Arpa.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    short_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    website = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    legal_form_id = table.Column<Guid>(type: "uuid", nullable: true),
                    organization_type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tags = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizations", x => x.id);
                    table.ForeignKey(
                        name: "fk_organizations_select_value_mappings_legal_form_id",
                        column: x => x.legal_form_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_organizations_select_value_mappings_organization_type_id",
                        column: x => x.organization_type_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "organization_relationships",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    source_organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    target_organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    relationship_type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization_relationships", x => x.id);
                    table.CheckConstraint("CK_OrganizationRelationship_NoSelfReference", "\"source_organization_id\" <> \"target_organization_id\"");
                    table.ForeignKey(
                        name: "fk_organization_relationships_organizations_source_organizatio",
                        column: x => x.source_organization_id,
                        principalTable: "organizations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_organization_relationships_organizations_target_organizatio",
                        column: x => x.target_organization_id,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organization_relationships_select_value_mappings_relationsh",
                        column: x => x.relationship_type_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "person_organizations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    function = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    relationship_type_id = table.Column<Guid>(type: "uuid", nullable: true),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(110)", maxLength: 110, nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_organizations", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_organizations_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_person_organizations_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_person_organizations_select_value_mappings_relationship_typ",
                        column: x => x.relationship_type_id,
                        principalTable: "select_value_mappings",
                        principalColumn: "id");
                });

            // Organization SelectValue Categories
            migrationBuilder.InsertData(
                table: "select_value_categories",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "name", "property", "table" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Legal Form", "LegalForm", "Organization" },
                    { new Guid("a1b2c3d4-0011-4000-8000-000000000011"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Organization Type", "OrganizationType", "Organization" },
                    { new Guid("a1b2c3d4-0012-4000-8000-000000000012"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Person-Org Relationship", "RelationshipType", "PersonOrganization" },
                    { new Guid("a1b2c3d4-0013-4000-8000-000000000013"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, "Org-Org Relationship", "RelationshipType", "OrganizationRelationship" }
                });

            // Organization SelectValues
            migrationBuilder.InsertData(
                table: "select_values",
                columns: new[] { "id", "created_at", "created_by", "deleted", "description", "modified_at", "modified_by", "name" },
                values: new object[,]
                {
                    // Legal Forms
                    { new Guid("c0000001-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Gesellschaft mit beschränkter Haftung", null, null, "GmbH" },
                    { new Guid("c0000001-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Aktiengesellschaft", null, null, "AG" },
                    { new Guid("c0000001-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "eingetragener Verein", null, null, "e.V." },
                    { new Guid("c0000001-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "gemeinnützige GmbH", null, null, "gGmbH" },
                    { new Guid("c0000001-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Gesellschaft bürgerlichen Rechts", null, null, "GbR" },
                    { new Guid("c0000001-0006-4000-8000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Einzelunternehmen" },
                    { new Guid("c0000001-0007-4000-8000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Kommanditgesellschaft", null, null, "KG" },
                    { new Guid("c0000001-0008-4000-8000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Offene Handelsgesellschaft", null, null, "OHG" },
                    { new Guid("c0000001-0009-4000-8000-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Stiftung" },
                    { new Guid("c0000001-000a-4000-8000-00000000000a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "Körperschaft des öffentlichen Rechts", null, null, "KdöR" },
                    { new Guid("c0000001-000b-4000-8000-00000000000b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Other" },
                    // Organization Types
                    { new Guid("c0000002-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Company" },
                    { new Guid("c0000002-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Association" },
                    { new Guid("c0000002-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Government Agency" },
                    { new Guid("c0000002-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Foundation" },
                    { new Guid("c0000002-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Educational Institution" },
                    { new Guid("c0000002-0006-4000-8000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Cultural Institution" },
                    { new Guid("c0000002-0007-4000-8000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Media" },
                    { new Guid("c0000002-0008-4000-8000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Agency" },
                    { new Guid("c0000002-0009-4000-8000-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Other" },
                    // Person-Org Relationship Types
                    { new Guid("c0000003-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Employee" },
                    { new Guid("c0000003-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Managing Director" },
                    { new Guid("c0000003-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Board Member" },
                    { new Guid("c0000003-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Member" },
                    { new Guid("c0000003-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Customer" },
                    { new Guid("c0000003-0006-4000-8000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Freelancer" },
                    { new Guid("c0000003-0007-4000-8000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Other" },
                    // Org-Org Relationship Types
                    { new Guid("c0000004-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Subsidiary" },
                    { new Guid("c0000004-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Parent Company" },
                    { new Guid("c0000004-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Partner" },
                    { new Guid("c0000004-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Member" },
                    { new Guid("c0000004-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Sponsor" },
                    { new Guid("c0000004-0006-4000-8000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Client" },
                    { new Guid("c0000004-0007-4000-8000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Service Provider" },
                    { new Guid("c0000004-0008-4000-8000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "", null, null, "Other" }
                });

            // Organization SelectValue Mappings
            migrationBuilder.InsertData(
                table: "select_value_mappings",
                columns: new[] { "id", "created_at", "created_by", "deleted", "modified_at", "modified_by", "select_value_category_id", "select_value_id", "sort_order" },
                values: new object[,]
                {
                    // Legal Form Mappings
                    { new Guid("d0000001-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-0001-4000-8000-000000000001"), 10 },
                    { new Guid("d0000001-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-0002-4000-8000-000000000002"), 20 },
                    { new Guid("d0000001-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-0003-4000-8000-000000000003"), 30 },
                    { new Guid("d0000001-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-0004-4000-8000-000000000004"), 40 },
                    { new Guid("d0000001-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-0005-4000-8000-000000000005"), 50 },
                    { new Guid("d0000001-0006-4000-8000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-0006-4000-8000-000000000006"), 60 },
                    { new Guid("d0000001-0007-4000-8000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-0007-4000-8000-000000000007"), 70 },
                    { new Guid("d0000001-0008-4000-8000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-0008-4000-8000-000000000008"), 80 },
                    { new Guid("d0000001-0009-4000-8000-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-0009-4000-8000-000000000009"), 90 },
                    { new Guid("d0000001-000a-4000-8000-00000000000a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-000a-4000-8000-00000000000a"), 100 },
                    { new Guid("d0000001-000b-4000-8000-00000000000b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0010-4000-8000-000000000010"), new Guid("c0000001-000b-4000-8000-00000000000b"), 110 },
                    // Organization Type Mappings
                    { new Guid("d0000002-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0011-4000-8000-000000000011"), new Guid("c0000002-0001-4000-8000-000000000001"), 10 },
                    { new Guid("d0000002-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0011-4000-8000-000000000011"), new Guid("c0000002-0002-4000-8000-000000000002"), 20 },
                    { new Guid("d0000002-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0011-4000-8000-000000000011"), new Guid("c0000002-0003-4000-8000-000000000003"), 30 },
                    { new Guid("d0000002-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0011-4000-8000-000000000011"), new Guid("c0000002-0004-4000-8000-000000000004"), 40 },
                    { new Guid("d0000002-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0011-4000-8000-000000000011"), new Guid("c0000002-0005-4000-8000-000000000005"), 50 },
                    { new Guid("d0000002-0006-4000-8000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0011-4000-8000-000000000011"), new Guid("c0000002-0006-4000-8000-000000000006"), 60 },
                    { new Guid("d0000002-0007-4000-8000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0011-4000-8000-000000000011"), new Guid("c0000002-0007-4000-8000-000000000007"), 70 },
                    { new Guid("d0000002-0008-4000-8000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0011-4000-8000-000000000011"), new Guid("c0000002-0008-4000-8000-000000000008"), 80 },
                    { new Guid("d0000002-0009-4000-8000-000000000009"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0011-4000-8000-000000000011"), new Guid("c0000002-0009-4000-8000-000000000009"), 90 },
                    // Person-Org Relationship Type Mappings
                    { new Guid("d0000003-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0012-4000-8000-000000000012"), new Guid("c0000003-0001-4000-8000-000000000001"), 10 },
                    { new Guid("d0000003-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0012-4000-8000-000000000012"), new Guid("c0000003-0002-4000-8000-000000000002"), 20 },
                    { new Guid("d0000003-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0012-4000-8000-000000000012"), new Guid("c0000003-0003-4000-8000-000000000003"), 30 },
                    { new Guid("d0000003-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0012-4000-8000-000000000012"), new Guid("c0000003-0004-4000-8000-000000000004"), 40 },
                    { new Guid("d0000003-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0012-4000-8000-000000000012"), new Guid("c0000003-0005-4000-8000-000000000005"), 50 },
                    { new Guid("d0000003-0006-4000-8000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0012-4000-8000-000000000012"), new Guid("c0000003-0006-4000-8000-000000000006"), 60 },
                    { new Guid("d0000003-0007-4000-8000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0012-4000-8000-000000000012"), new Guid("c0000003-0007-4000-8000-000000000007"), 70 },
                    // Org-Org Relationship Type Mappings
                    { new Guid("d0000004-0001-4000-8000-000000000001"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0013-4000-8000-000000000013"), new Guid("c0000004-0001-4000-8000-000000000001"), 10 },
                    { new Guid("d0000004-0002-4000-8000-000000000002"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0013-4000-8000-000000000013"), new Guid("c0000004-0002-4000-8000-000000000002"), 20 },
                    { new Guid("d0000004-0003-4000-8000-000000000003"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0013-4000-8000-000000000013"), new Guid("c0000004-0003-4000-8000-000000000003"), 30 },
                    { new Guid("d0000004-0004-4000-8000-000000000004"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0013-4000-8000-000000000013"), new Guid("c0000004-0004-4000-8000-000000000004"), 40 },
                    { new Guid("d0000004-0005-4000-8000-000000000005"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0013-4000-8000-000000000013"), new Guid("c0000004-0005-4000-8000-000000000005"), 50 },
                    { new Guid("d0000004-0006-4000-8000-000000000006"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0013-4000-8000-000000000013"), new Guid("c0000004-0006-4000-8000-000000000006"), 60 },
                    { new Guid("d0000004-0007-4000-8000-000000000007"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0013-4000-8000-000000000013"), new Guid("c0000004-0007-4000-8000-000000000007"), 70 },
                    { new Guid("d0000004-0008-4000-8000-000000000008"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, new Guid("a1b2c3d4-0013-4000-8000-000000000013"), new Guid("c0000004-0008-4000-8000-000000000008"), 80 }
                });

            // Indexes
            migrationBuilder.CreateIndex(
                name: "ix_organization_relationships_relationship_type_id",
                table: "organization_relationships",
                column: "relationship_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_organization_relationships_source_organization_id_target_or",
                table: "organization_relationships",
                columns: new[] { "source_organization_id", "target_organization_id", "relationship_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_organization_relationships_target_organization_id",
                table: "organization_relationships",
                column: "target_organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_organizations_legal_form_id",
                table: "organizations",
                column: "legal_form_id");

            migrationBuilder.CreateIndex(
                name: "ix_organizations_name",
                table: "organizations",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_organizations_organization_type_id",
                table: "organizations",
                column: "organization_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_organizations_organization_id",
                table: "person_organizations",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_organizations_person_id_organization_id_relationship",
                table: "person_organizations",
                columns: new[] { "person_id", "organization_id", "relationship_type_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_person_organizations_relationship_type_id",
                table: "person_organizations",
                column: "relationship_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_relationships");

            migrationBuilder.DropTable(
                name: "person_organizations");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DeleteData(
                table: "select_value_mappings",
                keyColumn: "id",
                keyValues: new object[]
                {
                    new Guid("d0000001-0001-4000-8000-000000000001"), new Guid("d0000001-0002-4000-8000-000000000002"),
                    new Guid("d0000001-0003-4000-8000-000000000003"), new Guid("d0000001-0004-4000-8000-000000000004"),
                    new Guid("d0000001-0005-4000-8000-000000000005"), new Guid("d0000001-0006-4000-8000-000000000006"),
                    new Guid("d0000001-0007-4000-8000-000000000007"), new Guid("d0000001-0008-4000-8000-000000000008"),
                    new Guid("d0000001-0009-4000-8000-000000000009"), new Guid("d0000001-000a-4000-8000-00000000000a"),
                    new Guid("d0000001-000b-4000-8000-00000000000b"),
                    new Guid("d0000002-0001-4000-8000-000000000001"), new Guid("d0000002-0002-4000-8000-000000000002"),
                    new Guid("d0000002-0003-4000-8000-000000000003"), new Guid("d0000002-0004-4000-8000-000000000004"),
                    new Guid("d0000002-0005-4000-8000-000000000005"), new Guid("d0000002-0006-4000-8000-000000000006"),
                    new Guid("d0000002-0007-4000-8000-000000000007"), new Guid("d0000002-0008-4000-8000-000000000008"),
                    new Guid("d0000002-0009-4000-8000-000000000009"),
                    new Guid("d0000003-0001-4000-8000-000000000001"), new Guid("d0000003-0002-4000-8000-000000000002"),
                    new Guid("d0000003-0003-4000-8000-000000000003"), new Guid("d0000003-0004-4000-8000-000000000004"),
                    new Guid("d0000003-0005-4000-8000-000000000005"), new Guid("d0000003-0006-4000-8000-000000000006"),
                    new Guid("d0000003-0007-4000-8000-000000000007"),
                    new Guid("d0000004-0001-4000-8000-000000000001"), new Guid("d0000004-0002-4000-8000-000000000002"),
                    new Guid("d0000004-0003-4000-8000-000000000003"), new Guid("d0000004-0004-4000-8000-000000000004"),
                    new Guid("d0000004-0005-4000-8000-000000000005"), new Guid("d0000004-0006-4000-8000-000000000006"),
                    new Guid("d0000004-0007-4000-8000-000000000007"), new Guid("d0000004-0008-4000-8000-000000000008")
                });

            migrationBuilder.DeleteData(
                table: "select_values",
                keyColumn: "id",
                keyValues: new object[]
                {
                    new Guid("c0000001-0001-4000-8000-000000000001"), new Guid("c0000001-0002-4000-8000-000000000002"),
                    new Guid("c0000001-0003-4000-8000-000000000003"), new Guid("c0000001-0004-4000-8000-000000000004"),
                    new Guid("c0000001-0005-4000-8000-000000000005"), new Guid("c0000001-0006-4000-8000-000000000006"),
                    new Guid("c0000001-0007-4000-8000-000000000007"), new Guid("c0000001-0008-4000-8000-000000000008"),
                    new Guid("c0000001-0009-4000-8000-000000000009"), new Guid("c0000001-000a-4000-8000-00000000000a"),
                    new Guid("c0000001-000b-4000-8000-00000000000b"),
                    new Guid("c0000002-0001-4000-8000-000000000001"), new Guid("c0000002-0002-4000-8000-000000000002"),
                    new Guid("c0000002-0003-4000-8000-000000000003"), new Guid("c0000002-0004-4000-8000-000000000004"),
                    new Guid("c0000002-0005-4000-8000-000000000005"), new Guid("c0000002-0006-4000-8000-000000000006"),
                    new Guid("c0000002-0007-4000-8000-000000000007"), new Guid("c0000002-0008-4000-8000-000000000008"),
                    new Guid("c0000002-0009-4000-8000-000000000009"),
                    new Guid("c0000003-0001-4000-8000-000000000001"), new Guid("c0000003-0002-4000-8000-000000000002"),
                    new Guid("c0000003-0003-4000-8000-000000000003"), new Guid("c0000003-0004-4000-8000-000000000004"),
                    new Guid("c0000003-0005-4000-8000-000000000005"), new Guid("c0000003-0006-4000-8000-000000000006"),
                    new Guid("c0000003-0007-4000-8000-000000000007"),
                    new Guid("c0000004-0001-4000-8000-000000000001"), new Guid("c0000004-0002-4000-8000-000000000002"),
                    new Guid("c0000004-0003-4000-8000-000000000003"), new Guid("c0000004-0004-4000-8000-000000000004"),
                    new Guid("c0000004-0005-4000-8000-000000000005"), new Guid("c0000004-0006-4000-8000-000000000006"),
                    new Guid("c0000004-0007-4000-8000-000000000007"), new Guid("c0000004-0008-4000-8000-000000000008")
                });

            migrationBuilder.DeleteData(
                table: "select_value_categories",
                keyColumn: "id",
                keyValues: new object[]
                {
                    new Guid("a1b2c3d4-0010-4000-8000-000000000010"),
                    new Guid("a1b2c3d4-0011-4000-8000-000000000011"),
                    new Guid("a1b2c3d4-0012-4000-8000-000000000012"),
                    new Guid("a1b2c3d4-0013-4000-8000-000000000013")
                });
        }
    }
}
