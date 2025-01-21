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
                position = "Software Developer",
                Username = "yassine",
                PasswordHash = "$2a$11$143FvUPdT95WWhJS3gP2JuDpj3zyezQC5Q8104ioM1YBKOH2UR3qi" ,
                Role = "Admin"
            },
            new User
            {
                Id = 2,
                Name = "Fatima",
                position = "Project Manager",
                Username = "fatima",
                PasswordHash = "$2a$11$BN6yt1TSvhtJUyrA3ezN6OhJIS7O3pTv3H0TVxTtglyURJBCjI3z2" , 
                Role = "Admin"
            },
            new User
            {
                Id = 3,
                Name = "Ahmed",
                position = "Data Scientist",
                Username = "ahmed",
                PasswordHash = "$2a$11$UiFQw1BdHwK1C13FyCc4G.ZzZZEToQbWOr12CGkyonsL11DTwnE8S" , 
                Role = "employee"
            }

        );
    }
}
