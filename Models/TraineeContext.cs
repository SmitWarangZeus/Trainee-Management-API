using Microsoft.EntityFrameworkCore;

namespace TraineeManagement.api.Models;

public class TraineeContext : DbContext
{
    public TraineeContext(DbContextOptions<TraineeContext> options)
        : base(options)
    {
    }

    public DbSet<Trainee> Trainees { get; set; }
}
