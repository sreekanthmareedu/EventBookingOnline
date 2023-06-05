using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventBooking.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddEventsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BEvents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eventName = table.Column<string>(type: "varchar(50)", nullable: false),
                    eventDescription = table.Column<string>(type: "varchar(500)", nullable: false),
                    location = table.Column<string>(type: "varchar(50)", nullable: false),
                    eventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    availableSeats = table.Column<int>(type: "int", nullable: false),
                    eventStatus = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BEvents", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BEvents");
        }
    }
}
