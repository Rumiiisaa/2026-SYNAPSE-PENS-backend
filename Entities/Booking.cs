namespace SynapsePENS.Api.Entities;

public class Booking
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int RoomId { get; set; }
    public DateTime StartTime { get; set; } // Pastikan ini StartTime
    public DateTime EndTime { get; set; }   // Pastikan ini EndTime
    public string Purpose { get; set; } = string.Empty;
    public string Status { get; set; } = "Menunggu Persetujuan";
    public bool IsDeleted { get; set; } = false;

    public Student? Student { get; set; }
    public Room? Room { get; set; }
}