using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class ChangedRelationSolicitudeByStudentRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachingAssigments_Solicitudes_SolicitudeId",
                table: "TeachingAssigments");

            migrationBuilder.DropIndex(
                name: "IX_TeachingAssigments_SolicitudeId",
                table: "TeachingAssigments");

            migrationBuilder.DropColumn(
                name: "SolicitudeId",
                table: "TeachingAssigments");

            migrationBuilder.AddColumn<int>(
                name: "StudentRequestId",
                table: "TeachingAssigments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssigments_StudentRequestId",
                table: "TeachingAssigments",
                column: "StudentRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingAssigments_StudentRequests_StudentRequestId",
                table: "TeachingAssigments",
                column: "StudentRequestId",
                principalTable: "StudentRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachingAssigments_StudentRequests_StudentRequestId",
                table: "TeachingAssigments");

            migrationBuilder.DropIndex(
                name: "IX_TeachingAssigments_StudentRequestId",
                table: "TeachingAssigments");

            migrationBuilder.DropColumn(
                name: "StudentRequestId",
                table: "TeachingAssigments");

            migrationBuilder.AddColumn<int>(
                name: "SolicitudeId",
                table: "TeachingAssigments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
