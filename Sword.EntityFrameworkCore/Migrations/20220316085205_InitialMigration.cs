using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sword.EntityFrameworkCore.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resc",
                columns: table => new
                {
                    RescId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Identity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deletor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resc", x => x.RescId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Display = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deletor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "RoleResc",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RescId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleResc", x => new { x.RoleId, x.RescId });
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenExpireTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TokenRefreshExpireTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deletor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.InsertData(
                table: "Resc",
                columns: new[] { "RescId", "Available", "CreateTime", "Creator", "DeleteTime", "Deletor", "Display", "Icon", "Identity", "ParentId", "Type", "UpdateTime", "Updator" },
                values: new object[,]
                {
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d00"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "System", "form", "System", null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d01"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Resc", "form", "Resc", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d00"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d02"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Create", "form", "Resc.Create", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d01"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d03"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Update", "form", "Resc.Update", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d01"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d04"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Delete", "form", "Resc.Delete", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d01"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d05"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Role", "form", "Role", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d00"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d06"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Create", "form", "Role.Create", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d05"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d07"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Update", "form", "Role.Update", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d05"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d08"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Delete", "form", "Role.Delete", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d05"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d09"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "User", "form", "User", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d00"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d10"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Create", "form", "User.Create", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d09"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d11"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Update", "form", "User.Update", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d09"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d12"), true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Delete", "form", "User.Delete", new Guid("890d6a3c-6a7f-4277-8e24-e14168f61d09"), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Account", "Available", "CreateTime", "Creator", "DeleteTime", "Deletor", "Display", "Password", "Token", "TokenExpireTime", "TokenRefreshExpireTime", "Type", "UpdateTime", "Updator" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "tester", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "tester", "tester", null, null, null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_Resc_Identity",
                table: "Resc",
                column: "Identity",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Display",
                table: "Role",
                column: "Display",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Account",
                table: "User",
                column: "Account",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resc");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RoleResc");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserRole");
        }
    }
}
