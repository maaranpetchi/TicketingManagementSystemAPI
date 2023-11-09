using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketingManagementSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class madetablechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedBy",
                table: "TicketFormData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "TicketFormData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedBy",
                table: "TicketFormData");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "TicketFormData");
        }
    }
}
