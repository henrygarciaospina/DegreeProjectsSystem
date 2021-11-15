using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddTableConfigToDatabaseAndUpdateModelTeachingAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachingAssigments_People_PersonId",
                table: "TeachingAssigments");

            migrationBuilder.DropIndex(
                name: "IX_TeachingAssigments_PersonId",
                table: "TeachingAssigments");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "TeachingAssigments");

            migrationBuilder.AddColumn<int>(
                name: "PersonTypePersonId",
                table: "TeachingAssigments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudenTypeId = table.Column<int>(nullable: false),
                    TeacherTypeId = table.Column<int>(nullable: false),
                    ContactTypeId = table.Column<int>(nullable: false),
                    AdministrativeTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssigments_PersonTypePersonId",
                table: "TeachingAssigments",
                column: "PersonTypePersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingAssigments_PersonTypePeople_PersonTypePersonId",
                table: "TeachingAssigments",
                column: "PersonTypePersonId",
                principalTable: "PersonTypePeople",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachingAssigments_PersonTypePeople_PersonTypePersonId",
                table: "TeachingAssigments");

            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropIndex(
                name: "IX_TeachingAssigments_PersonTypePersonId",
                table: "TeachingAssigments");

            migrationBuilder.DropColumn(
                name: "PersonTypePersonId",
                table: "TeachingAssigments");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "TeachingAssigments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeachingAssigments_PersonId",
                table: "TeachingAssigments",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingAssigments_People_PersonId",
                table: "TeachingAssigments",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
