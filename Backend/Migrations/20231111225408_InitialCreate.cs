using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    DriverId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PoolId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    PassengerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PoolId = table.Column<int>(type: "INTEGER", nullable: false),
                    PickupTime = table.Column<string>(type: "TEXT", nullable: true),
                    Fee = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.PassengerId);
                });

            migrationBuilder.CreateTable(
                name: "Pool",
                columns: table => new
                {
                    PoolId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HostId = table.Column<int>(type: "INTEGER", nullable: false),
                    PoolSize = table.Column<int>(type: "INTEGER", nullable: false),
                    ArrivalTime = table.Column<string>(type: "TEXT", nullable: true),
                    Destination = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pool", x => x.PoolId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    Age = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropTable(
                name: "Pool");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
