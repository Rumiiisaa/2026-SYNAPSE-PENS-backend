public class Booking {
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int RoomId { get; set; }
    public DateTime BookingDate { get; set; }
    public string Purpose { get; set; } = string.Empty;

    // Tambahkan ini agar Nama bisa muncul
    public Student? Student { get; set; }
    public Room? Room { get; set; }
}