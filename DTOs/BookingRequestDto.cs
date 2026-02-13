using System.ComponentModel.DataAnnotations;

namespace SynapsePENS.Api.DTOs;

public class BookingRequestDto
{
    [Required(ErrorMessage = "StudentId wajib diisi.")]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "RoomId wajib diisi.")]
    public int RoomId { get; set; }

    [Required(ErrorMessage = "Tujuan peminjaman (Purpose) tidak boleh kosong.")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Purpose minimal 5 karakter dan maksimal 100 karakter.")]
    public string Purpose { get; set; } = string.Empty;
}