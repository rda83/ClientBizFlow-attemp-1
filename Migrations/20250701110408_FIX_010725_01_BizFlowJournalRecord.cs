using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientBizFlow_attemp_1.Migrations
{
    /// <inheritdoc />
    public partial class FIX_010725_01_BizFlowJournalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TypeAction",
                table: "BizFlowJournalRecords",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeAction",
                table: "BizFlowJournalRecords",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
