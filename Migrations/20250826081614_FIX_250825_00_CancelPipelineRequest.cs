using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientBizFlow_attemp_1.Migrations
{
    /// <inheritdoc />
    public partial class FIX_250825_00_CancelPipelineRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CancelPipelineRequests",
                newName: "PipelineName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PipelineName",
                table: "CancelPipelineRequests",
                newName: "Name");
        }
    }
}
