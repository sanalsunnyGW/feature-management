using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeatureToggle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FeatManMig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                schema: "featuremanagement",
                table: "Log",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeatureName",
                schema: "featuremanagement",
                table: "Log",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "featuremanagement",
                table: "Log",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessName",
                schema: "featuremanagement",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "FeatureName",
                schema: "featuremanagement",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "featuremanagement",
                table: "Log");
        }
    }
}
