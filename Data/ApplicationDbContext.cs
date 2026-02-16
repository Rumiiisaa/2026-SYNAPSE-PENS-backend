using Microsoft.EntityFrameworkCore;
using SynapsePENS.Api.Entities;

namespace SynapsePENS.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Konfigurasi Logika Tabel Booking
        modelBuilder.Entity<Booking>(entity =>
        {
            // Nilai default status sesuai kriteria
            entity.Property(b => b.Status)
                .IsRequired()
                .HasDefaultValue("Menunggu Persetujuan");

            // Karena BookingDate sudah dihapus dan diganti StartTime/EndTime, 
            // kita tidak lagi menggunakan HasDefaultValueSql("CURRENT_TIMESTAMP") 
            // agar waktu benar-benar sesuai dengan pilihan user dari frontend.

            // Filter Global: Data yang IsDeleted = true secara otomatis disembunyikan
            // Ini mendukung fitur Soft Delete (Poin 3 PDF)
            entity.HasQueryFilter(b => !b.IsDeleted);
        });

        // 2. Seeding Data Ruangan (Data Master)
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, RoomName = "C 101", Capacity = 30 },
            new Room { Id = 2, RoomName = "C 102", Capacity = 30 },
            new Room { Id = 3, RoomName = "C 103", Capacity = 30 },
            new Room { Id = 4, RoomName = "C 104", Capacity = 30 },
            new Room { Id = 5, RoomName = "C 105", Capacity = 30 },
            new Room { Id = 6, RoomName = "C 106", Capacity = 30 },
            new Room { Id = 7, RoomName = "C 201", Capacity = 30 },
            new Room { Id = 8, RoomName = "C 202", Capacity = 30 },
            new Room { Id = 9, RoomName = "C 203", Capacity = 30 },
            new Room { Id = 10, RoomName = "C 204", Capacity = 30 },
            new Room { Id = 11, RoomName = "C 205", Capacity = 30 },
            new Room { Id = 12, RoomName = "SAW-03.06", Capacity = 120 },
            new Room { Id = 13, RoomName = "SAW-06.08", Capacity = 120 }
        );
    }
}