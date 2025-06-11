using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PBL3_DUTLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    author = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    available = table.Column<long>(type: "bigint", nullable: true, defaultValueSql: "('1')"),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    amount = table.Column<long>(type: "bigint", nullable: true),
                    image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "loans",
                columns: table => new
                {
                    loan_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    price = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_loans", x => x.loan_id);
                });

            migrationBuilder.CreateTable(
                name: "WebUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RealName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    sdt = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WebUser__1788CC4CC2A485BA", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "book_copy",
                columns: table => new
                {
                    BookCopyId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk3", x => new { x.BookCopyId, x.BookId });
                    table.ForeignKey(
                        name: "FK__book_copy__BookI__5CD6CB2B",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    genre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk4", x => new { x.BookId, x.genre });
                    table.ForeignKey(
                        name: "FK__genre__BookId__5FB337D6",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessHistory",
                columns: table => new
                {
                    AccessId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk", x => new { x.AccessId, x.UserId });
                    table.ForeignKey(
                        name: "FK__AccessHis__UserI__5629CD9C",
                        column: x => x.UserId,
                        principalTable: "WebUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Borrow",
                columns: table => new
                {
                    BorrowId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime", nullable: true),
                    dealine = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk2", x => new { x.BorrowId, x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK__Borrow__BookId__59FA5E80",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Borrow__UserId__59063A47",
                        column: x => x.UserId,
                        principalTable: "WebUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk5", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK__WishList__BookId__71D1E811",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__WishList__UserId__70DDC3D8",
                        column: x => x.UserId,
                        principalTable: "WebUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessHistory_UserId",
                table: "AccessHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_book_copy_BookId",
                table: "book_copy",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrow_BookId",
                table: "Borrow",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrow_UserId",
                table: "Borrow",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_BookId",
                table: "WishList",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessHistory");

            migrationBuilder.DropTable(
                name: "book_copy");

            migrationBuilder.DropTable(
                name: "Borrow");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "loans");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "WebUser");
        }
    }
}
