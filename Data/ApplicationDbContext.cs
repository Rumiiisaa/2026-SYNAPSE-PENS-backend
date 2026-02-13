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
        // Tanggal tetap agar tidak error 'PendingModelChanges'
        var seedDate = new DateTime(2026, 2, 14, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "Mahasiswa PENS", NRP = "2110191001" }
        );

        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, RoomName = "Ruang Robotik", Capacity = 20 },
            new Room { Id = 2, RoomName = "Aula Gedung TC", Capacity = 100 }
        );
    }
}