using Microsoft.Extensions.Options;
using Models.Entities;
using ToDoLIstAPi.DbContext.InitialConfiguration;

namespace ToDoLIstAPi.DbContext;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration()); 
        modelBuilder.ApplyConfiguration(new TasksConfiguration()); 
    
    }


    DbSet<User>? Users { get; set; }
    DbSet<Tasks>? Tasks { get; set; }
}
