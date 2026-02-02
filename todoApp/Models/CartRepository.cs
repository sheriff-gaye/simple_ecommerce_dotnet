namespace todoApp.Models;

public static class CartRepository
{
    private static List<CartItem> _cart = new();

    public static List<CartItem> GetCart() => _cart;

    public static void AddToCart(Product product)
    {
        var item = _cart.FirstOrDefault(x => x.ProductId == product.ProductId);

        if (item == null)
        {
            _cart.Add(new CartItem
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = 1
            });
        }
        else
        {
            item.Quantity++;
        }

        
    }

    public static void Remove(int productId)
    {
        var item = _cart.FirstOrDefault(x => x.ProductId == productId);
        if (item != null) _cart.Remove(item);
    }

    public static decimal Total() => _cart.Sum(x => x.Price * x.Quantity);
}
