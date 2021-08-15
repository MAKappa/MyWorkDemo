using Microsoft.EntityFrameworkCore.Migrations;


namespace MyWorkDemo.Migrations
{
    public partial class CampoOre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Ore",
                table: "Activity",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ore",
                table: "Activity");
        }
    }
}
