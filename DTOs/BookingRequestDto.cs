namespace SynapsePENS.Api.DTOs // Pastikan namespace ini benar sesuai folder
{
    public class BookingRequestDto {
    public int StudentId { get; set; }
    public int RoomId { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public DateTime StartTime { get; set; } // Pastikan ini ada
    public DateTime EndTime { get; set; }   // Pastikan ini ada
    }
}