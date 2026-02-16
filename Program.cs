using Microsoft.EntityFrameworkCore;
using SynapsePENS.Api.Data;
using System.Text.Json.Serialization;

// 1. FIX UTAMA: Menangani error DateTime PostgreSQL agar tidak otomatis berubah ke UTC
// Baris ini harus tetap berada di paling atas sebelum builder dibuat.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// 2. Tambahkan pengaturan JSON agar tidak terjadi error "Circular Reference" 
// saat mengambil data Booking yang memiliki Student dan Room.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Konfigurasi CORS (Sudah benar, tetap pertahankan)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 4. Konfigurasi Database PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 5. Konfigurasi Middleware Pipeline
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Synapse API v1");
        c.RoutePrefix = string.Empty; // Agar Swagger langsung terbuka di domain utama
    });
}

// Pastikan urutan middleware benar
app.UseCors("AllowAll");

// Nonaktifkan HttpsRedirection sementara jika Anda masih dalam tahap testing lokal 
// dan sering mengalami masalah sertifikat SSL
// app.UseHttpsRedirection(); 

app.UseAuthorization();
app.MapControllers();

app.Run();