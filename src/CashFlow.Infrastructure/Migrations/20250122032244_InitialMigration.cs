using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndUpdateExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Criação da tabela User
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserIdentifier = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            // Adiciona a coluna UserId à tabela Expenses
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Expenses",
                type: "bigint",
                nullable: false,
                defaultValue: 0);

            // Cria o índice na coluna UserId
            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            // Adiciona a chave estrangeira entre Expenses e User
            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_User_UserId",
                table: "Expenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove a foreign key entre Expenses e User
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_User_UserId",
                table: "Expenses");

            // Remove o índice da coluna UserId
            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses");

            // Remove a coluna UserId
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Expenses");

            // Exclui a tabela User
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
