using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cnpj_data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CNPJ = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    BusinessName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    FantasyName = table.Column<string>(type: "TEXT", maxLength: 55, nullable: true),
                    SocialCapital = table.Column<double>(type: "REAL", nullable: false),
                    RegistrationSituation = table.Column<int>(type: "INTEGER", nullable: false),
                    RegistrationSituationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Cep = table.Column<string>(type: "TEXT", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CNPJ);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    CNPJCPF = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false),
                    Identifier = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.CNPJCPF);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
