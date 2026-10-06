namespace InventoryHub.Web.Models;

// Cocok dengan JSON back-end (GetFromJsonAsync tidak peka huruf besar/kecil).
public record Category(int Id, string Name);
public record Product(int Id, string Name, decimal Price, int Stock, Category Category);
