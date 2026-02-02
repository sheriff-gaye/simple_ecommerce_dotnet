using Microsoft.AspNetCore.Mvc;
using todoApp.Models;

namespace todoApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string? search)
    {
        var products = ProductsRepository.GetAll();

        if (!string.IsNullOrWhiteSpace(search))
        {
           products = products
    .Where(p =>
        p.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase)
        || (p.Category != null &&
            p.Category.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
    )
    .ToList();

        }

        ViewBag.Search = search;
        return View(products);
    }

    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        var product = ProductsRepository.GetById(id);
        if (product != null)
        {
            CartRepository.AddToCart(product);
            TempData["Success"] = "Product added to cart!";
        }

        return RedirectToAction("Index");
    }

    public IActionResult Cart()
    {
        return View(CartRepository.GetCart());
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int id)
    {
        CartRepository.Remove(id);
        return RedirectToAction("Cart");
    }
}
