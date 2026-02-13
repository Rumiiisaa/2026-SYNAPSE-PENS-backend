namespace SynapsePENS.Api.Entities;

public class Booking {
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int RoomId { get; set; }
    public DateTime BookingDate { get; set; }
    public string Purpose { get; set; } = string.Empty;
}