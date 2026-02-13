using Microsoft.EntityFrameworkCore;
using SynapsePENS.Api.Entities;

namespace SynapsePENS.Api.Data;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        // SEEDING DATA: Menambahkan Identitas Kamu ke Database
        modelBuilder.Entity<Student>().HasData(
            new Student { 
                Id = 1, 
                Name = "Akari Kanzoo Triputra", 
                NRP = "3124600004" 
            }
        );

        // Seed Data Ruangan PENS
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, RoomName = "Ruang Robotik", Capacity = 20 },
            new Room { Id = 2, RoomName = "Aula Gedung TC", Capacity = 100 },
            new Room { Id = 3, RoomName = "Lab Data Science", Capacity = 30 }
        );
    }
}