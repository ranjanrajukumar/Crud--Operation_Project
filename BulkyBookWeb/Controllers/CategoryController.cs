using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
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
            //var objCategoryList=_db.Categories.ToList();
            //return View();

            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);


        }

        //Get
        public IActionResult Create()
        {
           return View();

        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "error");
            }

            if (ModelState.IsValid)
                {
                    _db.Categories.Add(obj);
                    _db.SaveChanges(); //go to database
                     TempData["success"] = "Category Create successfully.";
                    return RedirectToAction("Index", "Category");
                }
            
            return View(obj);   
        }


        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(Id);
            //var categoryFromDb=_db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFromDbFirst=_db.Categories.SingleOrDefault(c => c.Id == id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);

        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "error");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges(); //go to database
                TempData["success"] = "Category Update successfully.";
                return RedirectToAction("Index", "Category");
            }

            return View(obj);
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(Id);
            //var categoryFromDb=_db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFromDbFirst=_db.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);

        }

        //Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.Categories.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
                _db.Categories.Remove(obj);
                _db.SaveChanges(); //go to database
            TempData["success"] = "Category Delete successfully.";
            return RedirectToAction("Index");
            

        }
    }
}
