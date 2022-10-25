using System;
using Microsoft.AspNetCore.Mvc;
using ECommerceWeb.Data;
using ECommerceWeb.Models;

namespace ECommerceWeb.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

    }
}