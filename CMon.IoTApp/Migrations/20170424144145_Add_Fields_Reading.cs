using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMon.IoTApp.Migrations
{
    public partial class Add_Fields_Reading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Power",
                table: "Readings",
                nullable: false,
                defaultValue: 0.0d);

            migrationBuilder.AddColumn<int>(
                name: "Voltage",
                table: "Readings",
                nullable: false,
                defaultValue: 220);

            migrationBuilder.Sql(@"UPDATE Readings SET Power = Value * Voltage;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Power",
                table: "Readings");

            migrationBuilder.DropColumn(
                name: "Voltage",
                table: "Readings");
        }
    }
}
