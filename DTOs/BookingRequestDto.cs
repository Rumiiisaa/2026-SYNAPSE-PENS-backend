namespace SynapsePENS.Api.DTOs;

public class BookingRequestDto {
    public int StudentId { get; set; }
    public int RoomId { get; set; }
    public string Purpose { get; set; } = string.Empty;
}