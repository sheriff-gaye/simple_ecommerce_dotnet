namespace todoApp.Models;

public static class ProductsRepository
{
    private static List<Product> _products = new()
    {
        new Product
        {
            ProductId = 1,
            ProductName = "Laptop",
            CategoryId = 1,
            Category = CategoriesRepository.GetCategoryById(1),
            Price = 1200,
            Quantity = 5
        },
        new Product
        {
            ProductId = 2,
            ProductName = "Notebook",
            CategoryId = 3,
            Category = CategoriesRepository.GetCategoryById(3),
            Price = 5,
            Quantity = 50
        }
    };

    public static List<Product> GetAll() => _products;

    public static Product? GetById(int id) =>
        _products.FirstOrDefault(p => p.ProductId == id);

    public static void Add(Product product)
    {
        product.ProductId = _products.Any()
            ? _products.Max(p => p.ProductId) + 1
            : 1;

        product.Category = CategoriesRepository.GetCategoryById(product.CategoryId);
        _products.Add(product);
    }

    public static void Update(Product product)
    {
        var existing = GetById(product.ProductId);
        if (existing != null)
        {
            existing.ProductName = product.ProductName;
            existing.CategoryId = product.CategoryId;
            existing.Category = CategoriesRepository.GetCategoryById(product.CategoryId);
            existing.Price = product.Price;
            existing.Quantity = product.Quantity;
        }
    }

    public static void Delete(int id)
    {
        var p = GetById(id);
        if (p != null) _products.Remove(p);
    }
}
