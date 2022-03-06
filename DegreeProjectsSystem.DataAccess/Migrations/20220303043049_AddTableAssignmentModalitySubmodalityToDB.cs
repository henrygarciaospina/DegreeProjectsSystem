using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddTableAssignmentModalitySubmodalityToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentModalitySubmodality",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentRequestId = table.Column<int>(nullable: false),
                    ModalitySubmodalityId = table.Column<int>(nullable: false),
                    Observations = table.Column<string>(maxLength: 200, nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentModalitySubmodality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentModalitySubmodality_ModalitySubmodalities_ModalitySubmodalityId",
                        column: x => x.ModalitySubmodalityId,
                        principalTable: "ModalitySubmodalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentModalitySubmodality_StudentRequests_StudentRequestId",
                        column: x => x.StudentRequestId,
                        principalTable: "StudentRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentModalitySubmodality_StudentRequestId",
                table: "AssignmentModalitySubmodality",
                column: "StudentRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentModalitySubmodality_ModalitySubmodalityId_StudentRequestId",
                table: "AssignmentModalitySubmodality",
                columns: new[] { "ModalitySubmodalityId", "StudentRequestId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentModalitySubmodality");
        }
    }
}
