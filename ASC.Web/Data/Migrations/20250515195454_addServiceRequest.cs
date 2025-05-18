using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASC.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class addServiceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestedService",
                table: "ServiceRequests",
                newName: "RequestedServices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestedServices",
                table: "ServiceRequests",
                newName: "RequestedService");
        }
    }
}
