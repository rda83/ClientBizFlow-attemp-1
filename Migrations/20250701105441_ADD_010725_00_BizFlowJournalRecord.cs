using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientBizFlow_attemp_1.Migrations
{
    /// <inheritdoc />
    public partial class ADD_010725_00_BizFlowJournalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BizFlowJournalRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Period = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PipelineName = table.Column<string>(type: "text", nullable: false),
                    ItemDescription = table.Column<string>(type: "text", nullable: false),
                    ItemSortOrder = table.Column<int>(type: "integer", nullable: false),
                    TypeAction = table.Column<int>(type: "integer", nullable: false),
                    TypeOperationId = table.Column<string>(type: "text", nullable: false),
                    LaunchId = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Trigger = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BizFlowJournalRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BizFlowJournalRecords");
        }
    }
}
