using Microsoft.EntityFrameworkCore.Migrations;

namespace DegreeProjectsSystem.DataAccess.Migrations
{
    public partial class DeleteAttributeChangeModeTableSolicitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModalityChange",
                table: "Solicitudes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ModalityChange",
                table: "Solicitudes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
