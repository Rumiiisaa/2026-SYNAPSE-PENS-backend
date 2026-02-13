using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SynapsePENS.Api.Data;
using SynapsePENS.Api.Entities;
using SynapsePENS.Api.DTOs;

namespace SynapsePENS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public BookingsController(ApplicationDbContext context) => _context = context;

    // 1. GET ALL (Dengan Include agar nama muncul)
    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var bookings = await _context.Bookings
            .Include(b => b.Student)
            .Include(b => b.Room)
            .ToListAsync();
        return Ok(bookings);
    }

    // 2. GET BY ID (Untuk melihat detail peminjaman)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) {
        var booking = await _context.Bookings
            .Include(b => b.Student)
            .Include(b => b.Room)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null) return NotFound("Data peminjaman tidak ditemukan.");
        return Ok(booking);
    }

    // 3. POST (Tetap sama, sudah benar menggunakan DTO)
    [HttpPost]
    public async Task<IActionResult> Create(BookingRequestDto request) {
        var booking = new Booking {
            StudentId = request.StudentId,
            RoomId = request.RoomId,
            Purpose = request.Purpose,
            BookingDate = DateTime.UtcNow
        };
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
    }

    // 4. PUT (Untuk Mengubah data peminjaman)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookingRequestDto request) {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return NotFound();

        booking.StudentId = request.StudentId;
        booking.RoomId = request.RoomId;
        booking.Purpose = request.Purpose;

        await _context.SaveChangesAsync();
        return Ok(new { Message = "Data berhasil diperbarui", Data = booking });
    }

    // 5. DELETE (Untuk Menghapus data peminjaman)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return NotFound();

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Data peminjaman berhasil dihapus" });
    }

    // Helper Endpoints untuk Dropdown di Frontend
    [HttpGet("students")]
    public async Task<IActionResult> GetStudents() => Ok(await _context.Students.ToListAsync());

    [HttpGet("rooms")]
    public async Task<IActionResult> GetRooms() => Ok(await _context.Rooms.ToListAsync());
}