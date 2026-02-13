# SYNAPSE: System for Unified Network of Academic Space Scheduling Engine 🚀

Sistem Informasi Peminjaman Ruangan berbasis ASP.NET Core dan PostgreSQL. Project ini merupakan bagian dari ekosistem Synapse PENS.

## ✨ Fitur Utama
- **CRUD Booking**: Transaksi peminjaman ruangan oleh mahasiswa.
- **Data Seeding**: Data master Mahasiswa dan Ruangan otomatis tersedia.
- **DTO Implementation**: Mengamankan input data menggunakan Data Transfer Object.
- **PostgreSQL Integration**: Penyimpanan data relasional yang stabil.

## 🛠️ Teknologi yang Digunakan
- **Framework**: .NET 10 (ASP.NET Core Web API)
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **Documentation**: Swagger UI / OpenAPI

## 📂 Struktur Project (Separation of Concerns)
- `Entities/`: Representasi tabel database (Student, Room, Booking).
- `DTOs/`: Objek khusus untuk transfer data input dari user.
- `Data/`: Konfigurasi konteks database dan seeding data awal.
- `Controllers/`: Logika endpoint API untuk memproses permintaan.

## 🚀 Cara Menjalankan Project
1. Clone repository ini.
2. Pastikan PostgreSQL berjalan dan sesuaikan `ConnectionStrings` di `appsettings.json`.
3. Jalankan perintah di Terminal:
   ```bash
   dotnet ef database update
   dotnet run
