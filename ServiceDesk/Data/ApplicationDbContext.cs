using Microsoft.EntityFrameworkCore;
using ServiceDesk.Models;
using Task = ServiceDesk.Models.Task;


namespace ServiceDesk.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Executor> Executors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Role>().HasData(
            new Role 
            { 
                Id = 1, 
                RoleName = "admin" 
            },
            new Role 
            { 
                Id = 2,
                RoleName = "Заказчик" 
            },
            new Role 
            {
                Id = 3,
                RoleName = "Исполнитель" 
            });
        modelBuilder.Entity<User>().HasData( new User
        {
            Id = Guid.NewGuid(), 
            Email = "admin@mail.ru", 
            Password = "Qwerty123", 
            RoleId = 1
        });
        modelBuilder.Entity<Status>().HasData(
            new Status
            {
                StatusId = 1,
                StatusName = "Новая",
            },
            new Status
            {
                StatusId = 2,
                StatusName = "В работе",
            },
            new Status
            {
                StatusId = 3,
                StatusName = "Выполнено",
            }); 
    }
}