using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Passenger");

            migrationBuilder.CreateTable(
                name: "RequestJoin",
                columns: table => new
                {
                    PoolId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemId = table.Column<int>(type: "INTEGER", nullable: false),
                    PickupTime = table.Column<string>(type: "TEXT", nullable: true),
                    RouteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestJoin", x => x.PoolId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestJoin");

            migrationBuilder.AddColumn<float>(
                name: "Fee",
                table: "Passenger",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
