using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "iac");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "iac",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    AccountStatus = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    profilePhoto = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isDelete = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "iac",
                columns: table => new
                {
                    CustomrID = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomrID);
                    table.ForeignKey(
                        name: "FK_Customers_Users_CustomrID",
                        column: x => x.CustomrID,
                        principalSchema: "iac",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                schema: "iac",
                columns: table => new
                {
                    sellerID = table.Column<Guid>(type: "uuid", nullable: false),
                    shopName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    shopPhoto = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    isVerifiedByAdmin = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    verfiedSellerDocument = table.Column<string>(type: "text", nullable: true),
                    verfiedShopDocument = table.Column<string>(type: "text", nullable: true),
                    isVerfiedSellerDocumentBeenViewed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    isVerfiedShopDocumentBeenViewed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.sellerID);
                    table.ForeignKey(
                        name: "FK_Sellers_Users_sellerID",
                        column: x => x.sellerID,
                        principalSchema: "iac",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserOTPs",
                schema: "iac",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    userID = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    GeneratedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    FailedAttempts = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOTPs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserOTPs_Users_userID",
                        column: x => x.userID,
                        principalSchema: "iac",
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOTPs_userID",
                schema: "iac",
                table: "UserOTPs",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_UserOTPs_userID_Code",
                schema: "iac",
                table: "UserOTPs",
                columns: new[] { "userID", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "iac",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_phoneNumber",
                schema: "iac",
                table: "Users",
                column: "phoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                schema: "iac",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers",
                schema: "iac");

            migrationBuilder.DropTable(
                name: "Sellers",
                schema: "iac");

            migrationBuilder.DropTable(
                name: "UserOTPs",
                schema: "iac");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "iac");
        }
    }
}
