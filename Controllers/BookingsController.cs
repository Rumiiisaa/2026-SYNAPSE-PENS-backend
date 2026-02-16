using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SynapsePENS.Api.Data;
using SynapsePENS.Api.Entities;
using SynapsePENS.Api.DTOs;

namespace SynapsePENS.Api.Controllers;

public class StatusUpdateDto {
    public string Status { get; set; } = string.Empty;
}

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase 
{
    private readonly ApplicationDbContext _context;

    public BookingsController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAll() 
    {
        var bookings = await _context.Bookings
            .Include(b => b.Student)
            .Include(b => b.Room)
            .OrderByDescending(b => b.StartTime)
            .ToListAsync();
        return Ok(bookings);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetHistory() 
    {
        var history = await _context.Bookings
            .IgnoreQueryFilters()
            .Include(b => b.Student)
            .Include(b => b.Room)
            .Where(b => b.IsDeleted)
            .OrderByDescending(b => b.StartTime)
            .ToListAsync();
        return Ok(history);
    }

    // 3. POST: Create (Dengan Validasi Bentrokan)
    [HttpPost]
    public async Task<IActionResult> Create(BookingRequestDto request) 
    {
        if (request.EndTime <= request.StartTime)
            return BadRequest("Waktu selesai harus setelah waktu mulai.");

        // Paksa jam dianggap Unspecified agar sinkron dengan database PostgreSQL
        var startTime = DateTime.SpecifyKind(request.StartTime, DateTimeKind.Unspecified);
        var endTime = DateTime.SpecifyKind(request.EndTime, DateTimeKind.Unspecified);

        // --- LOGIKA CEK BENTROKAN JADWAL ---
        // Mencari apakah ada booking di ruangan yang sama, yang belum dihapus, 
        // bukan status "Ditolak", dan waktunya tumpang tindih.
        bool isRoomBusy = await _context.Bookings.AnyAsync(b => 
            b.RoomId == request.RoomId && 
            !b.IsDeleted && 
            b.Status != "Ditolak" &&
            ((startTime >= b.StartTime && startTime < b.EndTime) || 
             (endTime > b.StartTime && endTime <= b.EndTime) ||
             (startTime <= b.StartTime && endTime >= b.EndTime)));

        if (isRoomBusy)
        {
            return BadRequest("Ruangan tidak tersedia pada jadwal tersebut. Silakan pilih waktu atau ruangan lain.");
        }
        // ------------------------------------

        var booking = new Booking {
            StudentId = request.StudentId,
            RoomId = request.RoomId,
            Purpose = request.Purpose,
            StartTime = startTime,
            EndTime = endTime,
            Status = "Menunggu Persetujuan"
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return Ok(booking);
    }

    // 4. PUT: Update (Dengan Validasi Bentrokan)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookingRequestDto request)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return NotFound();

        var startTime = DateTime.SpecifyKind(request.StartTime, DateTimeKind.Unspecified);
        var endTime = DateTime.SpecifyKind(request.EndTime, DateTimeKind.Unspecified);

        // --- LOGIKA CEK BENTROKAN JADWAL (Kecuali Booking ini sendiri) ---
        bool isRoomBusy = await _context.Bookings.AnyAsync(b => 
            b.Id != id && // Abaikan ID yang sedang diedit agar tidak menabrak diri sendiri
            b.RoomId == request.RoomId && 
            !b.IsDeleted &&
            b.Status != "Ditolak" &&
            ((startTime >= b.StartTime && startTime < b.EndTime) || 
             (endTime > b.StartTime && endTime <= b.EndTime) ||
             (startTime <= b.StartTime && endTime >= b.EndTime)));

        if (isRoomBusy)
        {
            return BadRequest("Gagal update: Jadwal baru bertabrakan dengan peminjaman lain.");
        }
        // ------------------------------------------------------------------

        booking.StudentId = request.StudentId;
        booking.RoomId = request.RoomId;
        booking.Purpose = request.Purpose;
        booking.StartTime = startTime;
        booking.EndTime = endTime;

        await _context.SaveChangesAsync();
        return Ok(booking);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusUpdateDto request) 
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return NotFound();

        booking.Status = request.Status;
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Status diperbarui", Data = booking });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) 
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return NotFound();

        booking.IsDeleted = true;
        booking.Status = "Dibatalkan"; 

        await _context.SaveChangesAsync();
        return Ok(new { Message = "Berhasil dihapus." });
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetStudents() => Ok(await _context.Students.ToListAsync());

    [HttpPost("students")]
    public async Task<IActionResult> CreateStudent([FromBody] Student student) 
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return Ok(student);
    }

    [HttpGet("rooms")]
    public async Task<IActionResult> GetRooms() => Ok(await _context.Rooms.ToListAsync());
}