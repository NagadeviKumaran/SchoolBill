using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SchoolBill.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillMasters",
                columns: table => new
                {
                    Billid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Studentid = table.Column<int>(type: "integer", nullable: false),
                    AppNo = table.Column<int>(type: "integer", nullable: false),
                    BillDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillMasters", x => x.Billid);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Classid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClassName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Classid);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SchoolName = table.Column<string>(type: "text", nullable: true),
                    SchoolAddress = table.Column<string>(type: "text", nullable: true),
                    SchoolEmail = table.Column<string>(type: "text", nullable: true),
                    Phoneno = table.Column<string>(type: "text", nullable: true),
                    Billid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolId);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    Detailid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Billid = table.Column<int>(type: "integer", nullable: false),
                    Particulars = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Detailid);
                    table.ForeignKey(
                        name: "FK_BillDetails_BillMasters_Billid",
                        column: x => x.Billid,
                        principalTable: "BillMasters",
                        principalColumn: "Billid");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Studentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Studentname = table.Column<string>(type: "text", nullable: true),
                    Classid = table.Column<int>(type: "integer", nullable: false),
                    Sectionid = table.Column<int>(type: "integer", nullable: false),
                    ParentName = table.Column<string>(type: "text", nullable: true),
                    StuContactno = table.Column<string>(type: "text", nullable: true),
                    Billid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Studentid);
                    table.ForeignKey(
                        name: "FK_Students_BillMasters_Billid",
                        column: x => x.Billid,
                        principalTable: "BillMasters",
                        principalColumn: "Billid");
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Sectionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sectionname = table.Column<string>(type: "text", nullable: true),
                    Classid = table.Column<int>(type: "integer", nullable: false),
                    ClassEntityClassid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Sectionid);
                    table.ForeignKey(
                        name: "FK_Sections_Classes_ClassEntityClassid",
                        column: x => x.ClassEntityClassid,
                        principalTable: "Classes",
                        principalColumn: "Classid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_Billid",
                table: "BillDetails",
                column: "Billid");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ClassEntityClassid",
                table: "Sections",
                column: "ClassEntityClassid");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Billid",
                table: "Students",
                column: "Billid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "BillMasters");
        }
    }
}
