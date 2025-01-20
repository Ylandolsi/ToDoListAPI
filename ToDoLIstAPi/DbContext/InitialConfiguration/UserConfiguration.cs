using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace ToDoLIstAPi.DbContext.InitialConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = 1,
                Name = "Yassine",
                position = "Software Developer"
            },
            new User
            {
                Id = 2,
                Name = "Fatima",
                position = "Project Manager"
            },
            new User
            {
                Id = 3,
                Name = "Ahmed",
                position = "Data Scientist"
            },
            new User
            {
                Id = 4,
                Name = "Sara",
                position = "UI/UX Designer"
            },
            new User
            {
                Id = 5,
                Name = "Ali",
                position = "DevOps Engineer"
            },
            new User
            {
                Id = 6,
                Name = "Noor",
                position = "QA Engineer"
            },
            new User
            {
                Id = 7,
                Name = "Hassan",
                position = "Mobile Developer"
            },
            new User
            {
                Id = 8,
                Name = "Mounia",
                position = "Business Analyst"
            },
            new User
            {
                Id = 9,
                Name = "Ibrahim",
                position = "Backend Developer"
            },
            new User
            {
                Id = 10,
                Name = "Amina",
                position = "Frontend Developer"
            },
            new User
            {
                Id = 11,
                Name = "Khalid",
                position = "Database Administrator"
            }
        );
    }
}
