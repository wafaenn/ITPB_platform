using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITBS_Platform.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participations_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participations_CourseId_UserId",
                table: "Participations",
                columns: new[] { "CourseId", "UserId" },
                unique: true,
                filter: "[CourseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_EventId_UserId",
                table: "Participations",
                columns: new[] { "EventId", "UserId" },
                unique: true,
                filter: "[EventId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_UserId",
                table: "Participations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participations");
        }
    }
}
