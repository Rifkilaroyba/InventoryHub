# InventoryHub

Aplikasi full-stack: **Blazor WebAssembly** (front-end) + **.NET Minimal API** (back-end).

## Menjalankan
```bash
# Terminal 1 - API (http://localhost:5000)
cd InventoryHub.Api && dotnet run

# Terminal 2 - Web (http://localhost:5001)
cd InventoryHub.Web && dotnet run --urls http://localhost:5001
```

## Menguji JSON (Postman / curl)
`GET http://localhost:5000/api/productlist`

```json
[
  { "id": 1, "name": "Laptop", "price": 1200.50, "stock": 25,
    "category": { "id": 101, "name": "Elektronik" } },
  { "id": 2, "name": "Headphone", "price": 50.00, "stock": 100,
    "category": { "id": 102, "name": "Aksesori" } }
]
```

## Optimasi (Aktivitas 4)
- **Back-end:** `IMemoryCache` (5 menit) + header `Cache-Control: public,max-age=60`.
- **Front-end:** cache 60 detik di `ProductService`, dedupe panggilan bersamaan, data dimuat sekali di `OnInitializedAsync`.
- **Refaktor:** model `record` ringkas, nama properti JSON konsisten (`id, name, price, stock, category`).

Lihat `REFLECTION.md` untuk ringkasan reflektif.
