using Microsoft.EntityFrameworkCore.Migrations;

namespace marioProgetto.Migrations
{
    public partial class FakeSeedFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Features1')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Features2')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Features3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features");
        }
    }
}
