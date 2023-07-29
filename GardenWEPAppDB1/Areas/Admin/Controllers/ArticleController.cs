using GardenWEPAppDB1.Data;
using GardenWEPAppDB1.Helper;
using GardenWEPAppDB1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GardenWEPAppDB1.Areas.Admin.Controllers
{

    [Area(nameof(Admin))]
    [Authorize(Roles = "Super Admin")]
    public class ArticleController : Controller
    {
        private readonly AppDBContext _DBContext;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _env;
        public ArticleController(IHttpContextAccessor contextAccessor, IWebHostEnvironment env, AppDBContext appDBContext)
        {

            _contextAccessor = contextAccessor;

            _env = env;
            _DBContext = appDBContext;
        }

        public IActionResult Index()
        {
            var articles = _DBContext.ArticlesDB.Include(X => X.User).Include(x => x.Articletag).ThenInclude(the => the.tag).OrderByDescending(x =>x.CreateDate).ToList();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {

            var categories = _DBContext.CategoriesDB.ToList();
            var tags = _DBContext.TagsDB.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            ViewBag.Tags = tags;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article, IFormFile Photo, List<int> tagIds)
        {

            try
            {
                var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                article.UserId = userId;

                article.CreateDate = DateTime.Now;
                article.SeoUrl = article.Title.ReplaceInvalidChars();

                article.PhotoUrl = await FileUpload.saveFileasync(Photo, _env.WebRootPath);


                var categories = await _DBContext.CategoriesDB.ToListAsync();
                var tags = await _DBContext.TagsDB.ToListAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
                ViewData["Tags"] = tags;

                
                await _DBContext.ArticlesDB.AddAsync(article);
                await _DBContext.SaveChangesAsync();
                
                List<ArticleTag> articleTags = new();

                for (int i = 0; i < tagIds.Count; i++)
                {
                    ArticleTag articleTag = new()
                    {
                        ArticleId = article.Id,
                        TagId = tagIds[i],
                    };
                    articleTags.Add(articleTag);
                }
                await _DBContext.articleTagsDB.AddRangeAsync(articleTags);
                await _DBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var article = await _DBContext.ArticlesDB.Include(z => z.Articletag).ThenInclude(y => y.tag).FirstOrDefaultAsync(x => x.Id == id);
            if (article == null) return NotFound();
            var categories = await _DBContext.CategoriesDB.ToListAsync();
            var tags = _DBContext.TagsDB.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            ViewData["Tags"] = tags;
            return View(article);
        }

        [RequireHttps]
        [HttpPost]
        public async Task<IActionResult> Edit(Article article, List<int> tagIdss, IFormFile photo)
        {
            try
            {

                article.CreateDate = DateTime.Now;
                article.SeoUrl = article.Title.ReplaceInvalidChars();

                if (photo != null)
                {
                    article.PhotoUrl = await photo.saveFileasync(_env.WebRootPath);
                }


                var art = _DBContext.articleTagsDB.Where(x => x.ArticleId == article.Id).ToList();
                _DBContext.articleTagsDB.RemoveRange(art);
                await _DBContext.SaveChangesAsync();

                List<ArticleTag> articleTags = new();

                for (int i = 0; i < tagIdss.Count; i++)
                {
                    ArticleTag articleTag = new()
                    {
                        ArticleId = article.Id,
                        TagId = tagIdss[i],
                    };
                    articleTags.Add(articleTag);
                }
                await _DBContext.articleTagsDB.AddRangeAsync(articleTags);


                _DBContext.ArticlesDB.Update(article);
                await _DBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var article = await _DBContext.ArticlesDB.FirstOrDefaultAsync(x => x.Id == id);
            if (article == null) return NotFound();
            return View(article);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Article article)
        {
            //var articles = await _DBContext.Articles.FirstOrDefaultAsync(x => x.Id == id);
            _DBContext.ArticlesDB.Remove(article);
            await _DBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
