using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddForeignKeySolicitudeIdToTableTeachingAssigment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssigments_SolicitudeId",
                table: "TeachingAssigments",
                column: "SolicitudeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingAssigments_Solicitudes_SolicitudeId",
                table: "TeachingAssigments",
                column: "SolicitudeId",
                principalTable: "Solicitudes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachingAssigments_Solicitudes_SolicitudeId",
                table: "TeachingAssigments");

            migrationBuilder.DropIndex(
                name: "IX_TeachingAssigments_SolicitudeId",
                table: "TeachingAssigments");
        }
    }
}
