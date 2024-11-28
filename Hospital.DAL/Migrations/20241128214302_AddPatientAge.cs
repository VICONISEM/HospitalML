﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientAge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "patients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "patients");
        }
    }
}
