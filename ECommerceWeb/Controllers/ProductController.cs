using System;
using Microsoft.AspNetCore.Mvc;
using ECommerce.DataAccess;
using ECommerce.Models;
using ECommerce.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerce.Models.ViewModels;

namespace ECommerceWeb.Controllers
{

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index()
        {

            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

            };

            if (id == null || id == 0)
            {
                //create product
                //Transfer data from controller to view
                // ViewBag.CategoryList = CategoryList;
                // ViewData["CategoryList"] = CategoryList;
                return View(productVM);
            }
            else
            {
                //update product
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                //upload image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var coverTypeFromDbFisrt = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDbFisrt == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDbFisrt);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted succesfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return Json(new { data = productList });
        }
        #endregion
    }
}