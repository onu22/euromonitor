using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Euromonitor.ApI.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "bookseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "subscriptionseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "userseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "subscriptionStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    SubscribedDate = table.Column<DateTime>(nullable: false),
                    UnSubscribedDate = table.Column<DateTime>(nullable: false),
                    SubscriptionStatusId = table.Column<int>(nullable: false),
                    BookId = table.Column<int>(nullable: false),
                    UserEmail = table.Column<string>(maxLength: 500, nullable: false),
                    PurchasePrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subscriptions_books_BookId",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subscriptions_subscriptionStatus_SubscriptionStatusId",
                        column: x => x.SubscriptionStatusId,
                        principalTable: "subscriptionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_BookId",
                table: "subscriptions",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_SubscriptionStatusId",
                table: "subscriptions",
                column: "SubscriptionStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "subscriptionStatus");

            migrationBuilder.DropSequence(
                name: "bookseq");

            migrationBuilder.DropSequence(
                name: "subscriptionseq");

            migrationBuilder.DropSequence(
                name: "userseq");
        }
    }
}
