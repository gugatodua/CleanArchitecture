using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PersonalId",
                table: "Persons",
                type: "nchar(11)",
                fixedLength: true,
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Persons",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Persons",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "BirthDate", "CityId", "FirstName", "Gender", "ImageURL", "LastName", "PersonalId" },
                values: new object[,]
                {
                    { 1123, new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1221, "Jane", 0, null, "Doe", "12341238901" },
                    { 1234, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1221, "John", 1, null, "Doe", "12345678901" }
                });

            migrationBuilder.InsertData(
                table: "PhoneNumbers",
                columns: new[] { "Id", "Number", "PersonId", "Type" },
                values: new object[] { 1, "1234527890", 1123, 0 });

            migrationBuilder.InsertData(
                table: "RelatedPersons",
                columns: new[] { "Id", "PersonId", "RelationType" },
                values: new object[,]
                {
                    { 111, 1123, 0 },
                    { 122, 1123, 2 },
                    { 141, 1234, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RelatedPersons",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "RelatedPersons",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "RelatedPersons",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1123);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1234);

            migrationBuilder.AlterColumn<string>(
                name: "PersonalId",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(11)",
                oldFixedLength: true,
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
