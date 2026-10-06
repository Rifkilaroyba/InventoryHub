// ============================================================
// InventoryHub.Api - Minimal API (Back-End)
// Catatan Copilot:
//  - Aktivitas 3: Copilot membantu merapikan struktur JSON (camelCase,
//    objek "category" bersarang, harga bertipe decimal).
//  - Aktivitas 4: Copilot menyarankan IMemoryCache + header Cache-Control
//    untuk mengurangi beban server pada endpoint yang datanya jarang berubah.
// ============================================================
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddCors(o => o.AddPolicy("web", p =>
    p.WithOrigins("http://localhost:5001").AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseCors("web");

const string CacheKey = "productlist";

app.MapGet("/api/productlist", (IMemoryCache cache, HttpContext ctx) =>
{
    // Back-end cache: data dibangun sekali, lalu dipakai ulang selama 5 menit.
    var products = cache.GetOrCreate(CacheKey, entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
        return BuildProducts();
    });

    // Browser/client cache: kurangi permintaan berulang dari front-end.
    ctx.Response.Headers.CacheControl = "public,max-age=60";
    return Results.Ok(products);
})
.WithName("GetProductList");

app.Run("http://localhost:5000");

static Product[] BuildProducts() =>
[
    new(1, "Laptop",    1200.50m, 25,  new Category(101, "Elektronik")),
    new(2, "Headphone", 50.00m,   100, new Category(102, "Aksesori"))
];

// Hasil serialisasi (camelCase): id, name, price, stock, category{id,name}
record Category(int Id, string Name);
record Product(int Id, string Name, decimal Price, int Stock, Category Category);
