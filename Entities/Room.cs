namespace SynapsePENS.Api.Entities;

public class Room {
    public int Id { get; set; }
    public string RoomName { get; set; } = string.Empty;
    public int Capacity { get; set; }
}