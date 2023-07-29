using GardenWEPAppDB1.Data;
using GardenWEPAppDB1.HelperApple;
using GardenWEPAppDB1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GardenWEPAppDB1.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize (Roles = "Super Admin, Admin")]
    public class AppleController : Controller
    {

        private readonly AppDBContext _dbcontext;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _env;
        public AppleController(AppDBContext dbcontext, IHttpContextAccessor contextAccessor, IWebHostEnvironment env)
        {
            _dbcontext = dbcontext;
            _contextAccessor = contextAccessor;
            _env = env;
        }

        public IActionResult Index()
        {
            var apples = _dbcontext.AppleDBs.Include(x =>x.User).Include(x =>x.ArticleTag).ThenInclude(x =>x.tag).OrderByDescending(x =>x.CreateDate).ToList();
            return View(apples);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var category = _dbcontext.CategoriesDB.ToList();
            var tags = _dbcontext.TagsDB.ToList();
            ViewBag.Categories = new SelectList(category, "Id", "CategoryName");
            ViewBag.Tags = tags;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Apple apple, IFormFile Photo, List<int> tagIdss)
        {

            try
            {
                var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                apple.UserId = userId;
                apple.CreateDate = DateTime.Now;
                //apple.UpdateDate = DateTime.Now;

                apple.PhotoUrl = await FileUpload.saveFileasync(Photo, _env.WebRootPath);

                var category = _dbcontext.CategoriesDB.ToList();
                var tags = _dbcontext.TagsDB.ToList();
                ViewBag.Categories = new SelectList(category, "Id", "CategoryName");
                ViewBag.Tags = tags;
               

                await _dbcontext.AppleDBs.AddAsync(apple);
                await _dbcontext.SaveChangesAsync();

                List<ArticleTag> AppleTags = new();

                for (int i = 0; i < tagIdss.Count; i++)
                {
                    ArticleTag AppleTag = new()
                    {
                        ArticleId = apple.Id,
                        TagId = tagIdss[i],
                    };
                   AppleTags.Add(AppleTag);
                }

                await _dbcontext.AppleDBs.AddRangeAsync(apple);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }

          
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var DeleteId = _dbcontext.AppleDBs.FirstOrDefaultAsync( x => x.Id == id);
            if (DeleteId == null) return NotFound();
            return View(DeleteId);   
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Apple apple)
        {
             _dbcontext.AppleDBs.Remove(apple);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
