using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "Persons",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "Persons",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Persons",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "username",
                table: "Persons");
        }
    }
}
