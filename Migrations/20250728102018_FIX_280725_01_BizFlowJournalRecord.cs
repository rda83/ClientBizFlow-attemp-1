using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientBizFlow_attemp_1.Migrations
{
    /// <inheritdoc />
    public partial class FIX_280725_01_BizFlowJournalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BizFlowJournalRecords_LaunchId",
                table: "BizFlowJournalRecords",
                column: "LaunchId");

            migrationBuilder.CreateIndex(
                name: "IX_BizFlowJournalRecords_PipelineName",
                table: "BizFlowJournalRecords",
                column: "PipelineName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BizFlowJournalRecords_LaunchId",
                table: "BizFlowJournalRecords");

            migrationBuilder.DropIndex(
                name: "IX_BizFlowJournalRecords_PipelineName",
                table: "BizFlowJournalRecords");
        }
    }
}
