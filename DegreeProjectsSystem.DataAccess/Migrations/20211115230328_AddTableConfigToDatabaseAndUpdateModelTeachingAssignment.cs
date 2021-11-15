using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class AddTableConfigToDatabaseAndUpdateModelTeachingAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configs");
        }
    }
}
