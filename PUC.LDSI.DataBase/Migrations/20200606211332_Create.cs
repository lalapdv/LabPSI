using Microsoft.EntityFrameworkCore.Migrations;

namespace PUC.LDSI.DataBase.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotaObtida",
                table: "Prova");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NotaObtida",
                table: "Prova",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
