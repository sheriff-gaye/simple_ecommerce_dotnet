using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using todoApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace todoApp.Controllers;


public class ProductsController : Controller
{
    private void LoadCategories()
    {
        ViewBag.Categories = CategoriesRepository.GetAllCategories()
            .Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList();
    }

    public IActionResult Index()
    {
        return View(ProductsRepository.GetAll());
    }

    public IActionResult Create()
    {
        LoadCategories();
        return View();
    }

    public IActionResult Edit(int id)
    {
        var product = ProductsRepository.GetById(id);
        if (product == null) return NotFound();

        LoadCategories();
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Save(Product product)
    {
      var category = CategoriesRepository.GetCategoryById(product.CategoryId);
product.Category = category;

        if (!ModelState.IsValid)
        {
            LoadCategories();
            return View(product.ProductId == 0 ? "Create" : "Edit", product);
        }

        if (product.ProductId == 0)
        {
            ProductsRepository.Add(product);
            TempData["Success"] = "Product created successfully!";
        }
        else
        {
            ProductsRepository.Update(product);
            TempData["Success"] = "Product updated successfully!";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        ProductsRepository.Delete(id);
        TempData["Success"] = "Product deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
