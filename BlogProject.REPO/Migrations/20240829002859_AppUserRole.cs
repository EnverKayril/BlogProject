using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.REPO.Migrations
{
    public partial class AppUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "484f07d5-302b-438a-a719-be1ee2171312");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "8f6fe5dd-4bbd-4f38-a98a-a19c03022988");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "ad320577-dad8-4a9f-a854-863811e4a2ff");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "c25976ae-9349-4811-bbf3-8ae0cafe7f21");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "e3475b6e-ee64-4715-b1f0-770319d6b82e");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { "45b814b1-3aae-4caf-8359-b23d0d02c820", new DateTime(2024, 8, 29, 3, 28, 59, 640, DateTimeKind.Local).AddTicks(5628), null, "Sistem", 0, null },
                    { "4d187b45-883f-4036-8961-58b18ae36bef", new DateTime(2024, 8, 29, 3, 28, 59, 640, DateTimeKind.Local).AddTicks(5617), null, "Yazılım", 0, null },
                    { "99780402-3bb6-47ad-b5a4-c4e77f154ffa", new DateTime(2024, 8, 29, 3, 28, 59, 640, DateTimeKind.Local).AddTicks(5638), null, "Web Grafik", 0, null },
                    { "a20d39ef-8808-4cb6-91ca-e7040858f466", new DateTime(2024, 8, 29, 3, 28, 59, 640, DateTimeKind.Local).AddTicks(5641), null, "Güvenlik", 0, null },
                    { "f2d8bd49-4231-44ff-9c16-cd005f275c53", new DateTime(2024, 8, 29, 3, 28, 59, 640, DateTimeKind.Local).AddTicks(5643), null, "Bulut Sistemler", 0, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "45b814b1-3aae-4caf-8359-b23d0d02c820");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "4d187b45-883f-4036-8961-58b18ae36bef");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "99780402-3bb6-47ad-b5a4-c4e77f154ffa");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "a20d39ef-8808-4cb6-91ca-e7040858f466");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "f2d8bd49-4231-44ff-9c16-cd005f275c53");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { "484f07d5-302b-438a-a719-be1ee2171312", new DateTime(2024, 8, 29, 0, 22, 7, 643, DateTimeKind.Local).AddTicks(6144), null, "Web Grafik", 0, null },
                    { "8f6fe5dd-4bbd-4f38-a98a-a19c03022988", new DateTime(2024, 8, 29, 0, 22, 7, 643, DateTimeKind.Local).AddTicks(6157), null, "Güvenlik", 0, null },
                    { "ad320577-dad8-4a9f-a854-863811e4a2ff", new DateTime(2024, 8, 29, 0, 22, 7, 643, DateTimeKind.Local).AddTicks(6132), null, "Yazılım", 0, null },
                    { "c25976ae-9349-4811-bbf3-8ae0cafe7f21", new DateTime(2024, 8, 29, 0, 22, 7, 643, DateTimeKind.Local).AddTicks(6142), null, "Sistem", 0, null },
                    { "e3475b6e-ee64-4715-b1f0-770319d6b82e", new DateTime(2024, 8, 29, 0, 22, 7, 643, DateTimeKind.Local).AddTicks(6159), null, "Bulut Sistemler", 0, null }
                });
        }
    }
}
