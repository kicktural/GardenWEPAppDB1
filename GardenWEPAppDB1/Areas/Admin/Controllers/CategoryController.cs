using GardenWEPAppDB1.Data;
using GardenWEPAppDB1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GardenWEPAppDB1.Areas.Admin.Controllers
{

    [Area(nameof(Admin))]
    [Authorize(Roles = "Moderator, Super Admin, Admin")]
    public class CategoryController : Controller
    {

        private readonly AppDBContext _dbcontext;

        public CategoryController(AppDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            var category = _dbcontext.CategoriesDB.ToList();
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {

            try
            {
                category.CreateDate = DateTime.Now;
                _dbcontext.CategoriesDB.Add(category);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
                return View();
            }

        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var cate = _dbcontext.CategoriesDB.FirstOrDefault(t => t.Id == id);
            if (cate == null) return NotFound();
            return View(cate);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            _dbcontext.CategoriesDB.Update(category);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            var deta = _dbcontext.CategoriesDB.FirstOrDefault(x => x.Id == id);
            if (deta == null) return NotFound();
            return View(deta);
        }




        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var delete = _dbcontext.CategoriesDB.FirstOrDefault(c => c.Id == id);
            if (delete == null) return NotFound();
            return View(delete);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            _dbcontext.CategoriesDB.Remove(category);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
