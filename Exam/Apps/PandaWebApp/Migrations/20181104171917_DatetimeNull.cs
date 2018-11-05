using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PandaWebApp.Migrations
{
    public partial class DatetimeNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EstimateDeliveryDate",
                table: "Packages",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EstimateDeliveryDate",
                table: "Packages",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
