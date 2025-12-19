using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITBS_Platform.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipationStatut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Statut",
                table: "Participations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Participations");
        }
    }
}
