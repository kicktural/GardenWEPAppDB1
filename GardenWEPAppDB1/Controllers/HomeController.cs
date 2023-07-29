using GardenWEPAppDB1.Data;
using GardenWEPAppDB1.Models;
using GardenWEPAppDB1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace GardenWEPAppDB1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDBContext _Context;
        public HomeController(ILogger<HomeController> logger, AppDBContext appDBContext, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _Context = appDBContext;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            var Fisrtarticles = _Context.ArticlesDB.Include(c => c.Category).Include(z => z.User).OrderByDescending(a => a.CreateDate).ToList();
            var article = _Context.ArticlesDB.Include(x => x.User).Include(w => w.Category).OrderByDescending(w => w.CreateDate).ToList();
            var PopProduct = _Context.ArticlesDB .OrderByDescending(a => a.ViewCount).ToList();
            HomeVM homeVM = new()
            {
                PopularProduct = PopProduct,
                FirstSlot = Fisrtarticles,
                Articles = article,
            };
            return View(homeVM);
        }

        public async Task<IActionResult> Detail(int id, Article similarArticle, Article Article11)
        {
            if (User.Identity.IsAuthenticated)
            {
            ViewBag.CuurrentUser = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            if (id == null) NotFound();
            var Article =  _Context.ArticlesDB.Include(x => x.Category).Include(x => x.User).Include(z => z.Articletag).ThenInclude(x => x.tag).SingleOrDefault(x => x.Id == id);
            if (Article != null)
            {
             Article.ViewCount += 1;         
            _Context.ArticlesDB.Update(Article);
            _Context.SaveChanges();
            }
            if (Article == null) return NoContent();
            var Article12  = _Context.ArticlesDB.OrderByDescending(x => x.CreateDate).Take(1).ToList();
            var popArt = _Context.ArticlesDB.OrderByDescending(x => x.ViewCount).Take(4).ToList();
            //var nextArticle = _Context.Articles.FirstOrDefault(x => x.Id > article.Id);
            //var prewArticle = _Context.Articles.FirstOrDefault(c => c.Id < article.Id);
            //var similarArticle1 = _Context.Articles.OrderByDescending(x => x.Id).Where(x =>x. CategoryId == article.CategoryId && x.Id != article.Id).Take(2).ToList();
            var similarArticle11 = _Context.ArticlesDB.OrderByDescending(x => x.ViewCount).Where(x =>x.CategoryId == Article.CategoryId).ToList();
            DetailVM detailVM = new()
            {
                articles = Article, 
                PopularArticle = popArt,
                //NextArticle = nextArticle,
                //PrewArticle = prewArticle,
                SimilarArticle = similarArticle11,
                Articles11 = Article12,
            };

            return View(detailVM);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}