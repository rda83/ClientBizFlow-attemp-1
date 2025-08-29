using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientBizFlow_attemp_1.Migrations
{
    /// <inheritdoc />
    public partial class FIX_250825_01_CancelPipelineRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CancelPipelineRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "CancelPipelineRequests");
        }
    }
}
