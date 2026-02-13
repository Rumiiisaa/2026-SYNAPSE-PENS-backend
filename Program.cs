using Microsoft.EntityFrameworkCore;
using SynapsePENS.Api.Data;
// Pastikan package ini sudah terinstall
using Npgsql.EntityFrameworkCore.PostgreSQL; 

var builder = WebApplication.CreateBuilder(args);

// 1. Tambahkan dukungan untuk Controller (Sudah benar)
builder.Services.AddControllers();

// 2. Tambahkan Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Konfigurasi Database PostgreSQL (Sudah benar)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 4. Konfigurasi HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Baris ini memastikan UI Swagger muncul saat aplikasi dijalankan
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SynapsePENS API V1");
        c.RoutePrefix = string.Empty; // Membuat Swagger muncul langsung di halaman utama (localhost:port/)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 5. MAP CONTROLLERS: Ini baris paling krusial agar file di folder 'Controllers' dikenali
app.MapControllers();

app.Run();