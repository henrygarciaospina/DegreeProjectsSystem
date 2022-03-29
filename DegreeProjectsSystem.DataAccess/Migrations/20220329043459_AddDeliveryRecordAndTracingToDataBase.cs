using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddDeliveryRecordAndTracingToDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tracings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModalitySubmodalityId = table.Column<int>(nullable: false),
                    DeliveryDescription = table.Column<string>(maxLength: 500, nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracings_ModalitySubmodalities_ModalitySubmodalityId",
                        column: x => x.ModalitySubmodalityId,
                        principalTable: "ModalitySubmodalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentRequestId = table.Column<int>(nullable: false),
                    TracingId = table.Column<int>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    FileUrl = table.Column<string>(nullable: true),
                    Observations = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryRecords_StudentRequests_StudentRequestId",
                        column: x => x.StudentRequestId,
                        principalTable: "StudentRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryRecords_Tracings_TracingId",
                        column: x => x.TracingId,
                        principalTable: "Tracings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRecords_TracingId",
                table: "DeliveryRecords",
                column: "TracingId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRecords_StudentRequestId_TracingId",
                table: "DeliveryRecords",
                columns: new[] { "StudentRequestId", "TracingId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracings_ModalitySubmodalityId_DeliveryDescription",
                table: "Tracings",
                columns: new[] { "ModalitySubmodalityId", "DeliveryDescription" },
                unique: true,
                filter: "[DeliveryDescription] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryRecords");

            migrationBuilder.DropTable(
                name: "Tracings");
        }
    }
}
