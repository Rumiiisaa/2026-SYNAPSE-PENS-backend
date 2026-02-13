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

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _context.Bookings.ToListAsync());

    [HttpGet("students")]
    public async Task<IActionResult> GetStudents() => Ok(await _context.Students.ToListAsync());

    [HttpGet("rooms")]
    public async Task<IActionResult> GetRooms() => Ok(await _context.Rooms.ToListAsync());

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
        return Ok(booking);
    }
}