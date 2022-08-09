using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class IntializeDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "CityDetail",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityDetail", x => x.id);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "CityDetail",
                columns: new[] { "id", "city", "color", "end_date", "price", "start_date", "status" },
                values: new object[] { 1, "Neftegorsk", "#fd4e19", new DateTime(2022, 8, 17, 0, 0, 0, 0, DateTimeKind.Local), 55.82, new DateTime(2022, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), "Seldom" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityDetail",
                schema: "dbo");
        }
    }
}
