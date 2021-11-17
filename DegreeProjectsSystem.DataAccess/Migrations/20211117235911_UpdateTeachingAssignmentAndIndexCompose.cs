using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class UpdateTeachingAssignmentAndIndexCompose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeachingAssigments_PersonTypePersonId",
                table: "TeachingAssigments");

            migrationBuilder.DropIndex(
                name: "IX_TeachingAssigments_StudentRequestId",
                table: "TeachingAssigments");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssigments_PersonTypePersonId",
                table: "TeachingAssigments",
                column: "PersonTypePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssigments_StudentRequestId_PersonTypePersonId",
                table: "TeachingAssigments",
                columns: new[] { "StudentRequestId", "PersonTypePersonId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeachingAssigments_PersonTypePersonId",
                table: "TeachingAssigments");

            migrationBuilder.DropIndex(
                name: "IX_TeachingAssigments_StudentRequestId_PersonTypePersonId",
                table: "TeachingAssigments");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssigments_PersonTypePersonId",
                table: "TeachingAssigments",
                column: "PersonTypePersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssigments_StudentRequestId",
                table: "TeachingAssigments",
                column: "StudentRequestId");
        }
    }
}
