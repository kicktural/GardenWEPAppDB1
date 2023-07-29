using GardenWEPAppDB1.Data;
using GardenWEPAppDB1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GardenWEPAppDB1.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Moderator, Super Admin, Admin")]
    public class TagController : Controller
    {

        private readonly AppDBContext _context;

        public TagController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tags = _context.TagsDB.ToList();
            return View(tags);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            var tags = _context.TagsDB.FirstOrDefault(x => x.TagName == tag.TagName);
            if (tags != null)
            {
                ModelState.AddModelError("Error", "You cannot write a second Tagname");
                return View();
            }
            if (tag == null) return NotFound();
            if (ModelState.IsValid)
            {
                tag.CreateDate = DateTime.Now;
                await _context.TagsDB.AddAsync(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            var tags = _context.TagsDB.FirstOrDefault(x => x.Id == id);
            if (tags == null) return NotFound();
            return View(tags);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Tag tag)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(tag);
                }

                tag.UpdateDate = DateTime.Now;
                _context.TagsDB.Update(tag);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.Errors = ex.Message;
                throw;
            }

        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            var tags = _context.TagsDB.FirstOrDefault(x => x.Id == id);
            if (tags == null) return NotFound();
            return View(tags);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var tags = _context.TagsDB.FirstOrDefault(x => x.Id == id);
            if (tags == null) return NotFound();
            return View(tags);

        }

        [HttpPost]
        public IActionResult Delete(Tag tag)
        {
            tag.DeleteDate = DateTime.Now;
            _context.TagsDB.Remove(tag);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
