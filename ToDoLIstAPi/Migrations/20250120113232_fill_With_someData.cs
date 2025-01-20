using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoLIstAPi.Migrations
{
    /// <inheritdoc />
    public partial class fill_With_someData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "position" },
                values: new object[,]
                {
                    { 1, "Yassine", "Software Developer" },
                    { 2, "Fatima", "Project Manager" },
                    { 3, "Ahmed", "Data Scientist" },
                    { 4, "Sara", "UI/UX Designer" },
                    { 5, "Ali", "DevOps Engineer" },
                    { 6, "Noor", "QA Engineer" },
                    { 7, "Hassan", "Mobile Developer" },
                    { 8, "Mounia", "Business Analyst" },
                    { 9, "Ibrahim", "Backend Developer" },
                    { 10, "Amina", "Frontend Developer" },
                    { 11, "Khalid", "Database Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "DueDate", "IsCompleted", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Write the project documentation for the new API.", new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Complete Documentation", 1 },
                    { 2, "Develop the user authentication feature for the app.", new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Implement New Feature", 2 },
                    { 3, "Resolve the issue with the login API endpoint.", new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Fix Bug in API", 3 },
                    { 4, "Modify the database schema for the new feature.", new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Update Database Schema", 4 },
                    { 5, "Refactor the codebase to improve performance and readability.", new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Refactor Code", 5 },
                    { 6, "Review the code submitted by the team for the latest feature.", new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Conduct Code Review", 6 },
                    { 7, "Write unit tests for the new user profile feature.", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Write Unit Tests", 7 },
                    { 8, "Deploy the application to the staging environment.", new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Deploy Application", 8 },
                    { 9, "Backup all the relevant databases before the deployment.", new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Create Database Backups", 9 },
                    { 10, "Organize and plan the tasks for the upcoming sprint.", new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Plan Sprint", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
