using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace ToDoLIstAPi.DbContext.InitialConfiguration;

public class TasksConfiguration : IEntityTypeConfiguration<Tasks>
{
    public void Configure(EntityTypeBuilder<Tasks> builder)
    {
        builder.HasData(
            new Tasks
            {
                Id = 1,
                Title = "Complete Documentation",
                Description = "Write the project documentation for the new API.",
                DueDate = new DateTime(2025, 1, 25),
                IsCompleted = false,
                UserId = 1 // Reference to User with Id = 1
            },
            new Tasks
            {
                Id = 2,
                Title = "Implement New Feature",
                Description = "Develop the user authentication feature for the app.",
                DueDate = new DateTime(2025, 1, 30),
                IsCompleted = false,
                UserId = 2 // Reference to User with Id = 2
            },
            new Tasks
            {
                Id = 3,
                Title = "Fix Bug in API",
                Description = "Resolve the issue with the login API endpoint.",
                DueDate = new DateTime(2025, 1, 22),
                IsCompleted = true,
                UserId = 3 // Reference to User with Id = 3
            },
            new Tasks
            {
                Id = 4,
                Title = "Update Database Schema",
                Description = "Modify the database schema for the new feature.",
                DueDate = new DateTime(2025, 2, 5),
                IsCompleted = false,
                UserId = 4 // Reference to User with Id = 4
            },
            new Tasks
            {
                Id = 5,
                Title = "Refactor Code",
                Description = "Refactor the codebase to improve performance and readability.",
                DueDate = new DateTime(2025, 2, 10),
                IsCompleted = false,
                UserId = 5 // Reference to User with Id = 5
            },
            new Tasks
            {
                Id = 6,
                Title = "Conduct Code Review",
                Description = "Review the code submitted by the team for the latest feature.",
                DueDate = new DateTime(2025, 1, 23),
                IsCompleted = true,
                UserId = 6 // Reference to User with Id = 6
            },
            new Tasks
            {
                Id = 7,
                Title = "Write Unit Tests",
                Description = "Write unit tests for the new user profile feature.",
                DueDate = new DateTime(2025, 2, 1),
                IsCompleted = false,
                UserId = 7 // Reference to User with Id = 7
            },
            new Tasks
            {
                Id = 8,
                Title = "Deploy Application",
                Description = "Deploy the application to the staging environment.",
                DueDate = new DateTime(2025, 1, 28),
                IsCompleted = false,
                UserId = 8 // Reference to User with Id = 8
            },
            new Tasks
            {
                Id = 9,
                Title = "Create Database Backups",
                Description = "Backup all the relevant databases before the deployment.",
                DueDate = new DateTime(2025, 2, 3),
                IsCompleted = false,
                UserId = 9 // Reference to User with Id = 9
            },
            new Tasks
            {
                Id = 10,
                Title = "Plan Sprint",
                Description = "Organize and plan the tasks for the upcoming sprint.",
                DueDate = new DateTime(2025, 1, 21),
                IsCompleted = true,
                UserId = 10 // Reference to User with Id = 10
            }
        );
    }
}
