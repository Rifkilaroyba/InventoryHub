using System.Net.Http.Json;
using InventoryHub.Web.Models;

namespace InventoryHub.Web.Services;

// Catatan Copilot (Aktivitas 4): Copilot menyarankan cache sisi klien dan
// "in-flight task" agar beberapa komponen yang meminta data bersamaan
// hanya memicu SATU panggilan API.
public class ProductService(HttpClient http)
{
    private static readonly TimeSpan Ttl = TimeSpan.FromSeconds(60);
    private List<Product>? _cache;
    private DateTime _loadedAt = DateTime.MinValue;
    private Task<List<Product>>? _inFlight;

    public Task<List<Product>> GetProductsAsync(bool forceRefresh = false)
    {
        if (!forceRefresh && _cache is not null && DateTime.UtcNow - _loadedAt < Ttl)
            return Task.FromResult(_cache);

        return _inFlight ??= LoadAsync();
    }

    private async Task<List<Product>> LoadAsync()
    {
        try
        {
            var data = await http.GetFromJsonAsync<List<Product>>("api/productlist") ?? [];
            _cache = data;
            _loadedAt = DateTime.UtcNow;
            return data;
        }
        finally
        {
            _inFlight = null;
        }
    }
}
