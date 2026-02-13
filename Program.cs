using Microsoft.EntityFrameworkCore;
using SynapsePENS.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Tambahkan dukungan untuk Controller
builder.Services.AddControllers();

// 2. Tambahkan Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. TAMBAHKAN CORS (PENTING untuk Frontend)
// Ini agar browser tidak memblokir permintaan dari React/Vue/Flutter nanti
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// 4. Konfigurasi Database PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 5. Konfigurasi HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SynapsePENS API V1");
        c.RoutePrefix = string.Empty; // Swagger muncul di localhost:port/
    });
}

// URUTAN INI SANGAT PENTING:
app.UseHttpsRedirection();

// 6. AKTIFKAN CORS (Harus di atas UseAuthorization)
app.UseCors("AllowAll");

app.UseAuthorization();

// 7. Map Controllers
app.MapControllers();

app.Run();