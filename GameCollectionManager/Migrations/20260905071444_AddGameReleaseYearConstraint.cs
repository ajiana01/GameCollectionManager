using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameCollectionManager.Migrations
{
    /// <inheritdoc />
    public partial class AddGameReleaseYearConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Games_ReleaseYear",
                table: "Games",
                sql: "\"ReleaseYear\" BETWEEN 1950 AND 2100");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Games_ReleaseYear",
                table: "Games");
        }
    }
}
