using Microsoft.AspNetCore.Mvc;
using todoApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace todoApp.Controllers;


public class CategoriesController : Controller
{
    public IActionResult Index()
    {
        var categories = CategoriesRepository.GetAllCategories();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }


    public IActionResult Edit(int? id)
    {
        var category = CategoriesRepository.GetCategoryById(id ?? 0);
        return View(category);
    }

    public IActionResult Update(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View("Edit", category);
        }

        if (category.CategoryId == 0)
        {
            CategoriesRepository.AddCategory(category);
        }
        else
        {
            CategoriesRepository.UpdateCategory(category);
            TempData["Success"] = "Category updated successfully!";
        }

        return RedirectToAction(nameof(Index));
    }


    public IActionResult Delete(int id)
    {
        CategoriesRepository.DeleteCategory(id);
        TempData["Success"] = "Category deleted successfully!";
        return RedirectToAction(nameof(Index));
    }


}
