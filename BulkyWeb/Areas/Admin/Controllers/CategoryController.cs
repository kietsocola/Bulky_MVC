
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; // step 10 to read data in Categories Table and show in View
        public CategoryController(IUnitOfWork unitOfWork) // step 10, step 11 is code for index view to show categori and display order
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList(); // step 10
            return View(objCategoryList); // paste objCategoryList in here to call it in index.cshtml
        }
        public IActionResult Create() // step 14, after add bootstrap then create the Button Create
        {
            return View();
        }
        [HttpPost] // important
        public IActionResult Create(Category obj) // step 15
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDB = _unitOfWork.Category.Get(u => u.Id == id);
            if (CategoryFromDB == null)
            {
                return NotFound();
            }
            return View(CategoryFromDB);
        }
        [HttpPost] // important
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDB = _unitOfWork.Category.Get(u => u.Id == id);
            if (CategoryFromDB == null)
            {
                return NotFound();
            }
            return View(CategoryFromDB);
        }
        [HttpPost, ActionName("Delete")] // important
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
