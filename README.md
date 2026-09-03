# Game Collection Manager API

REST API untuk mengelola koleksi game, developer, genre, dan platform. Proyek ini dibangun dengan ASP.NET Core 8, Entity Framework Core, dan SQLite.

## Fitur

- CRUD untuk game, developer, genre, dan platform.
- Relasi satu developer ke banyak game.
- Relasi many-to-many antara game–genre dan game–platform.
- Validasi data melalui Data Annotations.
- Dokumentasi interaktif Swagger pada lingkungan Development.
- Migrasi database dan data awal dijalankan otomatis saat aplikasi mulai.

## Teknologi

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core 8
- SQLite
- Swagger / OpenAPI

## Prasyarat

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) atau versi yang kompatibel

## Menjalankan Proyek

1. Clone repositori lalu masuk ke direktorinya.

   ```bash
   git clone <URL_REPOSITORI>
   cd GameCollectionManager
   ```

2. Jalankan aplikasi.

   ```bash
   dotnet run --project GameCollectionManager
   ```

   Pada proses awal, aplikasi akan membuat/memperbarui database SQLite dan mengisinya dengan data contoh.

3. Buka Swagger UI di browser:

   ```text
   http://localhost:5248/swagger
   ```

   Port mengikuti profil `http` bawaan. Jika menggunakan profil HTTPS, Swagger tersedia di `https://localhost:7055/swagger`.

## Konfigurasi Database

Secara bawaan, koneksi SQLite didefinisikan di `GameCollectionManager/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=gamecollection.db"
  }
}
```

Anda dapat menggantinya melalui pengaturan lingkungan atau mengubah konfigurasi sesuai kebutuhan. Berkas database SQLite tidak ikut dilacak oleh Git.

## Endpoint API

Base URL lokal: `http://localhost:5248`

| Resource | Endpoint | Method | Deskripsi |
| --- | --- | --- | --- |
| Games | `/api/games` | `GET` | Mengambil semua game |
| Games | `/api/games/{id}` | `GET` | Mengambil game berdasarkan ID |
| Games | `/api/games` | `POST` | Menambahkan game |
| Games | `/api/games/{id}` | `PUT` | Memperbarui game |
| Games | `/api/games/{id}` | `DELETE` | Menghapus game |
| Developers | `/api/developers` | `GET`, `POST` | Mengambil atau menambahkan developer |
| Developers | `/api/developers/{id}` | `GET`, `PUT`, `DELETE` | Mengelola developer berdasarkan ID |
| Genres | `/api/genres` | `GET`, `POST` | Mengambil atau menambahkan genre |
| Genres | `/api/genres/{id}` | `GET`, `PUT`, `DELETE` | Mengelola genre berdasarkan ID |
| Platforms | `/api/platforms` | `GET`, `POST` | Mengambil atau menambahkan platform |
| Platforms | `/api/platforms/{id}` | `GET`, `PUT`, `DELETE` | Mengelola platform berdasarkan ID |

## Contoh Penggunaan

### Menambahkan game

```bash
curl -X POST http://localhost:5248/api/games \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Cyberpunk 2077",
    "description": "Open-world action RPG.",
    "releaseYear": 2020,
    "developerId": 1,
    "genreIds": [1, 2],
    "platformIds": [1, 2]
  }'
```

### Menambahkan developer

```bash
curl -X POST http://localhost:5248/api/developers \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Example Studio",
    "location": "Indonesia"
  }'
```

### Mengambil seluruh koleksi game

```bash
curl http://localhost:5248/api/games
```

## Data Awal

Saat database masih kosong, aplikasi menambahkan contoh data berikut:

- Developer: CD Projekt Red, Rockstar Games, dan FromSoftware.
- Genre: RPG, Action, Adventure, dan Open World.
- Platform: PC, PlayStation 5, dan Xbox Series X.
- Game: *The Witcher 3*, *Red Dead Redemption 2*, dan *Elden Ring*.

## Struktur Proyek

```text
GameCollectionManager/
├── Controllers/          # Endpoint HTTP
├── Data/                 # DbContext Entity Framework Core
├── DTOs/                 # Model request dan response API
├── Models/               # Entitas database
├── Services/             # Logika bisnis
├── Migrations/           # Riwayat migrasi database
├── Program.cs            # Konfigurasi aplikasi dan data awal
└── appsettings.json      # Konfigurasi aplikasi
```

## Validasi dan Respons

- `Title` dan nama developer wajib diisi, maksimal 200 karakter.
- Nama genre dan platform wajib diisi, maksimal 100 karakter.
- `ReleaseYear` harus berada pada rentang 1950–2100.
- Developer harus tersedia ketika membuat game; jika tidak, API mengembalikan `400 Bad Request`.
- Resource yang tidak ditemukan akan mengembalikan `404 Not Found`.

## Pengembangan

Untuk membangun proyek tanpa menjalankannya:

```bash
dotnet build GameCollectionManager.sln
```

Swagger hanya diaktifkan ketika `ASPNETCORE_ENVIRONMENT` bernilai `Development`.

## Kontribusi

Kontribusi dipersilakan. Silakan buat fork, buat branch untuk perubahan Anda, lalu ajukan pull request dengan deskripsi yang jelas.
