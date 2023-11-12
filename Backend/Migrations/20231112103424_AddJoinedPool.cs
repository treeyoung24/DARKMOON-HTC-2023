using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddJoinedPool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JoinedPoll",
                columns: table => new
                {
                    PoolId = table.Column<int>(type: "INTEGER", nullable: false),
                    MemId = table.Column<int>(type: "INTEGER", nullable: false),
                    PickupTime = table.Column<string>(type: "TEXT", nullable: true),
                    RouteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinedPoll", x => new { x.PoolId, x.MemId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinedPoll");
        }
    }
}
