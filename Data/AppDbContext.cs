using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.Models;

namespace TraineeManagement.api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Trainee> Trainees { get; set; }
    
    public DbSet<User> Users { get; set; }

    public DbSet<Mentor> Mentors { get; set; }
}
