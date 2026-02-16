# SYNAPSE: System for Unified Network of Academic Space Scheduling Engine 🚀

Sistem Informasi Peminjaman Ruangan berbasis ASP.NET Core dan PostgreSQL. Project ini merupakan bagian dari ekosistem Synapse PENS.

## ✨ Fitur Utama
- **Validation Logic**: Mencegah bentrokan jadwal (overlap) pada ruangan yang sama.
- **Timezone Fix**: Menggunakan `DateTimeKind.Unspecified` untuk konsistensi waktu lokal (WIB).
- **Soft Delete**: Data yang dihapus akan otomatis masuk ke filter `/history`.
- **Swagger Open API**: Dokumentasi endpoint interaktif.

## 🛠️ Tech Stack
- **Framework**: .NET 10 Web API
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core

## 📂 Struktur Project
```text
SynapsePENS.Api/
├── Controllers/          # Berisi logic API Endpoints (BookingsController.cs)
├── Data/                 # Konfigurasi Database & Context (ApplicationDbContext.cs)
├── DTOs/                 # Data Transfer Objects untuk validasi input request
├── Entities/             # Class Model Database (Booking.cs, Student.cs, Room.cs)
├── Migrations/           # File generate otomatis dari EF Core untuk skema DB
├── appsettings.json      # Konfigurasi aplikasi & Connection String
└── Program.cs            # Entry point & registrasi service

## 🚀 Cara Menjalankan Project
1. Clone repository ini.
2. Pastikan PostgreSQL berjalan dan sesuaikan `ConnectionStrings` di `appsettings.json`.
3. Jalankan perintah di Terminal:
   ```bash
   dotnet ef database update
   dotnet run
