# Game Collection Manager API

REST API modern dan komprehensif untuk mengelola koleksi game, developer, genre, dan platform. Proyek ini dibangun dengan **ASP.NET Core 8**, **Entity Framework Core**, **SQLite**, serta dilengkapi dengan sistem **Autentikasi & Otorisasi JWT (ASP.NET Core Identity)**, validasi data dengan **FluentValidation**, pemetaan objek dengan **AutoMapper**, dan arsitektur berlapis (**Repository & Service Pattern**).

---

## Daftar Isi

- [Fitur Utama](#fitur-utama)
- [Teknologi](#teknologi)
- [Arsitektur Proyek](#arsitektur-proyek)
- [Prasyarat](#prasyarat)
- [Cara Menjalankan Proyek](#cara-menjalankan-proyek)
- [Akses Swagger UI & Autentikasi](#akses-swagger-ui--autentikasi)
- [Konfigurasi Aplikasi](#konfigurasi-aplikasi)
- [Pengguna & Data Awal (Seed Data)](#pengguna--data-awal-seed-data)
- [Standar Respons API](#standar-respons-api)
- [Daftar Endpoint API](#daftar-endpoint-api)
- [Contoh Alur Penggunaan (cURL)](#contoh-alur-penggunaan-curl)
- [Aturan Validasi & Integritas Data](#aturan-validasi--integritas-data)
- [Pengembangan & Build](#pengembangan--build)
- [Kontribusi](#kontribusi)

---

## Fitur Utama

- **Autentikasi & Otorisasi Terintegrasi**:
  - Berbasis **JSON Web Token (JWT)** dan **ASP.NET Core Identity**.
  - Endpoint registrasi pengguna baru, login, dan pengecekan profil (`/api/auth/me`).
  - Sistem peran (*Role-Based Access Control* / RBAC): peran `Admin` dan `User`.
- **Isolasi Kepemilikan Data Pengguna (*Data Ownership*)**:
  - Developer dan Game terikat secara langsung dengan pengguna yang sedang login (`UserId`).
  - Setiap pengguna hanya dapat melihat, menambah, mengubah, dan menghapus game serta developer milik mereka sendiri.
- **Manajemen Master Data Terproteksi**:
  - Genre dan Platform dapat dilihat oleh publik/semua pengguna.
  - Operasi modifikasi (*Create*, *Update*, *Delete*) pada Genre dan Platform dibatasi khusus untuk peran `Admin`.
- **Arsitektur Multi-Tier Bersih**:
  - Pemisahan tanggung jawab yang jelas antara **Controllers**, **Services** (logika bisnis), dan **Repositories** (akses data).
- **Validasi Data Kuat (*FluentValidation*)**:
  - Setiap request DTO divalidasi menggunakan FluentValidation dengan pesan error yang spesifik dan ramah pengguna.
- **Relasi Database Lengkap**:
  - *One-to-Many*: Satu Pengguna (`ApplicationUser`) memiliki banyak Developer.
  - *One-to-Many*: Satu Developer memiliki banyak Game.
  - *Many-to-Many*: Relasi fleksibel antara Game dengan Genre (`GameGenres`) dan Game dengan Platform (`GamePlatforms`).
  - *Database Check Constraint*: Pembatasan tahun rilis game (1950–2100) langsung pada level database SQLite.
- **AutoMapper**: Pemetaan otomatis antara model entitas domain dengan Data Transfer Object (DTO).
- **Format Respons Seragam (`ApiResponseDto<T>`)**: Standarisasi format respons untuk setiap permintaan, baik berhasil maupun gagal.
- **Dokumentasi Interaktif Swagger**: Dilengkapi skema autentikasi *Bearer Token* langsung pada Swagger UI.
- **Otomatisasi Database**: Migrasi schema database dan *data seeding* (roles & akun default) dieksekusi otomatis saat aplikasi dijalankan.
- **Dukungan CORS**: Konfigurasi kebijakan CORS `AllowAll` untuk kemudahan integrasi dengan frontend/klien.

---

## Teknologi

- **Framework**: .NET 8 / ASP.NET Core Web API
- **Database & ORM**: SQLite & Entity Framework Core 8 (Code-First Migrations)
- **Autentikasi & Identitas**: ASP.NET Core Identity & JWT Bearer Authentication (`Microsoft.AspNetCore.Authentication.JwtBearer`, `System.IdentityModel.Tokens.Jwt`)
- **Validasi**: FluentValidation (`FluentValidation.DependencyInjectionExtensions`)
- **Object Mapping**: AutoMapper (`AutoMapper.Extensions.Microsoft.DependencyInjection`)
- **Dokumentasi API**: Swagger / OpenAPI (Swashbuckle)

---

## Arsitektur Proyek

Proyek ini mengadopsi pola arsitektur berlapis:

```text
GameCollectionManager/
├── Controllers/          # HTTP API Controller (Auth, Developers, Games, Genres, Platforms)
├── Data/                 # AppDbContext (EF Core & Identity) dan SeedData
├── DTOs/                 # Request & Response Model terstandarisasi (ApiResponseDto<T>)
├── MappingProfiles/      # Profil konfigurasi pemetaan objek AutoMapper
├── Migrations/           # Riwayat migrasi database Entity Framework Core
├── Models/               # Entitas domain (ApplicationUser, Developer, Game, Genre, Platform)
├── Repositories/         # Lapisan akses data (Repository Pattern & Interface)
│   └── Interface/
├── Services/             # Lapisan logika bisnis (Service Pattern & Interface)
│   └── Interface/
├── Validators/           # Validasi aturan input DTO (FluentValidation)
├── Properties/           # Pengaturan peluncuran lingkungan (launchSettings.json)
├── Program.cs            # Setup dependency injection, middleware, pipeline auth, dan inisialisasi DB
└── appsettings.json      # Konfigurasi ConnectionStrings dan JwtSettings
```

---

## Prasyarat

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) atau yang lebih baru.
- Command-line tool seperti Terminal / PowerShell / Bash.
- Browser modern atau HTTP Client (Postman / cURL / Thunder Client).

---

## Cara Menjalankan Proyek

1. **Clone repositori dan masuk ke direktori proyek**:

   ```bash
   git clone <URL_REPOSITORI>
   cd GameCollectionManager
   ```

2. **Jalankan aplikasi**:

   ```bash
   dotnet run --project GameCollectionManager
   ```

   Saat pertama kali dijalankan, aplikasi secara otomatis:
   - Menerapkan seluruh migrasi database ke berkas SQLite (`gamecollection.db`).
   - Melakukan *seed* peran `Admin` dan `User`.
   - Membuat akun default untuk Administrator dan User.

---

## Akses Swagger UI & Autentikasi

Swagger UI dikonfigurasi pada *root path* aplikasi ketika berada pada mode `Development`:

- **HTTP**: [http://localhost:5248](http://localhost:5248) atau [http://localhost:5248/swagger](http://localhost:5248/swagger)
- **HTTPS**: [https://localhost:7055](https://localhost:7055) atau [https://localhost:7055/swagger](https://localhost:7055/swagger)

### Menggunakan Autentikasi di Swagger UI:
1. Jalankan request ke endpoint `POST /api/auth/login` menggunakan salah satu akun bawaan.
2. Salin token JWT yang dihasilkan pada properti `data.token`.
3. Klik tombol **Authorize** (ikon gembok) di kanan atas Swagger UI.
4. Masukkan dengan format: `Bearer <TOKEN_ANDA>` (atau cukup masukkan token tergantung prompt).
5. Klik **Authorize**, lalu tutup modal. Sekarang Anda dapat menguji endpoint yang terproteksi.

---

## Konfigurasi Aplikasi

Konfigurasi aplikasi disimpan di `GameCollectionManager/appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=gamecollection.db"
  },
  "JwtSettings": {
    "Secret": "your-super-secret-key-that-is-at-least-256-bits-long-for-jwt-token-generation",
    "ExpirationInDays": 7
  }
}
```

> [!NOTE]
> Untuk keperluan produksi, pastikan mengganti nilai `JwtSettings:Secret` dengan string rahasia yang aman dan simpan di *Environment Variables* atau *User Secrets*. Berkas database SQLite (`gamecollection.db`) diabaikan oleh Git.

---

## Pengguna & Data Awal (Seed Data)

Secara otomatis diinisialisasi saat aplikasi pertama kali dijalankan:

| Role | Email | Password Bawaan | Hak Akses |
| --- | --- | --- | --- |
| **Admin** | `admin@gamecollectionmanager.com` | `Admin123!` | Akses penuh ke semua fitur, termasuk modifikasi master Genre & Platform. |
| **User** | `user@gamecollectionmanager.com` | `User123!` | Akses CRUD ke Developer & Game miliknya sendiri; read-only untuk Genre & Platform. |

---

## Standar Respons API

Semua endpoint mengembalikan struktur respons seragam berbasis `ApiResponseDto<T>`:

### Format Berhasil
```json
{
  "success": true,
  "message": "Game created successfully",
  "data": {
    "id": 1,
    "title": "The Witcher 3: Wild Hunt",
    "description": "Open world RPG.",
    "releaseYear": 2015,
    "developer": {
      "id": 1,
      "name": "CD Projekt Red",
      "location": "Poland",
      "createdAt": "2026-09-07T00:00:00Z"
    },
    "genres": [
      { "id": 1, "name": "RPG" }
    ],
    "platforms": [
      { "id": 1, "name": "PC" }
    ]
  },
  "errors": []
}
```

### Format Gagal / Error Validasi
```json
{
  "success": false,
  "message": "Validation failed",
  "data": null,
  "errors": [
    "Title cannot exceed 200 characters",
    "Release year must be between 1950 and 2100"
  ]
}
```

---

## Daftar Endpoint API

Base URL lokal: `http://localhost:5248`

### 1. Autentikasi (`/api/auth`)

| Method | Endpoint | Otorisasi | Deskripsi |
| --- | --- | --- | --- |
| `POST` | `/api/auth/register` | Publik | Mendaftarkan pengguna baru (otomatis diberi peran `User`) |
| `POST` | `/api/auth/login` | Publik | Otentikasi pengguna dan mengembalikan JWT Token |
| `GET` | `/api/auth/me` | Authenticated | Mengambil profil dan peran pengguna yang sedang login |

### 2. Games (`/api/games`)

> Setiap operasi game terisolasi per pengguna; pengguna hanya mengelola game yang terhubung ke developer milik pengguna bersangkutan.

| Method | Endpoint | Otorisasi | Deskripsi |
| --- | --- | --- | --- |
| `GET` | `/api/games` | Authenticated | Mengambil seluruh game milik pengguna |
| `GET` | `/api/games/{id}` | Authenticated | Mengambil detail game berdasarkan ID milik pengguna |
| `POST` | `/api/games` | Authenticated | Menambahkan game baru |
| `PUT` | `/api/games/{id}` | Authenticated | Memperbarui data game milik pengguna |
| `DELETE` | `/api/games/{id}` | Authenticated | Menghapus game milik pengguna |

### 3. Developers (`/api/developers`)

> Setiap operasi developer terisolasi per pengguna berdasarkan token autentikasi.

| Method | Endpoint | Otorisasi | Deskripsi |
| --- | --- | --- | --- |
| `GET` | `/api/developers` | Authenticated | Mengambil seluruh developer milik pengguna |
| `GET` | `/api/developers/{id}` | Authenticated | Mengambil data developer berdasarkan ID milik pengguna |
| `POST` | `/api/developers` | Authenticated | Menambahkan developer baru |
| `PUT` | `/api/developers/{id}` | Authenticated | Memperbarui data developer milik pengguna |
| `DELETE` | `/api/developers/{id}` | Authenticated | Menghapus data developer milik pengguna |

### 4. Genres (`/api/genres`)

| Method | Endpoint | Otorisasi | Deskripsi |
| --- | --- | --- | --- |
| `GET` | `/api/genres` | Publik | Mengambil semua daftar genre |
| `GET` | `/api/genres/{id}` | Publik | Mengambil genre berdasarkan ID |
| `POST` | `/api/genres` | **Admin** | Menambahkan genre baru |
| `PUT` | `/api/genres/{id}` | **Admin** | Memperbarui nama genre |
| `DELETE` | `/api/genres/{id}` | **Admin** | Menghapus genre |

### 5. Platforms (`/api/platforms`)

| Method | Endpoint | Otorisasi | Deskripsi |
| --- | --- | --- | --- |
| `GET` | `/api/platforms` | Publik | Mengambil semua daftar platform |
| `GET` | `/api/platforms/{id}` | Publik | Mengambil platform berdasarkan ID |
| `POST` | `/api/platforms` | **Admin** | Menambahkan platform baru |
| `PUT` | `/api/platforms/{id}` | **Admin** | Memperbarui nama platform |
| `DELETE` | `/api/platforms/{id}` | **Admin** | Menghapus platform |

---

## Contoh Alur Penggunaan (cURL)

### 1. Registrasi Akun Baru

```bash
curl -X POST http://localhost:5248/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "player@example.com",
    "password": "Password123!",
    "confirmPassword": "Password123!",
    "firstName": "Player",
    "lastName": "One"
  }'
```

### 2. Login & Mendapatkan Token JWT

```bash
curl -X POST http://localhost:5248/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "player@example.com",
    "password": "Password123!"
  }'
```

*Salin `token` dari respons JSON untuk digunakan pada request berikutnya.*

### 3. Menambahkan Genre Baru (Login sebagai Admin)

```bash
curl -X POST http://localhost:5248/api/genres \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <ADMIN_JWT_TOKEN>" \
  -d '{
    "name": "Action RPG"
  }'
```

### 4. Menambahkan Platform Baru (Login sebagai Admin)

```bash
curl -X POST http://localhost:5248/api/platforms \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <ADMIN_JWT_TOKEN>" \
  -d '{
    "name": "PlayStation 5"
  }'
```

### 5. Menambahkan Developer Baru

```bash
curl -X POST http://localhost:5248/api/developers \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <USER_JWT_TOKEN>" \
  -d '{
    "name": "FromSoftware",
    "location": "Japan"
  }'
```

### 6. Menambahkan Game

```bash
curl -X POST http://localhost:5248/api/games \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <USER_JWT_TOKEN>" \
  -d '{
    "title": "Elden Ring",
    "description": "Action RPG in the Lands Between.",
    "releaseYear": 2022,
    "developerId": 1,
    "genreIds": [1],
    "platformIds": [1]
  }'
```

### 7. Mengambil Koleksi Game Milik Pengguna

```bash
curl -X GET http://localhost:5248/api/games \
  -H "Authorization: Bearer <USER_JWT_TOKEN>"
```

---

## Aturan Validasi & Integritas Data

### Validasi Request (FluentValidation):
- **User Registration**:
  - `Email`: Wajib diisi, format email valid, unik di database.
  - `Password`: Minimal 6 karakter, wajib memiliki huruf besar (*uppercase*), huruf kecil (*lowercase*), dan angka.
  - `ConfirmPassword`: Wajib sama dengan `Password`.
  - `FirstName` & `LastName`: Maksimal 100 karakter.
- **Developer**:
  - `Name`: Wajib diisi, maksimal 200 karakter, nama tidak boleh duplikat.
  - `Location`: Maksimal 200 karakter.
- **Game**:
  - `Title`: Wajib diisi, maksimal 200 karakter.
  - `Description`: Wajib diisi, maksimal 2000 karakter.
  - `ReleaseYear`: Harus dalam rentang 1950 hingga 2100.
  - `DeveloperId`: Wajib lebih dari 0 dan harus merupakan developer milik pengguna yang bersangkutan.
  - `GenreIds` & `PlatformIds`: ID harus lebih dari 0.
- **Genre & Platform**:
  - `Name`: Wajib diisi, maksimal 100 karakter, nama unik.

### Integritas Database:
- Database Check Constraint: `CK_Games_ReleaseYear` memastikan integritas data tahun rilis pada level SQLite (`BETWEEN 1950 AND 2100`).
- Cascade Delete: Menghapus akun pengguna akan menghapus seluruh data developer terkait.
- Restrict Delete: Menghapus developer dibatasi jika masih memiliki relasi aktif ke game.

---

## Pengembangan & Build

Untuk melakukan kompilasi proyek tanpa menjalankannya:

```bash
dotnet build GameCollectionManager.sln
```

Untuk menjalankan migrasi Entity Framework Core secara manual (jika diperlukan):

```bash
dotnet ef database update --project GameCollectionManager
```

---

## Kontribusi

Kontribusi dipersilakan! Silakan buat *fork* dari repositori ini, buat *feature branch* baru untuk perubahan Anda, dan ajukan *pull request* dengan penjelasan yang jelas.
