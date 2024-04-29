using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class CollaboratorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaboratorTable",
                columns: table => new
                {
                    C_Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    C_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    NotesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorTable", x => x.C_Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorTable_NotesTable_NotesId",
                        column: x => x.NotesId,
                        principalTable: "NotesTable",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollaboratorTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTable_NotesId",
                table: "CollaboratorTable",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTable_UserId",
                table: "CollaboratorTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorTable");
        }
    }
}
