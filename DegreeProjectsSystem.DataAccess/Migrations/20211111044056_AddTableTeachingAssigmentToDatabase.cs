using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddTableTeachingAssigmentToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_ActNumber",
                table: "Solicitudes");

            migrationBuilder.CreateTable(
                name: "TeachingAssigments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudeId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    TeachingFunctionId = table.Column<int>(nullable: false),
                    AssigmentDate = table.Column<DateTime>(maxLength: 200, nullable: false),
                    Observations = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingAssigments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachingAssigments_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingAssigments_TeachingFunctions_TeachingFunctionId",
                        column: x => x.TeachingFunctionId,
                        principalTable: "TeachingFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssigments_PersonId",
                table: "TeachingAssigments",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssigments_TeachingFunctionId",
                table: "TeachingAssigments",
                column: "TeachingFunctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeachingAssigments");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ActNumber",
                table: "Solicitudes",
                column: "ActNumber",
                unique: true);
        }
    }
}
