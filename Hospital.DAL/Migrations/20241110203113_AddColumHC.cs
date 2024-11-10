using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddColumHC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HealthaCondition",
                table: "BiologicalIndicators",
                newName: "HealthCondition");

            migrationBuilder.AddColumn<int>(
                name: "HealthConditionScore",
                table: "BiologicalIndicators",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthConditionScore",
                table: "BiologicalIndicators");

            migrationBuilder.RenameColumn(
                name: "HealthCondition",
                table: "BiologicalIndicators",
                newName: "HealthaCondition");
        }
    }
}
