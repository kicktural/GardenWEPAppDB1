using GardenWEPAppDB1.Data;
using GardenWEPAppDB1.ViewModAPPle;
using GardenWEPAppDB1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenWEPAppDB1.Controllers
{
    public class AppleProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDBContext _Context;
        public AppleProductController(ILogger<HomeController> logger, AppDBContext appDBContext, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _Context = appDBContext;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            var Fisrtarticles = _Context.AppleDBs.Include(c => c.Category).Include(z => z.User).OrderByDescending(a => a.CreateDate).ToList();
            var article = _Context.AppleDBs.Include(x => x.User).Include(w => w.Category).OrderByDescending(w => w.CreateDate).ToList();
            //var PopProduct = _Context.ArticlesDB.OrderByDescending(a => a.ViewCount).ToList();
            HomeVMApple homeVMApple = new()
            {
                Apple = Fisrtarticles,
                FirstSlotApple = article,

            };
            return View(homeVMApple);         
        }

        public IActionResult Detail(int id)
        {
            var Apple = _Context.AppleDBs.Include(x => x.Category).Include(x => x.User).SingleOrDefault(x => x.Id == id);
            if (Apple != null)
            {
             Apple.ViewCount += 1;
            _Context.AppleDBs.Update(Apple);
            _Context.SaveChanges();
            }
            var PopulerApple = _Context.AppleDBs.OrderByDescending(x => x.ViewCount).Take(3).ToList();
            var NewProduct = _Context.AppleDBs.OrderByDescending(z =>z.CreateDate).Take(1).ToList();

            DetailVMApple detailVM = new()
            {
                Apple = Apple,
                PopularApple = PopulerApple,
                NewProduct = NewProduct,
            };
            return View(detailVM);
        }
    }
}
