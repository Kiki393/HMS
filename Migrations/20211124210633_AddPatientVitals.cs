// <copyright file="20211124210633_AddPatientVitals.cs" company="VVU">
// Copyright (c) VVU. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.Migrations
{
    public partial class AddPatientVitals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bp",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Temperature",
                table: "Patients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "weight",
                table: "Patients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bp",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "Patients");
        }
    }
}