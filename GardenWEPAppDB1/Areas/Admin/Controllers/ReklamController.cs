using GardenWEPAppDB1.Data;
using Microsoft.AspNetCore.Mvc;

namespace GardenWEPAppDB1.Areas.Admin.Controllers
{

    [Area(nameof(Admin))]
    public class ReklamController : Controller
    {
        private readonly AppDBContext _dbcontext;
        private readonly IWebHostEnvironment _env;
        public ReklamController(AppDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
