using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp.Domain.Migrations
{
    public partial class inittable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityWeatherDetail",
                columns: table => new
                {
                    CityWeatherDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityWeatherDetail", x => x.CityWeatherDetailId);
                });

            migrationBuilder.CreateTable(
              name: "CityLocationDetail",
              columns: table => new
              {
                  CityLocationDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                  Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                  Latitude = table.Column<decimal>(type: "decimal(19,9)", nullable: true),
                  Longitude = table.Column<decimal>(type: "decimal(19,9)", nullable: true),
                  UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_CityLocationDetail", x => x.CityLocationDetailId);
              });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityWeatherDetail");
        }
    }
}
